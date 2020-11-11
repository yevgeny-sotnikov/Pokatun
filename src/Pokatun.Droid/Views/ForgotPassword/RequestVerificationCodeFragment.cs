using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.Resources;
using Pokatun.Core.ViewModels.ForgotPassword;
using Pokatun.Core.ViewModels.Main;

namespace Pokatun.Droid.Views.ForgotPassword
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
    public sealed class RequestVerificationCodeFragment : BaseFragment<RequestVerificationCodeViewModel>
    {
        private static readonly IInputFilter[] MaxEmailLenghtFilter = new IInputFilter[] { new InputFilterLengthFilter(64) };

        private Button _button;
        private EditText _textField;
        private TextView _descriptionLabel;

        protected override int FragmentLayoutId => Resource.Layout.fragment_verification_code;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            _textField = view.FindViewById<EditText>(Resource.Id.textField);
            _descriptionLabel = view.FindViewById<TextView>(Resource.Id.descriptionLabel);
            _button = view.FindViewById<Button>(Resource.Id.button);

            _textField.Hint = Strings.Email;
            _textField.InputType = InputTypes.ClassText | InputTypes.TextVariationEmailAddress;
            _textField.SetFilters(MaxEmailLenghtFilter);

            _descriptionLabel.Text = Strings.WriteRegistrationEmailMessage;
            _button.Text = Strings.GetCode;

            #pragma warning disable IDE0008 // Use explicit type

            var set = CreateBindingSet();

            #pragma warning restore IDE0008 // Use explicit type

            set.Bind(_button).To(vm => vm.RequestCodeCommand);
            set.Bind(_button).For(b => b.Enabled).To(vm => vm.IsRequestCodeButtonEnabled);
            set.Bind(_textField).To(vm => vm.Email).OneWayToSource();

            set.Apply();

            return view;
        }
    }
}