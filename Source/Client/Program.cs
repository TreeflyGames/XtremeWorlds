using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Concurrent;
using System.Diagnostics;
using static Core.Enum;
using static Core.Global.Command;
using Color = Microsoft.Xna.Framework.Color;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using System;
using System.Reflection;

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

        public GameClient()
        {
            General.GetResolutionSize(SettingsManager.Instance.Resolution, ref GameState.ResolutionWidth,
                ref GameState.ResolutionHeight);

            Graphics = new GraphicsDeviceManager(this);

            // Set basic properties for GraphicsDeviceManager
            ref var withBlock = ref Graphics;
            withBlock.GraphicsProfile = GraphicsProfile.Reach;
            withBlock.IsFullScreen = SettingsManager.Instance.Fullscreen;
            withBlock.PreferredBackBufferWidth = GameState.ResolutionWidth;
            withBlock.PreferredBackBufferHeight = GameState.ResolutionHeight;
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

        public class RenderCommand
        {
            public byte Type { get; set; }
            public string Path { get; set; }
            public string Text { get; set; }
            public Rectangle sRect { get; set; }
            public Rectangle dRect { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public Color Color { get; set; }
            public Color Color2 { get; set; }
            public int EntityID { get; set; }
            public int TextureID { get; set; }
        }

        private static void LoadFonts()
        {
            for (int i = 1; i < (int)FontType.Count; i++)
                Text.Fonts[(FontType)i] = LoadFont(Core.Path.Fonts, (FontType)i);
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

        public static SpriteFont LoadFont(string path, FontType font)
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

        public static void RenderTexture(ref string path, int dX, int dY, int sX, int sY, int dW, int dH, int sW = 1,
            int sH = 1, float alpha = 1.0f, byte red = 255, byte green = 255, byte blue = 255)
        {
            // Create destination and source rectangles
            var dRect = new Rectangle(dX, dY, dW, dH);
            var sRect = new Rectangle(sX, sY, sW, sH);
            var color = new Color(red, green, blue, (byte)255);
            color = color * alpha;

            path = Core.Path.EnsureFileExtension(path);

            // Retrieve the texture
            var texture = GetTexture(path);
            if (texture is null)
            {
                return;
            }

            SpriteBatch.Draw(texture, dRect, sRect, color);
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
            var mouseState = Mouse.GetState();

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
                                             Math.Floor((mouseX + GameState.Camera.Left) / GameState.PicX));
            GameState.CurY = (int)Math.Round(GameState.TileView.Top +
                                             Math.Floor((mouseY + GameState.Camera.Top) / GameState.PicY));

            // Store raw mouse coordinates for interface interactions
            GameState.CurMouseX = mouseX;
            GameState.CurMouseY = mouseY;

            // Check for action keys
            GameState.VbKeyControl = CurrentKeyboardState.IsKeyDown(Keys.LeftControl);
            GameState.VbKeyShift = CurrentKeyboardState.IsKeyDown(Keys.LeftShift);

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
                        GameState.inSmallChat = Conversions.ToBoolean(0);
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
                        if (activeControl.CallBack[(int)EntState.Enter] is not null)
                        {
                            activeControl.CallBack[(int)EntState.Enter].Invoke();
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
                        if (GameState.CurLayer < (int)Core.Enum.LayerType.Count)
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
                    Gui.HandleInterfaceEvents(EntState.MouseScroll);
                }
            }
        }

        private static void HandleMouseClick()
        {
            int currentTime = Environment.TickCount;

            // Handle MouseMove event when the mouse moves
            if (CurrentMouseState.X != PreviousMouseState.X || CurrentMouseState.Y != PreviousMouseState.Y)
            {
                Gui.HandleInterfaceEvents(EntState.MouseMove);
            }

            // Check for MouseDown event (button pressed)
            if (IsMouseButtonDown(MouseButton.Left))
            {
                if ((DateTime.Now - lastMouseClickTime).TotalMilliseconds >= mouseClickCooldown)
                {
                    Gui.HandleInterfaceEvents(EntState.MouseDown);
                    lastMouseClickTime = DateTime.Now; // Update last mouse click time
                    GameState.LastLeftClickTime = currentTime; // Track time for double-click detection
                    GameState.ClickCount++;
                }

                if (GameState.ClickCount >= 2)
                {
                    Gui.HandleInterfaceEvents(EntState.DblClick);
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
                Gui.HandleInterfaceEvents(EntState.MouseUp);
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
                            if (Gui.Windows[i].Controls[j].State != Core.Enum.EntState.Normal)
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
                if (GameState.MyEditorType == (int)EditorType.Map)
                {
                    frmEditor_Map.MapEditorMouseDown(GameState.CurX, GameState.CurY, false);
                }             
                
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
                        if (GetPlayerAccess(GameState.MyIndex) >= (int)AccessType.Moderator)
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
                case (int)ColorType.Black:
                {
                    return Color.Black;
                }
                case (int)ColorType.Blue:
                {
                    return Color.Blue;
                }
                case (int)ColorType.Green:
                {
                    return Color.Green;
                }
                case (int)ColorType.Cyan:
                {
                    return Color.Cyan;
                }
                case (int)ColorType.Red:
                {
                    return Color.Red;
                }
                case (int)ColorType.Magenta:
                {
                    return Color.Magenta;
                }
                case (int)ColorType.Brown:
                {
                    return Color.Brown;
                }
                case (int)ColorType.Gray:
                {
                    return Color.LightGray;
                }
                case (int)ColorType.DarkGray:
                {
                    return Color.Gray;
                }
                case (int)ColorType.BrightBlue:
                {
                    return Color.LightBlue;
                }
                case (int)ColorType.BrightGreen:
                {
                    return Color.LightGreen;
                }
                case (int)ColorType.BrightCyan:
                {
                    return Color.LightCyan;
                }
                case (int)ColorType.BrightRed:
                {
                    return Color.LightCoral;
                }
                case (int)ColorType.Pink:
                {
                    return Color.Orchid;
                }
                case (int)ColorType.Yellow:
                {
                    return Color.Yellow;
                }
                case (int)ColorType.White:
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
            if (Conversions.ToInteger(GameState.ShowAnimLayers) == 1)
            {
                anim = 1;
            }
            else
            {
                anim = 0;
            }

            rec.Y = 0;
            rec.Height = GameState.PicX;
            rec.X = (int)Math.Round(anim *
                                    (GetGfxInfo(System.IO.Path.Combine(Core.Path.Emotes, sprite.ToString())).Width /
                                     2d));
            rec.Width = (int)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Emotes, sprite.ToString())).Width /
                                        2d);

            x = GameLogic.ConvertMapX(x2);
            y = GameLogic.ConvertMapY(y2) - (GameState.PicY + 16);

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
            RenderTexture(ref argpath, GameLogic.ConvertMapX(x * GameState.PicX),
                GameLogic.ConvertMapY(y * GameState.PicY),
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
                    var ret = GameLogic.IsDirBlocked(ref Core.Type.MyMap.Tile[x, y].DirBlock, ref argdir);
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
                RenderTexture(ref argpath1, GameLogic.ConvertMapX(x * GameState.PicX) + GameState.DirArrowX[i],
                    GameLogic.ConvertMapY(y * GameState.PicY) + GameState.DirArrowY[i], rec.X, rec.Y, rec.Width,
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

        public static void DrawNPC(double MapNPCNum)
        {
            byte anim;
            int x;
            int y;
            int sprite;
            var spriteLeft = default(int);
            Rectangle rect;
            int attackSpeed = 1000;

            // Check if NPC exists
            if (Core.Type.MyMapNPC[(int)MapNPCNum].Num < 0 ||
                Core.Type.MyMapNPC[(int)MapNPCNum].Num > Core.Constant.MAX_NPCS)
                return;

            // Ensure NPC is within the tile view range
            if (Core.Type.MyMapNPC[(int)MapNPCNum].X < GameState.TileView.Left |
                Core.Type.MyMapNPC[(int)MapNPCNum].X > GameState.TileView.Right)
                return;

            if (Core.Type.MyMapNPC[(int)MapNPCNum].Y < GameState.TileView.Top |
                Core.Type.MyMapNPC[(int)MapNPCNum].Y > GameState.TileView.Bottom)
                return;

            // Stream NPC if not yet loaded
            Database.StreamNPC((int)Core.Type.MyMapNPC[(int)MapNPCNum].Num);

            // Get the sprite of the NPC
            sprite = Core.Type.NPC[(int)Core.Type.MyMapNPC[(int)MapNPCNum].Num].Sprite;

            // Validate sprite
            if (sprite < 1 | sprite > GameState.NumCharacters)
                return;

            // Reset animation frame
            anim = 0;

            // Check for attacking animation
            if (Core.Type.MyMapNPC[(int)MapNPCNum].AttackTimer + attackSpeed / 2d > General.GetTickCount() &&
                Core.Type.MyMapNPC[(int)MapNPCNum].Attacking == 1)
            {
                anim = 3;
            }
            else
            {
                // Walking animation based on direction
                switch (Core.Type.MyMapNPC[(int)MapNPCNum].Dir)
                {
                    case (int)DirectionType.Up:
                    {
                        if (Core.Type.MyMapNPC[(int)MapNPCNum].YOffset > 8)
                            anim = (byte)Core.Type.MyMapNPC[(int)MapNPCNum].Steps;
                        break;
                    }
                    case (int)DirectionType.Down:
                    {
                        if (Core.Type.MyMapNPC[(int)MapNPCNum].YOffset < -8)
                            anim = (byte)Core.Type.MyMapNPC[(int)MapNPCNum].Steps;
                        break;
                    }
                    case (int)DirectionType.Left:
                    {
                        if (Core.Type.MyMapNPC[(int)MapNPCNum].XOffset > 8)
                            anim = (byte)Core.Type.MyMapNPC[(int)MapNPCNum].Steps;
                        break;
                    }
                    case (int)DirectionType.Right:
                    {
                        if (Core.Type.MyMapNPC[(int)MapNPCNum].XOffset < -8)
                            anim = (byte)Core.Type.MyMapNPC[(int)MapNPCNum].Steps;
                        break;
                    }
                }
            }

            // Reset attacking state if attack timer has passed
            {
                ref var withBlock = ref Core.Type.MyMapNPC[(int)MapNPCNum];
                if (withBlock.AttackTimer + attackSpeed < General.GetTickCount())
                {
                    withBlock.Attacking = 0;
                    withBlock.AttackTimer = 0;
                }
            }

            // Set sprite sheet position based on direction
            switch (Core.Type.MyMapNPC[(int)MapNPCNum].Dir)
            {
                case (int)DirectionType.Up:
                {
                    spriteLeft = 3;
                    break;
                }
                case (int)DirectionType.Right:
                {
                    spriteLeft = 2;
                    break;
                }
                case (int)DirectionType.Down:
                {
                    spriteLeft = 0;
                    break;
                }
                case (int)DirectionType.Left:
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
            x = (int)Math.Round(Core.Type.MyMapNPC[(int)MapNPCNum].X * GameState.PicX +
                                Core.Type.MyMapNPC[(int)MapNPCNum].XOffset -
                                (GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString())).Width /
                                 4d -
                                 32d) / 2d);

            if (GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString())).Height / 4d > 32d)
            {
                // Larger sprites need an offset for height adjustment
                y = (int)Math.Round(Core.Type.MyMapNPC[(int)MapNPCNum].Y * GameState.PicY +
                                    Core.Type.MyMapNPC[(int)MapNPCNum].YOffset -
                                    (GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, sprite.ToString()))
                                            .Height /
                                        4d - 32d));
            }
            else
            {
                // Normal sprite height
                y = Core.Type.MyMapNPC[(int)MapNPCNum].Y * GameState.PicY + Core.Type.MyMapNPC[(int)MapNPCNum].YOffset;
            }

            // Draw shadow and NPC sprite
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

            if (Core.Type.MyMapItem[itemNum].Num < 0 | Core.Type.MyMapItem[itemNum].Num > Core.Constant.MAX_ITEMS)
                return;

            Item.StreamItem((int)Core.Type.MyMapItem[itemNum].Num);

            picNum = Core.Type.Item[(int)Core.Type.MyMapItem[itemNum].Num].Icon;

            if (picNum < 1 | picNum > GameState.NumItems)
                return;

            {
                ref var withBlock = ref Core.Type.MyMapItem[itemNum];
                if (withBlock.X < GameState.TileView.Left | withBlock.X > GameState.TileView.Right)
                    return;

                if (withBlock.Y < GameState.TileView.Top | withBlock.Y > GameState.TileView.Bottom)
                    return;
            }

            srcrec = new Rectangle(0, 0, GameState.PicX, GameState.PicY);
            destrec = new Rectangle(GameLogic.ConvertMapX(Core.Type.MyMapItem[itemNum].X * GameState.PicX),
                GameLogic.ConvertMapY(Core.Type.MyMapItem[itemNum].Y * GameState.PicY), GameState.PicX, GameState.PicY);

            x = GameLogic.ConvertMapX(Core.Type.MyMapItem[itemNum].X * GameState.PicX);
            y = GameLogic.ConvertMapY(Core.Type.MyMapItem[itemNum].Y * GameState.PicY);

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
                ref var withBlock = ref Core.Type.Blood[index];
                if (withBlock.X < GameState.TileView.Left | withBlock.X > GameState.TileView.Right)
                    return;
                if (withBlock.Y < GameState.TileView.Top | withBlock.Y > GameState.TileView.Bottom)
                    return;

                // check if we should be seeing it
                if (withBlock.Timer + 20000 < General.GetTickCount())
                    return;

                x = GameLogic.ConvertMapX(Core.Type.Blood[index].X * GameState.PicX);
                y = GameLogic.ConvertMapY(Core.Type.Blood[index].Y * GameState.PicY);

                srcrec = new Rectangle((withBlock.Sprite - 1) * GameState.PicX, 0, GameState.PicX, GameState.PicY);
                destrec = new Rectangle(GameLogic.ConvertMapX(withBlock.X * GameState.PicX),
                    GameLogic.ConvertMapY(withBlock.Y * GameState.PicY), GameState.PicX, GameState.PicY);

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
            long NPCNum;

            // dynamic bar calculations
            Width = GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Bars")).Width;
            Height = (long)Math.Round(GetGfxInfo(System.IO.Path.Combine(Core.Path.Misc, "Bars")).Height / 4d);

            // render NPC health bars
            for (i = 0L; i < Constant.MAX_MAP_NPCS; i++)
            {
                NPCNum = (long)Core.Type.MyMapNPC[(int)i].Num;
                // exists?
                if (NPCNum >= 0L && NPCNum <= Core.Constant.MAX_NPCS)
                {
                    // alive?
                    if (Core.Type.MyMapNPC[(int)i].Vital[(int)VitalType.HP] > 0 &
                        Core.Type.MyMapNPC[(int)i].Vital[(int)VitalType.HP] < Core.Type.NPC[(int)NPCNum].HP)
                    {
                        // lock to NPC
                        tmpX = (long)Math.Round(Core.Type.MyMapNPC[(int)i].X * GameState.PicX +
                            Core.Type.MyMapNPC[(int)i].XOffset + 16 - Width / 2d);
                        tmpY = Core.Type.MyMapNPC[(int)i].Y * GameState.PicY + Core.Type.MyMapNPC[(int)i].YOffset + 35;

                        // calculate the width to fill
                        if (Width > 0L)
                            GameState.BarWidth_NPCHP_Max[(int)i] = (long)Math.Round(
                                Core.Type.MyMapNPC[(int)i].Vital[(int)VitalType.HP] / (double)Width /
                                (Core.Type.NPC[(int)NPCNum].HP / (double)Width) * Width);

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
                            (int)Left, (int)Top, (int)GameState.BarWidth_NPCHP[(int)i], (int)Height,
                            (int)GameState.BarWidth_NPCHP[(int)i], (int)Height);
                    }
                }
            }

            for (i = 0L; i < Constant.MAX_PLAYERS; i++)
            {
                if (GetPlayerMap((int)i) == GetPlayerMap((int)i))
                {
                    if (GetPlayerVital((int)i, VitalType.HP) > 0 &
                        GetPlayerVital((int)i, VitalType.HP) < GetPlayerMaxVital((int)i, VitalType.HP))
                    {
                        // lock to Player
                        tmpX = (long)Math.Round(GetPlayerX((int)i) * GameState.PicX + Core.Type.Player[(int)i].XOffset +
                            16 - Width / 2d);
                        tmpY = GetPlayerY((int)i) * GameState.PicY + Core.Type.Player[(int)i].YOffset + 35;

                        // calculate the width to fill
                        if (Width > 0L)
                            GameState.BarWidth_PlayerHP_Max[(int)i] = (long)Math.Round(
                                GetPlayerVital((int)i, VitalType.HP) / (double)Width /
                                (GetPlayerMaxVital((int)i, VitalType.HP) / (double)Width) * Width);

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

                    if (GetPlayerVital((int)i, VitalType.SP) > 0 &
                        GetPlayerVital((int)i, VitalType.SP) < GetPlayerMaxVital((int)i, VitalType.SP))
                    {
                        // lock to Player
                        tmpX = (long)Math.Round(GetPlayerX((int)i) * GameState.PicX + Core.Type.Player[(int)i].XOffset +
                            16 - Width / 2d);
                        tmpY = GetPlayerY((int)i) * GameState.PicY + Core.Type.Player[(int)i].YOffset + 35 + Height;

                        // calculate the width to fill
                        if (Width > 0L)
                            GameState.BarWidth_PlayerSP_Max[(int)i] = (long)Math.Round(
                                GetPlayerVital((int)i, VitalType.SP) / (double)Width /
                                (GetPlayerMaxVital((int)i, VitalType.SP) / (double)Width) * Width);

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
                        if ((int)Core.Type.Player[(int)i].Skill[GameState.SkillBuffer].Num >= 0)
                        {
                            if (Core.Type.Skill[(int)Core.Type.Player[(int)i].Skill[GameState.SkillBuffer].Num]
                                    .CastTime >
                                0)
                            {
                                // lock to player
                                tmpX = (long)Math.Round(GetPlayerX((int)i) * GameState.PicX +
                                    Core.Type.Player[(int)i].XOffset + 16 - Width / 2d);
                                tmpY = GetPlayerY((int)i) * GameState.PicY + Core.Type.Player[(int)i].YOffset + 35 +
                                       Height;

                                // calculate the width to fill
                                if (Width > 0L)
                                    barWidth = (long)Math.Round((General.GetTickCount() - GameState.SkillBufferTimer) /
                                        (double)(Core.Type
                                            .Skill[(int)Core.Type.Player[(int)i].Skill[GameState.SkillBuffer].Num]
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
            var position = new Vector2(GameLogic.ConvertMapX(GameState.CurX * GameState.PicX),
                GameLogic.ConvertMapY(GameState.CurY * GameState.PicY));
            var size = new Vector2(GameState.PicX, GameState.PicX);
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
                        int posX = GameLogic.ConvertMapX((int)Math.Round((x - 1d) * GameState.PicX));
                        int posY = GameLogic.ConvertMapY((int)Math.Round((y - 1d) * GameState.PicY));
                        int rectWidth = GameState.PicX;
                        int rectHeight = GameState.PicY;

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
                ref var withBlock = ref Core.Type.ChatBubble[(int)Index];

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
                        x = GameLogic.ConvertMapX(Core.Type.Player[withBlock.Target].X * 32 +
                                                  Core.Type.Player[withBlock.Target].XOffset) + 16;
                        y = GameLogic.ConvertMapY(Core.Type.Player[withBlock.Target].Y * 32 +
                                                  Core.Type.Player[withBlock.Target].YOffset) - 32;
                        break;
                    }
                    case (byte)TargetType.Event:
                    {
                        x = GameLogic.ConvertMapX(Core.Type.MyMap.Event[withBlock.Target].X * 32) + 16;
                        y = GameLogic.ConvertMapY(Core.Type.MyMap.Event[withBlock.Target].Y * 32) - 16;
                        break;
                    }

                    case (byte)TargetType.NPC:
                    {
                        x = GameLogic.ConvertMapX(Core.Type.MyMapNPC[withBlock.Target].X * 32) + 16;
                        y = GameLogic.ConvertMapY(Core.Type.MyMapNPC[withBlock.Target].Y * 32) - 32;
                        break;
                    }

                    case (byte)TargetType.Pet:
                    {
                        x = 0;
                        y = 0;
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
                Text.WordWrap(withBlock.Msg, FontType.Georgia, GameState.ChatBubbleWidth, ref theArray);

                // find max width
                tmpNum = Information.UBound(theArray);

                var loopTo = tmpNum;
                for (i = 0L; i <= loopTo; i++)
                {
                    if (Text.GetTextWidth(theArray[(int)i], FontType.Georgia) > MaxWidth)
                        MaxWidth = Text.GetTextWidth(theArray[(int)i], FontType.Georgia);
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
                    var textSize = Text.Fonts[FontType.Georgia].MeasureString(theArray[(int)i]);
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
                    withBlock.Active = Conversions.ToBoolean(0);
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
            if (GetPlayerEquipment(index, EquipmentType.Weapon) >= 0)
            {
                attackSpeed = Core.Type.Item[GetPlayerEquipment(index, EquipmentType.Weapon)].Speed;
            }
            else
            {
                attackSpeed = 1000;
            }

            // Reset frame
            anim = 0;

            // Check for attacking animation
            if (Core.Type.Player[index].AttackTimer + attackSpeed / 2d > General.GetTickCount())
            {
                if (Core.Type.Player[index].Attacking == 1)
                {
                    anim = 3;
                }
            }
            else
            {
                // If not attacking, walk normally
                switch (GetPlayerDir(index))
                {
                    case (int)DirectionType.Up:
                    {

                        if (Core.Type.Player[index].YOffset > 8)
                            anim = Core.Type.Player[index].Steps;
                        break;
                    }
                    case (int)DirectionType.Down:
                    {

                        if (Core.Type.Player[index].YOffset < -8)
                            anim = Core.Type.Player[index].Steps;
                        break;
                    }
                    case (int)DirectionType.Left:
                    {

                        if (Core.Type.Player[index].XOffset > 8)
                            anim = Core.Type.Player[index].Steps;
                        break;
                    }
                    case (int)DirectionType.Right:
                    {

                        if (Core.Type.Player[index].XOffset < -8)
                            anim = Core.Type.Player[index].Steps;
                        break;
                    }
                    case (int)DirectionType.UpRight:
                    {
                        if (Core.Type.Player[index].XOffset < -8)
                            anim = Core.Type.Player[index].Steps;
                        if (Core.Type.Player[index].YOffset > 8)
                            anim = Core.Type.Player[index].Steps;
                        break;
                    }

                    case (int)DirectionType.UpLeft:
                    {
                        if (Core.Type.Player[index].XOffset > 8)
                            anim = Core.Type.Player[index].Steps;
                        if (Core.Type.Player[index].YOffset > 8)
                            anim = Core.Type.Player[index].Steps;
                        break;
                    }

                    case (int)DirectionType.DownRight:
                    {
                        if (Core.Type.Player[index].XOffset < -8)
                            anim = Core.Type.Player[index].Steps;
                        if (Core.Type.Player[index].YOffset < -8)
                            anim = Core.Type.Player[index].Steps;
                        break;
                    }

                    case (int)DirectionType.DownLeft:
                    {
                        if (Core.Type.Player[index].XOffset > 8)
                            anim = Core.Type.Player[index].Steps;
                        if (Core.Type.Player[index].YOffset < -8)
                            anim = Core.Type.Player[index].Steps;
                        break;
                    }

                }

            }

            // Check to see if we want to stop making him attack
            {
                ref var withBlock = ref Core.Type.Player[index];
                if (withBlock.AttackTimer + attackSpeed < General.GetTickCount())
                {
                    withBlock.Attacking = 0;
                    withBlock.AttackTimer = 0;
                }

            }

            // Set the left
            switch (GetPlayerDir(index))
            {
                case (int)DirectionType.Up:
                {
                    spriteleft = 3;
                    break;
                }
                case (int)DirectionType.Right:
                {
                    spriteleft = 2;
                    break;
                }
                case (int)DirectionType.Down:
                {
                    spriteleft = 0;
                    break;
                }
                case (int)DirectionType.Left:
                {
                    spriteleft = 1;
                    break;
                }
                case (int)DirectionType.UpRight:
                {
                    spriteleft = 2;
                    break;
                }
                case (int)DirectionType.UpLeft:
                {
                    spriteleft = 1;
                    break;
                }
                case (int)DirectionType.DownLeft:
                {
                    spriteleft = 1;
                    break;
                }
                case (int)DirectionType.DownRight:
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
            x = (int)Math.Round(Core.Type.Player[index].X * GameState.PicX + Core.Type.Player[index].XOffset -
                                (gfxInfo.Width / 4d - 32d) / 2d);

            // Is the player's height more than 32..?
            if (gfxInfo.Height > 32)
            {
                // Create a 32 pixel offset for larger sprites
                y = (int)Math.Round(GetPlayerY(index) * GameState.PicY + Core.Type.Player[index].YOffset -
                                    (gfxInfo.Height / 4d - 32d));
            }
            else
            {
                // Proceed as normal
                y = GetPlayerY(index) * GameState.PicY + Core.Type.Player[index].YOffset;
            }

            rect = new Rectangle((int)Math.Round(anim * (gfxInfo.Width / 4d)),
                (int)Math.Round(spriteleft * (gfxInfo.Height / 4d)), (int)Math.Round(gfxInfo.Width / 4d),
                (int)Math.Round(gfxInfo.Height / 4d));

            // render the actual sprite
            // DrawShadow(x, y + 16)
            DrawCharacterSprite(spriteNum, x, y, rect);

            // check for paperdolling
            for (int i = 0; i < (int)EquipmentType.Count; i++)
            {
                if (GetPlayerEquipment(index, (EquipmentType)i) >= 0)
                {
                    if (Core.Type.Item[GetPlayerEquipment(index, (EquipmentType)i)].Paperdoll > 0)
                    {
                        DrawPaperdoll(x, y, Core.Type.Item[GetPlayerEquipment(index, (EquipmentType)i)].Paperdoll, anim,
                            spriteleft);
                    }
                }
            }

            // Check to see if we want to stop showing emote
            {
                ref var withBlock1 = ref Core.Type.Player[index];
                if (withBlock1.EmoteTimer < General.GetTickCount())
                {
                    withBlock1.Emote = 0;
                    withBlock1.EmoteTimer = 0;
                }
            }

            // check for emotes
            if (Core.Type.Player[GameState.MyIndex].Emote > 0)
            {
                DrawEmote(x, y, Core.Type.Player[GameState.MyIndex].Emote);
            }
        }

        public static void DrawEvents()
        {
            if (Core.Type.MyMap.EventCount <= 0)
                return; // Exit early if no events

            for (int i = 0, loopTo = Core.Type.MyMap.EventCount; i < loopTo; i++)
            {
                int x = GameLogic.ConvertMapX(Core.Type.MyMap.Event[i].X * GameState.PicX);
                int y = GameLogic.ConvertMapY(Core.Type.MyMap.Event[i].Y * GameState.PicY);

                // Skip event if there are no pages
                if (Core.Type.MyMap.Event[i].PageCount <= 0)
                {
                    DrawOutlineRectangle(x, y, GameState.PicX, GameState.PicY, Color.Blue, 0.6f);
                    continue;
                }

                // Render event based on its graphic type
                switch (Core.Type.MyMap.Event[i].Pages[0].GraphicType)
                {
                    case 0: // Text Event
                    {
                        int tX = x + GameState.PicX / 2 - 4;
                        int tY = y + GameState.PicY / 2 - 7;
                        Text.RenderText("E", tX, tY, Color.Green, Color.Black);
                        break;
                    }

                    case 1: // Character Graphic
                    {
                        RenderCharacterGraphic(Core.Type.MyMap.Event[i], x, y);
                        break;
                    }

                    case 2: // Tileset Graphic
                    {
                        RenderTilesetGraphic(Core.Type.MyMap.Event[i], x, y);
                        break;
                    }

                    default:
                    {
                        // Draw fallback outline rectangle if graphic type is unknown
                        DrawOutlineRectangle(x, y, GameState.PicX, GameState.PicY, Color.Blue, 0.6f);
                        break;
                    }
                }
            }
        }

        public static void RenderCharacterGraphic(Core.Type.EventStruct eventData, int x, int y)
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

        private static void RenderTilesetGraphic(Core.Type.EventStruct eventData, int x, int y)
        {
            int gfxIndex = eventData.Pages[0].Graphic;

            if (gfxIndex > 0 && gfxIndex <= GameState.NumTileSets)
            {
                // Define source rectangle from tileset graphics
                var srcRect = new Rectangle(eventData.Pages[0].GraphicX * 32, eventData.Pages[0].GraphicY * 32,
                    eventData.Pages[0].GraphicX2 * 32, eventData.Pages[0].GraphicY2 * 32);

                // Adjust position if the tile is larger than 32x32
                if (srcRect.Height > 32)
                    y -= GameState.PicY;

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
                DrawOutlineRectangle(x, y, GameState.PicX, GameState.PicY, Color.Blue, 0.6f);
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
                if (Core.Type.MapEvents[id].Visible == false)
                {
                    return;
                }

                switch (Core.Type.MapEvents[id].GraphicType)
                {
                    case 0:
                    {
                        return;
                    }
                    case 1:
                    {
                        if (Core.Type.MapEvents[id].Graphic <= 0 |
                            Core.Type.MapEvents[id].Graphic > GameState.NumCharacters)
                            return;

                        // Reset frame
                        if (Core.Type.MapEvents[id].Steps == 3)
                        {
                            anim = 0;
                        }
                        else if (Core.Type.MapEvents[id].Steps == 1)
                        {
                            anim = 2;
                        }

                        switch (Core.Type.MapEvents[id].Dir)
                        {
                            case (int)DirectionType.Up:
                            {
                                if (Core.Type.MapEvents[id].YOffset > 8)
                                    anim = Core.Type.MapEvents[id].Steps;
                                break;
                            }
                            case (int)DirectionType.Down:
                            {
                                if (Core.Type.MapEvents[id].YOffset < -8)
                                    anim = Core.Type.MapEvents[id].Steps;
                                break;
                            }
                            case (int)DirectionType.Left:
                            {
                                if (Core.Type.MapEvents[id].XOffset > 8)
                                    anim = Core.Type.MapEvents[id].Steps;
                                break;
                            }
                            case (int)DirectionType.Right:
                            {
                                if (Core.Type.MapEvents[id].XOffset < -8)
                                    anim = Core.Type.MapEvents[id].Steps;
                                break;
                            }
                        }

                        // Set the left
                        switch (Core.Type.MapEvents[id].ShowDir)
                        {
                            case (int)DirectionType.Up:
                            {
                                spritetop = 3;
                                break;
                            }
                            case (int)DirectionType.Right:
                            {
                                spritetop = 2;
                                break;
                            }
                            case (int)DirectionType.Down:
                            {
                                spritetop = 0;
                                break;
                            }
                            case (int)DirectionType.Left:
                            {
                                spritetop = 1;
                                break;
                            }
                        }

                        if (Core.Type.MapEvents[id].WalkAnim == 1)
                            anim = 0;

                        if (Core.Type.MapEvents[id].Moving == 0)
                            anim = Core.Type.MapEvents[id].GraphicX;

                        var gfxInfo = GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters,
                            Core.Type.MapEvents[id].Graphic.ToString()));
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
                        x = (int)Math.Round(Core.Type.MapEvents[id].X * GameState.PicX +
                                            Core.Type.MapEvents[id].XOffset -
                                            (width - 32d) / 2d);

                        // Is the player's height more than 32..?
                        if (gfxInfo.Height * 4 > 32)
                        {
                            // Create a 32 pixel offset for larger sprites
                            y = (int)Math.Round(Core.Type.MapEvents[id].Y * GameState.PicY +
                                Core.Type.MapEvents[id].YOffset - (height - 32d));
                        }
                        else
                        {
                            // Proceed as normal
                            y = Core.Type.MapEvents[id].Y * GameState.PicY + Core.Type.MapEvents[id].YOffset;
                        }

                        // render the actual sprite
                        DrawCharacterSprite(Core.Type.MapEvents[id].Graphic, x, y, sRect);
                        break;
                    }
                    case 2:
                    {
                        if (Core.Type.MapEvents[id].Graphic < 1 |
                            Core.Type.MapEvents[id].Graphic > GameState.NumTileSets)
                            return;

                        if (Core.Type.MapEvents[id].GraphicY2 > 0 | Core.Type.MapEvents[id].GraphicX2 > 0)
                        {
                            sRect.X = Core.Type.MapEvents[id].GraphicX * 32;
                            sRect.Y = Core.Type.MapEvents[id].GraphicY * 32;
                            sRect.Width = Core.Type.MapEvents[id].GraphicX2 * 32;
                            sRect.Height = Core.Type.MapEvents[id].GraphicY2 * 32;
                        }
                        else
                        {
                            sRect.X = Core.Type.MapEvents[id].GraphicY * 32;
                            sRect.Height = sRect.Top + 32;
                            sRect.Y = Core.Type.MapEvents[id].GraphicX * 32;
                            sRect.Width = sRect.Left + 32;
                        }

                        x = Core.Type.MapEvents[id].X * 32;
                        y = Core.Type.MapEvents[id].Y * 32;
                        x = (int)Math.Round(x - (sRect.Right - sRect.Left) / 2d);
                        y = y - (sRect.Bottom - sRect.Top) + 32;

                        if (Core.Type.MapEvents[id].GraphicY2 > 1)
                        {
                            string argpath = System.IO.Path.Combine(Core.Path.Tilesets,
                                Core.Type.MapEvents[id].Graphic.ToString());
                            RenderTexture(ref argpath,
                                GameLogic.ConvertMapX(Core.Type.MapEvents[id].X * GameState.PicX),
                                GameLogic.ConvertMapY(Core.Type.MapEvents[id].Y * GameState.PicY) - GameState.PicY,
                                sRect.Left, sRect.Top, sRect.Width, sRect.Height);
                        }
                        else
                        {
                            string argpath1 = System.IO.Path.Combine(Core.Path.Tilesets,
                                Core.Type.MapEvents[id].Graphic.ToString());
                            RenderTexture(ref argpath1,
                                GameLogic.ConvertMapX(Core.Type.MapEvents[id].X * GameState.PicX),
                                GameLogic.ConvertMapY(Core.Type.MapEvents[id].Y * GameState.PicY), sRect.Left,
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

            if (GameState.NumPanoramas > 0 & Core.Type.MyMap.Panorama > 0)
            {
                Map.DrawPanorama(Core.Type.MyMap.Panorama);
            }

            if (GameState.NumParallax > 0 & Core.Type.MyMap.Parallax > 0)
            {
                Map.DrawParallax(Core.Type.MyMap.Parallax);
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
                if (GameState.CurrentEvents > 0 & GameState.CurrentEvents <= Core.Type.MyMap.EventCount)
                {
                    var loopTo2 = GameState.CurrentEvents;
                    for (i = 0; i < loopTo2; i++)
                    {
                        if (i < Core.Type.MapEvents.Length)
                        {
                            if (Core.Type.MapEvents[i].Position == 0)
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
                    if (Core.Type.MyMapItem[i].Num >= 0)
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
                    if (Animation.AnimInstance[i].Used[0])
                    {
                        Animation.DrawAnimation(i, 0);
                    }
                }
            }

            // Y-based render. Renders Players, NPCs and Resources based on Y-axis.
            var loopTo3 = (int)Core.Type.MyMap.MaxY;
            for (y = 0; y < loopTo3; y++)
            {
                if (GameState.NumCharacters > 0)
                {
                    // NPCs
                    for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
                    {
                        if (Core.Type.MyMapNPC[i].Y == y)
                        {
                            DrawNPC(i);
                        }
                    }

                    // Players
                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i) & GetPlayerMap(i) == GetPlayerMap(GameState.MyIndex))
                        {
                            if (Core.Type.Player[i].Y == y)
                            {
                                DrawPlayer(i);
                            }
                        }
                    }

                    if (GameState.MyEditorType != (int)EditorType.Map)
                    {
                        if (GameState.CurrentEvents > 0 & GameState.CurrentEvents <= Core.Type.MyMap.EventCount)
                        {
                            var loopTo4 = GameState.CurrentEvents;
                            for (i = 0; i < loopTo4; i++)
                            {
                                if (Core.Type.MapEvents[i].Position == 1)
                                {
                                    if (y == Core.Type.MapEvents[i].Y)
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
                                    if (Core.Type.Player[GameState.MyTarget].Map ==
                                        Core.Type.Player[GameState.MyIndex].Map)
                                    {
                                        if (Core.Type.Player[GameState.MyTarget].Sprite > 0)
                                        {
                                            // Draw the target icon for the player
                                            DrawTarget(
                                                Core.Type.Player[GameState.MyTarget].X * 32 - 16 +
                                                Core.Type.Player[GameState.MyTarget].XOffset,
                                                Core.Type.Player[GameState.MyTarget].Y * 32 +
                                                Core.Type.Player[GameState.MyTarget].YOffset);
                                        }
                                    }
                                }

                                break;

                            case (int)TargetType.NPC:
                                DrawTarget(
                                    Core.Type.MyMapNPC[GameState.MyTarget].X * 32 - 16 +
                                    Core.Type.MyMapNPC[GameState.MyTarget].XOffset,
                                    Core.Type.MyMapNPC[GameState.MyTarget].Y * 32 +
                                    Core.Type.MyMapNPC[GameState.MyTarget].YOffset);
                                break;

                            case (int)TargetType.Pet:
                                break;

                        }
                    }

                    for (i = 0; i < Constant.MAX_PLAYERS; i++)
                    {
                        if (IsPlaying(i))
                        {
                            if (Core.Type.Player[i].Map == Core.Type.Player[GameState.MyIndex].Map)
                            {
                                if (Core.Type.Player[i].Sprite == 0)
                                    continue;

                                if (GameState.CurX == Core.Type.Player[i].X & GameState.CurY == Core.Type.Player[i].Y)
                                {
                                    if (GameState.MyTargetType == (int)TargetType.Player & GameState.MyTarget == i)
                                    {
                                    }

                                    else
                                    {
                                        DrawHover(Core.Type.Player[i].X * 32 - 16,
                                            Core.Type.Player[i].Y * 32 + Core.Type.Player[i].YOffset);
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
                                if (Core.Type.MyMapResource[i].Y == y)
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
                    if (Animation.AnimInstance[i].Used[1])
                    {
                        Animation.DrawAnimation(i, 1);
                    }
                }
            }

            if (GameState.NumProjectiles > 0)
            {
                for (i = 0; i < Constant.MAX_PROJECTILES; i++)
                {
                    if (Core.Type.MapProjectile[Core.Type.Player[GameState.MyIndex].Map, i].ProjectileNum >= 0)
                    {
                        Projectile.DrawProjectile(i);
                    }
                }
            }

            if (GameState.CurrentEvents > 0 & GameState.CurrentEvents <= Core.Type.MyMap.EventCount)
            {
                var loopTo6 = GameState.CurrentEvents;
                for (i = 0; i < loopTo6; i++)
                {
                    if (Core.Type.MapEvents[i].Position == 2)
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
                if (GameState.CurrentEvents > 0 && Core.Type.MyMap.EventCount >= GameState.CurrentEvents)
                {
                    var loopTo9 = GameState.CurrentEvents;
                    for (i = 0; i < loopTo9; i++)
                    {
                        if (Core.Type.MapEvents[i].Visible == true)
                        {
                            if (Core.Type.MapEvents[i].ShowName == 1)
                            {
                                Text.DrawEventName(i);
                            }
                        }
                    }
                }
            }

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                if (Core.Type.MyMapNPC[i].Num >= 0)
                {
                    Text.DrawNPCName(i);
                }
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
                if (Core.Type.ChatBubble[i].Active)
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
                if (GameState.MapTab == (int)MapTab.Events)
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
            if (GameState.MapTab == (int)MapTab.Attributes)
            {
                Text.DrawMapAttributes();
            }
        }

        public static void UpdateDirBlock()
        {
            int x;
            int y;

            if (GameState.MapTab == (int)MapTab.Directions)
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