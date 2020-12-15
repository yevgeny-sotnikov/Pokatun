using System;
using Acr.UserDialogs;
using Android.App;
using Android.Runtime;
using MvvmCross.Platforms.Android.Views;
using Pokatun.Core;

namespace Pokatun.Droid
{
    #if DEBUG
    [Application(Debuggable = true)]
#else
    [Application(Debuggable = false)]
#endif
        public class MainApplication : MvxAndroidApplication<Setup, App>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            UserDialogs.Init(this);
        }
    }
}
