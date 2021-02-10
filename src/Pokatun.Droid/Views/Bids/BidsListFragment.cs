using Android.OS;
using Android.Views;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.ViewModels.Bids;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.Bids
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
    public sealed class BidsListFragment : BaseFragment<BidsListViewModel>
    {
        private MvxRecyclerView _recyclerView;

        protected override int FragmentLayoutId => Resource.Layout.fragment_bids_list;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _recyclerView = (MvxRecyclerView)base.OnCreateView(inflater, container, savedInstanceState);
            _recyclerView.ItemTemplateId = Resource.Layout.hotel_number_item_template;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            //set.Bind(ToolbarSubtitleLabel).To(vm => vm.Subtitle).OneWay();
            set.Bind(_recyclerView).For(v => v.ItemsSource).To(vm => vm.Bids).OneTime();
            set.Bind(_recyclerView).For(v => v.ItemClick).To(vm => vm.OpenBidCommand).OneTime();

            set.Apply();

            return _recyclerView;
        }

    }
}
