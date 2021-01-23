using Android.App;
using MvvmCross.Platforms.Android.Views;

namespace TntSearch.Droid.Views
{
    [Activity(Label = "InitView")]
    public class InitView : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.activity_main);
        }
    }
}