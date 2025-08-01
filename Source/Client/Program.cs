using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Concurrent;
using System.Diagnostics;
using static Core.Global.Command;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using System;
using System.Reflection;
using Client.Game.Objects;
using SDL2;

namespace Client
{

    public class GameClient : Microsoft.Xna.Framework.Game
    {

        public static GraphicsDeviceManager Graphics;
        public static Microsoft.Xna.Framework.Graphics.SpriteBatch SpriteBatch;

        public static readonly ConcurrentDictionary<string, Texture2D> TextureCache =
            new ConcurrentDictionary<string, Texture2D>();

        public static readonly ConcurrentDictionary<string, GfxInfo> GfxInfoCache =
            new ConcurrentDictionary<string, GfxInfo>();

        public static int TextureCounter;

        public readonly BlendState MultiplyBlendState = new BlendState();

        private static int _gameFps;
        private static readonly object FpsLock = new object();

        // Safely set FPS with a lock
        public static void SetFps(int newFps)
        {
            lock (FpsLock)
                _gameFps = newFps;
        }

        // Safely get FPS with a lock
        public static int GetFps()
        {
            lock (FpsLock)
                return _gameFps;
        }

        // State tracking variables
        // Shared keyboard and mouse states for cross-thread access
        public static KeyboardState CurrentKeyboardState;
        public static KeyboardState PreviousKeyboardState;

        public static MouseState CurrentMouseState;
        public static MouseState PreviousMouseState;

        // Keep track of the key states to avoid repeated input
        public static readonly Dictionary<Keys, bool> KeyStates = new Dictionary<Keys, bool>();

        // Define a dictionary to store the last time a key was processed
        public static Dictionary<Keys, DateTime> KeyRepeatTimers = new Dictionary<Keys, DateTime>();

        // Minimum interval (in milliseconds) between repeated key inputs
        private const byte KeyRepeatInterval = 200;

        static float DPIScale = 96;

        // Lock object to ensure thread safety
        public static readonly object InputLock = new object();

        // Track the previous scroll value to compute delta
        private static readonly object ScrollLock = new object();

        private TimeSpan elapsedTime = TimeSpan.Zero;

        public static RenderTarget2D RenderTarget;
        public static Texture2D TransparentTexture;
        public static Texture2D PixelTexture;

        public static bool IsLoaded;

        // Add a timer to prevent spam
        private static DateTime lastInputTime = DateTime.MinValue;
        private const int inputCooldown = 250;

        // Handle Escape key to toggle menus
        private static DateTime lastMouseClickTime = DateTime.MinValue;
        private const int mouseClickCooldown = 250;
        private static DateTime lastSearchTime = DateTime.MinValue;

        // Ensure this class exists to store graphic info
        public class GfxInfo
        {
            public int Width;
            public int Height;
        }

        public static GfxInfo GetGfxInfo(string key)
        {
            // Check if the key does not end with ".gfxext" and append if needed
            if (!key.EndsWith(GameState.GfxExt, StringComparison.OrdinalIgnoreCase))
            {
                key += GameState.GfxExt;
            }

            // Retrieve the texture
            var texture = GetTexture(key);

            GfxInfo result = null;
            if (!GfxInfoCache.TryGetValue(key, out result))
            {
                // Log or handle the case where the key is not found in the cache
                Debug.WriteLine($"Warning: GfxInfo for key '{key}' not found in cache.");
                return null;
            }

            return result;
        }
        // Get DPI scale factor using SDL2
        private float GetDpiScale()
        {
            float dpi = 96.0f; // Default DPI (standard for Windows)
            int display = SDL.SDL_GetWindowDisplayIndex(Window.Handle);
            if (SDL.SDL_GetDisplayDPI(display, out float ddpi, out float hdpi, out float vdpi) == 0)
            {
                // Use diagonal DPI for scaling
                dpi = ddpi;
            }
            // Calculate scale factor (96 is standard DPI)
            return dpi / 96.0f;
        }

        public GameClient()
        {
            General.GetResolutionSize(SettingsManager.Instance.Resolution, ref GameState.ResolutionWidth,
                ref GameState.ResolutionHeight);

            Graphics = new GraphicsDeviceManager(this);
            
            DPIScale = GetDpiScale();

            // Set basic properties for GraphicsDeviceManager
            ref var withBlock = ref Graphics;
            withBlock.GraphicsProfile = GraphicsProfile.Reach;
            withBlock.IsFullScreen = SettingsManager.Instance.Fullscreen;
            withBlock.PreferredBackBufferWidth = GameState.ResolutionWidth * (int)DPIScale;
            withBlock.PreferredBackBufferHeight = GameState.ResolutionHeight * (int)DPIScale;
            withBlock.SynchronizeWithVerticalRetrace = SettingsManager.Instance.Vsync;
            IsFixedTimeStep = false;
            withBlock.PreferHalfPixelOffset = true;
            withBlock.PreferMultiSampling = true;
            
            // Add handler for PreparingDeviceSettings
            Graphics.PreparingDeviceSettings += (sender, args) =>
            {
                args.GraphicsDeviceInformation.PresentationParameters.RenderTargetUsage =
                    Microsoft.Xna.Framework.Graphics.RenderTargetUsage.PreserveContents;
                args.GraphicsDeviceInformation.PresentationParameters.MultiSampleCount = 8;
            };

#if DEBUG
            IsMouseVisible = true;
#endif
            Content.RootDirectory = "Content";

            // Hook into the Exiting event to handle window close
            Exiting += OnWindowClose;
            Graphics.DeviceReset += (_, __) => OnDeviceReset();
        }

        protected override void Initialize()
        {
            Window.Title = SettingsManager.Instance.GameName;

            // Create the RenderTarget2D with the same size as the screen
            RenderTarget = new RenderTarget2D(Graphics.GraphicsDevice,
                Graphics.GraphicsDevice.PresentationParameters.BackBufferWidth,
                Graphics.GraphicsDevice.PresentationParameters.BackBufferHeight, false,
                Graphics.GraphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);

            // Apply changes to GraphicsDeviceManager
            try
            {
                Graphics.ApplyChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GraphicsDevice initialization failed: {ex.Message}");
                throw;
            }

            base.Initialize();
        }

        static void LoadFonts()
        {
            // Get all defined font enum values except None (assumed to be 0)
            var fontValues = Enum.GetValues(typeof(Core.Font));
            for (int i = 1; i < fontValues.Length; i++)
                Text.Fonts[(Core.Font)fontValues.GetValue(i)] = LoadFont(Core.Path.Fonts, (Core.Font)fontValues.GetValue(i));
        }

        protected override void LoadContent()
        {
            SpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(GraphicsDevice);

            TransparentTexture = new Texture2D(GraphicsDevice, 1, 1);
            TransparentTexture.SetData(new Color[] { Color.White });
            PixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            PixelTexture.SetData(new Color[] { Color.White });

            LoadFonts();
            General.Startup();
            IsLoaded = true;
        }

        public static SpriteFont LoadFont(string path, Core.Font font)
        {
            return General.Client.Content.Load<SpriteFont>(System.IO.Path.Combine(path, ((int)font).ToString()));
        }

        public static Color ToXnaColor(System.Drawing.Color drawingColor)
        {
            return new Color(drawingColor.R, drawingColor.G, drawingColor.B, drawingColor.A);
        }

        public static System.Drawing.Color ToDrawingColor(Color xnaColor)
        {
            return System.Drawing.Color.FromArgb(xnaColor.A, xnaColor.R, xnaColor.G, xnaColor.B);
        }

       public static Rectangle GetAspectRatio(int x, int y, int screenWidth, int screenHeight, int texWidth, int texHeight, float targetAspect)
        {
            float newAspect = (float)screenWidth / screenHeight;

            int width, height;

            // Scale texture to match the target aspect ratio
            if (newAspect > targetAspect)
            {
                // Texture is wider than target: scale to fit width, adjust height
                width = texWidth;
                height = (int)(width / targetAspect);
            }
            else
            {
                // Texture is taller than target: scale to fit height, adjust width
                height = texHeight;
                width = texWidth;
            }

            // Calculate scaling factors
            float scaleX = (float)width / texWidth;
            float scaleY = (float)height / texHeight;

            // Adjust dimensions to fit within screen boundaries
            if (width > screenWidth)
            {
                width = screenWidth;
                height = (int)(width / targetAspect);
                scaleX = (float)width / texWidth;
                scaleY = (float)height / texHeight;
            }
    
            if (height > screenHeight)
            {
                height = screenHeight;
                width = (int)(height * targetAspect);
                scaleX = (float)width / texWidth;
                scaleY = (float)height / texHeight;
            }

            int destX = 0;
            int destY = 0;

            if (newAspect != targetAspect)
            {
                // Calculate position offset based on size difference
                destX = x + (int)((screenWidth - width) / 2 * scaleX);
                destY = y + (int)((screenHeight - height) / 2 * scaleY);
            }
            else
            {
                // Center the texture in the screen
                destX = x;
                destY = y;
            }

            return new Rectangle(destX, destY, width, height);
        }
        
        public static void RenderTexture(ref string path, int dX, int dY, int sX, int sY, int dW, int dH, int sW = 1,
            int sH = 1, float alpha = 1.0f, byte red = 255, byte green = 255, byte blue = 255)
        {
            path = Core.Path.EnsureFileExtension(path);
            
            // Retrieve the texture
            var texture = GetTexture(path);
            
            if (texture is null)
            {
                return;
            }
            
            int targetWidth = 0, targetHeight = 0;
            General.GetResolutionSize(SettingsManager.Instance.Resolution, ref targetWidth, ref targetHeight);
            var targetAspect = (float)targetWidth / targetHeight;
            
            var destRect = GetAspectRatio(dX, dY, Graphics.PreferredBackBufferWidth * (int)DPIScale, Graphics.PreferredBackBufferHeight * (int)DPIScale, dW, dH, targetAspect);
            var srcRect = new Rectangle(sX, sY, sW, sH);
            var color = new Color(red, green, blue, (byte)255) * alpha;
            
            SpriteBatch.Draw(texture, destRect, srcRect, color);
        }

        public static Texture2D GetTexture(string path)
        {
            if (!TextureCache.ContainsKey(path))
            {
                var texture = LoadTexture(path);
                return texture;
            }

            return TextureCache[path];
        }

        public static Texture2D LoadTexture(string path)
        {
            try
            {
                // Check if the key does not end with ".gfxext" and append if needed  
                if (!path.EndsWith(GameState.GfxExt, StringComparison.OrdinalIgnoreCase))
                {
                    path += GameState.GfxExt;
                }

                // Open the file stream with FileShare.Read to allow other processes to read the file  
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var texture = Texture2D.FromStream(Graphics.GraphicsDevice, stream);

                    // Cache graphics information  
                    var gfxInfo = new GfxInfo()
                    {
                        Width = texture.Width,
                        Height = texture.Height
                    };
                    GfxInfoCache.TryAdd(path, gfxInfo);

                    TextureCache[path] = texture;

                    return texture;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading texture from {path}: {ex.Message}");
                return null;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if (GameState.InGame == true)
            {
                Render_Game();
            }
            else
            {
                Render_Menu();
            }

            SpriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Update(GameTime gameTime)
        {
            // Ignore input if the window is minimized or inactive
            if ((!IsActive || Window.ClientBounds.Width == 0) | Window.ClientBounds.Height == 0)
            {
                ResetInputStates();
                base.Update(gameTime);
                return;
            }

            lock (InputLock)
            {
                UpdateMouseCache();
                UpdateKeyCache();
                ProcessInputs();
            }

            if (IsKeyStateActive(Keys.F12))
            {
                TakeScreenshot();
            }

            SetFps(_gameFps + 1);
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime.TotalSeconds >= 1d)
            {
                Console.WriteLine("FPS: " + GetFps());
                SetFps(0);
                elapsedTime = TimeSpan.Zero;
            }

            Loop.Game();

            base.Update(gameTime);
        }

        // Reset keyboard and mouse states
        private static void ResetInputStates()
        {
            CurrentKeyboardState = new KeyboardState();
            PreviousKeyboardState = new KeyboardState();
            CurrentMouseState = new MouseState();
            PreviousMouseState = new MouseState();
        }

        private static void UpdateKeyCache()
        {
            // Get the current keyboard state
            var keyboardState = Keyboard.GetState();

            // Update the previous and current states
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = keyboardState;
        }

        private static void UpdateMouseCache()
        {
            // Get the current mouse state
            var mouseState = Mouse.GetState();

            // Update the previous and current states
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = mouseState;
        }

        public static int GetMouseScrollDelta()
        {
            lock (ScrollLock)
                // Calculate the scroll delta between the previous and current states
                return CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue;
        }

        public static bool IsKeyStateActive(Keys key)
        {
            if (CanProcessKey(key) == true)
            {
                // Check if the key is down in the current keyboard state
                return CurrentKeyboardState.IsKeyDown(key);
            }

            return default;
        }

        public static Tuple<int, int> GetMousePosition()
        {
            // Return the current mouse position as a Tuple
            return new Tuple<int, int>(CurrentMouseState.X, CurrentMouseState.Y);
        }

        public static bool IsMouseButtonDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                {
                    return CurrentMouseState.LeftButton == ButtonState.Pressed;
                }
                case MouseButton.Right:
                {
                    return CurrentMouseState.RightButton == ButtonState.Pressed;
                }
                case MouseButton.Middle:
                {
                    return CurrentMouseState.MiddleButton == ButtonState.Pressed;
                }

                default:
                {
                    return false;
                }
            }
        }

        public static bool IsMouseButtonUp(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                {
                    return CurrentMouseState.LeftButton == ButtonState.Released;
                }
                case MouseButton.Right:
                {
                    return CurrentMouseState.RightButton == ButtonState.Released;
                }
                case MouseButton.Middle:
                {
                    return CurrentMouseState.MiddleButton == ButtonState.Released;
                }

                default:
                {
                    return false;
                }
            }
        }

        public static void ProcessInputs()
        {
            // Get the mouse position from the cache
            var mousePos = GetMousePosition();
            int mouseX = mousePos.Item1;
            int mouseY = mousePos.Item2;

            // Convert adjusted coordinates to game world coordinates
            GameState.CurX = (int)Math.Round(GameState.TileView.Left +
                                             Math.Floor((mouseX + GameState.Camera.Left) / GameState.SizeX));
            GameState.CurY = (int)Math.Round(GameState.TileView.Top +
                                             Math.Floor((mouseY + GameState.Camera.Top) / GameState.SizeY));

            // Store raw mouse coordinates for interface interactions
            GameState.CurMouseX = mouseX;
            GameState.CurMouseY = mouseY;

            // Check for action keys
            GameState.VbKeyControl = CurrentKeyboardState.IsKeyDown(Keys.LeftControl);
            GameState.VbKeyShift = CurrentKeyboardState.IsKeyDown(Keys.LeftShift);

            if (IsKeyStateActive(Keys.F8))
            {
                var uiPath = System.IO.Path.Combine(Core.Path.Skins, SettingsManager.Instance.Skin + ".cs");

                if (!System.IO.File.Exists(uiPath))
                {
                    Console.WriteLine($"File not found: {uiPath}");
                }
                else
                { 
                    // Open with default text editor
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = uiPath,
                        UseShellExecute = true
                    });
                }
            }

            if (IsKeyStateActive(Keys.F5))
            {
                UI.Load();
                Gui.Init();

            }

            // Handle Escape key to toggle menus
            if (IsKeyStateActive(Keys.Escape))
            {
                if (GameState.InMenu == true)
                    return;

                // Hide options screen
                if (Gui.Windows[Gui.GetWindowIndex("winOptions")].Visible == true)
                {
                    Gui.HideWindow(Gui.GetWindowIndex("winOptions"));
                    Gui.CloseComboMenu();
                    return;
                }

                // hide/show chat window
                if (Gui.Windows[Gui.GetWindowIndex("winChat")].Visible == true)
                {
                    Gui.Windows[Gui.GetWindowIndex("winChat")].Controls[(int)Gui.GetControlIndex("winChat", "txtChat")]
                        .Text = "";
                    Gui.HideChat();
                    return;
                }

                if (Gui.Windows[Gui.GetWindowIndex("winEscMenu")].Visible == true)
                {
                    Gui.HideWindow(Gui.GetWindowIndex("winEscMenu"));
                    return;
                }

                if (Gui.Windows[Gui.GetWindowIndex("winShop")].Visible == true)
                {
                    Shop.CloseShop();
                    return;
                }

                if (Gui.Windows[Gui.GetWindowIndex("winBank")].Visible == true)
                {
                    Bank.CloseBank();
                    return;
                }

                if (Gui.Windows[Gui.GetWindowIndex("winTrade")].Visible == true)
                {
                    Trade.SendDeclineTrade();
                    return;
                }

                if (Gui.Windows[Gui.GetWindowIndex("winInventory")].Visible == true)
                {
                    Gui.HideWindow(Gui.GetWindowIndex("winInventory"));
                    return;
                }

                if (Gui.Windows[Gui.GetWindowIndex("winCharacter")].Visible == true)
                {
                    Gui.HideWindow(Gui.GetWindowIndex("winCharacter"));
                    return;
                }

                if (Gui.Windows[Gui.GetWindowIndex("winSkills")].Visible == true)
                {
                    Gui.HideWindow(Gui.GetWindowIndex("winSkills"));
                    return;
                }

                // show them
                if (Gui.Windows[Gui.GetWindowIndex("winChat")].Visible == false)
                {
                    Gui.ShowWindow(Gui.GetWindowIndex("winEscMenu"), true);
                    return;
                }
            }

            if (CurrentKeyboardState.IsKeyDown(Keys.Space))
            {
                GameLogic.CheckMapGetItem();
            }

            if (CurrentKeyboardState.IsKeyDown(Keys.Insert))
            {
                NetworkSend.SendRequestAdmin();
            }

            HandleMouseInputs();
            HandleActiveWindowInput();
            HandleTextInput();

            if (GameState.InGame)
            {
                // Check for movement keys
                UpdateMovementKeys();

                HandleHotbarInput();

                // Exit if escape menu is open
                if (IsWindowVisible("winEscMenu"))
                    return;

                // Check for input cooldown
                if (!IsInputCooldownElapsed())
                    return;

                // Process toggle actions
                HandleWindowToggle(Keys.I, "winInventory", Gui.btnMenu_Inv);
                HandleWindowToggle(Keys.C, "winCharacter", Gui.btnMenu_Char);
                HandleWindowToggle(Keys.K, "winSkills", Gui.btnMenu_Skills);

                // Handle chat input
                if (CurrentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    if (IsWindowVisible("winChatSmall"))
                    {
                        Gui.ShowChat();
                        GameState.inSmallChat = false;
                    }
                    else
                    {
                        GameLogic.HandlePressEnter();
                    }

                    UpdateLastInputTime();
                }
            }
        }

        // Helper methods
        private static void UpdateMovementKeys()
        {
            GameState.DirUp = CurrentKeyboardState.IsKeyDown(Keys.W) | CurrentKeyboardState.IsKeyDown(Keys.Up);
            GameState.DirDown = CurrentKeyboardState.IsKeyDown(Keys.S) | CurrentKeyboardState.IsKeyDown(Keys.Down);
            GameState.DirLeft = CurrentKeyboardState.IsKeyDown(Keys.A) | CurrentKeyboardState.IsKeyDown(Keys.Left);
            GameState.DirRight = CurrentKeyboardState.IsKeyDown(Keys.D) | CurrentKeyboardState.IsKeyDown(Keys.Right);
        }

        private static bool IsWindowVisible(string windowName)
        {
            return Gui.Windows[Gui.GetWindowIndex(windowName)].Visible;
        }

        private static bool IsInputCooldownElapsed()
        {
            return (DateTime.Now - lastInputTime).TotalMilliseconds >= inputCooldown;
        }

        private static bool IsSeartchCooldownElapsed()
        {
            return (DateTime.Now - lastSearchTime).TotalMilliseconds >= inputCooldown;
        }

        private static void UpdateLastInputTime()
        {
            lastInputTime = DateTime.Now;
        }

        private static void HandleWindowToggle(Keys key, string windowName, Action toggleAction)
        {
            if (CurrentKeyboardState.IsKeyDown(key) && !IsWindowVisible("winChat"))
            {
                toggleAction.Invoke();
                UpdateLastInputTime();
            }
        }

        private static void HandleActiveWindowInput()
        {
            Keys key;

            // Check if there is an active window and that it is visible.
            if (Gui.ActiveWindow > 0L && Gui.Windows[Gui.ActiveWindow].Visible)
            {
                // Check if an active control exists.
                if (Gui.Windows[Gui.ActiveWindow].ActiveControl > 0)
                {
                    // Get the active control.
                    var activeControl = Gui.Windows[Gui.ActiveWindow]
                        .Controls[Gui.Windows[Gui.ActiveWindow].ActiveControl];

                    // Check if the Enter key is active and can be processed.
                    if (IsKeyStateActive(Keys.Enter))
                    {
                        // Handle Enter: Call the control's callback or activate a new control.
                        if (activeControl.CallBack[(int)ControlState.FocusEnter] is not null)
                        {
                            activeControl.CallBack[(int)ControlState.FocusEnter].Invoke();
                        }
                        // If no callback, activate a new control.
                        else if (Gui.ActivateControl() == 0)
                        {
                            Gui.ActivateControl(0, false);
                        }
                    }

                    // Check if the Tab key is active and can be processed
                    if (IsKeyStateActive(Keys.Tab))
                    {
                        // Handle Tab: Switch to the next control.
                        if (Gui.ActivateControl() == 0)
                        {
                            Gui.ActivateControl(0, false);
                        }
                    }
                }
            }
        }

        // Handles the hotbar key presses using KeyboardState
        private static void HandleHotbarInput()
        {
            if (GameState.inSmallChat)
            {
                // Iterate through hotbar slots and check for corresponding keys
                for (int i = 0; i < Constant.MAX_HOTBAR; i++)
                {
                    // Check if the corresponding hotbar key is pressed
                    if (CurrentKeyboardState.IsKeyDown((Keys)((int)Keys.D0 + i)))
                    {
                        NetworkSend.SendUseHotbarSlot(i);
                        return; // Exit once the matching slot is used
                    }
                }
            }
        }

        private static void HandleTextInput()
        {
            // Iterate over all pressed keys  
            foreach (Keys key in CurrentKeyboardState.GetPressedKeys())
            {
                // Check for special keys and skip processing
                if (key == Keys.Tab || key == Keys.LeftShift || key == Keys.RightShift || key == Keys.LeftControl ||
                    key == Keys.RightControl || key == Keys.LeftAlt || key == Keys.RightAlt)
                {
                    continue;
                }

                if (IsKeyStateActive(key))
                {
                    // Handle Backspace key separately  
                    if (key == Keys.Back)
                    {
                        var activeControl = Gui.GetActiveControl();

                        if (activeControl is not null && activeControl.Visible && activeControl.Text.Length > 0)
                        {
                            // Modify the text and update it back in the window  
                            activeControl.Text = activeControl.Text.Substring(0, activeControl.Text.Length - 1);
                            Gui.UpdateActiveControl(activeControl);
                        }

                        continue; // Move to the next key  
                    }

                    // Convert key to a character, considering Shift key  
                    char? character = ConvertKeyToChar(key, CurrentKeyboardState.IsKeyDown(Keys.LeftShift));

                    // If the character is valid, update the active control's text  
                    if (character.HasValue)
                    {
                        var activeControl = Gui.GetActiveControl();

                        if (activeControl is not null && activeControl.Visible && activeControl.Enabled)
                        {
                            string text = activeControl.Text + Conversions.ToString(character.Value);
                            if (Text.GetTextWidth(text) < activeControl.Width)
                            {
                                // Append character to the control's text  
                                activeControl.Text += Conversions.ToString(character.Value);
                                Gui.UpdateActiveControl(activeControl);
                                continue; // Move to the next key  
                            }
                        }
                    }

                    KeyStates.Remove(key);
                    KeyRepeatTimers.Remove(key);
                }
            }
        }

        // Check if the key can be processed (with interval-based repeat logic)
        private static bool CanProcessKey(Keys key)
        {
            var now = DateTime.Now;
            if (CurrentKeyboardState.IsKeyDown(key))
            {
                if (IsKeyPressedOnce(key) || !KeyRepeatTimers.ContainsKey(key) ||
                    (now - KeyRepeatTimers[key]).TotalMilliseconds >= KeyRepeatInterval)
                {
                    // If the key is released, remove it from KeyStates and reset the timer
                    KeyStates.Remove(key);
                    KeyRepeatTimers.Remove(key);
                    KeyRepeatTimers[key] = now; // Update the timer for the key
                    return true;
                }
            }

            return false;
        }

        private static bool IsKeyPressedOnce(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key) && PreviousKeyboardState.IsKeyUp(key);
        }

        // Convert a key to a character (if possible)
        private static char ConvertKeyToChar(Keys key, bool shiftPressed)
        {
            // Handle alphabetic keys
            if (key >= Keys.A && key <= Keys.Z)
            {
                char baseChar = Strings.ChrW(Strings.AscW('A') + ((int)key - (int)Keys.A));
                return shiftPressed ? baseChar : char.ToLower(baseChar);
            }

            // Handle numeric keys (0-9)
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                char digit = Strings.ChrW(Strings.AscW('0') + ((int)key - (int)Keys.D0));
                return shiftPressed ? General.GetShiftedDigit(digit) : digit;
            }

            // Handle space key
            if (key == Keys.Space)
                return ' ';

            // Handle the "/" character (typically mapped to OemQuestion)
            if (key == Keys.OemQuestion)
            {
                return shiftPressed ? '?' : '/';
            }

            // Ignore unsupported keys (e.g., function keys, control keys)
            return default;
        }

        private static void HandleMouseInputs()
        {
            HandleMouseClick();
            HandleScrollWheel();
        }

        private static void HandleScrollWheel()
        {
            // Handle scroll wheel (assuming delta calculation happens elsewhere)
            int scrollValue = GetMouseScrollDelta();
            if (scrollValue > 0)
            {
                GameLogic.ScrollChatBox(0); // Scroll up

                if (GameState.MyEditorType == (int)EditorType.Map)
                {
                    if (GameClient.CurrentKeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        if (GameState.CurLayer > 0)
                        {
                            GameState.CurLayer -= 1;
                        }
                    }

                    else if (GameState.CurTileset > 0)
                    {
                        GameState.CurTileset -= 1;
                    }

                }
            }
            else if (scrollValue < 0)
            {
                GameLogic.ScrollChatBox(1); // Scroll down

                if (GameState.MyEditorType == (int)EditorType.Map)
                {
                    if (GameClient.CurrentKeyboardState.IsKeyDown(Keys.LeftShift))
                    {
                        if (GameState.CurLayer < Enum.GetValues(typeof(MapLayer)).Length)
                        {
                            GameState.CurLayer += 1;
                        }
                    }
                    else if (GameState.CurTileset < GameState.NumTileSets)
                    {
                        GameState.CurTileset += 1;
                    }
                }

                if (scrollValue != 0)
                {
                    Gui.HandleInterfaceEvents(ControlState.MouseScroll);
                }
            }
        }

        private static void HandleMouseClick()
        {
            int currentTime = Environment.TickCount;

            // Handle MouseMove event when the mouse moves
            if (CurrentMouseState.X != PreviousMouseState.X || CurrentMouseState.Y != PreviousMouseState.Y)
            {
                Gui.HandleInterfaceEvents(ControlState.MouseMove);
            }

            // Check for MouseDown event (button pressed)
            if (IsMouseButtonDown(MouseButton.Left))
            {
                if ((DateTime.Now - lastMouseClickTime).TotalMilliseconds >= mouseClickCooldown)
                {
                    Gui.HandleInterfaceEvents(ControlState.MouseDown);
                    lastMouseClickTime = DateTime.Now; // Update last mouse click time
                    GameState.LastLeftClickTime = currentTime; // Track time for double-click detection
                    GameState.ClickCount++;
                }

                if (GameState.ClickCount >= 2)
                {
                    Gui.HandleInterfaceEvents(ControlState.DoubleClick);
                }
            }

            // Double-click detection for left button
            if ((DateTime.Now - lastMouseClickTime).TotalMilliseconds >= GameState.DoubleClickTImer)
            {
                GameState.ClickCount = 0;
                GameState.Info = false;
            }

            // Check for MouseUp event (button released)
            if (IsMouseButtonUp(MouseButton.Left))
            {
                Gui.HandleInterfaceEvents(ControlState.MouseUp);
            }

            for (int i = 1; i < Gui.Windows.Count; i++)
            {
                // Check if active control is hovered
                if (Gui.Windows[i].Controls != null)
                {
                    for (int j = 0; j < Gui.Windows[i].Controls.Count; j++)
                    {
                        if (GameState.CurMouseX >= Gui.Windows[i].Left &&
                            GameState.CurMouseX <= Gui.Windows[i].Width + Gui.Windows[i].Left &&
                            GameState.CurMouseY >= Gui.Windows[i].Top &&
                            GameState.CurMouseY <= Gui.Windows[i].Height + Gui.Windows[i].Top)
                        {
                            if (Gui.Windows[i].Controls[j].State != ControlState.Normal)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            // In-game interactions for left click
            if (GameState.InGame == true)
            {
                if (IsSeartchCooldownElapsed())
                {
                    if (IsMouseButtonDown(MouseButton.Left))
                    {
                        Player.CheckAttack(true);
                        NetworkSend.PlayerSearch(GameState.CurX, GameState.CurY, 0);
                        lastSearchTime = DateTime.Now;
                    }
                }

                // Right-click interactions
                if (IsMouseButtonDown(MouseButton.Right))
                {
                    int slotNum = (int)GameLogic.IsHotbar(Gui.Windows[Gui.GetWindowIndex("winHotbar")].Left,
                        Gui.Windows[Gui.GetWindowIndex("winHotbar")].Top);

                    if (slotNum >= 0L)
                    {
                        NetworkSend.SendDeleteHotbar(slotNum);
                    }

                    if (GameState.VbKeyShift == true)
                    {
                        // Admin warp if Shift is held and the player has moderator access
                        if (GetPlayerAccess(GameState.MyIndex) >= (int)AccessLevel.Moderator)
                        {
                            NetworkSend.AdminWarp(GameState.CurX, GameState.CurY);
                        }
                    }
                    else
                    {
                        // Handle right-click menu
                        HandleRightClickMenu();
                    }
                }
            }
        }

        private static void HandleRightClickMenu()
        {
            // Loop through all players and display the right-click menu for the matching one
            for (int i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                if (IsPlaying(i) && GetPlayerMap(i) == GetPlayerMap(GameState.MyIndex))
                {
                    if (GetPlayerX(i) == GameState.CurX && GetPlayerY(i) == GameState.CurY)
                    {
                        // Use current mouse state for the X and Y positions
                        GameLogic.ShowPlayerMenu(i, CurrentMouseState.X, CurrentMouseState.Y);
                    }
                }
            }

            // Perform player search at the current cursor position
            NetworkSend.PlayerSearch(GameState.CurX, GameState.CurY, 1);
        }

        private static void OnWindowClose(object sender, EventArgs e)
        {
            General.DestroyGame();
        }

        private static void OnDeviceReset()
        {
            Console.WriteLine("Device Reset");
        }

        public static void TakeScreenshot()
        {
            // Set the render target to our RenderTarget2D
            Graphics.GraphicsDevice.SetRenderTarget(RenderTarget);

            // Clear the render target with a transparent background
            Graphics.GraphicsDevice.Clear(Color.Transparent);

            // Draw everything to the render target
            General.Client.Draw(new GameTime()); // Assuming Draw handles your game rendering

            // Reset the render target to the back buffer (main display)
            Graphics.GraphicsDevice.SetRenderTarget(null);

            // Save the contents of the RenderTarget2D to a PNG file
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            using (var stream = new FileStream($"screenshot_{timestamp}.png", FileMode.Create))
            {
                RenderTarget.SaveAsPng(stream, RenderTarget.Width, RenderTarget.Height);
            }
        }

        // Draw a filled rectangle with an optional outline
        public static void DrawRectangle(Vector2 position, Vector2 size, Color fillColor, Color outlineColor,
            float outlineThickness)
        {
            // Create a 1x1 white texture for drawing
            var whiteTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);

            whiteTexture.SetData(new Color[] { Color.White });

            // Draw the filled rectangle
            SpriteBatch.Draw(whiteTexture, new Rectangle(position.ToPoint(), size.ToPoint()), fillColor);

            // Draw the outline if thickness > 0
            if (outlineThickness > 0f)
            {
                // Create the four sides of the outline
                var left = new Rectangle(position.ToPoint(),
                    new Point((int)Math.Round(outlineThickness), (int)Math.Round(size.Y)));
                var top = new Rectangle(position.ToPoint(),
                    new Point((int)Math.Round(size.X), (int)Math.Round(outlineThickness)));
                var right = new Rectangle(
                    new Point((int)Math.Round(position.X + size.X - outlineThickness), (int)Math.Round(position.Y)),
                    new Point((int)Math.Round(outlineThickness), (int)Math.Round(size.Y)));
                var bottom =
                    new Rectangle(
                        new Point((int)Math.Round(position.X), (int)Math.Round(position.Y + size.Y - outlineThickness)),
                        new Point((int)Math.Round(size.X), (int)Math.Round(outlineThickness)));

                // Draw the outline rectangles
                SpriteBatch.Draw(whiteTexture, left, outlineColor);
                SpriteBatch.Draw(whiteTexture, top, outlineColor);
                SpriteBatch.Draw(whiteTexture, right, outlineColor);
                SpriteBatch.Draw(whiteTexture, bottom, outlineColor);
            }

            // Dispose the texture to free memory
            whiteTexture.Dispose();
        }

        /// <summary>
        /// Draws a rectangle with a fill color and an outline.
        /// </summary>
        /// <param name="rect">The Rectangle to be drawn.</param>
        /// <param name="fillColor">The color to fill the rectangle.</param>
        /// <param name="outlineColor">The color of the outline.</param>
        /// <param name="outlineThickness">The thickness of the outline.</param>
        public static void DrawRectangleWithOutline(Rectangle rect, Color fillColor, Color outlineColor,
            float outlineThickness)
        {

            // Create a 1x1 white texture
            var whiteTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);
            whiteTexture.SetData(new Color[] { Color.White });

            // Draw the filled rectangle
            SpriteBatch.Draw(whiteTexture, rect, fillColor);

            // Draw the outline if thickness > 0
            if (outlineThickness > 0f)
            {
                // Define outline rectangles (left, top, right, bottom)
                var left = new Rectangle(rect.Left, rect.Top, (int)Math.Round(outlineThickness), rect.Height);
                var top = new Rectangle(rect.Left, rect.Top, rect.Width, (int)Math.Round(outlineThickness));
                var right = new Rectangle(rect.Right - (int)Math.Round(outlineThickness), rect.Top,
                    (int)Math.Round(outlineThickness), rect.Height);
                var bottom = new Rectangle(rect.Left, rect.Bottom - (int)Math.Round(outlineThickness), rect.Width,
                    (int)Math.Round(outlineThickness));

                // Draw the outline rectangles
                SpriteBatch.Draw(whiteTexture, left, outlineColor);
                SpriteBatch.Draw(whiteTexture, top, outlineColor);
                SpriteBatch.Draw(whiteTexture, right, outlineColor);
                SpriteBatch.Draw(whiteTexture, bottom, outlineColor);
            }

            // Dispose the texture after use
            whiteTexture.Dispose();
        }

        private static void DrawOutlineRectangle(int x, int y, int width, int height, Color color, float thickness)
        {
            var whiteTexture = new Texture2D(SpriteBatch.GraphicsDevice, 1, 1);

            // Define four rectangles for the outline
            var left = new Rectangle(x, y, (int)Math.Round(thickness), height);
            var top = new Rectangle(x, y, width, (int)Math.Round(thickness));
            var right = new Rectangle((int)Math.Round(x + width - thickness), y, (int)Math.Round(thickness), height);
            var bottom = new Rectangle(x, (int)Math.Round(y + height - thickness), width, (int)Math.Round(thickness));

            // Draw the outline
            SpriteBatch.Draw(whiteTexture, left, color);
            SpriteBatch.Draw(whiteTexture, top, color);
            SpriteBatch.Draw(whiteTexture, right, color);
            SpriteBatch.Draw(whiteTexture, bottom, color);
        }

        public static Color QbColorToXnaColor(int qbColor)
        {
            switch (qbColor)
            {
                case (int)Core.Color.Black:
                {
                    return Color.Black;
                }
                case (int)Core.Color.Blue:
                {
                    return Color.Blue;
                }
                case (int)Core.Color.Green:
                {
                    return Color.Green;
                }
                case (int)Core.Color.Cyan:
                {
                    return Color.Cyan;
                }
                case (int)Core.Color.Red:
                {
                    return Color.Red;
                }
                case (int)Core.Color.Magenta:
                {
                    return Color.Magenta;
                }
                case (int)Core.Color.Brown:
                {
                    return Color.Brown;
                }
                case (int)Core.Color.Gray:
                {
                    return Color.LightGray;
                }
                case (int)Core.Color.DarkGray:
                {
                    return Color.Gray;
                }
                case (int)Core.Color.BrightBlue:
                {
                    return Color.LightBlue;
                }
                case (int)Core.Color.BrightGreen:
                {
                    return Color.LightGreen;
                }
                case (int)Core.Color.BrightCyan:
                {
                    return Color.LightCyan;
                }
                case (int)Core.Color.BrightRed:
                {
                    return Color.LightCoral;
                }
                case (int)Core.Color.Pink:
                {
                    return Color.Orchid;
                }
                case (int)Core.Color.Yellow:
                {
                    return Color.Yellow;
                }
                case (int)Core.Color.White:
                {
                    return Color.White;
                }

                default:
                {
                    throw new ArgumentOutOfRangeException(nameof(qbColor), "Invalid QbColor value.");
                }
            }
        }

        public static void DrawEmote(int x2, int y2, int sprite)
        {
            Rectangle rec;
            int x;
            int y;
            int anim;

            if (sprite < 1 | sprite > GameState.NumEmotes)
                return;

            if (GameState.ShowAnimLayers)
            {
                anim = 1;
            }
            else
            {
                anim = 0;
            }

            rec.Y = 0;
            rec.Height = GameState.SizeX;
            rec.X = (int)Math.Round(anim *
                                    (GetGfxInfo(System.IO.Path.Combine(Core.Path.Emotes, sprite.ToString())).Width /
                                     2d));
            rec.Width = (int)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Emotes, sprite.ToString())).Width /
                                        2d);

            x = GameLogic.ConvertMapX(x2);
            y = GameLogic.ConvertMapY(y2) - (GameState.SizeY + 16);

            string argpath = System.IO.Path.Combine(Core.Path.Emotes, sprite.ToString());
            RenderTexture(ref argpath, x, y, rec.X, rec.Y, rec.Width, rec.Height);
        }

        public static void DrawDirections(int x, int y)
        {
            Rectangle rec;
            int i;

            // render grid
            rec.Y = 24;
            rec.X = 0;
            rec.Width = 32;
            rec.Height = 32;

            string argpath = System.IO.Path.Combine(Core.Path.Misc, "Direction");
            RenderTexture(ref argpath, GameLogic.ConvertMapX(x * GameState.SizeX),
                GameLogic.ConvertMapY(y * GameState.SizeY),
                rec.X, rec.Y, rec.Width, rec.Height, rec.Width, rec.Height);

            // render dir blobs
            for (i = 0; i < 4; i++)
            {
                rec.X = i * 8;
                rec.Width = 8;

                // find out whether render blocked or not
                bool localIsDirBlocked()
                {
                    byte argdir = (byte)i;
                    var ret = GameLogic.IsDirBlocked(ref Data.MyMap.Tile[x, y].DirBlock, ref argdir);
                    return ret;
                }

                if (!localIsDirBlocked())
                {
                    rec.Y = 8;
                }
                else
                {
                    rec.Y = 16;
                }

                rec.Height = 8;

                string argpath1 = System.IO.Path.Combine(Core.Path.Misc, "Direction");
                RenderTexture(ref argpath1, GameLogic.ConvertMapX(x * GameState.SizeX) + GameState.DirArrowX[i],
                    GameLogic.ConvertMapY(y * GameState.SizeY) + GameState.DirArrowY[i], rec.X, rec.Y, rec.Width,
                    rec.Height,
                    rec.Width, rec.Height);
            }
        }

        public static void DrawPaperdoll(int x2, int y2, int sprite, int anim, int spritetop)
        {
            Rectangle rec;
            int x;
            int y;
            int width;
            int height;

            if (sprite < 1 | sprite > GameState.NumPaperdolls)
                return;

            rec.Y = (int)Math.Round(spritetop *
                GetGfxInfo(System.IO.Path.Combine(Core.Path.Paperdolls, sprite.ToString())).Height / 4d);
            rec.Height =
                (int)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Paperdolls, sprite.ToString())).Height /
                                4d);
            rec.X = (int)Math.Round(anim *
                GetGfxInfo(System.IO.Path.Combine(Core.Path.Paperdolls, sprite.ToString())).Width / 4d);
            rec.Width = (int)Math.Round(
                GetGfxInfo(System.IO.Path.Combine(Core.Path.Paperdolls, sprite.ToString())).Width /
                4d);

            x = GameLogic.ConvertMapX(x2);
            y = GameLogic.ConvertMapY(y2);
            width = rec.Right - rec.Left;
            height = rec.Bottom - rec.Top;

            string argpath = System.IO.Path.Combine(Core.Path.Paperdolls, sprite.ToString());
            RenderTexture(ref argpath, x, y, rec.X, rec.Y, rec.Width, rec.Height);
        }

        public static void DrawNpc(int mapNpcNum)
        {
            byte anim;
            int x;
            int y;
            int sprite;
            var spriteLeft = default(int);
            Rectangle rect;
            int attackSpeed = 1000;

            // Check if Npc exists
            if (Data.MyMapNpc[(int)mapNpcNum].Num < 0 ||
                Data.MyMapNpc[(int)mapNpcNum].Num > Core.Constant.MAX_NPCS)
                return;

            x = (int)Math.Floor((double)Data.MyMapNpc[(int)mapNpcNum].X / 32);
            y = (int)Math.Floor((double)Data.MyMapNpc[(int)mapNpcNum].Y / 32);

            // Ensure Npc is within the tile view range
            if (x < GameState.TileView.Left |
                x> GameState.TileView.Right)
                return;

            if (y < GameState.TileView.Top |
                y > GameState.TileView.Bottom)
                return;

            // Stream Npc if not yet loaded
            Database.StreamNpc((int)Data.MyMapNpc[(int)mapNpcNum].Num);

            // Get the sprite of the Npc
            sprite = Data.Npc[(int)Data.MyMapNpc[(int)mapNpcNum].Num].Sprite;

            // Validate sprite
            if (sprite < 1 | sprite > GameState.NumCharacters)
                return;

            // Reset animation frame
            anim = 0;

            // Check for attacking animation
            if (Data.MyMapNpc[(int)mapNpcNum].AttackTimer + attackSpeed / 2d > General.GetTickCount() &&
                Data.MyMapNpc[(int)mapNpcNum].Attacking == 1)
            {
                anim = 3;
            }
            else
            {
                anim = (byte)Data.MyMapNpc[(int)mapNpcNum].Steps;
            }

            // Reset attacking state if attack timer has passed
            {
                ref var withBlock = ref Data.MyMapNpc[(int)mapNpcNum];
                if (withBlock.AttackTimer + attackSpeed < General.GetTickCount())
                {
                    withBlock.Attacking = 0;
                    withBlock.AttackTimer = 0;
                }
            }

            // Set sprite sheet position based on direction
            switch (Data.MyMapNpc[(int)mapNpcNum].Dir)
            {
                case (int)Direction.Up:
                {
                    spriteLeft = 3;
                    break;
                }
                case (int)Direction.Right:
                {
                    spriteLeft = 2;
                    break;
                }
                case (int)Direction.Down:
                {
                    spriteLeft = 0;
                    break;
                }
                case (int)Direction.Left:
                {
                    spriteLeft = 1;
                    break;
                }
            }

            // Create the rectangle for rendering the sprite
            rect = new Rectangle(
                (int)Math.Round(anim *
                                (GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString())).Width /
                                 4d)),
                (int)Math.Round(spriteLeft *
                                (GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString())).Height /
                                 4d)),
                (int)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString())).Width / 4d),
                (int)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString())).Height /
                                4d));

            // Calculate X and Y coordinates for rendering
            x = (int)Math.Round(Data.MyMapNpc[(int)mapNpcNum].X -
                                (GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString())).Width /
                                 4d -
                                 32d) / 2d);

            if (GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString())).Height / 4d > 32d)
            {
                // Larger sprites need an offset for height adjustment
                y = (int)Math.Round(Data.MyMapNpc[(int)mapNpcNum].Y -
                                    (GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString()))
                                            .Height /
                                        4d - 32d));
            }
            else
            {
                // Normal sprite height
                y = Data.MyMapNpc[(int)mapNpcNum].Y * GameState.SizeY;
            }

            // Draw shadow and Npc sprite
            // DrawShadow(x, y + 16)
            DrawCharacterSprite(sprite, x, y, rect);
        }

        public static void DrawMapItem(int itemNum)
        {
            Rectangle srcrec;
            Rectangle destrec;
            int picNum;
            int x;
            int y;

            if (Data.MyMapItem[itemNum].Num < 0 | Data.MyMapItem[itemNum].Num > Core.Constant.MAX_ITEMS)
                return;

            Item.StreamItem((int)Data.MyMapItem[itemNum].Num);

            picNum = Core.Data.Item[(int)Data.MyMapItem[itemNum].Num].Icon;

            if (picNum < 1 | picNum > GameState.NumItems)
                return;

            {
                ref var withBlock = ref Data.MyMapItem[itemNum];
                if (withBlock.X < GameState.TileView.Left | withBlock.X > GameState.TileView.Right)
                    return;

                if (withBlock.Y < GameState.TileView.Top | withBlock.Y > GameState.TileView.Bottom)
                    return;
            }

            srcrec = new Rectangle(0, 0, GameState.SizeX, GameState.SizeY);
            destrec = new Rectangle(GameLogic.ConvertMapX(Data.MyMapItem[itemNum].X),
                GameLogic.ConvertMapY(Data.MyMapItem[itemNum].Y), GameState.SizeX, GameState.SizeY);

            x = GameLogic.ConvertMapX(Data.MyMapItem[itemNum].X);
            y = GameLogic.ConvertMapY(Data.MyMapItem[itemNum].Y);

            string argpath = System.IO.Path.Combine(Core.Path.Items, picNum.ToString());
            RenderTexture(ref argpath, x, y, srcrec.X, srcrec.Y, srcrec.Width, srcrec.Height, srcrec.Width,
                srcrec.Height);
        }

        public static void DrawCharacterSprite(int sprite, int x2, int y2, Rectangle sRECT)
        {
            int x;
            int y;

            if (sprite < 1 | sprite > GameState.NumCharacters)
                return;

            x = GameLogic.ConvertMapX(x2);
            y = GameLogic.ConvertMapY(y2);

            string argpath = System.IO.Path.Combine(Core.Path.Characters, sprite.ToString());
            RenderTexture(ref argpath, x, y, sRECT.X, sRECT.Y, sRECT.Width, sRECT.Height, sRECT.Width, sRECT.Height);
        }

        public static void DrawBlood(int index)
        {
            Rectangle srcrec;
            Rectangle destrec;
            int x;
            int y;

            {
                ref var withBlock = ref Data.Blood[index];
                if (withBlock.X < GameState.TileView.Left | withBlock.X > GameState.TileView.Right)
                    return;
                if (withBlock.Y < GameState.TileView.Top | withBlock.Y > GameState.TileView.Bottom)
                    return;

                // check if we should be seeing it
                if (withBlock.Timer + 20000 < General.GetTickCount())
                    return;

                x = GameLogic.ConvertMapX(Data.Blood[index].X);
                y = GameLogic.ConvertMapY(Data.Blood[index].Y);

                srcrec = new Rectangle((withBlock.Sprite - 1) * GameState.SizeX, 0, GameState.SizeX, GameState.SizeY);
                destrec = new Rectangle(GameLogic.ConvertMapX(withBlock.X),
                    GameLogic.ConvertMapY(withBlock.Y), GameState.SizeX, GameState.SizeY);

                string argpath = System.IO.Path.Combine(Core.Path.Misc, "Blood");
                RenderTexture(ref argpath, x, y, srcrec.X, srcrec.Y, srcrec.Width, srcrec.Height);

            }
        }

        public static void DrawBars()
        {
            long Left;
            long Top;
            long Width;
            long Height;
            long tmpX;
            long tmpY;
            var barWidth = default(long);
            long i;
            long NpcNum;

            // dynamic bar calculations
            Width = GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Bars")).Width;
            Height = (long)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Bars")).Height / 4d);

            // render Npc health bars
            for (i = 0L; i < Constant.MAX_MAP_NPCS; i++)
            {
                NpcNum = (long)Data.MyMapNpc[(int)i].Num;
                // exists?
                if (NpcNum >= 0L && NpcNum <= Core.Constant.MAX_NPCS)
                {
                    // alive?
                    if (Data.MyMapNpc[(int)i].Vital[(int)Vital.Health] > 0 &
                        Data.MyMapNpc[(int)i].Vital[(int)Vital.Health] < Data.Npc[(int)NpcNum].HP)
                    {
                        // lock to Npc
                        tmpX = (long)Math.Round(Data.MyMapNpc[(int)i].X * GameState.SizeX + 16 - Width / 2d);
                        tmpY = Data.MyMapNpc[(int)i].Y * GameState.SizeY + 35;

                        // calculate the width to fill
                        if (Width > 0L)
                            GameState.BarWidth_NpcHP_Max[(int)i] = (long)Math.Round(
                                Data.MyMapNpc[(int)i].Vital[(int)Vital.Health] / (double)Width /
                                (Data.Npc[(int)NpcNum].HP / (double)Width) * Width);

                        // draw bar background
                        Top = Height * 3L; // HP bar background
                        Left = 0L;
                        string argpath = System.IO.Path.Combine(Core.Path.Misc, "Bars");
                        RenderTexture(ref argpath, GameLogic.ConvertMapX((int)tmpX), GameLogic.ConvertMapY((int)tmpY),
                            (int)Left, (int)Top, (int)Width, (int)Height, (int)Width, (int)Height);

                        // draw the bar proper
                        Top = 0L; // HP bar
                        Left = 0L;
                        string argpath1 = System.IO.Path.Combine(Core.Path.Misc, "Bars");
                        RenderTexture(ref argpath1, GameLogic.ConvertMapX((int)tmpX), GameLogic.ConvertMapY((int)tmpY),
                            (int)Left, (int)Top, (int)GameState.BarWidth_NpcHP[(int)i], (int)Height,
                            (int)GameState.BarWidth_NpcHP[(int)i], (int)Height);
                    }
                }
            }

            for (i = 0L; i < Constant.MAX_PLAYERS; i++)
            {
                if (GetPlayerMap((int)i) == GetPlayerMap((int)i))
                {
                    if (GetPlayerVital((int)i, Vital.Health) > 0 &
                        GetPlayerVital((int)i, Vital.Health) < GetPlayerMaxVital((int)i, Vital.Health))
                    {
                        // lock to Player
                        tmpX = (long)Math.Round(GetPlayerX((int)i) * GameState.SizeX +
                            16 - Width / 2d);
                        tmpY = GetPlayerY((int)i) * GameState.SizeY + 35;

                        // calculate the width to fill
                        if (Width > 0L)
                            GameState.BarWidth_PlayerHP_Max[(int)i] = (long)Math.Round(
                                GetPlayerVital((int)i, Vital.Health) / (double)Width /
                                (GetPlayerMaxVital((int)i, Vital.Health) / (double)Width) * Width);

                        // draw bar background
                        Top = Height * 3L; // HP bar background
                        Left = 0L;
                        string argpath2 = System.IO.Path.Combine(Core.Path.Misc, "Bars");
                        RenderTexture(ref argpath2, GameLogic.ConvertMapX((int)tmpX), GameLogic.ConvertMapY((int)tmpY),
                            (int)Left, (int)Top, (int)Width, (int)Height, (int)Width, (int)Height);

                        // draw the bar proper
                        Top = 0L; // HP bar
                        Left = 0L;
                        string argpath3 = System.IO.Path.Combine(Core.Path.Misc, "Bars");
                        RenderTexture(ref argpath3, GameLogic.ConvertMapX((int)tmpX), GameLogic.ConvertMapY((int)tmpY),
                            (int)Left, (int)Top, (int)GameState.BarWidth_PlayerHP[(int)i], (int)Height,
                            (int)GameState.BarWidth_PlayerHP[(int)i], (int)Height);
                    }

                    if (GetPlayerVital((int)i, Vital.Stamina) > 0 &
                        GetPlayerVital((int)i, Vital.Stamina) < GetPlayerMaxVital((int)i, Vital.Stamina))
                    {
                        // lock to Player
                        tmpX = (long)Math.Round(GetPlayerX((int)i) * GameState.SizeX +
                            16 - Width / 2d);
                        tmpY = GetPlayerY((int)i) * GameState.SizeY + 35 + Height;

                        // calculate the width to fill
                        if (Width > 0L)
                            GameState.BarWidth_PlayerSP_Max[(int)i] = (long)Math.Round(
                                GetPlayerVital((int)i, Vital.Stamina) / (double)Width /
                                (GetPlayerMaxVital((int)i, Vital.Stamina) / (double)Width) * Width);

                        // draw bar background
                        Top = Height * 3L; // SP bar background
                        Left = 0L;
                        string argpath4 = System.IO.Path.Combine(Core.Path.Misc, "Bars");
                        RenderTexture(ref argpath4, GameLogic.ConvertMapX((int)tmpX), GameLogic.ConvertMapY((int)tmpY),
                            (int)Left, (int)Top, (int)Width, (int)Height, (int)Width, (int)Height);

                        // draw the bar proper
                        Top = Height * 0L; // SP bar
                        Left = 0L;
                        string argpath5 = System.IO.Path.Combine(Core.Path.Misc, "Bars");
                        RenderTexture(ref argpath5, GameLogic.ConvertMapX((int)tmpX), GameLogic.ConvertMapY((int)tmpY),
                            (int)Left, (int)Top, (int)GameState.BarWidth_PlayerSP[(int)i], (int)Height,
                            (int)GameState.BarWidth_PlayerSP[(int)i], (int)Height);
                    }

                    if (GameState.SkillBuffer >= 0)
                    {
                        if ((int)Core.Data.Player[(int)i].Skill[GameState.SkillBuffer].Num >= 0)
                        {
                            if (Data.Skill[(int)Core.Data.Player[(int)i].Skill[GameState.SkillBuffer].Num]
                                    .CastTime >
                                0)
                            {
                                // lock to player
                                tmpX = (long)Math.Round(GetPlayerX((int)i) * GameState.SizeX + 16 - Width / 2d);

                                tmpY = GetPlayerY((int)i) * GameState.SizeY + 35 +
                                       Height;

                                // calculate the width to fill
                                if (Width > 0L)
                                    barWidth = (long)Math.Round((General.GetTickCount() - GameState.SkillBufferTimer) /
                                        (double)(Core.Data
                                            .Skill[(int)Core.Data.Player[(int)i].Skill[GameState.SkillBuffer].Num]
                                            .CastTime * 1000) * Width);

                                // draw bar background
                                Top = Height * 3L; // cooldown bar background
                                Left = 0L;
                                string argpath6 = System.IO.Path.Combine(Core.Path.Misc, "Bars");
                                RenderTexture(ref argpath6, GameLogic.ConvertMapX((int)tmpX),
                                    GameLogic.ConvertMapY((int)tmpY), (int)Left, (int)Top, (int)Width, (int)Height,
                                    (int)Width, (int)Height);

                                // draw the bar proper
                                Top = Height * 2L; // cooldown bar
                                Left = 0L;
                                string argpath7 = System.IO.Path.Combine(Core.Path.Misc, "Bars");
                                RenderTexture(ref argpath7, GameLogic.ConvertMapX((int)tmpX),
                                    GameLogic.ConvertMapY((int)tmpY), (int)Left, (int)Top, (int)barWidth, (int)Height,
                                    (int)barWidth, (int)Height);
                            }
                        }
                    }
                }
            }
        }

        internal void DrawEyeDropper()
        {
            SpriteBatch.Begin();

            // Define rectangle parameters.
            var position = new Vector2(GameLogic.ConvertMapX(GameState.CurX), GameLogic.ConvertMapY(GameState.CurY));
            var size = new Vector2(GameState.SizeX, GameState.SizeX);
            var fillColor = Color.Transparent; // No fill
            var outlineColor = Color.Cyan; // Cyan outline
            int outlineThickness = 1; // Thickness of outline

            // Draw the rectangle with an outline.
            DrawRectangle(position, size, fillColor, outlineColor, outlineThickness);
            SpriteBatch.End();
        }

        public static void DrawGrid()
        {
            // Use a single Begin/End pair to improve performance
            SpriteBatch.Begin();

            // Iterate over the tiles in the visible range
            for (double x = GameState.TileView.Left - 1d, loopTo = GameState.TileView.Right + 1d; x < loopTo; x++)
            {
                for (double y = GameState.TileView.Top - 1d, loopTo1 = GameState.TileView.Bottom + 1d; y < loopTo1; y++)
                {
                    if (GameLogic.IsValidMapPoint((int)Math.Round(x), (int)Math.Round(y)))
                    {
                        // Calculate the tile position and size
                        int posX = GameLogic.ConvertMapX((int)Math.Round((x - 1d)));
                        int posY = GameLogic.ConvertMapY((int)Math.Round((y - 1d) * GameState.SizeY));
                        int rectWidth = GameState.SizeX;
                        int rectHeight = GameState.SizeY;

                        // Draw the transparent rectangle as the tile background
                        SpriteBatch.Draw(TransparentTexture, new Rectangle(posX, posY, rectWidth, rectHeight),
                            Color.Transparent);

                        // Define the outline color and thickness
                        var outlineColor = Color.White;
                        int thickness = 1;

                        // Draw the tile outline (top, bottom, left, right)
                        SpriteBatch.Draw(TransparentTexture, new Rectangle(posX, posY, rectWidth, thickness),
                            outlineColor); // Top
                        SpriteBatch.Draw(TransparentTexture,
                            new Rectangle(posX, posY + rectHeight - thickness, rectWidth, thickness),
                            outlineColor); // Bottom
                        SpriteBatch.Draw(TransparentTexture, new Rectangle(posX, posY, thickness, rectHeight),
                            outlineColor); // Left
                        SpriteBatch.Draw(TransparentTexture,
                            new Rectangle(posX + rectWidth - thickness, posY, thickness, rectHeight),
                            outlineColor); // Right
                    }
                }
            }

            SpriteBatch.End();
        }

        public static void DrawTarget(int x2, int y2)
        {
            Rectangle rec;
            int x;
            int y;
            int width;
            int height;

            rec.Y = 0;
            rec.Height = GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Target")).Height;
            rec.X = 0;
            rec.Width = (int)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Target")).Width / 2d);
            x = GameLogic.ConvertMapX(x2 + 4);
            y = GameLogic.ConvertMapY(y2 - 32);
            width = rec.Right - rec.Left;
            height = rec.Bottom - rec.Top;

            string argpath = System.IO.Path.Combine(Core.Path.Misc, "Target");
            RenderTexture(ref argpath, x, y, rec.X, rec.Y, rec.Width, rec.Height, rec.Width, rec.Height);
        }

        public static Color ToMonoGameColor(System.Drawing.Color drawingColor)
        {
            return new Color(drawingColor.R, drawingColor.G, drawingColor.B, drawingColor.A);
        }

        public static void DrawHover(int x2, int y2)
        {
            Rectangle rec;
            int x;
            int y;
            int width;
            int height;

            rec.Y = 0;
            rec.Height = GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Target")).Height;
            rec.X = (int)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Target")).Width / 2d);
            rec.Width = (int)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Target")).Width / 2d +
                                        GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Target")).Width / 2d);

            x = GameLogic.ConvertMapX(x2 + 4);
            y = GameLogic.ConvertMapY(y2 - 32);
            width = rec.Right - rec.Left;
            height = rec.Bottom - rec.Top;

            string argpath = System.IO.Path.Combine(Core.Path.Misc, "Target");
            RenderTexture(ref argpath, x, y, rec.X, rec.Y, rec.Width, rec.Height, rec.Width, rec.Height);
        }

        public static void DrawChatBubble(long Index)
        {
            var theArray = default(string[]);
            int x;
            int y;
            long i;
            var MaxWidth = default(long);
            long x2;
            long y2;
            int Color;
            long tmpNum;

            {
                ref var withBlock = ref Data.ChatBubble[(int)Index];

                // exit out early
                if (withBlock.TargetType == 0)
                    return;

                Color = withBlock.Color;

                // calculate position
                switch (withBlock.TargetType)
                {
                    case (byte)TargetType.Player:
                    {
                        // it's a player
                        if (!(GetPlayerMap(withBlock.Target) == GetPlayerMap(GameState.MyIndex)))
                            return;

                        // it's on our map - get co-ords
                        x = GameLogic.ConvertMapX(Core.Data.Player[withBlock.Target].X ) + 16;
                        y = GameLogic.ConvertMapY(Core.Data.Player[withBlock.Target].Y) - 32;
                        break;
                    }
                    case (byte)TargetType.Event:
                    {
                        x = GameLogic.ConvertMapX(Data.MyMap.Event[withBlock.Target].X ) + 16;
                        y = GameLogic.ConvertMapY(Data.MyMap.Event[withBlock.Target].Y) - 16;
                        break;
                    }

                    case (byte)TargetType.Npc:
                    {
                        x = GameLogic.ConvertMapX(Data.MyMapNpc[withBlock.Target].X ) + 16;
                        y = GameLogic.ConvertMapY(Data.MyMapNpc[withBlock.Target].Y) - 32;
                        break;
                    }

                    default:
                    {
                        x = 0;
                        y = 0;
                        return;
                    }
                }

                withBlock.Msg = withBlock.Msg.Replace("\0", string.Empty);

                // word wrap
                Text.WordWrap(withBlock.Msg, Core.Font.Georgia, GameState.ChatBubbleWidth, ref theArray);

                // find max width
                tmpNum = Information.UBound(theArray);

                var loopTo = tmpNum;
                for (i = 0L; i <= loopTo; i++)
                {
                    if (Text.GetTextWidth(theArray[(int)i], Core.Font.Georgia) > MaxWidth)
                        MaxWidth = Text.GetTextWidth(theArray[(int)i], Core.Font.Georgia);
                }

                // calculate the new position 
                x2 = x - MaxWidth / 2L;
                y2 = y - (Information.UBound(theArray) + 1) * 12;

                // render bubble - top left
                string argpath = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath, (int)(x2 - 9L), (int)(y2 - 5L), 0, 0, 9, 5, 9, 5);

                // top right
                string argpath1 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath1, (int)(x2 + MaxWidth), (int)(y2 - 5L), 119, 0, 9, 5, 9, 5);

                // top
                string argpath2 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath2, (int)x2, (int)(y2 - 5L), 9, 0, (int)MaxWidth, 5, 5, 5);

                // bottom left
                string argpath3 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath3, (int)(x2 - 9L), (int)y, 0, 19, 9, 6, 9, 6);

                // bottom right
                string argpath4 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath4, (int)(x2 + MaxWidth), (int)y, 119, 19, 9, 6, 9, 6);

                // bottom - left half
                string argpath5 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath5, (int)x2, (int)y, 9, 19, (int)(MaxWidth / 2L - 5L), 6, 6, 6);

                // bottom - right half
                string argpath6 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath6, (int)(x2 + MaxWidth / 2L + 6L), (int)y, 9, 19, (int)(MaxWidth / 2L - 5L), 6,
                    9,
                    6);

                // left
                string argpath7 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath7, (int)(x2 - 9L), (int)y2, 0, 6, 9, (Information.UBound(theArray) + 1) * 12, 9, 6);

                // right
                string argpath8 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath8, (int)(x2 + MaxWidth), (int)y2, 119, 6, 9, (Information.UBound(theArray) + 1) * 12,
                    9,
                    6);

                // center
                string argpath9 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath9, (int)x2, (int)y2, 9, 5, (int)MaxWidth, (Information.UBound(theArray) + 1) * 12, 9,
                    5);

                // little pointy bit
                string argpath10 = System.IO.Path.Combine(Core.Path.Gui, 33.ToString());
                RenderTexture(ref argpath10, (int)(x - 5L), (int)y, 58, 19, 11, 11, 11, 11);

                // render each line centralized
                tmpNum = Information.UBound(theArray);

                var loopTo1 = tmpNum;
                for (i = 0; i <= loopTo1; i++)
                {
                    if (theArray[(int)i] == null)
                        continue;

                    // Measure button text size and apply padding
                    var textSize = Text.Fonts[Core.Font.Georgia].MeasureString(theArray[(int)i]);
                    float actualWidth = textSize.X;
                    float actualHeight = textSize.Y;

                    // Calculate horizontal and vertical centers with padding
                    double padding = (double)actualWidth / 6.0d;

                    Text.RenderText(theArray[(int)i],
                        (int)Math.Round(x - theArray[(int)i].Length / 2d - Text.GetTextWidth(theArray[(int)i]) / 2d +
                                        padding), (int)y2, QbColorToXnaColor(withBlock.Color),
                        Microsoft.Xna.Framework.Color.Black);
                    y2 = y2 + 12L;
                }

                // check if it's timed out - close it if so
                if (withBlock.Timer + 5000 < General.GetTickCount())
                {
                    withBlock.Active = false;
                }
            }
        }

        public static void DrawPlayer(int index)
        {
            byte anim;
            int x;
            int y;
            int spriteNum;
            var spriteleft = default(int);
            int attackSpeed;
            Rectangle rect;

            spriteNum = GetPlayerSprite(index);

            if (index < 0 | index > Constant.MAX_PLAYERS)
                return;

            if (spriteNum <= 0 | spriteNum > GameState.NumCharacters)
                return;

            // speed from weapon
            if (GetPlayerEquipment(index, Equipment.Weapon) >= 0)
            {
                attackSpeed = Core.Data.Item[GetPlayerEquipment(index, Equipment.Weapon)].Speed;
            }
            else
            {
                attackSpeed = 1000;
            }

            // Reset frame
            anim = 0;

            // Check for attacking animation
            if (Core.Data.Player[index].AttackTimer + attackSpeed / 2d > General.GetTickCount())
            {
                if (Core.Data.Player[index].Attacking == 1)
                {
                    anim = 3;
                }
            }
            else
            {
                anim = Core.Data.Player[index].Steps;
            }

            // Check to see if we want to stop making him attack
            {
                ref var withBlock = ref Core.Data.Player[index];
                if (withBlock.AttackTimer + attackSpeed < General.GetTickCount())
                {
                    withBlock.Attacking = 0;
                    withBlock.AttackTimer = 0;
                }

            }

            // Set the left
            switch (GetPlayerDir(index))
            {
                case (int)Direction.Up:
                {
                    spriteleft = 3;
                    break;
                }
                case (int)Direction.Right:
                {
                    spriteleft = 2;
                    break;
                }
                case (int)Direction.Down:
                {
                    spriteleft = 0;
                    break;
                }
                case (int)Direction.Left:
                {
                    spriteleft = 1;
                    break;
                }
                case (int)Direction.UpRight:
                {
                    spriteleft = 2;
                    break;
                }
                case (int)Direction.UpLeft:
                {
                    spriteleft = 1;
                    break;
                }
                case (int)Direction.DownLeft:
                {
                    spriteleft = 1;
                    break;
                }
                case (int)Direction.DownRight:
                {
                    spriteleft = 2;
                    break;
                }
            }

            var gfxInfo = GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, spriteNum.ToString()));
            if (gfxInfo == null)
            {
                // Handle the case where the graphic information is not found
                return;
            }

            // Calculate the X
            x = (int)Math.Round(Core.Data.Player[index].X - (gfxInfo.Width / 4d - 32d) / 2d);

            // Is the player's height more than 32..?
            if (gfxInfo.Height > 32)
            {
                // Create a 32 pixel offset for larger sprites
                y = (int)Math.Round(GetPlayerRawY(index) - (gfxInfo.Height / 4d - 32d));
            }
            else
            {
                // Proceed as normal
                y = GetPlayerRawY(index);
            }

            rect = new Rectangle((int)Math.Round(anim * (gfxInfo.Width / 4d)),
                (int)Math.Round(spriteleft * (gfxInfo.Height / 4d)), (int)Math.Round(gfxInfo.Width / 4d),
                (int)Math.Round(gfxInfo.Height / 4d));

            // render the actual sprite
            // DrawShadow(x, y + 16)
            DrawCharacterSprite(spriteNum, x, y, rect);

            // check for paperdolling
            for (int i = 0; i < Enum.GetValues(typeof(Equipment)).Length; i++)
            {
                if (GetPlayerEquipment(index, (Equipment)i) >= 0)
                {
                    if (Core.Data.Item[GetPlayerEquipment(index, (Equipment)i)].Paperdoll > 0)
                    {
                        DrawPaperdoll(x, y, Core.Data.Item[GetPlayerEquipment(index, (Equipment)i)].Paperdoll, anim,
                            spriteleft);
                    }
                }
            }

            // Check to see if we want to stop showing emote
            {
                ref var withBlock1 = ref Core.Data.Player[index];
                if (withBlock1.EmoteTimer < General.GetTickCount())
                {
                    withBlock1.Emote = 0;
                    withBlock1.EmoteTimer = 0;
                }
            }

            // check for emotes
            if (Core.Data.Player[GameState.MyIndex].Emote > 0)
            {
                DrawEmote(x, y, Core.Data.Player[GameState.MyIndex].Emote);
            }
        }

        public static void DrawEvents()
        {
            if (Data.MyMap.EventCount <= 0)
                return; // Exit early if no events

            for (int i = 0, loopTo = Data.MyMap.EventCount; i < loopTo; i++)
            {
                int x = GameLogic.ConvertMapX(Data.MyMap.Event[i].X);
                int y = GameLogic.ConvertMapY(Data.MyMap.Event[i].Y);

                // Skip event if there are no pages
                if (Data.MyMap.Event[i].PageCount <= 0)
                {
                    DrawOutlineRectangle(x, y, GameState.SizeX, GameState.SizeY, Color.Blue, 0.6f);
                    continue;
                }

                // Render event based on its graphic type
                switch (Data.MyMap.Event[i].Pages[0].GraphicType)
                {
                    case 0: // Text Event
                    {
                        int tX = x + GameState.SizeX / 2 - 4;
                        int tY = y + GameState.SizeY / 2 - 7;
                        Text.RenderText("E", tX, tY, Color.Green, Color.Black);
                        break;
                    }

                    case 1: // Character Graphic
                    {
                        RenderCharacterGraphic(Core.Data.MyMap.Event[i], x, y);
                        break;
                    }

                    case 2: // Tileset Graphic
                    {
                        RenderTilesetGraphic(Core.Data.MyMap.Event[i], x, y);
                        break;
                    }

                    default:
                    {
                        // Draw fallback outline rectangle if graphic type is unknown
                        DrawOutlineRectangle(x, y, GameState.SizeX, GameState.SizeY, Color.Blue, 0.6f);
                        break;
                    }
                }
            }
        }

        public static void RenderCharacterGraphic(Core.Type.Event eventData, int x, int y)
        {
            // Get the graphic index from the event's first page
            int gfxIndex = eventData.Pages[0].Graphic;

            // Validate the graphic index to ensure it�s within range
            if (gfxIndex <= 0 || gfxIndex > GameState.NumCharacters)
                return;

            // Get animation details (frame index and columns) from the event
            int frameIndex = eventData.Pages[0].GraphicX; // Example frame index
            int columns = 4;
            var gfxInfo = GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, gfxIndex.ToString()));
            if (gfxInfo == null)
            {
                // Handle the case where the graphic information is not found
                return;
            }

            // Calculate the frame size (assuming square frames for simplicity)
            int frameWidth = gfxInfo.Width / columns;
            int frameHeight = frameWidth; // Adjust if non-square frames

            // Calculate the source rectangle for the current frame
            int column = frameIndex % columns;
            int row = frameIndex / columns;
            var sourceRect = new Rectangle(column * frameWidth, row * frameHeight, frameWidth, frameHeight);

            // Define the position on the map where the graphic will be drawn
            var position = new Vector2(x, y);

            string argpath = System.IO.Path.Combine(Core.Path.Characters, gfxIndex.ToString());
            RenderTexture(ref argpath, (int)Math.Round(position.X), (int)Math.Round(position.Y), sourceRect.X,
                sourceRect.Y,
                frameWidth, frameHeight, sourceRect.Width, sourceRect.Height);
        }

        private static void RenderTilesetGraphic(Core.Type.Event eventData, int x, int y)
        {
            int gfxIndex = eventData.Pages[0].Graphic;

            if (gfxIndex > 0 && gfxIndex <= GameState.NumTileSets)
            {
                // Define source rectangle from tileset graphics
                var srcRect = new Rectangle(eventData.Pages[0].GraphicX * 32, eventData.Pages[0].GraphicY * 32,
                    eventData.Pages[0].GraphicX2 * 32, eventData.Pages[0].GraphicY2 * 32);

                // Adjust position if the tile is larger than 32x32
                if (srcRect.Height > 32)
                    y -= GameState.SizeY;

                // Define destination rectangle
                var destRect = new Rectangle(x, y, srcRect.Width, srcRect.Height);

                string argpath = System.IO.Path.Combine(Core.Path.Tilesets, gfxIndex.ToString());
                RenderTexture(ref argpath, destRect.X, destRect.Y, srcRect.X, srcRect.Y, destRect.Width,
                    destRect.Height,
                    srcRect.Width, srcRect.Height);
            }
            else
            {
                // Draw fallback outline if the tileset graphic is invalid
                DrawOutlineRectangle(x, y, GameState.SizeX, GameState.SizeY, Color.Blue, 0.6f);
            }
        }

        public static void DrawEvent(int id) // draw on map, outside the editor
        {
            int x;
            int y;
            int width;
            int height;
            var sRect = default(Rectangle);
            var anim = default(int);
            var spritetop = default(int);

            try
            {
                if (Data.MapEvents[id].Visible == false)
                {
                    return;
                }

                switch (Data.MapEvents[id].GraphicType)
                {
                    case 0:
                    {
                        return;
                    }
                    case 1:
                    {
                        if (Data.MapEvents[id].Graphic <= 0 |
                            Data.MapEvents[id].Graphic > GameState.NumCharacters)
                            return;
                        
                         anim = Data.MapEvents[id].Steps;
                        
                        // Set the left
                        switch (Data.MapEvents[id].ShowDir)
                        {
                            case (int)Direction.Up:
                            {
                                spritetop = 3;
                                break;
                            }
                            case (int)Direction.Right:
                            {
                                spritetop = 2;
                                break;
                            }
                            case (int)Direction.Down:
                            {
                                spritetop = 0;
                                break;
                            }
                            case (int)Direction.Left:
                            {
                                spritetop = 1;
                                break;
                            }
                        }

                        var gfxInfo = GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters,
                            Data.MapEvents[id].Graphic.ToString()));

                        if (gfxInfo == null)
                        {
                            // Handle the case where gfxInfo is null
                            return;
                        }

                        height = (int)Math.Round((double)gfxInfo.Height / 4d);
                        width = (int)Math.Round((double)gfxInfo.Width / 4d);
                        sRect = new Rectangle((int)Math.Round((double)anim * width),
                            (int)Math.Round((double)spritetop * height), width, height);

                        // Calculate the X
                        x = (int)Math.Round(Data.MapEvents[id].X -
                                            (width - 32d) / 2d);

                        // Is the player's height more than 32..?
                        if (gfxInfo.Height * 4 > 32)
                        {
                            // Create a 32 pixel offset for larger sprites
                            y = (int)Math.Round(Data.MapEvents[id].Y - (height - 32d));
                        }
                        else
                        {
                            // Proceed as normal
                            y = Data.MapEvents[id].Y * GameState.SizeY;
                        }

                        // render the actual sprite
                        DrawCharacterSprite(Data.MapEvents[id].Graphic, x, y, sRect);
                        break;
                    }
                    case 2:
                    {
                        if (Data.MapEvents[id].Graphic < 1 |
                            Data.MapEvents[id].Graphic > GameState.NumTileSets)
                            return;

                        if (Data.MapEvents[id].GraphicY2 > 0 | Data.MapEvents[id].GraphicX2 > 0)
                        {
                            sRect.X = Data.MapEvents[id].GraphicX * 32;
                            sRect.Y = Data.MapEvents[id].GraphicY * 32;
                            sRect.Width = Data.MapEvents[id].GraphicX2 * 32;
                            sRect.Height = Data.MapEvents[id].GraphicY2 * 32;
                        }
                        else
                        {
                            sRect.X = Data.MapEvents[id].GraphicY * 32;
                            sRect.Height = sRect.Top + 32;
                            sRect.Y = Data.MapEvents[id].GraphicX * 32;
                            sRect.Width = sRect.Left + 32;
                        }

                        x = Data.MapEvents[id].X * 32;
                        y = Data.MapEvents[id].Y * 32;
                        x = (int)Math.Round(x - (sRect.Right - sRect.Left) / 2d);
                        y = y - (sRect.Bottom - sRect.Top) + 32;

                        if (Data.MapEvents[id].GraphicY2 > 1)
                        {
                            string argpath = System.IO.Path.Combine(Core.Path.Tilesets,
                                Data.MapEvents[id].Graphic.ToString());
                            RenderTexture(ref argpath,
                                GameLogic.ConvertMapX(Data.MapEvents[id].X),
                                GameLogic.ConvertMapY(Data.MapEvents[id].Y) - GameState.SizeY,
                                sRect.Left, sRect.Top, sRect.Width, sRect.Height);
                        }
                        else
                        {
                            string argpath1 = System.IO.Path.Combine(Core.Path.Tilesets,
                                Data.MapEvents[id].Graphic.ToString());
                            RenderTexture(ref argpath1,
                                GameLogic.ConvertMapX(Data.MapEvents[id].X),
                                GameLogic.ConvertMapY(Data.MapEvents[id].Y), sRect.Left,
                                sRect.Top,
                                sRect.Width, sRect.Height);
                        }

                        break;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void Render_Game()
        {
            int x;
            int y;
            int i;

            if (GameState.GettingMap)
                return;

            GameLogic.UpdateCamera();

            if (GameState.NumPanoramas > 0 & Data.MyMap.Panorama > 0)
            {
                Map.DrawPanorama(Data.MyMap.Panorama);
            }

            if (GameState.NumParallax > 0 & Data.MyMap.Parallax > 0)
            {
                Map.DrawParallax(Data.MyMap.Parallax);
            }

            // Draw lower tiles
            if (GameState.NumTileSets > 0)
            {
                var loopTo = (int)Math.Round(GameState.TileView.Right + 1d);
                for (x = (int)Math.Round(GameState.TileView.Left - 1d); x < loopTo; x++)
                {
                    var loopTo1 = (int)Math.Round(GameState.TileView.Bottom + 1d);
                    for (y = (int)Math.Round(GameState.TileView.Top - 1d); y < loopTo1; y++)
                    {
                        if (GameLogic.IsValidMapPoint(x, y))
                        {
                            Map.DrawMapGroundTile(x, y);
                        }
                    }
                }
            }

            // events
            if (GameState.MyEditorType != (int)EditorType.Map)
            {
                if (GameState.CurrentEvents > 0 & GameState.CurrentEvents <= Data.MyMap.EventCount)
                {
                    var loopTo2 = GameState.CurrentEvents;
                    for (i = 0; i < loopTo2; i++)
                    {
                        if (i < Data.MapEvents.Length)
                        {
                            if (Data.MapEvents[i].Position == 0)
                            {
                                DrawEvent(i);
                            }
                        }
                    }
                }
            }

            // blood
            for (i = 0; i < byte.MaxValue; i++)
                DrawBlood(i);

            // Draw out the items
            if (GameState.NumItems > 0)
            {
                for (i = 0; i < Constant.MAX_MAP_ITEMS; i++)
                {
                    if (Data.MyMapItem[i].Num >= 0)
                    {
                        DrawMapItem(i);
                    }
                }
            }

            // draw animations
            if (GameState.NumAnimations > 0)
            {
                for (i = 0; i < byte.MaxValue; i++)
                {
                    if (Information.UBound(Animation.AnimInstance) >= i)
                    {
                        if (Animation.AnimInstance[i].Used[0])
                        {
                            Animation.DrawAnimation(i, 0);
                        }
                    }
                }
            }

            // Y-based render. Renders Players, Npcs and Resources based on Y-axis.
            var loopTo3 = (int)Data.MyMap.MaxY;
            for (y = 0; y < loopTo3; y++)
            {
                if (GameState.NumCharacters > 0)
                {
                    // Npcs
                    for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                    {
                        if (Math.Floor((decimal)Data.MyMapNpc[i].Y / 32) == y)
                        {
                            DrawNpc(i);
                        }
                    }

                    // Players
                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i) & GetPlayerMap(i) == GetPlayerMap(GameState.MyIndex))
                        {
                            if (GetPlayerY(i) == y)
                            {
                                DrawPlayer(i);
                            }
                        }
                    }

                    if (GameState.MyEditorType != (int)EditorType.Map)
                    {
                        if (GameState.CurrentEvents > 0 & GameState.CurrentEvents <= Data.MyMap.EventCount)
                        {
                            var loopTo4 = GameState.CurrentEvents;
                            for (i = 0; i < loopTo4; i++)
                            {
                                if (Data.MapEvents[i].Position == 1)
                                {
                                    if (Math.Floor((decimal)Data.MapEvents[i].Y / 32) == y)
                                    {
                                        DrawEvent(i);
                                    }
                                }
                            }
                        }
                    }

                    // Draw the target icon
                    if (GameState.MyTarget >= 0)
                    {
                        switch (GameState.MyTargetType)
                        {
                            case (int)TargetType.Player:
                                if (IsPlaying(GameState.MyTarget))
                                {
                                    if (Core.Data.Player[GameState.MyTarget].Map ==
                                        Core.Data.Player[GameState.MyIndex].Map)
                                    {
                                        if (Core.Data.Player[GameState.MyTarget].Sprite > 0)
                                        {
                                            // Draw the target icon for the player
                                            DrawTarget(
                                                Core.Data.Player[GameState.MyTarget].X - 16,
                                                Core.Data.Player[GameState.MyTarget].Y);
                                        }
                                    }
                                }

                                break;

                            case (int)TargetType.Npc:
                                DrawTarget(
                                    Data.MyMapNpc[GameState.MyTarget].X - 16,
                                    Data.MyMapNpc[GameState.MyTarget].Y);
                                break;

                        }
                    }

                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i))
                        {
                            if (Core.Data.Player[i].Map == Core.Data.Player[GameState.MyIndex].Map)
                            {
                                if (Core.Data.Player[i].Sprite == 0)
                                    continue;

                                if (GameState.CurX == Core.Data.Player[i].X & GameState.CurY == Core.Data.Player[i].Y)
                                {
                                    if (GameState.MyTargetType == (int)TargetType.Player & GameState.MyTarget == i)
                                    {
                                    }

                                    else
                                    {
                                        DrawHover(Core.Data.Player[i].X * 32 - 16,
                                            Core.Data.Player[i].Y * 32 + Core.Data.Player[i].Y);
                                    }
                                }

                            }
                        }
                    }
                }

                // Resources
                if (GameState.NumResources > 0)
                {
                    if (GameState.ResourcesInit)
                    {
                        if (GameState.ResourceIndex > 0)
                        {
                            var loopTo5 = GameState.ResourceIndex;
                            for (i = 0; i < loopTo5; i++)
                            {
                                if (Data.MyMapResource[i].Y == y)
                                {
                                    MapResource.DrawMapResource(i);
                                }
                            }
                        }
                    }
                }
            }

            // animations
            if (GameState.NumAnimations > 0)
            {
                for (i = 0; i < byte.MaxValue; i++)
                {
                    if (Information.UBound(Animation.AnimInstance) >= i)
                    {
                        if (Animation.AnimInstance[i].Used[1])
                        {
                            Animation.DrawAnimation(i, 1);
                        }
                    }
                }
            }

            if (GameState.NumProjectiles > 0)
            {
                for (i = 0; i < Constant.MAX_PROJECTILES; i++)
                {
                    if (Data.MapProjectile[Core.Data.Player[GameState.MyIndex].Map, i].ProjectileNum >= 0)
                    {
                        Projectile.DrawProjectile(i);
                    }
                }
            }

            if (GameState.CurrentEvents > 0 & GameState.CurrentEvents <= Data.MyMap.EventCount)
            {
                var loopTo6 = GameState.CurrentEvents;
                for (i = 0; i < loopTo6; i++)
                {
                    if (Data.MapEvents[i].Position == 2)
                    {
                        DrawEvent(i);
                    }
                }
            }

            if (GameState.NumTileSets > 0)
            {
                var loopTo7 = (int)Math.Round(GameState.TileView.Right + 1d);
                for (x = (int)Math.Round(GameState.TileView.Left - 1d); x < loopTo7; x++)
                {
                    var loopTo8 = (int)Math.Round(GameState.TileView.Bottom + 1d);
                    for (y = (int)Math.Round(GameState.TileView.Top - 1d); y < loopTo8; y++)
                    {
                        if (GameLogic.IsValidMapPoint(x, y))
                        {
                            Map.DrawMapRoofTile(x, y);
                        }
                    }
                }
            }

            Map.DrawWeather();
            Map.DrawThunderEffect();
            Map.DrawMapTint();

            // Draw out a square at mouse cursor
            if (Conversions.ToInteger(GameState.MapGrid) == 1 & GameState.MyEditorType == (int)EditorType.Map)
            {
                DrawGrid();
            }

            for (i = 0; i < Constant.MAX_PLAYERS; i++)
            {
                if (IsPlaying(i) & GetPlayerMap(i) == GetPlayerMap(GameState.MyIndex))
                {
                    Text.DrawPlayerName(i);
                }
            }

            if (GameState.MyEditorType != (int)EditorType.Map)
            {
                if (GameState.CurrentEvents > 0 && Data.MyMap.EventCount >= GameState.CurrentEvents)
                {
                    var loopTo9 = GameState.CurrentEvents;
                    for (i = 0; i < loopTo9; i++)
                    {
                        if (Data.MapEvents[i].Visible == true)
                        {
                            if (Data.MapEvents[i].ShowName == 1)
                            {
                                Text.DrawEventName(i);
                            }
                        }
                    }
                }
            }

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                Text.DrawNpcName(i);
            }

            Map.DrawFog();
            Map.DrawPicture();

            for (i = 0; i < byte.MaxValue; i++)
                Text.DrawActionMsg(i);

            if (GameState.MyEditorType == (int)EditorType.Map)
            {
                UpdateDirBlock();
                UpdateMapAttributes();

            }

            for (i = 0; i < byte.MaxValue; i++)
            {
                if (Data.ChatBubble[i].Active)
                {
                    DrawChatBubble(i);
                }
            }

            if (GameState.Bfps)
            {
                string fps = "FPS: " + GetFps();
                Text.RenderText(fps, (int)Math.Round(GameState.Camera.Left - 24d),
                    (int)Math.Round(GameState.Camera.Top + 60d), Color.Yellow, Color.Black);
            }

            // draw cursor, player X and Y locations
            if (GameState.BLoc)
            {
                string Cur = "Cur X: " + GameState.CurX + " Y: " + GameState.CurY;
                string Loc = "loc X: " + GetPlayerX(GameState.MyIndex) + " Y: " + GetPlayerY(GameState.MyIndex);
                string Map = " (Map #" + GetPlayerMap(GameState.MyIndex) + ")";

                Text.RenderText(Cur, (int)Math.Round(GameState.DrawLocX), (int)Math.Round(GameState.DrawLocY + 105f),
                    Color.Yellow, Color.Black);
                Text.RenderText(Loc, (int)Math.Round(GameState.DrawLocX), (int)Math.Round(GameState.DrawLocY + 120f),
                    Color.Yellow, Color.Black);
                Text.RenderText(Map, (int)Math.Round(GameState.DrawLocX), (int)Math.Round(GameState.DrawLocY + 135f),
                    Color.Yellow, Color.Black);
            }

            Text.DrawMapName();

            if (GameState.MyEditorType == (int)EditorType.Map)
            {
                if (GameState.MapEditorTab == (int)MapEditorTab.Events)
                {
                    DrawEvents();
                }
                
            }

            DrawBars();
            Map.DrawMapFade();
            Gui.Render();
            string argpath = System.IO.Path.Combine(Core.Path.Misc, "Cursor");
            RenderTexture(ref argpath, GameState.CurMouseX, GameState.CurMouseY, 0, 0, 16, 16, 32, 32);
        }

        public static void Render_Menu()
        {
            Gui.DrawMenuBG();
            Gui.Render();
            string argpath = System.IO.Path.Combine(Core.Path.Misc, "Cursor");
            RenderTexture(ref argpath, GameState.CurMouseX, GameState.CurMouseY, 0, 0, 16, 16, 32, 32);
        }

        public static void UpdateMapAttributes()
        {
            if (GameState.MapEditorTab == (int)MapEditorTab.Attributes)
            {
                Text.DrawMapAttributes();
            }
        }

        public static void UpdateDirBlock()
        {
            int x;
            int y;

            if (GameState.MapEditorTab == (int)MapEditorTab.Directions)
            {
                var loopTo10 = (int)Math.Round(GameState.TileView.Right + 1d);
                for (x = (int)Math.Round(GameState.TileView.Left - 1d); x < loopTo10; x++)
                {
                    var loopTo11 = (int)Math.Round(GameState.TileView.Bottom + 1d);
                    for (y = (int)Math.Round(GameState.TileView.Top - 1d); y < loopTo11; y++)
                    {
                        if (GameLogic.IsValidMapPoint(x, y))
                        {
                            DrawDirections(x, y);
                        }
                    }
                }
            }
        }
    }
}