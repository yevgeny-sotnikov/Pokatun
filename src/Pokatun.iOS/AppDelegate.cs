using Foundation;
using MvvmCross.Platforms.Ios.Core;
using Pokatun.Core;

namespace Pokatun.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}
