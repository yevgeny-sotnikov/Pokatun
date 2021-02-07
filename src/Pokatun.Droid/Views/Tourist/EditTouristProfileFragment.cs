
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
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Tourist;

namespace Pokatun.Droid.Views.Tourist
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
    public sealed class EditTouristProfileFragment : BaseFragment<EditTouristProfileViewModel>
    {
        private EditText _emailTextField;
        private Button _saveChangesButton;
        private EditText _fullnameTextField;
        private EditText _phoneTextField;

        protected override int FragmentLayoutId => Resource.Layout.fragment_edit_tourist_profile;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _fullnameTextField = view.FindViewById<EditText>(Resource.Id.fullnameTextField);
            _phoneTextField = view.FindViewById<EditText>(Resource.Id.phoneTextField);
            _emailTextField = view.FindViewById<EditText>(Resource.Id.emailTextField);
            _saveChangesButton = view.FindViewById<Button>(Resource.Id.saveChangesButton);

            _emailTextField.Hint = Strings.Email;
            _phoneTextField.Hint = Strings.Phone;
            _fullnameTextField.Hint = Strings.FullName;
            _saveChangesButton.Text = Strings.SaveChanges;

#pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(ToolbarRightButton).For(ToolbarRightButton.BindClick()).To(vm => vm.CloseCommand).OneTime();
            set.Bind(ToolbarAddPhotoButton).For(ToolbarAddPhotoButton.BindClick()).To(vm => vm.AddPhotoCommand).OneTime();
            set.Bind(ToolbarPhotoView).For(v => v.ImageStream).To(vm => vm.PhotoStream).OneWay();

            set.Bind(_emailTextField).To(vm => vm.Email).TwoWay();
            set.Bind(_phoneTextField).To(vm => vm.Phone).TwoWay();
            set.Bind(_fullnameTextField).To(vm => vm.FullName).TwoWay();

            set.Bind(_emailTextField).For(v => v.Activated).To(vm => vm.IsEmailInvalid).OneWay();
            set.Bind(_phoneTextField).For(v => v.Activated).To(vm => vm.IsPhoneInvalid).OneWay();
            set.Bind(_fullnameTextField).For(v => v.Activated).To(vm => vm.IsFullnameInvalid).OneWay();

            set.Bind(_saveChangesButton).To(vm => vm.SaveChangesCommand).OneTime();

            set.Apply();

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            ToolbarRightButton.SetImageResource(Resource.Drawable.close);
            ToolbarLeftSpaceView.Visibility = ViewStates.Gone;
            ToolbarRightButton.Visibility = ViewStates.Visible;
            ToolbarAddPhotoButton.Visibility = ViewStates.Visible;
            ToolbarPhotoContainer.Visibility = ViewStates.Visible;
        }

    }
}
