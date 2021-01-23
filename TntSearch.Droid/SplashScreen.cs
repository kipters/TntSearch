using Android.App;
using MvvmCross.Platforms.Android.Views;

namespace TntSearch.Droid
{
    [Activity(MainLauncher = true)]
    public class SplashScreen : MvxSplashScreenActivity<Setup, Core.App>
    {
        public SplashScreen() : base(Resource.Layout.splash_screen)
        {
        }
    }
}