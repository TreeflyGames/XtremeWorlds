﻿using Client;
using System.Reflection.Metadata;

namespace Client
{

    public class Program
    {
        private static System.Windows.Forms.Timer updateFormsTimer;

        public static void Main()
        {
            // Set visual styles and text rendering default before any forms are created
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

            var gameThread = new System.Threading.Thread(RunGame);
            gameThread.IsBackground = false;
            gameThread.Start();

            // Initialize and start the timer for updating forms
            updateFormsTimer = new System.Windows.Forms.Timer();
            updateFormsTimer.Tick += UpdateForms;
            updateFormsTimer.Interval = 1000; // Adjust the interval as needed
            updateFormsTimer.Start();

            // Add a Windows Forms message loop to keep the application running
            Application.Run();
        }

        public static void RunGame()
        {
            General.Client.Run();
        }

        private static void UpdateForms(object sender, EventArgs e)
        {
            var mainForm = Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null;

            try
            {
                if (mainForm != null && !mainForm.IsDisposed && !mainForm.Disposing && mainForm.InvokeRequired)
                {
                    mainForm.Invoke(new EventHandler(UpdateForms), sender, e);
                    return;
                }
            }
            catch (System.ObjectDisposedException)
            {
                return;
            }

            try
            {
                if (GameState.InitEventEditor)
                {
                    var withBlock = frmEditor_Event.Instance;
                    withBlock.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock.Show();

                    GameState.InitEventEditor = false;
                }

                if (GameState.InitAdminForm)
                {
                    var withBlock1 = FrmAdmin.Instance;
                    withBlock1.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock1.Show();
                    withBlock1.txtAdminName.Text = Core.Global.Command.GetPlayerName(GameState.MyIndex);
                    GameState.AdminPanel = true;

                    GameState.InitAdminForm = false;
                }

                if (GameState.InitMapReport)
                {
                    for (int i = 1, loopTo = GameState.MapNames.Length; i < loopTo; i++)
                    {
                        var item1 = new DarkUI.Controls.DarkListItem(i.ToString());
                        // Set the Text property to include both the index and the map name
                        item1.Text = $"{i}: {GameState.MapNames[i]}";
                        FrmAdmin.Instance.lstMaps.Items.Add(item1);
                    }
                    GameState.InitMapReport = false;
                }

                if (GameState.InitMapEditor)
                {
                    var withBlock2 = frmEditor_Map.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Map;
                    GameState.EditorIndex = 1;
                    withBlock2.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock2.Show();
                    frmEditor_Map.MapEditorInit();
                    General.SetWindowFocus(General.Client.Window.Handle);

                    GameState.InitMapEditor = false;
                }    

                if (GameState.InitAnimationEditor)
                {
                    var withBlock4 = frmEditor_Animation.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Animation;
                    GameState.EditorIndex = 1;
                    withBlock4.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock4.Show();
                    withBlock4.lstIndex.SelectedIndex = 0;
                    Editors.AnimationEditorInit();

                    GameState.InitAnimationEditor = false;
                }

                if (GameState.InitItemEditor)
                {
                    var withBlock5 = frmEditor_Item.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Item;
                    GameState.EditorIndex = 1;
                    withBlock5.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock5.Show();
                    withBlock5.lstIndex.SelectedIndex = 0;
                    Editors.ItemEditorInit();

                    GameState.InitItemEditor = false;
                }

                if (GameState.InitJobEditor)
                {
                    var withBlock6 = frmEditor_Job.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Job;
                    GameState.EditorIndex = 1;
                    withBlock6.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock6.Show();
                    withBlock6.lstIndex.SelectedIndex = 0;
                    Editors.JobEditorInit();

                    GameState.InitJobEditor = false;
                }

                if (GameState.InitMoralEditor)
                {
                    var withBlock7 = frmEditor_Moral.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Moral;
                    GameState.EditorIndex = 1;
                    withBlock7.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock7.Show();
                    withBlock7.lstIndex.SelectedIndex = 0;
                    Editors.MoralEditorInit();

                    GameState.InitMoralEditor = false;
                }

                if (GameState.InitResourceEditor)
                {
                    var withBlock8 = frmEditor_Resource.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Resource;
                    GameState.EditorIndex = 1;
                    withBlock8.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock8.Show();
                    withBlock8.lstIndex.SelectedIndex = 0;
                    Editors.ResourceEditorInit();

                    GameState.InitResourceEditor = false;
                }

                if (GameState.InitNpcEditor)
                {
                    var withBlock9 = frmEditor_Npc.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Npc;
                    GameState.EditorIndex = 1;
                    withBlock9.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock9.Show();
                    withBlock9.lstIndex.SelectedIndex = 0;
                    Editors.NpcEditorInit();

                    GameState.InitNpcEditor = false;
                }

                if (GameState.InitSkillEditor)
                {
                    var withBlock10 = frmEditor_Skill.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Skill;
                    GameState.EditorIndex = 1;
                    withBlock10.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock10.Show();
                    withBlock10.lstIndex.SelectedIndex = 0;
                    Editors.SkillEditorInit();

                    GameState.InitSkillEditor = false;
                }

                if (GameState.InitShopEditor)
                {
                    var withBlock11 = frmEditor_Shop.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Shop;
                    GameState.EditorIndex = 1;
                    withBlock11.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock11.Show();
                    withBlock11.lstIndex.SelectedIndex = 0;
                    Editors.ShopEditorInit();

                    GameState.InitShopEditor = false;
                }

                if (GameState.InitProjectileEditor)
                {
                    var withBlock12 = frmEditor_Projectile.Instance;
                    GameState.MyEditorType = (int)Core.EditorType.Projectile;
                    GameState.EditorIndex = 1;
                    withBlock12.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock12.Show();
                    withBlock12.lstIndex.SelectedIndex = 0;
                    Editors.ProjectileEditorInit();

                    GameState.InitProjectileEditor = false;
                }

                if (GameState.InitScriptEditor)
                {
                    var withBlock12 = frmEditor_Script.Instance;
                    GameState.MyEditorType = (int)Core. EditorType.Script;
                    GameState.EditorIndex = 1;
                    withBlock12.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                    withBlock12.Show();
                    Script.ScriptEditorInit();

                    GameState.InitScriptEditor = false;

                }

                frmEditor_Map.Instance.picBackSelect.Invalidate();
                frmEditor_Animation.Instance.picSprite0.Invalidate();
                frmEditor_Animation.Instance.picSprite1.Invalidate();

                if (GameState.InGame == false)
                {
                    // Close all open editor forms, not just the last opened one
                    frmEditor_Item.Instance.Dispose();
                    frmEditor_Job.Instance.Dispose();
                    frmEditor_Map.Instance.Dispose();
                    frmEditor_Event.Instance.Dispose();
                    frmEditor_Npc.Instance.Dispose();
                    frmEditor_Projectile.Instance.Dispose();
                    frmEditor_Resource.Instance.Dispose();
                    frmEditor_Shop.Instance.Dispose();
                    frmEditor_Skill.Instance.Dispose();
                    frmEditor_Animation.Instance.Dispose();
                    frmEditor_Moral.Instance.Dispose();
                    frmEditor_Script.Instance.Dispose();

                    if (GameState.AdminPanel)
                    {
                        FrmAdmin.Instance.Dispose();
                    }

                    Application.DoEvents();
                }
            }

            catch (InvalidOperationException)
            {
                return;
            }
        }
    }
}