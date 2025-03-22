using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
        public static readonly ConcurrentDictionary<long, Window> Windows = new ConcurrentDictionary<long, Window>();
        public static long ActiveWindow;

        // GUi parts
        public static Core.Type.ControlPartStruct DragBox;

        // Used for automatically the zOrder
        private static long zOrder_Win;
        private static long zOrder_Con;

        // Declare a timer to control when dragging can begin
        private static Stopwatch dragTimer = new Stopwatch();
        private const double dragInterval = 50d; // Set the interval in milliseconds to start dragging
        private static bool canDrag = false;  // Flag to control when dragging is allowed

        public class Window
        {
            public string Name { get; set; }
            public Core.Enum.ControlType Type { get; set; }
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
            public Core.Enum.FontType Font { get; set; }
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

            public Core.Enum.EntState State { get; set; }
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
            public Core.Enum.ControlType Type { get; set; }
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
            public Core.Enum.AlignmentType Align { get; set; }
            public Core.Enum.FontType Font { get; set; }
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
            public Core.Enum.EntState State { get; set; }
            public List<string> List { get; set; }

            // Arrays for states
            public List<long> Design { get; set; }
            public List<long> Image { get; set; }
            public List<string> Texture { get; set; }
            public List<Action> CallBack { get; set; }
        }

        public static void UpdateControl(long winNum, long zOrder, string name, Color color, Core.Enum.ControlType tType, List<long> design, List<long> image, List<string> texture, List<Action> callback, long left = 0L, long top = 0L, long width = 0L, long height = 0L, bool visible = true, bool canDrag = false, long Max = 0L, long Min = 0L, long value = 0L, string text = "", Core.Enum.AlignmentType align = 0, Core.Enum.FontType font = Core.Enum.FontType.Georgia, long alpha = 255L, bool clickThrough = false, long xOffset = 0L, long yOffset = 0L, byte zChange = 0, bool censor = false, long icon = 0L, Action onDraw = null, bool isActive = true, string tooltip = "", long @group = 0L, byte length = Constant.NAME_LENGTH, bool enabled = true)
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

                if (withBlock.zOrder == Windows.Count - 1)
                    return;

                oldZOrder = withBlock.zOrder;

                var loopTo = Windows.Count;
                for (i = 1L; i <= loopTo; i++)
                {

                    if (Windows[i].zOrder > oldZOrder)
                    {
                        Windows[i].zOrder = Windows[i].zOrder - 1L;
                    }
                }
                withBlock.zOrder = Windows.Count - 1;
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
                var loopTo = Windows.Count - 1;
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

        public static void UpdateWindow(string name, string caption, Core.Enum.FontType font, long zOrder, long left, long top, long width, long height, long icon, bool visible = true, long xOffset = 0L, long yOffset = 0L, long design_norm = 0L, long design_hover = 0L, long design_mousedown = 0L, long image_norm = 0L, long image_hover = 0L, long image_mousedown = 0L, Action callback_norm = null, Action callback_hover = null, Action callback_mousemove = null, Action callback_mousedown = null, Action callback_dblclick = null, Action onDraw = null, bool canDrag = true, byte zChange = 1, bool isActive = true, bool clickThrough = false)
        {
            var design = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, (int)Core.Enum.EntState.Count).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, (int)Core.Enum.EntState.Count).ToList());

            // Assign specific values for each state
            design[(int)Core.Enum.EntState.Normal] = design_norm;
            design[(int)Core.Enum.EntState.Hover] = design_hover;
            design[(int)Core.Enum.EntState.MouseDown] = design_mousedown;
            image[(int)Core.Enum.EntState.Normal] = image_norm;
            image[(int)Core.Enum.EntState.Hover] = image_hover;
            image[(int)Core.Enum.EntState.MouseDown] = image_mousedown;
            callback[(int)Core.Enum.EntState.Normal] = callback_norm;
            callback[(int)Core.Enum.EntState.Hover] = callback_hover;
            callback[(int)Core.Enum.EntState.MouseDown] = callback_mousedown;
            callback[(int)Core.Enum.EntState.MouseMove] = callback_mousemove;
            callback[(int)Core.Enum.EntState.DblClick] = callback_dblclick;

            // Create a new instance of Window and populate it
            var newWindow = new Window()
            {
                Name = name,
                Type = Core.Enum.ControlType.Window,
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
            Windows.TryAdd(Windows.Count + 1, newWindow);

            // Set the active window if visible
            if (visible)
                ActiveWindow = Windows.Count;
        }

        public static void UpdateTextbox(long winNum, string name, long left, long top, long width, long height, [Optional, DefaultParameterValue("")] string text, [Optional, DefaultParameterValue(Core.Enum.FontType.Georgia)] Core.Enum.FontType font, [Optional, DefaultParameterValue(Core.Enum.AlignmentType.Left)] Core.Enum.AlignmentType align, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(true)] bool isActive, [Optional, DefaultParameterValue(0L)] long xOffset, [Optional, DefaultParameterValue(0L)] long yOffset, [Optional, DefaultParameterValue(0L)] long image_norm, [Optional, DefaultParameterValue(0L)] long image_hover, [Optional, DefaultParameterValue(0L)] long image_mousedown, [Optional, DefaultParameterValue(0L)] long design_norm, [Optional, DefaultParameterValue(0L)] long design_hover, [Optional, DefaultParameterValue(0L)] long design_mousedown, [Optional, DefaultParameterValue(false)] bool censor, [Optional, DefaultParameterValue(0L)] long icon, [Optional, DefaultParameterValue(Constant.NAME_LENGTH)] byte length, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick, [Optional] ref Action callback_enter)
        {

            var design = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, (int)Core.Enum.EntState.Count).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, (int)Core.Enum.EntState.Count).ToList());

            // Assign specific values for each state
            design[(int)Core.Enum.EntState.Normal] = design_norm;
            design[(int)Core.Enum.EntState.Hover] = design_hover;
            design[(int)Core.Enum.EntState.MouseDown] = design_mousedown;
            image[(int)Core.Enum.EntState.Normal] = image_norm;
            image[(int)Core.Enum.EntState.Hover] = image_hover;
            image[(int)Core.Enum.EntState.MouseDown] = image_mousedown;
            callback[(int)Core.Enum.EntState.Normal] = callback_norm;
            callback[(int)Core.Enum.EntState.Hover] = callback_hover;
            callback[(int)Core.Enum.EntState.MouseDown] = callback_mousedown;
            callback[(int)Core.Enum.EntState.MouseMove] = callback_mousemove;
            callback[(int)Core.Enum.EntState.DblClick] = callback_dblclick;
            callback[(int)Core.Enum.EntState.Enter] = callback_enter;

            // Control the textbox
            UpdateControl(winNum, zOrder_Con, name, Color.White, Core.Enum.ControlType.TextBox, design, image, texture, callback, left, top, width, height, visible, text: text, align: align, font: font, alpha: alpha, xOffset: xOffset, yOffset: yOffset, censor: censor, icon: icon, isActive: isActive, length: length);
        }


        public static void UpdatePictureBox(long winNum, string name, long left, long top, long width, long height, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(false)] bool canDrag, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(true)] bool clickThrough, [Optional, DefaultParameterValue(0L)] long image_norm, [Optional, DefaultParameterValue(0L)] long image_hover, [Optional, DefaultParameterValue(0L)] long image_mousedown, [Optional, DefaultParameterValue(0L)] long design_norm, [Optional, DefaultParameterValue(0L)] long design_hover, [Optional, DefaultParameterValue(0L)] long design_mousedown, [Optional, DefaultParameterValue("")] string texturePath, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick, [Optional] ref Action onDraw)
        {

            var design = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, (int)Core.Enum.EntState.Count).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, (int)Core.Enum.EntState.Count).ToList());

            if (string.IsNullOrEmpty(texturePath))
            {
                texturePath = Path.Gui;
            }

            // fill temp arrays
            design[(int)Core.Enum.EntState.Normal] = design_norm;
            design[(int)Core.Enum.EntState.Hover] = design_hover;
            design[(int)Core.Enum.EntState.MouseDown] = design_mousedown;
            image[(int)Core.Enum.EntState.Normal] = image_norm;
            image[(int)Core.Enum.EntState.Hover] = image_hover;
            image[(int)Core.Enum.EntState.MouseDown] = image_mousedown;
            texture[(int)Core.Enum.EntState.Normal] = texturePath;
            texture[(int)Core.Enum.EntState.Hover] = texturePath;
            texture[(int)Core.Enum.EntState.MouseDown] = texturePath;

            callback[(int)Core.Enum.EntState.Normal] = callback_norm;
            callback[(int)Core.Enum.EntState.Hover] = callback_hover;
            callback[(int)Core.Enum.EntState.MouseDown] = callback_mousedown;
            callback[(int)Core.Enum.EntState.MouseMove] = callback_mousemove;
            callback[(int)Core.Enum.EntState.DblClick] = callback_dblclick;

            // Control the box
            UpdateControl(winNum, zOrder_Con, name, Color.White, Core.Enum.ControlType.PictureBox, design, image, texture, callback, left, top, width, height, visible, canDrag, clickThrough: Conversions.ToBoolean(alpha), xOffset: Conversions.ToLong(clickThrough), onDraw: onDraw);
        }

        public static void UpdateButton(long winNum, string name, long left, long top, long width, long height, [Optional, DefaultParameterValue("")] string text, [Optional, DefaultParameterValue(Core.Enum.FontType.Georgia)] Core.Enum.FontType font, [Optional, DefaultParameterValue(0L)] long icon, [Optional, DefaultParameterValue(0L)] long image_norm, [Optional, DefaultParameterValue(0L)] long image_hover, [Optional, DefaultParameterValue(0L)] long image_mousedown, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(0L)] long design_norm, [Optional, DefaultParameterValue(0L)] long design_hover, [Optional, DefaultParameterValue(0L)] long design_mousedown, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick, long xOffset = 0L, long yOffset = 0L, string tooltip = "", bool censor = false)
        {

            var design = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, (int)Core.Enum.EntState.Count).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, (int)Core.Enum.EntState.Count).ToList());

            // fill temp arrays
            design[(int)Core.Enum.EntState.Normal] = design_norm;
            design[(int)Core.Enum.EntState.Hover] = design_hover;
            design[(int)Core.Enum.EntState.MouseDown] = design_mousedown;
            image[(int)Core.Enum.EntState.Normal] = image_norm;
            image[(int)Core.Enum.EntState.Hover] = image_hover;
            image[(int)Core.Enum.EntState.MouseDown] = image_mousedown;
            texture[(int)Core.Enum.EntState.Normal] = Path.Gui;
            texture[(int)Core.Enum.EntState.Hover] = Path.Gui;
            texture[(int)Core.Enum.EntState.MouseDown] = Path.Gui;
            callback[(int)Core.Enum.EntState.Normal] = callback_norm;
            callback[(int)Core.Enum.EntState.Hover] = callback_hover;
            callback[(int)Core.Enum.EntState.MouseDown] = callback_mousedown;
            callback[(int)Core.Enum.EntState.MouseMove] = callback_mousemove;
            callback[(int)Core.Enum.EntState.DblClick] = callback_dblclick;

            // Control the button 
            UpdateControl(winNum, zOrder_Con, name, Color.White, Core.Enum.ControlType.Button, design, image, texture, callback, left, top, width, height, visible, text: text, font: font, clickThrough: Conversions.ToBoolean(alpha), xOffset: xOffset, yOffset: yOffset, censor: censor, icon: icon, tooltip: tooltip);
        }

        public static void UpdateLabel(long winNum, string name, long left, long top, long width, long height, string text, Core.Enum.FontType font, Color color, [Optional, DefaultParameterValue(Core.Enum.AlignmentType.Left)] Core.Enum.AlignmentType align, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(false)] bool clickThrough, [Optional, DefaultParameterValue(false)] bool censor, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick, [Optional] ref bool enabled)
        {
            var design = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, (int)Core.Enum.EntState.Count).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, (int)Core.Enum.EntState.Count).ToList());

            // fill temp arrays
            callback[(int)Core.Enum.EntState.Normal] = callback_norm;
            callback[(int)Core.Enum.EntState.Hover] = callback_hover;
            callback[(int)Core.Enum.EntState.MouseDown] = callback_mousedown;
            callback[(int)Core.Enum.EntState.MouseMove] = callback_mousemove;
            callback[(int)Core.Enum.EntState.DblClick] = callback_dblclick;

            // Control the label
            UpdateControl(winNum, zOrder_Con, name, Color.White, Core.Enum.ControlType.Label, design, image, texture, callback, left, top, width, height, visible, text: text, align: align, font: font, clickThrough: Conversions.ToBoolean(alpha), xOffset: Conversions.ToLong(clickThrough), censor: censor, enabled: enabled);
        }

        public static void UpdateCheckBox(long winNum, string name, long left, long top, long width, [Optional, DefaultParameterValue(15L)] long height, [Optional, DefaultParameterValue(0L)] long value, [Optional, DefaultParameterValue("")] string text, [Optional, DefaultParameterValue(Core.Enum.FontType.Georgia)] Core.Enum.FontType font, [Optional, DefaultParameterValue(Core.Enum.AlignmentType.Left)] Core.Enum.AlignmentType align, [Optional, DefaultParameterValue(true)] bool visible, [Optional, DefaultParameterValue(255L)] long alpha, [Optional, DefaultParameterValue(0L)] long theDesign, [Optional, DefaultParameterValue(0L)] long @group, [Optional, DefaultParameterValue(false)] bool censor, [Optional] ref Action callback_norm, [Optional] ref Action callback_hover, [Optional] ref Action callback_mousedown, [Optional] ref Action callback_mousemove, [Optional] ref Action callback_dblclick)
        {
            var design = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Designs, (int)Core.Enum.EntState.Count).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, (int)Core.Enum.EntState.Count).ToList());

            design[0] = theDesign;
            texture[0] = Path.Gui;

            // fill temp arrays
            callback[(int)Core.Enum.EntState.Normal] = callback_norm;
            callback[(int)Core.Enum.EntState.Hover] = callback_hover;
            callback[(int)Core.Enum.EntState.MouseDown] = callback_mousedown;
            callback[(int)Core.Enum.EntState.MouseMove] = callback_mousemove;
            callback[(int)Core.Enum.EntState.DblClick] = callback_dblclick;

            // Control the box
            UpdateControl(winNum, zOrder_Con, name, Color.White, Core.Enum.ControlType.Checkbox, design, image, texture, callback, left, top, width, height, visible, value: value, text: text, align: align, font: font, clickThrough: Conversions.ToBoolean(alpha), censor: censor, @group: group);
        }

        public static void UpdateComboBox(long winNum, string name, long left, long top, long width, long height, long design)
        {
            // Initialize lists for the control states
            var theDesign = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var image = new List<long>(Enumerable.Repeat(0L, (int)Core.Enum.EntState.Count).ToList());
            var texture = new List<string>(Enumerable.Repeat(Path.Gui, (int)Core.Enum.EntState.Count).ToList());
            var callback = new List<Action>(Enumerable.Repeat((Action)null, (int)Core.Enum.EntState.Count).ToList());

            // Set the design for the normal state
            theDesign[0] = design;
            texture[0] = Path.Gui;

            // Update the control in the window using the updated listsfupdate
            UpdateControl(winNum, zOrder_Con, name, Color.White, Core.Enum.ControlType.Combobox, theDesign, image, texture, callback, left, top, width, height);
        }

        public static int GetWindowIndex(string winName)
        {
            int GetWindowIndexRet = default;

            var loopTo = Windows.Count - 1;
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
                case Core.Enum.ControlType.TextBox:
                    {
                        Windows[curWindow].LastControl = Windows[curWindow].ActiveControl;
                        Windows[curWindow].ActiveControl = (int)curControl;
                        SetActiveControlRet = Conversions.ToBoolean(1);
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

            var loopTo = Windows.Count - 1;
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
            for (i = Windows.Count - 1; i >= 1L; i += -1)
            {
                if (Windows[i].Visible == true & Windows[i].zChange == 1)
                {
                    ActiveWindow = i;
                    break;
                }
            }
        }

        public static void UpdateWindow_Login()
        {
            // Control the window
            UpdateWindow("winLogin", "Login", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 276L, 212L, 45L, true, 3L, 5L, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm);

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Parchment
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 26L, 264L, 180L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);

            // Shadows
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picShadow_1", 67L, 43L, 142L, 9L, design_norm: (long)Core.Enum.DesignType.BlackOval, design_hover: (long)Core.Enum.DesignType.BlackOval, design_mousedown: (long)Core.Enum.DesignType.BlackOval, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picShadow_2", 67L, 79L, 142L, 9L, design_norm: (long)Core.Enum.DesignType.BlackOval, design_hover: (long)Core.Enum.DesignType.BlackOval, design_mousedown: (long)Core.Enum.DesignType.BlackOval, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw2);

            // Close button
            var argcallback_mousedown3 = new Action(General.DestroyGame);
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3);

            // Buttons
            var argcallback_mousedown4 = new Action(NetworkSend.btnLogin_Click);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Gui.UpdateButton(Windows.Count, "btnAccept", 67L, 134L, 67L, 22L, "Accept", Core.Enum.FontType.Arial, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
            var argcallback_mousedown5 = new Action(General.DestroyGame);
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Gui.UpdateButton(Windows.Count, "btnExit", 142L, 134L, 67L, 22L, "Exit", Core.Enum.FontType.Arial, design_norm: (long)Core.Enum.DesignType.Red, design_hover: (long)Core.Enum.DesignType.Red_Hover, design_mousedown: (long)Core.Enum.DesignType.Red_Click, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5);

            // Labels
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblUsername", 72L, 39L, 142L, 10L, "Username", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, enabled: ref enabled);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            UpdateLabel(Windows.Count, "lblPassword", 72L, 75L, 142L, 10L, "Password", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, enabled: ref enabled);

            // Textboxes
            if (SettingsManager.Instance.SaveUsername == true)
            {
                Action argcallback_norm8 = null;
                Action argcallback_hover8 = null;
                Action argcallback_mousedown8 = null;
                Action argcallback_mousemove8 = null;
                Action argcallback_dblclick8 = null;
                Action argcallback_enter = null;
                UpdateTextbox(Windows.Count, "txtUsername", 67L, 55L, 142L, 19L, SettingsManager.Instance.Username, Core.Enum.FontType.Arial, Core.Enum.AlignmentType.Left, xOffset: 5L, yOffset: 3L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, callback_enter: ref argcallback_enter);
            }
            else
            {
                Action argcallback_norm9 = null;
                Action argcallback_hover9 = null;
                Action argcallback_mousedown9 = null;
                Action argcallback_mousemove9 = null;
                Action argcallback_dblclick9 = null;
                Action argcallback_enter1 = null;
                UpdateTextbox(Windows.Count, "txtUsername", 67L, 55L, 142L, 19L, "", Core.Enum.FontType.Arial, Core.Enum.AlignmentType.Left, xOffset: 5L, yOffset: 3L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, callback_enter: ref argcallback_enter1);
            }
            Action argcallback_norm10 = null;
            Action argcallback_hover10 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Action argcallback_enter2 = null;
            UpdateTextbox(Windows.Count, "txtPassword", 67L, 86L, 142L, 19L, font: Core.Enum.FontType.Arial, align: Core.Enum.AlignmentType.Left, xOffset: 5L, yOffset: 3L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, censor: true, callback_norm: ref argcallback_norm10, callback_hover: ref argcallback_hover10, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, callback_enter: ref argcallback_enter2);

            // Checkbox
            var argcallback_mousedown11 = new Action(chkSaveUser_Click);
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Action argcallback_norm11 = null;
            Action argcallback_hover11 = null;
            Gui.UpdateCheckBox(Windows.Count, "chkSaveUsername", 67L, 114L, 142L, value: Conversions.ToLong(SettingsManager.Instance.SaveUsername), text: "Save Username?", font: Core.Enum.FontType.Arial, theDesign: (long)Core.Enum.DesignType.ChkNorm, callback_norm: ref argcallback_norm11, callback_hover: ref argcallback_hover11, callback_mousedown: ref argcallback_mousedown11, callback_mousemove: ref argcallback_mousemove11, callback_dblclick: ref argcallback_dblclick11);

            // Register Button
            var argcallback_mousedown12 = new Action(btnRegister_Click);
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            Action argcallback_norm12 = null;
            Action argcallback_hover12 = null;
            Gui.UpdateButton(Windows.Count, "btnRegister", 12L, Windows[Windows.Count].Height - 35L, 252L, 22L, "Register Account", Core.Enum.FontType.Arial, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref argcallback_norm12, callback_hover: ref argcallback_hover12, callback_mousedown: ref argcallback_mousedown12, callback_mousemove: ref argcallback_mousemove12, callback_dblclick: ref argcallback_dblclick12);

            // Set the active control
            if (!(Strings.Len(Windows[GetWindowIndex("winLogin")].Controls[GetControlIndex("winLogin", "txtUsername")].Text) > 0))
            {
                SetActiveControl(GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtUsername"));
            }
            else
            {
                SetActiveControl(GetWindowIndex("winLogin"), GetControlIndex("winLogin", "txtPassword"));
            }
        }

        public static void UpdateWindow_Register()
        {
            // Control the window
            UpdateWindow("winRegister", "Register Account", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 276L, 202L, 45L, false, 3L, 5L, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm);

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnReturnMain_Click);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Enum.FontType.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 26L, 264L, 170L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // Shadows
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picShadow_1", 67L, 43L, 142L, 9L, design_norm: (long)Core.Enum.DesignType.BlackOval, design_hover: (long)Core.Enum.DesignType.BlackOval, design_mousedown: (long)Core.Enum.DesignType.BlackOval, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picShadow_2", 67L, 79L, 142L, 9L, design_norm: (long)Core.Enum.DesignType.BlackOval, design_hover: (long)Core.Enum.DesignType.BlackOval, design_mousedown: (long)Core.Enum.DesignType.BlackOval, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw2);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Windows.Count, "picShadow_3", 67L, 115L, 142L, 9L, design_norm: (long)Core.Enum.DesignType.BlackOval, design_hover: (long)Core.Enum.DesignType.BlackOval, design_mousedown: (long)Core.Enum.DesignType.BlackOval, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw3);
            // UpdatePictureBox(Windows.Count, "picShadow_4", 67, 151, 142, 9, , , , , , , , DesignType.BlackOval, DesignType.BlackOval, DesignType.BlackOval)
            // UpdatePictureBox(Windows.Count, "picShadow_5", 67, 187, 142, 9, , , , , , , , DesignType.BlackOval, DesignType.BlackOval, DesignType.BlackOval)

            // Buttons
            var argcallback_mousedown5 = new Action(btnSendRegister_Click);
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Gui.UpdateButton(Windows.Count, "btnAccept", 68L, 152L, 67L, 22L, "Accept", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown5, ref argcallback_mousemove5, ref argcallback_dblclick5, 0L, 0L, "", false);

            var argcallback_mousedown6 = new Action(btnReturnMain_Click);
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Gui.UpdateButton(Windows.Count, "btnExit", 142L, 152L, 67L, 22L, "Back", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Red, (long)Core.Enum.DesignType.Red_Hover, (long)Core.Enum.DesignType.Red_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown6, ref argcallback_mousemove6, ref argcallback_dblclick6, 0L, 0L, "", false);

            // Labels
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblUsername", 66L, 39L, 142L, 10L, "Username", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm4, ref argcallback_hover4, ref argcallback_mousedown7, ref argcallback_mousemove7, ref argcallback_dblclick7, enabled: ref enabled);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            UpdateLabel(Windows.Count, "lblPassword", 66L, 75L, 142L, 10L, "Password", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm5, ref argcallback_hover5, ref argcallback_mousedown8, ref argcallback_mousemove8, ref argcallback_dblclick8, enabled: ref enabled);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            UpdateLabel(Windows.Count, "lblRetypePassword", 66L, 110L, 142L, 10L, "Retype Password", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm6, ref argcallback_hover6, ref argcallback_mousedown9, ref argcallback_mousemove9, ref argcallback_dblclick9, enabled: ref enabled);
            // UpdateLabel(Windows.Count, "lblCode", 66, 147, 142, 10, "Secret Code", FontType.Arial, AlignmentType.Center)
            // UpdateLabel(Windows.Count, "lblCaptcha", 66, 183, 142, 10, "Captcha", FontType.Arial, AlignmentType.Center)

            // Textboxes
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Action argcallback_enter = null;
            UpdateTextbox(Windows.Count, "txtUsername", 67L, 55L, 142L, 19L, "", Core.Enum.FontType.Arial, Core.Enum.AlignmentType.Left, true, 255L, true, 5L, 3L, 0L, 0L, 0L, (long)Core.Enum.DesignType.TextWhite, (long)Core.Enum.DesignType.TextWhite, (long)Core.Enum.DesignType.TextWhite, false, 0L, Constant.NAME_LENGTH, ref argcallback_norm7, ref argcallback_hover7, ref argcallback_mousedown10, ref argcallback_mousemove10, ref argcallback_dblclick10, ref argcallback_enter);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Action argcallback_enter1 = null;
            UpdateTextbox(Windows.Count, "txtPassword", 67L, 90L, 142L, 19L, "", Core.Enum.FontType.Arial, Core.Enum.AlignmentType.Left, true, 255L, true, 5L, 3L, 0L, 0L, 0L, (long)Core.Enum.DesignType.TextWhite, (long)Core.Enum.DesignType.TextWhite, (long)Core.Enum.DesignType.TextWhite, true, 0L, Constant.NAME_LENGTH, ref argcallback_norm8, ref argcallback_hover8, ref argcallback_mousedown11, ref argcallback_mousemove11, ref argcallback_dblclick11, ref argcallback_enter1);
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            Action argcallback_enter2 = null;
            UpdateTextbox(Windows.Count, "txtRetypePassword", 67L, 127L, 142L, 19L, "", Core.Enum.FontType.Arial, Core.Enum.AlignmentType.Left, true, 255L, true, 5L, 3L, 0L, 0L, 0L, (long)Core.Enum.DesignType.TextWhite, (long)Core.Enum.DesignType.TextWhite, (long)Core.Enum.DesignType.TextWhite, true, 0L, Constant.NAME_LENGTH, ref argcallback_norm9, ref argcallback_hover9, ref argcallback_mousedown12, ref argcallback_mousemove12, ref argcallback_dblclick12, ref argcallback_enter2);
            // UpdateTextbox(Windows.Count, "txtCode", 67, 163, 142, 19, , FontType.Arial, , AlignmentType.Left, , , , , , DesignType.TextWhite, DesignType.TextWhite, DesignType.TextWhite, False)
            // UpdateTextbox(Windows.Count, "txtCaptcha", 67, 235, 142, 19, , FontType.Arial, , AlignmentType.Left, , , , , , DesignType.TextWhite, DesignType.TextWhite, DesignType.TextWhite, False)

            // UpdatePictureBox(Windows.Count, "picCaptcha", 67, 199, 156, 30, , , , , Tex_Captcha(GlobalCaptcha), Tex_Captcha(GlobalCaptcha), Tex_Captcha(GlobalCaptcha), DesignType.BlackOval, DesignType.BlackOval, DesignType.BlackOval)

            SetActiveControl(GetWindowIndex("winRegister"), GetControlIndex("winRegister", "txtUsername"));
        }

        public static void UpdateWindow_NewChar()
        {
            // Control window
            UpdateWindow("winNewChar", "Create Character", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 290L, 172L, 17L, false, 2L, 6L, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm);

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnNewChar_Cancel);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Enum.FontType.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 26L, 278L, 140L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.Parchment, (long)Core.Enum.DesignType.Parchment, (long)Core.Enum.DesignType.Parchment, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown1, ref argcallback_mousemove1, ref argcallback_dblclick1, ref argonDraw);

            // Name
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picShadow_1", 29L, 42L, 124L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, "", ref argcallback_norm1, ref argcallback_hover1, ref argcallback_mousedown2, ref argcallback_mousemove2, ref argcallback_dblclick2, ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblName", 29L, 39L, 124L, 10L, "Name", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm2, ref argcallback_hover2, ref argcallback_mousedown3, ref argcallback_mousemove3, ref argcallback_dblclick3, ref enabled);

            // Textbox
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_enter = null;
            UpdateTextbox(Windows.Count, "txtName", 29L, 55L, 124L, 19L, "", Core.Enum.FontType.Arial, Core.Enum.AlignmentType.Left, true, 255L, true, 5L, 3L, 0L, 0L, 0L, (long)Core.Enum.DesignType.TextWhite, (long)Core.Enum.DesignType.TextWhite, (long)Core.Enum.DesignType.TextWhite, false, 0L, Constant.NAME_LENGTH, ref argcallback_norm3, ref argcallback_hover3, ref argcallback_mousedown4, ref argcallback_mousemove4, ref argcallback_dblclick4, ref argcallback_enter);

            // Sex
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picShadow_2", 29L, 85L, 124L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, "", ref argcallback_norm4, ref argcallback_hover4, ref argcallback_mousedown5, ref argcallback_mousemove5, ref argcallback_dblclick5, ref argonDraw2);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            UpdateLabel(Windows.Count, "lblGender", 29L, 82L, 124L, 10L, "Gender", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm5, ref argcallback_hover5, ref argcallback_mousedown6, ref argcallback_mousemove6, ref argcallback_dblclick6, ref enabled);

            // Checkboxes
            var argcallback_mousedown7 = new Action(chkNewChar_Male);
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Gui.UpdateCheckBox(Windows.Count, "chkMale", 29L, 103L, 55L, 15L, 0L, "Male", Core.Enum.FontType.Arial, Core.Enum.AlignmentType.Center, true, 255L, (long)Core.Enum.DesignType.ChkNorm, 0L, false, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown7, ref argcallback_mousemove7, ref argcallback_dblclick7);
            var argcallback_mousedown8 = new Action(chkNewChar_Female);
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Gui.UpdateCheckBox(Windows.Count, "chkFemale", 90L, 103L, 62L, 15L, 0L, "Female", Core.Enum.FontType.Arial, Core.Enum.AlignmentType.Center, true, 255L, (long)Core.Enum.DesignType.ChkNorm, 0L, false, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown8, ref argcallback_mousemove8, ref argcallback_dblclick8);

            // Buttons
            var argcallback_mousedown9 = new Action(btnNewChar_Accept);
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Gui.UpdateButton(Windows.Count, "btnAccept", 29L, 127L, 60L, 24L, "Accept", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown9, ref argcallback_mousemove9, ref argcallback_dblclick9, 0L, 0L, "", false);
            var argcallback_mousedown10 = new Action(btnNewChar_Cancel);
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Gui.UpdateButton(Windows.Count, "btnCancel", 93L, 127L, 60L, 24L, "Cancel", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Red, (long)Core.Enum.DesignType.Red_Hover, (long)Core.Enum.DesignType.Red_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown10, ref argcallback_mousemove10, ref argcallback_dblclick10, 0L, 0L, "", false);

            // Sprite
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Windows.Count, "picShadow_3", 175L, 42L, 76L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, "", ref argcallback_norm6, ref argcallback_hover6, ref argcallback_mousedown11, ref argcallback_mousemove11, ref argcallback_dblclick11, ref argonDraw3);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            UpdateLabel(Windows.Count, "lblSprite", 175L, 39L, 76L, 10L, "Sprite", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm7, ref argcallback_hover7, ref argcallback_mousedown12, ref argcallback_mousemove12, ref argcallback_dblclick12, ref enabled);

            // Scene
            var argonDraw4 = new Action(NewChar_OnDraw);
            Gui.UpdatePictureBox(Windows.Count, "picScene", 165L, 55L, 96L, 96L, true, false, 255L, true, 11L, 11L, 11L, 0L, 0L, 0L, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, ref argonDraw4);

            // Buttons
            var argcallback_mousedown13 = new Action(btnNewChar_Left);
            Action argcallback_mousemove13 = null;
            Action argcallback_dblclick13 = null;
            Gui.UpdateButton(Windows.Count, "btnLeft", 163L, 40L, 10L, 13L, "", Core.Enum.FontType.Georgia, 0L, 12L, 14L, 16L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown13, ref argcallback_mousemove13, ref argcallback_dblclick13, 0L, 0L, "", false);
            var argcallback_mousedown14 = new Action(btnNewChar_Right);
            Action argcallback_mousemove14 = null;
            Action argcallback_dblclick14 = null;
            Gui.UpdateButton(Windows.Count, "btnRight", 252L, 40L, 10L, 13L, "", Core.Enum.FontType.Georgia, 0L, 13L, 15L, 17L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown14, ref argcallback_mousemove14, ref argcallback_dblclick14, 0L, 0L, "", false);

            // Set the active control
            SetActiveControl(GetWindowIndex("winNewChar"), GetControlIndex("winNewChar", "txtName"));
        }

        public static void UpdateWindow_Chars()
        {
            // Control the window
            UpdateWindow("winChars", "Characters", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 364L, 229L, 62L, false, 3L, 5L, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm);

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnCharacters_Close);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Enum.FontType.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_mousedown, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            Action argcallback_hover = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 26L, 352L, 197L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.Parchment, (long)Core.Enum.DesignType.Parchment, (long)Core.Enum.DesignType.Parchment, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown1, ref argcallback_mousemove1, ref argcallback_dblclick1, ref argonDraw);

            // Names
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            bool enabled = false;
            UpdatePictureBox(Windows.Count, "picShadow_1", 22L, 40L, 98L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, "", ref argcallback_norm1, ref argcallback_hover1, ref argcallback_mousedown2, ref argcallback_mousemove2, ref argcallback_dblclick2, ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            UpdateLabel(Windows.Count, "lblCharName_1", 22L, 37L, 98L, 10L, "Blank Slot", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm2, ref argcallback_hover2, ref argcallback_mousedown3, ref argcallback_mousemove3, ref argcallback_dblclick3, ref enabled);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picShadow_2", 132L, 40L, 98L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, "", ref argcallback_norm3, ref argcallback_hover3, ref argcallback_mousedown4, ref argcallback_mousemove4, ref argcallback_dblclick4, ref argonDraw2);
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            UpdateLabel(Windows.Count, "lblCharName_2", 132L, 37L, 98L, 10L, "Blank Slot", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm4, ref argcallback_hover4, ref argcallback_mousedown5, ref argcallback_mousemove5, ref argcallback_dblclick5, ref enabled);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Windows.Count, "picShadow_3", 242L, 40L, 98L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, "", ref argcallback_norm5, ref argcallback_hover5, ref argcallback_mousedown6, ref argcallback_mousemove6, ref argcallback_dblclick6, ref argonDraw3);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            UpdateLabel(Windows.Count, "lblCharName_3", 242L, 37L, 98L, 10L, "Blank Slot", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm6, ref argcallback_hover6, ref argcallback_mousedown7, ref argcallback_mousemove7, ref argcallback_dblclick7, ref enabled);

            // Scenery Boxes
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Action argonDraw4 = null;
            UpdatePictureBox(Windows.Count, "picScene_1", 23L, 55L, 96L, 96L, true, false, 255L, true, 11L, 11L, 11L, 0L, 0L, 0L, "", ref argcallback_norm7, ref argcallback_hover7, ref argcallback_mousedown8, ref argcallback_mousemove8, ref argcallback_dblclick8, ref argonDraw4);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Action argonDraw5 = null;
            UpdatePictureBox(Windows.Count, "picScene_2", 133L, 55L, 96L, 96L, true, false, 255L, true, 11L, 11L, 11L, 0L, 0L, 0L, "", ref argcallback_norm8, ref argcallback_hover8, ref argcallback_mousedown9, ref argcallback_mousemove9, ref argcallback_dblclick9, ref argonDraw5);
            var argonDraw6 = new Action(Chars_OnDraw);
            Gui.UpdatePictureBox(Windows.Count, "picScene_3", 243L, 55L, 96L, 96L, true, false, 255L, true, 11L, 11L, 11L, 0L, 0L, 0L, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown1, ref argcallback_mousemove1, ref argcallback_dblclick1, ref argonDraw6);

            // Control Buttons
            var argcallback_mousedown10 = new Action(btnAcceptChar_1);
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Gui.UpdateButton(Windows.Count, "btnSelectChar_1", 22L, 155L, 98L, 24L, "Select", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown10, ref argcallback_mousemove10, ref argcallback_dblclick10, 0L, 0L, "", false);
            var argcallback_mousedown11 = new Action(btnCreateChar_1);
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Gui.UpdateButton(Windows.Count, "btnCreateChar_1", 22L, 155L, 98L, 24L, "Create", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown11, ref argcallback_mousemove11, ref argcallback_dblclick11, 0L, 0L, "", false);
            var argcallback_mousedown12 = new Action(btnDelChar_1);
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            Gui.UpdateButton(Windows.Count, "btnDelChar_1", 22L, 183L, 98L, 24L, "Delete", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Red, (long)Core.Enum.DesignType.Red_Hover, (long)Core.Enum.DesignType.Red_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown12, ref argcallback_mousemove12, ref argcallback_dblclick12, 0L, 0L, "", false);
            var argcallback_mousedown13 = new Action(btnAcceptChar_2);
            Action argcallback_mousemove13 = null;
            Action argcallback_dblclick13 = null;
            Gui.UpdateButton(Windows.Count, "btnSelectChar_2", 132L, 155L, 98L, 24L, "Select", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown13, ref argcallback_mousemove13, ref argcallback_dblclick13, 0L, 0L, "", false);
            var argcallback_mousedown14 = new Action(btnCreateChar_2);
            Action argcallback_mousemove14 = null;
            Action argcallback_dblclick14 = null;
            Gui.UpdateButton(Windows.Count, "btnCreateChar_2", 132L, 155L, 98L, 24L, "Create", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown14, ref argcallback_mousemove14, ref argcallback_dblclick14, 0L, 0L, "", false);
            var argcallback_mousedown15 = new Action(btnDelChar_2);
            Action argcallback_mousemove15 = null;
            Action argcallback_dblclick15 = null;
            Gui.UpdateButton(Windows.Count, "btnDelChar_2", 132L, 183L, 98L, 24L, "Delete", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Red, (long)Core.Enum.DesignType.Red_Hover, (long)Core.Enum.DesignType.Red_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown15, ref argcallback_mousemove15, ref argcallback_dblclick15, 0L, 0L, "", false);
            var argcallback_mousedown16 = new Action(btnAcceptChar_3);
            Action argcallback_mousemove16 = null;
            Action argcallback_dblclick16 = null;
            Gui.UpdateButton(Windows.Count, "btnSelectChar_3", 242L, 155L, 98L, 24L, "Select", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown16, ref argcallback_mousemove16, ref argcallback_dblclick16, 0L, 0L, "", false);
            var argcallback_mousedown17 = new Action(btnCreateChar_3);
            Action argcallback_mousemove17 = null;
            Action argcallback_dblclick17 = null;
            Gui.UpdateButton(Windows.Count, "btnCreateChar_3", 242L, 155L, 98L, 24L, "Create", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown17, ref argcallback_mousemove17, ref argcallback_dblclick17, 0L, 0L, "", false);
            var argcallback_mousedown18 = new Action(btnDelChar_3);
            Action argcallback_mousemove18 = null;
            Action argcallback_dblclick18 = null;
            Gui.UpdateButton(Windows.Count, "btnDelChar_3", 242L, 183L, 98L, 24L, "Delete", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Red, (long)Core.Enum.DesignType.Red_Hover, (long)Core.Enum.DesignType.Red_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown18, ref argcallback_mousemove18, ref argcallback_dblclick18, 0L, 0L, "", false);
        }

        public static void UpdateWindow_Jobs()
        {
            // Control window
            UpdateWindow("winJobs", "Select Job", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 364L, 229L, 17L, false, 2L, 6L, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm);

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnJobs_Close);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Enum.FontType.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            var argonDraw = new Action(Jobs_DrawFace);
            Gui.UpdatePictureBox(Windows.Count, "picParchment", 6L, 26L, 352L, 197L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.Parchment, (long)Core.Enum.DesignType.Parchment, (long)Core.Enum.DesignType.Parchment, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, ref argonDraw);

            // Job Name
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picShadow", 183L, 42L, 98L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, (long)Core.Enum.DesignType.BlackOval, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown1, ref argcallback_mousemove1, ref argcallback_dblclick1, ref argonDraw1);
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblClassName", 183L, 39L, 98L, 10L, "Warrior", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, true, 255L, false, false, ref argcallback_norm1, ref argcallback_hover1, ref argcallback_mousedown2, ref argcallback_mousemove2, ref argcallback_dblclick2, ref enabled);

            // Select Buttons
            var argcallback_mousedown3 = new Action(btnJobs_Left);
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Gui.UpdateButton(Windows.Count, "btnLeft", 170L, 40L, 10L, 13L, "", Core.Enum.FontType.Georgia, 0L, 12L, 14L, 16L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown3, ref argcallback_mousemove3, ref argcallback_dblclick3, 0L, 0L, "", false);

            var argcallback_mousedown4 = new Action(btnJobs_Right);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Gui.UpdateButton(Windows.Count, "btnRight", 282L, 40L, 10L, 13L, "", Core.Enum.FontType.Georgia, 0L, 13L, 15L, 17L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown4, ref argcallback_mousemove4, ref argcallback_dblclick4, 0L, 0L, "", false);

            // Accept Button
            var argcallback_mousedown5 = new Action(btnJobs_Accept);
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Gui.UpdateButton(Windows.Count, "btnAccept", 183L, 185L, 98L, 22L, "Accept", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown5, ref argcallback_mousemove5, ref argcallback_dblclick5, 0L, 0L, "", false);

            // Text background
            Action argcallback_hover2 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picBackground", 127L, 55L, 210L, 124L, true, false, 255L, true, 0L, 0L, 0L, (long)Core.Enum.DesignType.TextBlack, (long)Core.Enum.DesignType.TextBlack, (long)Core.Enum.DesignType.TextBlack, "", ref argcallback_norm, ref argcallback_hover2, ref argcallback_mousedown6, ref argcallback_mousemove6, ref argcallback_dblclick6, ref argonDraw2);

            // Overlay
            var argonDraw3 = new Action(Jobs_DrawText);
            Gui.UpdatePictureBox(Windows.Count, "picOverlay", 6L, 26L, 0L, 0L, true, false, 255L, true, 0L, 0L, 0L, 0L, 0L, 0L, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, ref argonDraw3);
        }

        public static void UpdateWindow_Dialogue()
        {
            // Control dialogue window
            UpdateWindow("winDialogue", "Warning", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 348L, 145L, 38L, false, 3L, 5L, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm, (long)Core.Enum.DesignType.Win_Norm, canDrag: false);

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnDialogue_Close);
            Action argcallback_norm = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Enum.FontType.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 26L, 335L, 113L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // Header
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picShadow", 103L, 44L, 144L, 9L, design_norm: (long)Core.Enum.DesignType.BlackOval, design_hover: (long)Core.Enum.DesignType.BlackOval, design_mousedown: (long)Core.Enum.DesignType.BlackOval, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblHeader", 103L, 40L, 144L, 10L, "Header", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);

            // Input
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_enter = null;
            UpdateTextbox(Windows.Count, "txtInput", 93L, 75L, 162L, 18L, font: Core.Enum.FontType.Arial, align: Core.Enum.AlignmentType.Center, xOffset: 5L, yOffset: 2L, design_norm: (long)Core.Enum.DesignType.TextBlack, design_hover: (long)Core.Enum.DesignType.TextBlack, design_mousedown: (long)Core.Enum.DesignType.TextBlack, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, callback_enter: ref argcallback_enter);

            // Labels
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            UpdateLabel(Windows.Count, "lblBody_1", 15L, 60L, 314L, 10L, "Invalid username or password.", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, enabled: ref enabled);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            UpdateLabel(Windows.Count, "lblBody_2", 15L, 75L, 314L, 10L, "Please try again!", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, enabled: ref enabled);

            // Buttons
            var argcallback_mousedown7 = new Action(Dialogue_Yes);
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Gui.UpdateButton(Windows.Count, "btnYes", 104L, 98L, 68L, 24L, "Yes", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown7, ref argcallback_dblclick7, ref argcallback_mousemove7, 0L, 0L, "", false);
            var argcallback_mousedown8 = new Action(Dialogue_No);
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Gui.UpdateButton(Windows.Count, "btnNo", 180L, 98L, 68L, 24L, "No", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)Core.Enum.DesignType.Red, (long)Core.Enum.DesignType.Red_Hover, (long)Core.Enum.DesignType.Red_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown8, ref argcallback_mousemove8, ref argcallback_dblclick8, 0L, 0L, "", false);
            var argcallback_mousedown9 = new Action(Dialogue_Okay);
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Gui.UpdateButton(Windows.Count, "btnOkay", 140L, 98L, 68L, 24L, "Okay", Core.Enum.FontType.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown9, ref argcallback_mousemove9, ref argcallback_dblclick9, 0L, 0L, "", false);

            // Set active control
            SetActiveControl(Windows.Count, GetControlIndex("winDialogue", "txtInput"));
        }

        public static void UpdateWindow_Party()
        {
            // Control window
            UpdateWindow("winParty", "", Core.Enum.FontType.Georgia, zOrder_Win, 4L, 78L, 252L, 158L, 0L, false, design_norm: (long)Core.Enum.DesignType.Win_Party, design_hover: (long)Core.Enum.DesignType.Win_Party, design_mousedown: (long)Core.Enum.DesignType.Win_Party, canDrag: false);

            // Name labels
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblName1", 60L, 20L, 173L, 10L, "Richard - Level 10", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, enabled: ref enabled);
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            UpdateLabel(Windows.Count, "lblName2", 60L, 60L, 173L, 10L, "Anna - Level 18", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, enabled: ref enabled);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            UpdateLabel(Windows.Count, "lblName3", 60L, 100L, 173L, 10L, "Doleo - Level 25", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, enabled: ref enabled);

            // Empty Bars - HP
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picEmptyBar_HP1", 58L, 34L, 173L, 9L, image_norm: 62L, image_hover: 62L, image_mousedown: 62L, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw);
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picEmptyBar_HP2", 58L, 74L, 173L, 9L, image_norm: 62L, image_hover: 62L, image_mousedown: 62L, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw1);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picEmptyBar_HP3", 58L, 114L, 173L, 9L, image_norm: 62L, image_hover: 62L, image_mousedown: 62L, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, onDraw: ref argonDraw2);

            // Empty Bars - SP
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Windows.Count, "picEmptyBar_SP1", 58L, 44L, 173L, 9L, image_norm: 63L, image_hover: 63L, image_mousedown: 63L, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, onDraw: ref argonDraw3);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argonDraw4 = null;
            UpdatePictureBox(Windows.Count, "picEmptyBar_SP2", 58L, 84L, 173L, 9L, image_norm: 63L, image_hover: 63L, image_mousedown: 63L, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, onDraw: ref argonDraw4);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Action argonDraw5 = null;
            UpdatePictureBox(Windows.Count, "picEmptyBar_SP3", 58L, 124L, 173L, 9L, image_norm: 63L, image_hover: 63L, image_mousedown: 63L, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, onDraw: ref argonDraw5);

            // Filled bars - HP
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Action argonDraw6 = null;
            UpdatePictureBox(Windows.Count, "picBar_HP1", 58L, 34L, 173L, 9L, image_norm: 64L, image_hover: 64L, image_mousedown: 64L, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, onDraw: ref argonDraw6);
            Action argcallback_norm10 = null;
            Action argcallback_hover10 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Action argonDraw7 = null;
            UpdatePictureBox(Windows.Count, "picBar_HP2", 58L, 74L, 173L, 9L, image_norm: 64L, image_hover: 64L, image_mousedown: 64L, callback_norm: ref argcallback_norm10, callback_hover: ref argcallback_hover10, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, onDraw: ref argonDraw7);
            Action argcallback_norm11 = null;
            Action argcallback_hover11 = null;
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Action argonDraw8 = null;
            UpdatePictureBox(Windows.Count, "picBar_HP3", 58L, 114L, 173L, 9L, image_norm: 64L, image_hover: 64L, image_mousedown: 64L, callback_norm: ref argcallback_norm11, callback_hover: ref argcallback_hover11, callback_mousedown: ref argcallback_mousedown11, callback_mousemove: ref argcallback_mousemove11, callback_dblclick: ref argcallback_dblclick11, onDraw: ref argonDraw8);

            // Filled bars - SP
            Action argcallback_norm12 = null;
            Action argcallback_hover12 = null;
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            Action argonDraw9 = null;
            UpdatePictureBox(Windows.Count, "picBar_SP1", 58L, 44L, 173L, 9L, image_norm: 65L, image_hover: 65L, image_mousedown: 65L, callback_norm: ref argcallback_norm12, callback_hover: ref argcallback_hover12, callback_mousedown: ref argcallback_mousedown12, callback_mousemove: ref argcallback_mousemove12, callback_dblclick: ref argcallback_dblclick12, onDraw: ref argonDraw9);
            Action argcallback_norm13 = null;
            Action argcallback_hover13 = null;
            Action argcallback_mousedown13 = null;
            Action argcallback_mousemove13 = null;
            Action argcallback_dblclick13 = null;
            Action argonDraw10 = null;
            UpdatePictureBox(Windows.Count, "picBar_SP2", 58L, 84L, 173L, 9L, image_norm: 65L, image_hover: 65L, image_mousedown: 65L, callback_norm: ref argcallback_norm13, callback_hover: ref argcallback_hover13, callback_mousedown: ref argcallback_mousedown13, callback_mousemove: ref argcallback_mousemove13, callback_dblclick: ref argcallback_dblclick13, onDraw: ref argonDraw10);
            Action argcallback_norm14 = null;
            Action argcallback_hover14 = null;
            Action argcallback_mousedown14 = null;
            Action argcallback_mousemove14 = null;
            Action argcallback_dblclick14 = null;
            Action argonDraw11 = null;
            UpdatePictureBox(Windows.Count, "picBar_SP3", 58L, 124L, 173L, 9L, image_norm: 65L, image_hover: 65L, image_mousedown: 65L, callback_norm: ref argcallback_norm14, callback_hover: ref argcallback_hover14, callback_mousedown: ref argcallback_mousedown14, callback_mousemove: ref argcallback_mousemove14, callback_dblclick: ref argcallback_dblclick14, onDraw: ref argonDraw11);

            // Shadows
            // UpdatePictureBox(Windows.Count, "picShadow1", 20, 24, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow
            // UpdatePictureBox Windows.Count, "picShadow2", 20, 64, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow
            // UpdatePictureBox Windows.Count, "picShadow3", 20, 104, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow

            // Characters
            Action argcallback_norm15 = null;
            Action argcallback_hover15 = null;
            Action argcallback_mousedown15 = null;
            Action argcallback_mousemove15 = null;
            Action argcallback_dblclick15 = null;
            Action argonDraw12 = null;
            UpdatePictureBox(Windows.Count, "picChar1", 20L, 20L, 32L, 32L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, texturePath: Path.Characters, callback_norm: ref argcallback_norm15, callback_hover: ref argcallback_hover15, callback_mousedown: ref argcallback_mousedown15, callback_mousemove: ref argcallback_mousemove15, callback_dblclick: ref argcallback_dblclick15, onDraw: ref argonDraw12);
            Action argcallback_norm16 = null;
            Action argcallback_hover16 = null;
            Action argcallback_mousedown16 = null;
            Action argcallback_mousemove16 = null;
            Action argcallback_dblclick16 = null;
            Action argonDraw13 = null;
            UpdatePictureBox(Windows.Count, "picChar2", 20L, 60L, 32L, 32L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, texturePath: Path.Characters, callback_norm: ref argcallback_norm16, callback_hover: ref argcallback_hover16, callback_mousedown: ref argcallback_mousedown16, callback_mousemove: ref argcallback_mousemove16, callback_dblclick: ref argcallback_dblclick16, onDraw: ref argonDraw13);
            Action argcallback_norm17 = null;
            Action argcallback_hover17 = null;
            Action argcallback_mousedown17 = null;
            Action argcallback_mousemove17 = null;
            Action argcallback_dblclick17 = null;
            Action argonDraw14 = null;
            UpdatePictureBox(Windows.Count, "picChar3", 20L, 100L, 32L, 32L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, texturePath: Path.Characters, callback_norm: ref argcallback_norm17, callback_hover: ref argcallback_hover17, callback_mousedown: ref argcallback_mousedown17, callback_mousemove: ref argcallback_mousemove17, callback_dblclick: ref argcallback_dblclick17, onDraw: ref argonDraw14);
        }

        public static void UpdateWindow_Trade()
        {
            // Control window
            UpdateWindow("winTrade", "Trading with [Name]", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 412L, 386L, 112L, false, 2L, 5L, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, onDraw: new Action(DrawTrade));

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Close Button
            var argcallback_mousedown = new Action(btnTrade_Close);
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 36L, 36L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 10L, 312L, 392L, 66L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // Labels
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            bool enabled = false;
            UpdatePictureBox(Windows.Count, "picShadow", 36L, 30L, 142L, 9L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw1);
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            UpdateLabel(Windows.Count, "lblYourTrade", 36L, 27L, 142L, 9L, "Robin's Offer", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picShadow", 36 + 200, 30L, 142L, 9L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw2);
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            UpdateLabel(Windows.Count, "lblTheirTrade", 36 + 200, 27L, 142L, 9L, "Richard's Offer", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, enabled: ref enabled);

            // Buttons
            var argcallback_mousedown6 = new Action(btnTrade_Accept);
            Gui.UpdateButton(Windows.Count, "btnAccept", 134L, 340L, 68L, 24L, "Accept", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown6, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            var argcallback_mousedown7 = new Action(btnTrade_Close);
            Gui.UpdateButton(Windows.Count, "btnDecline", 210L, 340L, 68L, 24L, "Decline", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.Red, design_hover: (long)Core.Enum.DesignType.Red_Hover, design_mousedown: (long)Core.Enum.DesignType.Red_Click, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown7, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Labels
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            UpdateLabel(Windows.Count, "lblStatus", 114L, 322L, 184L, 10L, "", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, enabled: ref enabled);

            // Amounts
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            UpdateLabel(Windows.Count, "lblBlank", 25L, 330L, 100L, 10L, "Total Value", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, enabled: ref enabled);
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            UpdateLabel(Windows.Count, "lblBlank", 285L, 330L, 100L, 10L, "Total Value", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, enabled: ref enabled);
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            UpdateLabel(Windows.Count, "lblYourValue", 25L, 344L, 100L, 10L, "52,812g", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown11, callback_mousemove: ref argcallback_mousemove11, callback_dblclick: ref argcallback_dblclick11, enabled: ref enabled);
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            UpdateLabel(Windows.Count, "lblTheirValue", 285L, 344L, 100L, 10L, "12,531g", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown12, callback_mousemove: ref argcallback_mousemove12, callback_dblclick: ref argcallback_dblclick12, enabled: ref enabled);

            // Item Containers
            var argcallback_mousedown13 = new Action(TradeMouseMove_Your);
            var argcallback_mousemove13 = new Action(TradeMouseMove_Your);
            var argcallback_dblclick13 = new Action(TradeDblClick_Your);
            var argonDraw3 = new Action(DrawYourTrade);
            Gui.UpdatePictureBox(Windows.Count, "picYour", 14L, 46L, 184L, 260L, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown13, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove13, callback_dblclick: ref argcallback_dblclick13, onDraw: ref argonDraw3);
            var argcallback_mousedown14 = new Action(TradeMouseMove_Their);
            var argcallback_mousemove14 = new Action(TradeMouseMove_Their);
            var argcallback_dblclick14 = new Action(TradeMouseMove_Their);
            var argonDraw4 = new Action(DrawTheirTrade);
            Gui.UpdatePictureBox(Windows.Count, "picTheir", 214L, 46L, 184L, 260L, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown14, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove14, callback_dblclick: ref argcallback_dblclick14, onDraw: ref argonDraw4);
        }

        // Rendering & Initialisation
        public static void Init()
        {
            // Starter values
            zOrder_Win = 0L;
            zOrder_Con = 0L;

            // Menu
            UpdateWindow_Register();
            UpdateWindow_Login();
            UpdateWindow_NewChar();
            UpdateWindow_Jobs();
            UpdateWindow_Chars();
            UpdateWindow_ChatSmall();
            UpdateWindow_Chat();
            UpdateWindow_Menu();
            UpdateWindow_Description();
            UpdateWindow_Inventory();
            UpdateWindow_Skills();
            UpdateWindow_Character();
            UpdateWindow_Hotbar();
            UpdateWindow_Bank();
            UpdateWindow_Shop();
            UpdateWindow_EscMenu();
            UpdateWindow_Bars();
            UpdateWindow_Dialogue();
            UpdateWindow_DragBox();
            UpdateWindow_Options();
            UpdateWindow_Trade();
            UpdateWindow_Party();
            UpdateWindow_PlayerMenu();
            UpdateWindow_RightClick();
            UpdateWindow_Combobox();
        }

        public static bool HandleInterfaceEvents(Core.Enum.EntState entState)
        {
            long i;
            var curWindow = default(long);
            var curControl = default(long);
            Action callBack;

            // Check for MouseDown to start the drag timer
            if (GameClient.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && GameClient.PreviousMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                dragTimer.Restart(); // Start the timer on initial mouse down
                canDrag = false; // Reset drag flag to ensure it doesn't drag immediately
            }

            // Check for MouseUp to reset dragging
            if (GameClient.CurrentMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                canDrag = false;
                dragTimer.Reset(); // Stop the timer on mouse up
            }

            // Enable dragging if the mouse has been held down for the specified interval
            if (!canDrag && dragTimer.ElapsedMilliseconds >= dragInterval)
            {
                canDrag = true;
            }

            lock (GameClient.InputLock)
            {
                // Find the container
                var loopTo = Windows.Count;
                for (i = 1L; i < loopTo; i++)
                {
                    var withBlock = Windows[i];
                    if (withBlock.Enabled && withBlock.Visible)
                    {
                        if (withBlock.State != Core.Enum.EntState.MouseDown)
                            withBlock.State = Core.Enum.EntState.Normal;

                        if (GameState.CurMouseX >= withBlock.Left && GameState.CurMouseX <= withBlock.Width + withBlock.Left && GameState.CurMouseY >= withBlock.Top && GameState.CurMouseY <= withBlock.Height + withBlock.Top)
                        {
                            // Handle combo menu logic
                            if (withBlock.Design[0] == (long)Core.Enum.DesignType.ComboMenuNorm)
                            {
                                if (entState == Core.Enum.EntState.MouseMove || entState == Core.Enum.EntState.Hover)
                                {
                                    ComboMenu_MouseMove(i);
                                }
                                else if (entState == Core.Enum.EntState.MouseDown)
                                {
                                    ComboMenu_MouseDown(i);
                                }
                            }

                            // Track the top-most window
                            if (curWindow == 0L || withBlock.zOrder > Windows[curWindow].zOrder)
                            {
                                curWindow = i;
                            }
                        }

                        // Handle window dragging only if dragging is enabled
                        if (entState == Core.Enum.EntState.MouseMove && withBlock.CanDrag && canDrag && GameClient.IsMouseButtonDown(Core.Enum.MouseButton.Left))
                        {
                            withBlock = Windows[ActiveWindow];
                            withBlock.Left = GameLogic.Clamp((int)(withBlock.Left + (GameState.CurMouseX - withBlock.Left - withBlock.MovedX)), 0, (int)(GameState.ResolutionWidth - withBlock.Width));
                            withBlock.Top = GameLogic.Clamp((int)(withBlock.Top + (GameState.CurMouseY - withBlock.Top - withBlock.MovedY)), 0, (int)(GameState.ResolutionHeight - withBlock.Height));
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
                        for (i = 0L; i <= loopTo1; i++)
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

                                // Handle control dragging only if dragging is enabled
                                if (entState == Core.Enum.EntState.MouseMove && withBlock1.CanDrag && canDrag && GameClient.IsMouseButtonDown(Core.Enum.MouseButton.Left))
                                {
                                    withBlock1.Left = GameLogic.Clamp((int)(withBlock1.Left + (GameState.CurMouseX - withBlock1.Left - withBlock1.MovedX)), 0, (int)(Windows[curWindow].Width - withBlock1.Width));
                                    withBlock1.Top = GameLogic.Clamp((int)(withBlock1.Top + (GameState.CurMouseY - withBlock1.Top - withBlock1.MovedY)), 0, (int)(Windows[curWindow].Height - withBlock1.Height));
                                }
                            }
                        }
                    }

                    // Handle active control
                    if (curControl > 0L)
                    {
                        var withBlock2 = Windows[curWindow].Controls[(int)curControl];
                        // Handle hover state separately
                        if (entState == Core.Enum.EntState.MouseMove)
                        {
                            withBlock2.State = Core.Enum.EntState.Hover;
                        }
                        else if (entState == Core.Enum.EntState.MouseDown)
                        {
                            withBlock2.State = Core.Enum.EntState.MouseDown;
                        }

                        if (GameClient.IsMouseButtonDown(Core.Enum.MouseButton.Left) && withBlock2.CanDrag)
                        {
                            withBlock2.MovedX = GameState.CurMouseX - withBlock2.Left;
                            withBlock2.MovedY = GameState.CurMouseY - withBlock2.Top;
                        }

                        // Handle specific control types
                        switch (withBlock2.Type)
                        {
                            case Core.Enum.ControlType.Checkbox:
                                {
                                    if (withBlock2.Group > 0L && withBlock2.Value == 0L)
                                    {
                                        var loopTo2 = (long)(Windows[curWindow].Controls.Count - 1);
                                        for (i = 0L; i <= loopTo2; i++)
                                        {
                                            if (Windows[curWindow].Controls[(int)i].Type == Core.Enum.ControlType.Checkbox && Windows[curWindow].Controls[(int)i].Group == withBlock2.Group)
                                            {
                                                Windows[curWindow].Controls[(int)i].Value = 0L;
                                            }
                                        }
                                        withBlock2.Value = 0L;
                                    }
                                    else
                                    {
                                        withBlock2.Value = withBlock2.Value == 0L ? 1 : 0;
                                    }

                                    break;
                                }

                            case Core.Enum.ControlType.Combobox:
                                {
                                    ShowComboMenu(curWindow, curControl);
                                    break;
                                }
                        }

                        if (GameClient.IsMouseButtonDown(Core.Enum.MouseButton.Left))
                        {
                            SetActiveControl(curWindow, curControl);
                        }

                        callBack = withBlock2.CallBack[(int)entState];

                        // Execute the callback if it exists
                        callBack?.Invoke();
                    }
                }

                // Reset mouse state on MouseUp
                if (GameClient.IsMouseButtonUp(Core.Enum.MouseButton.Left))
                    ResetMouseDown();
            }

            return true;
        }

        public static void ResetInterface()
        {
            long i;
            long x;

            var loopTo = Windows.Count - 1;
            for (i = 0L; i < loopTo; i++)
            {
                if (Windows[i].State != Core.Enum.EntState.MouseDown)
                    Windows[i].State = Core.Enum.EntState.Normal;

                var loopTo1 = (long)(Windows[i].Controls.Count - 1);
                for (x = 0L; x <= loopTo1; x++)
                {
                    if (Windows[i].Controls[(int)x].State != Core.Enum.EntState.MouseDown)
                        Windows[i].Controls[(int)x].State = Core.Enum.EntState.Normal;
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
                var loopTo = Windows.Count;
                for (i = 1L; i < loopTo; i++)
                {
                    {
                        var withBlock = Windows[i];
                        // Only reset the state if it was in MouseDown
                        if (withBlock.State == Core.Enum.EntState.MouseDown)
                        {
                            withBlock.State = Core.Enum.EntState.Normal;
                            callBack = withBlock.CallBack[(int)Core.Enum.EntState.Normal];
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
                                if (control.State == Core.Enum.EntState.MouseDown)
                                {
                                    control.State = Core.Enum.EntState.Normal;

                                    callBack = control.CallBack[(int)control.State];
                                    if (callBack is not null)
                                        callBack?.Invoke();
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void Render()
        {
            // Exit if no windows are present
            if (Windows.Count == 0)
                return;

            // Reset Z-order
            long curZOrder = 0L;

            // Loop through each window based on Z-order
            var loopTo = Windows.Count - 1;
            for (curZOrder = 0L; curZOrder <= loopTo; curZOrder++)
            {
                for (int i = 1, loopTo1 = Windows.Count; i <= loopTo1; i++)
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
            if ((winNum < 0L | winNum >= Windows.Count || entNum < 0L) | entNum > Windows[winNum].Controls.Count - 1)
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
                    case Core.Enum.ControlType.PictureBox:
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

                    case Core.Enum.ControlType.TextBox:
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
                                taddText = GameState.chatShowLine;
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

                    case Core.Enum.ControlType.Button:
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

                    case Core.Enum.ControlType.Label:
                        {
                            if (Strings.Len(withBlock.Text) > 0 & withBlock.Font > 0)
                            {
                                switch (withBlock.Align)
                                {
                                    case Core.Enum.AlignmentType.Left:
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

                                    case Core.Enum.AlignmentType.Right:
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

                                    case Core.Enum.AlignmentType.Center:
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
                    case Core.Enum.ControlType.Checkbox:
                        {
                            switch (withBlock.Design[0])
                            {
                                case (long)Core.Enum.DesignType.ChkNorm:
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
                                            case Core.Enum.AlignmentType.Left:
                                                {
                                                    left = withBlock.Left + 18L + xO;
                                                    break;
                                                }
                                            case Core.Enum.AlignmentType.Right:
                                                {
                                                    left = withBlock.Left + 18L + (withBlock.Width - 18L) - Text.GetTextWidth(withBlock.Text, withBlock.Font) + xO;
                                                    break;
                                                }
                                            case Core.Enum.AlignmentType.Center:
                                                {
                                                    left = (long)Math.Round(withBlock.Left + 18L + (withBlock.Width - 18L) / 2d - Text.GetTextWidth(withBlock.Text, withBlock.Font) / 2d + xO);
                                                    break;
                                                }
                                        }

                                        // render text
                                        Text.RenderText(withBlock.Text, (int)left, (int)(withBlock.Top + yO), withBlock.Color, Color.Black);
                                        break;
                                    }

                                case (long)Core.Enum.DesignType.ChkChat:
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

                                case (long)Core.Enum.DesignType.ChkBuying:
                                    {
                                        if (withBlock.Value == 0L)
                                            sprite = 58L;
                                        else
                                            sprite = 56L;
                                        string argpath6 = System.IO.Path.Combine(withBlock.Texture[0], sprite.ToString());
                                        GameClient.RenderTexture(ref argpath6, (int)(withBlock.Left + xO), (int)(withBlock.Top + yO), 0, 0, 49, 20, 49, 20);
                                        break;
                                    }

                                case (long)Core.Enum.DesignType.ChkSelling:
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
                    case Core.Enum.ControlType.Combobox:
                        {
                            switch (withBlock.Design[0])
                            {
                                case (long)Core.Enum.DesignType.ComboNorm:
                                    {
                                        // draw the background
                                        RenderDesign((long)Core.Enum.DesignType.TextBlack, withBlock.Left + xO, withBlock.Top + yO, withBlock.Width, withBlock.Height);

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
            if (winNum < 0L || winNum >= Windows.Count)
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
                    case (long)Core.Enum.DesignType.ComboMenuNorm:
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
                    case (long)Core.Enum.DesignType.Win_Black:
                        {
                            string argpath2 = System.IO.Path.Combine(Path.Gui, "61");
                            GameClient.RenderTexture(ref argpath2, (int)withBlock.Left, (int)withBlock.Top, 0, 0, (int)withBlock.Width, (int)withBlock.Height, 190, 255, 255, 255);
                            break;
                        }

                    case (long)Core.Enum.DesignType.Win_Norm:
                        {
                            RenderDesign((long)Core.Enum.DesignType.Wood, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            RenderDesign((long)Core.Enum.DesignType.Green, withBlock.Left, withBlock.Top, withBlock.Width, 23L);
                            string argpath3 = System.IO.Path.Combine(Path.Items, withBlock.Icon.ToString());
                            GameClient.RenderTexture(ref argpath3, (int)(withBlock.Left + withBlock.xOffset), (int)(withBlock.Top - 16L + withBlock.yOffset), 0, 0, (int)withBlock.Width, (int)withBlock.Height, (int)withBlock.Width, (int)withBlock.Height);
                            Text.RenderText(withBlock.Text, (int)(withBlock.Left + 32L), (int)(withBlock.Top + 4L), Color.White, Color.Black);
                            break;
                        }

                    case (long)Core.Enum.DesignType.Win_NoBar:
                        {
                            RenderDesign((long)Core.Enum.DesignType.Wood, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            break;
                        }

                    case (long)Core.Enum.DesignType.Win_Empty:
                        {
                            RenderDesign((long)Core.Enum.DesignType.Wood_Empty, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            RenderDesign((long)Core.Enum.DesignType.Green, withBlock.Left, withBlock.Top, withBlock.Width, 23L);
                            string argpath4 = System.IO.Path.Combine(Path.Items, withBlock.Icon.ToString());
                            GameClient.RenderTexture(ref argpath4, (int)(withBlock.Left + withBlock.xOffset), (int)(withBlock.Top - 16L + withBlock.yOffset), 0, 0, (int)withBlock.Width, (int)withBlock.Height, (int)withBlock.Width, (int)withBlock.Height);
                            Text.RenderText(withBlock.Text, (int)(withBlock.Left + 32L), (int)(withBlock.Top + 4L), Color.White, Color.Black);
                            break;
                        }

                    case (long)Core.Enum.DesignType.Win_Desc:
                        {
                            RenderDesign((long)Core.Enum.DesignType.Win_Desc, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            break;
                        }

                    case (long)Core.Enum.DesignType.Win_Shadow:
                        {
                            RenderDesign((long)Core.Enum.DesignType.Win_Shadow, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
                            break;
                        }

                    case (long)Core.Enum.DesignType.Win_Party:
                        {
                            RenderDesign((long)Core.Enum.DesignType.Win_Party, withBlock.Left, withBlock.Top, withBlock.Width, withBlock.Height);
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
                case (long)Core.Enum.DesignType.MenuHeader:
                    {
                        // render the header
                        string argpath = System.IO.Path.Combine(Path.Designs, "61");
                        GameClient.RenderTexture(ref argpath, (int)left, (int)top, 0, 0, (int)width, (int)height, (int)width, (int)height, 200, 47, 77, 29);
                        break;
                    }

                case (long)Core.Enum.DesignType.MenuOption:
                    {
                        // render the option
                        string argpath1 = System.IO.Path.Combine(Path.Designs, "61");
                        GameClient.RenderTexture(ref argpath1, (int)left, (int)top, 0, 0, (int)width, (int)height, (int)width, (int)height, 200, 98, 98, 98);
                        break;
                    }

                case (long)Core.Enum.DesignType.Wood:
                    {
                        bs = 4L;
                        // render the wood box
                        RenderControl_Square(1, left, top, width, height, bs, alpha);

                        // render wood texture
                        string argpath2 = System.IO.Path.Combine(Path.Gui, "1");
                        GameClient.RenderTexture(ref argpath2, (int)(left + bs), (int)(top + bs), 100, 100, (int)(width - bs * 2L), (int)(height - bs * 2L), (int)(width - bs * 2L), (int)(height - bs * 2L), (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Wood_Small:
                    {
                        bs = 2L;
                        // render the wood box
                        RenderControl_Square(8, left + bs, top + bs, width, height, bs, alpha);

                        // render wood texture
                        string argpath3 = System.IO.Path.Combine(Path.Gui, "1");
                        GameClient.RenderTexture(ref argpath3, (int)(left + bs), (int)(top + bs), 100, 100, (int)(width - bs * 2L), (int)(height - bs * 2L), (int)(width - bs * 2L), (int)(height - bs * 2L), (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Wood_Empty:
                    {
                        bs = 4L;
                        // render the wood box
                        RenderControl_Square(9, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Green:
                    {
                        bs = 2L;
                        // render the green box
                        RenderControl_Square(2, left, top, width, height, bs, alpha);

                        // render green gradient overlay
                        string argpath4 = System.IO.Path.Combine(Path.Gradients, "1");
                        GameClient.RenderTexture(ref argpath4, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Green_Hover:
                    {
                        bs = 2L;
                        // render the green box
                        RenderControl_Square(2, left, top, width, height, bs, alpha);

                        // render green gradient overlay
                        string argpath5 = System.IO.Path.Combine(Path.Gradients, "2");
                        GameClient.RenderTexture(ref argpath5, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Green_Click:
                    {
                        bs = 2L;
                        // render the green box
                        RenderControl_Square(2, left, top, width, height, bs, alpha);

                        // render green gradient overlay
                        string argpath6 = System.IO.Path.Combine(Path.Gradients, "3");
                        GameClient.RenderTexture(ref argpath6, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Red:
                    {
                        bs = 2L;
                        // render the red box
                        RenderControl_Square(3, left, top, width, height, bs, alpha);

                        // render red gradient overlay
                        string argpath7 = System.IO.Path.Combine(Path.Gradients, "4");
                        GameClient.RenderTexture(ref argpath7, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Red_Hover:
                    {
                        bs = 2L;
                        // render the red box
                        RenderControl_Square(3, left, top, width, height, bs, alpha);

                        // render red gradient overlay
                        string argpath8 = System.IO.Path.Combine(Path.Gradients, "5");
                        GameClient.RenderTexture(ref argpath8, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Red_Click:
                    {
                        bs = 2L;
                        // render the red box
                        RenderControl_Square(3, left, top, width, height, bs, alpha);

                        // render red gradient overlay
                        string argpath9 = System.IO.Path.Combine(Path.Gradients, "6");
                        GameClient.RenderTexture(ref argpath9, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Blue:
                    {
                        bs = 2L;
                        // render the Blue box
                        RenderControl_Square(14, left, top, width, height, bs, alpha);

                        // render Blue gradient overlay
                        string argpath10 = System.IO.Path.Combine(Path.Gradients, "8");
                        GameClient.RenderTexture(ref argpath10, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Blue_Hover:
                    {
                        bs = 2L;
                        // render the Blue box
                        RenderControl_Square(14, left, top, width, height, bs, alpha);

                        // render Blue gradient overlay
                        string argpath11 = System.IO.Path.Combine(Path.Gradients, "9");
                        GameClient.RenderTexture(ref argpath11, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Blue_Click:
                    {
                        bs = 2L;
                        // render the Blue box
                        RenderControl_Square(14, left, top, width, height, bs, alpha);

                        // render Blue gradient overlay
                        string argpath12 = System.IO.Path.Combine(Path.Gradients, "10");
                        GameClient.RenderTexture(ref argpath12, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Orange:
                    {
                        bs = 2L;
                        // render the Orange box
                        RenderControl_Square(15, left, top, width, height, bs, alpha);

                        // render Orange gradient overlay
                        string argpath13 = System.IO.Path.Combine(Path.Gradients, "11");
                        GameClient.RenderTexture(ref argpath13, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Orange_Hover:
                    {
                        bs = 2L;
                        // render the Orange box
                        RenderControl_Square(15, left, top, width, height, bs, alpha);

                        // render Orange gradient overlay
                        string argpath14 = System.IO.Path.Combine(Path.Gradients, "12");
                        GameClient.RenderTexture(ref argpath14, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Orange_Click:
                    {
                        bs = 2L;
                        // render the Orange box
                        RenderControl_Square(15, left, top, width, height, bs, alpha);

                        // render Orange gradient overlay
                        string argpath15 = System.IO.Path.Combine(Path.Gradients, "13");
                        GameClient.RenderTexture(ref argpath15, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Grey:
                    {
                        bs = 2L;
                        // render the Orange box
                        RenderControl_Square(17, left, top, width, height, bs, alpha);

                        // render Orange gradient overlay
                        string argpath16 = System.IO.Path.Combine(Path.Gradients, "14");
                        GameClient.RenderTexture(ref argpath16, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Parchment:
                    {
                        bs = 20L;
                        // render the parchment box
                        RenderControl_Square(4, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.BlackOval:
                    {
                        bs = 4L;
                        // render the black oval
                        RenderControl_Square(5, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.TextBlack:
                    {
                        bs = 5L;
                        // render the black oval
                        RenderControl_Square(6, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.TextWhite:
                    {
                        bs = 5L;
                        // render the black oval
                        RenderControl_Square(7, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.TextBlack_Sq:
                    {
                        bs = 4L;
                        // render the black oval
                        RenderControl_Square(10, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Win_Desc:
                    {
                        bs = 8L;
                        // render black square
                        RenderControl_Square(11, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.DescPic:
                    {
                        bs = 3L;
                        // render the green box
                        RenderControl_Square(12, left, top, width, height, bs, alpha);

                        // render green gradient overlay
                        string argpath17 = System.IO.Path.Combine(Path.Gradients, "7");
                        GameClient.RenderTexture(ref argpath17, (int)(left + bs), (int)(top + bs), 0, 0, (int)(width - bs * 2L), (int)(height - bs * 2L), 128, 128, (byte)alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Win_Shadow:
                    {
                        bs = 35L;
                        // render the green box
                        RenderControl_Square(13, left - bs, top - bs, width + bs * 2L, height + bs * 2L, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.Win_Party:
                    {
                        bs = 12L;
                        // render black square
                        RenderControl_Square(16, left, top, width, height, bs, alpha);
                        break;
                    }

                case (long)Core.Enum.DesignType.TileBox:
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

        public static void TradeDblClick_Your()
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
                if (Core.Type.TradeYourOffer[(int)itemNum].Num == -1)
                    return;
                if (GetPlayerInv(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)itemNum].Num) == -1)
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
                if (Core.Type.TradeYourOffer[(int)itemNum].Num == -1)
                {
                    Windows[GetWindowIndex("winDescription")].Visible = false;
                    return;
                }

                if (GetPlayerInv(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)itemNum].Num) == -1)
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
                GameLogic.ShowItemDesc(x, y, (long)GetPlayerInv(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)itemNum].Num));
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
                if (Core.Type.TradeTheirOffer[(int)itemNum].Num == -1)
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
                GameLogic.ShowItemDesc(x, y, (long)Core.Type.TradeTheirOffer[(int)itemNum].Num);
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
                    SettingsManager.Instance.SaveUsername = Conversions.ToBoolean(0);
                    SettingsManager.Instance.Username = "";
                    SettingsManager.Save();
                }
                else
                {
                    SettingsManager.Instance.SaveUsername = Conversions.ToBoolean(1);
                    SettingsManager.Save();
                }
            }
        }

        public static void btnRegister_Click()
        {
            if (!(bool)(NetworkConfig.Socket?.IsConnected))
            {
                GameLogic.Dialogue("Invalid Connection", "Cannot connect to game server.", "Please try again.", (byte)Core.Enum.DialogueType.Alert);
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
                    GameLogic.Dialogue("Register", "Passwords don't match.", "Please try again.", (byte)Core.Enum.DialogueType.Alert);
                    ClearPasswordTexts();
                    return;
                }

                if (NetworkConfig.Socket?.IsConnected == true)
                {
                    NetworkSend.SendRegister(User, Pass);
                }
                else
                {
                    GameLogic.Dialogue("Invalid Connection", "Cannot connect to game server.", "Please try again.", (byte)Core.Enum.DialogueType.Alert);
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
            GameLogic.Dialogue("Delete Character", "Deleting this character is permanent.", "Delete this character?", (byte)Core.Enum.DialogueType.DelChar, (byte)Core.Enum.DialogueStyle.YesNo, 1L);
        }

        public static void btnDelChar_2()
        {
            GameLogic.Dialogue("Delete Character", "Deleting this character is permanent.", "Delete this character?", (byte)Core.Enum.DialogueType.DelChar, (byte)Core.Enum.DialogueStyle.YesNo, 2L);
        }

        public static void btnDelChar_3()
        {
            GameLogic.Dialogue("Delete Character", "Deleting this character is permanent.", "Delete this character?", (byte)Core.Enum.DialogueType.DelChar, (byte)Core.Enum.DialogueStyle.YesNo, 3L);
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
            if (string.IsNullOrEmpty(Core.Type.Job[(int)GameState.NewCharJob].Desc))
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
                text = Core.Type.Job[(int)GameState.NewCharJob].Desc;
            }

            // Wrap text to fit within 330 pixels
            Text.WordWrap(text, Windows[GetWindowIndex("winJobs")].Font, 330L, ref textArray);

            count = Information.UBound(textArray);
            y = yO + 60L;
            var loopTo = count;
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
            Windows[GetWindowIndex("winJobs")].Controls[GetControlIndex("winJobs", "lblClassName")].Text = Core.Type.Job[(int)GameState.NewCharJob].Name;
        }

        public static void btnJobs_Right()
        {
            // Exit if the job is invalid or exceeds limits
            if (GameState.NewCharJob >= Constant.MAX_JOBS - 1 || string.IsNullOrEmpty(Core.Type.Job[(int)GameState.NewCharJob ].Desc) & GameState.NewCharJob >= Constant.MAX_JOBS)
                return;

            // Move to the next job
            GameState.NewCharJob += 1L;

            // Update class name display
            Windows[GetWindowIndex("winJobs")].Controls[GetControlIndex("winJobs", "lblClassName")].Text = Core.Type.Job[(int)GameState.NewCharJob ].Name;
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
            RenderDesign((long)Core.Enum.DesignType.Win_Desc, xO, yO, 352L, 152L);

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
            RenderDesign((long)Core.Enum.DesignType.Win_Shadow, xO, yO, 160L, 10L);
        }

        public static void chkChat_Game()
        {
            SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Game] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkGame")].Value;
            SettingsManager.Save();
        }

        public static void chkChat_Map()
        {
            SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Map] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkMap")].Value;
            SettingsManager.Save();
        }

        public static void chkChat_Global()
        {
            SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Broadcast] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkGlobal")].Value;
            SettingsManager.Save();
        }

        public static void chkChat_Party()
        {
            SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Party] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkParty")].Value;
            SettingsManager.Save();
        }

        public static void chkChat_Guild()
        {
            SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Guild] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkGuild")].Value;
            SettingsManager.Save();
        }

        public static void chkChat_Player()
        {
            SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Player] = (byte)Windows[GetWindowIndex("winChat")].Controls[GetControlIndex("winChat", "chkPlayer")].Value;
            SettingsManager.Save();
        }

        public static void btnChat_Up()
        {
            GameState.ChatButtonUp = Conversions.ToBoolean(1);
        }

        public static void btnChat_Down()
        {
            GameState.ChatButtonDown = Conversions.ToBoolean(1);
        }

        public static void btnChat_Up_MouseUp()
        {
            GameState.ChatButtonUp = Conversions.ToBoolean(0);
        }

        public static void btnChat_Down_MouseUp()
        {
            GameState.ChatButtonDown = Conversions.ToBoolean(0);
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

            if (GameState.NewCnarGender == (long)Core.Enum.SexType.Male)
            {
                imageChar = Core.Type.Job[(int)GameState.NewCharJob].MaleSprite;
            }
            else
            {
                imageChar = Core.Type.Job[(int)GameState.NewCharJob].FemaleSprite;
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

            if (GameState.NewCnarGender == (long)Core.Enum.SexType.Male)
            {
                spriteCount = Core.Type.Job[(int)GameState.NewCharJob].MaleSprite;
            }
            else
            {
                spriteCount = Core.Type.Job[(int)GameState.NewCharJob].FemaleSprite;
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

            if (GameState.NewCnarGender == (long)Core.Enum.SexType.Male)
            {
                spriteCount = Core.Type.Job[(int)GameState.NewCharJob].MaleSprite;
            }
            else
            {
                spriteCount = Core.Type.Job[(int)GameState.NewCharJob].FemaleSprite;
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
            GameState.NewCnarGender = (long)Core.Enum.SexType.Male;
            if (Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkMale")].Value == 0L)
            {
                Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkFemale")].Value = 0L;
                Windows[GetWindowIndex("winNewChar")].Controls[GetControlIndex("winNewChar", "chkMale")].Value = 1L;
            }
        }

        public static void chkNewChar_Female()
        {
            GameState.NewCharSprite = 1L;
            GameState.NewCnarGender = (long)Core.Enum.SexType.Female;
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
            GameState.NewCnarGender = (long)Core.Enum.SexType.Male;
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
            if (GameState.diaStyle == (int)Core.Enum.DialogueStyle.Okay)
            {
                GameLogic.DialogueHandler(1L);
            }
            else if (GameState.diaStyle == (int)Core.Enum.DialogueStyle.YesNo)
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
                withBlock.Type = Core.Enum.PartType.Item;
                withBlock.Value = (long)GetPlayerInv(GameState.MyIndex, (int)invNum);
                withBlock.Origin = Core.Enum.PartOriginType.Inventory;
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
                Windows[GetWindowIndex("winInventory")].State = Core.Enum.EntState.Normal;
            }

            Inventory_MouseMove();
        }

        public static void Inventory_DblClick()
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
                        if (Core.Type.TradeYourOffer[(int)i].Num == invNum)
                        {
                            // is currency?
                            if (Core.Type.Item[GetPlayerInv(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)i].Num)].Type == (byte)Core.Enum.ItemType.Currency)
                            {
                                // only exit out if we're offering all of it
                                if (Core.Type.TradeYourOffer[(int)i].Value == GetPlayerInvValue(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)i].Num))
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
                    if (Core.Type.Item[GetPlayerInv(GameState.MyIndex, (int)invNum)].Type == (byte)Core.Enum.ItemType.Currency)
                    {
                        GameLogic.Dialogue("Select Amount", "Please choose how many to offer.", "", (byte)Core.Enum.DialogueType.TradeAmount, (byte)Core.Enum.DialogueStyle.Input, invNum);
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
            if (DragBox.Type != Core.Enum.PartType.None)
                return;

            itemNum = General.IsInv(Windows[GetWindowIndex("winInventory")].Left, Windows[GetWindowIndex("winInventory")].Top);
            if (itemNum >= 0L)
            {
                // exit out if we're offering that item
                if (Trade.InTrade >= 0)
                {
                    for (i = 0L; i < Constant.MAX_INV; i++)
                    {
                        if (Core.Type.TradeYourOffer[(int)i].Num == itemNum)
                        {
                            // is currency?
                            if (Core.Type.Item[GetPlayerInv(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)i].Num)].Type == (byte)Core.Enum.ItemType.Currency)
                            {
                                // only exit out if we're offering all of it
                                if (Core.Type.TradeYourOffer[(int)i].Value == GetPlayerInvValue(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)i].Num))
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
                if (DragBox.Type == Core.Enum.PartType.Item & DragBox.Value == itemNum)
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
            if (DragBox.Type != Core.Enum.PartType.None)
                return;

            itemNum = General.IsBank(Windows[GetWindowIndex("winBank")].Left, Windows[GetWindowIndex("winBank")].Top);

            if (itemNum >= 0L)
            {
                // make sure we're not dragging the item
                if (DragBox.Type == Core.Enum.PartType.Item & DragBox.Value == itemNum)
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
                withBlock.Type = Core.Enum.PartType.Item;
                withBlock.Value = (long)GetBank(GameState.MyIndex, (int)bankSlot);
                withBlock.Origin = Core.Enum.PartOriginType.Bank;

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
                Windows[GetWindowIndex("winBank")].State = Core.Enum.EntState.Normal;
            }

            Bank_MouseMove();
        }

        public static void Bank_DblClick()
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

            if (DragBox.Type == Core.Enum.PartType.None)
                return;

            // get texture num
            {
                ref var withBlock = ref DragBox;
                switch (withBlock.Type)
                {
                    case Core.Enum.PartType.Item:
                        {
                            if (withBlock.Value >= 0)
                            {
                                texNum = Core.Type.Item[(int)withBlock.Value].Icon;
                                string argpath = System.IO.Path.Combine(Path.Items, texNum.ToString());
                                GameClient.RenderTexture(ref argpath, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32);
                            }

                            break;
                        }

                    case Core.Enum.PartType.Skill:
                        {
                            if (withBlock.Value >= 0)
                            {
                                texNum = Core.Type.Skill[(int)withBlock.Value].Icon;
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
            Core.Type.RectStruct tmpRec;

            winIndex = GetWindowIndex("winDragBox");

            if (DragBox.Type == Core.Enum.PartType.None)
                return;

            // check for other windows
            var loopTo = Windows.Count;
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
                            if (DragBox.Origin == Core.Enum.PartOriginType.Bank)
                            {
                                if (DragBox.Type == Core.Enum.PartType.Item)
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

                            if (DragBox.Origin == Core.Enum.PartOriginType.Inventory)
                            {
                                if (DragBox.Type == Core.Enum.PartType.Item)
                                {

                                    if (Core.Type.Item[GetPlayerInv(GameState.MyIndex, (int)DragBox.Slot)].Type != (byte)Core.Enum.ItemType.Currency)
                                    {
                                        Bank.DepositItem((int)DragBox.Slot, 1);
                                    }
                                    else
                                    {
                                        GameLogic.Dialogue("Deposit Item", "Enter the deposit quantity.", "", (byte)Core.Enum.DialogueType.DepositItem, (byte)Core.Enum.DialogueStyle.Input, DragBox.Slot);
                                    }

                                }
                            }

                            break;
                        }

                    case "winInventory":
                        {
                            if (DragBox.Origin == Core.Enum.PartOriginType.Inventory)
                            {
                                // it's from the inventory!
                                if (DragBox.Type == Core.Enum.PartType.Item)
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

                            if (DragBox.Origin == Core.Enum.PartOriginType.Bank)
                            {
                                if (DragBox.Type == Core.Enum.PartType.Item)
                                {

                                    if (Core.Type.Item[GetBank(GameState.MyIndex, (byte)DragBox.Slot)].Type != (byte)Core.Enum.ItemType.Currency)
                                    {
                                        Bank.WithdrawItem((byte)DragBox.Slot, 0);
                                    }
                                    else
                                    {
                                        GameLogic.Dialogue("Withdraw Item", "Enter the amount you wish to withdraw.", "", (byte)Core.Enum.DialogueType.WithdrawItem, (byte)Core.Enum.DialogueStyle.Input, DragBox.Slot);
                                    }

                                }
                            }

                            break;
                        }

                    case "winSkills":
                        {
                            if (DragBox.Origin == Core.Enum.PartOriginType.Skill)
                            {
                                if (DragBox.Type == Core.Enum.PartType.Skill)
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
                            if (DragBox.Origin != Core.Enum.PartOriginType.None)
                            {
                                if (DragBox.Type != Core.Enum.PartType.None)
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
                                                if (DragBox.Origin != Core.Enum.PartOriginType.Hotbar)
                                                {
                                                    if (DragBox.Type == Core.Enum.PartType.Item)
                                                    {
                                                        NetworkSend.SendSetHotbarSlot((int)Core.Enum.PartOriginsType.Inventory, (int)i, (int)DragBox.Slot, (int)DragBox.Value);
                                                    }
                                                    else if (DragBox.Type == Core.Enum.PartType.Skill)
                                                    {
                                                        NetworkSend.SendSetHotbarSlot((int)Core.Enum.PartOriginsType.Skill, (int)i, (int)DragBox.Slot, (int)DragBox.Value);
                                                    }
                                                }
                                                else if (DragBox.Slot != i)
                                                    NetworkSend.SendSetHotbarSlot((int)Core.Enum.PartOriginsType.Hotbar, (int)i, (int)DragBox.Slot, (int)DragBox.Value);
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
                    case Core.Enum.PartOriginType.Inventory:
                        {
                            if (Core.Type.Item[GetPlayerInv(GameState.MyIndex, (int)DragBox.Slot)].Type != (byte)Core.Enum.ItemType.Currency)
                            {
                                NetworkSend.SendDropItem((int)DragBox.Slot, GetPlayerInv(GameState.MyIndex, (int)DragBox.Slot));
                            }
                            else
                            {
                                GameLogic.Dialogue("Drop Item", "Please choose how many to drop.", "", (byte)Core.Enum.DialogueType.DropItem, (byte)Core.Enum.DialogueStyle.Input, DragBox.Slot);
                            }

                            break;
                        }

                    case Core.Enum.PartOriginType.Skill:
                        {
                            NetworkSend.ForgetSkill((int)DragBox.Slot);
                            break;
                        }

                    case Core.Enum.PartOriginType.Hotbar:
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
                withBlock1.Type = Core.Enum.PartType.None;
                withBlock1.Slot = 0L;
                withBlock1.Origin = Core.Enum.PartOriginType.None;
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
                withBlock.Type = Core.Enum.PartType.Skill;
                withBlock.Value = (long)Core.Type.Player[GameState.MyIndex].Skill[(int)slotNum].Num;
                withBlock.Origin = Core.Enum.PartOriginType.Skill;
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
                Windows[GetWindowIndex("winSkills")].State = Core.Enum.EntState.Normal;
            }

            Skills_MouseMove();
        }

        public static void Skills_DblClick()
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
            if (DragBox.Type != Core.Enum.PartType.None)
                return;

            slotNum = General.IsSkill(Windows[GetWindowIndex("winSkills")].Left, Windows[GetWindowIndex("winSkills")].Top);

            if (slotNum >= 0L)
            {
                // make sure we're not dragging the item
                if (DragBox.Type == Core.Enum.PartType.Item & DragBox.Value == slotNum)
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
                if (Core.Type.Player[GameState.MyIndex].Hotbar[(int)slotNum].SlotType == 1) // inventory
                {
                    withBlock.Type = (Core.Enum.PartType)Core.Enum.PartOriginsType.Inventory;
                }
                else if (Core.Type.Player[GameState.MyIndex].Hotbar[(int)slotNum].SlotType == 2) // Skill
                {
                    withBlock.Type = (Core.Enum.PartType)Core.Enum.PartOriginsType.Skill;
                }
                withBlock.Value = (long)Core.Type.Player[GameState.MyIndex].Hotbar[(int)slotNum].Slot;
                withBlock.Origin = Core.Enum.PartOriginType.Hotbar;
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
                Windows[GetWindowIndex("winHotbar")].State = Core.Enum.EntState.Normal;
            }

            Hotbar_MouseMove();
        }

        public static void Hotbar_DblClick()
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
            if (DragBox.Type != (int)Core.Enum.PartOriginsType.None)
                return;

            slotNum = GameLogic.IsHotbar(Windows[GetWindowIndex("winHotbar")].Left, Windows[GetWindowIndex("winHotbar")].Top);

            if (slotNum >= 0L)
            {
                // make sure we're not dragging the item
                if (DragBox.Origin == Core.Enum.PartOriginType.Hotbar & DragBox.Slot == slotNum)
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
                switch (Core.Type.Player[GameState.MyIndex].Hotbar[(int)slotNum].SlotType)
                {
                    case 1: // inventory
                        {
                            GameLogic.ShowItemDesc(x, y, (long)Core.Type.Player[GameState.MyIndex].Hotbar[(int)slotNum].Slot);
                            break;
                        }
                    case 2: // skill
                        {
                            GameLogic.ShowSkillDesc(x, y, (long)Core.Type.Player[GameState.MyIndex].Hotbar[(int)slotNum].Slot, 0L);
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
                withBlock.Controls[GetControlIndex("winBars", "lblHP")].Text = GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.HP) + "/" + GetPlayerMaxVital(GameState.MyIndex, Core.Enum.VitalType.HP);
                withBlock.Controls[GetControlIndex("winBars", "lblMP")].Text = GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.SP) + "/" + GetPlayerMaxVital(GameState.MyIndex, Core.Enum.VitalType.SP);
                withBlock.Controls[GetControlIndex("winBars", "lblEXP")].Text = GetPlayerExp(GameState.MyIndex) + "/" + GameState.NextlevelExp;
            }

            // update character screen
            {
                var withBlock1 = Windows[GetWindowIndex("winCharacter")];
                withBlock1.Controls[GetControlIndex("winCharacter", "lblHealth")].Text = "Health";
                withBlock1.Controls[GetControlIndex("winCharacter", "lblSpirit")].Text = "Spirit";
                withBlock1.Controls[GetControlIndex("winCharacter", "lblExperience")].Text = "Exp";
                withBlock1.Controls[GetControlIndex("winCharacter", "lblHealth2")].Text = GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.HP) + "/" + GetPlayerMaxVital(GameState.MyIndex, Core.Enum.VitalType.HP);
                withBlock1.Controls[GetControlIndex("winCharacter", "lblSpirit2")].Text = GetPlayerVital(GameState.MyIndex, Core.Enum.VitalType.SP) + "/" + GetPlayerMaxVital(GameState.MyIndex, Core.Enum.VitalType.SP);
                withBlock1.Controls[GetControlIndex("winCharacter", "lblExperience2")].Text = Core.Type.Player[GameState.MyIndex].Exp + "/" + GameState.NextlevelExp;

            }
        }

        public static void UpdateWindow_EscMenu()
        {
            // Control window
            UpdateWindow("winEscMenu", "", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 210L, 156L, 0L, false, design_norm: (long)Core.Enum.DesignType.Win_NoBar, design_hover: (long)Core.Enum.DesignType.Win_NoBar, design_mousedown: (long)Core.Enum.DesignType.Win_NoBar, canDrag: false, clickThrough: false);

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Parchment
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 6L, 198L, 144L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);

            // Buttons
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = new Action(btnEscMenu_Return);
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Gui.UpdateButton(Windows.Count, "btnReturn", 16L, 16L, 178L, 28L, "Return to Game", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1);

            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = new Action(btnEscMenu_Options);
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Gui.UpdateButton(Windows.Count, "btnOptions", 16L, 48L, 178L, 28L, "Options", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.Orange, design_hover: (long)Core.Enum.DesignType.Orange_Hover, design_mousedown: (long)Core.Enum.DesignType.Orange_Click, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2);

            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = new Action(btnEscMenu_MainMenu);
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Gui.UpdateButton(Windows.Count, "btnMainMenu", 16L, 80L, 178L, 28L, "Back to Main Menu", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.Blue, design_hover: (long)Core.Enum.DesignType.Blue_Hover, design_mousedown: (long)Core.Enum.DesignType.Blue_Click, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3);

            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = new Action(btnEscMenu_Exit);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Gui.UpdateButton(Windows.Count, "btnExit", 16L, 112L, 178L, 28L, "Exit the Game", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.Red, design_hover: (long)Core.Enum.DesignType.Red_Hover, design_mousedown: (long)Core.Enum.DesignType.Red_Click, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
        }

        public static void UpdateWindow_Bars()
        {
            // Control window
            UpdateWindow("winBars", "", Core.Enum.FontType.Georgia, zOrder_Win, 10L, 10L, 239L, 77L, 0L, false, design_norm: (long)Core.Enum.DesignType.Win_NoBar, design_hover: (long)Core.Enum.DesignType.Win_NoBar, design_mousedown: (long)Core.Enum.DesignType.Win_NoBar, canDrag: false, clickThrough: true);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Parchment
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 6L, 227L, 65L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);

            // Blank Bars
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picHP_Blank", 15L, 15L, 209L, 13L, image_norm: 24L, image_hover: 24L, image_mousedown: 24L, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picSP_Blank", 15L, 32L, 209L, 13L, image_norm: 25L, image_hover: 25L, image_mousedown: 25L, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw2);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Windows.Count, "picEXP_Blank", 15L, 49L, 209L, 13L, image_norm: 26L, image_hover: 26L, image_mousedown: 26L, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw3);

            // Bars
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            var argonDraw4 = new Action(Bars_OnDraw);
            Gui.UpdatePictureBox(Windows.Count, "picBlank", 0L, 0L, 0L, 0L, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw4);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argonDraw5 = null;
            UpdatePictureBox(Windows.Count, "picHealth", 16L, 10L, 44L, 14L, image_norm: 20L, image_hover: 20L, image_mousedown: 20L, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, onDraw: ref argonDraw5);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw6 = null;
            UpdatePictureBox(Windows.Count, "picSpirit", 16L, 28L, 44L, 14L, image_norm: 22L, image_hover: 22L, image_mousedown: 22L, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, onDraw: ref argonDraw6);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argonDraw7 = null;
            UpdatePictureBox(Windows.Count, "picExperience", 16L, 45L, 74L, 14L, image_norm: 23L, image_hover: 23L, image_mousedown: 23L, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, onDraw: ref argonDraw7);

            // Labels
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblHP", 15L, 14L, 209L, 10L, "999/999", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, enabled: ref enabled);
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            UpdateLabel(Windows.Count, "lblMP", 15L, 30L, 209L, 10L, "999/999", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, enabled: ref enabled);
            Action argcallback_norm10 = null;
            Action argcallback_hover10 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            UpdateLabel(Windows.Count, "lblEXP", 15L, 48L, 209L, 10L, "999/999", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm10, callback_hover: ref argcallback_hover10, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, enabled: ref enabled);
        }

        public static void UpdateWindow_Chat()
        {
            // Control window
            UpdateWindow("winChat", "", Core.Enum.FontType.Georgia, zOrder_Win, 8L, GameState.ResolutionHeight - 178, 352L, 152L, 0L, false, canDrag: false);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Channel boxes
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = new Action(chkChat_Game);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Gui.UpdateCheckBox(Windows.Count, "chkGame", 10L, 2L, 49L, 23L, 0L, "Game", Core.Enum.FontType.Arial, theDesign: (long)Core.Enum.DesignType.ChkChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(chkChat_Map);
            Gui.UpdateCheckBox(Windows.Count, "chkMap", 60L, 2L, 49L, 23L, 0L, "Map", Core.Enum.FontType.Arial, theDesign: (long)Core.Enum.DesignType.ChkChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(chkChat_Global);
            Gui.UpdateCheckBox(Windows.Count, "chkGlobal", 110L, 2L, 49L, 23L, 0L, "Global", Core.Enum.FontType.Arial, theDesign: (long)Core.Enum.DesignType.ChkChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(chkChat_Party);
            Gui.UpdateCheckBox(Windows.Count, "chkParty", 160L, 2L, 49L, 23L, 0L, "Party", Core.Enum.FontType.Arial, theDesign: (long)Core.Enum.DesignType.ChkChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(chkChat_Guild);
            Gui.UpdateCheckBox(Windows.Count, "chkGuild", 210L, 2L, 49L, 23L, 0L, "Guild", Core.Enum.FontType.Arial, theDesign: (long)Core.Enum.DesignType.ChkChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(chkChat_Player);
            Gui.UpdateCheckBox(Windows.Count, "chkPlayer", 260L, 2L, 49L, 23L, 0L, "Player", Core.Enum.FontType.Arial, theDesign: (long)Core.Enum.DesignType.ChkChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Blank picturebox
            var argonDraw = new Action(Chat_OnDraw);
            Action argcallback_norm_pic = null;
            Action argcallback_hover_pic = null;
            Action argcallback_mousedown_pic = null;
            Action argcallback_mousemove_pic = null;
            Action argcallback_dblclick_pic = null;
            Gui.UpdatePictureBox(Windows.Count, "picNull", 0L, 0L, 0L, 0L, onDraw: ref argonDraw, callback_norm: ref argcallback_norm_pic, callback_hover: ref argcallback_hover_pic, callback_mousedown: ref argcallback_mousedown_pic, callback_mousemove: ref argcallback_mousemove_pic, callback_dblclick: ref argcallback_dblclick_pic);

            // Chat button
            argcallback_norm = new Action(btnSay_Click);
            Gui.UpdateButton(Windows.Count, "btnChat", 296L, (long)(124 + 16), 48L, 20L, "Say", Core.Enum.FontType.Arial, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Chat Textbox
            Action argcallback_enter = null;
            UpdateTextbox(Windows.Count, "txtChat", 12L, 127 + 16, 352L, 25L, font: Core.Enum.FontType.Georgia, visible: false, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, callback_enter: ref argcallback_enter);

            // Buttons
            argcallback_norm = new Action(btnChat_Up);
            Gui.UpdateButton(Windows.Count, "btnUp", 328L, 28L, 10L, 13L, image_norm: 4L, image_hover: 52L, image_mousedown: 4L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_norm = new Action(btnChat_Down);
            Gui.UpdateButton(Windows.Count, "btnDown", 327L, 122L, 10L, 13L, image_norm: 5L, image_hover: 53L, image_mousedown: 5L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Custom Handlers for mouse up
            Windows[Windows.Count].Controls[GetControlIndex("winChat", "btnUp")].CallBack[(int)Core.Enum.EntState.MouseUp] = new Action(btnChat_Up_MouseUp);
            Windows[Windows.Count].Controls[GetControlIndex("winChat", "btnDown")].CallBack[(int)Core.Enum.EntState.MouseUp] = new Action(btnChat_Down_MouseUp);

            // Set the active control
            SetActiveControl(GetWindowIndex("winChat"), GetControlIndex("winChat", "txtChat"));

            // sort out the tabs
            {
                var withBlock = Windows[GetWindowIndex("winChat")];
                withBlock.Controls[GetControlIndex("winChat", "chkGame")].Value = SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Game];
                withBlock.Controls[GetControlIndex("winChat", "chkMap")].Value = SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Map];
                withBlock.Controls[GetControlIndex("winChat", "chkGlobal")].Value = SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Broadcast];
                withBlock.Controls[GetControlIndex("winChat", "chkParty")].Value = SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Party];
                withBlock.Controls[GetControlIndex("winChat", "chkGuild")].Value = SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Guild];
                withBlock.Controls[GetControlIndex("winChat", "chkPlayer")].Value = SettingsManager.Instance.ChannelState[(int)Core.Enum.ChatChannel.Player];
            }
        }

        public static void UpdateWindow_ChatSmall()
        {
            // Control window
            UpdateWindow("winChatSmall", "", Core.Enum.FontType.Georgia, zOrder_Win, 8L, 0L, 0L, 0L, 0L, false, onDraw: new Action(ChatSmall_OnDraw), canDrag: false, clickThrough: true);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Chat Label
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblMsg", 12L, 140L, 286L, 25L, "Press 'Enter' to open chatbox.", Core.Enum.FontType.Georgia, Color.White, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, enabled: ref enabled);
        }

        public static void UpdateWindow_Hotbar()
        {
            // Control window
            UpdateWindow("winHotbar", "", Core.Enum.FontType.Georgia, zOrder_Win, 432L, 10L, 418L, 36L, 0L, false, callback_mousemove: new Action(Hotbar_MouseMove), callback_mousedown: new Action(Hotbar_MouseDown), callback_dblclick: new Action(Hotbar_DblClick), onDraw: new Action(DrawHotbar), canDrag: false, zChange: Conversions.ToByte(false));
        }

        public static void UpdateWindow_Menu()
        {
            // Control window
            UpdateWindow("winMenu", "", Core.Enum.FontType.Georgia, zOrder_Win, GameState.ResolutionWidth - 229, GameState.ResolutionHeight - 31, 229L, 30L, 0L, false, isActive: false, canDrag: false, clickThrough: true);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Wood part
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picWood", 0L, 5L, 228L, 20L, design_norm: (long)Core.Enum.DesignType.Wood, design_hover: (long)Core.Enum.DesignType.Wood, design_mousedown: (long)Core.Enum.DesignType.Wood, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);
            // Buttons
            var argcallback_mousedown1 = new Action(btnMenu_Char);
            Action callback_norm = null;
            Action callback_hover = null;
            Action callback_mousemove = null;
            Action callback_dblclick = null;
            Gui.UpdateButton(Windows.Count, "btnChar", 8L, 0L, 29L, 29L, text: "", font: Core.Enum.FontType.Georgia, icon: 108L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2, tooltip: "Character (C)");
            var argcallback_mousedown2 = new Action(btnMenu_Inv);
            Gui.UpdateButton(Windows.Count, "btnInv", 44L, 0L, 29L, 29L, text: "", font: Core.Enum.FontType.Georgia, icon: 1L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2, tooltip: "Inventory (I)");
            var argcallback_mousedown3 = new Action(btnMenu_Skills);
            Gui.UpdateButton(Windows.Count, "btnSkills", 82L, 0L, 29L, 29L, text: "", font: Core.Enum.FontType.Georgia, icon: 109L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2, tooltip: "Skills (K)");
            var argcallback_mousedown4 = new Action(btnMenu_Map);
            Gui.UpdateButton(Windows.Count, "btnMap", 119L, 0L, 29L, 29L, text: "", font: Core.Enum.FontType.Georgia, icon: 106L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)Core.Enum.DesignType.Grey, design_hover: (long)Core.Enum.DesignType.Grey, design_mousedown: (long)Core.Enum.DesignType.Grey, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2);
            var argcallback_mousedown5 = new Action(btnMenu_Guild);
            Gui.UpdateButton(Windows.Count, "btnGuild", 155L, 0L, 29L, 29L, text: "", font: Core.Enum.FontType.Georgia, icon: 107L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)Core.Enum.DesignType.Grey, design_hover: (long)Core.Enum.DesignType.Grey, design_mousedown: (long)Core.Enum.DesignType.Grey, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-1);
            var argcallback_mousedown6 = new Action(btnMenu_Quest);
            Gui.UpdateButton(Windows.Count, "btnQuest", 190L, 0L, 29L, 29L, text: "", font: Core.Enum.FontType.Georgia, icon: 23L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)Core.Enum.DesignType.Grey, design_hover: (long)Core.Enum.DesignType.Grey, design_mousedown: (long)Core.Enum.DesignType.Grey, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2);
        }

        public static void UpdateWindow_Inventory()
        {
            // Control window
            UpdateWindow("winInventory", "Inventory", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 202L, 319L, 1L, false, 2L, 7L, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, callback_mousemove: new Action(Inventory_MouseMove), callback_mousedown: new Action(Inventory_MouseDown), callback_dblclick: new Action(Inventory_DblClick), onDraw: new Action(DrawInventory));

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnMenu_Inv);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Enum.FontType.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Gold amount
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picBlank", 8L, 293L, 186L, 18L, image_norm: 67L, image_hover: 67L, image_mousedown: 67L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            bool enabled = false;
            //UpdateLabel(Windows.Count, "lblGold", 42L, 296L, 100L, 10L, "g", Core.Enum.FontType.Georgia, Color.Yellow, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, enabled: ref enabled);

            // Drop
            //Gui.UpdateButton(Windows.Count, "btnDrop", 155L, 294L, 38L, 16L, "Drop", Core.Enum.FontType.Georgia, 0L, 0L, 0L, 0L, true, 255L, (long)Core.Enum.DesignType.Green, (long)Core.Enum.DesignType.Green_Hover, (long)Core.Enum.DesignType.Green_Click, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 5L, 3L, "", false, true);
        }

        public static void UpdateWindow_Character()
        {
            // Control window
            UpdateWindow("winCharacter", "Character", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 174L, 356L, 62L, false, 2L, 6L, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, callback_mousemove: new Action(Character_MouseMove), callback_mousedown: new Action(Character_MouseMove), callback_dblclick: new Action(Character_DblClick), onDraw: new Action(DrawCharacter));

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            Action argcallback_norm = null;
            var argcallback_mousedown = new Action(btnMenu_Char);
            Action argcallback_hover = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 26L, 162L, 287L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // White boxes
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picWhiteBox", 13L, 34L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picWhiteBox", 13L, 54L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw2);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Windows.Count, "picWhiteBox", 13L, 74L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw3);
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argonDraw4 = null;
            UpdatePictureBox(Windows.Count, "picWhiteBox", 13L, 94L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, onDraw: ref argonDraw4);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw5 = null;
            UpdatePictureBox(Windows.Count, "picWhiteBox", 13L, 114L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, onDraw: ref argonDraw5);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argonDraw6 = null;
            UpdatePictureBox(Windows.Count, "picWhiteBox", 13L, 134L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, onDraw: ref argonDraw6);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Action argonDraw7 = null;
            UpdatePictureBox(Windows.Count, "picWhiteBox", 13L, 154L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextWhite, design_hover: (long)Core.Enum.DesignType.TextWhite, design_mousedown: (long)Core.Enum.DesignType.TextWhite, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, onDraw: ref argonDraw7);

            // Labels
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblName", 18L, 36L, 147L, 10L, "Name", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, enabled: ref enabled);
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            UpdateLabel(Windows.Count, "lblJob", 18L, 56L, 147L, 10L, "Job", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, enabled: ref enabled);
            Action argcallback_norm10 = null;
            Action argcallback_hover10 = null;
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            UpdateLabel(Windows.Count, "lblLevel", 18L, 76L, 147L, 10L, "Level", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm10, callback_hover: ref argcallback_hover10, callback_mousedown: ref argcallback_mousedown11, callback_mousemove: ref argcallback_mousemove11, callback_dblclick: ref argcallback_dblclick11, enabled: ref enabled);
            Action argcallback_norm11 = null;
            Action argcallback_hover11 = null;
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            UpdateLabel(Windows.Count, "lblGuild", 18L, 96L, 147L, 10L, "Guild", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm11, callback_hover: ref argcallback_hover11, callback_mousedown: ref argcallback_mousedown12, callback_mousemove: ref argcallback_mousemove12, callback_dblclick: ref argcallback_dblclick12, enabled: ref enabled);
            Action argcallback_norm12 = null;
            Action argcallback_hover12 = null;
            Action argcallback_mousedown13 = null;
            Action argcallback_mousemove13 = null;
            Action argcallback_dblclick13 = null;
            UpdateLabel(Windows.Count, "lblHealth", 18L, 116L, 147L, 10L, "Health", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm12, callback_hover: ref argcallback_hover12, callback_mousedown: ref argcallback_mousedown13, callback_mousemove: ref argcallback_mousemove13, callback_dblclick: ref argcallback_dblclick13, enabled: ref enabled);
            Action argcallback_norm13 = null;
            Action argcallback_hover13 = null;
            Action argcallback_mousedown14 = null;
            Action argcallback_mousemove14 = null;
            Action argcallback_dblclick14 = null;
            UpdateLabel(Windows.Count, "lblSpirit", 18L, 136L, 147L, 10L, "Spirit", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm13, callback_hover: ref argcallback_hover13, callback_mousedown: ref argcallback_mousedown14, callback_mousemove: ref argcallback_mousemove14, callback_dblclick: ref argcallback_dblclick14, enabled: ref enabled);
            Action argcallback_norm14 = null;
            Action argcallback_hover14 = null;
            Action argcallback_mousedown15 = null;
            Action argcallback_mousemove15 = null;
            Action argcallback_dblclick15 = null;
            UpdateLabel(Windows.Count, "lblExperience", 18L, 156L, 147L, 10L, "Experience", Core.Enum.FontType.Arial, Color.White, callback_norm: ref argcallback_norm14, callback_hover: ref argcallback_hover14, callback_mousedown: ref argcallback_mousedown15, callback_mousemove: ref argcallback_mousemove15, callback_dblclick: ref argcallback_dblclick15, enabled: ref enabled);
            Action argcallback_norm15 = null;
            Action argcallback_hover15 = null;
            Action argcallback_mousedown16 = null;
            Action argcallback_mousemove16 = null;
            Action argcallback_dblclick16 = null;
            UpdateLabel(Windows.Count, "lblName2", 13L, 36L, 147L, 10L, "Name", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm15, callback_hover: ref argcallback_hover15, callback_mousedown: ref argcallback_mousedown16, callback_mousemove: ref argcallback_mousemove16, callback_dblclick: ref argcallback_dblclick16, enabled: ref enabled);
            Action argcallback_norm16 = null;
            Action argcallback_hover16 = null;
            Action argcallback_mousedown17 = null;
            Action argcallback_mousemove17 = null;
            Action argcallback_dblclick17 = null;
            UpdateLabel(Windows.Count, "lblJob2", 13L, 56L, 147L, 10L, "", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm16, callback_hover: ref argcallback_hover16, callback_mousedown: ref argcallback_mousedown17, callback_mousemove: ref argcallback_mousemove17, callback_dblclick: ref argcallback_dblclick17, enabled: ref enabled);
            Action argcallback_norm17 = null;
            Action argcallback_hover17 = null;
            Action argcallback_mousedown18 = null;
            Action argcallback_mousemove18 = null;
            Action argcallback_dblclick18 = null;
            UpdateLabel(Windows.Count, "lblLevel2", 13L, 76L, 147L, 10L, "Level", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm17, callback_hover: ref argcallback_hover17, callback_mousedown: ref argcallback_mousedown18, callback_mousemove: ref argcallback_mousemove18, callback_dblclick: ref argcallback_dblclick18, enabled: ref enabled);
            Action argcallback_norm18 = null;
            Action argcallback_hover18 = null;
            Action argcallback_mousedown19 = null;
            Action argcallback_mousemove19 = null;
            Action argcallback_dblclick19 = null;
            UpdateLabel(Windows.Count, "lblGuild2", 13L, 96L, 147L, 10L, "Guild", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm18, callback_hover: ref argcallback_hover18, callback_mousedown: ref argcallback_mousedown19, callback_mousemove: ref argcallback_mousemove19, callback_dblclick: ref argcallback_dblclick19, enabled: ref enabled);
            Action argcallback_norm19 = null;
            Action argcallback_hover19 = null;
            Action argcallback_mousedown20 = null;
            Action argcallback_mousemove20 = null;
            Action argcallback_dblclick20 = null;
            UpdateLabel(Windows.Count, "lblHealth2", 13L, 116L, 147L, 10L, "Health", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm19, callback_hover: ref argcallback_hover19, callback_mousedown: ref argcallback_mousedown20, callback_mousemove: ref argcallback_mousemove20, callback_dblclick: ref argcallback_dblclick20, enabled: ref enabled);
            Action argcallback_norm20 = null;
            Action argcallback_hover20 = null;
            Action argcallback_mousedown21 = null;
            Action argcallback_mousemove21 = null;
            Action argcallback_dblclick21 = null;
            UpdateLabel(Windows.Count, "lblSpirit2", 13L, 136L, 147L, 10L, "Spirit", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm20, callback_hover: ref argcallback_hover20, callback_mousedown: ref argcallback_mousedown21, callback_mousemove: ref argcallback_mousemove21, callback_dblclick: ref argcallback_dblclick21, enabled: ref enabled);
            Action argcallback_norm21 = null;
            Action argcallback_hover21 = null;
            Action argcallback_mousedown22 = null;
            Action argcallback_mousemove22 = null;
            Action argcallback_dblclick22 = null;
            UpdateLabel(Windows.Count, "lblExperience2", 13L, 156L, 147L, 10L, "Experience", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm21, callback_hover: ref argcallback_hover21, callback_mousedown: ref argcallback_mousedown22, callback_mousemove: ref argcallback_mousemove22, callback_dblclick: ref argcallback_dblclick22, enabled: ref enabled);

            // Attributes
            Action argcallback_norm22 = null;
            Action argcallback_hover22 = null;
            Action argcallback_mousedown23 = null;
            Action argcallback_mousemove23 = null;
            Action argcallback_dblclick23 = null;
            Action argonDraw8 = null;
            UpdatePictureBox(Windows.Count, "picShadow", 18L, 176L, 138L, 9L, design_norm: (long)Core.Enum.DesignType.BlackOval, design_hover: (long)Core.Enum.DesignType.BlackOval, design_mousedown: (long)Core.Enum.DesignType.BlackOval, callback_norm: ref argcallback_norm22, callback_hover: ref argcallback_hover22, callback_mousedown: ref argcallback_mousedown23, callback_mousemove: ref argcallback_mousemove23, callback_dblclick: ref argcallback_dblclick23, onDraw: ref argonDraw8);
            Action argcallback_norm23 = null;
            Action argcallback_hover23 = null;
            Action argcallback_mousedown24 = null;
            Action argcallback_mousemove24 = null;
            Action argcallback_dblclick24 = null;
            UpdateLabel(Windows.Count, "lblLabel", 18L, 173L, 138L, 10L, "Attributes", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm23, callback_hover: ref argcallback_hover23, callback_mousedown: ref argcallback_mousedown24, callback_mousemove: ref argcallback_mousemove24, callback_dblclick: ref argcallback_dblclick24, enabled: ref enabled);

            // Black boxes
            Action argcallback_norm24 = null;
            Action argcallback_hover24 = null;
            Action argcallback_mousedown25 = null;
            Action argcallback_mousemove25 = null;
            Action argcallback_dblclick25 = null;
            Action argonDraw9 = null;
            UpdatePictureBox(Windows.Count, "picBlackBox", 13L, 186L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextBlack, design_hover: (long)Core.Enum.DesignType.TextBlack, design_mousedown: (long)Core.Enum.DesignType.TextBlack, callback_norm: ref argcallback_norm24, callback_hover: ref argcallback_hover24, callback_mousedown: ref argcallback_mousedown25, callback_mousemove: ref argcallback_mousemove25, callback_dblclick: ref argcallback_dblclick25, onDraw: ref argonDraw9);
            Action argcallback_norm25 = null;
            Action argcallback_hover25 = null;
            Action argcallback_mousedown26 = null;
            Action argcallback_mousemove26 = null;
            Action argcallback_dblclick26 = null;
            Action argonDraw10 = null;
            UpdatePictureBox(Windows.Count, "picBlackBox", 13L, 206L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextBlack, design_hover: (long)Core.Enum.DesignType.TextBlack, design_mousedown: (long)Core.Enum.DesignType.TextBlack, callback_norm: ref argcallback_norm25, callback_hover: ref argcallback_hover25, callback_mousedown: ref argcallback_mousedown26, callback_mousemove: ref argcallback_mousemove26, callback_dblclick: ref argcallback_dblclick26, onDraw: ref argonDraw10);
            Action argcallback_norm26 = null;
            Action argcallback_hover26 = null;
            Action argcallback_mousedown27 = null;
            Action argcallback_mousemove27 = null;
            Action argcallback_dblclick27 = null;
            Action argonDraw11 = null;
            UpdatePictureBox(Windows.Count, "picBlackBox", 13L, 226L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextBlack, design_hover: (long)Core.Enum.DesignType.TextBlack, design_mousedown: (long)Core.Enum.DesignType.TextBlack, callback_norm: ref argcallback_norm26, callback_hover: ref argcallback_hover26, callback_mousedown: ref argcallback_mousedown27, callback_mousemove: ref argcallback_mousemove27, callback_dblclick: ref argcallback_dblclick27, onDraw: ref argonDraw11);
            Action argcallback_norm27 = null;
            Action argcallback_hover27 = null;
            Action argcallback_mousedown28 = null;
            Action argcallback_mousemove28 = null;
            Action argcallback_dblclick28 = null;
            Action argonDraw12 = null;
            UpdatePictureBox(Windows.Count, "picBlackBox", 13L, 246L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextBlack, design_hover: (long)Core.Enum.DesignType.TextBlack, design_mousedown: (long)Core.Enum.DesignType.TextBlack, callback_norm: ref argcallback_norm27, callback_hover: ref argcallback_hover27, callback_mousedown: ref argcallback_mousedown28, callback_mousemove: ref argcallback_mousemove28, callback_dblclick: ref argcallback_dblclick28, onDraw: ref argonDraw12);
            Action argcallback_norm28 = null;
            Action argcallback_hover28 = null;
            Action argcallback_mousedown29 = null;
            Action argcallback_mousemove29 = null;
            Action argcallback_dblclick29 = null;
            Action argonDraw13 = null;
            UpdatePictureBox(Windows.Count, "picBlackBox", 13L, 266L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextBlack, design_hover: (long)Core.Enum.DesignType.TextBlack, design_mousedown: (long)Core.Enum.DesignType.TextBlack, callback_norm: ref argcallback_norm28, callback_hover: ref argcallback_hover28, callback_mousedown: ref argcallback_mousedown29, callback_mousemove: ref argcallback_mousemove29, callback_dblclick: ref argcallback_dblclick29, onDraw: ref argonDraw13);
            Action argcallback_norm29 = null;
            Action argcallback_hover29 = null;
            Action argcallback_mousedown30 = null;
            Action argcallback_mousemove30 = null;
            Action argcallback_dblclick30 = null;
            Action argonDraw14 = null;
            UpdatePictureBox(Windows.Count, "picBlackBox", 13L, 286L, 148L, 19L, design_norm: (long)Core.Enum.DesignType.TextBlack, design_hover: (long)Core.Enum.DesignType.TextBlack, design_mousedown: (long)Core.Enum.DesignType.TextBlack, callback_norm: ref argcallback_norm29, callback_hover: ref argcallback_hover29, callback_mousedown: ref argcallback_mousedown30, callback_mousemove: ref argcallback_mousemove30, callback_dblclick: ref argcallback_dblclick30, onDraw: ref argonDraw14);

            // Labels
            Action argcallback_norm30 = null;
            Action argcallback_hover30 = null;
            Action argcallback_mousedown31 = null;
            Action argcallback_mousemove31 = null;
            Action argcallback_dblclick31 = null;
            UpdateLabel(Windows.Count, "lblLabel", 18L, 188L, 138L, 10L, "Strength", Core.Enum.FontType.Arial, Color.Yellow, callback_norm: ref argcallback_norm30, callback_hover: ref argcallback_hover30, callback_mousedown: ref argcallback_mousedown31, callback_mousemove: ref argcallback_mousemove31, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);
            Action argcallback_norm31 = null;
            Action argcallback_hover31 = null;
            Action argcallback_mousedown32 = null;
            Action argcallback_mousemove32 = null;
            Action argcallback_dblclick32 = null;
            UpdateLabel(Windows.Count, "lblLabel", 18L, 208L, 138L, 10L, "Vitality", Core.Enum.FontType.Arial, Color.Yellow, callback_norm: ref argcallback_norm31, callback_hover: ref argcallback_hover31, callback_mousedown: ref argcallback_mousedown32, callback_mousemove: ref argcallback_mousemove32, callback_dblclick: ref argcallback_dblclick32, enabled: ref enabled);
            Action argcallback_norm32 = null;
            Action argcallback_hover32 = null;
            Action argcallback_mousedown33 = null;
            Action argcallback_mousemove33 = null;
            Action argcallback_dblclick33 = null;
            UpdateLabel(Windows.Count, "lblLabel", 18L, 228L, 138L, 10L, "Intelligence", Core.Enum.FontType.Arial, Color.Yellow, callback_norm: ref argcallback_norm32, callback_hover: ref argcallback_hover32, callback_mousedown: ref argcallback_mousedown33, callback_mousemove: ref argcallback_mousemove33, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);
            Action argcallback_norm33 = null;
            Action argcallback_hover33 = null;
            Action argcallback_mousedown34 = null;
            Action argcallback_mousemove34 = null;
            Action argcallback_dblclick34 = null;
            UpdateLabel(Windows.Count, "lblLabel", 18L, 248L, 138L, 10L, "Luck", Core.Enum.FontType.Arial, Color.Yellow, callback_norm: ref argcallback_norm33, callback_hover: ref argcallback_hover33, callback_mousedown: ref argcallback_mousedown34, callback_mousemove: ref argcallback_mousemove34, callback_dblclick: ref argcallback_dblclick34, enabled: ref enabled);
            Action argcallback_norm34 = null;
            Action argcallback_hover34 = null;
            Action argcallback_mousedown35 = null;
            Action argcallback_mousemove35 = null;
            Action argcallback_dblclick35 = null;
            UpdateLabel(Windows.Count, "lblLabel", 18L, 268L, 138L, 10L, "Spirit", Core.Enum.FontType.Arial, Color.Yellow, callback_norm: ref argcallback_norm34, callback_hover: ref argcallback_hover34, callback_mousedown: ref argcallback_mousedown35, callback_mousemove: ref argcallback_mousemove35, callback_dblclick: ref argcallback_dblclick35, enabled: ref enabled);
            Action argcallback_norm35 = null;
            Action argcallback_hover35 = null;
            Action argcallback_mousedown36 = null;
            Action argcallback_mousemove36 = null;
            Action argcallback_dblclick36 = null;
            UpdateLabel(Windows.Count, "lblLabel", 18L, 288L, 138L, 10L, "Stat Points", Core.Enum.FontType.Arial, Color.Green, callback_norm: ref argcallback_norm35, callback_hover: ref argcallback_hover35, callback_mousedown: ref argcallback_mousedown36, callback_mousemove: ref argcallback_mousemove36, callback_dblclick: ref argcallback_dblclick36, enabled: ref enabled);

            // Buttons
            var argcallback_mousedown37 = new Action(Character_SpendPoint1);
            Action argcallback_mousemove37 = null;
            Action argcallback_dblclick37 = null;
            Gui.UpdateButton(Windows.Count, "btnStat_1", 144L, 188L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown37, callback_mousemove: ref argcallback_mousemove37, callback_dblclick: ref argcallback_dblclick37);
            var argcallback_mousedown38 = new Action(Character_SpendPoint2);
            Action argcallback_mousemove38 = null;
            Action argcallback_dblclick38 = null;
            Gui.UpdateButton(Windows.Count, "btnStat_2", 144L, 208L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown38, callback_mousemove: ref argcallback_mousemove38, callback_dblclick: ref argcallback_dblclick3);
            var argcallback_mousedown39 = new Action(Character_SpendPoint3);
            Action argcallback_mousemove39 = null;
            Action argcallback_dblclick39 = null;
            Gui.UpdateButton(Windows.Count, "btnStat_3", 144L, 228L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown39, callback_mousemove: ref argcallback_mousemove39, callback_dblclick: ref argcallback_dblclick39);
            var argcallback_mousedown40 = new Action(Character_SpendPoint4);
            Action argcallback_mousemove40 = null;
            Action argcallback_dblclick40 = null;
            Gui.UpdateButton(Windows.Count, "btnStat_4", 144L, 248L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown40, callback_mousemove: ref argcallback_mousemove40, callback_dblclick: ref argcallback_dblclick4);
            var argcallback_mousedown41 = new Action(Character_SpendPoint5);
            Action argcallback_mousemove41 = null;
            Action argcallback_dblclick41 = null;
            Gui.UpdateButton(Windows.Count, "btnStat_5", 144L, 268L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown41, callback_mousemove: ref argcallback_mousemove41, callback_dblclick: ref argcallback_dblclick41);

            // fake buttons
            Action argcallback_norm36 = null;
            Action argcallback_hover36 = null;
            Action argcallback_mousedown42 = null;
            Action argcallback_mousemove42 = null;
            Action argcallback_dblclick42 = null;
            Action argonDraw15 = null;
            UpdatePictureBox(Windows.Count, "btnGreyStat_1", 144L, 188L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm36, callback_hover: ref argcallback_hover36, callback_mousedown: ref argcallback_mousedown42, callback_mousemove: ref argcallback_mousemove42, callback_dblclick: ref argcallback_dblclick42, onDraw: ref argonDraw15);
            Action argcallback_norm37 = null;
            Action argcallback_hover37 = null;
            Action argcallback_mousedown43 = null;
            Action argcallback_mousemove43 = null;
            Action argcallback_dblclick43 = null;
            Action argonDraw16 = null;
            UpdatePictureBox(Windows.Count, "btnGreyStat_2", 144L, 208L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm37, callback_hover: ref argcallback_hover37, callback_mousedown: ref argcallback_mousedown43, callback_mousemove: ref argcallback_mousemove43, callback_dblclick: ref argcallback_dblclick43, onDraw: ref argonDraw16);
            Action argcallback_norm38 = null;
            Action argcallback_hover38 = null;
            Action argcallback_mousedown44 = null;
            Action argcallback_mousemove44 = null;
            Action argcallback_dblclick44 = null;
            Action argonDraw17 = null;
            UpdatePictureBox(Windows.Count, "btnGreyStat_3", 144L, 228L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm38, callback_hover: ref argcallback_hover38, callback_mousedown: ref argcallback_mousedown44, callback_mousemove: ref argcallback_mousemove44, callback_dblclick: ref argcallback_dblclick44, onDraw: ref argonDraw17);
            Action argcallback_norm39 = null;
            Action argcallback_hover39 = null;
            Action argcallback_mousedown45 = null;
            Action argcallback_mousemove45 = null;
            Action argcallback_dblclick45 = null;
            Action argonDraw18 = null;
            UpdatePictureBox(Windows.Count, "btnGreyStat_4", 144L, 248L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm39, callback_hover: ref argcallback_hover39, callback_mousedown: ref argcallback_mousedown45, callback_mousemove: ref argcallback_mousemove45, callback_dblclick: ref argcallback_dblclick45, onDraw: ref argonDraw18);
            Action argcallback_norm40 = null;
            Action argcallback_hover40 = null;
            Action argcallback_mousedown46 = null;
            Action argcallback_mousemove46 = null;
            Action argcallback_dblclick46 = null;
            Action argonDraw19 = null;
            UpdatePictureBox(Windows.Count, "btnGreyStat_5", 144L, 268L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm40, callback_hover: ref argcallback_hover40, callback_mousedown: ref argcallback_mousedown46, callback_mousemove: ref argcallback_mousemove46, callback_dblclick: ref argcallback_dblclick46, onDraw: ref argonDraw19);

            // Labels
            Action argcallback_norm41 = null;
            Action argcallback_hover41 = null;
            Action argcallback_mousedown47 = null;
            Action argcallback_mousemove47 = null;
            Action argcallback_dblclick47 = null;
            UpdateLabel(Windows.Count, "lblStat_1", 42L, 188L, 100L, 15L, "255", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm41, callback_hover: ref argcallback_hover41, callback_mousedown: ref argcallback_mousedown47, callback_mousemove: ref argcallback_mousemove47, callback_dblclick: ref argcallback_dblclick47, enabled: ref enabled);
            Action argcallback_norm42 = null;
            Action argcallback_hover42 = null;
            Action argcallback_mousedown48 = null;
            Action argcallback_mousemove48 = null;
            Action argcallback_dblclick48 = null;
            UpdateLabel(Windows.Count, "lblStat_2", 42L, 208L, 100L, 15L, "255", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm42, callback_hover: ref argcallback_hover42, callback_mousedown: ref argcallback_mousedown48, callback_mousemove: ref argcallback_mousemove48, callback_dblclick: ref argcallback_dblclick48, enabled: ref enabled);
            Action argcallback_norm43 = null;
            Action argcallback_hover43 = null;
            Action argcallback_mousedown49 = null;
            Action argcallback_mousemove49 = null;
            Action argcallback_dblclick49 = null;
            UpdateLabel(Windows.Count, "lblStat_3", 42L, 228L, 100L, 15L, "255", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm43, callback_hover: ref argcallback_hover43, callback_mousedown: ref argcallback_mousedown49, callback_mousemove: ref argcallback_mousemove49, callback_dblclick: ref argcallback_dblclick49, enabled: ref enabled);
            Action argcallback_norm44 = null;
            Action argcallback_hover44 = null;
            Action argcallback_mousedown50 = null;
            Action argcallback_mousemove50 = null;
            Action argcallback_dblclick50 = null;
            UpdateLabel(Windows.Count, "lblStat_4", 42L, 248L, 100L, 15L, "255", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm44, callback_hover: ref argcallback_hover44, callback_mousedown: ref argcallback_mousedown50, callback_mousemove: ref argcallback_mousemove50, callback_dblclick: ref argcallback_dblclick50, enabled: ref enabled);
            Action argcallback_norm45 = null;
            Action argcallback_hover45 = null;
            Action argcallback_mousedown51 = null;
            Action argcallback_mousemove51 = null;
            Action argcallback_dblclick51 = null;
            UpdateLabel(Windows.Count, "lblStat_5", 42L, 268L, 100L, 15L, "255", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm45, callback_hover: ref argcallback_hover45, callback_mousedown: ref argcallback_mousedown51, callback_mousemove: ref argcallback_mousemove51, callback_dblclick: ref argcallback_dblclick51, enabled: ref enabled);
            Action argcallback_norm46 = null;
            Action argcallback_hover46 = null;
            Action argcallback_mousedown52 = null;
            Action argcallback_mousemove52 = null;
            Action argcallback_dblclick52 = null;
            UpdateLabel(Windows.Count, "lblPoints", 57L, 288L, 100L, 15L, "255", Core.Enum.FontType.Arial, Color.White, Core.Enum.AlignmentType.Right, callback_norm: ref argcallback_norm46, callback_hover: ref argcallback_hover46, callback_mousedown: ref argcallback_mousedown52, callback_mousemove: ref argcallback_mousemove52, callback_dblclick: ref argcallback_dblclick5, enabled: ref enabled);
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
            for (i = 0L; i < (int)Core.Enum.EquipmentType.Count; i++)
            {
                itemNum = (long)GetPlayerEquipment(GameState.MyIndex, (Core.Enum.EquipmentType)i);

                if (itemNum >= 0L)
                {
                    itemIcon = Core.Type.Item[(int)itemNum].Icon;

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

        public static void Character_DblClick()
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
            if (DragBox.Type != Core.Enum.PartType.None)
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
            NetworkSend.SendTrainStat(1);
        }

        public static void Character_SpendPoint2()
        {
            NetworkSend.SendTrainStat(2);
        }

        public static void Character_SpendPoint3()
        {
            NetworkSend.SendTrainStat(3);
        }

        public static void Character_SpendPoint4()
        {
            NetworkSend.SendTrainStat(4);
        }

        public static void Character_SpendPoint5()
        {
            NetworkSend.SendTrainStat(5);
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
                    if (!(DragBox.Origin == Core.Enum.PartOriginType.Inventory & DragBox.Slot == i))
                    {
                        itemIcon = Core.Type.Item[(int)itemNum].Icon;

                        // exit out if we're offering item in a trade.
                        amountModifier = 0L;
                        if (Trade.InTrade >= 0)
                        {
                            for (x = 0L; x < Constant.MAX_INV; x++)
                            {
                                if (Core.Type.TradeYourOffer[(int)x].Num >= 0)
                                { 
                                    tmpItem = (long)GetPlayerInv(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)x].Num);
                                    if (Core.Type.TradeYourOffer[(int)x].Num == i)
                                    {
                                        // check if currency
                                        if (!(Core.Type.Item[(int)tmpItem].Type == (byte)Core.Enum.ItemType.Currency))
                                        {
                                            // normal item, exit out
                                            skipItem = Conversions.ToBoolean(1);
                                        }
                                        // if amount = all currency, remove from inventory
                                        else if (Core.Type.TradeYourOffer[(int)x].Value == GetPlayerInvValue(GameState.MyIndex, (int)i))
                                        {
                                            skipItem = Conversions.ToBoolean(1);
                                        }
                                        else
                                        {
                                            // not all, change modifier to show change in currency count
                                            amountModifier = Core.Type.TradeYourOffer[(int)x].Value;
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
                                        Color = GameClient.QbColorToXnaColor((int)Core.Enum.ColorType.White);
                                    }
                                    else if (Conversions.ToLong(Amount) > 1000000L & Conversions.ToLong(Amount) < 10000000L)
                                    {
                                        Color = GameClient.QbColorToXnaColor((int)Core.Enum.ColorType.Yellow);
                                    }
                                    else if (Conversions.ToLong(Amount) > 10000000L)
                                    {
                                        Color = GameClient.QbColorToXnaColor((int)Core.Enum.ColorType.BrightGreen);
                                    }

                                    Text.RenderText(GameLogic.ConvertCurrency(Conversions.ToInteger(Amount)), (int)x, (int)y, Color, Color, Core.Enum.FontType.Georgia);
                                }
                            }
                        }

                        // reset
                        skipItem = Conversions.ToBoolean(0);
                    }
                }
            }
        }

        public static void UpdateWindow_Description()
        {
            // Control window
            UpdateWindow("winDescription", "", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 193L, 142L, 0L, false, design_norm: (long)Core.Enum.DesignType.Win_Desc, design_hover: (long)Core.Enum.DesignType.Win_Desc, design_mousedown: (long)Core.Enum.DesignType.Win_Desc, canDrag: false, clickThrough: true);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Name
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblName", 8L, 12L, 177L, 10L, "Flame Sword", Core.Enum.FontType.Arial, Color.Blue, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, enabled: ref enabled);

            // Sprite box
            var argonDraw = new Action(Description_OnDraw);
            Action argcallback_norm_pic = null;
            Action argcallback_hover_pic = null;
            Action argcallback_mousedown_pic = null;
            Action argcallback_mousemove_pic = null;
            Action argcallback_dblclick_pic = null;
            Gui.UpdatePictureBox(Windows.Count, "picSprite", 18L, 32L, 68L, 68L, design_norm: (long)Core.Enum.DesignType.DescPic, design_hover: (long)Core.Enum.DesignType.DescPic, design_mousedown: (long)Core.Enum.DesignType.DescPic, callback_norm: ref argcallback_norm_pic, callback_hover: ref argcallback_hover_pic, callback_mousedown: ref argcallback_mousedown_pic, callback_mousemove: ref argcallback_mousemove_pic, callback_dblclick: ref argcallback_dblclick_pic, onDraw: ref argonDraw);

            // Sep
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picSep", 96L, 28L, 0L, 92L, image_norm: 44L, image_hover: 44L, image_mousedown: 44L, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw1);

            // Requirements
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            UpdateLabel(Windows.Count, "lblClass", 5L, 102L, 92L, 10L, "Warrior", Core.Enum.FontType.Georgia, Color.Green, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, enabled: ref enabled);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            UpdateLabel(Windows.Count, "lblLevel", 5L, 114L, 92L, 10L, "Level 20", Core.Enum.FontType.Georgia, Color.Red, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);

            // Bar
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picBar", 19L, 114L, 66L, 12L, false, image_norm: 45L, image_hover: 45L, image_mousedown: 45L, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw2);
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
                        texNum = Core.Type.Item[(int)GameState.descItem].Icon;

                        // render sprite
                        string argpath = System.IO.Path.Combine(Path.Items, texNum.ToString());
                        GameClient.RenderTexture(ref argpath, (int)(xO + 20L), (int)(yO + 34L), 0, 0, 64, 64, 32, 32);
                        break;
                    }

                case 2: // Skill Icon
                    {
                        texNum = Core.Type.Skill[(int)GameState.descItem].Icon;

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
            count = Information.UBound(GameState.descText);
            var loopTo = count;
            for (i = 0L; i < loopTo; i++)
            {
                Text.RenderText(GameState.descText[(int)i].Text, (int)(xO + 140L - Text.GetTextWidth(GameState.descText[(int)i].Text) / 2), (int)(yO + y), GameClient.ToXnaColor(GameState.descText[(int)i].Color), Color.Black);
                y = y + 12L;
            }
        }

        public static void UpdateWindow_DragBox()
        {
            // Control window
            UpdateWindow("winDragBox", "", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 32L, 32L, 0L, false, onDraw: new Action(DragBox_OnDraw));

            // Need to set up unique mouseup event
            Windows[Windows.Count].CallBack[(int)Core.Enum.EntState.MouseUp] = new Action(DragBox_Check);
        }

        public static void UpdateWindow_Options()
        {
            UpdateWindow("winOptions", "", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 210L, 212L, 0L, Conversions.ToBoolean(0), design_norm: (long)Core.Enum.DesignType.Win_NoBar, design_hover: (long)Core.Enum.DesignType.Win_NoBar, design_mousedown: (long)Core.Enum.DesignType.Win_NoBar, isActive: false, clickThrough: false);

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Parchment
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Windows.Count, "picParchment", 6L, 6L, 198L, 200L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);

            // General
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Windows.Count, "picBlank", 35L, 25L, 140L, 10L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblBlank", 35L, 22L, 140L, 0L, "General Options", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, enabled: ref enabled);

            // Check boxes
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            UpdateCheckBox(Windows.Count, "chkMusic", 35L, 40L, 80L, text: "Music", font: Core.Enum.FontType.Georgia, theDesign: (long)Core.Enum.DesignType.ChkNorm, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3);
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            UpdateCheckBox(Windows.Count, "chkSound", 115L, 40L, 80L, text: "Sound", font: Core.Enum.FontType.Georgia, theDesign: (long)Core.Enum.DesignType.ChkNorm, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            UpdateCheckBox(Windows.Count, "chkAutotile", 35L, 60L, 80L, text: "Autotile", font: Core.Enum.FontType.Georgia, theDesign: (long)Core.Enum.DesignType.ChkNorm, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            UpdateCheckBox(Windows.Count, "chkFullscreen", 115L, 60L, 80L, text: "Fullscreen", font: Core.Enum.FontType.Georgia, theDesign: (long)Core.Enum.DesignType.ChkNorm, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6);

            // Resolution
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picBlank", 35L, 85L, 140L, 10L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, onDraw: ref argonDraw2);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            UpdateLabel(Windows.Count, "lblBlank", 35L, 92L, 140L, 10L, "Select Resolution", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Center, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, enabled: ref enabled);

            // combobox
            UpdateComboBox(Windows.Count, "cmbRes", 30L, 100L, 150L, 18L, (long)Core.Enum.DesignType.ComboNorm);

            // Button
            Action argcallback_mousedown9 = btnOptions_Confirm;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Gui.UpdateButton(Windows.Count, "btnConfirm", 65L, 168L, 80L, 22L, "Confirm", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_hover: ref argcallback_hover, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9);

            // Populate the options screen
            GameLogic.SetOptionsScreen();
        }

        public static void UpdateWindow_Combobox()
        {
            // background window
            UpdateWindow("winComboMenuBG", "ComboMenuBG", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 800L, 600L, 0L, false, callback_dblclick: new Action(CloseComboMenu), zChange: Conversions.ToByte(false), isActive: false);

            // window
            UpdateWindow("winComboMenu", "ComboMenu", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 100L, 100L, 0L, false, design_norm: (long)Core.Enum.DesignType.ComboMenuNorm, isActive: false, clickThrough: false);

            // centralize it
            CentralizeWindow(Windows.Count);
        }

        public static void UpdateWindow_Skills()
        {
            // Control window
            UpdateWindow("winSkills", "Skills", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 202L, 297L, 109L, false, 2L, 7L, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, callback_mousemove: new Action(Skills_MouseMove), callback_mousedown: new Action(Skills_MouseDown), callback_dblclick: new Action(Skills_DblClick), onDraw: new Action(DrawSkills));

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnMenu_Skills);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 16L, 16L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
        }

        public static void UpdateWindow_Bank()
        {
            UpdateWindow("winBank", "Bank", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 390L, 373L, 0L, false, 2L, 5L, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, callback_mousemove: new Action(Bank_MouseMove), callback_mousedown: new Action(Bank_MouseDown), callback_dblclick: new Action(Bank_DblClick), onDraw: new Action(DrawBank));

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;
            var argcallback_mousedown = new Action(btnMenu_Bank);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 5L, 36L, 36L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
        }

        public static void UpdateWindow_Shop()
        {
            // Control window
            UpdateWindow("winShop", "Shop", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 278L, 293L, 17L, false, 2L, 5L, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, (long)Core.Enum.DesignType.Win_Empty, callback_mousemove: new Action(Shop_MouseMove), callback_mousedown: new Action(Shop_MouseDown), onDraw: new Action(DrawShopBackground));

            // Centralize it
            CentralizeWindow(Windows.Count);

            // Close button
            var argcallback_mousedown = new Action(btnShop_Close);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnClose", Windows[Windows.Count].Width - 19L, 6L, 36L, 36L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Parchment
            var argonDraw = new Action(DrawShop);
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Gui.UpdatePictureBox(Windows.Count, "picParchment", 6L, 215L, 266L, 50L, design_norm: (long)Core.Enum.DesignType.Parchment, design_hover: (long)Core.Enum.DesignType.Parchment, design_mousedown: (long)Core.Enum.DesignType.Parchment, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // Picture Box
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Windows.Count, "picItemBG", 13L, 222L, 36L, 36L, image_norm: 30L, image_hover: 30L, image_mousedown: 30L, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw2);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Windows.Count, "picItem", 15L, 224L, 32L, 32L, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw3);

            // Buttons
            var argcallback_mousedown4 = new Action(btnShopBuy);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Gui.UpdateButton(Windows.Count, "btnBuy", 190L, 228L, 70L, 24L, "Buy", Core.Enum.FontType.Arial, design_norm: (long)Core.Enum.DesignType.Green, design_hover: (long)Core.Enum.DesignType.Green_Hover, design_mousedown: (long)Core.Enum.DesignType.Green_Click, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
            var argcallback_mousedown5 = new Action(btnShopSell);
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Gui.UpdateButton(Windows.Count, "btnSell", 190L, 228L, 70L, 24L, "Sell", Core.Enum.FontType.Arial, visible: false, design_norm: (long)Core.Enum.DesignType.Red, design_hover: (long)Core.Enum.DesignType.Red_Hover, design_mousedown: (long)Core.Enum.DesignType.Red_Click, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5);

            // Buying/Selling
            var argcallback_mousedown6 = new Action(chkShopBuying);
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Gui.UpdateCheckBox(Windows.Count, "chkBuying", 173L, 265L, 49L, 20L, 0L, theDesign: (long)Core.Enum.DesignType.ChkBuying, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6);
            var argcallback_mousedown7 = new Action(chkShopSelling);
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Gui.UpdateCheckBox(Windows.Count, "chkSelling", 222L, 265L, 49L, 20L, 0L, theDesign: (long)Core.Enum.DesignType.ChkSelling, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7);

            // Labels
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            bool enabled = false;
            UpdateLabel(Windows.Count, "lblName", 56L, 226L, 300L, 10L, "Test Item", Core.Enum.FontType.Arial, Color.Black, Core.Enum.AlignmentType.Left, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, enabled: ref enabled);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            UpdateLabel(Windows.Count, "lblCost", 56L, 240L, 300L, 10L, "1000g", Core.Enum.FontType.Arial, Color.Black, Core.Enum.AlignmentType.Left, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, enabled: ref enabled);

            // Gold
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            //UpdateLabel(Windows.Count, "lblGold", 44L, 269L, 300L, 10L, "g", Core.Enum.FontType.Georgia, Color.White, Core.Enum.AlignmentType.Left, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, enabled: ref enabled);
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
                if (withBlock.Controls[GetControlIndex("winShop", "chkBuying")].Value == 0L)
                {
                    withBlock.Controls[GetControlIndex("winShop", "chkSelling")].Value = 0L;
                }
                else
                {
                    withBlock.Controls[GetControlIndex("winShop", "chkSelling")].Value = 0L;
                    withBlock.Controls[GetControlIndex("winShop", "chkBuying")].Value = 0L;
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
            GameState.shopIsSelling = Conversions.ToBoolean(0);
            GameState.shopSelectedSlot = 0L;
            UpdateShop();
        }

        public static void chkShopSelling()
        {
            {
                var withBlock = Windows[GetWindowIndex("winShop")];
                if (withBlock.Controls[GetControlIndex("winShop", "chkSelling")].Value == 0L)
                {
                    withBlock.Controls[GetControlIndex("winShop", "chkBuying")].Value = 0L;
                }
                else
                {
                    withBlock.Controls[GetControlIndex("winShop", "chkBuying")].Value = 0L;
                    withBlock.Controls[GetControlIndex("winShop", "chkSelling")].Value = 0L;
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
            GameState.shopIsSelling = Conversions.ToBoolean(1);
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
                    if (Core.Type.Shop[GameState.InShop].TradeItem[shopNum].Item >= 0)
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
                    itemNum = Core.Type.Shop[GameState.InShop].TradeItem[(int)shopSlot].Item;
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
                skillNum = (long)Core.Type.Player[GameState.MyIndex].Skill[(int)i].Num;
                if (skillNum >= 0L & skillNum < Constant.MAX_SKILLS)
                {
                    Database.StreamSkill((int)skillNum);

                    // not dragging?
                    if (!(DragBox.Origin == Core.Enum.PartOriginType.Skill & DragBox.Slot == i))
                    {
                        SkillPic = Core.Type.Skill[(int)skillNum].Icon;

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
                    Text.AddText("Music turned off.", (int)Core.Enum.ColorType.BrightGreen);
                    Sound.StopMusic();
                }
                else
                {
                    Text.AddText("Music tured on.", (int)Core.Enum.ColorType.BrightGreen);
                    // play music
                    if (GameState.InGame)
                        musicFile = Core.Type.MyMap.Music;
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
                    Text.AddText("Sound turned off.", (int)Core.Enum.ColorType.BrightGreen);
                }
                else
                {
                    Text.AddText("Sound tured on.", (int)Core.Enum.ColorType.BrightGreen);
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
                        Text.AddText("Autotiles turned off.", (int)Core.Enum.ColorType.BrightGreen);
                        Autotile.InitAutotiles();
                    }
                }
                else if (GameState.InGame)
                {
                    Text.AddText("Autotiles turned on.", (int)Core.Enum.ColorType.BrightGreen);
                    Autotile.InitAutotiles();
                }
            }

            // fullscreen
            Value = Windows[GetWindowIndex("winOptions")].Controls[GetControlIndex("winOptions", "chkFullscreen")].Value;
            if (Conversions.ToLong(SettingsManager.Instance.Fullscreen) != Value)
            {
                SettingsManager.Instance.Fullscreen = Conversions.ToBoolean(Value);
                message = Conversions.ToBoolean(1);
            }

            // resolution
            {
                var withBlock = Windows[GetWindowIndex("winOptions")].Controls[GetControlIndex("winOptions", "cmbRes")];
                if (withBlock.Value > 0L & withBlock.Value <= 13L)
                {
                    message = Conversions.ToBoolean(1);
                }
            }

            // save options
            SettingsManager.Save();

            // let them know
            if (GameState.InGame)
            {
                if (message)
                    Text.AddText("Some changes will take effect next time you load the game.", (int)Core.Enum.ColorType.BrightGreen);
            }

            // close
            btnOptions_Close();
        }

        public static void UpdateWindow_RightClick()
        {
            // Control window
            UpdateWindow("winRightClickBG", "", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 800L, 600L, 0L, false, callback_mousedown: new Action(RightClick_Close), canDrag: false);

            // Centralize it
            CentralizeWindow(Windows.Count);
        }

        public static void UpdateWindow_PlayerMenu()
        {
            // Control window  
            UpdateWindow("winPlayerMenu", "", Core.Enum.FontType.Georgia, zOrder_Win, 0L, 0L, 110L, 106L, 0L, false, design_norm: (long)Core.Enum.DesignType.Win_Desc, design_hover: (long)Core.Enum.DesignType.Win_Desc, design_mousedown: (long)Core.Enum.DesignType.Win_Desc, callback_mousedown: new Action(RightClick_Close), canDrag: false);

            // Centralize it  
            CentralizeWindow(Windows.Count);

            // Name  
            var argcallback_mousedown = new Action(RightClick_Close);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Windows.Count, "btnName", 8L, 8L, 94L, 18L, "[Name]", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.MenuHeader, design_hover: (long)Core.Enum.DesignType.MenuHeader, design_mousedown: (long)Core.Enum.DesignType.MenuHeader, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Options  
            var argcallback_mousedown1 = new Action(PlayerMenu_Party);
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Gui.UpdateButton(Windows.Count, "btnParty", 8L, 26L, 94L, 18L, "Invite to Party", Core.Enum.FontType.Georgia, design_hover: (long)Core.Enum.DesignType.MenuOption, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1);

            var argcallback_mousedown2 = new Action(PlayerMenu_Trade);
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Gui.UpdateButton(Windows.Count, "btnTrade", 8L, 44L, 94L, 18L, "Request Trade", Core.Enum.FontType.Georgia, design_hover: (long)Core.Enum.DesignType.MenuOption, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2);

            var argcallback_mousedown3 = new Action(PlayerMenu_Guild);
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Gui.UpdateButton(Windows.Count, "btnGuild", 8L, 62L, 94L, 18L, "Invite to Guild", Core.Enum.FontType.Georgia, design_norm: (long)Core.Enum.DesignType.MenuOption, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3);

            var argcallback_mousedown4 = new Action(PlayerMenu_Player);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Gui.UpdateButton(Windows.Count, "btnPM", 8L, 80L, 94L, 18L, "Private Message", Core.Enum.FontType.Georgia, design_hover: (long)Core.Enum.DesignType.MenuOption, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
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
            Text.AddText("System not yet in place.", (int)Core.Enum.ColorType.BrightRed);
        }

        public static void PlayerMenu_Player()
        {
            RightClick_Close();
            Text.AddText("System not yet in place.", (int)Core.Enum.ColorType.BrightRed);
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
                    GameState.shopSelectedItem = Core.Type.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].Item;
                    // labels
                    if (GameState.shopSelectedItem >= 0L)
                    {
                        withBlock.Controls[GetControlIndex("winShop", "lblName")].Text = Core.Type.Item[(int)GameState.shopSelectedItem].Name;
                        // check if it's gold
                        if (Core.Type.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostItem == 0)
                        {
                            // it's gold
                            withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = Core.Type.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostValue + "g";
                        }
                        // if it's one then just print the name
                        else if (Core.Type.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostValue == 1)
                        {
                            withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = Core.Type.Item[Core.Type.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostItem].Name;
                        }
                        else
                        {
                            withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = Core.Type.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostValue + " " + Core.Type.Item[Core.Type.Shop[GameState.InShop].TradeItem[(int)GameState.shopSelectedSlot].CostItem].Name;
                        }

                        // draw the item
                        for (i = 0L; i <= 4L; i++)
                        {
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Image[(int)i] = Core.Type.Item[(int)GameState.shopSelectedItem].Icon;
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
                        withBlock.Controls[GetControlIndex("winShop", "lblName")].Text = Core.Type.Item[(int)GameState.shopSelectedItem].Name;
                        // calc cost
                        CostValue = (long)Math.Round(Core.Type.Item[(int)GameState.shopSelectedItem].Price / 100d * Core.Type.Shop[GameState.InShop].BuyRate);
                        withBlock.Controls[GetControlIndex("winShop", "lblCost")].Text = CostValue + "g";

                        // draw the item
                        for (i = 0L; i <= 4L; i++)
                        {
                            withBlock.Controls[GetControlIndex("winShop", "picItem")].Image[(int)i] = Core.Type.Item[(int)GameState.shopSelectedItem].Icon;
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
            if (Core.Type.MyParty.Leader == 0)
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

                var loopTo = (long)Core.Type.MyParty.MemberCount;
                for (i = 0L; i < loopTo; i++)
                {
                    // cache the index
                    pIndex = Core.Type.MyParty.Member[(int)i];
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
                switch (Core.Type.MyParty.MemberCount)
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
                if (!(DragBox.Origin == Core.Enum.PartOriginType.Hotbar & DragBox.Slot == i))
                {
                    switch (Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].SlotType)
                    {
                        case (byte)Core.Enum.PartOriginType.Inventory:
                            {
                                Item.StreamItem((int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot);
                                if (Strings.Len(Core.Type.Item[(int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Name) > 0 & Core.Type.Item[(int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon > 0)
                                {
                                    string argpath4 = System.IO.Path.Combine(Path.Items, Core.Type.Item[(int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon.ToString());
                                    GameClient.RenderTexture(ref argpath4, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32);
                                }

                                break;
                            }

                        case (byte)Core.Enum.PartOriginType.Skill:
                            {
                                Database.StreamSkill((int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot);
                                if (Strings.Len(Core.Type.Skill[(int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Name) > 0 & Core.Type.Skill[(int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon > 0)
                                {
                                    string argpath5 = System.IO.Path.Combine(Path.Skills, Core.Type.Skill[(int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon.ToString());
                                    GameClient.RenderTexture(ref argpath5, (int)xO, (int)yO, 0, 0, 32, 32, 32, 32);
                                    for (t = 0L; t < Constant.MAX_PLAYER_SKILLS; t++)
                                    {
                                        if (GetPlayerSkill(GameState.MyIndex, (int)t) >= 0)
                                        {
                                            if (GetPlayerSkill(GameState.MyIndex, (int)t) == Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot & GetPlayerSkillCD(GameState.MyIndex, (int)t) > 0)
                                            {
                                                string argpath6 = System.IO.Path.Combine(Path.Skills, Core.Type.Skill[(int)Core.Type.Player[GameState.MyIndex].Hotbar[(int)i].Slot].Icon.ToString());
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
                    itemNum = Core.Type.Shop[GameState.InShop].TradeItem[(int)i].Item;

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
                        itemIcon = Core.Type.Item[(int)itemNum].Icon;
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
                        itemIcon = Core.Type.Item[(int)itemNum].Icon;
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
                                    Color = (long)Core.Enum.ColorType.White;
                                }
                                else if (Amount > 1000000L & Amount < 10000000L)
                                {
                                    Color = (long)Core.Enum.ColorType.Yellow;
                                }
                                else if (Amount > 10000000L)
                                {
                                    Color = (long)Core.Enum.ColorType.BrightGreen;
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
                    if (!(DragBox.Origin == Core.Enum.PartOriginType.Bank & DragBox.Slot == i))
                    {
                        itemIcon = Core.Type.Item[(int)itemNum].Icon;

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
                                    color = (long)Core.Enum.ColorType.White;
                                }
                                else if (amount > 1000000L & amount < 10000000L)
                                {
                                    color = (long)Core.Enum.ColorType.Yellow;
                                }
                                else if (amount > 10000000L)
                                {
                                    color = (long)Core.Enum.ColorType.BrightGreen;
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
                if (Core.Type.TradeYourOffer[(int)i].Num >= 0)
                {
                    itemNum = (long)GetPlayerInv(GameState.MyIndex, (int)Core.Type.TradeYourOffer[(int)i].Num);
                    if (itemNum >= 0L & itemNum < Constant.MAX_ITEMS)
                    {
                        Item.StreamItem((int)itemNum);
                        ItemPic = Core.Type.Item[(int)itemNum].Icon;

                        if (ItemPic > 0L & ItemPic <= GameState.NumItems)
                        {
                            Top = Yo + GameState.TradeTop + (GameState.TradeOffsetY + 32L) * (i / GameState.TradeColumns);
                            Left = Xo + GameState.TradeLeft + (GameState.TradeOffsetX + 32L) * (i % GameState.TradeColumns);

                            // draw icon
                            string argpath = System.IO.Path.Combine(Path.Items, ItemPic.ToString());
                            GameClient.RenderTexture(ref argpath, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);

                            // If item is a stack - draw the amount you have
                            if (Core.Type.TradeYourOffer[(int)i].Value > 1)
                            {
                                Y = Top + 20L;
                                X = Left + 1L;
                                Amount = Core.Type.TradeYourOffer[(int)i].Value.ToString();

                                // Draw currency but with k, m, b etc. using a convertion function
                                if (Conversions.ToLong(Amount) < 1000000L)
                                {
                                    Color = (long)Core.Enum.ColorType.White;
                                }
                                else if (Conversions.ToLong(Amount) > 1000000L & Conversions.ToLong(Amount) < 10000000L)
                                {
                                    Color = (long)Core.Enum.ColorType.Yellow;
                                }
                                else if (Conversions.ToLong(Amount) > 10000000L)
                                {
                                    Color = (long)Core.Enum.ColorType.BrightGreen;
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
                itemNum = (long)Core.Type.TradeTheirOffer[(int)i].Num;
                if (itemNum >= 0L & itemNum < Constant.MAX_ITEMS)
                {
                    Item.StreamItem((int)itemNum);
                    ItemPic = Core.Type.Item[(int)itemNum].Icon;

                    if (ItemPic > 0L & ItemPic <= GameState.NumItems)
                    {
                        Top = Yo + GameState.TradeTop + (GameState.TradeOffsetY + 32L) * (i / GameState.TradeColumns);
                        Left = Xo + GameState.TradeLeft + (GameState.TradeOffsetX + 32L) * (i % GameState.TradeColumns);

                        // draw icon
                        string argpath = System.IO.Path.Combine(Path.Items, ItemPic.ToString());
                        GameClient.RenderTexture(ref argpath, (int)Left, (int)Top, 0, 0, 32, 32, 32, 32);

                        // If item is a stack - draw the amount you have
                        if (Core.Type.TradeTheirOffer[(int)i].Value > 1)
                        {
                            Y = Top + 20L;
                            X = Left + 1L;
                            Amount = Core.Type.TradeTheirOffer[(int)i].Value.ToString();

                            // Draw currency but with k, m, b etc. using a convertion function
                            if (Conversions.ToLong(Amount) < 1000000L)
                            {
                                Color = (long)Core.Enum.ColorType.White;
                            }
                            else if (Conversions.ToLong(Amount) > 1000000L & Conversions.ToLong(Amount) < 10000000L)
                            {
                                Color = (long)Core.Enum.ColorType.Yellow;
                            }
                            else if (Conversions.ToLong(Amount) > 10000000L)
                            {
                                Color = (long)Core.Enum.ColorType.BrightGreen;
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
            GameState.inSmallChat = Conversions.ToBoolean(0);
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

            GameState.inSmallChat = Conversions.ToBoolean(1);
            GameState.ChatScroll = 0L;
        }

        private static string FilterUnsupportedCharacters(string text, Core.Enum.FontType fontType)
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