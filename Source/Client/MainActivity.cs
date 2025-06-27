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
    public class MainActivity : AndroidGameActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView((View)General.Client.Services.GetService(typeof(View)));
            General.Client.Run();
        }
    }
}