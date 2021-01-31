using MvvmCross.Platforms.Uap.Views;
using SQLitePCL;
using System;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;

namespace TntSearch.Uwp
{
    public abstract class AppBase : MvxApplication<Setup, Core.App> { }
    sealed partial class App : AppBase
    {
        public App()
        {
            InitializeComponent();
            
            Batteries_V2.Init();

#if DEBUG
            Console.SetOut(new DebugStreamWriter());
#endif
        }
    }
}
