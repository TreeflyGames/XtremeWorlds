using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Xna.Framework;
using Android.Views;
using Android.Content;
using Android.Runtime;

namespace Client.Android
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
        public static MainActivity Instance { get; private set; }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var game = Client.General.Client; // ✅ Correct: accesses the static instance
            SetContentView((View)game.Services.GetService(typeof(View)));
            game.Run();
        }
    }
}