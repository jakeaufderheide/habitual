using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Habitual.Core.Entities;
using Habitual.Droid.UI;

namespace Habitual.Droid.Util
{
    public class RewardListAdapter : BaseAdapter<Reward>
    {
        private Activity context;
        private List<Reward> items;
        private MainApplicationCallback callback;

        public RewardListAdapter(Activity context, List<Reward> items, MainApplicationCallback callback)
        {
            this.context = context;
            this.items = items;
            this.callback = callback;
        }

        public override Reward this[int position]
        {
            get
            {
                return items[position];
            }
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];
            if (convertView == null) // no view to re-use, create new
                convertView = context.LayoutInflater.Inflate(Resource.Layout.TaskCell, null);
            return GenerateRewardCell(item, convertView, parent);
        }

        private View GenerateRewardCell(Reward reward, View view, ViewGroup parent)
        {
            var rewardDescription = view.FindViewById<TextView>(Resource.Id.taskDescription);
            rewardDescription.Text = reward.Description;

            var rewardCost = view.FindViewById<TextView>(Resource.Id.habitCount);
            rewardCost.Visibility = ViewStates.Visible;
            rewardCost.Text = reward.Cost.ToString();

            var image = view.FindViewById<ImageView>(Resource.Id.taskIcon);
            image.SetImageResource(Resource.Drawable.reward);

            var checkBox = view.FindViewById<CheckBox>(Resource.Id.markDoneCheckbox);
            checkBox.Visibility = ViewStates.Gone;
            checkBox.Checked = false;
            checkBox.Focusable = false;
            checkBox.FocusableInTouchMode = false;
            checkBox.Clickable = false;
            
            return view;
        }

        public void Update(List<Reward> items)
        {
            if (items != null)
            {
                this.items = items;
                context.RunOnUiThread(() => NotifyDataSetChanged());
            }
            callback.UserUpdateRequested();
        }
    }
}
