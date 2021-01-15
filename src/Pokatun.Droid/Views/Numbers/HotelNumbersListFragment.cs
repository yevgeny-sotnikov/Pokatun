
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Numbers;

namespace Pokatun.Droid.Views.Numbers
{
    [MvxFragmentPresentation(
        typeof(MainContainerViewModel),
        Resource.Id.content_frame,
        AddToBackStack = true,
        EnterAnimation = Android.Resource.Animation.SlideInLeft,
        PopEnterAnimation = Android.Resource.Animation.SlideInLeft,
        ExitAnimation = Android.Resource.Animation.SlideOutRight,
        PopExitAnimation = Android.Resource.Animation.SlideOutRight
    )]
    public sealed class HotelNumbersListFragment : BaseFragment<HotelNumbersListViewModel>
    {
        private MvxRecyclerView _recyclerView;

        protected override int FragmentLayoutId => Resource.Layout.fragment_hotel_numbers_list;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _recyclerView = (MvxRecyclerView)base.OnCreateView(inflater, container, savedInstanceState);

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();
    
            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.AddCommand).OneTime();
            set.Bind(_recyclerView).For(v => v.ItemsSource).To(vm => vm.HotelNumbers).OneTime();

            set.Apply();

            return _recyclerView;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarRightButton.SetImageResource(Resource.Drawable.plus);

            ToolbarRightButton.Visibility = ViewStates.Visible;
        }
    }
}
