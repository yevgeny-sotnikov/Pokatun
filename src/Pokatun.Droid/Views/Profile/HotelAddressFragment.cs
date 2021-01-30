using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Profile;

namespace Pokatun.Droid.Views.Profile
{
    [MvxFragmentPresentation(
        typeof(MainContainerViewModel),
        Resource.Id.content_frame,
        AddToBackStack = true,
        EnterAnimation = Android.Resource.Animation.FadeIn,
        PopEnterAnimation = Android.Resource.Animation.FadeIn,
        ExitAnimation = Android.Resource.Animation.FadeOut,
        PopExitAnimation = Android.Resource.Animation.FadeOut
    )]
    public class HotelAddressFragment : BaseFragment<HotelAddressViewModel>
    {
        private EditText _searchTextField;
        private MvxRecyclerView _foundResultsRecyclerView;

        protected override int FragmentLayoutId => Resource.Layout.fragment_hotel_address;

        protected override bool IsHeaderBackButtonVisible => false;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _searchTextField = view.FindViewById<EditText>(Resource.Id.searchTextField);
            _foundResultsRecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.foundResultsRecyclerView);

            _searchTextField.Hint = Strings.HotelLocationAddress;

            _foundResultsRecyclerView.ItemTemplateId = Resource.Layout.address_item_template;
            _foundResultsRecyclerView.SetLayoutManager(new LinearLayoutManager(Context));

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.CloseCommand).OneTime();
            set.Bind(_searchTextField).For(v => v.Text).To(vm => vm.SearchText).OneWayToSource();
            set.Bind(_foundResultsRecyclerView).For(v => v.ItemsSource).To(vm => vm.FoundAdresses).OneTime();
            set.Bind(_foundResultsRecyclerView).For(v => v.ItemClick).To(vm => vm.AddressSelectedCommand);

            set.Apply();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarRightButton.SetImageResource(Resource.Drawable.close);
            ToolbarRightButton.Visibility = ViewStates.Visible;
        }
    }
}
