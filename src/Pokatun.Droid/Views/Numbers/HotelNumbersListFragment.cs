using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using MvvmCross.Commands;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.Platforms.Android.Binding;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Pokatun.Core.ViewModels.Main;
using Pokatun.Core.ViewModels.Numbers;
using Pokatun.Data;

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
            _recyclerView.ItemTemplateId = Resource.Layout.hotel_number_item_template;

            _recyclerView.AddItemDecoration(new DividerItemDecoration(Context, RecyclerView.Vertical)
            {
                Drawable = ContextCompat.GetDrawable(Context, Resource.Color.separatorColor)
            });

            var swipeHandler = new SwipeToDeleteCallback(Context, ViewModel.DeleteCommand);
            var itemTouchHelper = new ItemTouchHelper(swipeHandler);
            itemTouchHelper.AttachToRecyclerView(_recyclerView);

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

        public class SwipeToDeleteCallback : ItemTouchHelper.SimpleCallback
        {
            private Drawable deleteIcon;
            private int intrinsicWidth;
            private int intrinsicHeight;
            private ColorDrawable background;
            private Color backgroundColor;
            private Paint clearPaint;
            private readonly IMvxCommand<HotelNumberDto> deleteCommand;

            public SwipeToDeleteCallback(Context context, IMvxCommand<HotelNumberDto> deleteCommand) : base(0, ItemTouchHelper.Left)
            {
                deleteIcon = ContextCompat.GetDrawable(context, Resource.Drawable.del_list);
                intrinsicWidth = deleteIcon.IntrinsicWidth;
                intrinsicHeight = deleteIcon.IntrinsicHeight;
                background = new ColorDrawable();
                backgroundColor = Color.ParseColor("#ce2d3c");
                clearPaint = new Paint();
                clearPaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));
                this.deleteCommand = deleteCommand;
            }

            public override int GetMovementFlags(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
            {
                if (viewHolder.AdapterPosition == 10)
                {
                    return 0;
                }
                return base.GetMovementFlags(recyclerView, viewHolder);
            }

            public override bool OnMove(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, RecyclerView.ViewHolder target)
            {
                return false;
            }

            public override void OnChildDraw(Canvas c, RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder, float dX, float dY, int actionState, bool isCurrentlyActive)
            {
                var itemView = viewHolder.ItemView;
                var itemHeight = itemView.Bottom - itemView.Top;

                // Draw the red delete background
                background.Color = backgroundColor;
                background.SetBounds(itemView.Right + (int)dX, itemView.Top, itemView.Right, itemView.Bottom);
                background.Draw(c);

                // Calculate position of delete icon
                var deleteIconTop = itemView.Top + (itemHeight - intrinsicHeight) / 2;
                var deleteIconMargin = (itemHeight - intrinsicHeight) / 2;
                var deleteIconLeft = itemView.Right - deleteIconMargin - intrinsicWidth;
                var deleteIconRight = itemView.Right - deleteIconMargin;
                var deleteIconBottom = deleteIconTop + intrinsicHeight;

                deleteIcon.SetBounds(deleteIconLeft, deleteIconTop, deleteIconRight, deleteIconBottom);
                deleteIcon.Draw(c);

                base.OnChildDraw(c, recyclerView, viewHolder, dX, dY, actionState, isCurrentlyActive);

                //var itemView = viewHolder.ItemView;
                //var itemHeight = itemView.Bottom - itemView.Top;
                //var isCanceled = dX == 0f && !isCurrentlyActive;

                //if (isCanceled)
                //{
                //    clearCanvas(c, itemView.Right + dX, (float)itemView.Top, (float)itemView.Right, (float)itemView.Bottom);
                //    base.OnChildDraw(c, recyclerView
                //        , viewHolder, dX, dY, actionState, isCurrentlyActive);
                //    return;
                //}
                //background.Color = backgroundColor;
                //background.SetBounds(itemView.Right + (int)dX, itemView.Top, itemView.Right, itemView.Bottom);
                //background.Draw(c);

                //var deleteIconTop = itemView.Top + (itemHeight - intrinsicHeight) / 2;
                //var deleteIconMargin = (itemHeight - intrinsicHeight) / 2;
                //var deleteIconLeft = itemView.Right - deleteIconMargin - intrinsicWidth;
                //var deleteIconRight = itemView.Right - deleteIconMargin;
                //var deleteIconBottom = deleteIconTop + intrinsicHeight;

                //deleteIcon.SetBounds(deleteIconLeft, deleteIconTop, deleteIconRight, deleteIconBottom);
                //deleteIcon.Draw(c);
            }

            private void clearCanvas(Canvas c, float v, float top, float right, float bottom)
            {
                c.DrawRect(v, top, right, bottom, clearPaint);
            }

            public override void OnSwiped(RecyclerView.ViewHolder viewHolder, int direction)
            {
                MvxRecyclerViewHolder holder = viewHolder as MvxRecyclerViewHolder;

                HotelNumberDto data = (HotelNumberDto)holder.DataContext;
                //Invoke Removing Item method from adapter
                if (deleteCommand.CanExecute(data))
                {
                    deleteCommand.Execute(data);
                }
            }

            public override void ClearView(RecyclerView recyclerView, RecyclerView.ViewHolder viewHolder)
            {
                base.ClearView(recyclerView, viewHolder);
            }
        }
    }
}