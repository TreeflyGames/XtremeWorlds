using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Core;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Client
{

    internal partial class frmEditor_Event
    {
        private int tmpGraphicIndex;
        private byte tmpGraphicType;

        #region Frm Code

        public void ClearConditionFrame()
        {
            int i;

            cmbCondition_PlayerVarIndex.Enabled = false;
            cmbCondition_PlayerVarIndex.Items.Clear();

            for (i = 0; i < Constant.NAX_VARIABLES; i++)
                cmbCondition_PlayerVarIndex.Items.Add(i + 1 + ". " + Event.Variables[i]);
            cmbCondition_PlayerVarIndex.SelectedIndex = 0;
            cmbCondition_PlayerVarCompare.SelectedIndex = 0;
            cmbCondition_PlayerVarCompare.Enabled = false;
            nudCondition_PlayerVarCondition.Enabled = false;
            nudCondition_PlayerVarCondition.Value = 0m;
            cmbCondition_PlayerSwitch.Enabled = false;
            cmbCondition_PlayerSwitch.Items.Clear();

            for (i = 0; i < Constant.MAX_SWITCHES; i++)
                cmbCondition_PlayerSwitch.Items.Add(i + 1 + ". " + Event.Switches[i]);
            cmbCondition_PlayerSwitch.SelectedIndex = 0;
            cmbCondtion_PlayerSwitchCondition.Enabled = false;
            cmbCondtion_PlayerSwitchCondition.SelectedIndex = 0;
            cmbCondition_HasItem.Enabled = false;
            cmbCondition_HasItem.Items.Clear();

            for (i = 0; i < Constant.MAX_ITEMS; i++)
                cmbCondition_HasItem.Items.Add(i + 1 + ". " + Core.Type.Item[i].Name);
            cmbCondition_HasItem.SelectedIndex = 0;
            nudCondition_HasItem.Enabled = false;
            nudCondition_HasItem.Value = 1m;
            cmbCondition_JobIs.Enabled = false;
            cmbCondition_JobIs.Items.Clear();

            for (i = 0; i < Constant.MAX_JOBS; i++)
                cmbCondition_JobIs.Items.Add(i + 1 + ". " + Core.Type.Job[i].Name);
            cmbCondition_JobIs.SelectedIndex = 0;
            cmbCondition_LearntSkill.Enabled = false;
            cmbCondition_LearntSkill.Items.Clear();

            for (i = 0; i < Constant.MAX_SKILLS; i++)
                cmbCondition_LearntSkill.Items.Add(i + 1 + ". " + Strings.Trim(Core.Type.Skill[i].Name));
            cmbCondition_LearntSkill.SelectedIndex = 0;
            cmbCondition_LevelCompare.Enabled = false;
            cmbCondition_LevelCompare.SelectedIndex = 0;
            nudCondition_LevelAmount.Enabled = false;
            nudCondition_LevelAmount.Value = 0m;
            if (cmbCondition_SelfSwitch.Items.Count > 0)
            {
                cmbCondition_SelfSwitch.SelectedIndex = 0;
            }

            cmbCondition_SelfSwitch.Enabled = false;

            if (cmbCondition_SelfSwitchCondition.Items.Count > 0)
            {
                cmbCondition_SelfSwitchCondition.SelectedIndex = 0;
            }

            cmbCondition_SelfSwitchCondition.Enabled = false;

            cmbCondition_Gender.Enabled = false;

            cmbCondition_Time.Enabled = false;
        }

        private void frmEditor_Events_Load(object sender, EventArgs e)
        {
            int i;

            cmbSwitch.Items.Clear();
            for (i = 0; i < Constant.MAX_SWITCHES; i++)
                cmbSwitch.Items.Add(i + 1 + ". " + Event.Switches[i]);
            cmbSwitch.SelectedIndex = 0;
            cmbVariable.Items.Clear();

            for (i = 0; i < Constant.NAX_VARIABLES; i++)
                cmbVariable.Items.Add(i + 1 + ". " + Event.Variables[i]);
            cmbVariable.SelectedIndex = 0;
            cmbChangeItemIndex.Items.Clear();
            for (i = 0; i < Constant.MAX_ITEMS; i++)
                cmbChangeItemIndex.Items.Add(Core.Type.Item[i].Name);
            cmbChangeItemIndex.SelectedIndex = 0;
            nudChangeLevel.Minimum = 1m;
            nudChangeLevel.Maximum = Constant.MAX_LEVEL;
            nudChangeLevel.Value = 1m;
            cmbChangeSkills.Items.Clear();

            for (i = 0; i < Constant.MAX_SKILLS; i++)
                cmbChangeSkills.Items.Add(Core.Type.Skill[i].Name);
            cmbChangeSkills.SelectedIndex = 0;
            cmbChangeJob.Items.Clear();

            for (i = 0; i < Constant.MAX_JOBS; i++)
                cmbChangeJob.Items.Add(Strings.Trim(Core.Type.Job[i].Name));
            cmbChangeJob.SelectedIndex = 0;
            nudChangeSprite.Maximum = GameState.NumCharacters;
            cmbPlayAnim.Items.Clear();

            for (i = 0; i < Constant.MAX_ANIMATIONS; i++)
                cmbPlayAnim.Items.Add(i + 1 + ". " + Core.Type.Animation[i].Name);
            cmbPlayAnim.SelectedIndex = 0;

            cmbPlayBGM.Items.Clear();

            General.CacheMusic();
            var loopTo = Information.UBound(Sound.MusicCache);
            for (i = 0; i < loopTo; i++)
                cmbPlayBGM.Items.Add(Sound.MusicCache[i]);
            cmbPlayBGM.SelectedIndex = 0;
            cmbPlaySound.Items.Clear();

            General.CacheSound();
            var loopTo1 = Information.UBound(Sound.SoundCache);
            for (i = 0; i < loopTo1; i++)
                cmbPlaySound.Items.Add(Sound.SoundCache[i]);
            cmbPlaySound.SelectedIndex = 0;
            cmbOpenShop.Items.Clear();

            for (i = 0; i < Constant.MAX_SHOPS; i++)
                cmbOpenShop.Items.Add(i + 1 + ". " + Core.Type.Shop[i].Name);
            cmbOpenShop.SelectedIndex = 0;
            cmbSpawnNPC.Items.Clear();

            for (i = 0; i < Constant.MAX_MAP_NPCS; i++)
            {
                if (Core.Type.MyMap.NPC[i] > 0)
                {
                    cmbSpawnNPC.Items.Add(i + 1 + ". " + Core.Type.NPC[Core.Type.MyMap.NPC[i]].Name);
                }
                else
                {
                    cmbSpawnNPC.Items.Add(i + ". ");
                }
            }

            cmbSpawnNPC.SelectedIndex = 0;
            nudFogData0.Maximum = GameState.NumFogs;
            nudWPMap.Maximum = Constant.MAX_MAPS;

            Width = 946;

            fraDialogue.Width = Width;
            fraDialogue.Height = Height;
            fraDialogue.Top = 0;
            fraDialogue.Left = 0;

            fraMoveRoute.Width = Width;
            fraMoveRoute.Height = Height;
            fraMoveRoute.Top = 0;
            fraMoveRoute.Left = 0;

            cmbEvent.Items.Add("This Event");
            cmbEvent.SelectedIndex = 0;

            // set the tabs
            tabPages.TabPages.Clear();

            var loopTo2 = Event.TmpEvent.PageCount;
            for (i = 0; i < loopTo2; i++)
                tabPages.TabPages.Add(Conversion.Str(i));

            // items
            cmbHasItem.Items.Clear();
            for (i = 0; i < Constant.MAX_ITEMS; i++)
                cmbHasItem.Items.Add(i + 1 + ": " + Core.Type.Item[i].Name);

            // variables
            cmbPlayerVar.Items.Clear();
            for (i = 0; i < Constant.NAX_VARIABLES; i++)
                cmbPlayerVar.Items.Add(i + 1 + ". " + Event.Variables[i]);
            // switches
            cmbPlayerSwitch.Items.Clear();
            for (i = 0; i < Constant.MAX_SWITCHES; i++)
                cmbPlayerSwitch.Items.Add(i + 1 + ". " + Event.Switches[i]);
            cmbSelfSwitch.SelectedIndex = 0;

            // enable delete button
            if (Event.TmpEvent.PageCount > 1)
            {
                btnDeletePage.Enabled = true;
            }
            else
            {
                btnDeletePage.Enabled = false;
            }
            btnPastePage.Enabled = false;

            nudShowPicture.Maximum = GameState.NumPictures;

            cmbPicLoc.SelectedIndex = 0;

            fraDialogue.Visible = false;

            if (tabPages.SelectedIndex == 0)
                tabPages.SelectedIndex = 1;

            // Load page 1 to start off with
            Event.CurPageNum = 1;
            if (string.IsNullOrEmpty(Event.TmpEvent.Name))
                Event.TmpEvent.Name = "";
            txtName.Text = Event.TmpEvent.Name;

            Event.EventEditorLoadPage(Event.CurPageNum);
            DrawGraphic();
        }

        public void DrawGraphic()
        {
            Core.Type.RectStruct sRect;
            Core.Type.RectStruct dRect;
            Bitmap targetBitmap; // Bitmap we draw to
            Bitmap sourceBitmap; // This is our sprite or tileset that we are drawing from
            Graphics g; // This is our graphics Job that helps us draw to the targetBitmap

            if (picGraphicSel.Visible)
            {
                switch (cmbGraphic.SelectedIndex)
                {
                    case 0:
                        {
                            // None
                            picGraphicSel.BackgroundImage = null;
                            break;
                        }
                    case 1:
                        {
                            if (nudGraphic.Value > 0m & nudGraphic.Value <= GameState.NumCharacters)
                            {
                                // Load character from Contents into our sourceBitmap
                                sourceBitmap = new Bitmap(System.IO.Path.Combine(Core.Path.Characters, nudGraphic.Value + GameState.GfxExt));
                                targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height); // Create our target Bitmap

                                // Create the Graphics object
                                g = Graphics.FromImage(targetBitmap);

                                // This is the section we are pulling from the source graphic (using RectangleF)
                                var sourceRect = new RectangleF(0f, 0f, sourceBitmap.Width / 4.0f, sourceBitmap.Height / 4.0f);

                                // This is the rectangle in the target graphic we want to render to (using RectangleF)
                                var destRect = new RectangleF(0f, 0f, targetBitmap.Width / 4.0f, targetBitmap.Height / 4.0f);

                                // Draw the image using RectangleF for source and destination rectangles
                                g.DrawImage(sourceBitmap, destRect, sourceRect, GraphicsUnit.Pixel);

                                // Draw a rectangle (using RectangleF)
                                var graphicRectF = new RectangleF(Event.GraphicSelX * GameState.PicX, Event.GraphicSelY * GameState.PicY, Event.GraphicSelX2 * GameState.PicX, Event.GraphicSelY2 * GameState.PicY);
                                g.DrawRectangle(Pens.Red, graphicRectF);

                                // Set the BackgroundImage properties of the forms
                                picGraphic.BackgroundImage = targetBitmap;
                                picGraphicSel.BackgroundImage = null;

                                // Dispose of the Graphics object
                                g.Dispose();
                            }

                            else
                            {
                                picGraphic.BackgroundImage = null;
                                picGraphicSel.BackgroundImage = null;
                                return;
                            }

                            break;
                        }
                    case 2:
                        {
                            if (nudGraphic.Value > 0m & nudGraphic.Value <= GameState.NumTileSets)
                            {
                                // Load tilesheet from Contents into our sourceBitmap
                                sourceBitmap = new Bitmap(System.IO.Path.Combine(Core.Path.Tilesets, nudGraphic.Value + GameState.GfxExt));
                                targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height); // Create our target Bitmap

                                if (Event.TmpEvent.Pages[Event.CurPageNum].GraphicX2 == 0 & Event.TmpEvent.Pages[Event.CurPageNum].GraphicY2 == 0)
                                {
                                    sRect.Top = Event.TmpEvent.Pages[Event.CurPageNum].GraphicY * 32;
                                    sRect.Left = Event.TmpEvent.Pages[Event.CurPageNum].GraphicX * 32;
                                    sRect.Bottom = sRect.Top + 32d;
                                    sRect.Right = sRect.Left + 32d;

                                    dRect.Top = 193d / 2d - (sRect.Bottom - sRect.Top) / 2d;
                                    dRect.Bottom = dRect.Top + (sRect.Bottom - sRect.Top);
                                    dRect.Left = 120d / 2d - (sRect.Right - sRect.Left) / 2d;
                                    dRect.Right = dRect.Left + (sRect.Right - sRect.Left);
                                }
                                else
                                {
                                    sRect.Top = Event.TmpEvent.Pages[Event.CurPageNum].GraphicY * 32;
                                    sRect.Left = Event.TmpEvent.Pages[Event.CurPageNum].GraphicX * 32;
                                    sRect.Bottom = sRect.Top + (Event.TmpEvent.Pages[Event.CurPageNum].GraphicY2 - Event.TmpEvent.Pages[Event.CurPageNum].GraphicY) * 32;
                                    sRect.Right = sRect.Left + (Event.TmpEvent.Pages[Event.CurPageNum].GraphicX2 - Event.TmpEvent.Pages[Event.CurPageNum].GraphicX) * 32;

                                    dRect.Top = 193d / 2d - (sRect.Bottom - sRect.Top) / 2d;
                                    dRect.Bottom = dRect.Top + (sRect.Bottom - sRect.Top);
                                    dRect.Left = 120d / 2d - (sRect.Right - sRect.Left) / 2d;
                                    dRect.Right = dRect.Left + (sRect.Right - sRect.Left);

                                }

                                g = Graphics.FromImage(targetBitmap);

                                var sourceRect = new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height);  // This is the section we are pulling from the source graphic
                                var destRect = new Rectangle(0, 0, targetBitmap.Width, targetBitmap.Height);     // This is the rectangle in the target graphic we want to render to

                                // Ensure destRect and sourceRect are RectangleF
                                var destRectF = new RectangleF(destRect.X, destRect.Y, destRect.Width, destRect.Height);
                                var sourceRectF = new RectangleF(sourceRect.X, sourceRect.Y, sourceRect.Width, sourceRect.Height);

                                // Call DrawImage with RectangleF
                                g.DrawImage(sourceBitmap, destRectF, sourceRectF, GraphicsUnit.Pixel);

                                // For DrawRectangle, ensure the rectangle is of type Rectangle
                                var rectF = new RectangleF(Event.GraphicSelX * GameState.PicX, Event.GraphicSelY * GameState.PicY, Event.GraphicSelX2 * GameState.PicX, Event.GraphicSelY2 * GameState.PicY);
                                g.DrawRectangle(Pens.Red, rectF);

                                g.Dispose();


                                picGraphicSel.BackgroundImage = targetBitmap;
                                picGraphic.BackgroundImage = null;
                            }
                            else
                            {
                                picGraphicSel.BackgroundImage = null;
                                picGraphic.BackgroundImage = null;
                                return;
                            }

                            break;
                        }
                }
            }
            else if (Event.TmpEvent.PageCount > 0)
            {
                switch (Event.TmpEvent.Pages[Event.CurPageNum].GraphicType)
                {
                    case 0:
                        {
                            picGraphicSel.BackgroundImage = null;
                            break;
                        }
                    case 1:
                        {
                            if (Event.TmpEvent.Pages[Event.CurPageNum].Graphic > 0 & Event.TmpEvent.Pages[Event.CurPageNum].Graphic <= GameState.NumCharacters)
                            {
                                // Load character from Contents into our sourceBitmap
                                sourceBitmap = new Bitmap(System.IO.Path.Combine(Core.Path.Characters, Event.TmpEvent.Pages[Event.CurPageNum].Graphic + GameState.GfxExt));
                                targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height); // Create our target Bitmap

                                g = Graphics.FromImage(targetBitmap);

                                var sourceRect = new Rectangle(0, 0, (int)Math.Round(sourceBitmap.Width / 4d), (int)Math.Round(sourceBitmap.Height / 4d));  // This is the section we are pulling from the source graphic
                                var destRect = new Rectangle(0, 0, (int)Math.Round(targetBitmap.Width / 4d), (int)Math.Round(targetBitmap.Height / 4d));     // This is the rectangle in the target graphic we want to render to

                                g.DrawImage(sourceBitmap, destRect, sourceRect, GraphicsUnit.Pixel);
                                g.Dispose();

                                picGraphic.BackgroundImage = targetBitmap;
                            }
                            else
                            {
                                picGraphic.BackgroundImage = null;
                                return;
                            }

                            break;
                        }
                    case 2:
                        {
                            if (Event.TmpEvent.Pages[Event.CurPageNum].Graphic > 0 & Event.TmpEvent.Pages[Event.CurPageNum].Graphic <= GameState.NumTileSets)
                            {
                                // Load tilesheet from Contents into our sourceBitmap
                                sourceBitmap = new Bitmap(Core.Path.Graphics + @"tilesets\" + Event.TmpEvent.Pages[Event.CurPageNum].Graphic + ".png");
                                targetBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height); // Create our target Bitmap

                                if (Event.TmpEvent.Pages[Event.CurPageNum].GraphicX2 == 0 & Event.TmpEvent.Pages[Event.CurPageNum].GraphicY2 == 0)
                                {
                                    sRect.Top = Event.TmpEvent.Pages[Event.CurPageNum].GraphicY * 32;
                                    sRect.Left = Event.TmpEvent.Pages[Event.CurPageNum].GraphicX * 32;
                                    sRect.Bottom = sRect.Top + 32d;
                                    sRect.Right = sRect.Left + 32d;

                                    dRect.Top = 0d;
                                    dRect.Bottom = GameState.PicY;
                                    dRect.Left = 0d;
                                    dRect.Right = GameState.PicX;
                                }
                                else
                                {
                                    sRect.Top = Event.TmpEvent.Pages[Event.CurPageNum].GraphicY * 32;
                                    sRect.Left = Event.TmpEvent.Pages[Event.CurPageNum].GraphicX * 32;
                                    sRect.Bottom = Event.TmpEvent.Pages[Event.CurPageNum].GraphicY2 * 32;
                                    sRect.Right = Event.TmpEvent.Pages[Event.CurPageNum].GraphicX2 * 32;

                                    dRect.Top = 0d;
                                    dRect.Bottom = sRect.Bottom;
                                    dRect.Left = 0d;
                                    dRect.Right = sRect.Right;

                                }

                                g = Graphics.FromImage(targetBitmap);

                                var sourceRect = new Rectangle((int)Math.Round(sRect.Left), (int)Math.Round(sRect.Top), (int)Math.Round(sRect.Right), (int)Math.Round(sRect.Bottom));  // This is the section we are pulling from the source graphic
                                var destRect = new Rectangle((int)Math.Round(dRect.Left), (int)Math.Round(dRect.Top), (int)Math.Round(dRect.Right), (int)Math.Round(dRect.Bottom));     // This is the rectangle in the target graphic we want to render to

                                g.DrawImage(sourceBitmap, destRect, sourceRect, GraphicsUnit.Pixel);
                                g.Dispose();

                                picGraphic.BackgroundImage = targetBitmap;
                            }

                            break;
                        }
                }
            }

        }

        private void frmEditor_Events_FormClosing(object sender, FormClosingEventArgs e)
        {
            Event.TmpEvent = default;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (fraGraphic.Visible == false)
            {
                Event.EventEditorOK();
                Event.TmpEvent = default;
            }
            else
            {
                if (Event.GraphicSelType == 0)
                {
                    Event.TmpEvent.Pages[Event.CurPageNum].GraphicType = (byte)cmbGraphic.SelectedIndex;
                    Event.TmpEvent.Pages[Event.CurPageNum].Graphic = (int)Math.Round(nudGraphic.Value);
                }
                else
                {
                    AddMoveRouteCommand(42);
                    Event.GraphicSelType = 0;
                }
                fraGraphic.Visible = false;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (fraGraphic.Visible == false)
            {
                Event.TmpEvent = default;
                Dispose();
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].GraphicType = tmpGraphicType;
                Event.TmpEvent.Pages[Event.CurPageNum].Graphic = tmpGraphicIndex;
                fraGraphic.Visible = false;
                DrawGraphic();
            }
        }

        private void TvCommands_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var x = default(int);

            fraDialogue.Width = Width;
            fraDialogue.Height = Height;

            fraDialogue.BringToFront();

            // MsgBox(tvCommands.SelectedNode.Text)

            switch (tvCommands.SelectedNode.Text ?? "")
            {
                // Messages

                // show text
                case "Show Text":
                    {
                        txtShowText.Text = "";
                        fraDialogue.Visible = true;
                        fraShowText.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // show choices
                case "Show Choices":
                    {
                        txtChoicePrompt.Text = "";
                        txtChoices1.Text = "";
                        txtChoices2.Text = "";
                        txtChoices3.Text = "";
                        txtChoices4.Text = "";

                        fraDialogue.Visible = true;
                        fraShowChoices.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // chatbox text
                case "Add Chatbox Text":
                    {
                        txtAddText_Text.Text = "";
                        optAddText_Player.Checked = true;
                        fraDialogue.Visible = true;
                        fraAddText.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // chat bubble
                case "Show ChatBubble":
                    {
                        txtChatbubbleText.Text = "";
                        cmbChatBubbleTargetType.SelectedIndex = 0;
                        cmbChatBubbleTarget.Visible = false;
                        fraDialogue.Visible = true;
                        fraShowChatBubble.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // event progression
                // player variable
                case "Set Player Variable":
                    {
                        nudVariableData0.Value = 0m;
                        nudVariableData1.Value = 0m;
                        nudVariableData2.Value = 0m;
                        nudVariableData3.Value = 0m;
                        nudVariableData4.Value = 0m;

                        cmbVariable.SelectedIndex = 0;
                        optVariableAction0.Checked = true;
                        fraDialogue.Visible = true;
                        fraPlayerVariable.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // player switch
                case "Set Player Switch":
                    {
                        cmbPlayerSwitchSet.SelectedIndex = 0;
                        cmbSwitch.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraPlayerSwitch.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // self switch
                case "Set Self Switch":
                    {
                        cmbSetSelfSwitchTo.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraSetSelfSwitch.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // flow control

                // conditional branch
                case "Conditional Branch":
                    {
                        fraDialogue.Visible = true;
                        fraConditionalBranch.Visible = true;
                        optCondition0.Checked = true;
                        ClearConditionFrame();
                        cmbCondition_PlayerVarIndex.Enabled = true;
                        cmbCondition_PlayerVarCompare.Enabled = true;
                        nudCondition_PlayerVarCondition.Enabled = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Exit Event Process
                case "Stop Event Processing":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.ExitProcess);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Label
                case "Label":
                    {
                        txtLabelName.Text = "";
                        fraCreateLabel.Visible = true;
                        fraCommands.Visible = false;
                        fraDialogue.Visible = true;
                        break;
                    }
                // GoTo Label
                case "GoTo Label":
                    {
                        txtGoToLabel.Text = "";
                        fraGoToLabel.Visible = true;
                        fraCommands.Visible = false;
                        fraDialogue.Visible = true;
                        break;
                    }
                // Player Control

                // Change Items
                case "Change Items":
                    {
                        cmbChangeItemIndex.SelectedIndex = 0;
                        optChangeItemSet.Checked = true;
                        nudChangeItemsAmount.Value = 0m;
                        fraDialogue.Visible = true;
                        fraChangeItems.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Restore HP
                case "Restore HP":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.RestoreHP);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Restore MP
                case "Restore MP":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.RestoreSP);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Level Up
                case "Level Up":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.LevelUp);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Change Level
                case "Change Level":
                    {
                        nudChangeLevel.Value = 1m;
                        fraDialogue.Visible = true;
                        fraChangeLevel.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Change Skills
                case "Change Skills":
                    {
                        cmbChangeSkills.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraChangeSkills.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Change Job
                case "Change Job":
                    {
                        if (Constant.MAX_JOBS > 0)
                        {
                            if (cmbChangeJob.Items.Count == 0)
                            {
                                cmbChangeJob.Items.Clear();

                                for (int i = 0; i < Constant.MAX_JOBS; i++)
                                    cmbChangeJob.Items.Add(Strings.Trim(Core.Type.Job[i].Name));
                                cmbChangeJob.SelectedIndex = 0;
                            }
                        }
                        fraDialogue.Visible = true;
                        fraChangeJob.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Change Sprite
                case "Change Sprite":
                    {
                        nudChangeSprite.Value = 1m;
                        fraDialogue.Visible = true;
                        fraChangeSprite.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Change Gender
                case "Change Gender":
                    {
                        optChangeSexMale.Checked = true;
                        fraDialogue.Visible = true;
                        fraChangeGender.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Change PK
                case "Change PK":
                    {
                        cmbSetPK.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraChangePK.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Give Exp
                case "Give Experience":
                    {
                        nudGiveExp.Value = 0m;
                        fraDialogue.Visible = true;
                        fraGiveExp.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Movement

                // Warp Player
                case "Warp Player":
                    {
                        nudWPMap.Value = 0m;
                        nudWPX.Value = 0m;
                        nudWPY.Value = 0m;
                        cmbWarpPlayerDir.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraPlayerWarp.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Set Move Route
                case "Set Move Route":
                    {
                        fraMoveRoute.Visible = true;
                        lstMoveRoute.Items.Clear();
                        Event.ListOfEvents = new int[Core.Type.MyMap.EventCount];
                        Event.ListOfEvents[0] = Event.EditorEvent;
                        for (int i = 0, loopTo = Core.Type.MyMap.EventCount; i < loopTo; i++)
                        {
                            if (i != Event.EditorEvent)
                            {
                                cmbEvent.Items.Add(Core.Type.MyMap.Event[i].Name);
                                x = x + 1;
                                Event.ListOfEvents[x] = i;
                            }
                        }
                        Event.IsMoveRouteCommand = true;
                        chkIgnoreMove.Checked = Conversions.ToBoolean(0);
                        chkRepeatRoute.Checked = Conversions.ToBoolean(0);
                        Event.TempMoveRouteCount = 0;
                        Event.TempMoveRoute = new Core.Type.MoveRouteStruct[1];
                        fraMoveRoute.Visible = true;
                        fraMoveRoute.BringToFront();
                        fraCommands.Visible = false;
                        break;
                    }
                // Wait for Route Completion
                case "Wait for Route Completion":
                    {
                        cmbMoveWait.Items.Clear();
                        Event.ListOfEvents = new int[Core.Type.MyMap.EventCount];
                        Event.ListOfEvents[0] = Event.EditorEvent;
                        cmbMoveWait.Items.Add("This Event");
                        cmbMoveWait.SelectedIndex = 0;
                        cmbMoveWait.Enabled = true;
                        for (int i = 0, loopTo1 = Core.Type.MyMap.EventCount; i < loopTo1; i++)
                        {
                            if (i != Event.EditorEvent)
                            {
                                cmbMoveWait.Items.Add(Core.Type.MyMap.Event[i].Name);
                                x = x + 1;
                                Event.ListOfEvents[x] = i;
                            }
                        }
                        fraDialogue.Visible = true;
                        fraMoveRouteWait.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Force Spawn NPC
                case "Force Spawn NPC":
                    {
                        // lets populate the combobox
                        cmbSpawnNPC.Items.Clear();
                        for (int i = 0; i < Constant.MAX_NPCS; i++)
                            cmbSpawnNPC.Items.Add(Strings.Trim(Core.Type.NPC[i].Name));
                        cmbSpawnNPC.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraSpawnNPC.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Hold Player
                case "Hold Player":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.HoldPlayer);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Release Player
                case "Release Player":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.ReleasePlayer);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Animation

                // Play Animation
                case "Play Animation":
                    {
                        cmbPlayAnimEvent.Items.Clear();

                        for (int i = 0, loopTo2 = Core.Type.MyMap.EventCount; i < loopTo2; i++)
                            cmbPlayAnimEvent.Items.Add(i + 1 + ". " + Core.Type.MyMap.Event[i].Name);
                        cmbPlayAnimEvent.SelectedIndex = 0;
                        cmbAnimTargetType.SelectedIndex = 0;
                        cmbPlayAnim.SelectedIndex = 0;
                        nudPlayAnimTileX.Value = 0m;
                        nudPlayAnimTileY.Value = 0m;
                        nudPlayAnimTileX.Maximum = Core.Type.MyMap.MaxX;
                        nudPlayAnimTileY.Maximum = Core.Type.MyMap.MaxY;
                        fraDialogue.Visible = true;
                        fraPlayAnimation.Visible = true;
                        fraCommands.Visible = false;
                        lblPlayAnimX.Visible = false;
                        lblPlayAnimY.Visible = false;
                        nudPlayAnimTileX.Visible = false;
                        nudPlayAnimTileY.Visible = false;
                        cmbPlayAnimEvent.Visible = false;
                        break;
                    }
                // Map Functions

                // Set Fog
                case "Set Fog":
                    {
                        nudFogData0.Value = 0m;
                        nudFogData1.Value = 0m;
                        nudFogData2.Value = 0m;
                        fraDialogue.Visible = true;
                        fraSetFog.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Set Weather
                case "Set Weather":
                    {
                        CmbWeather.SelectedIndex = 0;
                        nudWeatherIntensity.Value = 0m;
                        fraDialogue.Visible = true;
                        fraSetWeather.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Set Map Tinting
                case "Set Map Tinting":
                    {
                        nudMapTintData0.Value = 0m;
                        nudMapTintData1.Value = 0m;
                        nudMapTintData2.Value = 0m;
                        nudMapTintData3.Value = 0m;
                        fraDialogue.Visible = true;
                        fraMapTint.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Music and Sound

                // PlayBGM
                case "Play BGM":
                    {
                        cmbPlayBGM.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraPlayBGM.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Stop BGM
                case "Stop BGM":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.FadeoutBgm);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Play Sound
                case "Play Sound":
                    {
                        cmbPlaySound.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraPlaySound.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Stop Sounds
                case "Stop Sounds":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.StopSound);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Etc...

                // Wait...
                case "Wait...":
                    {
                        nudWaitAmount.Value = 1m;
                        fraDialogue.Visible = true;
                        fraSetWait.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Set Access
                case "Set Access":
                    {
                        cmbSetAccess.SelectedIndex = 0;
                        fraDialogue.Visible = true;
                        fraSetAccess.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Custom Script
                case "Custom Script":
                    {
                        nudCustomScript.Value = 0m;
                        fraDialogue.Visible = true;
                        fraCustomScript.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }

                // Shop, bank etc

                // Open bank
                case "Open Bank":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.OpenBank);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Open shop
                case "Open Shop":
                    {
                        fraDialogue.Visible = true;
                        fraOpenShop.Visible = true;
                        cmbOpenShop.SelectedIndex = 0;
                        fraCommands.Visible = false;
                        break;
                    }
                // cutscene options

                // Fade in
                case "Fade In":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.FadeIn);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Fade out
                case "Fade Out":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.FadeOut);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Flash white
                case "Flash White":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.FlashWhite);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
                // Show pic
                case "Show Picture":
                    {
                        nudShowPicture.Value = 0m;
                        cmbPicLoc.SelectedIndex = 0;
                        nudPicOffsetX.Value = 0m;
                        nudPicOffsetY.Value = 0m;
                        fraDialogue.Visible = true;
                        fraShowPic.Visible = true;
                        fraCommands.Visible = false;
                        break;
                    }
                // Hide pic
                case "Hide Picture":
                    {
                        Event.AddCommand((int)Core.Enum.EventType.HidePicture);
                        fraCommands.Visible = false;
                        fraDialogue.Visible = false;
                        break;
                    }
            }
        }

        private void BtnCancelCommand_Click(object sender, EventArgs e)
        {
            fraCommands.Visible = false;
        }

        #endregion

        #region Page Buttons

        private void TabPages_Click(object sender, EventArgs e)
        {
            if (tabPages.SelectedIndex == 0)
                tabPages.SelectedIndex = 1;
            Event.CurPageNum = tabPages.SelectedIndex;
            Event.EventEditorLoadPage(Event.CurPageNum);
        }

        private void BtnNewPage_Click(object sender, EventArgs e)
        {
            int pageCount;
            int i;

            if (chkGlobal.Checked == true)
            {
                Interaction.MsgBox("You cannot have multiple pages on global events!");
                return;
            }

            pageCount = Event.TmpEvent.PageCount + 1;

            // redim the array
            Array.Resize(ref Event.TmpEvent.Pages, pageCount + 1);

            Event.TmpEvent.PageCount = pageCount;

            // set the tabs
            tabPages.TabPages.Clear();

            var loopTo = Event.TmpEvent.PageCount;
            for (i = 0; i < loopTo; i++)
                tabPages.TabPages.Add(Conversion.Str(i));
            btnDeletePage.Enabled = true;
        }

        private void BtnCopyPage_Click(object sender, EventArgs e)
        {
            Event.CopyEventPage = Event.TmpEvent.Pages[Event.CurPageNum];
            btnPastePage.Enabled = true;
        }

        private void BtnPastePage_Click(object sender, EventArgs e)
        {
            Event.TmpEvent.Pages[Event.CurPageNum] = Event.CopyEventPage;
            Event.EventEditorLoadPage(Event.CurPageNum);
        }

        private void BtnDeletePage_Click(object sender, EventArgs e)
        {
            Event.TmpEvent.Pages[Event.CurPageNum] = default;

            // move everything else down a notch
            if (Event.CurPageNum < Event.TmpEvent.PageCount)
            {
                for (int i = Event.CurPageNum, loopTo = Event.TmpEvent.PageCount; i < loopTo; i++)
                    Event.TmpEvent.Pages[i] = Event.TmpEvent.Pages[i];
            }
            Event.TmpEvent.PageCount = Event.TmpEvent.PageCount - 1;
            Event.CurPageNum = Event.TmpEvent.PageCount;
            Event.EventEditorLoadPage(Event.CurPageNum);

            // set the tabs
            tabPages.TabPages.Clear();

            for (int i = 0, loopTo1 = Event.TmpEvent.PageCount; i < loopTo1; i++)
                tabPages.TabPages.Add("0", Conversion.Str(i), "");
            // set the tab back
            if (Event.CurPageNum <= Event.TmpEvent.PageCount)
            {
                tabPages.SelectedIndex = tabPages.TabPages.IndexOfKey(Event.CurPageNum.ToString());
            }
            else
            {
                tabPages.SelectedIndex = tabPages.TabPages.IndexOfKey(Event.TmpEvent.PageCount.ToString());
            }
            // make sure we disable
            if (Event.TmpEvent.PageCount <= 1)
            {
                btnDeletePage.Enabled = false;
            }

        }

        private void BtnClearPage_Click(object sender, EventArgs e)
        {
            Event.TmpEvent.Pages[Event.CurPageNum] = default;
        }

        private void TxtName_TextChanged(object sender, EventArgs e)
        {
            Event.TmpEvent.Name = Strings.Trim(txtName.Text);
        }

        #endregion

        #region Conditions

        private void ChkPlayerVar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlayerVar.Checked == true)
            {
                cmbPlayerVar.Enabled = true;
                nudPlayerVariable.Enabled = true;
                cmbPlayervarCompare.Enabled = true;
                Event.TmpEvent.Pages[Event.CurPageNum].ChkVariable = 1;
            }
            else
            {
                cmbPlayerVar.Enabled = false;
                nudPlayerVariable.Enabled = false;
                cmbPlayervarCompare.Enabled = false;
                Event.TmpEvent.Pages[Event.CurPageNum].ChkVariable = 0;
            }
        }

        private void CmbPlayerVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlayerVar.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].VariableIndex = cmbPlayerVar.SelectedIndex;
        }

        private void CmbPlayervarCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlayervarCompare.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].VariableCompare = cmbPlayervarCompare.SelectedIndex;
        }

        private void NudPlayerVariable_ValueChanged(object sender, EventArgs e)
        {
            Event.TmpEvent.Pages[Event.CurPageNum].VariableCondition = (int)Math.Round(nudPlayerVariable.Value);
        }

        private void ChkPlayerSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlayerSwitch.Checked == true)
            {
                cmbPlayerSwitch.Enabled = true;
                cmbPlayerSwitchCompare.Enabled = true;
                Event.TmpEvent.Pages[Event.CurPageNum].ChkSwitch = 1;
            }
            else
            {
                cmbPlayerSwitch.Enabled = false;
                cmbPlayerSwitchCompare.Enabled = false;
                Event.TmpEvent.Pages[Event.CurPageNum].ChkSwitch = 0;
            }
        }

        private void CmbPlayerSwitch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlayerSwitch.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].SwitchIndex = cmbPlayerSwitch.SelectedIndex;
        }

        private void CmbPlayerSwitchCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlayerSwitchCompare.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].SwitchCompare = cmbPlayerSwitchCompare.SelectedIndex;
        }

        private void ChkHasItem_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHasItem.Checked == true)
            {
                Event.TmpEvent.Pages[Event.CurPageNum].ChkHasItem = 1;
                cmbHasItem.Enabled = true;
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].ChkHasItem = 0;
                cmbHasItem.Enabled = false;
            }

        }

        private void CmbHasItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHasItem.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].HasItemIndex = cmbHasItem.SelectedIndex;
            Event.TmpEvent.Pages[Event.CurPageNum].HasItemAmount = (int)Math.Round(nudCondition_HasItem.Value);
        }

        private void ChkSelfSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelfSwitch.Checked == true)
            {
                cmbSelfSwitch.Enabled = true;
                cmbSelfSwitchCompare.Enabled = true;
                Event.TmpEvent.Pages[Event.CurPageNum].ChkSelfSwitch = 1;
            }
            else
            {
                cmbSelfSwitch.Enabled = false;
                cmbSelfSwitchCompare.Enabled = false;
                Event.TmpEvent.Pages[Event.CurPageNum].ChkSelfSwitch = 0;
            }
        }

        private void CmbSelfSwitch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelfSwitch.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].SelfSwitchIndex = cmbSelfSwitch.SelectedIndex;
        }

        private void CmbSelfSwitchCompare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelfSwitchCompare.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].SelfSwitchCompare = cmbSelfSwitchCompare.SelectedIndex;
        }

        #endregion

        #region Graphic

        private void PicGraphic_Click(object sender, EventArgs e)
        {
            fraGraphic.BringToFront();
            tmpGraphicIndex = Event.TmpEvent.Pages[Event.CurPageNum].Graphic;
            tmpGraphicType = Event.TmpEvent.Pages[Event.CurPageNum].GraphicType;
            fraGraphic.Visible = true;
            Event.GraphicSelType = 0;
        }

        private void CmbGraphic_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGraphic.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].GraphicType = (byte)cmbGraphic.SelectedIndex;
            // set the max on the scrollbar
            switch (cmbGraphic.SelectedIndex)
            {
                case 0: // None
                    {
                        nudGraphic.Enabled = false;
                        break;
                    }
                case 1: // character
                    {
                        nudGraphic.Maximum = GameState.NumCharacters;
                        nudGraphic.Enabled = true;
                        break;
                    }
                case 2: // Tileset
                    {
                        nudGraphic.Maximum = GameState.NumTileSets;
                        nudGraphic.Enabled = true;
                        break;
                    }
            }

            if (Event.TmpEvent.Pages[Event.CurPageNum].GraphicType == 1)
            {
                if (nudGraphic.Value <= 0m | nudGraphic.Value > GameState.NumCharacters)
                    return;
            }

            else if (Event.TmpEvent.Pages[Event.CurPageNum].GraphicType == 2)
            {
                if (nudGraphic.Value <= 0m | nudGraphic.Value > GameState.NumTileSets)
                    return;

            }
            DrawGraphic();
        }

        private void NudGraphic_ValueChanged(object sender, EventArgs e)
        {
            DrawGraphic();
        }

        private void PicGraphicSel_Click(object sender, MouseEventArgs e)
        {
            int X;
            int Y;

            X = e.Location.X;
            Y = e.Location.Y;

            int selW = (int)Math.Round(Math.Ceiling((decimal)(X / GameState.PicX)) - Event.GraphicSelX);
            int selH = (int)Math.Round(Math.Ceiling((decimal)(Y / GameState.PicY)) - Event.GraphicSelY);

            if (cmbGraphic.SelectedIndex == 2)
            {
                if (ModifierKeys == Keys.Shift)
                {
                    if (Event.GraphicSelX > -1 & Event.GraphicSelY > -1)
                    {
                        if (selW >= 0 & selH >= 0)
                        {
                            Event.GraphicSelX2 = selW + 1;
                            Event.GraphicSelY2 = selH + 1;
                        }
                    }
                }
                else
                {
                    Event.GraphicSelX = (int)Math.Round(Math.Ceiling((decimal)(X / GameState.PicX)));
                    Event.GraphicSelY = (int)Math.Round(Math.Ceiling((decimal)(Y / GameState.PicY)));
                    Event.GraphicSelX2 = 1;
                    Event.GraphicSelY2 = 1;
                }
            }
            else if (cmbGraphic.SelectedIndex == 1)
            {
                Event.GraphicSelX = X;
                Event.GraphicSelY = Y;
                Event.GraphicSelX2 = 0;
                Event.GraphicSelY2 = 0;
                if (nudGraphic.Value <= 0m | nudGraphic.Value > GameState.NumCharacters)
                    return;
                for (int i = 0; i <= 3; i++)
                {
                    if (Event.GraphicSelX >= GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, nudGraphic.Value.ToString())).Width / 4d * i & Event.GraphicSelX < GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, nudGraphic.Value.ToString())).Width / 4d * (i + 1))
                    {
                        Event.GraphicSelX = i;
                    }
                }
                for (int i = 0; i <= 3; i++)
                {
                    if (Event.GraphicSelY >= GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, nudGraphic.Value.ToString())).Height / 4d * i & Event.GraphicSelY < GameClient.GetGfxInfo(System.IO.Path.Combine(Core.Path.Characters, nudGraphic.Value.ToString())).Height / 4d * (i + 1))
                    {
                        Event.GraphicSelY = i;
                    }
                }
            }
            DrawGraphic();
        }

        #endregion

        #region Movement

        private void CmbMoveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMoveType.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].MoveType = (byte)cmbMoveType.SelectedIndex;
            if (cmbMoveType.SelectedIndex == 2)
            {
                btnMoveRoute.Enabled = true;
            }
            else
            {
                btnMoveRoute.Enabled = false;
            }
        }

        private void BtnMoveRoute_Click(object sender, EventArgs e)
        {
            fraMoveRoute.BringToFront();
            lstMoveRoute.Items.Clear();
            Event.IsMoveRouteCommand = false;
            chkIgnoreMove.Checked = Conversions.ToBoolean(Event.TmpEvent.Pages[Event.CurPageNum].IgnoreMoveRoute);
            chkRepeatRoute.Checked = Conversions.ToBoolean(Event.TmpEvent.Pages[Event.CurPageNum].RepeatMoveRoute);
            Event.TempMoveRouteCount = Event.TmpEvent.Pages[Event.CurPageNum].MoveRouteCount;

            // Will it let me do this?
            Event.TempMoveRoute = Event.TmpEvent.Pages[Event.CurPageNum].MoveRoute;
            for (int i = 0, loopTo = Event.TempMoveRouteCount; i < loopTo; i++)
            {
                switch (Event.TempMoveRoute[i].Index)
                {
                    case 1:
                        {
                            lstMoveRoute.Items.Add("Move Up");
                            break;
                        }
                    case 2:
                        {
                            lstMoveRoute.Items.Add("Move Down");
                            break;
                        }
                    case 3:
                        {
                            lstMoveRoute.Items.Add("Move Left");
                            break;
                        }
                    case 4:
                        {
                            lstMoveRoute.Items.Add("Move Right");
                            break;
                        }
                    case 5:
                        {
                            lstMoveRoute.Items.Add("Move Randomly");
                            break;
                        }
                    case 6:
                        {
                            lstMoveRoute.Items.Add("Move Towards Player");
                            break;
                        }
                    case 7:
                        {
                            lstMoveRoute.Items.Add("Move Away From Player");
                            break;
                        }
                    case 8:
                        {
                            lstMoveRoute.Items.Add("Step Forward");
                            break;
                        }
                    case 9:
                        {
                            lstMoveRoute.Items.Add("Step Back");
                            break;
                        }
                    case 10:
                        {
                            lstMoveRoute.Items.Add("Wait 100ms");
                            break;
                        }
                    case 11:
                        {
                            lstMoveRoute.Items.Add("Wait 500ms");
                            break;
                        }
                    case 12:
                        {
                            lstMoveRoute.Items.Add("Wait 1000ms");
                            break;
                        }
                    case 13:
                        {
                            lstMoveRoute.Items.Add("Turn Up");
                            break;
                        }
                    case 14:
                        {
                            lstMoveRoute.Items.Add("Turn Down");
                            break;
                        }
                    case 15:
                        {
                            lstMoveRoute.Items.Add("Turn Left");
                            break;
                        }
                    case 16:
                        {
                            lstMoveRoute.Items.Add("Turn Right");
                            break;
                        }
                    case 17:
                        {
                            lstMoveRoute.Items.Add("Turn 90 Degrees To the Right");
                            break;
                        }
                    case 18:
                        {
                            lstMoveRoute.Items.Add("Turn 90 Degrees To the Left");
                            break;
                        }
                    case 19:
                        {
                            lstMoveRoute.Items.Add("Turn Around 180 Degrees");
                            break;
                        }
                    case 20:
                        {
                            lstMoveRoute.Items.Add("Turn Randomly");
                            break;
                        }
                    case 21:
                        {
                            lstMoveRoute.Items.Add("Turn Towards Player");
                            break;
                        }
                    case 22:
                        {
                            lstMoveRoute.Items.Add("Turn Away from Player");
                            break;
                        }
                    case 23:
                        {
                            lstMoveRoute.Items.Add("Set Speed 8x Slower");
                            break;
                        }
                    case 24:
                        {
                            lstMoveRoute.Items.Add("Set Speed 4x Slower");
                            break;
                        }
                    case 25:
                        {
                            lstMoveRoute.Items.Add("Set Speed 2x Slower");
                            break;
                        }
                    case 26:
                        {
                            lstMoveRoute.Items.Add("Set Speed to Normal");
                            break;
                        }
                    case 27:
                        {
                            lstMoveRoute.Items.Add("Set Speed 2x Faster");
                            break;
                        }
                    case 28:
                        {
                            lstMoveRoute.Items.Add("Set Speed 4x Faster");
                            break;
                        }
                    case 29:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Lowest");
                            break;
                        }
                    case 30:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Lower");
                            break;
                        }
                    case 31:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Normal");
                            break;
                        }
                    case 32:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Higher");
                            break;
                        }
                    case 33:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Highest");
                            break;
                        }
                    case 34:
                        {
                            lstMoveRoute.Items.Add("Turn On Walking Animation");
                            break;
                        }
                    case 35:
                        {
                            lstMoveRoute.Items.Add("Turn Off Walking Animation");
                            break;
                        }
                    case 36:
                        {
                            lstMoveRoute.Items.Add("Turn On Fixed Direction");
                            break;
                        }
                    case 37:
                        {
                            lstMoveRoute.Items.Add("Turn Off Fixed Direction");
                            break;
                        }
                    case 38:
                        {
                            lstMoveRoute.Items.Add("Turn On Walk Through");
                            break;
                        }
                    case 39:
                        {
                            lstMoveRoute.Items.Add("Turn Off Walk Through");
                            break;
                        }
                    case 40:
                        {
                            lstMoveRoute.Items.Add("Set Position Below Player");
                            break;
                        }
                    case 41:
                        {
                            lstMoveRoute.Items.Add("Set Position at Player Level");
                            break;
                        }
                    case 42:
                        {
                            lstMoveRoute.Items.Add("Set Position Above Player");
                            break;
                        }
                    case 43:
                        {
                            lstMoveRoute.Items.Add("Set Graphic");
                            break;
                        }
                }
            }

            fraMoveRoute.Visible = true;

        }

        private void CmbMoveSpeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMoveSpeed.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].MoveSpeed = (byte)cmbMoveSpeed.SelectedIndex;
        }

        private void CmbMoveFreq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMoveFreq.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].MoveFreq = (byte)cmbMoveFreq.SelectedIndex;
        }

        #endregion

        #region Positioning

        private void CmbPositioning_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPositioning.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].Position = (byte)cmbPositioning.SelectedIndex;
        }

        #endregion

        #region Trigger

        private void CmbTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTrigger.SelectedIndex == -1)
                return;
            Event.TmpEvent.Pages[Event.CurPageNum].Trigger = (byte)cmbTrigger.SelectedIndex;
        }

        private void ChkGlobal_CheckedChanged(object sender, EventArgs e)
        {
            if (Event.TmpEvent.PageCount > 1)
            {
                if (MessageBox.Show("If you set the event to global you will lose all pages except for your first one. Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
            if (chkGlobal.Checked == true)
            {
                Event.TmpEvent.Globals = 1;
            }
            else
            {
                Event.TmpEvent.Globals = 0;
            }

            Event.TmpEvent.PageCount = 1;
            Event.CurPageNum = 1;
            tabPages.TabPages.Clear();

            for (int i = 0, loopTo = Event.TmpEvent.PageCount; i < loopTo; i++)
                tabPages.TabPages.Add("0", i.ToString(), "0");
            Event.EventEditorLoadPage(Event.CurPageNum);
        }

        #endregion

        #region Options

        private void ChkWalkAnim_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWalkAnim.Checked == true)
            {
                Event.TmpEvent.Pages[Event.CurPageNum].WalkAnim = 1;
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].WalkAnim = 0;
            }

        }

        private void ChkDirFix_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDirFix.Checked == true)
            {
                Event.TmpEvent.Pages[Event.CurPageNum].DirFix = 1;
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].DirFix = 0;
            }

        }

        private void ChkWalkThrough_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWalkThrough.Checked == true)
            {
                Event.TmpEvent.Pages[Event.CurPageNum].WalkThrough = 1;
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].WalkThrough = 0;
            }

        }

        private void ChkShowName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowName.Checked == true)
            {
                Event.TmpEvent.Pages[Event.CurPageNum].ShowName = 1;
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].ShowName = 0;
            }

        }

        #endregion

        #region Commands

        private void LstCommands_SelectedIndexChanged(object sender, EventArgs e)
        {
            Event.CurCommand = lstCommands.SelectedIndex;
        }

        private void BtnAddCommand_Click(object sender, EventArgs e)
        {
            if (lstCommands.SelectedIndex > -1)
            {
                Event.IsEdit = false;
                // tabPages.SelectedTab = TabPage
                fraCommands.Visible = true;
            }
        }

        private void BtnEditCommand_Click(object sender, EventArgs e)
        {
            Event.EditEventCommand();
        }

        private void BtnDeleteComand_Click(object sender, EventArgs e)
        {
            Event.DeleteEventCommand();
        }

        private void BtnClearCommand_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all event commands?", "Clear Event Commands?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Event.ClearEventCommands();
            }
        }

        #endregion

        #region Variables/Switches

        // 'Renaming Variables/Switches
        private void BtnLabeling_Click(object sender, EventArgs e)
        {
            pnlVariableSwitches.Visible = true;
            pnlVariableSwitches.BringToFront();
            pnlVariableSwitches.Top = 0;
            pnlVariableSwitches.Left = 0;
            pnlVariableSwitches.Width = Width;
            pnlVariableSwitches.Height = Height;
            lstSwitches.Items.Clear();

            for (int i = 0; i < Constant.MAX_SWITCHES; i++)
                lstSwitches.Items.Add(i.ToString() + ". " + Strings.Trim(Event.Switches[i]));
            lstVariables.Items.Clear();

            for (int i = 0; i < Constant.NAX_VARIABLES; i++)
                lstVariables.Items.Add(i.ToString() + ". " + Strings.Trim(Event.Variables[i]));

        }

        private void BtnRename_Ok_Click(object sender, EventArgs e)
        {
            FraRenaming.Visible = false;
            fraLabeling.Visible = true;

            switch (Event.RenameType)
            {
                case 1:
                    {
                        // Variable
                        if (Event.RenameIndex > 0 & Event.RenameIndex < Constant.NAX_VARIABLES + 1)
                        {
                            Event.Variables[Event.RenameIndex] = txtRename.Text;
                            FraRenaming.Visible = false;
                            fraLabeling.Visible = true;
                            Event.RenameType = 0;
                            Event.RenameIndex = 0;
                        }

                        break;
                    }
                case 2:
                    {
                        // Switch
                        if (Event.RenameIndex > 0 & Event.RenameIndex < Constant.MAX_SWITCHES + 1)
                        {
                            Event.Switches[Event.RenameIndex] = txtRename.Text;
                            FraRenaming.Visible = false;
                            fraLabeling.Visible = true;
                            Event.RenameType = 0;
                            Event.RenameIndex = 0;
                        }

                        break;
                    }
            }
            lstSwitches.Items.Clear();
            for (int i = 0; i < Constant.MAX_SWITCHES; i++)
                lstSwitches.Items.Add(i.ToString() + ". " + Strings.Trim(Event.Switches[i]));
            lstSwitches.SelectedIndex = 0;
            lstVariables.Items.Clear();

            for (int i = 0; i < Constant.NAX_VARIABLES; i++)
                lstVariables.Items.Add(i.ToString() + ". " + Strings.Trim(Event.Variables[i]));
            lstVariables.SelectedIndex = 0;
        }

        private void BtnRename_Cancel_Click(object sender, EventArgs e)
        {
            FraRenaming.Visible = false;
            fraLabeling.Visible = true;

            Event.RenameType = 0;
            Event.RenameIndex = 0;
            lstSwitches.Items.Clear();

            for (int i = 0; i < Constant.MAX_SWITCHES; i++)
                lstSwitches.Items.Add(i.ToString() + ". " + Strings.Trim(Event.Switches[i]));
            lstSwitches.SelectedIndex = 0;
            lstVariables.Items.Clear();

            for (int i = 0; i < Constant.NAX_VARIABLES; i++)
                lstVariables.Items.Add(i.ToString() + ". " + Strings.Trim(Event.Variables[i]));
            lstVariables.SelectedIndex = 0;
        }

        private void TxtRename_TextChanged(object sender, EventArgs e)
        {
            Event.TmpEvent.Name = Strings.Trim(txtName.Text);
        }

        private void lstVariables_Click(object sender, EventArgs e)
        {
            if (lstVariables.SelectedIndex == 0)
                lstVariables.SelectedIndex = 1;
        }

        private void LstVariables_DoubleClick(object sender, EventArgs e)
        {
            if (lstVariables.SelectedIndex > -1 & lstVariables.SelectedIndex < Constant.NAX_VARIABLES)
            {
                FraRenaming.Visible = true;
                fraLabeling.Visible = false;
                lblEditing.Text = "Editing Variable #" + lstVariables.SelectedIndex.ToString();
                txtRename.Text = Event.Variables[lstVariables.SelectedIndex];
                Event.RenameType = 1;
                Event.RenameIndex = lstVariables.SelectedIndex;
            }
        }

        private void lstSwitches_Click(object sender, EventArgs e)
        {
            if (lstSwitches.SelectedIndex == 0)
                lstSwitches.SelectedIndex = 1;
        }

        private void LstSwitches_DoubleClick(object sender, EventArgs e)
        {
            if (lstSwitches.SelectedIndex > -1 & lstSwitches.SelectedIndex < Constant.MAX_SWITCHES)
            {
                FraRenaming.Visible = true;
                fraLabeling.Visible = false;
                lblEditing.Text = "Editing Switch #" + lstSwitches.SelectedIndex.ToString();
                txtRename.Text = Event.Switches[lstSwitches.SelectedIndex];
                Event.RenameType = 2;
                Event.RenameIndex = lstSwitches.SelectedIndex;
            }
        }

        private void BtnRenameVariable_Click(object sender, EventArgs e)
        {
            if (lstVariables.SelectedIndex > -1 & lstVariables.SelectedIndex < Constant.NAX_VARIABLES)
            {
                FraRenaming.Visible = true;
                fraLabeling.Visible = false;
                lblEditing.Text = "Editing Variable #" + lstVariables.SelectedIndex.ToString();
                txtRename.Text = Event.Variables[lstVariables.SelectedIndex];
                Event.RenameType = 1;
                Event.RenameIndex = lstVariables.SelectedIndex;
            }
        }

        private void BtnRenameSwitch_Click(object sender, EventArgs e)
        {
            if (lstSwitches.SelectedIndex > -1 & lstSwitches.SelectedIndex < Constant.MAX_SWITCHES)
            {
                FraRenaming.Visible = true;
                lblEditing.Text = "Editing Switch #" + lstSwitches.SelectedIndex.ToString();
                txtRename.Text = Event.Switches[lstSwitches.SelectedIndex];
                Event.RenameType = 2;
                Event.RenameIndex = lstSwitches.SelectedIndex;
            }
        }

        private void BtnLabel_Ok_Click(object sender, EventArgs e)
        {
            pnlVariableSwitches.Visible = false;
            Event.SendSwitchesAndVariables();
        }

        private void BtnLabel_Cancel_Click(object sender, EventArgs e)
        {
            pnlVariableSwitches.Visible = false;
            Event.RequestSwitchesAndVariables();
        }

        #endregion

        #region Move Route

        // MoveRoute Commands
        private void LstvwMoveRoute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstvwMoveRoute.SelectedItems.Count == 0)
                return;

            switch (lstvwMoveRoute.SelectedItems[0].Index + 1)
            {
                // Set Graphic
                case 43:
                    {
                        fraGraphic.BringToFront();
                        Event.GraphicSelType = 1;
                        break;
                    }

                default:
                    {
                        AddMoveRouteCommand(lstvwMoveRoute.SelectedItems[0].Index);
                        break;
                    }
            }
        }

        private void LstMoveRoute_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // remove move route command lol
                if (lstMoveRoute.SelectedIndex > -1)
                {
                    RemoveMoveRouteCommand(lstMoveRoute.SelectedIndex);
                }
            }
        }

        public void AddMoveRouteCommand(int Index)
        {
            int i;
            int X;

            Index = Index + 1;
            if (lstMoveRoute.SelectedIndex > -1)
            {
                i = lstMoveRoute.SelectedIndex;
                Event.TempMoveRouteCount = Event.TempMoveRouteCount;
                Array.Resize(ref Event.TempMoveRoute, Event.TempMoveRouteCount);
                var loopTo = i;
                for (X = Event.TempMoveRouteCount; X > loopTo; X -= 1)
                    Event.TempMoveRoute[X + 1] = Event.TempMoveRoute[X];
                Event.TempMoveRoute[i].Index = Index;
                // if set graphic then...
                if (Index == 43)
                {
                    Event.TempMoveRoute[i].Data1 = cmbGraphic.SelectedIndex;
                    Event.TempMoveRoute[i].Data2 = (int)Math.Round(nudGraphic.Value);
                    Event.TempMoveRoute[i].Data3 = Event.GraphicSelX;
                    Event.TempMoveRoute[i].Data4 = Event.GraphicSelX2;
                    Event.TempMoveRoute[i].Data5 = Event.GraphicSelY;
                    Event.TempMoveRoute[i].Data6 = Event.GraphicSelY2;
                }
                PopulateMoveRouteList();
            }
            else
            {
                Event.TempMoveRouteCount = Event.TempMoveRouteCount + 1;
                Array.Resize(ref Event.TempMoveRoute, Event.TempMoveRouteCount);
                Event.TempMoveRoute[Event.TempMoveRouteCount].Index = Index;
                PopulateMoveRouteList();
                // if set graphic then....
                if (Index == 43)
                {
                    Event.TempMoveRoute[Event.TempMoveRouteCount].Data1 = cmbGraphic.SelectedIndex;
                    Event.TempMoveRoute[Event.TempMoveRouteCount].Data2 = (int)Math.Round(nudGraphic.Value);
                    Event.TempMoveRoute[Event.TempMoveRouteCount].Data3 = Event.GraphicSelX;
                    Event.TempMoveRoute[Event.TempMoveRouteCount].Data4 = Event.GraphicSelX2;
                    Event.TempMoveRoute[Event.TempMoveRouteCount].Data5 = Event.GraphicSelY;
                    Event.TempMoveRoute[Event.TempMoveRouteCount].Data6 = Event.GraphicSelY2;
                }
            }

        }

        public void RemoveMoveRouteCommand(int Index)
        {
            int i;

            Index = Index + 1;
            if (Index > 0 & Index <= Event.TempMoveRouteCount)
            {
                var loopTo = Event.TempMoveRouteCount;
                for (i = Index + 1; i < loopTo; i++)
                    Event.TempMoveRoute[i - 1] = Event.TempMoveRoute[i];
                Event.TempMoveRouteCount = Event.TempMoveRouteCount - 1;
                if (Event.TempMoveRouteCount == 0)
                {
                    Event.TempMoveRoute = new Core.Type.MoveRouteStruct[1];
                }
                else
                {
                    Array.Resize(ref Event.TempMoveRoute, Event.TempMoveRouteCount);
                }
                PopulateMoveRouteList();
            }

        }

        public void PopulateMoveRouteList()
        {
            int i;

            lstMoveRoute.Items.Clear();

            var loopTo = Event.TempMoveRouteCount;
            for (i = 0; i < loopTo; i++)
            {
                switch (Event.TempMoveRoute[i].Index)
                {
                    case 1:
                        {
                            lstMoveRoute.Items.Add("Move Up");
                            break;
                        }
                    case 2:
                        {
                            lstMoveRoute.Items.Add("Move Down");
                            break;
                        }
                    case 3:
                        {
                            lstMoveRoute.Items.Add("Move Left");
                            break;
                        }
                    case 4:
                        {
                            lstMoveRoute.Items.Add("Move Right");
                            break;
                        }
                    case 5:
                        {
                            lstMoveRoute.Items.Add("Move Randomly");
                            break;
                        }
                    case 6:
                        {
                            lstMoveRoute.Items.Add("Move Towards Player");
                            break;
                        }
                    case 7:
                        {
                            lstMoveRoute.Items.Add("Move Away From Player");
                            break;
                        }
                    case 8:
                        {
                            lstMoveRoute.Items.Add("Step Forward");
                            break;
                        }
                    case 9:
                        {
                            lstMoveRoute.Items.Add("Step Back");
                            break;
                        }
                    case 10:
                        {
                            lstMoveRoute.Items.Add("Wait 100ms");
                            break;
                        }
                    case 11:
                        {
                            lstMoveRoute.Items.Add("Wait 500ms");
                            break;
                        }
                    case 12:
                        {
                            lstMoveRoute.Items.Add("Wait 1000ms");
                            break;
                        }
                    case 13:
                        {
                            lstMoveRoute.Items.Add("Turn Up");
                            break;
                        }
                    case 14:
                        {
                            lstMoveRoute.Items.Add("Turn Down");
                            break;
                        }
                    case 15:
                        {
                            lstMoveRoute.Items.Add("Turn Left");
                            break;
                        }
                    case 16:
                        {
                            lstMoveRoute.Items.Add("Turn Right");
                            break;
                        }
                    case 17:
                        {
                            lstMoveRoute.Items.Add("Turn 90 Degrees To the Right");
                            break;
                        }
                    case 18:
                        {
                            lstMoveRoute.Items.Add("Turn 90 Degrees To the Left");
                            break;
                        }
                    case 19:
                        {
                            lstMoveRoute.Items.Add("Turn Around 180 Degrees");
                            break;
                        }
                    case 20:
                        {
                            lstMoveRoute.Items.Add("Turn Randomly");
                            break;
                        }
                    case 21:
                        {
                            lstMoveRoute.Items.Add("Turn Towards Player");
                            break;
                        }
                    case 22:
                        {
                            lstMoveRoute.Items.Add("Turn Away from Player");
                            break;
                        }
                    case 23:
                        {
                            lstMoveRoute.Items.Add("Set Speed 8x Slower");
                            break;
                        }
                    case 24:
                        {
                            lstMoveRoute.Items.Add("Set Speed 4x Slower");
                            break;
                        }
                    case 25:
                        {
                            lstMoveRoute.Items.Add("Set Speed 2x Slower");
                            break;
                        }
                    case 26:
                        {
                            lstMoveRoute.Items.Add("Set Speed to Normal");
                            break;
                        }
                    case 27:
                        {
                            lstMoveRoute.Items.Add("Set Speed 2x Faster");
                            break;
                        }
                    case 28:
                        {
                            lstMoveRoute.Items.Add("Set Speed 4x Faster");
                            break;
                        }
                    case 29:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Lowest");
                            break;
                        }
                    case 30:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Lower");
                            break;
                        }
                    case 31:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Normal");
                            break;
                        }
                    case 32:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Higher");
                            break;
                        }
                    case 33:
                        {
                            lstMoveRoute.Items.Add("Set Frequency Highest");
                            break;
                        }
                    case 34:
                        {
                            lstMoveRoute.Items.Add("Turn On Walking Animation");
                            break;
                        }
                    case 35:
                        {
                            lstMoveRoute.Items.Add("Turn Off Walking Animation");
                            break;
                        }
                    case 36:
                        {
                            lstMoveRoute.Items.Add("Turn On Fixed Direction");
                            break;
                        }
                    case 37:
                        {
                            lstMoveRoute.Items.Add("Turn Off Fixed Direction");
                            break;
                        }
                    case 38:
                        {
                            lstMoveRoute.Items.Add("Turn On Walk Through");
                            break;
                        }
                    case 39:
                        {
                            lstMoveRoute.Items.Add("Turn Off Walk Through");
                            break;
                        }
                    case 40:
                        {
                            lstMoveRoute.Items.Add("Set Position Below Player");
                            break;
                        }
                    case 41:
                        {
                            lstMoveRoute.Items.Add("Set Position at Player Level");
                            break;
                        }
                    case 42:
                        {
                            lstMoveRoute.Items.Add("Set Position Above Player");
                            break;
                        }
                    case 43:
                        {
                            lstMoveRoute.Items.Add("Set Graphic");
                            break;
                        }
                }
            }

        }

        private void ChkIgnoreMove_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIgnoreMove.Checked == true)
            {
                Event.TmpEvent.Pages[Event.CurPageNum].IgnoreMoveRoute = 1;
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].IgnoreMoveRoute = 0;
            }
        }

        private void ChkRepeatRoute_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRepeatRoute.Checked == true)
            {
                Event.TmpEvent.Pages[Event.CurPageNum].RepeatMoveRoute = 1;
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].RepeatMoveRoute = 0;
            }
        }

        private void BtnMoveRouteOk_Click(object sender, EventArgs e)
        {
            if (Event.IsMoveRouteCommand == true)
            {
                if (!Event.IsEdit)
                {
                    Event.AddCommand((int)Core.Enum.EventType.SetMoveRoute);
                }
                else
                {
                    Event.EditCommand();
                }
                Event.TempMoveRouteCount = 0;
                Event.TempMoveRoute = new Core.Type.MoveRouteStruct[1];
                fraMoveRoute.Visible = false;
            }
            else
            {
                Event.TmpEvent.Pages[Event.CurPageNum].MoveRouteCount = Event.TempMoveRouteCount;
                Event.TmpEvent.Pages[Event.CurPageNum].MoveRoute = Event.TempMoveRoute;
                Event.TempMoveRouteCount = 0;
                Event.TempMoveRoute = new Core.Type.MoveRouteStruct[1];
                fraMoveRoute.Visible = false;
            }
        }

        private void BtnMoveRouteCancel_Click(object sender, EventArgs e)
        {
            Event.TempMoveRouteCount = 0;
            Event.TempMoveRoute = new Core.Type.MoveRouteStruct[1];
            fraMoveRoute.Visible = false;
        }

        #endregion

        #region CommandFrames

        #region Show Text

        private void BtnShowTextOk_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.ShowText);
            }
            else
            {
                Event.EditCommand();
            }

            // hide
            fraDialogue.Visible = false;
            fraShowText.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnShowTextCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraShowText.Visible = false;
        }

        #endregion

        #region Add Text

        private void BtnAddTextOk_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.AddText);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraAddText.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnAddTextCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraAddText.Visible = false;
        }

        #endregion

        #region Show Choices
        private void BtnShowChoicesOk_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.ShowChoices);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraShowChoices.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnShowChoicesCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraShowChoices.Visible = false;
        }

        #endregion

        #region Show Chatbubble

        private void CmbChatBubbleTargetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChatBubbleTargetType.SelectedIndex == (int)Core.Enum.TargetType.None)
            {
                cmbChatBubbleTarget.Visible = false;
            }
            else if (cmbChatBubbleTargetType.SelectedIndex == (int)Core.Enum.TargetType.Player)
            {
                cmbChatBubbleTarget.Visible = true;
                cmbChatBubbleTarget.Items.Clear();

                for (int i = 0; i < Constant.MAX_MAP_NPCS; i++)
                {
                    if (Core.Type.MyMap.NPC[i] < 0)
                    {
                        cmbChatBubbleTarget.Items.Add(i + ". ");
                    }
                    else
                    {
                        cmbChatBubbleTarget.Items.Add(i + 1 + ". " + Core.Type.NPC[Core.Type.MyMap.NPC[i]].Name);
                    }
                }
                cmbChatBubbleTarget.SelectedIndex = 0;
            }
            else if (cmbChatBubbleTargetType.SelectedIndex == (int)Core.Enum.TargetType.NPC)
            {
                cmbChatBubbleTarget.Visible = true;
                cmbChatBubbleTarget.Items.Clear();

                for (int i = 0, loopTo = Core.Type.MyMap.EventCount; i < loopTo; i++)
                    cmbChatBubbleTarget.Items.Add(i + 1 + ". " + Core.Type.MyMap.Event[i].Name);
                cmbChatBubbleTarget.SelectedIndex = 0;
            }

        }

        private void BtnShowChatBubbleOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.ShowChatBubble);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraShowChatBubble.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnShowChatBubbleCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraShowChatBubble.Visible = false;
        }

        #endregion

        #region Set Player Variable

        private void OptVariableAction0_CheckedChanged(object sender, EventArgs e)
        {
            if (optVariableAction0.Checked == true)
            {
                nudVariableData0.Enabled = true;
                nudVariableData0.Value = 0m;
                nudVariableData1.Enabled = false;
                nudVariableData1.Value = 0m;
                nudVariableData2.Enabled = false;
                nudVariableData2.Value = 0m;
                nudVariableData3.Enabled = false;
                nudVariableData3.Value = 0m;
                nudVariableData4.Enabled = false;
                nudVariableData4.Value = 0m;
            }
        }

        private void OptVariableAction1_CheckedChanged(object sender, EventArgs e)
        {
            if (optVariableAction1.Checked == true)
            {
                nudVariableData0.Enabled = false;
                nudVariableData0.Value = 0m;
                nudVariableData1.Enabled = true;
                nudVariableData1.Value = 0m;
                nudVariableData2.Enabled = false;
                nudVariableData2.Value = 0m;
                nudVariableData3.Enabled = false;
                nudVariableData3.Value = 0m;
                nudVariableData4.Enabled = false;
                nudVariableData4.Value = 0m;
            }
        }

        private void OptVariableAction2_CheckedChanged(object sender, EventArgs e)
        {
            if (optVariableAction2.Checked == true)
            {
                nudVariableData0.Enabled = false;
                nudVariableData0.Value = 0m;
                nudVariableData1.Enabled = false;
                nudVariableData1.Value = 0m;
                nudVariableData2.Enabled = true;
                nudVariableData2.Value = 0m;
                nudVariableData3.Enabled = false;
                nudVariableData3.Value = 0m;
                nudVariableData4.Enabled = false;
                nudVariableData4.Value = 0m;
            }
        }

        private void OptVariableAction3_CheckedChanged(object sender, EventArgs e)
        {
            if (optVariableAction2.Checked == true)
            {
                nudVariableData0.Enabled = false;
                nudVariableData0.Value = 0m;
                nudVariableData1.Enabled = false;
                nudVariableData1.Value = 0m;
                nudVariableData2.Enabled = false;
                nudVariableData2.Value = 0m;
                nudVariableData3.Enabled = true;
                nudVariableData3.Value = 0m;
                nudVariableData4.Enabled = true;
                nudVariableData4.Value = 0m;
            }
        }

        private void BtnPlayerVarOk_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.PlayerVar);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraPlayerVariable.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnPlayerVarCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraPlayerVariable.Visible = false;
        }

        #endregion

        #region Set Player Switch

        private void BtnSetPlayerSwitchOk_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.PlayerSwitch);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraPlayerSwitch.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnSetPlayerswitchCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraPlayerSwitch.Visible = false;
        }

        #endregion

        #region Set Self Switch

        private void BtnSelfswitchOk_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.SelfSwitch);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraSetSelfSwitch.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnSelfswitchCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraSetSelfSwitch.Visible = false;
        }

        #endregion

        #region Conditional Branch

        private void OptCondition_Index0_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition0.Checked)
                return;

            ClearConditionFrame();

            cmbCondition_PlayerVarIndex.Enabled = true;
            cmbCondition_PlayerVarCompare.Enabled = true;
            nudCondition_PlayerVarCondition.Enabled = true;
        }

        private void OptCondition1_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition1.Checked)
                return;

            ClearConditionFrame();

            cmbCondition_PlayerSwitch.Enabled = true;
            cmbCondtion_PlayerSwitchCondition.Enabled = true;
        }

        private void OptCondition2_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition2.Checked)
                return;

            ClearConditionFrame();

            cmbCondition_HasItem.Enabled = true;
            nudCondition_HasItem.Enabled = true;
        }

        private void OptCondition3_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition3.Checked)
                return;

            ClearConditionFrame();

            cmbCondition_JobIs.Enabled = true;
        }

        private void OptCondition4_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition4.Checked)
                return;

            cmbCondition_LearntSkill.Enabled = true;
        }

        private void OptCondition5_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition5.Checked)
                return;

            ClearConditionFrame();

            cmbCondition_LevelCompare.Enabled = true;
            nudCondition_LevelAmount.Enabled = true;
        }

        private void OptCondition6_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition6.Checked)
                return;

            ClearConditionFrame();

            cmbCondition_SelfSwitch.Enabled = true;
            cmbCondition_SelfSwitchCondition.Enabled = true;
        }

        private void OptCondition8_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition8.Checked)
                return;

            ClearConditionFrame();

            cmbCondition_Gender.Enabled = true;
        }

        private void OptCondition9_CheckedChanged(object sender, EventArgs e)
        {
            if (!optCondition9.Checked)
                return;

            ClearConditionFrame();

            cmbCondition_Time.Enabled = true;
        }

        private void BtnConditionalBranchOk_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.Condition);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraCommands.Visible = false;
            fraConditionalBranch.Visible = false;
        }

        private void BtnConditionalBranchCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraConditionalBranch.Visible = false;
        }

        #endregion

        #region Create Label

        private void BtnCreatelabelOk_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.Label);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraCreateLabel.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnCreateLabelCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraCreateLabel.Visible = false;
        }

        #endregion

        #region GoTo Label

        private void BtnGoToLabelOk_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.GoToLabel);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraGoToLabel.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnGoToLabelCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraGoToLabel.Visible = false;
        }

        #endregion

        #region Change Items

        private void BtnChangeItemsOk_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.ChangeItems);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraCommands.Visible = false;
            fraChangeItems.Visible = false;
        }

        private void BtnChangeItemsCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraChangeItems.Visible = false;
        }

        #endregion

        #region Change Level

        private void BtnChangeLevelOK_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.ChangeLevel);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraChangeLevel.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnChangeLevelCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraChangeLevel.Visible = false;
        }

        #endregion

        #region Change Skills

        private void BtnChangeSkillsOK_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.ChangeSkills);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraChangeSkills.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnChangeSkillsCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraChangeSkills.Visible = false;
        }

        #endregion

        #region Change Job

        private void BtnChangeJobOK_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.ChangeJob);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraChangeJob.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnChangeJobCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraChangeJob.Visible = false;
        }

        #endregion

        #region Change Sprite

        private void BtnChangeSpriteOK_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.ChangeSprite);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraChangeSprite.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnChangeSpriteCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraChangeSprite.Visible = false;
        }

        #endregion

        #region Change Gender

        private void BtnChangeGenderOK_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.ChangeSex);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraChangeGender.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnChangeGenderCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraChangeGender.Visible = false;
        }

        #endregion

        #region Change PK

        private void BtnChangePkOK_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.ChangePk);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraChangePK.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnChangePkCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraChangePK.Visible = false;
        }

        #endregion

        #region Give Exp

        private void BtnGiveExpOK_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.GiveExp);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraGiveExp.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnGiveExpCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraGiveExp.Visible = false;
        }

        #endregion

        #region Player Warp

        private void BtnPlayerWarpOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.WarpPlayer);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraPlayerWarp.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnPlayerWarpCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraPlayerWarp.Visible = false;
        }

        #endregion

        #region Route Completion

        private void BtnMoveWaitOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.WaitMovement);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraMoveRouteWait.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnMoveWaitCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraMoveRouteWait.Visible = false;
        }

        #endregion

        #region Spawn NPC

        private void BtnSpawnNPCOK_Click(object sender, EventArgs e)
        {
            if (Event.IsEdit == false)
            {
                Event.AddCommand((int)Core.Enum.EventType.SpawnNPC);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraSpawnNPC.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnSpawnNPCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraSpawnNPC.Visible = false;
        }

        #endregion

        #region Play Animation

        private void OptPlayAnimPlayer_CheckedChanged(object sender, EventArgs e)
        {
            lblPlayAnimX.Visible = false;
            lblPlayAnimY.Visible = false;
            nudPlayAnimTileX.Visible = false;
            nudPlayAnimTileY.Visible = false;
            cmbPlayAnimEvent.Visible = false;
        }

        private void OptPlayAnimEvent_CheckedChanged(object sender, EventArgs e)
        {
            lblPlayAnimX.Visible = false;
            lblPlayAnimY.Visible = false;
            nudPlayAnimTileX.Visible = false;
            nudPlayAnimTileY.Visible = false;
            cmbPlayAnimEvent.Visible = true;
        }

        private void OptPlayAnimTile_CheckedChanged(object sender, EventArgs e)
        {
            lblPlayAnimX.Visible = true;
            lblPlayAnimY.Visible = true;
            nudPlayAnimTileX.Visible = true;
            nudPlayAnimTileY.Visible = true;
            cmbPlayAnimEvent.Visible = false;
        }

        private void BtnPlayAnimationOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.PlayAnimation);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraPlayAnimation.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnPlayAnimationCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraPlayAnimation.Visible = false;
        }

        #endregion

        #region Set Fog

        private void BtnSetFogOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.SetFog);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraSetFog.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnSetFogCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraSetFog.Visible = false;
        }

        #endregion

        #region Set Weather

        private void BtnSetWeatherOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.SetWeather);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraSetWeather.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnSetWeatherCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraSetWeather.Visible = false;
        }

        #endregion

        #region Set Map Tint

        private void BtnMapTintOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.SetTint);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraMapTint.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnMapTintCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraMapTint.Visible = false;
        }

        #endregion

        #region Play BGM

        private void BtnPlayBgmOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.PlayBgm);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraPlayBGM.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnPlayBgmCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraPlayBGM.Visible = false;
        }

        #endregion

        #region Play Sound

        private void BtnPlaySoundOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.PlaySound);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraPlaySound.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnPlaySoundCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraPlaySound.Visible = false;
        }

        #endregion

        #region Wait

        private void BtnSetWaitOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.Wait);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraSetWait.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnSetWaitCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraSetWait.Visible = false;
        }

        #endregion

        #region Set Access

        private void BtnSetAccessOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.SetAccess);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraSetAccess.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnSetAccessCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraSetAccess.Visible = false;
        }

        #endregion

        #region Custom Script

        private void BtnCustomScriptOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.CustomScript);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraCustomScript.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnCustomScriptCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraCustomScript.Visible = false;
        }

        #endregion

        #region Show Pic

        private void BtnShowPicOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.ShowPicture);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraShowPic.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnShowPicCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraShowPic.Visible = false;
        }

        private void nudShowPicture_Click(object sender, EventArgs e)
        {
            DrawPicture();
        }

        private void DrawPicture()
        {
            int Sprite;

            Sprite = (int)Math.Round(nudShowPicture.Value);

            if (Sprite < 1 | Sprite > GameState.NumPictures)
            {
                picShowPic.BackgroundImage = null;
                return;
            }

            if (File.Exists(System.IO.Path.Combine(Core.Path.Pictures, Sprite + GameState.GfxExt)))
            {
                picShowPic.Width = Image.FromFile(System.IO.Path.Combine(Core.Path.Pictures, Sprite + GameState.GfxExt)).Width;
                picShowPic.Height = Image.FromFile(System.IO.Path.Combine(Core.Path.Pictures, Sprite + GameState.GfxExt)).Height;
                picShowPic.BackgroundImage = Image.FromFile(System.IO.Path.Combine(Core.Path.Pictures, Sprite + GameState.GfxExt));
            }
        }

        #endregion

        #region Open Shop

        private void BtnOpenShopOK_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
            {
                Event.AddCommand((int)Core.Enum.EventType.OpenShop);
            }
            else
            {
                Event.EditCommand();
            }
            // hide
            fraDialogue.Visible = false;
            fraOpenShop.Visible = false;
            fraCommands.Visible = false;
        }

        private void BtnOpenShopCancel_Click(object sender, EventArgs e)
        {
            if (!Event.IsEdit)
                fraCommands.Visible = true;
            else
                fraCommands.Visible = false;
            fraDialogue.Visible = false;
            fraOpenShop.Visible = false;
        }

        #endregion

        #endregion

    }
}