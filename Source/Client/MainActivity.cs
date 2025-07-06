using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Xna.Framework;
using Android.Views;
using Android.Content;
using Android.Print;
using Android.Provider;
using Android.Runtime;
using Client;
using Core;
using Path = System.IO.Path;
using Point = Android.Graphics.Point;

namespace Client
{
    [Activity(Label = "XtremeWorlds", 
        MainLauncher = true,
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/AppTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        Exported = true,
        ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : AndroidGameActivity
    {
        public static GameClient? Client { get; private set; }
        public static MainActivity? Instance { get; private set; }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            Instance = this;
            
            // Get initial screen size
            GetDisplaySize(out int width, out int height);
            
            // Initialize the game client with the screen size
            Client = new GameClient(width, height);
            
            SettingsManager.Load();
            SettingsManager.Instance.Resolution = 1;  
            SettingsManager.Save();
            
            MoveAppSettings(this);
            
            SetContentView((View)Client.Services.GetService(typeof(View)));
            Client.Run();
        }
        
        private void GetDisplaySize(out int width, out int height)
        {
            var realSize = new Point();
            WindowManager.DefaultDisplay.GetRealSize(realSize);
            width = realSize.X;
            height = realSize.Y;
        }
        
        public void MoveAppSettings(Activity activity)
        {
            string fileName = "appsettings.json";
            string destPath = Path.Combine(activity.FilesDir.AbsolutePath, fileName);

            if (!File.Exists(destPath))
            {
                using var assetStream = activity.Assets.Open(fileName);
                using var fileStream = File.Create(destPath);
                assetStream.CopyTo(fileStream);
            }
        }
    }
}