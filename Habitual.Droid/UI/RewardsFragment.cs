using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Habitual.Core.Entities;
using Habitual.Core.Executors;
using Habitual.Core.Executors.Impl;
using Habitual.Droid.Helpers;
using Habitual.Droid.Presenters;
using Habitual.Droid.Presenters.Impl;
using Habitual.Droid.Threading;
using Habitual.Droid.Util;
using Habitual.Storage;
using Habitual.Storage.Local;

namespace Habitual.Droid.UI
{
    public class RewardsFragment : Fragment, RewardView
    {
        private ListView rewardList;
        private List<Reward> items;
        private RewardListAdapter adapter;
        private RewardPresenter presenter;
        private MainThread mainThread;
        private MainApplicationCallback callback;

        public RewardsFragment(MainApplicationCallback callback)
        {
            this.callback = callback;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Init();
        }

        private void Init()
        {
            mainThread = new MainThreadImpl(this.Activity);
            presenter = new RewardPresenterImpl(TaskExecutor.GetInstance(), mainThread, this, new RewardRepositoryImpl(), new UserRepositoryImpl(), LocalData.Username, LocalData.Password);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.Rewards, container, false);
            InitializeElements(view);

            return view;
        }

        private void InitializeElements(View view)
        {
            if (items == null) items = new List<Reward>();
            rewardList = view.FindViewById<ListView>(Resource.Id.rewardsList);
            var addButton = (FloatingActionButton)view.FindViewById(Resource.Id.addRewardButton);
            addButton.Click += AddButton_Click;
            adapter = new RewardListAdapter(Activity, items, callback);
            rewardList.ItemClick += RewardTapped;
            rewardList.Adapter = adapter;
            Update();
        }

        internal void Reset()
        {
            Init();
        }

        private void RewardTapped(object sender, AdapterView.ItemClickEventArgs e)
        {
            TextDialogBuilder builder = new TextDialogBuilder();
            var dialog = builder.BuildStandardYesNoDialog(this.Activity, $"Buy or Delete {items[e.Position].Description}?", "Would you like to buy or delete this reward?", BuyOrDeleteReward, e, "Buy", "Delete");
            dialog.Show();
        }

        private void BuyOrDeleteReward(int result, AdapterView.ItemClickEventArgs e)
        {
            if (result == 0) // Delete
            {
                presenter.DeleteReward(items[e.Position]);
            }

            if (result == 1) // Buy
            {
                presenter.BuyReward(items[e.Position]);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            PromptNewReward();
        }

        private void PromptNewReward()
        {
            TextDialogBuilder builder = new TextDialogBuilder();
            var dialog = builder.BuildNewRewardDialog(this.Activity, AddNewReward);
            dialog.Show();
        }

        private void AddNewReward(Reward reward)
        {
            presenter.CreateReward(reward);
        }

        public void Update()
        {
            presenter.GetRewards(LocalData.Username);
        }

        public void UpdateRewards(List<Reward> rewards)
        {
            this.items = rewards;
            adapter.Update(items);
        }

        public void ShowProgress()
        {
            throw new NotImplementedException();
        }

        public void HideProgress()
        {
            throw new NotImplementedException();
        }

        public void ShowError()
        {
            throw new NotImplementedException();
        }

        public void OnRewardsRetrieved(List<Reward> rewards)
        {
            UpdateRewards(rewards);
        }

        public void OnRewardCreated(Reward reward)
        {
            Update();
        }

        public void OnRewardDeleted()
        {
            Update();
        }

        public void OnRewardBought(Reward reward)
        {
            callback.ShowPointsUpdate(reward.Cost * -1);
            callback.UserUpdateRequested();
            Toast.MakeText(this.Activity, $"{reward.Description} purchased for {reward.Cost}!", ToastLength.Short).Show();
        }

        public void OnError(string message)
        {
            Toast.MakeText(this.Activity, $"{message}", ToastLength.Long).Show();
        }
    }
}