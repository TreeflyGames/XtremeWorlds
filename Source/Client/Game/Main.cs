using System.Reflection.Metadata;
using static Core.Enum;
using Timer = System.Windows.Forms.Timer;

namespace Client
{

    public class Program
    {
        private static Timer updateFormsTimer;

        public static void Main()
        {
            // Set visual styles and text rendering default before any forms are created
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);

            var gameThread = new System.Threading.Thread(RunGame);
            gameThread.IsBackground = true;
            gameThread.Start();

            // Initialize and start the timer for updating forms
            updateFormsTimer = new Timer();
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
                if (GameState.InitEventEditorForm)
                {
                    {
                        var withBlock = frmEditor_Event.Instance;
                        withBlock.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock.Show();
                    }
                    GameState.InitEventEditorForm = false;
                }

                if (GameState.InitAdminForm)
                {
                    {
                        var withBlock1 = FrmAdmin.Instance;
                        withBlock1.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock1.Show();
                        withBlock1.txtAdminName.Text = Core.Global.Command.GetPlayerName(GameState.MyIndex);
                        GameState.AdminPanel = true;
                    }
                    GameState.InitAdminForm = false;
                }

                if (GameState.InitMapReport)
                {
                    for (int i = 1, loopTo = GameState.MapNames.Length; i < loopTo; i++)
                    {
                        var item1 = new ListViewItem((i).ToString());
                        item1.SubItems.Add(GameState.MapNames[i]);
                        FrmAdmin.Instance.lstMaps.Items.Add(item1);
                    }
                    GameState.InitMapReport = false;
                }

                if (GameState.InitMapEditor)
                {
                    {
                        var withBlock2 = frmEditor_Map.Instance;
                        GameState.MyEditorType = (int)EditorType.Map;
                        GameState.EditorIndex = 1;
                        withBlock2.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock2.Show();
                        frmEditor_Map.MapEditorInit();
                        General.SetWindowFocus(General.Client.Window.Handle);
                    }
                    GameState.InitMapEditor = false;
                }

                if (GameState.InitPetEditor)
                {
                    {
                        var withBlock3 = frmEditor_Pet.Instance;
                        GameState.MyEditorType = (int)EditorType.Pet;
                        GameState.EditorIndex = 1;
                        withBlock3.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock3.Show();
                        withBlock3.lstIndex.SelectedIndex = 0;
                        Editors.PetEditorInit();
                    }
                    GameState.InitPetEditor = false;
                }

                if (GameState.InitAnimationEditor)
                {
                    {
                        var withBlock4 = frmEditor_Animation.Instance;
                        GameState.MyEditorType = (int)EditorType.Animation;
                        GameState.EditorIndex = 1;
                        withBlock4.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock4.Show();
                        withBlock4.lstIndex.SelectedIndex = 0;
                        Editors.AnimationEditorInit();
                    }
                    GameState.InitAnimationEditor = false;
                }

                if (GameState.InitItemEditor)
                {
                    {
                        var withBlock5 = frmEditor_Item.Instance;
                        GameState.MyEditorType = (int)EditorType.Item;
                        GameState.EditorIndex = 1;
                        withBlock5.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock5.Show();
                        withBlock5.lstIndex.SelectedIndex = 0;
                        Editors.ItemEditorInit();
                    }
                    GameState.InitItemEditor = false;
                }

                if (GameState.InitJobEditor)
                {
                    {
                        var withBlock6 = frmEditor_Job.Instance;
                        GameState.MyEditorType = (int)EditorType.Job;
                        GameState.EditorIndex = 1;
                        withBlock6.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock6.Show();
                        withBlock6.lstIndex.SelectedIndex = 0;

                        Editors.JobEditorInit();
                    }
                    GameState.InitJobEditor = false;
                }

                if (GameState.InitMoralEditor)
                {
                    {
                        var withBlock7 = frmEditor_Moral.Instance;
                        GameState.MyEditorType = (int)EditorType.Moral;
                        GameState.EditorIndex = 1;
                        withBlock7.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock7.Show();
                        withBlock7.lstIndex.SelectedIndex = 0;
                        Editors.MoralEditorInit();
                    }
                    GameState.InitMoralEditor = false;
                }

                if (GameState.InitResourceEditor)
                {
                    {
                        var withBlock8 = frmEditor_Resource.Instance;
                        GameState.MyEditorType = (int)EditorType.Resource;
                        GameState.EditorIndex = 1;
                        withBlock8.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock8.Show();
                        withBlock8.lstIndex.SelectedIndex = 0;
                        Editors.ResourceEditorInit();
                    }
                    GameState.InitResourceEditor = false;
                }

                if (GameState.InitNPCEditor)
                {
                    {
                        var withBlock9 = frmEditor_NPC.Instance;
                        GameState.MyEditorType = (int)EditorType.NPC;
                        GameState.EditorIndex = 1;
                        withBlock9.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock9.Show();
                        withBlock9.lstIndex.SelectedIndex = 0;
                        Editors.NPCEditorInit();
                    }
                    GameState.InitNPCEditor = false;
                }

                if (GameState.InitSkillEditor)
                {
                    {
                        var withBlock10 = frmEditor_Skill.Instance;
                        GameState.MyEditorType = (int)EditorType.Skill;
                        GameState.EditorIndex = 1;
                        withBlock10.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock10.Show();
                        withBlock10.lstIndex.SelectedIndex = 0;
                        Editors.SkillEditorInit();
                    }
                    GameState.InitSkillEditor = false;
                }

                if (GameState.InitShopEditor)
                {
                    {
                        var withBlock11 = frmEditor_Shop.Instance;
                        GameState.MyEditorType = (int)EditorType.Shop;
                        GameState.EditorIndex = 1;
                        withBlock11.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock11.Show();
                        withBlock11.lstIndex.SelectedIndex = 0;
                        Editors.ShopEditorInit();
                    }
                    GameState.InitShopEditor = false;
                }

                if (GameState.InitProjectileEditor)
                {
                    {
                        var withBlock12 = frmEditor_Projectile.Instance;
                        GameState.MyEditorType = (int)EditorType.Projectile;
                        GameState.EditorIndex = 1;
                        withBlock12.Owner = (Form)Control.FromHandle(General.Client.Window?.Handle ?? IntPtr.Zero);
                        withBlock12.Show();
                        withBlock12.lstIndex.SelectedIndex = 0;
                        Editors.ProjectileEditorInit();
                    }

                    GameState.InitProjectileEditor = false;
                }

                frmEditor_Map.Instance.picBackSelect.Invalidate();
                frmEditor_Animation.Instance.picSprite0.Invalidate();
                frmEditor_Animation.Instance.picSprite1.Invalidate();

                Application.DoEvents();
            }

            catch (InvalidOperationException)
            {
                return;
            }
        }
    }
}