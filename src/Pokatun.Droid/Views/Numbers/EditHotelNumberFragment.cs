using System;
using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Numbers;

namespace Pokatun.Droid.Views.Numbers
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
    public class EditHotelNumberFragment : BaseFragment<EditHotelNumberViewModel>
    {
        private EditText _roomNumberTextField;

        protected override int FragmentLayoutId => Resource.Layout.fragment_edit_hotel_number;

        protected override bool IsHeaderBackButtonVisible => false;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _roomNumberTextField = view.FindViewById<EditText>(Resource.Id.roomNumberTextField);

            _roomNumberTextField.Hint = Strings.RoomNumber;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.CloseCommand).OneTime();

            set.Bind(_roomNumberTextField).To(vm => vm.RoomNumber).TwoWay();

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
