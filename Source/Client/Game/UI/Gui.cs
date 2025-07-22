using Client.Content.Skins;
using Client.Game.Objects;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using static Core.Global.Command;
using Color = Microsoft.Xna.Framework.Color;
using Path = Core.Path;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Client
{

    public class Gui
    {
        // GUI
        public static ConcurrentDictionary<long, Window> Windows = new ConcurrentDictionary<long, Window>();
        public static long ActiveWindow;

        // GUi parts
        public static Core.Type.ControlPart DragBox;

        // Used for automatically the zOrder
        public static long zOrder_Win;
        public static long zOrder_Con;

        // Declare a timer to control when dragging can begin
        private static Stopwatch dragTimer = new Stopwatch();
        private const double dragInterval = 100d; // Set the interval in milliseconds to start dragging
        private static bool canDrag = false;  // Flag to control when dragging is allowed
        private static bool isDragging = false;

        public class Window
        {
            public string Name { get; set; }
            public ControlType Type { get; set; }
            public long Left { get; set; }
            public long Top { get; set; }
            public long OrigLeft { get; set; }
            public long OrigTop { get; set; }
            public long MovedX { get; set; }
            public long MovedY { get; set; }
            public long Width { get; set; }
            public long Height { get; set; }
            public bool Visible { get; set; }
            public bool CanDrag { get; set; }
            public Core.Font Font { get; set; }
            public string Text { get; set; }
            public long xOffset { get; set; }
            public long yOffset { get; set; }
            public long Icon { get; set; }
            public bool Enabled { get; set; }
            public long Value { get; set; }
            public long Group { get; set; }
            public byte zChange { get; set; }
            public long zOrder { get; set; }
            public Action OnDraw { get; set; }
            public bool Censor { get; set; }
            public bool ClickThrough { get; set; }
            public long LinkedToWin { get; set; }
            public long LinkedToCon { get; set; }

            public ControlState State { get; set; }
            public List<string> List { get; set; }

            // Arrays for states
            public List<long> Design { get; set; }
            public List<long> Image { get; set; }
            public List<Action> CallBack { get; set; }

            // Controls in this window
            public List<Control> Controls { get; set; }
            public int LastControl { get; set; }
            public int ActiveControl { get; set; }
        }

        public class Control
        {
            public string Name { get; set; }
            public ControlType Type { get; set; }
            public long Left { get; set; }
            public long Top { get; set; }
            public long OrigLeft { get; set; }
            public long OrigTop { get; set; }
            public long MovedX { get; set; }
            public long MovedY { get; set; }
            public long Width { get; set; }
            public long Height { get; set; }
            public bool Visible { get; set; }
            public bool CanDrag { get; set; }
            public long Max { get; set; }
            public long Min { get; set; }
            public long Value { get; set; }
            public string Text { get; set; }
            public byte Length { get; set; }
            public Alignment Align { get; set; }
            public Core.Font Font { get; set; }
            public Color Color { get; set; }
            public long Alpha { get; set; }
            public bool ClickThrough { get; set; }
            public long xOffset { get; set; }
            public long yOffset { get; set; }
            public byte zChange { get; set; }
            public long zOrder { get; set; }
            public bool Enabled { get; set; }
            public Action OnDraw { get; set; }
            public string Tooltip { get; set; }
            public long Group { get; set; }
            public bool Censor { get; set; }
            public long Icon { get; set; }
            public ControlState State { get; set; }
            public List<string> List { get; set; }

            // Arrays for states
            public List<long> Design { get; set; }
            public List<long> Image { get; set; }
            public List<string> Texture { get; set; }
            public List<Action> CallBack { get; set; }
        }

        public static void UpdateControl(long winNum, long zOrder, string name, Color color, ControlType tType, List<long> design, List<long> image, List<string> texture, List<Action> callback, long left = 0L, long top = 0L, long width = 0L, long height = 0L, bool visible = true, bool canDrag = false, long Max = 0L, long Min = 0L, long value = 0L, string text = "", Alignment align = 0, Core.Font font = Core.Font.Georgia, long alpha = 255L, bool clickThrough = false, long xOffset = 0L, long yOffset = 0L, byte zChange = 0, bool censor = false, long icon = 0L, Action onDraw = null, bool isActive = true, string tooltip = "", long @group = 0L, byte length = Constant.NAME_LENGTH, bool enabled = true)
        {

            // Ensure the window exists in the Windows collection
            if (!Windows.ContainsKey(winNum))
                return;

            // Create a new instance of Control with specified properties
            var newControl = new Control()
            {
                Name = name,
                Type = tType,
                Left = left,
                Top = top,
                OrigLeft = left,
                OrigTop = top,
                Width = width,
                Height = height,
                Visible = visible,
                CanDrag = canDrag,
                Max = Max,
                Min = Min,
                Value = value,
                Text = text,
                Length = length,
                Align = align,
                Font = font,
                Color = color,
                Alpha = alpha,
                ClickThrough = clickThrough,
                xOffset = xOffset,
                yOffset = yOffset,
                zChange = zChange,
                zOrder = zOrder,
                Enabled = enabled,
                OnDraw = onDraw,
                Tooltip = tooltip,
                Group = @group,
                Censor = censor,
                Icon = icon,
                Design = design,
                Image = image,
                Texture = texture,
                CallBack = callback
            };

            // Add the new control to the specified window's controls list
            if (Windows[winNum].Controls is null)
            {
                Windows[winNum].Controls = new List<Control>();
                Windows[winNum].Controls.Add(new Control());
            }
            Windows[winNum].Controls.Add(newControl);

            // Update active control if necessary
            if (isActive)
                Windows[winNum].ActiveControl = Windows[winNum].Controls.Count - 1;

            // set the zOrder
            zOrder_Con = zOrder_Con + 1L;
        }

        public static void UpdateZOrder(long winNum, bool forced = false)
        {
            long i;
            long oldZOrder;

            {
                var withBlock = Windows[winNum];

                if (!forced)
                {
                    if (withBlock.zChange == 0)
                        return;
                }

                if (withBlock.zOrder == Gui.Windows.Count - 1)
                    return;

                oldZOrder = withBlock.zOrder;

                var loopTo = Gui.Windows.Count;
                for (i = 1L; i <= loopTo; i++)
                {

                    if (Windows[i].zOrder > oldZOrder)
                    {
                        Windows[i].zOrder = Windows[i].zOrder - 1L;
                    }
                }
                withBlock.zOrder = Gui.Windows.Count - 1;
            }
        }

        public static void SortWindows()
        {
            Window tempWindow;
            long i;
            long x;
            x = 0L;
            while (x != 0L)
            {
                x = 0L;
                var loopTo = Gui.Windows.Count - 1;
                for (i = 1L; i <= loopTo; i++)
                {
                    if (Windows[i].zOrder > Windows[i + 1L].zOrder)
                    {
                        tempWindow = Windows[i];
                        Windows[i] = Windows[i + 1L];
                        Windows[i + 1L] = tempWindow;
                        x = 0L;
                    }
                }
            }
        }

        public static void Combobox_AddItem(string winName, long controlIndex, string text)
        {
            // Ensure the List property is initialized as a List(Of String) in Control class
            if (Windows[Conversions.ToLong(winName)].Controls[(int)controlIndex].List is null)
            {
                Windows[Conversions.ToLong(winName)].Controls[(int)controlIndex].List = new List<string>();
            }
            Windows[Conversions.ToLong(winName)].Controls[(int)controlIndex].List.Add(text);
        }

        public static void UpdateWindow(string name, string caption, Core.Font font, long zOrder, long left, long top, long width, long height, long icon, bool visible = true, long xOffset = 0L, long yOffset = 0L, long design_norm = 0L, long design_hover = 0L, long design_mousedown = 0L, long image_norm = 0L, long image_hover = 0L, long image_mousedown = 0L, Action callback_norm = null, Action callback_hover = null, Action callback_mousemove = null, Action callback_mousedown = null, Action callback_dblclick = null, Action onDraw = null, bool canDrag = true, byte zChange = 1, bool isActive = true, bool clickThrough = false)
        {
            int stateCount = Enum.GetValues(typeof(ControlState)).Length;
            var design = new List<long>(Enumerable.Repeat(0L, stateCount));
            var image = new List<long>(Enumerable.Repeat(0L, stateCount));
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, stateCount));
            var callback = new List<Action>(Enumerable.Repeat((Action)null, stateCount));

            // Assign specific values for each state
            design[(int)ControlState.Normal] = design_norm;
            design[(int)ControlState.Hover] = design_hover;
            design[(int)ControlState.MouseDown] = design_mousedown;
            image[(int)ControlState.Normal] = image_norm;
            image[(int)ControlState.Hover] = image_hover;
            image[(int)ControlState.MouseDown] = image_mousedown;
            callback[(int)ControlState.Normal] = callback_norm;
            callback[(int)ControlState.Hover] = callback_hover;
            callback[(int)ControlState.MouseDown] = callback_mousedown;
            callback[(int)ControlState.MouseMove] = callback_mousemove;
            callback[(int)ControlState.DoubleClick] = callback_dblclick;

            // Create a new instance of Window and populate it
            var newWindow = new Window()
            {
                Name = name,
                Type = ControlType.Window,
                Left = left,
                Top = top,
                OrigLeft = left,
                OrigTop = top,
                Width = width,
                Height = height,
                Visible = visible,
                CanDrag = canDrag,
                Font = font,
                Text = caption,
                xOffset = xOffset,
                yOffset = yOffset,
                Icon = icon,
                Enabled = true,
                zChange = zChange,
                zOrder = zOrder,
                OnDraw = onDraw,
                ClickThrough = clickThrough,
                Design = design,
                Image = image,
                CallBack = callback
            };

            // Add the new control to the specified window's controls list
            Windows.TryAdd(Gui.Windows.Count + 1, newWindow);

            // Set the active window if visible
            if (visible)
                ActiveWindow = Gui.Windows.Count;
        }

        public static void UpdateTextbox(long winNum, string name, long left, long top, long width, long height, [Optional, DefaultParameterValue("")] string text, [Optional, DefaultParameterValue(Core.Font.Georgia)] Core.Font font, [Optional, DefaultParameterValue(Alignment.Left)] Alignment align, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(true)] bool isActive, [Optional, DefaultParameterValue(0L)] long xOffset, [Optional, DefaultParameterValue(0L)] long yOffset, [Optional, DefaultParameterValue(0L)] long image_norm, [Optional, DefaultParameterValue(0L)] long image_hover, [Optional, DefaultParameterValue(0L)] long image_mousedown, [Optional, DefaultParameterValue(0L)] long design_norm, [Optional, DefaultParameterValue(0L)] long design_hover, [Optional, DefaultParameterValue(0L)] long design_mousedown, [Optional, DefaultParameterValue(false)] bool censor, [Optional, DefaultParameterValue(0L)] long icon, [Optional, DefaultParameterValue(Constant.NAME_LENGTH)] byte length, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick, [Optional] ref Action callback_enter)
        {
            var stateCount = Enum.GetValues(typeof(ControlState)).Length;
            var design = new List<long>(Enumerable.Repeat(0L, stateCount).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, stateCount).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, stateCount).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, stateCount).ToList());

            // Assign specific values for each state
            design[(int)ControlState.Normal] = design_norm;
            design[(int)ControlState.Hover] = design_hover;
            design[(int)ControlState.MouseDown] = design_mousedown;
            image[(int)ControlState.Normal] = image_norm;
            image[(int)ControlState.Hover] = image_hover;
            image[(int)ControlState.MouseDown] = image_mousedown;
            callback[(int)ControlState.Normal] = callback_norm;
            callback[(int)ControlState.Hover] = callback_hover;
            callback[(int)ControlState.MouseDown] = callback_mousedown;
            callback[(int)ControlState.MouseMove] = callback_mousemove;
            callback[(int)ControlState.DoubleClick] = callback_dblclick;
            callback[(int)ControlState.FocusEnter] = callback_enter;

            // Control the textbox
            UpdateControl(winNum, zOrder_Con, name, Color.White, ControlType.TextBox, design, image, texture, callback, left, top, width, height, visible, text: text, align: align, font: font, alpha: alpha, xOffset: xOffset, yOffset: yOffset, censor: censor, icon: icon, isActive: isActive, length: length);
        }


        public static void UpdatePictureBox(long winNum, string name, long left, long top, long width, long height, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(false)] bool canDrag, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(true)] bool clickThrough, [Optional, DefaultParameterValue(0L)] long image_norm, [Optional, DefaultParameterValue(0L)] long image_hover, [Optional, DefaultParameterValue(0L)] long image_mousedown, [Optional, DefaultParameterValue(0L)] long design_norm, [Optional, DefaultParameterValue(0L)] long design_hover, [Optional, DefaultParameterValue(0L)] long design_mousedown, [Optional, DefaultParameterValue("")] string texturePath, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick, [Optional] ref Action onDraw)
        {
            const int stateCount = 6; // ControlState enum has 6 states used here: Normal, Hover, MouseDown, MouseMove, MouseUp, DoubleClick

            var design = new List<long>(Enumerable.Repeat(0L, stateCount));
            var image = new List<long>(Enumerable.Repeat(0L, stateCount));
            var texture = new List<string>(Enumerable.Repeat(string.Empty, stateCount));
            var callback = new List<Action>(Enumerable.Repeat((Action)null, stateCount));

            if (string.IsNullOrEmpty(texturePath))
            {
                texturePath = Path.Gui;
            }

            // fill temp arrays
            design[(int)ControlState.Normal] = design_norm;
            design[(int)ControlState.Hover] = design_hover;
            design[(int)ControlState.MouseDown] = design_mousedown;
            image[(int)ControlState.Normal] = image_norm;
            image[(int)ControlState.Hover] = image_hover;
            image[(int)ControlState.MouseDown] = image_mousedown;
            texture[(int)ControlState.Normal] = texturePath;
            texture[(int)ControlState.Hover] = texturePath;
            texture[(int)ControlState.MouseDown] = texturePath;

            callback[(int)ControlState.Normal] = callback_norm;
            callback[(int)ControlState.Hover] = callback_hover;
            callback[(int)ControlState.MouseDown] = callback_mousedown;
            callback[(int)ControlState.MouseMove] = callback_mousemove;
            callback[(int)ControlState.DoubleClick] = callback_dblclick;

            // Control the box
            UpdateControl(winNum, zOrder_Con, name, Color.White, ControlType.PictureBox, design, image, texture, callback, left, top, width, height, visible, canDrag, alpha: alpha, clickThrough: clickThrough, xOffset: 0L, yOffset: 0L, onDraw: onDraw);
        }

        public static void UpdateButton(long winNum, string name, long left, long top, long width, long height, [Optional, DefaultParameterValue("")] string text, [Optional, DefaultParameterValue(Core.Font.Georgia)] Core.Font font, [Optional, DefaultParameterValue(0L)] long icon, [Optional, DefaultParameterValue(0L)] long image_norm, [Optional, DefaultParameterValue(0L)] long image_hover, [Optional, DefaultParameterValue(0L)] long image_mousedown, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(0L)] long design_norm, [Optional, DefaultParameterValue(0L)] long design_hover, [Optional, DefaultParameterValue(0L)] long design_mousedown, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick, long xOffset = 0L, long yOffset = 0L, string tooltip = "", bool censor = false)
        {
            int stateCount = Enum.GetValues(typeof(ControlState)).Length;

            var design = new List<long>(Enumerable.Repeat(0L, stateCount).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, stateCount).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, stateCount).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, stateCount).ToList());

            // fill temp arrays
            design[(int)ControlState.Normal] = design_norm;
            design[(int)ControlState.Hover] = design_hover;
            design[(int)ControlState.MouseDown] = design_mousedown;
            image[(int)ControlState.Normal] = image_norm;
            image[(int)ControlState.Hover] = image_hover;
            image[(int)ControlState.MouseDown] = image_mousedown;
            texture[(int)ControlState.Normal] = Path.Gui;
            texture[(int)ControlState.Hover] = Path.Gui;
            texture[(int)ControlState.MouseDown] = Path.Gui;
            callback[(int)ControlState.Normal] = callback_norm;
            callback[(int)ControlState.Hover] = callback_hover;
            callback[(int)ControlState.MouseDown] = callback_mousedown;
            callback[(int)ControlState.MouseMove] = callback_mousemove;
            callback[(int)ControlState.DoubleClick] = callback_dblclick;

            // Control the button 
            UpdateControl(winNum, zOrder_Con, name, Color.White, ControlType.Button, design, image, texture, callback, left, top, width, height, visible, text: text, font: font, clickThrough: Conversions.ToBoolean(alpha), xOffset: xOffset, yOffset: yOffset, censor: censor, icon: icon, tooltip: tooltip);
        }

        public static void UpdateLabel(long winNum, string name, long left, long top, long width, long height, string text, Core.Font font, Color color, [Optional, DefaultParameterValue(Alignment.Left)] Alignment align, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(false)] bool clickThrough, [Optional, DefaultParameterValue(false)] bool censor, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick, [Optional] ref bool enabled)
        {
            // Get the number of states in ControlState enum
            int controlStateCount = Enum.GetValues(typeof(ControlState)).Length;

            var designLabel = new List<long>(Enumerable.Repeat(0L, controlStateCount).ToList());
            var imageLabel = new List<long>(Enumerable.Repeat(0L, controlStateCount).ToList());
            var textureLabel = new List<string>(Enumerable.Repeat(Path.Designs, controlStateCount).ToList());
            var callbackLabel = new List<Action>(Enumerable.Repeat((Action)null, controlStateCount).ToList());

            // fill temp arrays
            callbackLabel[(int)ControlState.Normal] = callback_norm;
            callbackLabel[(int)ControlState.Hover] = callback_hover;
            callbackLabel[(int)ControlState.MouseDown] = callback_mousedown;
            callbackLabel[(int)ControlState.MouseMove] = callback_mousemove;
            callbackLabel[(int)ControlState.DoubleClick] = callback_dblclick;

            // Control the label
            UpdateControl(winNum, zOrder_Con, name, Color.White, ControlType.Label, designLabel, imageLabel, textureLabel, callbackLabel, left, top, width, height, visible, text: text, align: align, font: font, clickThrough: Conversions.ToBoolean(alpha), xOffset: Conversions.ToLong(clickThrough), censor: censor, enabled: enabled);
        }

        public static void UpdateCheckBox(long winNum, string name, long left, long top, long width, [Optional, DefaultParameterValue(15L)] long height, [Optional, DefaultParameterValue(0L)] long value, [Optional, DefaultParameterValue("")] string text, [Optional, DefaultParameterValue(Core.Font.Georgia)] Core.Font font, [Optional, DefaultParameterValue(Alignment.Left)] Alignment align, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(0L)] long theDesign, [Optional, DefaultParameterValue(0L)] long @group, [Optional, DefaultParameterValue(false)] bool censor, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick)
        {
            int stateCount = Enum.GetValues(typeof(ControlState)).Length;
            var design = new List<long>(Enumerable.Repeat(0L, stateCount).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, stateCount).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, stateCount).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, stateCount).ToList());

            design[0] = theDesign;
            texture[0] = Path.Gui;

            // fill temp arrays
            callback[(int)ControlState.Normal] = callback_norm;
            callback[(int)ControlState.Hover] = callback_hover;
            callback[(int)ControlState.MouseDown] = callback_mousedown;
            callback[(int)ControlState.MouseMove] = callback_mousemove;
            callback[(int)ControlState.DoubleClick] = callback_dblclick;

            // Control the box
            UpdateControl(winNum, zOrder_Con, name, Color.White, ControlType.Checkbox, design, image, texture, callback, left, top, width, height, visible, value: value, text: text, align: align, font: font, clickThrough: Conversions.ToBoolean(alpha), censor: censor, @group: group);
        }

        public static void UpdateComboBox(long winNum, string name, long left, long top, long width, long height, long design)
        {
            // Get the number of ControlState enum values
            int controlStateCount = Enum.GetValues(typeof(ControlState)).Length;

            // Initialize lists for the control states
            var theDesign = new List<long>(Enumerable.Repeat(0L, controlStateCount).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, controlStateCount).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Gui, controlStateCount).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, controlStateCount).ToList());

            // Set the design for the normal state
            theDesign[0] = design;
            texture[0] = Path.Gui;

            // Update the control in the window using the updated lists
            UpdateControl(winNum, zOrder_Con, name, Color.White, ControlType.ComboMenu, theDesign, image, texture, callback, left, top, width, height);
        }

        public static int GetWindowIndex(string winName)
        {
            int GetWindowIndexRet = default;

            var loopTo = Gui.Windows.Count - 1;
            for (int i = 0; i <= loopTo; i++)
            {
                if ((Strings.LCase(Windows[i + 1].Name) ?? "") == (Strings.LCase(winName) ?? ""))
                {
                    GetWindowIndexRet = i + 1;
                    return GetWindowIndexRet;
                }

            }

            return 0;

        }

        public static int GetControlIndex(string winName, string controlName)
        {
            int GetControlIndexRet = default;
            int winIndex;

            winIndex = GetWindowIndex(winName);

            var loopTo = (Windows[winIndex].Controls.Count - 1);
            for (int i = 0; i <= loopTo; i++)
            {

                if ((Strings.LCase(Windows[winIndex].Controls[(int)i].Name) ?? "") == (Strings.LCase(controlName) ?? ""))
                {
                    GetControlIndexRet = i;
                    return GetControlIndexRet;
                }

            }

            return 0;

        }

        public static bool SetActiveControl(long curWindow, long curControl)
        {
            bool SetActiveControlRet = default;
            // make sure it's something which CAN be active
            switch (Windows[curWindow].Controls[(int)curControl].Type)
            {
                case ControlType.TextBox:
                    {
                        Windows[curWindow].LastControl = Windows[curWindow].ActiveControl;
                        Windows[curWindow].ActiveControl = (int)curControl;
                        SetActiveControlRet = true;
                        break;
                    }
            }

            return SetActiveControlRet;
        }

        public static int ActivateControl(int startIndex = 0, bool skipLast = true)
        {
            int currentActive = Windows[ActiveWindow].ActiveControl;
            int lastControl = Windows[ActiveWindow].LastControl;

            // Ensure the starting index is correct
            if (startIndex <= currentActive)
            {
                startIndex = currentActive + 1;
            }

            // Attempt to activate the next available control, starting from the given index
            for (int i = startIndex, loopTo = Windows[ActiveWindow].Controls.Count - 1; i <= loopTo; i++)
            {
                if (i != currentActive && (!skipLast || i != lastControl))
                {
                    if (SetActiveControl(ActiveWindow, i))
                    {
                        return i;  // Return the index of the control that was activated
                    }
                }
            }

            // If we reached the end, wrap around and start from the beginning
            for (int i = 0, loopTo1 = startIndex - 1; i <= loopTo1; i++)
            {
                if (i != currentActive && (!skipLast || i != lastControl))
                {
                    if (SetActiveControl(ActiveWindow, i))
                    {
                        return i;  // Return the index of the control that was activated
                    }
                }
            }

            // No control was activated, return 0 or handle as needed
            if (skipLast)
            {
                return ActivateControl(0, false);  // Retry without skipping the last control
            }
            else
            {
                return 0;
            }  // Indicate no control was activated
        }

        public static void CentralizeWindow(long curWindow)
        {
            {
                var withBlock = Windows[curWindow];
                withBlock.Left = (long)Math.Round(GameState.ResolutionWidth / 2d - withBlock.Width / 2d);
                withBlock.Top = (long)Math.Round(GameState.ResolutionHeight / 2d - withBlock.Height / 2d);
                withBlock.OrigLeft = withBlock.Left;
                withBlock.OrigTop = withBlock.Top;
            }
        }

        public static void HideWindows()
        {
            long i;

            var loopTo = Gui.Windows.Count - 1;
            for (i = 1L; i <= loopTo; i++)
            {
                HideWindow(i);
            }
        }

        public static void ShowWindow(long curWindow, bool forced = false, bool resetPosition = true)
        {
            if (curWindow == 0L)
                return;

            Windows[curWindow].Visible = true;

            if (forced == true)
            {
                UpdateZOrder(curWindow, forced);
                ActiveWindow = curWindow;
            }
            else if (Conversions.ToBoolean(Windows[curWindow].zChange))
            {
                UpdateZOrder(curWindow);
                ActiveWindow = curWindow;
            }

            if (resetPosition)
            {
                {
                    var withBlock = Windows[curWindow];
                    withBlock.Left = withBlock.OrigLeft;
                    withBlock.Top = withBlock.OrigTop;
                }
            }
        }

        public static void HideWindow(long curWindow)
        {
            long i;

            Windows[curWindow].Visible = false;

            // find next window to set as active
            for (i = Gui.Windows.Count - 1; i >= 1L; i += -1)
            {
                if (Windows[i].Visible == true & Windows[i].zChange == 1)
                {
                    ActiveWindow = i;
                    break;
                }
            }
        }

        // Rendering & Initialisation
        public static void Init()
        {
            // Erase windows
            Gui.Windows = new ConcurrentDictionary<long, Window>();

            // Starter values
            zOrder_Win = 0L;
            zOrder_Con = 0L;

            // Menu (dynamic UI initialization via Script.Instance)
            dynamic ui = UI.Instance;
            ui?.UpdateWindow_Menu();
            ui?.UpdateWindow_Register();
            ui?.UpdateWindow_Login();
            ui?.UpdateWindow_NewChar();
            ui?.UpdateWindow_Jobs();
            ui?.UpdateWindow_Chars();
            ui?.UpdateWindow_ChatSmall();
            ui?.UpdateWindow_Chat();
            ui?.UpdateWindow_Menu();
            ui?.UpdateWindow_Description();
            ui?.UpdateWindow_Inventory();
            ui?.UpdateWindow_Skills();
            ui?.UpdateWindow_Character();
            ui?.UpdateWindow_Hotbar();
            ui?.UpdateWindow_Bank();
            ui?.UpdateWindow_Shop();
            ui?.UpdateWindow_EscMenu();
            ui?.UpdateWindow_Bars();
            ui?.UpdateWindow_Dialogue();
            ui?.UpdateWindow_DragBox();
            ui?.UpdateWindow_Options();
            ui?.UpdateWindow_Trade();
            ui?.UpdateWindow_Party();
            ui?.UpdateWindow_PlayerMenu();
            ui?.UpdateWindow_RightClick();
            ui?.UpdateWindow_Combobox();
        }

        public static bool HandleInterfaceEvents(ControlState entState)
        {
            int i;
            var curWindow = default(long);
            var curControl = default(long);
            Action callBack;

            // Check for MouseDown to start the drag timer
            if (GameClient.IsMouseButtonDown(MouseButton.Left) && GameClient.PreviousMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                dragTimer.Restart(); // Start the timer on initial mouse down
                canDrag = false; // Reset drag flag to ensure it doesn't drag immediately
            }

            // Check for MouseUp to reset dragging
            if (GameClient.IsMouseButtonUp(MouseButton.Left))
            {
                isDragging = false;
                dragTimer.Reset(); // Stop the timer on mouse up
            }

            // Enable dragging if the mouse has been held down for the specified interval
            if (dragTimer.ElapsedMilliseconds >= dragInterval)
            {
                canDrag = true;
            }
            else
            {
                canDrag = false;
            }

            lock (GameClient.InputLock)
            {
                // Find the container
                var loopTo = Gui.Windows.Count;
                for (i = 1; i < loopTo; i++)
                {
                    var withBlock = Windows[i];
                    if (withBlock.Enabled && withBlock.Visible)
                    {
                        if (withBlock.State != ControlState.MouseDown)
                            withBlock.State = ControlState.Normal;

                        if (GameState.CurMouseX >= withBlock.Left && GameState.CurMouseX <= withBlock.Width + withBlock.Left && GameState.CurMouseY >= withBlock.Top && GameState.CurMouseY <= withBlock.Height + withBlock.Top)
                        {
                            // Handle combo menu logic
                            if (withBlock.Design[0] == (long)UiDesign.ComboMenuNormal)
                            {
                                if (entState == ControlState.MouseMove || entState == ControlState.Hover)
                                {
                                    ComboMenu_MouseMove(i);
                                }
                                else if (entState == ControlState.MouseDown)
                                {
                                    ComboMenu_MouseDown(i);
                                }
                            }

                            // Track the top-most window
                            if (curWindow == 0L || withBlock.zOrder > Windows[curWindow].zOrder)
                            {
                                curWindow = i;
                                isDragging = true;
                            }

                            if (ActiveWindow > 0)
                            {
                                if (!Windows[ActiveWindow].Visible || !Windows[ActiveWindow].Enabled || !Windows[ActiveWindow].CanDrag)
                                    ActiveWindow = curWindow;

                            }
                            else
                            {
                                ActiveWindow = curWindow;
                            }
                        }

                        // Handle window dragging only if dragging is enabled
                        if (entState == ControlState.MouseMove && GameClient.IsMouseButtonDown(MouseButton.Left))
                        {
                            if (ActiveWindow > 0 && isDragging)
                            {
                                withBlock = Windows[ActiveWindow];
                                if (canDrag && withBlock.CanDrag && withBlock.Enabled && withBlock.Visible)
                                {
                                    withBlock.Left = GameLogic.Clamp((int)(withBlock.Left + (GameState.CurMouseX - withBlock.Left - withBlock.MovedX)), 0, (int)(GameState.ResolutionWidth - withBlock.Width));
                                    withBlock.Top = GameLogic.Clamp((int)(withBlock.Top + (GameState.CurMouseY - withBlock.Top - withBlock.MovedY)), 0, (int)(GameState.ResolutionHeight - withBlock.Height));
                                    break;
                                }
                            }
                        }
                    }
                }

                if (curWindow > 0L)
                {
                    // Handle the active window's callback
                    callBack = Windows[curWindow].CallBack[(int)entState];

                    // Execute the callback if it exists
                    callBack?.Invoke();

                    if (Windows[curWindow].Controls is not null)
                    {
                        // Handle controls in the active window
                        var loopTo1 = (long)((Windows[curWindow].Controls?.Count) - 1);
                        for (i = 0; i <= loopTo1; i++)
                        {
                            var withBlock1 = Windows[curWindow].Controls[(int)i];

                            if (withBlock1.Enabled && withBlock1.Visible)
                            {
                                if (GameState.CurMouseX >= withBlock1.Left + Windows[curWindow].Left && GameState.CurMouseX <= withBlock1.Left + withBlock1.Width + Windows[curWindow].Left && GameState.CurMouseY >= withBlock1.Top + Windows[curWindow].Top && GameState.CurMouseY <= withBlock1.Top + withBlock1.Height + Windows[curWindow].Top)
                                {
                                    if (curControl == 0L || withBlock1.zOrder > Windows[curWindow].Controls[(int)curControl].zOrder)
                                    {
                                        curControl = i;
                                    }
                                }

                                if (isDragging)
                                {
                                    // Handle control dragging only if dragging is enabled
                                    if (entState == ControlState.MouseMove && withBlock1.CanDrag && canDrag && GameClient.IsMouseButtonDown(MouseButton.Left))
                                    {
                                        withBlock1.Left = GameLogic.Clamp((int)(withBlock1.Left + (GameState.CurMouseX - withBlock1.Left - withBlock1.MovedX)), 0, (int)(Windows[curWindow].Width - withBlock1.Width));
                                        withBlock1.Top = GameLogic.Clamp((int)(withBlock1.Top + (GameState.CurMouseY - withBlock1.Top - withBlock1.MovedY)), 0, (int)(Windows[curWindow].Height - withBlock1.Height));
                                    }
                                }
                            }
                        }
                    }

                    if (curControl > 0L)
                    {
                        // Reset all control states
                        for (int j = 0; j < Windows[curWindow].Controls.Count; j++)
                        {
                            if (curControl != j)
                                Windows[curWindow].Controls[j].State = ControlState.Normal;
                        }

                        var withBlock2 = Windows[curWindow].Controls[(int)curControl];

                        // Handle hover state separately
                        if (entState == ControlState.MouseMove)
                        {
                            withBlock2.State = ControlState.Hover;
                        }
                        else if (entState == ControlState.MouseDown)
                        {
                            withBlock2.State = ControlState.MouseDown;
                        }

                        if (GameClient.IsMouseButtonDown(MouseButton.Left) && withBlock2.CanDrag)
                        {
                            withBlock2.MovedX = GameState.CurMouseX - withBlock2.Left;
                            withBlock2.MovedY = GameState.CurMouseY - withBlock2.Top;
                        }

                        // Handle specific control types
                        switch (withBlock2.Type)
                        {
                            case ControlType.Checkbox:
                                {
                                    if (withBlock2.Group > 0L && withBlock2.Value == 0L)
                                    {
                                        for (i = 0; i < Windows[curWindow].Controls.Count; i++)
                                        {
                                            if (Windows[curWindow].Controls[i].Type == ControlType.Checkbox &&
                                                Windows[curWindow].Controls[i].Group == withBlock2.Group)
                                            {
                                                Windows[curWindow].Controls[i].Value = 0L;
                                            }
                                        }
                                        withBlock2.Value = 0L;
                                    }

                                    break;
                                }

                            case ControlType.ComboMenu:
                                {
                                    ShowComboMenu(curWindow, curControl);
                                    break;
                                }
                        }

                        if (GameClient.IsMouseButtonDown(MouseButton.Left))
                        {
                            SetActiveControl(curWindow, curControl);
                        }

                        callBack = withBlock2.CallBack[(int)entState];

                        // Execute the callback if it exists
                        callBack?.Invoke();
                    }
                }

                if (curWindow == 0)
                {
                    ResetInterface();
                }

                // Reset mouse state on MouseUp
                if (entState == ControlState.MouseUp)
                    ResetMouseDown();
            }

            return true;
        }

        public static void ResetInterface()
        {
            long i;
            long x;

            var loopTo = Gui.Windows.Count;
            for (i = 1L; i < loopTo; i++)
            {
                if (Windows[i].State != ControlState.MouseDown)
                    Windows[i].State = ControlState.Normal;
                
                if (Windows[i].Controls is null || Windows[i].Controls.Count == 0)
                    continue;

                var loopTo1 = (long)(Windows[i].Controls.Count);
                for (x = 0L; x < loopTo1; x++)
                {
                    if (Windows[i].Controls[(int)x].State != ControlState.MouseDown)
                        Windows[i].Controls[(int)x].State = ControlState.Normal;
                }
            }

        }

        public static void ResetMouseDown()
        {
            Action callBack;
            long i;
            long x;

            lock (GameClient.InputLock)
            {
                var loopTo = Gui.Windows.Count;
                for (i = 1L; i < loopTo; i++)
                {                 
                    var withBlock = Windows[i];
                    // Only reset the state if it was in MouseDown
                    if (withBlock.State == ControlState.MouseDown)
                    {
                        withBlock.State = ControlState.Normal;
                        callBack = withBlock.CallBack[(int)ControlState.Normal];
                        if (callBack is not null)
                            callBack?.Invoke();
                    }

                    // Check if Controls is not Nothing and has at least one element
                    if (withBlock.Controls is not null && withBlock.Controls.Count > 0)
                    {
                        var loopTo1 = (long)(withBlock.Controls.Count - 1);
                        for (x = 0L; x <= loopTo1; x++)
                        {
                            var control = withBlock.Controls[(int)x];

                            // Only reset the state if it was in MouseDown
                            if (control.State == ControlState.MouseDown)
                            {
                                control.State = ControlState.Normal;

                                callBack = control.CallBack[(int)control.State];
                                if (callBack is not null)
                                    callBack?.Invoke();
                            }
                        }
                    }
                    
                }
            }
        }

        public static void Render()
        {
            // Exit if no windows are present
            if (Gui.Windows.Count == 0)
                return;

            // Reset Z-order
            long curZOrder = 0L;

            // Loop through each window based on Z-order
            var loopTo = Gui.Windows.Count - 1;
            for (curZOrder = 0L; curZOrder <= loopTo; curZOrder++)
            {
                for (int i = 1, loopTo1 = Gui.Windows.Count; i <= loopTo1; i++)
                {
                    if (curZOrder == Windows[i].zOrder && Windows[i].Visible)
                    {
                        // Render the window
                        RenderWindow(i);

                        // Render visible controls within the window
                        for (int? x = 0, loopTo2 = (Windows[i].Controls?.Count) - 1; x <= loopTo2; x++)
                        {
                            if (Windows[i].Controls[(int)x].Visible)
                            {
                                RenderControl(i, (long)x);
                            }
                        }
                    }
                }
            }
        }

        public static void RenderControl(long winNum, long entNum)
        {
            long xO;
            long yO;
            double hor_centre;
            double ver_centre;
            double height;
            double width;
            var textArray = default(string[]);
            long count;
            long i;
            var taddText = default(string);
            var yOffset = default(long);
            long sprite;
            var left = default(long);

            // Check if the window and Control exist
            if ((winNum < 0L | winNum >= Gui.Windows.Count || entNum < 0L) | entNum > Windows[winNum].Controls.Count - 1)
            {
                return;
            }

            // Get the window's position offsets
            xO = Windows[winNum].Left;
            yO = Windows[winNum].Top;

            {
                var withBlock = Windows[winNum].Controls[(int)entNum];
                switch (withBlock.Type)
                {
                    case ControlType.PictureBox:
                        {
                            if (withBlock.Design[(int)withBlock.State] > 0L)
                            {
                                RenderDesign(withBlock.Design[(int)withBlock.State], withBlock.Left + xO, withBlock.Top + yO, withBlock.Width, withBlock.Height, withBlock.Alpha);
                            }

                            if (withBlock.Image[(int)withBlock.State] > 0L)
                            {
                                string argpath = System.IO.Path.Combine(withBlock.Texture[(int)withBlock.State], withBlock.Image[(int)withBlock.State].ToString());
                                GameClient.RenderTexture(ref argpath, (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), 0, 0, (int)withBlock.Width, (int)withBlock.Height, (int)withBlock.Width, (int)withBlock.Height, (byte)withBlock.Alpha);
                            }

                            break;
                        }

                    case ControlType.TextBox:
                        {
                            // Render the design if available
                            if (withBlock.Design[(int)withBlock.State] > 0L)
                            {
                                RenderDesign(withBlock.Design[(int)withBlock.State], withBlock.Left + xO, withBlock.Top + yO, withBlock.Width, withBlock.Height, withBlock.Alpha);
                            }

                            // Render the image if present
                            if (withBlock.Image[(int)withBlock.State] > 0L)
                            {
                                string argpath1 = System.IO.Path.Combine(withBlock.Texture[(int)withBlock.State], withBlock.Image[(int)withBlock.State].ToString());
                                GameClient.RenderTexture(ref argpath1, (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), 0, 0, (int)withBlock.Width, (int)withBlock.Height, (int)withBlock.Width, (int)withBlock.Height, (byte)withBlock.Alpha);
                            }

                            // Handle active window text input
                            if (ActiveWindow == winNum & Windows[winNum].ActiveControl == entNum)
                            {
                                taddText = GameState.ChatShowLine;
                            }

                            // Final text with potential censoring and additional input
                            string finalText = (withBlock.Censor ? Text.CensorText(withBlock.Text) : withBlock.Text) + taddText;

                            // Remove vbNullChar from the finalText
                            finalText = finalText.Replace("\0", string.Empty);

                            // Measure the text size
                            var actualSize = Text.Fonts[withBlock.Font].MeasureString(FilterUnsupportedCharacters(finalText, withBlock.Font));
                            float actualWidth = actualSize.X;
                            float actualHeight = actualSize.Y;

                            // Apply padding and calculate position
                            left = withBlock.Left + xO + withBlock.xOffset;
                            double top = withBlock.Top + yO + withBlock.yOffset + (double)(withBlock.Height - actualHeight) / 2.0d;

                            // Render the final text
                            Text.RenderText(finalText, (int)left, (int)Math.Round(top), withBlock.Color, Color.Black, withBlock.Font);
                            break;
                        }

                    case ControlType.Button:
                        {
                            // Render the button design if defined
                            if (withBlock.Design[(int)withBlock.State] > 0L)
                            {
                                RenderDesign(withBlock.Design[(int)withBlock.State], withBlock.Left + xO, withBlock.Top + yO, withBlock.Width, withBlock.Height);
                            }

                            // Enqueue the button image if present
                            if (withBlock.Image[(int)withBlock.State] > 0L)
                            {
                                string argpath2 = System.IO.Path.Combine(withBlock.Texture[(int)withBlock.State], withBlock.Image[(int)withBlock.State].ToString());
                                GameClient.RenderTexture(ref argpath2, (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), 0, 0, (int)withBlock.Width, (int)withBlock.Height, (int)withBlock.Width, (int)withBlock.Height);
                            }

                            // Render the icon if available
                            if (withBlock.Icon > 0L)
                            {
                                var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Items, withBlock.Icon.ToString()));
                                if (gfxInfo == null)
                                    break;
                                int iconWidth = gfxInfo.Width;
                                int iconHeight = gfxInfo.Height;

                                string argpath3 = System.IO.Path.Combine(Path.Items, withBlock.Icon.ToString());
                                GameClient.RenderTexture(ref argpath3, (int)(withBlock.Left + xO + withBlock.xOffset), (int)(withBlock.Top + yO + withBlock.yOffset), 0, 0, iconWidth, iconHeight, iconWidth, iconHeight);
                            }

                            // Measure button text size and apply padding
                            var textSize = Text.Fonts[withBlock.Font].MeasureString(FilterUnsupportedCharacters(withBlock.Text, withBlock.Font));
                            float actualWidth = textSize.X;
                            float actualHeight = textSize.Y;

                            // Calculate horizontal and vertical centers with padding
                            double padding = (double)actualWidth / 6.0d;
                            double horCentre = withBlock.Left + xO + withBlock.xOffset + (double)(withBlock.Width - actualWidth) / 2.0d + padding - 4d;
                            padding = (double)actualHeight / 6.0d;
                            double verCentre = withBlock.Top + yO + withBlock.yOffset + (double)(withBlock.Height - actualHeight) / 2.0d + padding;

                            // Render the button's text
                            Text.RenderText(withBlock.Text, (int)Math.Round(horCentre), (int)Math.Round(verCentre), withBlock.Color, Color.Black, withBlock.Font);
                            break;
                        }

                    case ControlType.Label:
                        {
                            if (Strings.Len(withBlock.Text) > 0 & withBlock.Font > 0)
                            {
                                switch (withBlock.Align)
                                {
                                    case Alignment.Left:
                                        {
                                            if (Text.GetTextWidth(withBlock.Text, withBlock.Font) > withBlock.Width)
                                            {
                                                Text.WordWrap(withBlock.Text, withBlock.Font, withBlock.Width, ref textArray);
                                                count = Information.UBound(textArray);
                                                var loopTo = count;
                                                for (i = 0L; i < loopTo; i++)
                                                {
                                                    var actualSize = Text.Fonts[withBlock.Font].MeasureString(FilterUnsupportedCharacters(textArray[(int)i], withBlock.Font));
                                                    float actualWidth = actualSize.X;
                                                    double padding = (double)actualWidth / 6.0d;
                                                    left = (long)Math.Round(withBlock.Left + xO + withBlock.xOffset + padding);

                                                    Text.RenderText(textArray[(int)i], (int)left, (int)(withBlock.Top + yO + withBlock.yOffset + yOffset), withBlock.Color, Color.Black, withBlock.Font);
                                                    yOffset += 14L;
                                                }
                                            }
                                            else
                                            {
                                                var actualSize = Text.Fonts[withBlock.Font].MeasureString(withBlock.Text);
                                                float actualWidth = actualSize.X;
                                                left = withBlock.Left + xO + withBlock.xOffset;

                                                Text.RenderText(withBlock.Text, (int)left, (int)(withBlock.Top + yO + withBlock.yOffset), withBlock.Color, Color.Black, withBlock.Font);
                                            }

                                            break;
                                        }

                                    case Alignment.Right:
                                        {
                                            if (Text.GetTextWidth(withBlock.Text, withBlock.Font) > withBlock.Width)
                                            {
                                                Text.WordWrap(withBlock.Text, withBlock.Font, withBlock.Width, ref textArray);
                                                count = Information.UBound(textArray);
                                                var loopTo1 = count;
                                                for (i = 0L; i < loopTo1; i++)
                                                {
                                                    var actualSize = Text.Fonts[withBlock.Font].MeasureString(textArray[(int)i]);
                                                    float actualWidth = actualSize.X;
                                                    double padding = (double)actualWidth / 6.0d;
                                                    left = (long)Math.Round((double)(withBlock.Left + withBlock.Width - actualWidth + xO + withBlock.xOffset) + padding);

                                                    Text.RenderText(textArray[(int)i], (int)left, (int)(withBlock.Top + yO + withBlock.yOffset + yOffset), withBlock.Color, Color.Black, withBlock.Font);
                                                    yOffset += 14L;
                                                }
                                            }
                                            else
                                            {
                                                var actualSize = Text.Fonts[withBlock.Font].MeasureString(FilterUnsupportedCharacters(withBlock.Text, withBlock.Font));
                                                float actualWidth = actualSize.X;
                                                left = (long)Math.Round(withBlock.Left + withBlock.Width - actualSize.X + xO + withBlock.xOffset);

                                                Text.RenderText(withBlock.Text, (int)left, (int)(withBlock.Top + yO + withBlock.yOffset), withBlock.Color, Color.Black, withBlock.Font);
                                            }

                                            break;
                                        }

                                    case Alignment.Center:
                                        {
                                            if (Text.GetTextWidth(withBlock.Text, withBlock.Font) > withBlock.Width)
                                            {
                                                Text.WordWrap(withBlock.Text, withBlock.Font, withBlock.Width, ref textArray);
                                                count = Information.UBound(textArray);

                                                var loopTo2 = count;
                                                for (i = 0L; i < loopTo2; i++)
                                                {
                                                    var actualSize = Text.Fonts[withBlock.Font].MeasureString(FilterUnsupportedCharacters(textArray[(int)i], withBlock.Font));
                                                    float actualWidth = actualSize.X;
                                                    float actualHeight = actualSize.Y;
                                                    double padding = (double)actualWidth / 8.0d;
                                                    left = (long)Math.Round(withBlock.Left + (double)(withBlock.Width - actualWidth) / 2.0d + xO + withBlock.xOffset + padding - 4d);
                                                    double top = withBlock.Top + yO + withBlock.yOffset + yOffset + (double)(withBlock.Height - actualHeight) / 2.0d;

                                                    Text.RenderText(textArray[(int)i], (int)left, (int)Math.Round(top), withBlock.Color, Color.Black, withBlock.Font);
                                                    yOffset += 14L;
                                                }
                                            }
                                            else
                                            {
                                                var actualSize = Text.Fonts[withBlock.Font].MeasureString(FilterUnsupportedCharacters(withBlock.Text, withBlock.Font));
                                                float actualWidth = actualSize.X;
                                                float actualHeight = actualSize.Y;
                                                double padding = (double)actualWidth / 8.0d;
                                                left = (long)Math.Round(withBlock.Left + (double)(withBlock.Width - actualWidth) / 2.0d + xO + withBlock.xOffset + padding - 4d);
                                                double top = withBlock.Top + yO + withBlock.yOffset + (double)(withBlock.Height - actualHeight) / 2.0d;

                                                Text.RenderText(withBlock.Text, (int)left, (int)Math.Round(top), withBlock.Color, Color.Black, withBlock.Font);
                                            }

                                            break;
                                        }
                                }
                            }

                            break;
                        }
                    // Checkboxes
                    case ControlType.Checkbox:
                        {
                            switch (withBlock.Design[0])
                            {
                                case (long)UiDesign.CheckboxNormal:
                                    {
                                        // empty?
                                        if (withBlock.Value == 0L)
                                            sprite = 2L;
                                        else
                                            sprite = 3L;

                                        // render box
                                        string argpath4 = System.IO.Path.Combine(withBlock.Texture[0], sprite.ToString());
                                        GameClient.RenderTexture(ref argpath4, (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), 0, 0, 16, 16, 16, 16);

                                        // find text position
                                        switch (withBlock.Align)
                                        {
                                            case Alignment.Left:
                                                {
                                                    left = withBlock.Left + 18L + xO;
                                                    break;
                                                }
                                            case Alignment.Right:
                                                {
                                                    left = withBlock.Left + 18L + (withBlock.Width - 18L) - Text.GetTextWidth(withBlock.Text, withBlock.Font) + xO;
                                                    break;
                                                }
                                            case Alignment.Center:
                                                {
                                                    left = (long)Math.Round(withBlock.Left + 18L + (withBlock.Width - 18L) / 2d - Text.GetTextWidth(withBlock.Text, withBlock.Font) / 2d + xO);
                                                    break;
                                                }
                                        }

                                        // render text
                                        Text.RenderText(withBlock.Text, (int)left, (int)(withBlock.Top + yO), withBlock.Color, Color.Black);
                                        break;
                                    }

                                case (long)UiDesign.CheckboxChat:
                                    {
                                        if (withBlock.Value == 0L)
                                            withBlock.Alpha = 150L;
                                        else
                                            withBlock.Alpha = 255L;

                                        // render box
                                        string argpath5 = System.IO.Path.Combine(Path.Gui, 51.ToString());
                                        GameClient.RenderTexture(ref argpath5, (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), 0, 0, 49, 23, 49, 23);

                                        // render text
                                        left = (long)Math.Round(withBlock.Left + 22L - Text.GetTextWidth(withBlock.Text, withBlock.Font) / 2d + xO);
                                        Text.RenderText(withBlock.Text, (int)left + 8, (int)(withBlock.Top + yO + 4L), withBlock.Color, Color.Black);
                                        break;
                                    }

                                case (long)UiDesign.CheckboxBuying:
                                    {
                                        if (withBlock.Value == 0L)
                                            sprite = 58L;
                                        else
                                            sprite = 56L;
                                        string argpath6 = System.IO.Path.Combine(withBlock.Texture[0], sprite.ToString());
                                        GameClient.RenderTexture(ref argpath6, (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), 0, 0, 49, 20, 49, 20);
                                        break;
                                    }

                                case (long)UiDesign.CheckboxSelling:
                                    {
                                        if (withBlock.Value == 0L)
                                            sprite = 59L;
                                        else
                                            sprite = 57L;
                                        string argpath7 = System.IO.Path.Combine(withBlock.Texture[0], sprite.ToString());
                                        GameClient.RenderTexture(ref argpath7, (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), 0, 0, 49, 20, 49, 20);
                                        break;
                                    }
                            }

                            break;
                        }

                    // comboboxes
                    case ControlType.ComboMenu:
                        {
                            switch (withBlock.Design[0])
                            {
                                case (long)UiDesign.ComboBoxNormal:
                                    {
                                        // draw the background
                                        RenderDesign((long)UiDesign.TextBlack, withBlock.Left + xO, withBlock.Top + yO, withBlock.Width, withBlock.Height);

                                        // render the text
                                        if (withBlock.Value > 0L)
                                        {
                                            if (withBlock.Value <= withBlock.List.Count - 1)
                                            {
                                                Text.RenderText(withBlock.List[(int)withBlock.Value], (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), withBlock.Color, Color.Black);
                                            }
                                        }

                                        // draw the little arrow
                                        string argpath8 = System.IO.Path.Combine(withBlock.Texture[0], "66");
                                        GameClient.RenderTexture(ref argpath8, (int)(withBlock.Left + xO + withBlock.Width), (int)(withBlock.Top + yO), 0, 0, 5, 4, 5, 4);
                                        break;
                                    }
                            }

                            break;
                        }
                }

                if (withBlock.OnDraw is not null)
                    withBlock.OnDraw.Invoke();
            }
        }

        public static void RenderWindow(long winNum)
        {
            long x;
            long y;
            long i;
            long left;

            // Check if the window exists
            if (winNum < 0L || winNum >= Gui.Windows.Count)
            {
                return;
            }

            {
                var withBlock = Windows[winNum];

                // Apply censoring if necessary
                if (withBlock.Censor)
                {
                    withBlock.Text = Text.CensorText(withBlock.Text);
                }

                switch (withBlock.Design[0])
                {
                    case (long)UiDesign.ComboMenuNormal:
                        {
                            string argpath = System.IO.Path.Combine(Path.Gui, "1");
                            GameClient.RenderTexture(ref argpath, (int)withBlock.Left, (int)withBlock.Top, 0, 0, (int)withBlock.Width, (int)withBlock.Height, 157, 0, 0, 0);

                            // Render text
                            if (withBlock.List.Count > 0)
                            {
                                y = withBlock.Top + 2L;
                                x = withBlock.Left;

                                var loopTo = (long)(withBlock.List.Count - 1);
                                for (i = 0L; i < loopTo; i++)
                                {
                                    // Render selection
                                    if (i == withBlock.Value || i == withBlock.Group)
                                    {
                                        string argpath1 = System.IO.Path.Combine(Path.Gui, "1");
                                        GameClient.RenderTexture(ref argpath1, (int)x, (int)(y - 1L), 0, 0, (int)withBlock.Width, 15, 255, 0, 0, 0);
                                    }

                                    // Render the text, centered
                                    left = x + withBlock.Width / 2L - Text.GetTextWidth(withBlock.List[(int)i], withBlock.Font) / 2;
                                    Text.RenderText(withBlock.List[(int)i], (int)left, (int)y, Color.White, Color.Black);

                                    y += 16L;
                                }
                            }
                            return;
                        }
                }

                // Handle different window designs
                switch (withBlock.Design[(int)withBlock.State])
                {
                    case (long)UiDesign.WindowBlack:
                        {
                            string argpath2 = System.IO.Path.Combine(Path.Gui, "61");
                            GameClient.RenderTexture(ref argpath2, (int)withBlock.Left, (int)withBlock.Top, 0, 0, (int)withBlock.Width, (int)withBlock.Height, 190, 255, 255, 255);
                            break;
                        }

                    case (long)UiDesign.WindowNormal:
                        {
                            RenderDesign((long)UiDesign.Wood, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            RenderDesign((long)UiDesign.Green, withBlock.Left, withBlock.Top, withBlock.Width, 23L);
                            string argpath3 = System.IO.Path.Combine(Path.Items, withBlock.Icon.ToString());
                            GameClient.RenderTexture(ref argpath3, (int)(withBlock.Left + withBlock.xOffset), (int)(withBlock.Top - 16L + withBlock.yOffset), 0, 0, (int)withBlock.Width, (int)withBlock.Height, (int)withBlock.Width, (int)withBlock.Height);
                            Text.RenderText(withBlock.Text, (int)(withBlock.Left + 32L), (int)(withBlock.Top + 4L), Color.White, Color.Black);
                            break;
                        }

                    case (long)UiDesign.WindowNoBar:
                        {
                            RenderDesign((long)UiDesign.Wood, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            break;
                        }

                    case (long)UiDesign.WindowEmpty:
                        {
                            RenderDesign((long)UiDesign.WoodEmpty, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            RenderDesign((long)UiDesign.Green, withBlock.Left, withBlock.Top, withBlock.Width, 23L);
                            string argpath4 = System.IO.Path.Combine(Path.Items, withBlock.Icon.ToString());
                            GameClient.RenderTexture(ref argpath4, (int)(withBlock.Left + withBlock.xOffset), (int)(withBlock.Top - 16L + withBlock.yOffset), 0, 0, (int)withBlock.Width, (int)withBlock.Height, (int)withBlock.Width, (int)withBlock.Height);
                            Text.RenderText(withBlock.Text, (int)(withBlock.Left + 32L), (int)(withBlock.Top + 4L), Color.White, Color.Black);
                            break;
                        }

                    case (long)UiDesign.WindowDescription:
                        {
                            RenderDesign((long)UiDesign.WindowDescription, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            break;
                        }

                    case (long)UiDesign.WindowWithShadow:
                        {
                            RenderDesign((long)UiDesign.WindowWithShadow, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            break;
                        }

                    case (long)UiDesign.WindowParty:
                        {
                            RenderDesign((long)UiDesign.WindowParty, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            break;
                        }
                }

                // Call the OnDraw action if it exists
                if (withBlock.OnDraw is not null)
                    withBlock.OnDraw.Invoke();
            }
        }

        public static void RenderDesign(long design, long left, long top, long width, long height, long alpha = 255L)
        {
            long bs;

            switch (design)
            {
                case (long)UiDesign.MenuHeader:
                    {
                        // render the header
                        string argpath = System.IO.Path.Combine(Path.Designs, "61");
                        GameClient.RenderTexture(ref argpath, (int)left, (int)top, 0, 0, (int)width, (int)height, (int)width, (int)height, 200, 47, 77, 29);
                        break;
                    }

                case (long)UiDesign.MenuOption:
                    {
                        // render the option
                        string argpath1 = System.IO.Path.Combine(Path.Designs, "61");
                        GameClient.RenderTexture(ref argpath1, (int)left, (int)top, 0, 0, (int)width, (int)height, (int)width, (int)height, 200, 98, 98, 98);
                        break;
                    }

                case (long)UiDesign.Wood:
                    {
                        bs = 4L;
                        // render the wood box
                        RenderControl_Square(1, left, top, width, height, bs, alpha);

                        // render wood texture
                        string argpath2 = System.IO.Path.Combine(Path.Gui, "1");
                        GameClient.RenderTexture(ref argpath2, (int)(left + bs), (int)(top + bs), 100, 100, (int)(width - bs * 2L), (int)(height - bs * 2L), (int)(width - bs * 2L), (int)(height - bs * 2L), (byte)alpha);
                        break;
                    }

                case (long)UiDesign.WoodSmall:
                    {
                        bs = 2L;
                        // render the wood box
                        RenderControl_Square(8, left + bs, top + bs, width, height, bs, alpha);

                        // render wood texture
                        string argpath3 = System.IO.Path.Combine(Path.Gui, "1");
                        GameClient.RenderTexture(ref argpath3, (int)(left + bs), (int)(top + bs), 100, 100, (int)(width - bs * 2L), (int)(height - bs * 2L), (int)(width - bs * 2L), (int)(height - bs * 2L), (byte)alpha);
                        break;
                    }

                case (long)UiDesign.WoodEmpty:
                    {
                        bs = 4L;
                        // render the wood box
                        RenderControl_Square(9, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)UiDesign.Green:
                    {
                        bs = 2L;
                        // render the green box
                        RenderControl_Square(2, left, top, width, height, bs, alpha);

                        // render green gradient overlay
                        string argpath4 = System.IO.Path.Combine(Path.Gradients, "1");
                        GameClient.RenderTexture(ref argpath4, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.GreenHover:
                    {
                        bs = 2L;
                        // render the green box
                        RenderControl_Square(2, left, top, width, height, bs, alpha);

                        // render green gradient overlay
                        string argpath5 = System.IO.Path.Combine(Path.Gradients, "2");
                        GameClient.RenderTexture(ref argpath5, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.GreenClick:
                    {
                        bs = 2L;
                        // render the green box
                        RenderControl_Square(2, left, top, width, height, bs, alpha);

                        // render green gradient overlay
                        string argpath6 = System.IO.Path.Combine(Path.Gradients, "3");
                        GameClient.RenderTexture(ref argpath6, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.Red:
                    {
                        bs = 2L;
                        // render the red box
                        RenderControl_Square(3, left, top, width, height, bs, alpha);

                        // render red gradient overlay
                        string argpath7 = System.IO.Path.Combine(Path.Gradients, "4");
                        GameClient.RenderTexture(ref argpath7, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.RedHover:
                    {
                        bs = 2L;
                        // render the red box
                        RenderControl_Square(3, left, top, width, height, bs, alpha);

                        // render red gradient overlay
                        string argpath8 = System.IO.Path.Combine(Path.Gradients, "5");
                        GameClient.RenderTexture(ref argpath8, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.RedClick:
                    {
                        bs = 2L;
                        // render the red box
                        RenderControl_Square(3, left, top, width, height, bs, alpha);

                        // render red gradient overlay
                        string argpath9 = System.IO.Path.Combine(Path.Gradients, "6");
                        GameClient.RenderTexture(ref argpath9, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.Blue:
                    {
                        bs = 2L;
                        // render the Blue box
                        RenderControl_Square(14, left, top, width, height, bs, alpha);

                        // render Blue gradient overlay
                        string argpath10 = System.IO.Path.Combine(Path.Gradients, "8");
                        GameClient.RenderTexture(ref argpath10, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.BlueHover:
                    {
                        bs = 2L;
                        // render the Blue box
                        RenderControl_Square(14, left, top, width, height, bs, alpha);

                        // render Blue gradient overlay
                        string argpath11 = System.IO.Path.Combine(Path.Gradients, "9");
                        GameClient.RenderTexture(ref argpath11, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.BlueClick:
                    {
                        bs = 2L;
                        // render the Blue box
                        RenderControl_Square(14, left, top, width, height, bs, alpha);

                        // render Blue gradient overlay
                        string argpath12 = System.IO.Path.Combine(Path.Gradients, "10");
                        GameClient.RenderTexture(ref argpath12, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.Orange:
                    {
                        bs = 2L;
                        // render the Orange box
                        RenderControl_Square(15, left, top, width, height, bs, alpha);

                        // render Orange gradient overlay
                        string argpath13 = System.IO.Path.Combine(Path.Gradients, "11");
                        GameClient.RenderTexture(ref argpath13, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.OrangeHover:
                    {
                        bs = 2L;
                        // render the Orange box
                        RenderControl_Square(15, left, top, width, height, bs, alpha);

                        // render Orange gradient overlay
                        string argpath14 = System.IO.Path.Combine(Path.Gradients, "12");
                        GameClient.RenderTexture(ref argpath14, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.OrangeClick:
                    {
                        bs = 2L;
                        // render the Orange box
                        RenderControl_Square(15, left, top, width, height, bs, alpha);

                        // render Orange gradient overlay
                        string argpath15 = System.IO.Path.Combine(Path.Gradients, "13");
                        GameClient.RenderTexture(ref argpath15, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.Grey:
                    {
                        bs = 2L;
                        // render the Orange box
                        RenderControl_Square(17, left, top, width, height, bs, alpha);

                        // render Orange gradient overlay
                        string argpath16 = System.IO.Path.Combine(Path.Gradients, "14");
                        GameClient.RenderTexture(ref argpath16, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.Parchment:
                    {
                        bs = 20L;
                        // render the parchment box
                        RenderControl_Square(4, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)UiDesign.BlackOval:
                    {
                        bs = 4L;
                        // render the black oval
                        RenderControl_Square(5, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)UiDesign.TextBlack:
                    {
                        bs = 5L;
                        // render the black oval
                        RenderControl_Square(6, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)UiDesign.TextWhite:
                    {
                        bs = 5L;
                        // render the black oval
                        RenderControl_Square(7, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)UiDesign.TextBlackSquare:
                    {
                        bs = 4L;
                        // render the black oval
                        RenderControl_Square(10, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)UiDesign.WindowDescription:
                    {
                        bs = 8L;
                        // render black square
                        RenderControl_Square(11, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)UiDesign.DescriptionPicture:
                    {
                        bs = 3L;
                        // render the green box
                        RenderControl_Square(12, left, top, width, height, bs, alpha);

                        // render green gradient overlay
                        string argpath17 = System.IO.Path.Combine(Path.Gradients, "7");
                        GameClient.RenderTexture(ref argpath17, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)UiDesign.WindowWithShadow:
                    {
                        bs = 35L;
                        // render the green box
                        RenderControl_Square(13, left - bs, top - bs, width + bs * 2L, height + bs * 2L, bs, alpha);
                        break;
                    }

                case (long)UiDesign.WindowParty:
                    {
                        bs = 12L;
                        // render black square
                        RenderControl_Square(16, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)UiDesign.TileSelectionBox:
                    {
                        bs = 4L;
                        // render box
                        RenderControl_Square(18, left, top, width, height, bs, alpha);
                        break;
                    }
            }

        }

        public static void RenderControl_Square(int sprite, long x, long y, long width, long height, long borderSize, long alpha = 255L, int windowID = 0)
        {
            long bs;

            // Set the border size
            bs = borderSize;

            // Draw center
            string argpath = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath, (int)(x + bs), (int)(y + bs), (int)(bs + 1L), (int)(bs + 1L), (int)(width - bs * 2L), (int)(height - bs * 2L), alpha: (byte)alpha);

            // Draw top side
            string argpath1 = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath1, (int)(x + bs), (int)y, (int)bs, 0, (int)(width - bs * 2L), (int)bs, 1, (int)bs, (byte)alpha);

            // Draw left side
            string argpath2 = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath2, (int)x, (int)(y + bs), 0, (int)bs, (int)bs, (int)(height - bs * 2L), (int)bs, alpha: (byte)alpha);

            // Draw right side
            string argpath3 = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath3, (int)(x + width - bs), (int)(y + bs), (int)(bs + 3L), (int)bs, (int)bs, (int)(height - bs * 2L), (int)bs, alpha: (byte)alpha);

            // Draw bottom side
            string argpath4 = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath4, (int)(x + bs), (int)(y + height - bs), (int)bs, (int)(bs + 3L), (int)(width - bs * 2L), (int)bs, 1, (int)bs, (byte)alpha);

            // Draw top left corner
            string argpath5 = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath5, (int)x, (int)y, 0, 0, (int)bs, (int)bs, (int)bs, (int)bs, (byte)alpha);

            // Draw top right corner
            string argpath6 = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath6, (int)(x + width - bs), (int)y, (int)(bs + 3L), 0, (int)bs, (int)bs, (int)bs, (int)bs, (byte)alpha);

            // Draw bottom left corner
            string argpath7 = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath7, (int)x, (int)(y + height - bs), 0, (int)(bs + 3L), (int)bs, (int)bs, (int)bs, (int)bs, (byte)alpha);

            // Draw bottom right corner
            string argpath8 = System.IO.Path.Combine(Path.Designs, sprite.ToString());
            GameClient.RenderTexture(ref argpath8, (int)(x + width - bs), (int)(y + height - bs), (int)(bs + 3L), (int)(bs + 3L), (int)bs, (int)bs, (int)bs, (int)bs, (byte)alpha);
        }

        // Trade
        public static void btnTrade_Close()
        {
            HideWindow(GetWindowIndex("winTrade"));
            Trade.SendDeclineTrade();
        }

        public static void btnTrade_Accept()
        {
            Trade.SendAcceptTrade();
        }

        public static void TradeDoubleClick_Your()
        {
            long xo;
            long yo;
            long itemNum;

            xo = Windows[GetWindowIndex("winTrade")].Left + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picYour")].Left;
            yo = Windows[GetWindowIndex("winTrade")].Top + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picYour")].Top;
            itemNum = General.IsTrade(xo, yo);

            // make sure it exists
            if (itemNum >= 0L)
            {
                if (Data.TradeYourOffer[(int)itemNum].Num == -1)
                    return;
                if (GetPlayerInv(GameState.MyIndex, (int)Data.TradeYourOffer[(int)itemNum].Num) == -1)
                    return;

                // unoffer the item
                Trade.UntradeItem((int)itemNum);
            }

            TradeMouseMove_Your();
        }

        public static void TradeMouseMove_Your()
        {
            long xo;
            long yo;
            long itemNum;
            long x;
            long y;

            xo = Windows[GetWindowIndex("winTrade")].Left + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picYour")].Left;
            yo = Windows[GetWindowIndex("winTrade")].Top + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picYour")].Top;

            itemNum = General.IsTrade(xo, yo);

            // make sure it exists
            if (itemNum >= 0L)
            {
                if (Data.TradeYourOffer[(int)itemNum].Num == -1)
                {
                    Windows[GetWindowIndex("winDescription")].Visible = false;
                    return;
                }

                if (GetPlayerInv(GameState.MyIndex, (int)Data.TradeYourOffer[(int)itemNum].Num) == -1)
                {
                    Windows[GetWindowIndex("winDescription")].Visible = false;
                    return;
                }

                x = Windows[GetWindowIndex("winTrade")].Left - Windows[GetWindowIndex("winDescription")].Width;
                y = Windows[GetWindowIndex("winTrade")].Top - 6L;

                // offscreen?
                if (x < 0L)
                {
                    // switch to right
                    x = Windows[GetWindowIndex("winTrade")].Left + Windows[GetWindowIndex("winTrade")].Width;
                }

                // go go go
                GameLogic.ShowItemDesc(x, y, (long)GetPlayerInv(GameState.MyIndex, (int)Data.TradeYourOffer[(int)itemNum].Num));
            }
            else
            {
                Windows[GetWindowIndex("winDescription")].Visible = false;
            }
        }

        public static void TradeMouseMove_Their()
        {
            long xo;
            long yo;
            long itemNum;
            long x;
            long y;

            xo = Windows[GetWindowIndex("winTrade")].Left + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picTheir")].Left;
            yo = Windows[GetWindowIndex("winTrade")].Top + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picTheir")].Top;

            itemNum = General.IsTrade(xo, yo);

            // make sure it exists
            if (itemNum >= 0L)
            {
                if (Data.TradeTheirOffer[(int)itemNum].Num == -1)
                {
                    Windows[GetWindowIndex("winDescription")].Visible = false;
                    return;
                }

                // calc position
                x = Windows[GetWindowIndex("winTrade")].Left - Windows[GetWindowIndex("winDescription")].Width;
                y = Windows[GetWindowIndex("winTrade")].Top - 6L;

                // offscreen?
                if (x < 0L)
                {
                    // switch to right
                    x = Windows[GetWindowIndex("winTrade")].Left + Windows[GetWindowIndex("winTrade")].Width;
                }

                // go go go
                GameLogic.ShowItemDesc(x, y, (long)Data.TradeTheirOffer[(int)itemNum].Num);
            }
            else
            {
                Windows[GetWindowIndex("winDescription")].Visible = false;
            }
        }

        public static void CloseComboMenu()
        {
            HideWindow(GetWindowIndex("winComboMenuBG"));
            HideWindow(GetWindowIndex("winComboMenu"));
        }

        public static void ShowComboMenu(long curWindow, long curControl)
        {
            long Top;

            {
                var withBlock = Windows[curWindow].Controls[(int)curControl];
                // Linked to
                long comboMenuIndex = GetWindowIndex("winComboMenu");
                Windows[comboMenuIndex].LinkedToWin = curWindow;
                Windows[comboMenuIndex].LinkedToCon = curControl;

                // Set the size
                Windows[comboMenuIndex].Height = 2 + withBlock.List.Count * 16;  // Assumes .List is a collection
                Windows[comboMenuIndex].Left = Windows[curWindow].Left + withBlock.Left + 2L;
                Top = Windows[curWindow].Top + withBlock.Top + withBlock.Height;
                if (Top + Windows[comboMenuIndex].Height > GameState.ResolutionHeight)
                {
                    Top = GameState.ResolutionHeight - Windows[comboMenuIndex].Height;
                }
                Windows[comboMenuIndex].Top = Top;
                Windows[comboMenuIndex].Width = withBlock.Width - 4L;

                // Set the values
                Windows[comboMenuIndex].List = withBlock.List;
                Windows[comboMenuIndex].Value = withBlock.Value;
                Windows[comboMenuIndex].Group = 0L;

                // Load the menu
                Windows[comboMenuIndex].Visible = true;
                Windows[GetWindowIndex("winComboMenuBG")].Visible = true;
                ShowWindow(GetWindowIndex("winComboMenuBG"), true, false);
                ShowWindow(GetWindowIndex("winComboMenu"), true, false);
            }
        }

        public static void ComboMenu_MouseMove(long curWindow)
        {
            long y;
            long i;
            {
                var withBlock = Windows[curWindow];
                y = GameState.CurMouseY - withBlock.Top;

                // Find the option we're hovering over
                if (withBlock.List.Count > 0)
                {
                    var loopTo = (long)(withBlock.List.Count - 1);
                    for (i = 0L; i < loopTo; i++)
                    {
                        if (y >= 16L * i & y <= 16L * i)
                        {
                            withBlock.Group = i;
                        }
                    }
                }
            }
        }

        public static void ComboMenu_MouseDown(long curWindow)
        {
            long y;
            long i;

            {
                var withBlock = Windows[curWindow];
                y = GameState.CurMouseY - withBlock.Top;

                // Find the option we're hovering over
                if (withBlock.List is not null && withBlock.List.Count > 0)
                {
                    var loopTo = (long)withBlock.List.Count;
                    for (i = 0L; i < loopTo; i++)
                    {
                        if (y >= 16L * i & y <= 16L * i)
                        {
                            Windows[withBlock.LinkedToWin].Controls[(int)withBlock.LinkedToCon].Value = i;
                            CloseComboMenu();
                            break;
                        }
                    }
                }
            }
        }

        public static void chkSaveUser_Click()
        {
            {
                var withBlock = Windows[GetWindowIndex("winLogin")].Controls[GetControlIndex("winLogin", "chkSaveUsername")];
                if (withBlock.Value == 0L) // set as false
                {
                    SettingsManager.Instance.SaveUsername = false;
                    SettingsManager.Instance.Username = "";
                    SettingsManager.Save();
                }
                else
                {
                    SettingsManager.Instance.SaveUsername = true;
                    SettingsManager.Save();
                }
            }
        }

        public static void btnRegister_Click()
        {
            if (!(bool)(NetworkConfig.Socket?.IsConnected))
            {
                GameLogic.Dialogue("Invalid Connection", "Cannot connect to game server.", "Please try again.", (byte)DialogueType.Alert);
                return;
            }

            HideWindows();
            // RenCaptcha()
            ClearPasswordTexts();
            ShowWindow(GetWindowIndex("winRegister"));
        }

        public static void ClearPasswordTexts()
        {
            long I;
            {
                var withBlock = Windows[GetWindowIndex("winRegister")];
                // .Controls(GetControlIndex("winRegister", "txtUsername")).Text = ""
                withBlock.Controls[GetControlIndex("winRegister", "txtPassword")].Text = "";
                withBlock.Controls[GetControlIndex("winRegister", "txtRetypePassword")].Text = "";
                // .Controls(GetControlIndex("winRegister", "txtCode")).Text = ""
                // .Controls(GetControlIndex("winRegister", "txtCaptcha")).Text = ""
                // For i = 0 To 6
                // .Controls(GetControlIndex("winRegister", "picCaptcha")).Image(I) = Tex_Captcha(GlobalCaptcha)
                // Next
            }

            {
                var withBlock1 = Windows[GetWindowIndex("winLogin")];
                withBlock1.Controls[GetControlIndex("winLogin", "txtPassword")].Text = "";
            }
        }

        public static void btnSendRegister_Click()
        {
            string User;
            string Pass;
            string pass2; // Code As String, Captcha As String

            {
                var withBlock = Windows[GetWindowIndex("winRegister")];
                User = withBlock.Controls[GetControlIndex("winRegister", "txtUsername")].Text;
                Pass = withBlock.Controls[GetControlIndex("winRegister", "txtPassword")].Text;
                pass2 = withBlock.Controls[GetControlIndex("winRegister", "txtRetypePassword")].Text;
                // Code = .Controls(GetControlIndex("winRegister", "txtCode")).Text
                // Captcha = .Controls(GetControlIndex("winRegister", "txtCaptcha")).Text

                if ((Pass ?? "") != (pass2 ?? ""))
                {
                    GameLogic.Dialogue("Register", "Passwords don't match.", "Please try again.", (byte)DialogueType.Alert);
                    ClearPasswordTexts();
                    return;
                }

                if (NetworkConfig.Socket?.IsConnected == true)
                {
                    NetworkSend.SendRegister(User, Pass);
                }
                else
                {
                    GameLogic.Dialogue("Invalid Connection", "Cannot connect to game server.", "Please try again.", (byte)DialogueType.Alert);
                }
            }
        }

        public static void btnReturnMain_Click()
        {
            HideWindows();
            ShowWindow(GetWindowIndex("winLogin"));
        }

        // ##########
        // ## Menu ##
        // ##########
        public static void btnMenu_Char()
        {
            long curWindow;

            curWindow = GetWindowIndex("winCharacter");

            if (Windows[curWindow].Visible == true)
            {
                HideWindow(curWindow);
            }
            else
            {
                ShowWindow(curWindow, resetPosition: false);
            }
        }

        public static void btnMenu_Inv()
        {
            long curWindow;

            curWindow = GetWindowIndex("winInventory");

            if (Windows[curWindow].Visible == true)
            {
                HideWindow(curWindow);
            }
            else
            {
                ShowWindow(curWindow, resetPosition: false);
            }
        }

        public static void btnMenu_Skills()
        {
            long curWindow;

            curWindow = GetWindowIndex("winSkills");

            if (Windows[curWindow].Visible == true)
            {
                HideWindow(curWindow);
            }
            else
            {
                ShowWindow(curWindow, resetPosition: false);
            }
        }

        public static void btnMenu_Map()
        {
            // Windows(GetWindowIndex("winCharacter")).Visible = Not Windows(GetWindowIndex("winCharacter")).Visible
        }

        public static void btnMenu_Guild()
        {
            // Windows(GetWindowIndex("winCharacter")).Visible = Not Windows(GetWindowIndex("winCharacter")).Visible
        }

        public static void btnMenu_Quest()
        {
            // Windows(GetWindowIndex("winCharacter")).Visible = Not Windows(GetWindowIndex("winCharacter")).Visible
        }

        // ##############
        // ## Esc Menu ##
        // ##############
        public static void btnEscMenu_Return()
        {
            HideWindow(GetWindowIndex("winEscMenu"));
        }

        public static void btnEscMenu_Options()
        {
            HideWindow(GetWindowIndex("winEscMenu"));
            ShowWindow(GetWindowIndex("winOptions"), true, true);
        }

        public static void btnEscMenu_MainMenu()
        {
            HideWindows();
            ShowWindow(GetWindowIndex("winLogin"));
            GameLogic.LogoutGame();
        }

        public static void btnEscMenu_Exit()
        {
            HideWindow(GetWindowIndex("winEscMenu"));
            General.DestroyGame();
        }

        public static void btnAcceptChar_1()
        {
            NetworkSend.SendUseChar(1);
        }

        public static void btnAcceptChar_2()
        {
            NetworkSend.SendUseChar(2);
        }

        public static void btnAcceptChar_3()
        {
            NetworkSend.SendUseChar(3);
        }

        public static void btnDelChar_1()
        {
            GameLogic.Dialogue("Delete Character", "Deleting this character is permanent.", "Delete this character?", (byte)DialogueType.DeleteCharacter, (byte)DialogueStyle.YesNo, 1L);
        }

        public static void btnDelChar_2()
        {
            GameLogic.Dialogue("Delete Character", "Deleting this character is permanent.", "Delete this character?", (byte)DialogueType.DeleteCharacter, (byte)DialogueStyle.YesNo, 2L);
        }

        public static void btnDelChar_3()
        {
            GameLogic.Dialogue("Delete Character", "Deleting this character is permanent.", "Delete this character?", (byte)DialogueType.DeleteCharacter, (byte)DialogueStyle.YesNo, 3L);
        }

        public static void btnCreateChar_1()
        {
            GameState.CharNum = 1;
            GameLogic.ShowJobs();
        }

        public static void btnCreateChar_2()
        {
            GameState.CharNum = 2;
            GameLogic.ShowJobs();
        }

        public static void btnCreateChar_3()
        {
            GameState.CharNum = 3;
            GameLogic.ShowJobs();
        }

        public static void btnCharacters_Close()
        {
            NetworkConfig.InitNetwork();
            HideWindows();
            ShowWindow(GetWindowIndex("winLogin"));
        }

        // ##########
        // ## Bars ##
        // ##########
        public static void Bars_OnDraw()
        {
            long xO;
            long yO;
            long Width;

            xO = Windows[GetWindowIndex("winBars")].Left;
            yO = Windows[GetWindowIndex("winBars")].Top;

            // Bars
            string argpath = System.IO.Path.Combine(Path.Gui, 27.ToString());
            GameClient.RenderTexture(ref argpath, (int)(xO + 15L), (int)(yO + 15L), 0, 0, (int)GameState.BarWidth_GuiHP, 13, (int)GameState.BarWidth_GuiHP, 13);
            string argpath1 = System.IO.Path.Combine(Path.Gui, 28.ToString());
            GameClient.RenderTexture(ref argpath1, (int)(xO + 15L), (int)(yO + 32L), 0, 0, (int)GameState.BarWidth_GuiSP, 13, (int)GameState.BarWidth_GuiSP, 13);
            string argpath2 = System.IO.Path.Combine(Path.Gui, 29.ToString());
            GameClient.RenderTexture(ref argpath2, (int)(xO + 15L), (int)(yO + 49L), 0, 0, (int)GameState.BarWidth_GuiEXP, 13, (int)GameState.BarWidth_GuiEXP, 13);
        }

        // #######################
        // ## Characters Window ##
        // #######################

        public static void Chars_OnDraw()
        {
            long xO;
            long yO;
            long x;
            long i;

            // Get the window's top-left corner coordinates
            xO = Windows[GetWindowIndex("WinChars")].Left;
            yO = Windows[GetWindowIndex("WinChars")].Top;

            // Set the initial X position for character rendering
            x = xO + 24L;

            // Loop through all characters and render them if they exist
            for (i = 0L; i <= Constant.MAX_CHARS - 1; i++)
            {
                if (!string.IsNullOrEmpty(GameState.CharName[(int)i])) // Ensure character name exists
                {
                    if (GameState.CharSprite[(int)i] > 0L) // Ensure character sprite is valid
                    {
                        // Define the rectangle for the character sprite
                        var rect = new Rectangle((int)Math.Round(GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, GameState.CharSprite[(int)i].ToString())).Width / 4d), (int)Math.Round(GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, GameState.CharSprite[(int)i].ToString())).Height / 4d), (int)Math.Round(GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, GameState.CharSprite[(int)i].ToString())).Width / 4d), (int)Math.Round(GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, GameState.CharSprite[(int)i].ToString())).Height / 4d));

                        // Ensure the sprite index is within bounds
                        if (GameState.CharSprite[(int)i] <= GameState.NumCharacters)
                        {
                            // Render the character sprite
                            string argpath = System.IO.Path.Combine(Path.Characters, GameState.CharSprite[(int)i].ToString());
                            GameClient.RenderTexture(ref argpath, (int)(x + 30L), (int)(yO + 100L), 0, 0, rect.Width, rect.Height, rect.Width, rect.Height);
                        }
                    }
                }

                // Move to the next position for the next character
                x += 110L;
            }
        }

        // ####################
        // ## Jobs Window ##
        // ####################

        public static void Jobs_DrawFace()
        {
            var imageChar = default(long);
            long xO;
            long yO;

            // Get window coordinates
            xO = Windows[GetWindowIndex("winJobs")].Left;
            yO = Windows[GetWindowIndex("winJobs")].Top;

            // Determine character image based on job
            switch (GameState.NewCharJob)
            {
                case 0L: // Warrior
                    {
                        imageChar = 1L;
                        break;
                    }
                case 1L: // Wizard
                    {
                        imageChar = 2L;
                        break;
                    }
                case 2L: // Whisperer
                    {
                        imageChar = 3L;
                        break;
                    }
            }

            // Render the character's face
            var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, imageChar.ToString()));

            string argpath = System.IO.Path.Combine(Path.Characters, imageChar.ToString());
            GameClient.RenderTexture(ref argpath, (int)(xO + 50L), (int)(yO + 90L), 0, 0, (int)Math.Round(gfxInfo.Width / 4d), (int)Math.Round(gfxInfo.Height / 4d), (int)Math.Round(gfxInfo.Width / 4d), (int)Math.Round(gfxInfo.Height / 4d));

        }

        public static void Jobs_DrawText()
        {
            var text = default(string);
            long xO;
            long yO;
            var textArray = default(string[]);
            long i;
            long count;
            long y;
            long x;

            // Get window coordinates
            xO = Windows[GetWindowIndex("winJobs")].Left;
            yO = Windows[GetWindowIndex("winJobs")].Top;

            // Get job description or use default
            if (string.IsNullOrEmpty(Data.Job[(int)GameState.NewCharJob].Desc))
            {
                switch (GameState.NewCharJob)
                {
                    case 0L: // Warrior
                        {
                            text = "The way of a warrior has never been an easy one. ...";
                            break;
                        }
                    case 1L: // Wizard
                        {
                            text = "Wizards are often mistrusted characters who ... enjoy setting things on fire.";
                            break;
                        }
                    case 2L: // Whisperer
                        {
                            text = "The art of healing comes with pressure and guilt, ...";
                            break;
                        }
                }
            }
            else
            {
                text = Data.Job[(int)GameState.NewCharJob].Desc;
            }

            // Wrap text to fit within 330 pixels
            Text.WordWrap(text, Windows[GetWindowIndex("winJobs")].Font, 330L, ref textArray);

            count = Information.UBound(textArray);
            y = yO + 60L;
            var loopTo = count + 1;
            for (i = 0L; i < loopTo; i++)
            {
                x = xO + 118L + 200 / 2 - Text.GetTextWidth(textArray[(int)i], Windows[GetWindowIndex("winJobs")].Font) / 2;
                // Render each line of the wrapped text
                string sanitizedText = new string(textArray[(int)i].Where(c => Text.Fonts[Windows[GetWindowIndex("winJobs")].Font].Characters.Contains(c)).ToArray());
                var actualSize = Text.Fonts[Windows[GetWindowIndex("winJobs")].Font].MeasureString(sanitizedText);
                float actualWidth = actualSize.X;
                float actualHeight = actualSize.Y;

                // Calculate horizontal and vertical centers with padding
                double padding = (double)actualWidth / 6.0d;
                Text.RenderText(textArray[(int)i], (int)Math.Round(x + padding), (int)y, Color.White, Color.Black);
                y += 14L;
            }
        }

        public static void btnJobs_Left()
        {
            // Move to the previous job
            GameState.NewCharJob -= 1L;
            if (GameState.NewCharJob < 0L)
                GameState.NewCharJob = 0L;

            // Update class name display
            Windows[GetWindowIndex("winJobs")].Controls[GetControlIndex("winJobs", "lblJobName")].Text = Data.Job[(int)GameState.NewCharJob].Name;
        }

        public static void btnJobs_Right()
        {
            // Exit if the job is invalid or exceeds limits
            if (GameState.NewCharJob >= Constant.MAX_JOBS - 1 || string.IsNullOrEmpty(Data.Job[(int)GameState.NewCharJob ].Desc) & GameState.NewCharJob >= Constant.MAX_JOBS)
                return;

            // Move to the next job
            GameState.NewCharJob += 1L;

            // Update class name display
            Windows[GetWindowIndex("winJobs")].Controls[GetControlIndex("winJobs", "lblJobName")].Text = Data.Job[(int)GameState.NewCharJob ].Name;
        }

        public static void btnJobs_Accept()
        {
            HideWindow(GetWindowIndex("winJobs"));
            ShowWindow(GetWindowIndex("winNewChar"));
            Gui.Windows[Gui.GetWindowIndex("winNewChar")].Controls[(int)Gui.GetControlIndex("winNewChar", "txtName")].Text = "";
            Gui.Windows[Gui.GetWindowIndex("winNewChar")].Controls[(int)Gui.GetControlIndex("winNewChar", "chkMale")].Value = 1L;
            Gui.Windows[Gui.GetWindowIndex("winNewChar")].Controls[(int)Gui.GetControlIndex("winNewChar", "chkFemale")].Value = 0L;
        }

        public static void btnJobs_Close()
        {
            HideWindows();
            ShowWindow(GetWindowIndex("winChars"));
        }

        // Chat
        public static void btnSay_Click()
        {
            GameLogic.HandlePressEnter();
        }

        public static void Chat_OnDraw()
        {
            long winIndex;
            long xO;
            long yO;

            winIndex = GetWindowIndex("winChat");
            xO = Windows[winIndex].Left;
            yO = Windows[winIndex].Top + 16L;

            // draw the box
            RenderDesign((long)UiDesign.WindowDescription, xO, yO, 352L, 152L);

            // draw the input box
            string argpath = System.IO.Path.Combine(Path.Gui, 46.ToString());
            GameClient.RenderTexture(ref argpath, (int)(xO + 7L), (int)(yO + 123L), 0, 0, 171, 22, 171, 22);
            string argpath1 = System.IO.Path.Combine(Path.Gui, 46.ToString());
            GameClient.RenderTexture(ref argpath1, (int)(xO + 174L), (int)(yO + 123L), 0, 22, 171, 22, 171, 22);

            // call the chat render
            Text.DrawChat();
        }

        public static void ChatSmall_OnDraw()
        {
            long winIndex;
            long xO;
            long yO;

            winIndex = GetWindowIndex("winChatSmall");

            if (GameState.actChatWidth < 160L)
                GameState.actChatWidth = 160L;
            if (GameState.actChatHeight < 10L)
                GameState.actChatHeight = 10L;

            xO = Windows[winIndex].Left + 10L;
            yO = GameState.ResolutionHeight - 10;

            // draw the background
            RenderDesign((long)UiDesign.WindowWithShadow, xO, yO, 160L, 10L);
        }

        public static void CheckboxChat_Game()
        {
            SettingsManager.Instance.ChannelState[(int)ChatChannel.Game] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkGame")].Value;
            SettingsManager.Save();
        }

        public static void CheckboxChat_Map()
        {
            SettingsManager.Instance.ChannelState[(int)ChatChannel.Map] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkMap")].Value;
            SettingsManager.Save();
        }

        public static void CheckboxChat_Global()
        {
            SettingsManager.Instance.ChannelState[(int)ChatChannel.Broadcast] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkGlobal")].Value;
            SettingsManager.Save();
        }

        public static void CheckboxChat_Party()
        {
            SettingsManager.Instance.ChannelState[(int)ChatChannel.Party] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkParty")].Value;
            SettingsManager.Save();
        }

        public static void CheckboxChat_Guild()
        {
            SettingsManager.Instance.ChannelState[(int)ChatChannel.Guild] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkGuild")].Value;
            SettingsManager.Save();
        }

        public static void CheckboxChat_Player()
        {
            SettingsManager.Instance.ChannelState[(int)ChatChannel.Private] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkPlayer")].Value;
            SettingsManager.Save();
        }

        public static void btnChat_Up()
        {
            GameState.ChatButtonUp = true;
        }

        public static void btnChat_Down()
        {
            GameState.ChatButtonDown = true;
        }

        public static void btnChat_Up_MouseUp()
        {
            GameState.ChatButtonUp = false;
        }

        public static void btnChat_Down_MouseUp()
        {
            GameState.ChatButtonDown = false;
        }

        // ###################
        // ## New Character ##
        // ###################
        public static void NewChar_OnDraw()
        {
            long imageChar;
            long xO;
            long yO;

            xO = Windows[GetWindowIndex("winNewChar")].Left;
            yO = Windows[GetWindowIndex("winNewChar")].Top;

            if (GameState.NewCnarGender == (long)Sex.Male)
            {
                imageChar = Data.Job[(int)GameState.NewCharJob].MaleSprite;
            }
            else
            {
                imageChar = Data.Job[(int)GameState.NewCharJob].FemaleSprite;
            }

            if (imageChar == 0)
                imageChar = 1;

            var gfxInfo = GameClient.GetGfxInfo(System.IO.Path.Combine(Path.Characters, imageChar.ToString()));
            if (gfxInfo is null)
            {
                return; // Or handle this case gracefully
            }

            var rect = new Rectangle((int)Math.Round(gfxInfo.Width / 4d), (int)Math.Round(gfxInfo.Height / 4d), (int)Math.Round(gfxInfo.Width / 4d), (int)Math.Round(gfxInfo.Height / 4d));

            // render char
            string argpath = System.IO.Path.Combine(Path.Characters, imageChar.ToString());
            GameClient.RenderTexture(ref argpath, (int)(xO + 190L), (int)(yO + 100L), 0, 0, rect.Width, rect.Height, rect.Width, rect.Height);
        }

        public static void btnNewChar_Left()
        {
            long spriteCount;

            if (GameState.NewCnarGender == (long)Sex.Male)
            {
                spriteCount = Data.Job[(int)GameState.NewCharJob].MaleSprite;
            }
            else
            {
                spriteCount = Data.Job[(int)GameState.NewCharJob].FemaleSprite;
            }

            if (GameState.NewCharSprite < 0L)
            {
                GameState.NewCharSprite = spriteCount;
            }
            else
            {
                GameState.NewCharSprite = GameState.NewCharSprite - 1L;
            }
        }

        public static void btnNewChar_Right()
        {
            long spriteCount;

            if (GameState.NewCnarGender == (long)Sex.Male)
            {
                spriteCount = Data.Job[(int)GameState.NewCharJob].MaleSprite;
            }
            else
            {
                spriteCount = Data.Job[(int)GameState.NewCharJob].FemaleSprite;
            }

            if (GameState.NewCharSprite >= spriteCount)
            {
                GameState.NewCharSprite = 1L;
            }
            else
            {
                GameState.NewCharSprite = GameState.NewCharSprite + 1L;
            }
        }

        public static void chkNewChar_Male()
        {
            GameState.NewCharSprite = 1L;
            GameState.NewCnarGender = (long)Sex.Male;
            if (Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkMale")].Value == 0L)
            {
                Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkFemale")].Value = 0L;
                Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkMale")].Value = 1L;
            }
        }

        public static void chkNewChar_Female()
        {
            GameState.NewCharSprite = 1L;
            GameState.NewCnarGender = (long)Sex.Female;
            if (Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkFemale")].Value == 0L)
            {
                Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkFemale")].Value = 1L;
                Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkMale")].Value = 0L;
            }
        }

        public static void btnNewChar_Cancel()
        {
            Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "txtName")].Text = "";
            Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkMale")].Value = 0L;
            Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkFemale")].Value = 0L;
            GameState.NewCharSprite = 1L;
            GameState.NewCnarGender = (long)Sex.Male;
            HideWindows();
            ShowWindow(GetWindowIndex("winJobs"));
        }

        public static void btnNewChar_Accept()
        {
            string name;
            name = FilterUnsupportedCharacters(Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "txtName")].Text, Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "txtName")].Font);
            HideWindows();
            GameLogic.AddChar(name, (int)GameState.NewCnarGender, (int)GameState.NewCharJob, (int)GameState.NewCharSprite);
        }

        // #####################
        // ## Dialogue Window ##
        // #####################
        public static void btnDialogue_Close()
        {
            if (GameState.diaStyle == (int)DialogueStyle.Okay)
            {
                GameLogic.DialogueHandler(1L);
            }
            else if (GameState.diaStyle == (int)DialogueStyle.YesNo)
            {
                GameLogic.DialogueHandler(3L);
            }
        }

        // ###############
        // ## Inventory ##
        // ###############
        public static void Inventory_MouseDown()
        {
            long invNum;
            long winIndex;
            long I;

            if (Trade.InTrade == 1)
                return;

            // is there an item?
            invNum = General.IsInv(Windows[GetWindowIndex("winInventory")].Left, Windows[GetWindowIndex("winInventory")].Top);

            if (invNum >= 0L)
            {
                // drag it
                ref var withBlock = ref DragBox;
                withBlock.Type = DraggablePartType.Item;
                withBlock.Value = (long)GetPlayerInv(GameState.MyIndex, (int)invNum);
                withBlock.Origin = PartOrigin.Inventory;
                withBlock.Slot = invNum;

                winIndex = GetWindowIndex("winDragBox");
                {
                    var withBlock1 = Windows[winIndex];
                    withBlock1.Left = GameState.CurMouseX;
                    withBlock1.Top = GameState.CurMouseY;
                    withBlock1.MovedX = GameState.CurMouseX - withBlock1.Left;
                    withBlock1.MovedY = GameState.CurMouseY - withBlock1.Top;
                }

                ShowWindow(winIndex, resetPosition: false);

                // stop dragging inventory
                Windows[GetWindowIndex("winInventory")].State = ControlState.Normal;
            }

            Inventory_MouseMove();
        }

        public static void Inventory_DoubleClick()
        {
            long invNum;
            long i;

            invNum = General.IsInv(Windows[GetWindowIndex("winInventory")].Left, Windows[GetWindowIndex("winInventory")].Top);

            if (invNum >= 0L)
            {
                if (GameState.InBank)
                {
                    Bank.DepositItem((int)invNum, GetPlayerInvValue(GameState.MyIndex, (int)invNum));
                    return;
                }

                if (GameState.InShop >= 0)
                {
                    Shop.SellItem((int)invNum);
                    return;
                }

                // exit out if we're offering that item
                if (Trade.InTrade >= 0)
                {
                    for (i = 0L; i < Constant.MAX_INV; i++)
                    {
                        if (Data.TradeYourOffer[(int)i].Num == invNum)
                        {
                            // is currency?
                            if (Core.Data.Item[GetPlayerInv(GameState.MyIndex, (int)Data.TradeYourOffer[(int)i].Num)].Type == (byte)ItemCategory.Currency)
                            {
                                // only exit out if we're offering all of it
                                if (Data.TradeYourOffer[(int)i].Value == GetPlayerInvValue(GameState.MyIndex, (int)Data.TradeYourOffer[(int)i].Num))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    // currency handler
                    if (Core.Data.Item[GetPlayerInv(GameState.MyIndex, (int)invNum)].Type == (byte)ItemCategory.Currency)
                    {
                        GameLogic.Dialogue("Select Amount", "Please choose how many to offer.", "", (byte)DialogueType.TradeAmount, (byte)DialogueStyle.Input, invNum);
                        return;
                    }

                    // trade the normal item
                    Trade.TradeItem((int)invNum, 0);
                    return;
                }
            }

            NetworkSend.SendUseItem((int)invNum);

            Inventory_MouseMove();
        }

        public static void Inventory_MouseMove()
        {
            long itemNum;
            long x;
            long y;
            long i;

            // exit out early if dragging
            if (DragBox.Type != DraggablePartType.None)
                return;

            itemNum = General.IsInv(Windows[GetWindowIndex("winInventory")].Left, Windows[GetWindowIndex("winInventory")].Top);
            if (itemNum >= 0L)
            {
                // exit out if we're offering that item
                if (Trade.InTrade >= 0)
                {
                    for (i = 0L; i < Constant.MAX_INV; i++)
                    {
                        if (Data.TradeYourOffer[(int)i].Num == itemNum)
                        {
                            // is currency?
                            if (Core.Data.Item[GetPlayerInv(GameState.MyIndex, (int)Data.TradeYourOffer[(int)i].Num)].Type == (byte)ItemCategory.Currency)
                            {
                                // only exit out if we're offering all of it
                                if (Data.TradeYourOffer[(int)i].Value == GetPlayerInvValue(GameState.MyIndex, (int)Data.TradeYourOffer[(int)i].Num))
                                {
                                    return;
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }

                // make sure we're not dragging the item
                if (DragBox.Type == DraggablePartType.Item & DragBox.Value == itemNum)
                    return;

                // calc position
                x = Windows[GetWindowIndex("winInventory")].Left - Windows[GetWindowIndex("winDescription")].Width;
                y = Windows[GetWindowIndex("winInventory")].Top - 6L;

                // offscreen?
                if (x < 0L)
                {
                    // switch to right
                    x = Windows[GetWindowIndex("winInventory")].Left + Windows[GetWindowIndex("winInventory")].Width;
                }

                // go go go
                GameLogic.ShowInvDesc(x, y, itemNum);
            }
            else
            {
                Windows[GetWindowIndex("winDescription")].Visible = false;
            }
        }

        // ###############
        // ##    Bank   ##
        // ###############
        public static void btnMenu_Bank()
        {
            if (Windows[GetWindowIndex("winBank")].Visible == true)
            {
                Bank.CloseBank();
            }
        }

        public static void Bank_MouseMove()
        {
            long itemNum;
            long x;
            long y;
            long i;

            // exit out early if dragging
            if (DragBox.Type != DraggablePartType.None)
                return;

            itemNum = General.IsBank(Windows[GetWindowIndex("winBank")].Left, Windows[GetWindowIndex("winBank")].Top);

            if (itemNum >= 0L)
            {
                // make sure we're not dragging the item
                if (DragBox.Type == DraggablePartType.Item & DragBox.Value == itemNum)
                    return;

                // calc position
                x = Windows[GetWindowIndex("winBank")].Left - Windows[GetWindowIndex("winDescription")].Width;
                y = Windows[GetWindowIndex("winBank")].Top - 6L;

                // offscreen?
                if (x < 0L)
                {
                    // switch to right
                    x = Windows[GetWindowIndex("winBank")].Left + Windows[GetWindowIndex("winBank")].Width;
                }

                GameLogic.ShowItemDesc(x, y, (long)GetBank(GameState.MyIndex, (byte)itemNum));
            }
            else
            {
                Windows[GetWindowIndex("winDescription")].Visible = false;
            }
        }

        public static void Bank_MouseDown()
        {
            long bankSlot;
            long winIndex;
            long i;

            // is there an item?
            bankSlot = General.IsBank(Windows[GetWindowIndex("winBank")].Left, Windows[GetWindowIndex("winBank")].Top);

            if (bankSlot >= 0L)
            {
                // exit out if we're offering that item

                // drag it
                ref var withBlock = ref DragBox;
                withBlock.Type = DraggablePartType.Item;
                withBlock.Value = (long)GetBank(GameState.MyIndex, (int)bankSlot);
                withBlock.Origin = PartOrigin.Bank;

                withBlock.Slot = bankSlot;

                winIndex = GetWindowIndex("winDragBox");
                {
                    var withBlock1 = Windows[winIndex];
                    withBlock1.Left = GameState.CurMouseX;
                    withBlock1.Top = GameState.CurMouseY;
                    withBlock1.MovedX = GameState.CurMouseX - withBlock1.Left;
                    withBlock1.MovedY = GameState.CurMouseY - withBlock1.Top;
                }

                ShowWindow(winIndex, resetPosition: false);

                // stop dragging inventory
                Windows[GetWindowIndex("winBank")].State = ControlState.Normal;
            }

            Bank_MouseMove();
        }

        public static void Bank_DoubleClick()
        {
            long bankSlot;
            long winIndex;
            long i;

            // is there an item?
            bankSlot = General.IsBank(Windows[GetWindowIndex("winBank")].Left, Windows[GetWindowIndex("winBank")].Top);

            if (bankSlot >= 0L)
            {
                Bank.WithdrawItem((byte)bankSlot, GetBankValue(GameState.MyIndex, (int)bankSlot));
                return;
            }

            Bank_MouseMove();
        }

        // ##############
        // ## Drag Box ##
        // ##############
        public static void DragBox_OnDraw()
        {
            long xO;
            long yO;
            long texNum;
            long winIndex;

            winIndex = GetWindowIndex("winDragBox");
            xO = Windows[winIndex].Left;
            yO = Windows[winIndex].Top;

            if (DragBox.Type == DraggablePartType.None)
                return;

            // get texture num
            {
                ref var withBlock = ref DragBox;
                switch (withBlock.Type)
                {
                    case DraggablePartType.Item:
                        {
                            if (withBlock.Value >= 0)
                            {
                                texNum = Core.Data.Item[(int)withBlock.Value].Icon;
                                string argpath = System.IO.Path.Combine(Path.Items, texNum.ToString());
                                GameClient.RenderTexture(ref argpath, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32);
                            }

                            break;
                        }

                    case DraggablePartType.Skill:
                        {
                            if (withBlock.Value >= 0)
                            {
                                texNum = Data.Skill[(int)withBlock.Value].Icon;
                                string argpath1 = System.IO.Path.Combine(Path.Skills, texNum.ToString());
                                GameClient.RenderTexture(ref argpath1, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32);
                            }

                            break;
                        }
                }
            }
        }

        public static void DragBox_Check()
        {
            long winIndex;
            long i;
            var curWindow = default(long);
            long curControl;
            Core.Type.Rect tmpRec;

            winIndex = GetWindowIndex("winDragBox");

            if (DragBox.Type == DraggablePartType.None)
                return;

            // check for other windows
            var loopTo = Gui.Windows.Count;
            for (i = 1L; i < loopTo; i++)
            {
                {
                    var withBlock = Windows[i];
                    if (withBlock.Visible == true)
                    {
                        // can't drag to self
                        if (withBlock.Name != "winDragBox")
                        {
                            if (GameState.CurMouseX >= withBlock.Left & GameState.CurMouseX <= withBlock.Left + withBlock.Width)
                            {
                                if (GameState.CurMouseY >= withBlock.Top & GameState.CurMouseY <= withBlock.Top + withBlock.Height)
                                {
                                    if (curWindow == 0L)
                                        curWindow = i;

                                    if (withBlock.zOrder > Windows[curWindow].zOrder)
                                        curWindow = i;
                                }
                            }
                        }
                    }
                }
            }

            // we have a window - check if we can drop
            if (curWindow > 0)
            {
                switch (Windows[curWindow].Name ?? "")
                {
                    case "winBank":
                        {
                            if (DragBox.Origin == PartOrigin.Bank)
                            {
                                if (DragBox.Type == DraggablePartType.Item)
                                {
                                    // find the slot to switch with
                                    for (i = 0L; i <= Constant.MAX_BANK; i++)
                                    {
                                        tmpRec.Top = Windows[curWindow].Top + GameState.BankTop + (GameState.BankOffsetY + 32L) * (i / GameState.BankColumns);
                                        tmpRec.Bottom = tmpRec.Top + 32d;
                                        tmpRec.Left = Windows[curWindow].Left + GameState.BankLeft + (GameState.BankOffsetX + 32L) * (i % GameState.BankColumns);
                                        tmpRec.Right = tmpRec.Left + 32d;

                                        if (GameState.CurMouseX >= tmpRec.Left & GameState.CurMouseX <= tmpRec.Right)
                                        {
                                            if (GameState.CurMouseY >= tmpRec.Top & GameState.CurMouseY <= tmpRec.Bottom)
                                            {
                                                // switch the slots
                                                if (DragBox.Slot != i)
                                                {
                                                    Bank.ChangeBankSlots((int)DragBox.Slot, (int)i);
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (DragBox.Origin == PartOrigin.Inventory)
                            {
                                if (DragBox.Type == DraggablePartType.Item)
                                {

                                    if (Core.Data.Item[GetPlayerInv(GameState.MyIndex, (int)DragBox.Slot)].Type != (byte)ItemCategory.Currency)
                                    {
                                        Bank.DepositItem((int)DragBox.Slot, 1);
                                    }
                                    else
                                    {
                                        GameLogic.Dialogue("Deposit Item", "Enter the deposit quantity.", "", (byte)DialogueType.DepositItem, (byte)DialogueStyle.Input, DragBox.Slot);
                                    }

                                }
                            }

                            break;
                        }

                    case "winInventory":
                        {
                            if (DragBox.Origin == PartOrigin.Inventory)
                            {
                                // it's from the inventory!
                                if (DragBox.Type == DraggablePartType.Item)
                                {
                                    // find the slot to switch with
                                    for (i = 0L; i < Constant.MAX_INV; i++)
                                    {
                                        tmpRec.Top = Windows[curWindow].Top + GameState.InvTop + (GameState.InvOffsetY + 32L) * (i / GameState.InvColumns);
                                        tmpRec.Bottom = tmpRec.Top + 32d;
                                        tmpRec.Left = Windows[curWindow].Left + GameState.InvLeft + (GameState.InvOffsetX + 32L) * (i % GameState.InvColumns);
                                        tmpRec.Right = tmpRec.Left + 32d;

                                        if (GameState.CurMouseX >= tmpRec.Left & GameState.CurMouseX <= tmpRec.Right)
                                        {
                                            if (GameState.CurMouseY >= tmpRec.Top & GameState.CurMouseY <= tmpRec.Bottom)
                                            {
                                                // switch the slots
                                                if (DragBox.Slot != i)
                                                    NetworkSend.SendChangeInvSlots((int)DragBox.Slot, (int)i);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            if (DragBox.Origin == PartOrigin.Bank)
                            {
                                if (DragBox.Type == DraggablePartType.Item)
                                {

                                    if (Core.Data.Item[GetBank(GameState.MyIndex, (byte)DragBox.Slot)].Type != (byte)ItemCategory.Currency)
                                    {
                                        Bank.WithdrawItem((byte)DragBox.Slot, 0);
                                    }
                                    else
                                    {
                                        GameLogic.Dialogue("Withdraw Item", "Enter the amount you wish to withdraw.", "", (byte)DialogueType.WithdrawItem, (byte)DialogueStyle.Input, DragBox.Slot);
                                    }

                                }
                            }

                            break;
                        }

                    case "winSkills":
                        {
                            if (DragBox.Origin == PartOrigin.SkillTree)
                            {
                                if (DragBox.Type == DraggablePartType.Skill)
                                {
                                    // find the slot to switch with
                                    for (i = 0L; i < Constant.MAX_PLAYER_SKILLS; i++)
                                    {
                                        tmpRec.Top = Windows[curWindow].Top + GameState.SkillTop + (GameState.SkillOffsetY + 32L) * (i / GameState.SkillColumns);
                                        tmpRec.Bottom = tmpRec.Top + 32d;
                                        tmpRec.Left = Windows[curWindow].Left + GameState.SkillLeft + (GameState.SkillOffsetX + 32L) * (i % GameState.SkillColumns);
                                        tmpRec.Right = tmpRec.Left + 32d;

                                        if (GameState.CurMouseX >= tmpRec.Left & GameState.CurMouseX <= tmpRec.Right)
                                        {
                                            if (GameState.CurMouseY >= tmpRec.Top & GameState.CurMouseY <= tmpRec.Bottom)
                                            {
                                                // switch the slots
                                                if (DragBox.Slot != i)
                                                    NetworkSend.SendChangeSkillSlots((int)DragBox.Slot, (int)i);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                        }

                    case "winHotbar":
                        {
                            if (DragBox.Origin != PartOrigin.None)
                            {
                                if (DragBox.Type != DraggablePartType.None)
                                {
                                    // find the slot
                                    for (i = 0L; i < Constant.MAX_HOTBAR; i++)
                                    {
                                        tmpRec.Top = Windows[curWindow].Top + GameState.HotbarTop;
                                        tmpRec.Bottom = tmpRec.Top + 32d;
                                        tmpRec.Left = Windows[curWindow].Left + GameState.HotbarLeft + i * GameState.HotbarOffsetX;
                                        tmpRec.Right = tmpRec.Left + 32d;

                                        if (GameState.CurMouseX >= tmpRec.Left & GameState.CurMouseX <= tmpRec.Right)
                                        {
                                            if (GameState.CurMouseY >= tmpRec.Top & GameState.CurMouseY <= tmpRec.Bottom)
                                            {
                                                // set the Hotbar slot
                                                if (DragBox.Origin != PartOrigin.Hotbar)
                                                {
                                                    if (DragBox.Type == DraggablePartType.Item)
                                                    {
                                                        NetworkSend.SendSetHotbarSlot((int)PartOrigin.Inventory, (int)i, (int)DragBox.Slot, (int)DragBox.Value);
                                                    }
                                                    else if (DragBox.Type == DraggablePartType.Skill)
                                                    {
                                                        NetworkSend.SendSetHotbarSlot((int)PartOrigin.SkillTree, (int)i, (int)DragBox.Slot, (int)DragBox.Value);
                                                    }
                                                }
                                                else if (DragBox.Slot != i)
                                                    NetworkSend.SendSetHotbarSlot((int)PartOrigin.Hotbar, (int)i, (int)DragBox.Slot, (int)DragBox.Value);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                        }
                }
            }
            else
            {
                // no windows found - dropping on bare map
                switch (DragBox.Origin)
                {
                    case PartOrigin.Inventory:
                        {
                            if (Core.Data.Item[GetPlayerInv(GameState.MyIndex, (int)DragBox.Slot)].Type != (byte)ItemCategory.Currency)
                            {
                                NetworkSend.SendDropItem((int)DragBox.Slot, GetPlayerInv(GameState.MyIndex, (int)DragBox.Slot));
                            }
                            else
                            {
                                GameLogic.Dialogue("Drop Item", "Please choose how many to drop.", "", (byte)DialogueType.DropItem, (byte)DialogueStyle.Input, DragBox.Slot);
                            }

                            break;
                        }

                    case PartOrigin.SkillTree:
                        {
                            NetworkSend.ForgetSkill((int)DragBox.Slot);
                            break;
                        }

                    case PartOrigin.Hotbar:
                        {
                            NetworkSend.SendSetHotbarSlot((int)DragBox.Origin, (int)DragBox.Slot, (int)DragBox.Slot, 0);
                            break;
                        }
                }
            }

            // close window
            HideWindow(winIndex);

            {
                ref var withBlock1 = ref DragBox;
                withBlock1.Type = DraggablePartType.None;
                withBlock1.Slot = 0L;
                withBlock1.Origin = PartOrigin.None;
                withBlock1.Value = 0L;
            }
        }

        // ############
        // ## Skills ##
        // ############
        public static void Skills_MouseDown()
        {
            long slotNum;
            long winIndex;

            // is there an item?
            slotNum = General.IsSkill(Windows[GetWindowIndex("winSkills")].Left, Windows[GetWindowIndex("winSkills")].Top);

            if (slotNum >= 0)
            {
                ref var withBlock = ref DragBox;
                withBlock.Type = DraggablePartType.Skill;
                withBlock.Value = (long)Core.Data.Player[GameState.MyIndex].Skill[(int)slotNum].Num;
                withBlock.Origin = PartOrigin.SkillTree;
                withBlock.Slot = slotNum;
                
                winIndex = GetWindowIndex("winDragBox");
                {
                    var withBlock1 = Windows[winIndex];
                    withBlock1.Left = GameState.CurMouseX;
                    withBlock1.Top = GameState.CurMouseY;
                    withBlock1.MovedX = GameState.CurMouseX - withBlock1.Left;
                    withBlock1.MovedY = GameState.CurMouseY - withBlock1.Top;
                }

                ShowWindow(winIndex, resetPosition: false);

                // stop dragging inventory
                Windows[GetWindowIndex("winSkills")].State = ControlState.Normal;
            }

            Skills_MouseMove();
        }

        public static void Skills_DoubleClick()
        {
            long slotNum;

            slotNum = General.IsSkill(Windows[GetWindowIndex("winSkills")].Left, Windows[GetWindowIndex("winSkills")].Top);

            if (slotNum >= 0L)
            {
                Player.PlayerCastSkill((int)slotNum);
            }

            Skills_MouseMove();
        }

        public static void Skills_MouseMove()
        {
            long slotNum;
            long x;
            long y;

            // exit out early if dragging
            if (DragBox.Type != DraggablePartType.None)
                return;

            slotNum = General.IsSkill(Windows[GetWindowIndex("winSkills")].Left, Windows[GetWindowIndex("winSkills")].Top);

            if (slotNum >= 0L)
            {
                // make sure we're not dragging the item
                if (DragBox.Type == DraggablePartType.Item & DragBox.Value == slotNum)
                    return;

                // calc position
                x = Windows[GetWindowIndex("winSkills")].Left - Windows[GetWindowIndex("winDescription")].Width;
                y = Windows[GetWindowIndex("winSkills")].Top - 6L;

                // offscreen?
                if (x < 0L)
                {
                    // switch to right
                    x = Windows[GetWindowIndex("winSkills")].Left + Windows[GetWindowIndex("winSkills")].Width;
                }

                // go go go
                GameLogic.ShowSkillDesc(x, y, (long)GetPlayerSkill(GameState.MyIndex, (int)slotNum), slotNum);
            }
            else
            {
                Windows[GetWindowIndex("winDescription")].Visible = false;
            }
        }

        // ############
        // ## Hotbar ##
        // ############=
        public static void Hotbar_MouseDown()
        {
            long slotNum;
            long winIndex;

            // is there an item?
            slotNum = GameLogic.IsHotbar(Windows[GetWindowIndex("winHotbar")].Left, Windows[GetWindowIndex("winHotbar")].Top);

            if (slotNum >= 0L)
            {
                ref var withBlock = ref DragBox;
                if (Core.Data.Player[GameState.MyIndex].Hotbar[(int)slotNum].SlotType == 1) // inventory
                {
                    withBlock.Type = (DraggablePartType)PartOrigin.Inventory;
                }
                else if (Core.Data.Player[GameState.MyIndex].Hotbar[(int)slotNum].SlotType == 2) // Skill
                {
                    withBlock.Type = (DraggablePartType)PartOrigin.SkillTree;
                }
                withBlock.Value = (long)Core.Data.Player[GameState.MyIndex].Hotbar[(int)slotNum].Slot;
                withBlock.Origin = PartOrigin.Hotbar;
                withBlock.Slot = slotNum;
                
                winIndex = GetWindowIndex("winDragBox");
                {
                    var withBlock1 = Windows[winIndex];
                    withBlock1.Left = GameState.CurMouseX;
                    withBlock1.Top = GameState.CurMouseY;
                    withBlock1.MovedX = GameState.CurMouseX - withBlock1.Left;
                    withBlock1.MovedY = GameState.CurMouseY - withBlock1.Top;
                }
                ShowWindow(winIndex, resetPosition: false);

                // stop dragging inventory
                Windows[GetWindowIndex("winHotbar")].State = ControlState.Normal;
            }

            Hotbar_MouseMove();
        }

        public static void Hotbar_DoubleClick()
        {
            long slotNum;

            slotNum = GameLogic.IsHotbar(Windows[GetWindowIndex("winHotbar")].Left, Windows[GetWindowIndex("winHotbar")].Top);

            if (slotNum >= 0L)
            {
                NetworkSend.SendUseHotbarSlot((int)slotNum);
            }

            Hotbar_MouseMove();
        }

        public static void Hotbar_MouseMove()
        {
            long slotNum;
            long x;
            long y;

            // exit out early if dragging
            if (DragBox.Type != (int)PartOrigin.None)
                return;

            slotNum = GameLogic.IsHotbar(Windows[GetWindowIndex("winHotbar")].Left, Windows[GetWindowIndex("winHotbar")].Top);

            if (slotNum >= 0L)
            {
                // make sure we're not dragging the item
                if (DragBox.Origin == PartOrigin.Hotbar & DragBox.Slot == slotNum)
                    return;

                // calc position
                x = Windows[GetWindowIndex("winHotbar")].Left - Windows[GetWindowIndex("winDescription")].Width;
                y = Windows[GetWindowIndex("winHotbar")].Top - 6L;

                // offscreen?
                if (x < 0L)
                {
                    // switch to right
                    x = Windows[GetWindowIndex("winHotbar")].Left + Windows[GetWindowIndex("winHotbar")].Width;
                }

                // go go go
                switch (Core.Data.Player[GameState.MyIndex].Hotbar[(int)slotNum].SlotType)
                {
                    case 1: // inventory
                        {
                            GameLogic.ShowItemDesc(x, y, (long)Core.Data.Player[GameState.MyIndex].Hotbar[(int)slotNum].Slot);
                            break;
                        }
                    case 2: // skill
                        {
                            GameLogic.ShowSkillDesc(x, y, (long)Core.Data.Player[GameState.MyIndex].Hotbar[(int)slotNum].Slot, 0L);
                            break;
                        }
                }
            }
            else
            {
                Windows[GetWindowIndex("winDescription")].Visible = false;
            }
        }

        public static void Dialogue_Okay()
        {
            GameLogic.DialogueHandler(1L);
        }

        public static void Dialogue_Yes()
        {
            GameLogic.DialogueHandler(2L);
        }

        public static void Dialogue_No()
        {
            GameLogic.DialogueHandler(3L);
        }

        public static void UpdateStats_UI()
        {
            // set the bar labels
            {
                var withBlock = Windows[GetWindowIndex("winBars")];
                withBlock.Controls[GetControlIndex("winBars", "lblHP")].Text = GetPlayerVital(GameState.MyIndex, Core.Vital.Health) + "/" + GetPlayerMaxVital(GameState.MyIndex, Core.Vital.Health);
                withBlock.Controls[GetControlIndex("winBars", "lblMP")].Text = GetPlayerVital(GameState.MyIndex, Core.Vital.Stamina) + "/" + GetPlayerMaxVital(GameState.MyIndex, Core.Vital.Stamina);
                withBlock.Controls[GetControlIndex("winBars", "lblEXP")].Text = GetPlayerExp(GameState.MyIndex) + "/" + GameState.NextlevelExp;
            }

            // update character screen
            {
                var withBlock1 = Windows[GetWindowIndex("winCharacter")];
                withBlock1.Controls[GetControlIndex("winCharacter", "lblHealth")].Text = "Health";
                withBlock1.Controls[GetControlIndex("winCharacter", "lblSpirit")].Text = "Spirit";
                withBlock1.Controls[GetControlIndex("winCharacter", "lblExperience")].Text = "Exp";
                withBlock1.Controls[GetControlIndex("winCharacter", "lblHealth2")].Text = GetPlayerVital(GameState.MyIndex, Core.Vital.Health) + "/" + GetPlayerMaxVital(GameState.MyIndex, Core.Vital.Health);
                withBlock1.Controls[GetControlIndex("winCharacter", "lblSpirit2")].Text = GetPlayerVital(GameState.MyIndex, Core.Vital.Stamina) + "/" + GetPlayerMaxVital(GameState.MyIndex, Core.Vital.Stamina);
                withBlock1.Controls[GetControlIndex("winCharacter", "lblExperience2")].Text = Core.Data.Player[GameState.MyIndex].Exp + "/" + GameState.NextlevelExp;

            }
        }

        // ###############
        // ## Character ##
        // ###############
        public static void DrawCharacter()
        {
            long xO;
            long yO;
            long Width;
            long Height;
            long i;
            long sprite;
            long itemNum;
            var itemIcon = default(long);

            if (GameState.MyIndex < 0| GameState.MyIndex > Constant.MAX_PLAYERS)
                return;

            xO = Windows[GetWindowIndex("winCharacter")].Left;
            yO = Windows[GetWindowIndex("winCharacter")].Top;

            // Render bottom
            string argpath = System.IO.Path.Combine(Path.Gui, "37");
            GameClient.RenderTexture(ref argpath, (int)(xO + 4L), (int)(yO + 314L), 0, 0, 40, 38, 40, 38);
            string argpath1 = System.IO.Path.Combine(Path.Gui, "37");
            GameClient.RenderTexture(ref argpath1, (int)(xO + 44L), (int)(yO + 314L), 0, 0, 40, 38, 40, 38);
            string argpath2 = System.IO.Path.Combine(Path.Gui, "37");
            GameClient.RenderTexture(ref argpath2, (int)(xO + 84L), (int)(yO + 314L), 0, 0, 40, 38, 40, 38);
            string argpath3 = System.IO.Path.Combine(Path.Gui, "37");
            GameClient.RenderTexture(ref argpath3, (int)(xO + 124L), (int)(yO + 314L), 0, 0, 46, 38, 46, 38);

            // render top wood
            string argpath4 = System.IO.Path.Combine(Path.Gui, "1");
            GameClient.RenderTexture(ref argpath4, (int)(xO + 4L), (int)(yO + 23L), 100, 100, 166, 291, 166, 291);

            // loop through equipment
            for (i = 0L; i < Enum.GetValues(typeof(Equipment)).Length; i++)
            {
                itemNum = (long)GetPlayerEquipment(GameState.MyIndex, (Equipment)i);

                if (itemNum >= 0L)
                {
                    itemIcon = Core.Data.Item[(int)itemNum].Icon;

                    if (itemIcon > 0 && itemIcon < GameState.NumItems)
                    {
                        yO = Windows[GetWindowIndex("winCharacter")].Top + GameState.EqTop;
                        xO = Windows[GetWindowIndex("winCharacter")].Left + GameState.EqLeft + (GameState.EqOffsetX + 32L) * (i % GameState.EqColumns);
                        string argpath5 = System.IO.Path.Combine(Path.Items, itemIcon.ToString());
                        GameClient.RenderTexture(ref argpath5, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32);
                    }
                }
            }
        }

        public static void Character_DoubleClick()
        {
            long itemNum;

            itemNum = General.IsEq(Windows[GetWindowIndex("winCharacter")].Left, Windows[GetWindowIndex("winCharacter")].Top);

            if (itemNum >= 0L)
            {
                NetworkSend.SendUnequip((int)itemNum);
            }

            Character_MouseMove();
        }

        public static void Character_MouseMove()
        {
            long itemNum;
            long x;
            long y;

            // exit out early if dragging
            if (DragBox.Type != DraggablePartType.None)
                return;

            itemNum = General.IsEq(Windows[GetWindowIndex("winCharacter")].Left, Windows[GetWindowIndex("winCharacter")].Top);

            if (itemNum >= 0L)
            {
                // calc position
                x = Windows[GetWindowIndex("winCharacter")].Left - Windows[GetWindowIndex("winDescription")].Width;
                y = Windows[GetWindowIndex("winCharacter")].Top - 6L;

                // offscreen?
                if (x < 0L)
                {
                    // switch to right
                    x = Windows[GetWindowIndex("winCharacter")].Left + Windows[GetWindowIndex("winCharacter")].Width;
                }

                // go go go
                GameLogic.ShowEqDesc(x, y, itemNum);
            }
            else
            {
                Windows[GetWindowIndex("winDescription")].Visible = false;
            }
        }

        public static void Character_DbClick()
        {
            long itemNum;

            itemNum = General.IsEq(Windows[GetWindowIndex("winCharacter")].Left, Windows[GetWindowIndex("winCharacter")].Top);

            if (itemNum >= 0L)
            {
                NetworkSend.SendUnequip((int)itemNum);
            }

            Character_MouseMove();
        }

        public static void Character_SpendPoint1()
        {
            NetworkSend.SendTrainStat(0);
        }

        public static void Character_SpendPoint2()
        {
            NetworkSend.SendTrainStat(1);
        }

        public static void Character_SpendPoint3()
        {
            NetworkSend.SendTrainStat(2);
        }

        public static void Character_SpendPoint4()
        {
            NetworkSend.SendTrainStat(3);
        }

        public static void Character_SpendPoint5()
        {
            NetworkSend.SendTrainStat(4);
        }

        public static void DrawInventory()
        {
            long xO;
            long yO;
            long Width;
            long Height;
            long i;
            long y;
            long itemNum;
            long itemIcon;
            long x;
            long Top;
            long Left;
            string Amount;
            var Color = default(Color);
            var skipItem = default(bool);
            long amountModifier;
            long tmpItem;

            if (GameState.MyIndex < 0| GameState.MyIndex > Constant.MAX_PLAYERS)
                return;

            xO = Windows[GetWindowIndex("winInventory")].Left;
            yO = Windows[GetWindowIndex("winInventory")].Top;
            Width = Windows[GetWindowIndex("winInventory")].Width;
            Height = Windows[GetWindowIndex("winInventory")].Height;

            // render green
            string argpath = System.IO.Path.Combine(Path.Gui, 34.ToString());
            GameClient.RenderTexture(ref argpath, (int)(xO + 4L), (int)(yO + 23L), 0, 0, (int)(Width - 8L), (int)(Height - 27L), 4, 4);

            Width = 76L;
            Height = 76L;

            y = yO + 23L;
            // render grid - row
            for (i = 0L; i <= 3L; i++)
            {
                if (i == 3L)
                    Height = 38L;
                string argpath1 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath1, (int)(xO + 4L), (int)y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath2 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath2, (int)(xO + 80L), (int)y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath3 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath3, (int)(xO + 156L), (int)y, 0, 0, 42, (int)Height, 42, (int)Height);
                y = y + 76L;
            }

            // render bottom wood
            string argpath4 = System.IO.Path.Combine(Path.Gui, 1.ToString());
            GameClient.RenderTexture(ref argpath4, (int)(xO + 4L), (int)(yO + 289L), 100, 100, 194, 26, 194, 26);

            // actually draw the icons
            for (i = 0L; i < Constant.MAX_INV; i++)
            {
                itemNum = (long)GetPlayerInv(GameState.MyIndex, (int)i); 

                if (itemNum >= 0L & itemNum < Constant.MAX_ITEMS)
                {
                    Item.StreamItem((int)itemNum);

                    // not dragging?
                    if (!(DragBox.Origin == PartOrigin.Inventory & DragBox.Slot == i))
                    {
                        itemIcon = Core.Data.Item[(int)itemNum].Icon;

                        // exit out if we're offering item in a trade.
                        amountModifier = 0L;
                        if (Trade.InTrade >= 0)
                        {
                            for (x = 0L; x < Constant.MAX_INV; x++)
                            {
                                if (Data.TradeYourOffer[(int)x].Num >= 0)
                                { 
                                    tmpItem = (long)GetPlayerInv(GameState.MyIndex, (int)Data.TradeYourOffer[(int)x].Num);
                                    if (Data.TradeYourOffer[(int)x].Num == i)
                                    {
                                        // check if currency
                                        if (!(Core.Data.Item[(int)tmpItem].Type == (byte)ItemCategory.Currency))
                                        {
                                            // normal item, exit out
                                            skipItem = true;
                                        }
                                        // if amount = all currency, remove from inventory
                                        else if (Data.TradeYourOffer[(int)x].Value == GetPlayerInvValue(GameState.MyIndex, (int)i))
                                        {
                                            skipItem = true;
                                        }
                                        else
                                        {
                                            // not all, change modifier to show change in currency count
                                            amountModifier = Data.TradeYourOffer[(int)x].Value;
                                        }
                                    }
                                }
                            }
                        }

                        if (!skipItem)
                        {
                            if (itemIcon > 0L & itemIcon <= GameState.NumItems)
                            {
                                Top = yO + GameState.InvTop + (GameState.InvOffsetY + 32L) * (i / GameState.InvColumns);
                                Left = xO + GameState.InvLeft + (GameState.InvOffsetX + 32L) * (i % GameState.InvColumns);

                                // draw icon
                                string argpath5 = System.IO.Path.Combine(Path.Items, itemIcon.ToString());
                                GameClient.RenderTexture(ref argpath5, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);

                                // If item is a stack - draw the amount you have
                                if (GetPlayerInvValue(GameState.MyIndex, (int)i) > 1)
                                {
                                    y = Top + 20L;
                                    x = Left + 1L;
                                    Amount = (GetPlayerInvValue(GameState.MyIndex, (int)i) - amountModifier).ToString();

                                    // Draw currency but with k, m, b etc. using a convertion function
                                    if (Conversions.ToLong(Amount) < 1000000L)
                                    {
                                        Color = GameClient.QbColorToXnaColor((int)Core.Color.White);
                                    }
                                    else if (Conversions.ToLong(Amount) > 1000000L & Conversions.ToLong(Amount) < 10000000L)
                                    {
                                        Color = GameClient.QbColorToXnaColor((int)Core.Color.Yellow);
                                    }
                                    else if (Conversions.ToLong(Amount) > 10000000L)
                                    {
                                        Color = GameClient.QbColorToXnaColor((int)Core.Color.BrightGreen);
                                    }

                                    Text.RenderText(GameLogic.ConvertCurrency(Conversions.ToInteger(Amount)), (int)x, (int)y, Color, Color, Core.Font.Georgia);
                                }
                            }
                        }

                        // reset
                        skipItem = false;
                    }
                }
            }
        }

        // #################
        // ## Description ##
        // #################
        public static void Description_OnDraw()
        {
            long xO;
            long yO;
            long texNum;
            long y;
            long i;
            long count;

            // exit out if we don't have a num
            if (GameState.descItem == -1L | GameState.descType == 0)
                return;

            xO = Windows[GetWindowIndex("winDescription")].Left;
            yO = Windows[GetWindowIndex("winDescription")].Top;

            switch (GameState.descType)
            {
                case 1: // Inventory Item
                    {
                        texNum = Core.Data.Item[(int)GameState.descItem].Icon;

                        // render sprite
                        string argpath = System.IO.Path.Combine(Path.Items, texNum.ToString());
                        GameClient.RenderTexture(ref argpath, (int)(xO + 20L), (int)(yO + 34L), 0, 0, 64, 64, 32, 32);
                        break;
                    }

                case 2: // Skill Icon
                    {
                        texNum = Data.Skill[(int)GameState.descItem].Icon;

                        // render bar
                        {
                            var withBlock = Windows[GetWindowIndex("winDescription")].Controls[GetControlIndex("winDescription", "picBar")];
                            if (withBlock.Visible == true)
                            {
                                string argpath1 = System.IO.Path.Combine(Path.Gui, 45.ToString());
                                GameClient.RenderTexture(ref argpath1, (int)(xO + withBlock.Left), (int)(yO + withBlock.Top), 0, 12, (int)withBlock.Value, 12, (int)withBlock.Value, 12);
                            }
                        }

                        // render sprite
                        string argpath2 = System.IO.Path.Combine(Path.Skills, texNum.ToString());
                        GameClient.RenderTexture(ref argpath2, (int)(xO + 20L), (int)(yO + 34L), 0, 0, 64, 64, 32, 32);
                        break;
                    }
            }

            // render text array
            y = 18L;
            count = Information.UBound(GameState.Description);
            var loopTo = count;
            for (i = 0L; i < loopTo; i++)
            {
                Text.RenderText(GameState.Description[(int)i].Caption, (int)(xO + 140L - Text.GetTextWidth(GameState.Description[(int)i].Caption) / 2), (int)(yO + y), GameClient.ToXnaColor(GameState.Description[(int)i].Color), Color.Black);
                y = y + 12L;
            }
        }

        // Shop
        public static void btnShop_Close()
        {
            Shop.CloseShop();
        }

        public static void chkShopBuying()
        {
            {
                var withBlock = Windows[GetWindowIndex("winShop")];
                if (withBlock.Controls[GetControlIndex("winShop", "CheckboxBuying")].Value == 0L)
                {
                    withBlock.Controls[GetControlIndex("winShop", "CheckboxSelling")].Value = 0L;
                }
                else
                {
                    withBlock.Controls[GetControlIndex("winShop", "CheckboxSelling")].Value = 0L;
                    withBlock.Controls[GetControlIndex("winShop", "CheckboxBuying")].Value = 0L;
                    return;
                }
            }

            // show buy button, hide sell
            {
                var withBlock1 = Windows[GetWindowIndex("winShop")];
                withBlock1.Controls[GetControlIndex("winShop", "btnSell")].Visible = false;
                withBlock1.Controls[GetControlIndex("winShop", "btnBuy")].Visible = true;
            }

            // update the shop
            GameState.shopIsSelling = false;
            GameState.shopSelectedSlot = 0L;
            UpdateShop();
        }

        public static void chkShopSelling()
        {
            {
                var withBlock = Windows[GetWindowIndex("winShop")];
                if (withBlock.Controls[GetControlIndex("winShop", "CheckboxSelling")].Value == 0L)
                {
                    withBlock.Controls[GetControlIndex("winShop", "CheckboxBuying")].Value = 0L;
                }
                else
                {
                    withBlock.Controls[GetControlIndex("winShop", "CheckboxBuying")].Value = 0L;
                    withBlock.Controls[GetControlIndex("winShop", "CheckboxSelling")].Value = 0L;
                    return;
                }
            }

            // show sell button, hide buy
            {
                var withBlock1 = Windows[GetWindowIndex("winShop")];
                withBlock1.Controls[GetControlIndex("winShop", "btnBuy")].Visible = false;
                withBlock1.Controls[GetControlIndex("winShop", "btnSell")].Visible = true;
            }

            // update the shop
            GameState.shopIsSelling = true;
            GameState.shopSelectedSlot = 0L;
            UpdateShop();
        }

        public static void btnShopBuy()
        {
            Shop.BuyItem((int)GameState.shopSelectedSlot);
        }

        public static void btnShopSell()
        {
            Shop.SellItem((int)GameState.shopSelectedSlot);
        }

        public static void Shop_MouseDown()
        {
            long shopNum;

            // is there an item?
            shopNum = General.IsShop(Windows[GetWindowIndex("winShop")].Left, Windows[GetWindowIndex("winShop")].Top);
            if (shopNum >= 0L)
            {
                if (GameState.shopIsSelling)
                {
                    if (GetPlayerInv(GameState.MyIndex, (int)shopNum) >= 0)
                    {
                        // set the active slot
                        GameState.shopSelectedSlot = shopNum;
                        UpdateShop();
                    }
                }
                else
                {
                    if (Data.Shop[GameState.InShop].TradeItem[shopNum].Item >= 0)
                    {
                        // set the active slot
                        GameState.shopSelectedSlot = shopNum;
                        UpdateShop();
                    }
                }
            }
            Shop_MouseMove();
        }

        public static void Shop_MouseMove()
        {
            long shopSlot;
            long itemNum;
            long x;
            long y;

            if (GameState.InShop < 0 | GameState.InShop > Constant.MAX_SHOPS)
                return;

            shopSlot = General.IsShop(Windows[GetWindowIndex("winShop")].Left, Windows[GetWindowIndex("winShop")].Top);

            if (shopSlot >= 0L)
            {
                // calc position
                x = Windows[GetWindowIndex("winShop")].Left - Windows[GetWindowIndex("winDescription")].Width;
                y = Windows[GetWindowIndex("winShop")].Top - 6L;

                // offscreen?
                if (x < 0L)
                {
                    // switch to right
                    x = Windows[GetWindowIndex("winShop")].Left + Windows[GetWindowIndex("winShop")].Width;
                }

                // selling/buying
                if (!GameState.shopIsSelling)
                {
                    // get the itemnum
                    itemNum = Data.Shop[GameState.InShop].TradeItem[(int)shopSlot].Item;
                    if (itemNum == -1L)
                        return;
                    GameLogic.ShowShopDesc(x, y, itemNum);
                }
                else
                {
                    // get the itemnum
                    itemNum = (long)GetPlayerInv(GameState.MyIndex, (int)shopSlot);
                    if (itemNum == -1L)
                        return;
                    GameLogic.ShowShopDesc(x, y, itemNum);
                }
            }
            else
            {
                Windows[GetWindowIndex("winDescription")].Visible = false;
            }
        }

        public static void ResizeGUI()
        {
            long Top;

            return;

            // move Hotbar
            Windows[GetWindowIndex("winHotbar")].Left = GameState.ResolutionWidth - 432;

            // move chat
            Windows[GetWindowIndex("winChat")].Top = GameState.ResolutionHeight - 178;
            Windows[GetWindowIndex("winChatSmall")].Top = GameState.ResolutionHeight - 162;

            // move menu
            Windows[GetWindowIndex("winMenu")].Left = GameState.ResolutionWidth - 238;
            Windows[GetWindowIndex("winMenu")].Top = GameState.ResolutionHeight - 42;

            // loop through
            Top = -80;

            // re-size right-click background
            Windows[GetWindowIndex("winRightClickBG")].Width = GameState.ResolutionWidth;
            Windows[GetWindowIndex("winRightClickBG")].Height = GameState.ResolutionHeight;

            // re-size combo background
            Windows[GetWindowIndex("winComboMenuBG")].Width = GameState.ResolutionWidth;
            Windows[GetWindowIndex("winComboMenuBG")].Height = GameState.ResolutionHeight;
        }

        public static void DrawSkills()
        {
            long xO;
            long yO;
            long Width;
            long Height;
            long i;
            long y;
            var skillNum = default(long);
            long SkillPic;
            long x;
            long Top;
            long Left;

            if (GameState.MyIndex < 0| GameState.MyIndex > Constant.MAX_PLAYERS)
                return;

            xO = Windows[GetWindowIndex("winSkills")].Left;
            yO = Windows[GetWindowIndex("winSkills")].Top;

            Width = Windows[GetWindowIndex("winSkills")].Width;
            Height = Windows[GetWindowIndex("winSkills")].Height;

            // render green
            string argpath = System.IO.Path.Combine(Path.Gui, 34.ToString());
            GameClient.RenderTexture(ref argpath, (int)(xO + 4L), (int)(yO + 23L), 0, 0, (int)(Width - 8L), (int)(Height - 27L), 4, 4);

            Width = 76L;
            Height = 76L;

            y = yO + 23L;
            // render grid - row
            for (i = 0L; i <= 3L; i++)
            {
                if (i == 3L)
                    Height = 42L;
                string argpath1 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath1, (int)(xO + 4L), (int)y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath2 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath2, (int)(xO + 80L), (int)y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath3 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath3, (int)(xO + 156L), (int)y, 0, 0, 42, (int)Height, 42, (int)Height);
                y = y + 76L;
            }

            // actually draw the icons
            for (i = 0L; i < Constant.MAX_PLAYER_SKILLS; i++)
            {
                skillNum = (long)Core.Data.Player[GameState.MyIndex].Skill[(int)i].Num;
                if (skillNum >= 0L & skillNum < Constant.MAX_SKILLS)
                {
                    Database.StreamSkill((int)skillNum);

                    // not dragging?
                    if (!(DragBox.Origin == PartOrigin.SkillTree & DragBox.Slot == i))
                    {
                        SkillPic = Data.Skill[(int)skillNum].Icon;

                        if (SkillPic > 0L & SkillPic <= GameState.NumSkills)
                        {
                            Top = yO + GameState.SkillTop + (GameState.SkillOffsetY + 32L) * (i / GameState.SkillColumns);
                            Left = xO + GameState.SkillLeft + (GameState.SkillOffsetX + 32L) * (i % GameState.SkillColumns);

                            string argpath4 = System.IO.Path.Combine(Path.Skills, SkillPic.ToString());
                            GameClient.RenderTexture(ref argpath4, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);
                        }
                    }
                }
            }
        }

        // Options
        public static void btnOptions_Close()
        {
            HideWindow(GetWindowIndex("winOptions"));
            ShowWindow(GetWindowIndex("winEscMenu"));
        }

        public static void btnOptions_Confirm()
        {
            long i;
            long Value;
            long Width;
            long Height;
            var message = default(bool);
            string musicFile;

            // music
            Value = Windows[GetWindowIndex("winOptions")].Controls[GetControlIndex("winOptions", "chkMusic")].Value;
            if (Conversions.ToLong(SettingsManager.Instance.Music) != Value)
            {
                SettingsManager.Instance.Music = Conversions.ToBoolean(Value);

                // let them know
                if (Value == 0L)
                {
                    Text.AddText("Music turned off.", (int)Core.Color.BrightGreen);
                    Sound.StopMusic();
                }
                else
                {
                    Text.AddText("Music tured on.", (int)Core.Color.BrightGreen);
                    // play music
                    if (GameState.InGame)
                        musicFile = Data.MyMap.Music;
                    else
                        musicFile = Conversions.ToString(SettingsManager.Instance.Music);
                    if (!(musicFile == "None."))
                    {
                        Sound.PlayMusic(musicFile);
                    }
                    else
                    {
                        Sound.StopMusic();
                    }
                }
            }

            // sound
            Value = Windows[GetWindowIndex("winOptions")].Controls[GetControlIndex("winOptions", "chkSound")].Value;
            if (Conversions.ToLong(SettingsManager.Instance.Sound) != Value)
            {
                SettingsManager.Instance.Sound = Conversions.ToBoolean(Value);
                // let them know
                if (Value == 0L)
                {
                    Text.AddText("Sound turned off.", (int)Core.Color.BrightGreen);
                }
                else
                {
                    Text.AddText("Sound tured on.", (int)Core.Color.BrightGreen);
                }
            }

            // autotiles
            Value = Windows[GetWindowIndex("winOptions")].Controls[GetControlIndex("winOptions", "chkAutotile")].Value;
            if (Conversions.ToLong(SettingsManager.Instance.Autotile) != Value)
            {
                SettingsManager.Instance.Autotile = Conversions.ToBoolean(Value);
                // let them know
                if (Value == 0L)
                {
                    if (GameState.InGame)
                    {
                        Text.AddText("Autotiles turned off.", (int)Core.Color.BrightGreen);
                        Autotile.InitAutotiles();
                    }
                }
                else if (GameState.InGame)
                {
                    Text.AddText("Autotiles turned on.", (int)Core.Color.BrightGreen);
                    Autotile.InitAutotiles();
                }
            }

            // fullscreen
            Value = Windows[GetWindowIndex("winOptions")].Controls[GetControlIndex("winOptions", "chkFullscreen")].Value;
            if (Conversions.ToLong(SettingsManager.Instance.Fullscreen) != Value)
            {
                SettingsManager.Instance.Fullscreen = Conversions.ToBoolean(Value);
                message = true;
            }

            // resolution
            {
                var withBlock = Windows[GetWindowIndex("winOptions")].Controls[GetControlIndex("winOptions", "cmbRes")];
                if (withBlock.Value > 0L & withBlock.Value <= 13L)
                {
                    message = true;
                }
            }

            // save options
            SettingsManager.Save();

            // let them know
            if (GameState.InGame)
            {
                if (message)
                    Text.AddText("Some changes will take effect next time you load the game.", (int)Core.Color.BrightGreen);
            }

            // close
            btnOptions_Close();
        }

        // Right Click Menu
        public static void RightClick_Close()
        {
            // close all menus
            HideWindow(GetWindowIndex("winRightClickBG"));
            HideWindow(GetWindowIndex("winPlayerMenu"));
        }

        // Player Menu
        public static void PlayerMenu_Party()
        {
            RightClick_Close();
            Party.SendPartyRequest(GetPlayerName((int)GameState.PlayerMenuIndex));
        }

        public static void PlayerMenu_Trade()
        {
            RightClick_Close();
            Trade.SendTradeRequest(GetPlayerName((int)GameState.PlayerMenuIndex));
        }

        public static void PlayerMenu_Guild()
        {
            RightClick_Close();
            Text.AddText("System not yet in place.", (int)Core.Color.BrightRed);
        }

        public static void PlayerMenu_Player()
        {
            RightClick_Close();
            Text.AddText("System not yet in place.", (int)Core.Color.BrightRed);
        }

        public static void UpdateShop()
        {
            long i;
            long CostValue;

            if (GameState.InShop < 0)
                return;

            {
                var withBlock = Windows[GetWindowIndex("winShop")];
                // buying items
                if (!GameState.shopIsSelling)
                {
                    GameState.shopSelectedItem = Data.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].Item;
                    // labels
                    if (GameState.shopSelectedItem >= 0L)
                    {
                        withBlock.Controls[GetControlIndex("winShop", "lblName")].Text = Core.Data.Item[(int)GameState.shopSelectedItem].Name;
                        // check if it's gold
                        if (Data.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostItem == 0)
                        {
                            // it's gold
                            withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = Data.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostValue + "g";
                        }
                        // if it's one then just print the name
                        else if (Data.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostValue == 1)
                        {
                            withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = Core.Data.Item[Data.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostItem].Name;
                        }
                        else
                        {
                            withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = Data.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostValue + " " + Core.Data.Item[Data.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostItem].Name;
                        }

                        // draw the item
                        for (i = 0L; i <= 4L; i++)
                        {
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Image[(int)i] = Core.Data.Item[(int)GameState.shopSelectedItem].Icon;
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Texture[(int)i] = Path.Items;
                        }
                    }
                    else
                    {
                        withBlock.Controls[GetControlIndex("winShop", "lblName")].Text = "Empty Slot";
                        withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = "";

                        // draw the item
                        for (i = 0L; i <= 4L; i++)
                        {
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Image[(int)i] = 0L;
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Texture[(int)i] = null;
                        }
                    }
                }
                else
                {
                    GameState.shopSelectedItem = (long)GetPlayerInv(GameState.MyIndex, (int)GameState.shopSelectedSlot);
                    // labels
                    if (GameState.shopSelectedItem >= 0L)
                    {
                        withBlock.Controls[GetControlIndex("winShop", "lblName")].Text = Core.Data.Item[(int)GameState.shopSelectedItem].Name;
                        // calc cost
                        CostValue = (long)Math.Round(Core.Data.Item[(int)GameState.shopSelectedItem].Price / 100d * Data.Shop[GameState.InShop].BuyRate);
                        withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = CostValue + "g";

                        // draw the item
                        for (i = 0L; i <= 4L; i++)
                        {
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Image[(int)i] = Core.Data.Item[(int)GameState.shopSelectedItem].Icon;
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Texture[(int)i] = Path.Items;
                        }
                    }
                    else
                    {
                        withBlock.Controls[GetControlIndex("winShop", "lblName")].Text = "Empty Slot";
                        withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = "";

                        // draw the item
                        for (i = 0L; i <= 4L; i++)
                        {
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Image[(int)i] = 0L;
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Texture[(int)i] = null;
                        }
                    }
                }
            }
        }

        public static void UpdatePartyInterface()
        {
            long i;
            var image = new long[6];
            long x;
            long pIndex;
            var Height = default(long);
            long cIn;

            // unload it if we're not in a party
            if (Data.MyParty.Leader == 0)
            {
                HideWindow(GetWindowIndex("winParty"));
                return;
            }

            // load the window
            ShowWindow(GetWindowIndex("winParty"));

            // fill the controls
            {
                var withBlock = Windows[GetWindowIndex("winParty")];
                // clear controls first
                for (i = 0L; i <= 3L; i++)
                {
                    withBlock.Controls[GetControlIndex("winParty", "lblName" + i)].Text = "";
                    withBlock.Controls[GetControlIndex("winParty", "picEmptyBar_HP" + i)].Visible = false;
                    withBlock.Controls[GetControlIndex("winParty", "picEmptyBar_SP" + i)].Visible = false;
                    withBlock.Controls[GetControlIndex("winParty", "picBar_HP" + i)].Visible = false;
                    withBlock.Controls[GetControlIndex("winParty", "picBar_SP" + i)].Visible = false;
                    withBlock.Controls[GetControlIndex("winParty", "picShadow" + i)].Visible = false;
                    withBlock.Controls[GetControlIndex("winParty", "picChar" + i)].Visible = false;
                    withBlock.Controls[GetControlIndex("winParty", "picChar" + i)].Value = 0L;
                }

                // labels
                cIn = 0L;

                var loopTo = (long)Data.MyParty.MemberCount;
                for (i = 0L; i < loopTo; i++)
                {
                    // cache the index
                    pIndex = Data.MyParty.Member[(int)i];
                    if (pIndex > 0L)
                    {
                        if (pIndex != GameState.MyIndex)
                        {
                            if (IsPlaying((int)pIndex))
                            {
                                // name and level
                                withBlock.Controls[GetControlIndex("winParty", "lblName" + cIn)].Visible = true;
                                withBlock.Controls[GetControlIndex("winParty", "lblName" + cIn)].Text = GetPlayerName((int)pIndex);
                                // picture
                                withBlock.Controls[GetControlIndex("winParty", "picShadow" + cIn)].Visible = true;
                                withBlock.Controls[GetControlIndex("winParty", "picChar" + cIn)].Visible = true;
                                // store the player's index as a value for later use
                                withBlock.Controls[GetControlIndex("winParty", "picChar" + cIn)].Value = pIndex;
                                for (x = 0L; x <= 4L; x++)
                                {
                                    withBlock.Controls[GetControlIndex("winParty", "picChar" + cIn)].Image[(int)x] = GetPlayerSprite((int)pIndex);
                                    withBlock.Controls[GetControlIndex("winParty", "picChar" + cIn)].Texture[(int)x] = Path.Characters;
                                }
                                // bars
                                withBlock.Controls[GetControlIndex("winParty", "picEmptyBar_HP" + cIn)].Visible = true;
                                withBlock.Controls[GetControlIndex("winParty", "picEmptyBar_SP" + cIn)].Visible = true;
                                withBlock.Controls[GetControlIndex("winParty", "picBar_HP" + cIn)].Visible = true;
                                withBlock.Controls[GetControlIndex("winParty", "picBar_SP" + cIn)].Visible = true;
                                // increment control usage
                                cIn = cIn + 1L;
                            }
                        }
                    }
                }

                // update the bars
                GameLogic.UpdatePartyBars();

                // set the window size
                switch (Data.MyParty.MemberCount)
                {
                    case 2:
                        {
                            Height = 78L;
                            break;
                        }
                    case 3:
                        {
                            Height = 118L;
                            break;
                        }
                    case 4:
                        {
                            Height = 158L;
                            break;
                        }
                }
                withBlock.Height = Height;
            }
        }

        public static void DrawMenuBG()
        {
            // row 1
            string argpath = System.IO.Path.Combine(Path.Pictures, "1");
            GameClient.RenderTexture(ref argpath, GameState.ResolutionWidth - 512, GameState.ResolutionHeight - 512, 0, 0, 512, 512, 512, 512);
            string argpath1 = System.IO.Path.Combine(Path.Pictures, "2");
            GameClient.RenderTexture(ref argpath1, GameState.ResolutionWidth - 1024, GameState.ResolutionHeight - 512, 0, 0, 512, 512, 512, 512);
            string argpath2 = System.IO.Path.Combine(Path.Pictures, "3");
            GameClient.RenderTexture(ref argpath2, GameState.ResolutionWidth - 1536, GameState.ResolutionHeight - 512, 0, 0, 512, 512, 512, 512);
            string argpath3 = System.IO.Path.Combine(Path.Pictures, "4");
            GameClient.RenderTexture(ref argpath3, GameState.ResolutionWidth - 2048, GameState.ResolutionHeight - 512, 0, 0, 512, 512, 512, 512);

            // row 2
            string argpath4 = System.IO.Path.Combine(Path.Pictures, "5");
            GameClient.RenderTexture(ref argpath4, GameState.ResolutionWidth - 512, GameState.ResolutionHeight - 1024, 0, 0, 512, 512, 512, 512);
            string argpath5 = System.IO.Path.Combine(Path.Pictures, "6");
            GameClient.RenderTexture(ref argpath5, GameState.ResolutionWidth - 1024, GameState.ResolutionHeight - 1024, 0, 0, 512, 512, 512, 512);
            string argpath6 = System.IO.Path.Combine(Path.Pictures, "7");
            GameClient.RenderTexture(ref argpath6, GameState.ResolutionWidth - 1536, GameState.ResolutionHeight - 1024, 0, 0, 512, 512, 512, 512);
            string argpath7 = System.IO.Path.Combine(Path.Pictures, "8");
            GameClient.RenderTexture(ref argpath7, GameState.ResolutionWidth - 2048, GameState.ResolutionHeight - 1024, 0, 0, 512, 512, 512, 512);

            // row 3
            string argpath8 = System.IO.Path.Combine(Path.Pictures, "9");
            GameClient.RenderTexture(ref argpath8, GameState.ResolutionWidth - 512, GameState.ResolutionHeight - 1088, 0, 0, 512, 64, 512, 64);
            string argpath9 = System.IO.Path.Combine(Path.Pictures, "10");
            GameClient.RenderTexture(ref argpath9, GameState.ResolutionWidth - 1024, GameState.ResolutionHeight - 1088, 0, 0, 512, 64, 512, 64);
            string argpath10 = System.IO.Path.Combine(Path.Pictures, "11");
            GameClient.RenderTexture(ref argpath10, GameState.ResolutionWidth - 1536, GameState.ResolutionHeight - 1088, 0, 0, 512, 64, 512, 64);
            string argpath11 = System.IO.Path.Combine(Path.Pictures, "12");
            GameClient.RenderTexture(ref argpath11, GameState.ResolutionWidth - 2048, GameState.ResolutionHeight - 1088, 0, 0, 512, 64, 512, 64);
        }

        public static void DrawHotbar()
        {
            long xO;
            long yO;
            long Width;
            long Height;
            long i;
            long t;
            string sS;

            if (GameState.MyIndex < 0| GameState.MyIndex > Constant.MAX_PLAYERS)
                return;

            xO = Windows[GetWindowIndex("winHotbar")].Left;
            yO = Windows[GetWindowIndex("winHotbar")].Top;

            // Render start + end wood
            string argpath = System.IO.Path.Combine(Path.Gui, 31.ToString());
            GameClient.RenderTexture(ref argpath, (int)(xO - 1L), (int)(yO + 3L), 0, 0, 11, 26, 11, 26);
            string argpath1 = System.IO.Path.Combine(Path.Gui, 31.ToString());
            GameClient.RenderTexture(ref argpath1, (int)(xO + 407L), (int)(yO + 3L), 0, 0, 11, 26, 11, 26);
            for (i = 0L; i < Constant.MAX_HOTBAR; i++)
            {
                xO = Windows[GetWindowIndex("winHotbar")].Left + GameState.HotbarLeft + i * GameState.HotbarOffsetX;
                yO = Windows[GetWindowIndex("winHotbar")].Top + GameState.HotbarTop;
                Width = 36L;
                Height = 36L;

                // Don't render last one
                if (i != Constant.MAX_HOTBAR)
                {
                    // Render wood
                    string argpath2 = System.IO.Path.Combine(Path.Gui, 32.ToString());
                    GameClient.RenderTexture(ref argpath2, (int)(xO + 30L), (int)(yO + 3L), 0, 0, 13, 26, 13, 26);
                }

                // Render box
                string argpath3 = System.IO.Path.Combine(Path.Gui, 30.ToString());
                GameClient.RenderTexture(ref argpath3, (int)(xO - 2L), (int)(yO - 2L), 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);

                // Render icon
                if (!(DragBox.Origin == PartOrigin.Hotbar & DragBox.Slot == i))
                {
                    switch (Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].SlotType)
                    {
                        case (byte)PartOrigin.Inventory:
                            {
                                Item.StreamItem((int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot);
                                if (Strings.Len(Core.Data.Item[(int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Name) > 0 & Core.Data.Item[(int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon > 0)
                                {
                                    string argpath4 = System.IO.Path.Combine(Path.Items, Core.Data.Item[(int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon.ToString());
                                    GameClient.RenderTexture(ref argpath4, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32);
                                }

                                break;
                            }

                        case (byte)PartOrigin.SkillTree:
                            {
                                Database.StreamSkill((int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot);
                                if (Strings.Len(Data.Skill[(int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Name) > 0 & Data.Skill[(int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon > 0)
                                {
                                    string argpath5 = System.IO.Path.Combine(Path.Skills, Data.Skill[(int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon.ToString());
                                    GameClient.RenderTexture(ref argpath5, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32);
                                    for (t = 0L; t < Constant.MAX_PLAYER_SKILLS; t++)
                                    {
                                        if (GetPlayerSkill(GameState.MyIndex, (int)t) >= 0)
                                        {
                                            if (GetPlayerSkill(GameState.MyIndex, (int)t) == Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot & GetPlayerSkillCD(GameState.MyIndex, (int)t) > 0)
                                            {
                                                string argpath6 = System.IO.Path.Combine(Path.Skills, Data.Skill[(int)Core.Data.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon.ToString());
                                                GameClient.RenderTexture(ref argpath6, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32, 255, 100, 100, 100);
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                    }
                }

                // Draw the numbers
                sS = Conversion.Str(i);
                if (i == Constant.MAX_HOTBAR)
                    sS = "0";
                Text.RenderText(sS, (int)(xO + 4L), (int)(yO + 19L), Color.White, Color.White);
            }
        }

        public static void DrawShop()
        {
            long Xo;
            long Yo;
            long itemIcon;
            long itemNum;
            long Amount;
            long i;
            long Top;
            long Left;
            long Y;
            long X;
            var Color = default(long);

            if (GameState.InShop < 0 | GameState.InShop > Constant.MAX_SHOPS)
                return;

            Shop.StreamShop(GameState.InShop);

            Xo = Windows[GetWindowIndex("winShop")].Left;
            Yo = Windows[GetWindowIndex("winShop")].Top;

            if (!GameState.shopIsSelling)
            {
                // render the shop items
                for (i = 0L; i < Constant.MAX_TRADES; i++)
                {
                    itemNum = Data.Shop[GameState.InShop].TradeItem[(int)i].Item;

                    // draw early
                    Top = Yo + GameState.ShopTop + (GameState.ShopOffsetY + 32L) * (i / GameState.ShopColumns);
                    Left = Xo + GameState.ShopLeft + (GameState.ShopOffsetX + 32L) * (i % GameState.ShopColumns);

                    // draw selected square
                    if (GameState.shopSelectedSlot == i)
                    {
                        string argpath = System.IO.Path.Combine(Path.Gui, 61.ToString());
                        GameClient.RenderTexture(ref argpath, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);
                    }

                    if (itemNum >= 0L & itemNum < Constant.MAX_ITEMS)
                    {
                        Item.StreamItem((int)itemNum);
                        itemIcon = Core.Data.Item[(int)itemNum].Icon;
                        if (itemIcon > 0L & itemIcon <= GameState.NumItems)
                        {
                            // draw item
                            string argpath1 = System.IO.Path.Combine(Path.Items, itemIcon.ToString());
                            GameClient.RenderTexture(ref argpath1, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);
                        }
                    }
                }
            }
            else
            {
                // render the shop items
                for (i = 0L; i < Constant.MAX_TRADES; i++)
                {
                    itemNum = (long)GetPlayerInv(GameState.MyIndex, (int)i);

                    // draw early
                    Top = Yo + GameState.ShopTop + (GameState.ShopOffsetY + 32L) * (i / GameState.ShopColumns);
                    Left = Xo + GameState.ShopLeft + (GameState.ShopOffsetX + 32L) * (i % GameState.ShopColumns);

                    // draw selected square
                    if (GameState.shopSelectedSlot == i)
                    {
                        string argpath2 = System.IO.Path.Combine(Path.Gui, 61.ToString());
                        GameClient.RenderTexture(ref argpath2, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);
                    }

                    if (itemNum >= 0L & itemNum < Constant.MAX_ITEMS)
                    {
                        Item.StreamItem((int)itemNum);
                        itemIcon = Core.Data.Item[(int)itemNum].Icon;
                        if (itemIcon > 0L & itemIcon <= GameState.NumItems)
                        {
                            // draw item
                            string argpath3 = System.IO.Path.Combine(Path.Items, itemIcon.ToString());
                            GameClient.RenderTexture(ref argpath3, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);

                            // If item is a stack - draw the amount you have
                            if (GetPlayerInvValue(GameState.MyIndex, (int)i) > 1)
                            {
                                Y = Top + 20L;
                                X = Left + 1L;
                                Amount = Conversions.ToLong(GetPlayerInvValue(GameState.MyIndex, (int)i).ToString());

                                // Draw currency but with k, m, b etc. using a conversion function
                                if (Amount < 1000000L)
                                {
                                    Color = (long)Core.Color.White;
                                }
                                else if (Amount > 1000000L & Amount < 10000000L)
                                {
                                    Color = (long)Core.Color.Yellow;
                                }
                                else if (Amount > 10000000L)
                                {
                                    Color = (long)Core.Color.BrightGreen;
                                }

                                Text.RenderText(GameLogic.ConvertCurrency((int)Amount), (int)X, (int)Y, GameClient.QbColorToXnaColor((int)Color), GameClient.QbColorToXnaColor((int)Color));
                            }
                        }
                    }
                }
            }
        }

        public static void DrawShopBackground()
        {
            long Xo;
            long Yo;
            long Width;
            long Height;
            long i;
            long Y;

            Xo = Windows[GetWindowIndex("winShop")].Left;
            Yo = Windows[GetWindowIndex("winShop")].Top;
            Width = Windows[GetWindowIndex("winShop")].Width;
            Height = Windows[GetWindowIndex("winShop")].Height;

            // render green
            string argpath = System.IO.Path.Combine(Path.Gui, 34.ToString());
            GameClient.RenderTexture(ref argpath, (int)(Xo + 4L), (int)(Yo + 23L), 0, 0, (int)(Width - 8L), (int)(Height - 27L), 4, 4);

            Width = 76L;
            Height = 76L;

            Y = Yo + 23L;
            // render grid - row
            for (i = 0L; i < 3L; i++)
            {
                if (i == 3L)
                    Height = 42L;
                string argpath1 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath1, (int)(Xo + 4L), (int)Y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath2 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath2, (int)(Xo + 80L), (int)Y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath3 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath3, (int)(Xo + 156L), (int)Y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath4 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath4, (int)(Xo + 232L), (int)Y, 0, 0, 42, (int)Height, 42, (int)Height);
                Y = Y + 76L;
            }

            // render bottom wood
            string argpath5 = System.IO.Path.Combine(Path.Gui, 1.ToString());
            GameClient.RenderTexture(ref argpath5, (int)(Xo + 4L), (int)(Y - 34L), 0, 0, 270, 72, 270, 72);
        }

        public static void DrawBank()
        {
            long X;
            long Y;
            long Xo;
            long Yo;
            long width;
            long height;
            long i;
            long itemNum;
            long itemIcon;

            long Left;
            long top;
            var color = default(long);
            bool skipItem;
            long amount;
            long tmpItem;

            if (GameState.MyIndex < 0| GameState.MyIndex > Constant.MAX_PLAYERS)
                return;

            Xo = Windows[GetWindowIndex("winBank")].Left;
            Yo = Windows[GetWindowIndex("winBank")].Top;
            width = Windows[GetWindowIndex("winBank")].Width;
            height = Windows[GetWindowIndex("winBank")].Height;

            // render green
            string argpath = System.IO.Path.Combine(Path.Gui, 34.ToString());
            GameClient.RenderTexture(ref argpath, (int)(Xo + 4L), (int)(Yo + 23L), 0, 0, (int)(width - 8L), (int)(height - 27L), 4, 4);

            width = 76L;
            height = 76L;

            Y = Yo + 23L;
            // render grid - row
            for (i = 0L; i <= 4L; i++)
            {
                if (i == 4L)
                    height = 42L;
                string argpath1 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath1, (int)(Xo + 4L), (int)Y, 0, 0, (int)width, (int)height, (int)width, (int)height);
                string argpath2 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath2, (int)(Xo + 80L), (int)Y, 0, 0, (int)width, (int)height, (int)width, (int)height);
                string argpath3 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath3, (int)(Xo + 156L), (int)Y, 0, 0, (int)width, (int)height, (int)width, (int)height);
                string argpath4 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath4, (int)(Xo + 232L), (int)Y, 0, 0, (int)width, (int)height, (int)width, (int)height);
                string argpath5 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath5, (int)(Xo + 308L), (int)Y, 0, 0, 79, (int)height, 79, (int)height);
                Y = Y + 76L;
            }

            // actually draw the icons
            for (i = 0L; i < Constant.MAX_BANK; i++)
            {
                itemNum = (long)GetBank(GameState.MyIndex, (byte)i);

                if (itemNum >= 0L & itemNum < Constant.MAX_ITEMS)
                {
                    Item.StreamItem((int)itemNum);

                    // not dragging?
                    if (!(DragBox.Origin == PartOrigin.Bank & DragBox.Slot == i))
                    {
                        itemIcon = Core.Data.Item[(int)itemNum].Icon;

                        if (itemIcon > 0L & itemIcon <= GameState.NumItems)
                        {
                            top = Yo + GameState.BankTop + (GameState.BankOffsetY + 32L) * (i / GameState.BankColumns);
                            Left = Xo + GameState.BankLeft + (GameState.BankOffsetX + 32L) * (i % GameState.BankColumns);

                            // draw icon
                            string argpath6 = System.IO.Path.Combine(Path.Items, itemIcon.ToString());
                            GameClient.RenderTexture(ref argpath6, (int)Left, (int)top, 0, 0, 32, 32, 32, 32);

                            // If item is a stack - draw the amount you have
                            if (GetBankValue(GameState.MyIndex, (byte)i) > 1)
                            {
                                Y = top + 20L;
                                X = Left + 1L;
                                amount = GetBankValue(GameState.MyIndex, (byte)i);

                                // Draw currency but with k, m, b etc. using a convertion function
                                if (amount < 1000000L)
                                {
                                    color = (long)Core.Color.White;
                                }
                                else if (amount > 1000000L & amount < 10000000L)
                                {
                                    color = (long)Core.Color.Yellow;
                                }
                                else if (amount > 10000000L)
                                {
                                    color = (long)Core.Color.BrightGreen;
                                }

                                Text.RenderText(GameLogic.ConvertCurrency((int)amount), (int)X, (int)Y, GameClient.QbColorToXnaColor((int)color), GameClient.QbColorToXnaColor((int)color));
                            }
                        }
                    }
                }
            }

        }

        public static void DrawTrade()
        {
            long Xo;
            long Yo;
            long Width;
            long Height;
            long i;
            long Y;
            long X;

            Xo = Windows[GetWindowIndex("winTrade")].Left;
            Yo = Windows[GetWindowIndex("winTrade")].Top;
            Width = Windows[GetWindowIndex("winTrade")].Width;
            Height = Windows[GetWindowIndex("winTrade")].Height;

            // render green
            string argpath = System.IO.Path.Combine(Path.Gui, 34.ToString());
            GameClient.RenderTexture(ref argpath, (int)(Xo + 4L), (int)(Yo + 23L), 0, 0, (int)(Width - 8L), (int)(Height - 27L), 4, 4);

            // top wood
            string argpath1 = System.IO.Path.Combine(Path.Gui, 1.ToString());
            GameClient.RenderTexture(ref argpath1, (int)(Xo + 4L), (int)(Yo + 23L), 100, 100, (int)(Width - 8L), 18, (int)(Width - 8L), 18);

            // left wood
            string argpath2 = System.IO.Path.Combine(Path.Gui, 1.ToString());
            GameClient.RenderTexture(ref argpath2, (int)(Xo + 4L), (int)(Yo + 40L), 350, 0, 5, (int)(Height - 45L), 5, (int)(Height - 45L));

            // right wood
            string argpath3 = System.IO.Path.Combine(Path.Gui, 1.ToString());
            GameClient.RenderTexture(ref argpath3, (int)(Xo + Width - 9L), (int)(Yo + 40L), 350, 0, 5, (int)(Height - 45L), 5, (int)(Height - 45L));

            // centre wood
            string argpath4 = System.IO.Path.Combine(Path.Gui, 1.ToString());
            GameClient.RenderTexture(ref argpath4, (int)(Xo + 203L), (int)(Yo + 40L), 350, 0, 6, (int)(Height - 45L), 6, (int)(Height - 45L));

            // bottom wood
            string argpath5 = System.IO.Path.Combine(Path.Gui, 1.ToString());
            GameClient.RenderTexture(ref argpath5, (int)(Xo + 4L), (int)(Yo + 307L), 100, 100, (int)(Width - 8L), 75, (int)(Width - 8L), 75);

            // left
            Width = 76L;
            Height = 76L;
            Y = Yo + 40L;
            for (i = 0L; i <= 4L; i++)
            {
                if (i == 4L)
                    Height = 38L;
                string argpath6 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath6, (int)(Xo + 4L + 5L), (int)Y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath7 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath7, (int)(Xo + 80L + 5L), (int)Y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath8 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath8, (int)(Xo + 156L + 5L), (int)Y, 0, 0, 42, (int)Height, 42, (int)Height);
                Y = Y + 76L;
            }

            // right
            Width = 76L;
            Height = 76L;
            Y = Yo + 40L;
            for (i = 0L; i <= 4L; i++)
            {
                if (i == 4L)
                    Height = 38L;
                string argpath9 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath9, (int)(Xo + 4L + 205L), (int)Y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath10 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath10, (int)(Xo + 80L + 205L), (int)Y, 0, 0, (int)Width, (int)Height, (int)Width, (int)Height);
                string argpath11 = System.IO.Path.Combine(Path.Gui, 35.ToString());
                GameClient.RenderTexture(ref argpath11, (int)(Xo + 156L + 205L), (int)Y, 0, 0, 42, (int)Height, 42, (int)Height);

                Y = Y + 76L;
            }
        }

        public static void DrawYourTrade()
        {
            long i;
            long itemNum;
            long ItemPic;
            long Top;
            long Left;
            var Color = default(long);
            string Amount;
            long X;
            long Y;
            long Xo;
            long Yo;

            Xo = Windows[GetWindowIndex("winTrade")].Left + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picYour")].Left;
            Yo = Windows[GetWindowIndex("winTrade")].Top + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picYour")].Top;

            // your items
            for (i = 0L; i < Constant.MAX_INV; i++)
            {
                if (Data.TradeYourOffer[(int)i].Num >= 0)
                {
                    itemNum = (long)GetPlayerInv(GameState.MyIndex, (int)Data.TradeYourOffer[(int)i].Num);
                    if (itemNum >= 0L & itemNum < Constant.MAX_ITEMS)
                    {
                        Item.StreamItem((int)itemNum);
                        ItemPic = Core.Data.Item[(int)itemNum].Icon;

                        if (ItemPic > 0L & ItemPic <= GameState.NumItems)
                        {
                            Top = Yo + GameState.TradeTop + (GameState.TradeOffsetY + 32L) * (i / GameState.TradeColumns);
                            Left = Xo + GameState.TradeLeft + (GameState.TradeOffsetX + 32L) * (i % GameState.TradeColumns);

                            // draw icon
                            string argpath = System.IO.Path.Combine(Path.Items, ItemPic.ToString());
                            GameClient.RenderTexture(ref argpath, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);

                            // If item is a stack - draw the amount you have
                            if (Data.TradeYourOffer[(int)i].Value > 1)
                            {
                                Y = Top + 20L;
                                X = Left + 1L;
                                Amount = Data.TradeYourOffer[(int)i].Value.ToString();

                                // Draw currency but with k, m, b etc. using a convertion function
                                if (Conversions.ToLong(Amount) < 1000000L)
                                {
                                    Color = (long)Core.Color.White;
                                }
                                else if (Conversions.ToLong(Amount) > 1000000L & Conversions.ToLong(Amount) < 10000000L)
                                {
                                    Color = (long)Core.Color.Yellow;
                                }
                                else if (Conversions.ToLong(Amount) > 10000000L)
                                {
                                    Color = (long)Core.Color.BrightGreen;
                                }

                                Text.RenderText(GameLogic.ConvertCurrency(Conversions.ToInteger(Amount)), (int)X, (int)Y, GameClient.QbColorToXnaColor((int)Color), GameClient.QbColorToXnaColor((int)Color));
                            }
                        }
                    }
                }
            }
        }

        public static void DrawTheirTrade()
        {
            long i;
            long itemNum;
            long ItemPic;
            long Top;
            long Left;
            var Color = default(long);
            string Amount;
            long X;
            long Y;
            long Xo;
            long Yo;

            Xo = Windows[GetWindowIndex("winTrade")].Left + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picTheir")].Left;
            Yo = Windows[GetWindowIndex("winTrade")].Top + Windows[GetWindowIndex("winTrade")].Controls[GetControlIndex("winTrade", "picTheir")].Top;

            // their items
            for (i = 0L; i < Constant.MAX_INV; i++)
            {
                itemNum = (long)Data.TradeTheirOffer[(int)i].Num;
                if (itemNum >= 0L & itemNum < Constant.MAX_ITEMS)
                {
                    Item.StreamItem((int)itemNum);
                    ItemPic = Core.Data.Item[(int)itemNum].Icon;

                    if (ItemPic > 0L & ItemPic <= GameState.NumItems)
                    {
                        Top = Yo + GameState.TradeTop + (GameState.TradeOffsetY + 32L) * (i / GameState.TradeColumns);
                        Left = Xo + GameState.TradeLeft + (GameState.TradeOffsetX + 32L) * (i % GameState.TradeColumns);

                        // draw icon
                        string argpath = System.IO.Path.Combine(Path.Items, ItemPic.ToString());
                        GameClient.RenderTexture(ref argpath, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);

                        // If item is a stack - draw the amount you have
                        if (Data.TradeTheirOffer[(int)i].Value > 1)
                        {
                            Y = Top + 20L;
                            X = Left + 1L;
                            Amount = Data.TradeTheirOffer[(int)i].Value.ToString();

                            // Draw currency but with k, m, b etc. using a convertion function
                            if (Conversions.ToLong(Amount) < 1000000L)
                            {
                                Color = (long)Core.Color.White;
                            }
                            else if (Conversions.ToLong(Amount) > 1000000L & Conversions.ToLong(Amount) < 10000000L)
                            {
                                Color = (long)Core.Color.Yellow;
                            }
                            else if (Conversions.ToLong(Amount) > 10000000L)
                            {
                                Color = (long)Core.Color.BrightGreen;
                            }

                            Text.RenderText(GameLogic.ConvertCurrency(Conversions.ToInteger(Amount)), (int)X, (int)Y, GameClient.QbColorToXnaColor((int)Color), GameClient.QbColorToXnaColor((int)Color));
                        }
                    }
                }
            }
        }

        public static void UpdateActiveControl(Control modifiedControl)
        {
            // Ensure there is an active window and an active control to update
            if (ActiveWindow > 0L && Windows[ActiveWindow].ActiveControl > 0)
            {
                // Update the control within the active window's Controls array
                Windows[ActiveWindow].Controls[Windows[ActiveWindow].ActiveControl] = modifiedControl;
            }
        }

        public static Control GetActiveControl()
        {
            // Ensure there is an active window and an active control within that window
            if (ActiveWindow > 0L && Windows.ContainsKey(ActiveWindow) && Windows[ActiveWindow].ActiveControl > 0)
            {
                // Return the active control from the active window
                return Windows[ActiveWindow].Controls[Windows[ActiveWindow].ActiveControl];
            }

            // No active control found, return Nothing
            return null;
        }

        public static void ShowChat()
        {
            ShowWindow(GetWindowIndex("winChat"), resetPosition: false);
            HideWindow(GetWindowIndex("winChatSmall"));
            // Set the active control
            ActiveWindow = GetWindowIndex("winChat");
            SetActiveControl(GetWindowIndex("winChat"), GetControlIndex("winChat", "txtChat"));
            Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "txtChat")].Visible = true;
            GameState.inSmallChat = false;
            GameState.ChatScroll = 0L;
        }

        public static void HideChat()
        {
            ShowWindow(GetWindowIndex("winChatSmall"), resetPosition: false);
            HideWindow(GetWindowIndex("winChat"));

            // Set the active control
            ActiveWindow = GetWindowIndex("winChat");
            SetActiveControl(GetWindowIndex("winChat"), GetControlIndex("winChat", "txtChat"));
            Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "txtChat")].Visible = false;

            GameState.inSmallChat = true;
            GameState.ChatScroll = 0L;
        }

        private static string FilterUnsupportedCharacters(string text, Core.Font fontType)
        {
            if (text == null)
            {
                return string.Empty; // or handle it as appropriate
            }

            var supportedText = new StringBuilder();
            foreach (char ch in text)
            {
                if (Text.Fonts[fontType].Characters.Contains(ch))
                {
                    supportedText.Append(ch);           
                }
            }
            return supportedText.ToString();
        }
    }
}