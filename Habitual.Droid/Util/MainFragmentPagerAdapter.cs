using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Habitual.Droid.UI;
using Java.Lang;
using JavaString = Java.Lang.String;

namespace Habitual.Droid.Util
{
    public class MainFragmentPagerAdapter : FragmentPagerAdapter
    {
        private static int PAGE_COUNT = 3;
        private JavaString[] tabTitles = new JavaString[] { new JavaString("Overview"), new JavaString("Manage"), new JavaString("Rewards") };
        private Context context;
        private OverviewFragment overview;
        private ManageFragment manage;
        private RewardsFragment rewards;
        private MainApplicationCallback callback;

        public MainFragmentPagerAdapter(Android.Support.V4.App.FragmentManager fm, Context context) : base(fm)
        {
            this.context = context;
            this.callback = (MainApplicationCallback)context;
        }

        public override int Count
        {
            get
            {
                return PAGE_COUNT;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            if (position == 0)
            {
                if (overview == null) overview = new OverviewFragment(callback);
                return overview;
            }

            if (position == 1)
            {
                if (manage == null) manage = new ManageFragment(callback);
                return manage;
            }

            if (position == 2)
            {
                if (rewards == null) rewards = new RewardsFragment(callback);
                return rewards;
            }
            throw new IndexOutOfRangeException();
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return tabTitles[position];
        }

        public void UpdateFragments()
        {
            if (overview != null ) overview.Update();
            if (manage != null) manage.Update();
            if (rewards != null) rewards.Update();
        }

        internal void ResetAllFrags()
        {
            overview.Reset();
            manage.Reset();
            if (rewards != null) rewards.Reset();
        }
    }
}