using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Xna.Framework;
using Android.Views;
using Android.Content;
using Android.Runtime;
using Client;

namespace Client
{
    [Activity(Label = "XtremeWorlds", 
        MainLauncher = true,
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/AppTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        Exported = true,
        ScreenOrientation = ScreenOrientation.Landscape)]
    public class MainActivity : Activity
    {
        public static MainActivity Instance { get; private set; }
        public static GameClient Client { get; private set; }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;
            var game = Client = new GameClient();
            while (game == null)
            {
                
            }
            SetContentView((View)game.Services.GetService(typeof(View)));
            game.Run();
        }
    }
}