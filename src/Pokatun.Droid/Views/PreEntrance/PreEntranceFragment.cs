
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
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.PreEntrance;
using Pokatun.Droid;

namespace Pokatun.Droid.Views.PreEntrance
{
    [MvxFragmentPresentation(typeof(MainContainerViewModel), Resource.Id.content_frame, AddToBackStack = true)]
    public sealed class PreEntranceFragment : BaseFragment<PreEntranceViewModel>
    {
        private TextView _helloLabel;
        private TextView _preEntranceDescriptionLabel;
        private Button _registrationButton;
        private TextView _iStillDoesntHaveAccLabel;
        private Button _loginButton;
        private TextView _iHaveAccLabel;

        protected override int FragmentLayoutId => Resource.Layout.fragment_pre_entrance;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _helloLabel = view.FindViewById<TextView>(Resource.Id.helloLabel);
            _preEntranceDescriptionLabel = view.FindViewById<TextView>(Resource.Id.preEntranceDescriptionLabel);
            _registrationButton = view.FindViewById<Button>(Resource.Id.registrationButton);
            _iStillDoesntHaveAccLabel = view.FindViewById<TextView>(Resource.Id.iStillDoesntHaveAccLabel);
            _loginButton = view.FindViewById<Button>(Resource.Id.loginButton);
            _iHaveAccLabel = view.FindViewById<TextView>(Resource.Id.iHaveAccLabel);

            _helloLabel.Text = Strings.HelloCaps;
            _preEntranceDescriptionLabel.Text = Strings.PreEntranceDescriptionText;
            _registrationButton.Text = Strings.Registration;
            _iStillDoesntHaveAccLabel.Text = Strings.IStillDoesntHaveAccText;
            _loginButton.Text = Strings.Entrance;
            _iHaveAccLabel.Text = Strings.IHaveAccText;

            return view;
        }

    }
}
