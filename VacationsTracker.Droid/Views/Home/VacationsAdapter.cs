using Android.Support.Annotation;
using Android.Support.V7.Widget;
using Android.Views;
using FlexiMvvm.Collections;

namespace VacationsTracker.Droid.Views.Home
{
    internal class VacationsAdapter : RecyclerViewObservablePlainAdapterBase
    {
        protected override RecyclerViewObservableViewHolder OnCreateItemViewHolder([NonNullAttribute] ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.From(parent.Context).Inflate(
                Resource.Layout.cell_vacation,
                parent,
                false);

            return new VacationCellViewHolder(itemView);
        }

        public VacationsAdapter(RecyclerView recyclerView) : base(recyclerView)
        {
        }
    }
}