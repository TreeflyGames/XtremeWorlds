using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Client.Gui;

namespace Client.Content.Skins
{

    public class Crystalshire
    {

        public static void UpdateWindow_Login()
        {
            // Control the window
            UpdateWindow("winLogin", "Login", Core.Font.Georgia, zOrder_Win, 0L, 0L, 276L, 212L, 45L, true, 3L, 5L, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Parchment
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 26L, 264L, 180L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);

            // Shadows
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_1", 67L, 43L, 142L, 9L, design_norm: (long)UiDesign.BlackOval, design_hover: (long)UiDesign.BlackOval, design_mousedown: (long)UiDesign.BlackOval, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_2", 67L, 79L, 142L, 9L, design_norm: (long)UiDesign.BlackOval, design_hover: (long)UiDesign.BlackOval, design_mousedown: (long)UiDesign.BlackOval, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw2);

            // Close button
            var argcallback_mousedown3 = new Action(General.DestroyGame);
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3);

            // Buttons
            var argcallback_mousedown4 = new Action(NetworkSend.btnLogin_Click);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnAccept", 67L, 134L, 67L, 22L, "Accept", Core.Font.Arial, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
            var argcallback_mousedown5 = new Action(General.DestroyGame);
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnExit", 142L, 134L, 67L, 22L, "Exit", Core.Font.Arial, design_norm: (long)UiDesign.Red, design_hover: (long)UiDesign.RedHover, design_mousedown: (long)UiDesign.RedClick, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5);

            // Labels
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblUsername", 72L, 39L, 142L, 10L, "Username", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, enabled: ref enabled);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            UpdateLabel(Gui.Windows.Count, "lblPassword", 72L, 75L, 142L, 10L, "Password", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, enabled: ref enabled);

            // Textboxes
            if (SettingsManager.Instance.SaveUsername == true)
            {
                Action argcallback_norm8 = null;
                Action argcallback_hover8 = null;
                Action argcallback_mousedown8 = null;
                Action argcallback_mousemove8 = null;
                Action argcallback_dblclick8 = null;
                Action argcallback_enter = null;
                UpdateTextbox(Gui.Windows.Count, "txtUsername", 67L, 55L, 142L, 19L, SettingsManager.Instance.Username, Core.Font.Arial, Alignment.Left, xOffset: 5L, yOffset: 3L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, callback_enter: ref argcallback_enter);
            }
            else
            {
                Action argcallback_norm9 = null;
                Action argcallback_hover9 = null;
                Action argcallback_mousedown9 = null;
                Action argcallback_mousemove9 = null;
                Action argcallback_dblclick9 = null;
                Action argcallback_enter1 = null;
                UpdateTextbox(Gui.Windows.Count, "txtUsername", 67L, 55L, 142L, 19L, "", Core.Font.Arial, Alignment.Left, xOffset: 5L, yOffset: 3L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, callback_enter: ref argcallback_enter1);
            }
            Action argcallback_norm10 = null;
            Action argcallback_hover10 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Action argcallback_enter2 = null;
            UpdateTextbox(Gui.Windows.Count, "txtPassword", 67L, 86L, 142L, 19L, font: Core.Font.Arial, align: Alignment.Left, xOffset: 5L, yOffset: 3L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, censor: true, callback_norm: ref argcallback_norm10, callback_hover: ref argcallback_hover10, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, callback_enter: ref argcallback_enter2);

            // Checkbox
            var argcallback_mousedown11 = new Action(chkSaveUser_Click);
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Action argcallback_norm11 = null;
            Action argcallback_hover11 = null;
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkSaveUsername", 67L, 114L, 142L, value: Conversions.ToLong(SettingsManager.Instance.SaveUsername), text: "Save Username?", font: Core.Font.Arial, theDesign: (long)UiDesign.CheckboxNormal, callback_norm: ref argcallback_norm11, callback_hover: ref argcallback_hover11, callback_mousedown: ref argcallback_mousedown11, callback_mousemove: ref argcallback_mousemove11, callback_dblclick: ref argcallback_dblclick11);

            // Register Button
            var argcallback_mousedown12 = new Action(btnRegister_Click);
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            Action argcallback_norm12 = null;
            Action argcallback_hover12 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnRegister", 12L, Gui.Windows[Gui.Windows.Count].Height - 35L, 252L, 22L, "Register Account", Core.Font.Arial, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref argcallback_norm12, callback_hover: ref argcallback_hover12, callback_mousedown: ref argcallback_mousedown12, callback_mousemove: ref argcallback_mousemove12, callback_dblclick: ref argcallback_dblclick12);

            // Set the active control
            if (!(Strings.Len(Gui.Windows[GetWindowIndex("winLogin")].Controls[GetControlIndex("winLogin", "txtUsername")].Text) > 0))
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
            UpdateWindow("winRegister", "Register Account", Core.Font.Georgia, zOrder_Win, 0L, 0L, 276L, 202L, 45L, false, 3L, 5L, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnReturnMain_Click);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Font.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 26L, 264L, 170L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // Shadows
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_1", 67L, 43L, 142L, 9L, design_norm: (long)UiDesign.BlackOval, design_hover: (long)UiDesign.BlackOval, design_mousedown: (long)UiDesign.BlackOval, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_2", 67L, 79L, 142L, 9L, design_norm: (long)UiDesign.BlackOval, design_hover: (long)UiDesign.BlackOval, design_mousedown: (long)UiDesign.BlackOval, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw2);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_3", 67L, 115L, 142L, 9L, design_norm: (long)UiDesign.BlackOval, design_hover: (long)UiDesign.BlackOval, design_mousedown: (long)UiDesign.BlackOval, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw3);
            // UpdatePictureBox(Gui.Windows.Count, "picShadow_4", 67, 151, 142, 9, , , , , , , , UiDesign.BlackOval, UiDesign.BlackOval, UiDesign.BlackOval)
            // UpdatePictureBox(Gui.Windows.Count, "picShadow_5", 67, 187, 142, 9, , , , , , , , UiDesign.BlackOval, UiDesign.BlackOval, UiDesign.BlackOval)

            // Buttons
            var argcallback_mousedown5 = new Action(btnSendRegister_Click);
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnAccept", 68L, 152L, 67L, 22L, "Accept", Core.Font.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown5, ref argcallback_mousemove5, ref argcallback_dblclick5, 0L, 0L, "", false);

            var argcallback_mousedown6 = new Action(btnReturnMain_Click);
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnExit", 142L, 152L, 67L, 22L, "Back", Core.Font.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)UiDesign.Red, (long)UiDesign.RedHover, (long)UiDesign.RedClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown6, ref argcallback_mousemove6, ref argcallback_dblclick6, 0L, 0L, "", false);

            // Labels
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblUsername", 66L, 39L, 142L, 10L, "Username", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm4, ref argcallback_hover4, ref argcallback_mousedown7, ref argcallback_mousemove7, ref argcallback_dblclick7, enabled: ref enabled);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            UpdateLabel(Gui.Windows.Count, "lblPassword", 66L, 75L, 142L, 10L, "Password", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm5, ref argcallback_hover5, ref argcallback_mousedown8, ref argcallback_mousemove8, ref argcallback_dblclick8, enabled: ref enabled);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            UpdateLabel(Gui.Windows.Count, "lblRetypePassword", 66L, 110L, 142L, 10L, "Retype Password", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm6, ref argcallback_hover6, ref argcallback_mousedown9, ref argcallback_mousemove9, ref argcallback_dblclick9, enabled: ref enabled);
            // UpdateLabel(Gui.Windows.Count, "lblCode", 66, 147, 142, 10, "Secret Code", Core.Font.Arial, Alignment.Center)
            // UpdateLabel(Gui.Windows.Count, "lblCaptcha", 66, 183, 142, 10, "Captcha", Core.Font.Arial, Alignment.Center)

            // Textboxes
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Action argcallback_enter = null;
            UpdateTextbox(Gui.Windows.Count, "txtUsername", 67L, 55L, 142L, 19L, "", Core.Font.Arial, Alignment.Left, true, 255L, true, 5L, 3L, 0L, 0L, 0L, (long)UiDesign.TextWhite, (long)UiDesign.TextWhite, (long)UiDesign.TextWhite, false, 0L, Constant.NAME_LENGTH, ref argcallback_norm7, ref argcallback_hover7, ref argcallback_mousedown10, ref argcallback_mousemove10, ref argcallback_dblclick10, ref argcallback_enter);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Action argcallback_enter1 = null;
            UpdateTextbox(Gui.Windows.Count, "txtPassword", 67L, 90L, 142L, 19L, "", Core.Font.Arial, Alignment.Left, true, 255L, true, 5L, 3L, 0L, 0L, 0L, (long)UiDesign.TextWhite, (long)UiDesign.TextWhite, (long)UiDesign.TextWhite, true, 0L, Constant.NAME_LENGTH, ref argcallback_norm8, ref argcallback_hover8, ref argcallback_mousedown11, ref argcallback_mousemove11, ref argcallback_dblclick11, ref argcallback_enter1);
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            Action argcallback_enter2 = null;
            UpdateTextbox(Gui.Windows.Count, "txtRetypePassword", 67L, 127L, 142L, 19L, "", Core.Font.Arial, Alignment.Left, true, 255L, true, 5L, 3L, 0L, 0L, 0L, (long)UiDesign.TextWhite, (long)UiDesign.TextWhite, (long)UiDesign.TextWhite, true, 0L, Constant.NAME_LENGTH, ref argcallback_norm9, ref argcallback_hover9, ref argcallback_mousedown12, ref argcallback_mousemove12, ref argcallback_dblclick12, ref argcallback_enter2);
            // UpdateTextbox(Gui.Windows.Count, "txtCode", 67, 163, 142, 19, , Core.Font.Arial, , Alignment.Left, , , , , , UiDesign.TextWhite, UiDesign.TextWhite, UiDesign.TextWhite, False)
            // UpdateTextbox(Gui.Windows.Count, "txtCaptcha", 67, 235, 142, 19, , Core.Font.Arial, , Alignment.Left, , , , , , UiDesign.TextWhite, UiDesign.TextWhite, UiDesign.TextWhite, False)

            // UpdatePictureBox(Gui.Windows.Count, "picCaptcha", 67, 199, 156, 30, , , , , Tex_Captcha(GlobalCaptcha), Tex_Captcha(GlobalCaptcha), Tex_Captcha(GlobalCaptcha), UiDesign.BlackOval, UiDesign.BlackOval, UiDesign.BlackOval)

            SetActiveControl(GetWindowIndex("winRegister"), GetControlIndex("winRegister", "txtUsername"));
        }

        public static void UpdateWindow_NewChar()
        {
            // Control window
            UpdateWindow("winNewChar", "Create Character", Core.Font.Georgia, zOrder_Win, 0L, 0L, 290L, 172L, 17L, false, 2L, 6L, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnNewChar_Cancel);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Font.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 26L, 278L, 140L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.Parchment, (long)UiDesign.Parchment, (long)UiDesign.Parchment, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown1, ref argcallback_mousemove1, ref argcallback_dblclick1, ref argonDraw);

            // Name
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_1", 29L, 42L, 124L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, "", ref argcallback_norm1, ref argcallback_hover1, ref argcallback_mousedown2, ref argcallback_mousemove2, ref argcallback_dblclick2, ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblName", 29L, 39L, 124L, 10L, "Name", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm2, ref argcallback_hover2, ref argcallback_mousedown3, ref argcallback_mousemove3, ref argcallback_dblclick3, ref enabled);

            // Textbox
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_enter = null;
            UpdateTextbox(Gui.Windows.Count, "txtName", 29L, 55L, 124L, 19L, "", Core.Font.Arial, Alignment.Left, true, 255L, true, 5L, 3L, 0L, 0L, 0L, (long)UiDesign.TextWhite, (long)UiDesign.TextWhite, (long)UiDesign.TextWhite, false, 0L, Constant.NAME_LENGTH, ref argcallback_norm3, ref argcallback_hover3, ref argcallback_mousedown4, ref argcallback_mousemove4, ref argcallback_dblclick4, ref argcallback_enter);

            // Sex
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_2", 29L, 85L, 124L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, "", ref argcallback_norm4, ref argcallback_hover4, ref argcallback_mousedown5, ref argcallback_mousemove5, ref argcallback_dblclick5, ref argonDraw2);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            UpdateLabel(Gui.Windows.Count, "lblGender", 29L, 82L, 124L, 10L, "Gender", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm5, ref argcallback_hover5, ref argcallback_mousedown6, ref argcallback_mousemove6, ref argcallback_dblclick6, ref enabled);

            // Checkboxes
            var argcallback_mousedown7 = new Action(chkNewChar_Male);
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkMale", 29L, 103L, 55L, 15L, 0L, "Male", Core.Font.Arial, Alignment.Center, true, 255L, (long)UiDesign.CheckboxNormal, 0L, false, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown7, ref argcallback_mousemove7, ref argcallback_dblclick7);
            var argcallback_mousedown8 = new Action(chkNewChar_Female);
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkFemale", 90L, 103L, 62L, 15L, 0L, "Female", Core.Font.Arial, Alignment.Center, true, 255L, (long)UiDesign.CheckboxNormal, 0L, false, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown8, ref argcallback_mousemove8, ref argcallback_dblclick8);

            // Buttons
            var argcallback_mousedown9 = new Action(btnNewChar_Accept);
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnAccept", 29L, 127L, 60L, 24L, "Accept", Core.Font.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown9, ref argcallback_mousemove9, ref argcallback_dblclick9, 0L, 0L, "", false);
            var argcallback_mousedown10 = new Action(btnNewChar_Cancel);
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnCancel", 93L, 127L, 60L, 24L, "Cancel", Core.Font.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)UiDesign.Red, (long)UiDesign.RedHover, (long)UiDesign.RedClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown10, ref argcallback_mousemove10, ref argcallback_dblclick10, 0L, 0L, "", false);

            // Sprite
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_3", 175L, 42L, 76L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, "", ref argcallback_norm6, ref argcallback_hover6, ref argcallback_mousedown11, ref argcallback_mousemove11, ref argcallback_dblclick11, ref argonDraw3);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            UpdateLabel(Gui.Windows.Count, "lblSprite", 175L, 39L, 76L, 10L, "Sprite", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm7, ref argcallback_hover7, ref argcallback_mousedown12, ref argcallback_mousemove12, ref argcallback_dblclick12, ref enabled);

            // Scene
            var argonDraw4 = new Action(NewChar_OnDraw);
            Gui.UpdatePictureBox(Gui.Windows.Count, "picScene", 165L, 55L, 96L, 96L, true, false, 255L, true, 11L, 11L, 11L, 0L, 0L, 0L, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, ref argonDraw4);

            // Buttons
            var argcallback_mousedown13 = new Action(btnNewChar_Left);
            Action argcallback_mousemove13 = null;
            Action argcallback_dblclick13 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnLeft", 163L, 40L, 10L, 13L, "", Core.Font.Georgia, 0L, 12L, 14L, 16L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown13, ref argcallback_mousemove13, ref argcallback_dblclick13, 0L, 0L, "", false);
            var argcallback_mousedown14 = new Action(btnNewChar_Right);
            Action argcallback_mousemove14 = null;
            Action argcallback_dblclick14 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnRight", 252L, 40L, 10L, 13L, "", Core.Font.Georgia, 0L, 13L, 15L, 17L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown14, ref argcallback_mousemove14, ref argcallback_dblclick14, 0L, 0L, "", false);

            // Set the active control
            SetActiveControl(GetWindowIndex("winNewChar"), GetControlIndex("winNewChar", "txtName"));
        }

        public static void UpdateWindow_Chars()
        {
            // Control the window
            UpdateWindow("winChars", "Characters", Core.Font.Georgia, zOrder_Win, 0L, 0L, 364L, 229L, 62L, false, 3L, 5L, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnCharacters_Close);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Font.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_mousedown, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            Action argcallback_hover = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 26L, 352L, 197L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.Parchment, (long)UiDesign.Parchment, (long)UiDesign.Parchment, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown1, ref argcallback_mousemove1, ref argcallback_dblclick1, ref argonDraw);

            // Names
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            bool enabled = false;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_1", 22L, 40L, 98L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, "", ref argcallback_norm1, ref argcallback_hover1, ref argcallback_mousedown2, ref argcallback_mousemove2, ref argcallback_dblclick2, ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            UpdateLabel(Gui.Windows.Count, "lblCharName_1", 22L, 37L, 98L, 10L, "Blank Slot", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm2, ref argcallback_hover2, ref argcallback_mousedown3, ref argcallback_mousemove3, ref argcallback_dblclick3, ref enabled);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_2", 132L, 40L, 98L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, "", ref argcallback_norm3, ref argcallback_hover3, ref argcallback_mousedown4, ref argcallback_mousemove4, ref argcallback_dblclick4, ref argonDraw2);
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            UpdateLabel(Gui.Windows.Count, "lblCharName_2", 132L, 37L, 98L, 10L, "Blank Slot", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm4, ref argcallback_hover4, ref argcallback_mousedown5, ref argcallback_mousemove5, ref argcallback_dblclick5, ref enabled);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow_3", 242L, 40L, 98L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, "", ref argcallback_norm5, ref argcallback_hover5, ref argcallback_mousedown6, ref argcallback_mousemove6, ref argcallback_dblclick6, ref argonDraw3);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            UpdateLabel(Gui.Windows.Count, "lblCharName_3", 242L, 37L, 98L, 10L, "Blank Slot", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm6, ref argcallback_hover6, ref argcallback_mousedown7, ref argcallback_mousemove7, ref argcallback_dblclick7, ref enabled);

            // Scenery Boxes
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Action argonDraw4 = null;
            UpdatePictureBox(Gui.Windows.Count, "picScene_1", 23L, 55L, 96L, 96L, true, false, 255L, true, 11L, 11L, 11L, 0L, 0L, 0L, "", ref argcallback_norm7, ref argcallback_hover7, ref argcallback_mousedown8, ref argcallback_mousemove8, ref argcallback_dblclick8, ref argonDraw4);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Action argonDraw5 = null;
            UpdatePictureBox(Gui.Windows.Count, "picScene_2", 133L, 55L, 96L, 96L, true, false, 255L, true, 11L, 11L, 11L, 0L, 0L, 0L, "", ref argcallback_norm8, ref argcallback_hover8, ref argcallback_mousedown9, ref argcallback_mousemove9, ref argcallback_dblclick9, ref argonDraw5);
            var argonDraw6 = new Action(Chars_OnDraw);
            Gui.UpdatePictureBox(Gui.Windows.Count, "picScene_3", 243L, 55L, 96L, 96L, true, false, 255L, true, 11L, 11L, 11L, 0L, 0L, 0L, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown1, ref argcallback_mousemove1, ref argcallback_dblclick1, ref argonDraw6);

            // Control Buttons
            var argcallback_mousedown10 = new Action(btnAcceptChar_1);
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnSelectChar_1", 22L, 155L, 98L, 24L, "Select", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown10, ref argcallback_mousemove10, ref argcallback_dblclick10, 0L, 0L, "", false);
            var argcallback_mousedown11 = new Action(btnCreateChar_1);
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnCreateChar_1", 22L, 155L, 98L, 24L, "Create", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown11, ref argcallback_mousemove11, ref argcallback_dblclick11, 0L, 0L, "", false);
            var argcallback_mousedown12 = new Action(btnDelChar_1);
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnDelChar_1", 22L, 183L, 98L, 24L, "Delete", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Red, (long)UiDesign.RedHover, (long)UiDesign.RedClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown12, ref argcallback_mousemove12, ref argcallback_dblclick12, 0L, 0L, "", false);
            var argcallback_mousedown13 = new Action(btnAcceptChar_2);
            Action argcallback_mousemove13 = null;
            Action argcallback_dblclick13 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnSelectChar_2", 132L, 155L, 98L, 24L, "Select", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown13, ref argcallback_mousemove13, ref argcallback_dblclick13, 0L, 0L, "", false);
            var argcallback_mousedown14 = new Action(btnCreateChar_2);
            Action argcallback_mousemove14 = null;
            Action argcallback_dblclick14 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnCreateChar_2", 132L, 155L, 98L, 24L, "Create", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown14, ref argcallback_mousemove14, ref argcallback_dblclick14, 0L, 0L, "", false);
            var argcallback_mousedown15 = new Action(btnDelChar_2);
            Action argcallback_mousemove15 = null;
            Action argcallback_dblclick15 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnDelChar_2", 132L, 183L, 98L, 24L, "Delete", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Red, (long)UiDesign.RedHover, (long)UiDesign.RedClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown15, ref argcallback_mousemove15, ref argcallback_dblclick15, 0L, 0L, "", false);
            var argcallback_mousedown16 = new Action(btnAcceptChar_3);
            Action argcallback_mousemove16 = null;
            Action argcallback_dblclick16 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnSelectChar_3", 242L, 155L, 98L, 24L, "Select", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown16, ref argcallback_mousemove16, ref argcallback_dblclick16, 0L, 0L, "", false);
            var argcallback_mousedown17 = new Action(btnCreateChar_3);
            Action argcallback_mousemove17 = null;
            Action argcallback_dblclick17 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnCreateChar_3", 242L, 155L, 98L, 24L, "Create", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown17, ref argcallback_mousemove17, ref argcallback_dblclick17, 0L, 0L, "", false);
            var argcallback_mousedown18 = new Action(btnDelChar_3);
            Action argcallback_mousemove18 = null;
            Action argcallback_dblclick18 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnDelChar_3", 242L, 183L, 98L, 24L, "Delete", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Red, (long)UiDesign.RedHover, (long)UiDesign.RedClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown18, ref argcallback_mousemove18, ref argcallback_dblclick18, 0L, 0L, "", false);
        }

        public static void UpdateWindow_Jobs()
        {
            // Control window
            UpdateWindow("winJobs", "Select Job", Core.Font.Georgia, zOrder_Win, 0L, 0L, 364L, 229L, 17L, false, 2L, 6L, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnJobs_Close);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Font.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            var argonDraw = new Action(Jobs_DrawFace);
            Gui.UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 26L, 352L, 197L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.Parchment, (long)UiDesign.Parchment, (long)UiDesign.Parchment, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, ref argonDraw);

            // Job Name
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow", 183L, 42L, 98L, 9L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, (long)UiDesign.BlackOval, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown1, ref argcallback_mousemove1, ref argcallback_dblclick1, ref argonDraw1);
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblJobName", 183L, 39L, 98L, 10L, "Warrior", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, true, 255L, false, false, ref argcallback_norm1, ref argcallback_hover1, ref argcallback_mousedown2, ref argcallback_mousemove2, ref argcallback_dblclick2, ref enabled);

            // Select Buttons
            var argcallback_mousedown3 = new Action(btnJobs_Left);
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnLeft", 170L, 40L, 10L, 13L, "", Core.Font.Georgia, 0L, 12L, 14L, 16L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown3, ref argcallback_mousemove3, ref argcallback_dblclick3, 0L, 0L, "", false);

            var argcallback_mousedown4 = new Action(btnJobs_Right);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnRight", 282L, 40L, 10L, 13L, "", Core.Font.Georgia, 0L, 13L, 15L, 17L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown4, ref argcallback_mousemove4, ref argcallback_dblclick4, 0L, 0L, "", false);

            // Accept Button
            var argcallback_mousedown5 = new Action(btnJobs_Accept);
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnAccept", 183L, 185L, 98L, 22L, "Accept", Core.Font.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown5, ref argcallback_mousemove5, ref argcallback_dblclick5, 0L, 0L, "", false);

            // Text background
            Action argcallback_hover2 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBackground", 127L, 55L, 210L, 124L, true, false, 255L, true, 0L, 0L, 0L, (long)UiDesign.TextBlack, (long)UiDesign.TextBlack, (long)UiDesign.TextBlack, "", ref argcallback_norm, ref argcallback_hover2, ref argcallback_mousedown6, ref argcallback_mousemove6, ref argcallback_dblclick6, ref argonDraw2);

            // Overlay
            var argonDraw3 = new Action(Jobs_DrawText);
            Gui.UpdatePictureBox(Gui.Windows.Count, "picOverlay", 6L, 26L, 0L, 0L, true, false, 255L, true, 0L, 0L, 0L, 0L, 0L, 0L, "", ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, ref argonDraw3);
        }

        public static void UpdateWindow_Dialogue()
        {
            // Control dialogue window
            UpdateWindow("winDialogue", "Warning", Core.Font.Georgia, zOrder_Win, 0L, 0L, 348L, 145L, 38L, false, 3L, 5L, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal, (long)UiDesign.WindowNormal, canDrag: false);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnDialogue_Close);
            Action argcallback_norm = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Font.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 26L, 335L, 113L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // Header
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow", 103L, 44L, 144L, 9L, design_norm: (long)UiDesign.BlackOval, design_hover: (long)UiDesign.BlackOval, design_mousedown: (long)UiDesign.BlackOval, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblHeader", 103L, 40L, 144L, 10L, "Header", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);

            // Input
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_enter = null;
            UpdateTextbox(Gui.Windows.Count, "txtInput", 93L, 75L, 162L, 18L, font: Core.Font.Arial, align: Alignment.Center, xOffset: 5L, yOffset: 2L, design_norm: (long)UiDesign.TextBlack, design_hover: (long)UiDesign.TextBlack, design_mousedown: (long)UiDesign.TextBlack, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, callback_enter: ref argcallback_enter);

            // Labels
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            UpdateLabel(Gui.Windows.Count, "lblBody_1", 15L, 60L, 314L, 10L, "Invalid username or password.", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, enabled: ref enabled);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            UpdateLabel(Gui.Windows.Count, "lblBody_2", 15L, 75L, 314L, 10L, "Please try again!", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, enabled: ref enabled);

            // Buttons
            var argcallback_mousedown7 = new Action(Dialogue_Yes);
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnYes", 104L, 98L, 68L, 24L, "Yes", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown7, ref argcallback_dblclick7, ref argcallback_mousemove7, 0L, 0L, "", false);
            var argcallback_mousedown8 = new Action(Dialogue_No);
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnNo", 180L, 98L, 68L, 24L, "No", Core.Font.Arial, 0L, 0L, 0L, 0L, false, 255L, (long)UiDesign.Red, (long)UiDesign.RedHover, (long)UiDesign.RedClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown8, ref argcallback_mousemove8, ref argcallback_dblclick8, 0L, 0L, "", false);
            var argcallback_mousedown9 = new Action(Dialogue_Okay);
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnOkay", 140L, 98L, 68L, 24L, "Okay", Core.Font.Arial, 0L, 0L, 0L, 0L, true, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown9, ref argcallback_mousemove9, ref argcallback_dblclick9, 0L, 0L, "", false);

            // Set active control
            SetActiveControl(Gui.Windows.Count, GetControlIndex("winDialogue", "txtInput"));
        }

        public static void UpdateWindow_Party()
        {
            // Control window
            UpdateWindow("winParty", "", Core.Font.Georgia, zOrder_Win, 4L, 78L, 252L, 158L, 0L, false, design_norm: (long)UiDesign.WindowParty, design_hover: (long)UiDesign.WindowParty, design_mousedown: (long)UiDesign.WindowParty, canDrag: false);

            // Name labels
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblName1", 60L, 20L, 173L, 10L, "Richard - Level 10", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, enabled: ref enabled);
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            UpdateLabel(Gui.Windows.Count, "lblName2", 60L, 60L, 173L, 10L, "Anna - Level 18", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, enabled: ref enabled);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            UpdateLabel(Gui.Windows.Count, "lblName3", 60L, 100L, 173L, 10L, "Doleo - Level 25", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, enabled: ref enabled);

            // Empty Bars - HP
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picEmptyBar_HP1", 58L, 34L, 173L, 9L, image_norm: 62L, image_hover: 62L, image_mousedown: 62L, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw);
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picEmptyBar_HP2", 58L, 74L, 173L, 9L, image_norm: 62L, image_hover: 62L, image_mousedown: 62L, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw1);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picEmptyBar_HP3", 58L, 114L, 173L, 9L, image_norm: 62L, image_hover: 62L, image_mousedown: 62L, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, onDraw: ref argonDraw2);

            // Empty Bars - SP
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Gui.Windows.Count, "picEmptyBar_SP1", 58L, 44L, 173L, 9L, image_norm: 63L, image_hover: 63L, image_mousedown: 63L, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, onDraw: ref argonDraw3);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argonDraw4 = null;
            UpdatePictureBox(Gui.Windows.Count, "picEmptyBar_SP2", 58L, 84L, 173L, 9L, image_norm: 63L, image_hover: 63L, image_mousedown: 63L, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, onDraw: ref argonDraw4);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Action argonDraw5 = null;
            UpdatePictureBox(Gui.Windows.Count, "picEmptyBar_SP3", 58L, 124L, 173L, 9L, image_norm: 63L, image_hover: 63L, image_mousedown: 63L, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, onDraw: ref argonDraw5);

            // Filled bars - HP
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Action argonDraw6 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBar_HP1", 58L, 34L, 173L, 9L, image_norm: 64L, image_hover: 64L, image_mousedown: 64L, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, onDraw: ref argonDraw6);
            Action argcallback_norm10 = null;
            Action argcallback_hover10 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            Action argonDraw7 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBar_HP2", 58L, 74L, 173L, 9L, image_norm: 64L, image_hover: 64L, image_mousedown: 64L, callback_norm: ref argcallback_norm10, callback_hover: ref argcallback_hover10, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, onDraw: ref argonDraw7);
            Action argcallback_norm11 = null;
            Action argcallback_hover11 = null;
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            Action argonDraw8 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBar_HP3", 58L, 114L, 173L, 9L, image_norm: 64L, image_hover: 64L, image_mousedown: 64L, callback_norm: ref argcallback_norm11, callback_hover: ref argcallback_hover11, callback_mousedown: ref argcallback_mousedown11, callback_mousemove: ref argcallback_mousemove11, callback_dblclick: ref argcallback_dblclick11, onDraw: ref argonDraw8);

            // Filled bars - SP
            Action argcallback_norm12 = null;
            Action argcallback_hover12 = null;
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            Action argonDraw9 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBar_SP1", 58L, 44L, 173L, 9L, image_norm: 65L, image_hover: 65L, image_mousedown: 65L, callback_norm: ref argcallback_norm12, callback_hover: ref argcallback_hover12, callback_mousedown: ref argcallback_mousedown12, callback_mousemove: ref argcallback_mousemove12, callback_dblclick: ref argcallback_dblclick12, onDraw: ref argonDraw9);
            Action argcallback_norm13 = null;
            Action argcallback_hover13 = null;
            Action argcallback_mousedown13 = null;
            Action argcallback_mousemove13 = null;
            Action argcallback_dblclick13 = null;
            Action argonDraw10 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBar_SP2", 58L, 84L, 173L, 9L, image_norm: 65L, image_hover: 65L, image_mousedown: 65L, callback_norm: ref argcallback_norm13, callback_hover: ref argcallback_hover13, callback_mousedown: ref argcallback_mousedown13, callback_mousemove: ref argcallback_mousemove13, callback_dblclick: ref argcallback_dblclick13, onDraw: ref argonDraw10);
            Action argcallback_norm14 = null;
            Action argcallback_hover14 = null;
            Action argcallback_mousedown14 = null;
            Action argcallback_mousemove14 = null;
            Action argcallback_dblclick14 = null;
            Action argonDraw11 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBar_SP3", 58L, 124L, 173L, 9L, image_norm: 65L, image_hover: 65L, image_mousedown: 65L, callback_norm: ref argcallback_norm14, callback_hover: ref argcallback_hover14, callback_mousedown: ref argcallback_mousedown14, callback_mousemove: ref argcallback_mousemove14, callback_dblclick: ref argcallback_dblclick14, onDraw: ref argonDraw11);

            // Shadows
            // UpdatePictureBox(Gui.Windows.Count, "picShadow1", 20, 24, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow
            // UpdatePictureBox Gui.Windows.Count, "picShadow2", 20, 64, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow
            // UpdatePictureBox Gui.Windows.Count, "picShadow3", 20, 104, 32, 32, , , , , Tex_Shadow, Tex_Shadow, Tex_Shadow

            // Characters
            Action argcallback_norm15 = null;
            Action argcallback_hover15 = null;
            Action argcallback_mousedown15 = null;
            Action argcallback_mousemove15 = null;
            Action argcallback_dblclick15 = null;
            Action argonDraw12 = null;
            UpdatePictureBox(Gui.Windows.Count, "picChar1", 20L, 20L, 32L, 32L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, texturePath: Core.Path.Characters, callback_norm: ref argcallback_norm15, callback_hover: ref argcallback_hover15, callback_mousedown: ref argcallback_mousedown15, callback_mousemove: ref argcallback_mousemove15, callback_dblclick: ref argcallback_dblclick15, onDraw: ref argonDraw12);
            Action argcallback_norm16 = null;
            Action argcallback_hover16 = null;
            Action argcallback_mousedown16 = null;
            Action argcallback_mousemove16 = null;
            Action argcallback_dblclick16 = null;
            Action argonDraw13 = null;
            UpdatePictureBox(Gui.Windows.Count, "picChar2", 20L, 60L, 32L, 32L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, texturePath: Core.Path.Characters, callback_norm: ref argcallback_norm16, callback_hover: ref argcallback_hover16, callback_mousedown: ref argcallback_mousedown16, callback_mousemove: ref argcallback_mousemove16, callback_dblclick: ref argcallback_dblclick16, onDraw: ref argonDraw13);
            Action argcallback_norm17 = null;
            Action argcallback_hover17 = null;
            Action argcallback_mousedown17 = null;
            Action argcallback_mousemove17 = null;
            Action argcallback_dblclick17 = null;
            Action argonDraw14 = null;
            UpdatePictureBox(Gui.Windows.Count, "picChar3", 20L, 100L, 32L, 32L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, texturePath: Core.Path.Characters, callback_norm: ref argcallback_norm17, callback_hover: ref argcallback_hover17, callback_mousedown: ref argcallback_mousedown17, callback_mousemove: ref argcallback_mousemove17, callback_dblclick: ref argcallback_dblclick17, onDraw: ref argonDraw14);
        }

        public static void UpdateWindow_Trade()
        {
            // Control window
            UpdateWindow("winTrade", "Trading with [Name]", Core.Font.Georgia, zOrder_Win, 0L, 0L, 412L, 386L, 112L, false, 2L, 5L, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, onDraw: new Action(DrawTrade));

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Close Button
            var argcallback_mousedown = new Action(btnTrade_Close);
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 36L, 36L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 10L, 312L, 392L, 66L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // Labels
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            bool enabled = false;
            UpdatePictureBox(Gui.Windows.Count, "picShadow", 36L, 30L, 142L, 9L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw1);
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            UpdateLabel(Gui.Windows.Count, "lblYourTrade", 36L, 27L, 142L, 9L, "Robin's Offer", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow", 36 + 200, 30L, 142L, 9L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw2);
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            UpdateLabel(Gui.Windows.Count, "lblTheirTrade", 36 + 200, 27L, 142L, 9L, "Richard's Offer", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, enabled: ref enabled);

            // Buttons
            var argcallback_mousedown6 = new Action(btnTrade_Accept);
            Gui.UpdateButton(Gui.Windows.Count, "btnAccept", 134L, 340L, 68L, 24L, "Accept", Core.Font.Georgia, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown6, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            var argcallback_mousedown7 = new Action(btnTrade_Close);
            Gui.UpdateButton(Gui.Windows.Count, "btnDecline", 210L, 340L, 68L, 24L, "Decline", Core.Font.Georgia, design_norm: (long)UiDesign.Red, design_hover: (long)UiDesign.RedHover, design_mousedown: (long)UiDesign.RedClick, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown7, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Labels
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            UpdateLabel(Gui.Windows.Count, "lblStatus", 114L, 322L, 184L, 10L, "", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, enabled: ref enabled);

            // Amounts
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            UpdateLabel(Gui.Windows.Count, "lblBlank", 25L, 330L, 100L, 10L, "Total Value", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, enabled: ref enabled);
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            UpdateLabel(Gui.Windows.Count, "lblBlank", 285L, 330L, 100L, 10L, "Total Value", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, enabled: ref enabled);
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            UpdateLabel(Gui.Windows.Count, "lblYourValue", 25L, 344L, 100L, 10L, "52,812g", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown11, callback_mousemove: ref argcallback_mousemove11, callback_dblclick: ref argcallback_dblclick11, enabled: ref enabled);
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            UpdateLabel(Gui.Windows.Count, "lblTheirValue", 285L, 344L, 100L, 10L, "12,531g", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown12, callback_mousemove: ref argcallback_mousemove12, callback_dblclick: ref argcallback_dblclick12, enabled: ref enabled);

            // Item Containers
            var argcallback_mousedown13 = new Action(TradeMouseMove_Your);
            var argcallback_mousemove13 = new Action(TradeMouseMove_Your);
            var argcallback_dblclick13 = new Action(TradeDoubleClick_Your);
            var argonDraw3 = new Action(DrawYourTrade);
            Gui.UpdatePictureBox(Gui.Windows.Count, "picYour", 14L, 46L, 184L, 260L, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown13, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove13, callback_dblclick: ref argcallback_dblclick13, onDraw: ref argonDraw3);
            var argcallback_mousedown14 = new Action(TradeMouseMove_Their);
            var argcallback_mousemove14 = new Action(TradeMouseMove_Their);
            var argcallback_dblclick14 = new Action(TradeMouseMove_Their);
            var argonDraw4 = new Action(DrawTheirTrade);
            Gui.UpdatePictureBox(Gui.Windows.Count, "picTheir", 214L, 46L, 184L, 260L, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown14, callback_hover: ref argcallback_hover, callback_mousemove: ref argcallback_mousemove14, callback_dblclick: ref argcallback_dblclick14, onDraw: ref argonDraw4);
        }

        public static void UpdateWindow_EscMenu()
        {
            // Control window
            UpdateWindow("winEscMenu", "", Core.Font.Georgia, zOrder_Win, 0L, 0L, 210L, 156L, 0L, false, design_norm: (long)UiDesign.WindowNoBar, design_hover: (long)UiDesign.WindowNoBar, design_mousedown: (long)UiDesign.WindowNoBar, canDrag: false, clickThrough: false);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Parchment
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 6L, 198L, 144L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);

            // Buttons
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = new Action(btnEscMenu_Return);
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnReturn", 16L, 16L, 178L, 28L, "Return to Game", Core.Font.Georgia, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1);

            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = new Action(btnEscMenu_Options);
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnOptions", 16L, 48L, 178L, 28L, "Options", Core.Font.Georgia, design_norm: (long)UiDesign.Orange, design_hover: (long)UiDesign.OrangeHover, design_mousedown: (long)UiDesign.OrangeClick, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2);

            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = new Action(btnEscMenu_MainMenu);
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnMainMenu", 16L, 80L, 178L, 28L, "Back to Main Menu", Core.Font.Georgia, design_norm: (long)UiDesign.Blue, design_hover: (long)UiDesign.BlueHover, design_mousedown: (long)UiDesign.BlueClick, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3);

            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = new Action(btnEscMenu_Exit);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnExit", 16L, 112L, 178L, 28L, "Exit the Game", Core.Font.Georgia, design_norm: (long)UiDesign.Red, design_hover: (long)UiDesign.RedHover, design_mousedown: (long)UiDesign.RedClick, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
        }

        public static void UpdateWindow_Bars()
        {
            // Control window
            UpdateWindow("winBars", "", Core.Font.Georgia, zOrder_Win, 10L, 10L, 239L, 77L, 0L, false, design_norm: (long)UiDesign.WindowNoBar, design_hover: (long)UiDesign.WindowNoBar, design_mousedown: (long)UiDesign.WindowNoBar, canDrag: false, clickThrough: true);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Parchment
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 6L, 227L, 65L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);

            // Blank Bars
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picHP_Blank", 15L, 15L, 209L, 13L, image_norm: 24L, image_hover: 24L, image_mousedown: 24L, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picSP_Blank", 15L, 32L, 209L, 13L, image_norm: 25L, image_hover: 25L, image_mousedown: 25L, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw2);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Gui.Windows.Count, "picEXP_Blank", 15L, 49L, 209L, 13L, image_norm: 26L, image_hover: 26L, image_mousedown: 26L, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw3);

            // Bars
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            var argonDraw4 = new Action(Bars_OnDraw);
            Gui.UpdatePictureBox(Gui.Windows.Count, "picBlank", 0L, 0L, 0L, 0L, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw4);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argonDraw5 = null;
            UpdatePictureBox(Gui.Windows.Count, "picHealth", 16L, 10L, 44L, 14L, image_norm: 21L, image_hover: 21L, image_mousedown: 21L, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, onDraw: ref argonDraw5);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw6 = null;
            UpdatePictureBox(Gui.Windows.Count, "picSpirit", 16L, 28L, 44L, 14L, image_norm: 22L, image_hover: 22L, image_mousedown: 22L, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, onDraw: ref argonDraw6);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argonDraw7 = null;
            UpdatePictureBox(Gui.Windows.Count, "picExperience", 16L, 45L, 74L, 14L, image_norm: 23L, image_hover: 23L, image_mousedown: 23L, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, onDraw: ref argonDraw7);

            // Labels
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblHP", 15L, 14L, 209L, 10L, "999/999", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, enabled: ref enabled);
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            UpdateLabel(Gui.Windows.Count, "lblMP", 15L, 30L, 209L, 10L, "999/999", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, enabled: ref enabled);
            Action argcallback_norm10 = null;
            Action argcallback_hover10 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            UpdateLabel(Gui.Windows.Count, "lblEXP", 15L, 48L, 209L, 10L, "999/999", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm10, callback_hover: ref argcallback_hover10, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, enabled: ref enabled);
        }

        public static void UpdateWindow_Chat()
        {
            // Control window
            UpdateWindow("winChat", "", Core.Font.Georgia, zOrder_Win, 8L, GameState.ResolutionHeight - 178, 352L, 152L, 0L, false, canDrag: false);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Channel boxes
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = new Action(CheckboxChat_Game);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkGame", 10L, 2L, 49L, 23L, 0L, "Game", Core.Font.Arial, theDesign: (long)UiDesign.CheckboxChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(CheckboxChat_Map);
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkMap", 60L, 2L, 49L, 23L, 0L, "Map", Core.Font.Arial, theDesign: (long)UiDesign.CheckboxChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(CheckboxChat_Global);
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkGlobal", 110L, 2L, 49L, 23L, 0L, "Global", Core.Font.Arial, theDesign: (long)UiDesign.CheckboxChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(CheckboxChat_Party);
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkParty", 160L, 2L, 49L, 23L, 0L, "Party", Core.Font.Arial, theDesign: (long)UiDesign.CheckboxChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(CheckboxChat_Guild);
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkGuild", 210L, 2L, 49L, 23L, 0L, "Guild", Core.Font.Arial, theDesign: (long)UiDesign.CheckboxChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_mousedown = new Action(CheckboxChat_Player);
            Gui.UpdateCheckBox(Gui.Windows.Count, "chkPlayer", 260L, 2L, 49L, 23L, 0L, "Player", Core.Font.Arial, theDesign: (long)UiDesign.CheckboxChat, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Blank picturebox
            var argonDraw = new Action(Chat_OnDraw);
            Action argcallback_norm_pic = null;
            Action argcallback_hover_pic = null;
            Action argcallback_mousedown_pic = null;
            Action argcallback_mousemove_pic = null;
            Action argcallback_dblclick_pic = null;
            Gui.UpdatePictureBox(Gui.Windows.Count, "picNull", 0L, 0L, 0L, 0L, onDraw: ref argonDraw, callback_norm: ref argcallback_norm_pic, callback_hover: ref argcallback_hover_pic, callback_mousedown: ref argcallback_mousedown_pic, callback_mousemove: ref argcallback_mousemove_pic, callback_dblclick: ref argcallback_dblclick_pic);

            // Chat button
            argcallback_norm = new Action(btnSay_Click);
            Gui.UpdateButton(Gui.Windows.Count, "btnChat", 296L, (long)(124 + 16), 48L, 20L, "Say", Core.Font.Arial, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Chat Textbox
            Action argcallback_enter = null;
            UpdateTextbox(Gui.Windows.Count, "txtChat", 12L, 127 + 16, 352L, 25L, font: Core.Font.Georgia, visible: false, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, callback_enter: ref argcallback_enter);

            // Buttons
            argcallback_norm = new Action(btnChat_Up);
            Gui.UpdateButton(Gui.Windows.Count, "btnUp", 328L, 28L, 10L, 13L, image_norm: 4L, image_hover: 52L, image_mousedown: 4L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
            argcallback_norm = new Action(btnChat_Down);
            Gui.UpdateButton(Gui.Windows.Count, "btnDown", 327L, 122L, 10L, 13L, image_norm: 5L, image_hover: 53L, image_mousedown: 5L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Custom Handlers for mouse up
            Gui.Windows[Gui.Windows.Count].Controls[GetControlIndex("winChat", "btnUp")].CallBack[(int)ControlState.MouseUp] = new Action(btnChat_Up_MouseUp);
            Gui.Windows[Gui.Windows.Count].Controls[GetControlIndex("winChat", "btnDown")].CallBack[(int)ControlState.MouseUp] = new Action(btnChat_Down_MouseUp);

            // Set the active control
            SetActiveControl(GetWindowIndex("winChat"), GetControlIndex("winChat", "txtChat"));

            // sort out the tabs
            {
                var withBlock = Gui.Windows[GetWindowIndex("winChat")];
                withBlock.Controls[GetControlIndex("winChat", "chkGame")].Value = SettingsManager.Instance.ChannelState[(int)ChatChannel.Game];
                withBlock.Controls[GetControlIndex("winChat", "chkMap")].Value = SettingsManager.Instance.ChannelState[(int)ChatChannel.Map];
                withBlock.Controls[GetControlIndex("winChat", "chkGlobal")].Value = SettingsManager.Instance.ChannelState[(int)ChatChannel.Broadcast];
                withBlock.Controls[GetControlIndex("winChat", "chkParty")].Value = SettingsManager.Instance.ChannelState[(int)ChatChannel.Party];
                withBlock.Controls[GetControlIndex("winChat", "chkGuild")].Value = SettingsManager.Instance.ChannelState[(int)ChatChannel.Guild];
                withBlock.Controls[GetControlIndex("winChat", "chkPlayer")].Value = SettingsManager.Instance.ChannelState[(int)ChatChannel.Private];
            }
        }

        public static void UpdateWindow_ChatSmall()
        {
            // Control window
            UpdateWindow("winChatSmall", "", Core.Font.Georgia, zOrder_Win, 8L, 0L, 0L, 0L, 0L, false, onDraw: new Action(ChatSmall_OnDraw), canDrag: false, clickThrough: true);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Chat Label
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblMsg", 12L, 140L, 286L, 25L, "Press 'Enter' to open chat", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, enabled: ref enabled);
        }

        public static void UpdateWindow_Hotbar()
        {
            // Control window
            UpdateWindow("winHotbar", "", Core.Font.Georgia, zOrder_Win, 432L, 10L, 418L, 36L, 0L, false, callback_mousemove: new Action(Hotbar_MouseMove), callback_mousedown: new Action(Hotbar_MouseDown), callback_dblclick: new Action(Hotbar_DoubleClick), onDraw: new Action(DrawHotbar), canDrag: false, zChange: Conversions.ToByte(false));
        }

        public static void UpdateWindow_Menu()
        {
            // Control window
            UpdateWindow("winMenu", "", Core.Font.Georgia, zOrder_Win, GameState.ResolutionWidth - 229, GameState.ResolutionHeight - 31, 229L, 30L, 0L, false, isActive: false, canDrag: false, clickThrough: true);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Wood part
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picWood", 0L, 5L, 228L, 20L, design_norm: (long)UiDesign.Wood, design_hover: (long)UiDesign.Wood, design_mousedown: (long)UiDesign.Wood, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);
            // Buttons
            var argcallback_mousedown1 = new Action(btnMenu_Char);
            Action callback_norm = null;
            Action callback_hover = null;
            Action callback_mousemove = null;
            Action callback_dblclick = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnChar", 8L, 0L, 29L, 29L, text: "", font: Core.Font.Georgia, icon: 108L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2, tooltip: "Character (C)");
            var argcallback_mousedown2 = new Action(btnMenu_Inv);
            Gui.UpdateButton(Gui.Windows.Count, "btnInv", 44L, 0L, 29L, 29L, text: "", font: Core.Font.Georgia, icon: 1L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2, tooltip: "Inventory (I)");
            var argcallback_mousedown3 = new Action(btnMenu_Skills);
            Gui.UpdateButton(Gui.Windows.Count, "btnSkills", 82L, 0L, 29L, 29L, text: "", font: Core.Font.Georgia, icon: 109L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2, tooltip: "Skills (K)");
            var argcallback_mousedown4 = new Action(btnMenu_Map);
            Gui.UpdateButton(Gui.Windows.Count, "btnMap", 119L, 0L, 29L, 29L, text: "", font: Core.Font.Georgia, icon: 106L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)UiDesign.Grey, design_hover: (long)UiDesign.Grey, design_mousedown: (long)UiDesign.Grey, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2);
            var argcallback_mousedown5 = new Action(btnMenu_Guild);
            Gui.UpdateButton(Gui.Windows.Count, "btnGuild", 155L, 0L, 29L, 29L, text: "", font: Core.Font.Georgia, icon: 107L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)UiDesign.Grey, design_hover: (long)UiDesign.Grey, design_mousedown: (long)UiDesign.Grey, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-1);
            var argcallback_mousedown6 = new Action(btnMenu_Quest);
            Gui.UpdateButton(Gui.Windows.Count, "btnQuest", 190L, 0L, 29L, 29L, text: "", font: Core.Font.Georgia, icon: 23L, image_norm: 0L, image_hover: 0L, image_mousedown: 0L, visible: true, alpha: 255L, design_norm: (long)UiDesign.Grey, design_hover: (long)UiDesign.Grey, design_mousedown: (long)UiDesign.Grey, callback_norm: ref callback_norm, callback_hover: ref callback_hover, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref callback_mousemove, callback_dblclick: ref callback_dblclick, xOffset: (long)-1, yOffset: (long)-2);
        }

        public static void UpdateWindow_Inventory()
        {
            // Control window
            UpdateWindow("winInventory", "Inventory", Core.Font.Georgia, zOrder_Win, 0L, 0L, 202L, 319L, 1L, false, 2L, 7L, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, callback_mousemove: new Action(Inventory_MouseMove), callback_mousedown: new Action(Inventory_MouseDown), callback_dblclick: new Action(Inventory_DoubleClick), onDraw: new Action(DrawInventory));

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnMenu_Inv);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, "", Core.Font.Georgia, 0L, 8L, 9L, 10L, true, 255L, 0L, 0L, 0L, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 0L, 0L, "", false);

            // Gold amount
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlank", 8L, 293L, 186L, 18L, image_norm: 67L, image_hover: 67L, image_mousedown: 67L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            bool enabled = false;
            //UpdateLabel(Gui.Windows.Count, "lblGold", 42L, 296L, 100L, 10L, "g", Core.Font.Georgia, Microsoft.Xna.Framework.Color.Yellow, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, enabled: ref enabled);

            // Drop
            //Gui.UpdateButton(Gui.Windows.Count, "btnDrop", 155L, 294L, 38L, 16L, "Drop", Core.Font.Georgia, 0L, 0L, 0L, 0L, true, 255L, (long)UiDesign.Green, (long)UiDesign.GreenHover, (long)UiDesign.GreenClick, ref argcallback_norm, ref argcallback_hover, ref argcallback_mousedown, ref argcallback_mousemove, ref argcallback_dblclick, 5L, 3L, "", false, true);
        }

        public static void UpdateWindow_Character()
        {
            // Control window
            UpdateWindow("winCharacter", "Character", Core.Font.Georgia, zOrder_Win, 0L, 0L, 174L, 356L, 62L, false, 2L, 6L, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, callback_mousemove: new Action(Character_MouseMove), callback_mousedown: new Action(Character_MouseMove), callback_dblclick: new Action(Character_DoubleClick), onDraw: new Action(DrawCharacter));

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            Action argcallback_norm = null;
            var argcallback_mousedown = new Action(btnMenu_Char);
            Action argcallback_hover = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Parchment
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 26L, 162L, 287L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // White boxes
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picWhiteBox", 13L, 34L, 148L, 19L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picWhiteBox", 13L, 54L, 148L, 19L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw2);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Gui.Windows.Count, "picWhiteBox", 13L, 74L, 148L, 19L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw3);
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argonDraw4 = null;
            UpdatePictureBox(Gui.Windows.Count, "picWhiteBox", 13L, 94L, 148L, 19L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5, onDraw: ref argonDraw4);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argonDraw5 = null;
            UpdatePictureBox(Gui.Windows.Count, "picWhiteBox", 13L, 114L, 148L, 19L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6, onDraw: ref argonDraw5);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argonDraw6 = null;
            UpdatePictureBox(Gui.Windows.Count, "picWhiteBox", 13L, 134L, 148L, 19L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, onDraw: ref argonDraw6);
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            Action argonDraw7 = null;
            UpdatePictureBox(Gui.Windows.Count, "picWhiteBox", 13L, 154L, 148L, 19L, design_norm: (long)UiDesign.TextWhite, design_hover: (long)UiDesign.TextWhite, design_mousedown: (long)UiDesign.TextWhite, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, onDraw: ref argonDraw7);

            // Labels
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblName", 18L, 36L, 147L, 10L, "Name", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, enabled: ref enabled);
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            UpdateLabel(Gui.Windows.Count, "lblJob", 18L, 56L, 147L, 10L, "Job", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, enabled: ref enabled);
            Action argcallback_norm10 = null;
            Action argcallback_hover10 = null;
            Action argcallback_mousedown11 = null;
            Action argcallback_mousemove11 = null;
            Action argcallback_dblclick11 = null;
            UpdateLabel(Gui.Windows.Count, "lblLevel", 18L, 76L, 147L, 10L, "Level", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm10, callback_hover: ref argcallback_hover10, callback_mousedown: ref argcallback_mousedown11, callback_mousemove: ref argcallback_mousemove11, callback_dblclick: ref argcallback_dblclick11, enabled: ref enabled);
            Action argcallback_norm11 = null;
            Action argcallback_hover11 = null;
            Action argcallback_mousedown12 = null;
            Action argcallback_mousemove12 = null;
            Action argcallback_dblclick12 = null;
            UpdateLabel(Gui.Windows.Count, "lblGuild", 18L, 96L, 147L, 10L, "Guild", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm11, callback_hover: ref argcallback_hover11, callback_mousedown: ref argcallback_mousedown12, callback_mousemove: ref argcallback_mousemove12, callback_dblclick: ref argcallback_dblclick12, enabled: ref enabled);
            Action argcallback_norm12 = null;
            Action argcallback_hover12 = null;
            Action argcallback_mousedown13 = null;
            Action argcallback_mousemove13 = null;
            Action argcallback_dblclick13 = null;
            UpdateLabel(Gui.Windows.Count, "lblHealth", 18L, 116L, 147L, 10L, "Health", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm12, callback_hover: ref argcallback_hover12, callback_mousedown: ref argcallback_mousedown13, callback_mousemove: ref argcallback_mousemove13, callback_dblclick: ref argcallback_dblclick13, enabled: ref enabled);
            Action argcallback_norm13 = null;
            Action argcallback_hover13 = null;
            Action argcallback_mousedown14 = null;
            Action argcallback_mousemove14 = null;
            Action argcallback_dblclick14 = null;
            UpdateLabel(Gui.Windows.Count, "lblSpirit", 18L, 136L, 147L, 10L, "Spirit", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm13, callback_hover: ref argcallback_hover13, callback_mousedown: ref argcallback_mousedown14, callback_mousemove: ref argcallback_mousemove14, callback_dblclick: ref argcallback_dblclick14, enabled: ref enabled);
            Action argcallback_norm14 = null;
            Action argcallback_hover14 = null;
            Action argcallback_mousedown15 = null;
            Action argcallback_mousemove15 = null;
            Action argcallback_dblclick15 = null;
            UpdateLabel(Gui.Windows.Count, "lblExperience", 18L, 156L, 147L, 10L, "Experience", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, callback_norm: ref argcallback_norm14, callback_hover: ref argcallback_hover14, callback_mousedown: ref argcallback_mousedown15, callback_mousemove: ref argcallback_mousemove15, callback_dblclick: ref argcallback_dblclick15, enabled: ref enabled);
            Action argcallback_norm15 = null;
            Action argcallback_hover15 = null;
            Action argcallback_mousedown16 = null;
            Action argcallback_mousemove16 = null;
            Action argcallback_dblclick16 = null;
            UpdateLabel(Gui.Windows.Count, "lblName2", 13L, 36L, 147L, 10L, "Name", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm15, callback_hover: ref argcallback_hover15, callback_mousedown: ref argcallback_mousedown16, callback_mousemove: ref argcallback_mousemove16, callback_dblclick: ref argcallback_dblclick16, enabled: ref enabled);
            Action argcallback_norm16 = null;
            Action argcallback_hover16 = null;
            Action argcallback_mousedown17 = null;
            Action argcallback_mousemove17 = null;
            Action argcallback_dblclick17 = null;
            UpdateLabel(Gui.Windows.Count, "lblJob2", 13L, 56L, 147L, 10L, "", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm16, callback_hover: ref argcallback_hover16, callback_mousedown: ref argcallback_mousedown17, callback_mousemove: ref argcallback_mousemove17, callback_dblclick: ref argcallback_dblclick17, enabled: ref enabled);
            Action argcallback_norm17 = null;
            Action argcallback_hover17 = null;
            Action argcallback_mousedown18 = null;
            Action argcallback_mousemove18 = null;
            Action argcallback_dblclick18 = null;
            UpdateLabel(Gui.Windows.Count, "lblLevel2", 13L, 76L, 147L, 10L, "Level", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm17, callback_hover: ref argcallback_hover17, callback_mousedown: ref argcallback_mousedown18, callback_mousemove: ref argcallback_mousemove18, callback_dblclick: ref argcallback_dblclick18, enabled: ref enabled);
            Action argcallback_norm18 = null;
            Action argcallback_hover18 = null;
            Action argcallback_mousedown19 = null;
            Action argcallback_mousemove19 = null;
            Action argcallback_dblclick19 = null;
            UpdateLabel(Gui.Windows.Count, "lblGuild2", 13L, 96L, 147L, 10L, "Guild", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm18, callback_hover: ref argcallback_hover18, callback_mousedown: ref argcallback_mousedown19, callback_mousemove: ref argcallback_mousemove19, callback_dblclick: ref argcallback_dblclick19, enabled: ref enabled);
            Action argcallback_norm19 = null;
            Action argcallback_hover19 = null;
            Action argcallback_mousedown20 = null;
            Action argcallback_mousemove20 = null;
            Action argcallback_dblclick20 = null;
            UpdateLabel(Gui.Windows.Count, "lblHealth2", 13L, 116L, 147L, 10L, "Health", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm19, callback_hover: ref argcallback_hover19, callback_mousedown: ref argcallback_mousedown20, callback_mousemove: ref argcallback_mousemove20, callback_dblclick: ref argcallback_dblclick20, enabled: ref enabled);
            Action argcallback_norm20 = null;
            Action argcallback_hover20 = null;
            Action argcallback_mousedown21 = null;
            Action argcallback_mousemove21 = null;
            Action argcallback_dblclick21 = null;
            UpdateLabel(Gui.Windows.Count, "lblSpirit2", 13L, 136L, 147L, 10L, "Spirit", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm20, callback_hover: ref argcallback_hover20, callback_mousedown: ref argcallback_mousedown21, callback_mousemove: ref argcallback_mousemove21, callback_dblclick: ref argcallback_dblclick21, enabled: ref enabled);
            Action argcallback_norm21 = null;
            Action argcallback_hover21 = null;
            Action argcallback_mousedown22 = null;
            Action argcallback_mousemove22 = null;
            Action argcallback_dblclick22 = null;
            UpdateLabel(Gui.Windows.Count, "lblExperience2", 13L, 156L, 147L, 10L, "Experience", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm21, callback_hover: ref argcallback_hover21, callback_mousedown: ref argcallback_mousedown22, callback_mousemove: ref argcallback_mousemove22, callback_dblclick: ref argcallback_dblclick22, enabled: ref enabled);

            // Attributes
            Action argcallback_norm22 = null;
            Action argcallback_hover22 = null;
            Action argcallback_mousedown23 = null;
            Action argcallback_mousemove23 = null;
            Action argcallback_dblclick23 = null;
            Action argonDraw8 = null;
            UpdatePictureBox(Gui.Windows.Count, "picShadow", 18L, 176L, 138L, 9L, design_norm: (long)UiDesign.BlackOval, design_hover: (long)UiDesign.BlackOval, design_mousedown: (long)UiDesign.BlackOval, callback_norm: ref argcallback_norm22, callback_hover: ref argcallback_hover22, callback_mousedown: ref argcallback_mousedown23, callback_mousemove: ref argcallback_mousemove23, callback_dblclick: ref argcallback_dblclick23, onDraw: ref argonDraw8);
            Action argcallback_norm23 = null;
            Action argcallback_hover23 = null;
            Action argcallback_mousedown24 = null;
            Action argcallback_mousemove24 = null;
            Action argcallback_dblclick24 = null;
            UpdateLabel(Gui.Windows.Count, "lblLabel", 18L, 173L, 138L, 10L, "Attributes", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm23, callback_hover: ref argcallback_hover23, callback_mousedown: ref argcallback_mousedown24, callback_mousemove: ref argcallback_mousemove24, callback_dblclick: ref argcallback_dblclick24, enabled: ref enabled);

            // Black boxes
            Action argcallback_norm24 = null;
            Action argcallback_hover24 = null;
            Action argcallback_mousedown25 = null;
            Action argcallback_mousemove25 = null;
            Action argcallback_dblclick25 = null;
            Action argonDraw9 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlackBox", 13L, 186L, 148L, 19L, design_norm: (long)UiDesign.TextBlack, design_hover: (long)UiDesign.TextBlack, design_mousedown: (long)UiDesign.TextBlack, callback_norm: ref argcallback_norm24, callback_hover: ref argcallback_hover24, callback_mousedown: ref argcallback_mousedown25, callback_mousemove: ref argcallback_mousemove25, callback_dblclick: ref argcallback_dblclick25, onDraw: ref argonDraw9);
            Action argcallback_norm25 = null;
            Action argcallback_hover25 = null;
            Action argcallback_mousedown26 = null;
            Action argcallback_mousemove26 = null;
            Action argcallback_dblclick26 = null;
            Action argonDraw10 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlackBox", 13L, 206L, 148L, 19L, design_norm: (long)UiDesign.TextBlack, design_hover: (long)UiDesign.TextBlack, design_mousedown: (long)UiDesign.TextBlack, callback_norm: ref argcallback_norm25, callback_hover: ref argcallback_hover25, callback_mousedown: ref argcallback_mousedown26, callback_mousemove: ref argcallback_mousemove26, callback_dblclick: ref argcallback_dblclick26, onDraw: ref argonDraw10);
            Action argcallback_norm26 = null;
            Action argcallback_hover26 = null;
            Action argcallback_mousedown27 = null;
            Action argcallback_mousemove27 = null;
            Action argcallback_dblclick27 = null;
            Action argonDraw11 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlackBox", 13L, 226L, 148L, 19L, design_norm: (long)UiDesign.TextBlack, design_hover: (long)UiDesign.TextBlack, design_mousedown: (long)UiDesign.TextBlack, callback_norm: ref argcallback_norm26, callback_hover: ref argcallback_hover26, callback_mousedown: ref argcallback_mousedown27, callback_mousemove: ref argcallback_mousemove27, callback_dblclick: ref argcallback_dblclick27, onDraw: ref argonDraw11);
            Action argcallback_norm27 = null;
            Action argcallback_hover27 = null;
            Action argcallback_mousedown28 = null;
            Action argcallback_mousemove28 = null;
            Action argcallback_dblclick28 = null;
            Action argonDraw12 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlackBox", 13L, 246L, 148L, 19L, design_norm: (long)UiDesign.TextBlack, design_hover: (long)UiDesign.TextBlack, design_mousedown: (long)UiDesign.TextBlack, callback_norm: ref argcallback_norm27, callback_hover: ref argcallback_hover27, callback_mousedown: ref argcallback_mousedown28, callback_mousemove: ref argcallback_mousemove28, callback_dblclick: ref argcallback_dblclick28, onDraw: ref argonDraw12);
            Action argcallback_norm28 = null;
            Action argcallback_hover28 = null;
            Action argcallback_mousedown29 = null;
            Action argcallback_mousemove29 = null;
            Action argcallback_dblclick29 = null;
            Action argonDraw13 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlackBox", 13L, 266L, 148L, 19L, design_norm: (long)UiDesign.TextBlack, design_hover: (long)UiDesign.TextBlack, design_mousedown: (long)UiDesign.TextBlack, callback_norm: ref argcallback_norm28, callback_hover: ref argcallback_hover28, callback_mousedown: ref argcallback_mousedown29, callback_mousemove: ref argcallback_mousemove29, callback_dblclick: ref argcallback_dblclick29, onDraw: ref argonDraw13);
            Action argcallback_norm29 = null;
            Action argcallback_hover29 = null;
            Action argcallback_mousedown30 = null;
            Action argcallback_mousemove30 = null;
            Action argcallback_dblclick30 = null;
            Action argonDraw14 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlackBox", 13L, 286L, 148L, 19L, design_norm: (long)UiDesign.TextBlack, design_hover: (long)UiDesign.TextBlack, design_mousedown: (long)UiDesign.TextBlack, callback_norm: ref argcallback_norm29, callback_hover: ref argcallback_hover29, callback_mousedown: ref argcallback_mousedown30, callback_mousemove: ref argcallback_mousemove30, callback_dblclick: ref argcallback_dblclick30, onDraw: ref argonDraw14);

            // Labels
            Action argcallback_norm30 = null;
            Action argcallback_hover30 = null;
            Action argcallback_mousedown31 = null;
            Action argcallback_mousemove31 = null;
            Action argcallback_dblclick31 = null;
            UpdateLabel(Gui.Windows.Count, "lblLabel", 18L, 188L, 138L, 10L, "Strength", Core.Font.Arial, Microsoft.Xna.Framework.Color.Yellow, callback_norm: ref argcallback_norm30, callback_hover: ref argcallback_hover30, callback_mousedown: ref argcallback_mousedown31, callback_mousemove: ref argcallback_mousemove31, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);
            Action argcallback_norm31 = null;
            Action argcallback_hover31 = null;
            Action argcallback_mousedown32 = null;
            Action argcallback_mousemove32 = null;
            Action argcallback_dblclick32 = null;
            UpdateLabel(Gui.Windows.Count, "lblLabel", 18L, 208L, 138L, 10L, "Vitality", Core.Font.Arial, Microsoft.Xna.Framework.Color.Yellow, callback_norm: ref argcallback_norm31, callback_hover: ref argcallback_hover31, callback_mousedown: ref argcallback_mousedown32, callback_mousemove: ref argcallback_mousemove32, callback_dblclick: ref argcallback_dblclick32, enabled: ref enabled);
            Action argcallback_norm32 = null;
            Action argcallback_hover32 = null;
            Action argcallback_mousedown33 = null;
            Action argcallback_mousemove33 = null;
            Action argcallback_dblclick33 = null;
            UpdateLabel(Gui.Windows.Count, "lblLabel", 18L, 228L, 138L, 10L, "Intelligence", Core.Font.Arial, Microsoft.Xna.Framework.Color.Yellow, callback_norm: ref argcallback_norm32, callback_hover: ref argcallback_hover32, callback_mousedown: ref argcallback_mousedown33, callback_mousemove: ref argcallback_mousemove33, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);
            Action argcallback_norm33 = null;
            Action argcallback_hover33 = null;
            Action argcallback_mousedown34 = null;
            Action argcallback_mousemove34 = null;
            Action argcallback_dblclick34 = null;
            UpdateLabel(Gui.Windows.Count, "lblLabel", 18L, 248L, 138L, 10L, "Luck", Core.Font.Arial, Microsoft.Xna.Framework.Color.Yellow, callback_norm: ref argcallback_norm33, callback_hover: ref argcallback_hover33, callback_mousedown: ref argcallback_mousedown34, callback_mousemove: ref argcallback_mousemove34, callback_dblclick: ref argcallback_dblclick34, enabled: ref enabled);
            Action argcallback_norm34 = null;
            Action argcallback_hover34 = null;
            Action argcallback_mousedown35 = null;
            Action argcallback_mousemove35 = null;
            Action argcallback_dblclick35 = null;
            UpdateLabel(Gui.Windows.Count, "lblLabel", 18L, 268L, 138L, 10L, "Spirit", Core.Font.Arial, Microsoft.Xna.Framework.Color.Yellow, callback_norm: ref argcallback_norm34, callback_hover: ref argcallback_hover34, callback_mousedown: ref argcallback_mousedown35, callback_mousemove: ref argcallback_mousemove35, callback_dblclick: ref argcallback_dblclick35, enabled: ref enabled);
            Action argcallback_norm35 = null;
            Action argcallback_hover35 = null;
            Action argcallback_mousedown36 = null;
            Action argcallback_mousemove36 = null;
            Action argcallback_dblclick36 = null;
            UpdateLabel(Gui.Windows.Count, "lblLabel", 18L, 288L, 138L, 10L, "Stat Points", Core.Font.Arial, Microsoft.Xna.Framework.Color.Green, callback_norm: ref argcallback_norm35, callback_hover: ref argcallback_hover35, callback_mousedown: ref argcallback_mousedown36, callback_mousemove: ref argcallback_mousemove36, callback_dblclick: ref argcallback_dblclick36, enabled: ref enabled);

            // Buttons
            var argcallback_mousedown37 = new Action(Character_SpendPoint1);
            Action argcallback_mousemove37 = null;
            Action argcallback_dblclick37 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnStat_1", 144L, 188L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown37, callback_mousemove: ref argcallback_mousemove37, callback_dblclick: ref argcallback_dblclick37);
            var argcallback_mousedown38 = new Action(Character_SpendPoint2);
            Action argcallback_mousemove38 = null;
            Action argcallback_dblclick38 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnStat_2", 144L, 208L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown38, callback_mousemove: ref argcallback_mousemove38, callback_dblclick: ref argcallback_dblclick3);
            var argcallback_mousedown39 = new Action(Character_SpendPoint3);
            Action argcallback_mousemove39 = null;
            Action argcallback_dblclick39 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnStat_3", 144L, 228L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown39, callback_mousemove: ref argcallback_mousemove39, callback_dblclick: ref argcallback_dblclick39);
            var argcallback_mousedown40 = new Action(Character_SpendPoint4);
            Action argcallback_mousemove40 = null;
            Action argcallback_dblclick40 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnStat_4", 144L, 248L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown40, callback_mousemove: ref argcallback_mousemove40, callback_dblclick: ref argcallback_dblclick4);
            var argcallback_mousedown41 = new Action(Character_SpendPoint5);
            Action argcallback_mousemove41 = null;
            Action argcallback_dblclick41 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnStat_5", 144L, 268L, 15L, 15L, image_norm: 48L, image_hover: 49L, image_mousedown: 50L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown41, callback_mousemove: ref argcallback_mousemove41, callback_dblclick: ref argcallback_dblclick41);

            // fake buttons
            Action argcallback_norm36 = null;
            Action argcallback_hover36 = null;
            Action argcallback_mousedown42 = null;
            Action argcallback_mousemove42 = null;
            Action argcallback_dblclick42 = null;
            Action argonDraw15 = null;
            UpdatePictureBox(Gui.Windows.Count, "btnGreyStat_1", 144L, 188L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm36, callback_hover: ref argcallback_hover36, callback_mousedown: ref argcallback_mousedown42, callback_mousemove: ref argcallback_mousemove42, callback_dblclick: ref argcallback_dblclick42, onDraw: ref argonDraw15);
            Action argcallback_norm37 = null;
            Action argcallback_hover37 = null;
            Action argcallback_mousedown43 = null;
            Action argcallback_mousemove43 = null;
            Action argcallback_dblclick43 = null;
            Action argonDraw16 = null;
            UpdatePictureBox(Gui.Windows.Count, "btnGreyStat_2", 144L, 208L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm37, callback_hover: ref argcallback_hover37, callback_mousedown: ref argcallback_mousedown43, callback_mousemove: ref argcallback_mousemove43, callback_dblclick: ref argcallback_dblclick43, onDraw: ref argonDraw16);
            Action argcallback_norm38 = null;
            Action argcallback_hover38 = null;
            Action argcallback_mousedown44 = null;
            Action argcallback_mousemove44 = null;
            Action argcallback_dblclick44 = null;
            Action argonDraw17 = null;
            UpdatePictureBox(Gui.Windows.Count, "btnGreyStat_3", 144L, 228L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm38, callback_hover: ref argcallback_hover38, callback_mousedown: ref argcallback_mousedown44, callback_mousemove: ref argcallback_mousemove44, callback_dblclick: ref argcallback_dblclick44, onDraw: ref argonDraw17);
            Action argcallback_norm39 = null;
            Action argcallback_hover39 = null;
            Action argcallback_mousedown45 = null;
            Action argcallback_mousemove45 = null;
            Action argcallback_dblclick45 = null;
            Action argonDraw18 = null;
            UpdatePictureBox(Gui.Windows.Count, "btnGreyStat_4", 144L, 248L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm39, callback_hover: ref argcallback_hover39, callback_mousedown: ref argcallback_mousedown45, callback_mousemove: ref argcallback_mousemove45, callback_dblclick: ref argcallback_dblclick45, onDraw: ref argonDraw18);
            Action argcallback_norm40 = null;
            Action argcallback_hover40 = null;
            Action argcallback_mousedown46 = null;
            Action argcallback_mousemove46 = null;
            Action argcallback_dblclick46 = null;
            Action argonDraw19 = null;
            UpdatePictureBox(Gui.Windows.Count, "btnGreyStat_5", 144L, 268L, 15L, 15L, image_norm: 47L, image_hover: 47L, image_mousedown: 47L, callback_norm: ref argcallback_norm40, callback_hover: ref argcallback_hover40, callback_mousedown: ref argcallback_mousedown46, callback_mousemove: ref argcallback_mousemove46, callback_dblclick: ref argcallback_dblclick46, onDraw: ref argonDraw19);

            // Labels
            Action argcallback_norm41 = null;
            Action argcallback_hover41 = null;
            Action argcallback_mousedown47 = null;
            Action argcallback_mousemove47 = null;
            Action argcallback_dblclick47 = null;
            UpdateLabel(Gui.Windows.Count, "lblStat_1", 42L, 188L, 100L, 15L, "255", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm41, callback_hover: ref argcallback_hover41, callback_mousedown: ref argcallback_mousedown47, callback_mousemove: ref argcallback_mousemove47, callback_dblclick: ref argcallback_dblclick47, enabled: ref enabled);
            Action argcallback_norm42 = null;
            Action argcallback_hover42 = null;
            Action argcallback_mousedown48 = null;
            Action argcallback_mousemove48 = null;
            Action argcallback_dblclick48 = null;
            UpdateLabel(Gui.Windows.Count, "lblStat_2", 42L, 208L, 100L, 15L, "255", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm42, callback_hover: ref argcallback_hover42, callback_mousedown: ref argcallback_mousedown48, callback_mousemove: ref argcallback_mousemove48, callback_dblclick: ref argcallback_dblclick48, enabled: ref enabled);
            Action argcallback_norm43 = null;
            Action argcallback_hover43 = null;
            Action argcallback_mousedown49 = null;
            Action argcallback_mousemove49 = null;
            Action argcallback_dblclick49 = null;
            UpdateLabel(Gui.Windows.Count, "lblStat_3", 42L, 228L, 100L, 15L, "255", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm43, callback_hover: ref argcallback_hover43, callback_mousedown: ref argcallback_mousedown49, callback_mousemove: ref argcallback_mousemove49, callback_dblclick: ref argcallback_dblclick49, enabled: ref enabled);
            Action argcallback_norm44 = null;
            Action argcallback_hover44 = null;
            Action argcallback_mousedown50 = null;
            Action argcallback_mousemove50 = null;
            Action argcallback_dblclick50 = null;
            UpdateLabel(Gui.Windows.Count, "lblStat_4", 42L, 248L, 100L, 15L, "255", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm44, callback_hover: ref argcallback_hover44, callback_mousedown: ref argcallback_mousedown50, callback_mousemove: ref argcallback_mousemove50, callback_dblclick: ref argcallback_dblclick50, enabled: ref enabled);
            Action argcallback_norm45 = null;
            Action argcallback_hover45 = null;
            Action argcallback_mousedown51 = null;
            Action argcallback_mousemove51 = null;
            Action argcallback_dblclick51 = null;
            UpdateLabel(Gui.Windows.Count, "lblStat_5", 42L, 268L, 100L, 15L, "255", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm45, callback_hover: ref argcallback_hover45, callback_mousedown: ref argcallback_mousedown51, callback_mousemove: ref argcallback_mousemove51, callback_dblclick: ref argcallback_dblclick51, enabled: ref enabled);
            Action argcallback_norm46 = null;
            Action argcallback_hover46 = null;
            Action argcallback_mousedown52 = null;
            Action argcallback_mousemove52 = null;
            Action argcallback_dblclick52 = null;
            UpdateLabel(Gui.Windows.Count, "lblPoints", 57L, 288L, 100L, 15L, "255", Core.Font.Arial, Microsoft.Xna.Framework.Color.White, Alignment.Right, callback_norm: ref argcallback_norm46, callback_hover: ref argcallback_hover46, callback_mousedown: ref argcallback_mousedown52, callback_mousemove: ref argcallback_mousemove52, callback_dblclick: ref argcallback_dblclick5, enabled: ref enabled);
        }

        public static void UpdateWindow_Description()
        {
            // Control window
            UpdateWindow("winDescription", "", Core.Font.Georgia, zOrder_Win, 0L, 0L, 193L, 142L, 0L, false, design_norm: (long)UiDesign.WindowDescription, design_hover: (long)UiDesign.WindowDescription, design_mousedown: (long)UiDesign.WindowDescription, canDrag: false, clickThrough: true);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Name
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblName", 8L, 12L, 177L, 10L, "Flame Sword", Core.Font.Arial, Microsoft.Xna.Framework.Color.Blue, Alignment.Center, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, enabled: ref enabled);

            // Sprite box
            var argonDraw = new Action(Description_OnDraw);
            Action argcallback_norm_pic = null;
            Action argcallback_hover_pic = null;
            Action argcallback_mousedown_pic = null;
            Action argcallback_mousemove_pic = null;
            Action argcallback_dblclick_pic = null;
            Gui.UpdatePictureBox(Gui.Windows.Count, "picSprite", 18L, 32L, 68L, 68L, design_norm: (long)UiDesign.DescriptionPicture, design_hover: (long)UiDesign.DescriptionPicture, design_mousedown: (long)UiDesign.DescriptionPicture, callback_norm: ref argcallback_norm_pic, callback_hover: ref argcallback_hover_pic, callback_mousedown: ref argcallback_mousedown_pic, callback_mousemove: ref argcallback_mousemove_pic, callback_dblclick: ref argcallback_dblclick_pic, onDraw: ref argonDraw);

            // Sep
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picSep", 96L, 28L, 0L, 92L, image_norm: 44L, image_hover: 44L, image_mousedown: 44L, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw1);

            // Requirements
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            UpdateLabel(Gui.Windows.Count, "lblJob", 5L, 102L, 92L, 10L, "Warrior", Core.Font.Georgia, Microsoft.Xna.Framework.Color.Green, Alignment.Center, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, enabled: ref enabled);
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            UpdateLabel(Gui.Windows.Count, "lblLevel", 5L, 114L, 92L, 10L, "Level 20", Core.Font.Georgia, Microsoft.Xna.Framework.Color.Red, Alignment.Center, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, enabled: ref enabled);

            // Bar
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBar", 19L, 114L, 66L, 12L, false, image_norm: 45L, image_hover: 45L, image_mousedown: 45L, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4, onDraw: ref argonDraw2);
        }

        public static void UpdateWindow_RightClick()
        {
            // Control window
            UpdateWindow("winRightClickBG", "", Core.Font.Georgia, zOrder_Win, 0L, 0L, 800L, 600L, 0L, false, callback_mousedown: new Action(RightClick_Close), canDrag: false);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);
        }

        public static void UpdateWindow_PlayerMenu()
        {
            // Control window  
            UpdateWindow("winPlayerMenu", "", Core.Font.Georgia, zOrder_Win, 0L, 0L, 110L, 106L, 0L, false, design_norm: (long)UiDesign.WindowDescription, design_hover: (long)UiDesign.WindowDescription, design_mousedown: (long)UiDesign.WindowDescription, callback_mousedown: new Action(RightClick_Close), canDrag: false);

            // Centralize it  
            CentralizeWindow(Gui.Windows.Count);

            // Name  
            var argcallback_mousedown = new Action(RightClick_Close);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnName", 8L, 8L, 94L, 18L, "[Name]", Core.Font.Georgia, design_norm: (long)UiDesign.MenuHeader, design_hover: (long)UiDesign.MenuHeader, design_mousedown: (long)UiDesign.MenuHeader, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Options  
            var argcallback_mousedown1 = new Action(PlayerMenu_Party);
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnParty", 8L, 26L, 94L, 18L, "Invite to Party", Core.Font.Georgia, design_hover: (long)UiDesign.MenuOption, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1);

            var argcallback_mousedown2 = new Action(PlayerMenu_Trade);
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnTrade", 8L, 44L, 94L, 18L, "Request Trade", Core.Font.Georgia, design_hover: (long)UiDesign.MenuOption, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2);

            var argcallback_mousedown3 = new Action(PlayerMenu_Guild);
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnGuild", 8L, 62L, 94L, 18L, "Invite to Guild", Core.Font.Georgia, design_norm: (long)UiDesign.MenuOption, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3);

            var argcallback_mousedown4 = new Action(PlayerMenu_Player);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnPM", 8L, 80L, 94L, 18L, "Private Message", Core.Font.Georgia, design_hover: (long)UiDesign.MenuOption, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
        }

        public static void UpdateWindow_DragBox()
        {
            // Control window
            UpdateWindow("winDragBox", "", Core.Font.Georgia, zOrder_Win, 0L, 0L, 32L, 32L, 0L, false, onDraw: new Action(DragBox_OnDraw));

            // Need to set up unique mouseup event
            Gui.Windows[Gui.Windows.Count].CallBack[(int)ControlState.MouseUp] = new Action(DragBox_Check);
        }

        public static void UpdateWindow_Options()
        {
            UpdateWindow("winOptions", "", Core.Font.Georgia, zOrder_Win, 0L, 0L, 210L, 212L, 0L, Conversions.ToBoolean(0), design_norm: (long)UiDesign.WindowNoBar, design_hover: (long)UiDesign.WindowNoBar, design_mousedown: (long)UiDesign.WindowNoBar, isActive: false, clickThrough: false);

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Parchment
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Action argcallback_mousedown = null;
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argonDraw = null;
            UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 6L, 198L, 200L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick, onDraw: ref argonDraw);

            // General
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Action argonDraw1 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlank", 35L, 25L, 140L, 10L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw1);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblBlank", 35L, 22L, 140L, 0L, "General Options", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, enabled: ref enabled);

            // Check boxes
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            UpdateCheckBox(Gui.Windows.Count, "chkMusic", 35L, 40L, 80L, text: "Music", font: Core.Font.Georgia, theDesign: (long)UiDesign.CheckboxNormal, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3);
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Action argcallback_mousedown4 = null;
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            UpdateCheckBox(Gui.Windows.Count, "chkSound", 115L, 40L, 80L, text: "Sound", font: Core.Font.Georgia, theDesign: (long)UiDesign.CheckboxNormal, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Action argcallback_mousedown5 = null;
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            UpdateCheckBox(Gui.Windows.Count, "chkAutotile", 35L, 60L, 80L, text: "Autotile", font: Core.Font.Georgia, theDesign: (long)UiDesign.CheckboxNormal, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5);
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Action argcallback_mousedown6 = null;
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            UpdateCheckBox(Gui.Windows.Count, "chkFullscreen", 115L, 60L, 80L, text: "Fullscreen", font: Core.Font.Georgia, theDesign: (long)UiDesign.CheckboxNormal, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6);

            // Resolution
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown7 = null;
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picBlank", 35L, 85L, 140L, 10L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7, onDraw: ref argonDraw2);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            UpdateLabel(Gui.Windows.Count, "lblBlank", 35L, 92L, 140L, 10L, "Select Resolution", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Center, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, enabled: ref enabled);

            // combobox
            UpdateComboBox(Gui.Windows.Count, "cmbRes", 30L, 100L, 150L, 18L, (long)UiDesign.ComboBoxNormal);

            // Button
            Action argcallback_mousedown9 = btnOptions_Confirm;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnConfirm", 65L, 168L, 80L, 22L, "Confirm", Core.Font.Georgia, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_hover: ref argcallback_hover, callback_norm: ref argcallback_norm, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9);

            // Populate the options screen
            GameLogic.SetOptionsScreen();
        }

        public static void UpdateWindow_Combobox()
        {
            // background window
            UpdateWindow("winComboMenuBG", "ComboMenuBG", Core.Font.Georgia, zOrder_Win, 0L, 0L, 800L, 600L, 0L, false, callback_dblclick: new Action(CloseComboMenu), zChange: Conversions.ToByte(false), isActive: false);

            // window
            UpdateWindow("winComboMenu", "ComboMenu", Core.Font.Georgia, zOrder_Win, 0L, 0L, 100L, 100L, 0L, false, design_norm: (long)UiDesign.ComboMenuNormal, isActive: false, clickThrough: false);

            // centralize it
            CentralizeWindow(Gui.Windows.Count);
        }

        public static void UpdateWindow_Skills()
        {
            // Control window
            UpdateWindow("winSkills", "Skills", Core.Font.Georgia, zOrder_Win, 0L, 0L, 202L, 297L, 109L, false, 2L, 7L, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, callback_mousemove: new Action(Skills_MouseMove), callback_mousedown: new Action(Skills_MouseDown), callback_dblclick: new Action(Skills_DoubleClick), onDraw: new Action(DrawSkills));

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;

            // Close button
            var argcallback_mousedown = new Action(btnMenu_Skills);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 16L, 16L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
        }

        public static void UpdateWindow_Bank()
        {
            UpdateWindow("winBank", "Bank", Core.Font.Georgia, zOrder_Win, 0L, 0L, 390L, 373L, 0L, false, 2L, 5L, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, callback_mousemove: new Action(Bank_MouseMove), callback_mousedown: new Action(Bank_MouseDown), callback_dblclick: new Action(Bank_DoubleClick), onDraw: new Action(DrawBank));

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Set the index for spawning controls
            zOrder_Con = 0L;
            var argcallback_mousedown = new Action(btnMenu_Bank);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 5L, 36L, 36L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);
        }

        public static void UpdateWindow_Shop()
        {
            // Control window
            UpdateWindow("winShop", "Shop", Core.Font.Georgia, zOrder_Win, 0L, 0L, 278L, 293L, 17L, false, 2L, 5L, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, (long)UiDesign.WindowEmpty, callback_mousemove: new Action(Shop_MouseMove), callback_mousedown: new Action(Shop_MouseDown), onDraw: new Action(DrawShopBackground));

            // Centralize it
            CentralizeWindow(Gui.Windows.Count);

            // Close button
            var argcallback_mousedown = new Action(btnShop_Close);
            Action argcallback_mousemove = null;
            Action argcallback_dblclick = null;
            Action argcallback_norm = null;
            Action argcallback_hover = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnClose", Gui.Windows[Gui.Windows.Count].Width - 19L, 6L, 36L, 36L, image_norm: 8L, image_hover: 9L, image_mousedown: 10L, callback_norm: ref argcallback_norm, callback_hover: ref argcallback_hover, callback_mousedown: ref argcallback_mousedown, callback_mousemove: ref argcallback_mousemove, callback_dblclick: ref argcallback_dblclick);

            // Parchment
            var argonDraw = new Action(DrawShop);
            Action argcallback_norm1 = null;
            Action argcallback_hover1 = null;
            Action argcallback_mousedown1 = null;
            Action argcallback_mousemove1 = null;
            Action argcallback_dblclick1 = null;
            Gui.UpdatePictureBox(Gui.Windows.Count, "picParchment", 6L, 215L, 266L, 50L, design_norm: (long)UiDesign.Parchment, design_hover: (long)UiDesign.Parchment, design_mousedown: (long)UiDesign.Parchment, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown1, callback_mousemove: ref argcallback_mousemove1, callback_dblclick: ref argcallback_dblclick1, onDraw: ref argonDraw);

            // Picture Box
            Action argcallback_mousedown2 = null;
            Action argcallback_mousemove2 = null;
            Action argcallback_dblclick2 = null;
            Action argonDraw2 = null;
            UpdatePictureBox(Gui.Windows.Count, "picItemBG", 13L, 222L, 36L, 36L, image_norm: 30L, image_hover: 30L, image_mousedown: 30L, callback_norm: ref argcallback_norm1, callback_hover: ref argcallback_hover1, callback_mousedown: ref argcallback_mousedown2, callback_mousemove: ref argcallback_mousemove2, callback_dblclick: ref argcallback_dblclick2, onDraw: ref argonDraw2);
            Action argcallback_norm2 = null;
            Action argcallback_hover2 = null;
            Action argcallback_mousedown3 = null;
            Action argcallback_mousemove3 = null;
            Action argcallback_dblclick3 = null;
            Action argonDraw3 = null;
            UpdatePictureBox(Gui.Windows.Count, "picItem", 15L, 224L, 32L, 32L, callback_norm: ref argcallback_norm2, callback_hover: ref argcallback_hover2, callback_mousedown: ref argcallback_mousedown3, callback_mousemove: ref argcallback_mousemove3, callback_dblclick: ref argcallback_dblclick3, onDraw: ref argonDraw3);

            // Buttons
            var argcallback_mousedown4 = new Action(btnShopBuy);
            Action argcallback_mousemove4 = null;
            Action argcallback_dblclick4 = null;
            Action argcallback_norm3 = null;
            Action argcallback_hover3 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnBuy", 190L, 228L, 70L, 24L, "Buy", Core.Font.Arial, design_norm: (long)UiDesign.Green, design_hover: (long)UiDesign.GreenHover, design_mousedown: (long)UiDesign.GreenClick, callback_norm: ref argcallback_norm3, callback_hover: ref argcallback_hover3, callback_mousedown: ref argcallback_mousedown4, callback_mousemove: ref argcallback_mousemove4, callback_dblclick: ref argcallback_dblclick4);
            var argcallback_mousedown5 = new Action(btnShopSell);
            Action argcallback_mousemove5 = null;
            Action argcallback_dblclick5 = null;
            Action argcallback_norm4 = null;
            Action argcallback_hover4 = null;
            Gui.UpdateButton(Gui.Windows.Count, "btnSell", 190L, 228L, 70L, 24L, "Sell", Core.Font.Arial, visible: false, design_norm: (long)UiDesign.Red, design_hover: (long)UiDesign.RedHover, design_mousedown: (long)UiDesign.RedClick, callback_norm: ref argcallback_norm4, callback_hover: ref argcallback_hover4, callback_mousedown: ref argcallback_mousedown5, callback_mousemove: ref argcallback_mousemove5, callback_dblclick: ref argcallback_dblclick5);

            // Buying/Selling
            var argcallback_mousedown6 = new Action(chkShopBuying);
            Action argcallback_mousemove6 = null;
            Action argcallback_dblclick6 = null;
            Action argcallback_norm5 = null;
            Action argcallback_hover5 = null;
            Gui.UpdateCheckBox(Gui.Windows.Count, "CheckboxBuying", 173L, 265L, 49L, 20L, 0L, theDesign: (long)UiDesign.CheckboxBuying, callback_norm: ref argcallback_norm5, callback_hover: ref argcallback_hover5, callback_mousedown: ref argcallback_mousedown6, callback_mousemove: ref argcallback_mousemove6, callback_dblclick: ref argcallback_dblclick6);
            var argcallback_mousedown7 = new Action(chkShopSelling);
            Action argcallback_mousemove7 = null;
            Action argcallback_dblclick7 = null;
            Action argcallback_norm6 = null;
            Action argcallback_hover6 = null;
            Gui.UpdateCheckBox(Gui.Windows.Count, "CheckboxSelling", 222L, 265L, 49L, 20L, 0L, theDesign: (long)UiDesign.CheckboxSelling, callback_norm: ref argcallback_norm6, callback_hover: ref argcallback_hover6, callback_mousedown: ref argcallback_mousedown7, callback_mousemove: ref argcallback_mousemove7, callback_dblclick: ref argcallback_dblclick7);

            // Labels
            Action argcallback_norm7 = null;
            Action argcallback_hover7 = null;
            Action argcallback_mousedown8 = null;
            Action argcallback_mousemove8 = null;
            Action argcallback_dblclick8 = null;
            bool enabled = false;
            UpdateLabel(Gui.Windows.Count, "lblName", 56L, 226L, 300L, 10L, "Test Item", Core.Font.Arial, Microsoft.Xna.Framework.Color.Black, Alignment.Left, callback_norm: ref argcallback_norm7, callback_hover: ref argcallback_hover7, callback_mousedown: ref argcallback_mousedown8, callback_mousemove: ref argcallback_mousemove8, callback_dblclick: ref argcallback_dblclick8, enabled: ref enabled);
            Action argcallback_norm8 = null;
            Action argcallback_hover8 = null;
            Action argcallback_mousedown9 = null;
            Action argcallback_mousemove9 = null;
            Action argcallback_dblclick9 = null;
            UpdateLabel(Gui.Windows.Count, "lblCost", 56L, 240L, 300L, 10L, "1000g", Core.Font.Arial, Microsoft.Xna.Framework.Color.Black, Alignment.Left, callback_norm: ref argcallback_norm8, callback_hover: ref argcallback_hover8, callback_mousedown: ref argcallback_mousedown9, callback_mousemove: ref argcallback_mousemove9, callback_dblclick: ref argcallback_dblclick9, enabled: ref enabled);

            // Gold
            Action argcallback_norm9 = null;
            Action argcallback_hover9 = null;
            Action argcallback_mousedown10 = null;
            Action argcallback_mousemove10 = null;
            Action argcallback_dblclick10 = null;
            //UpdateLabel(Gui.Windows.Count, "lblGold", 44L, 269L, 300L, 10L, "g", Core.Font.Georgia, Microsoft.Xna.Framework.Color.White, Alignment.Left, callback_norm: ref argcallback_norm9, callback_hover: ref argcallback_hover9, callback_mousedown: ref argcallback_mousedown10, callback_mousemove: ref argcallback_mousemove10, callback_dblclick: ref argcallback_dblclick10, enabled: ref enabled);
        }
    }
}
