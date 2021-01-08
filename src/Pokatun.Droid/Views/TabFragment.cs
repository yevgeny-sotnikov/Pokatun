using System;
using Android.Views;
using Android.Widget;
using Pokatun.Core.ViewModels;
using static Android.Widget.TabHost;

namespace Pokatun.Droid.Views
{
    public abstract class TabFragment<TViewModel> : BaseFragment<TViewModel> where TViewModel : BaseViewModel
    {
        protected TabHost TabHost { get; set; }

        protected TabSpec CreateTab(string tag, int contentId, int tabLayoutId, string title)
        {
            TabSpec tabSpec = TabHost.NewTabSpec(tag);
            tabSpec.SetContent(contentId);

            View view = LayoutInflater.Inflate(tabLayoutId, null);

            TextView tabLabel = view.FindViewById<TextView>(Resource.Id.tabLabel);
            tabLabel.Text = title;

            tabSpec.SetIndicator(view);

            return tabSpec;
        }

    }
}
