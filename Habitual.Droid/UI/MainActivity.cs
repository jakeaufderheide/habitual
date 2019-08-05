/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Jacob Aufderheide
 * North Central College - Master's Project Application
 * Primary Reader: Brian Craig
 * Xamarin Cross-Platform Habit Tracking Application
 * 5/15/2017
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.App;
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
    public interface MainApplicationCallback
    {
        void UserUpdateRequested();
        void UpdateAllRequested();
        void ShowPointsUpdate(int pointsAdded);
    }

    [Activity(Label = "Habitual.Droid", MainLauncher = true, Icon = "@drawable/ic_launcher", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity, MainView, MainApplicationCallback
    {
        private MainPresenter mainPresenter;
        private MainThread mainThread;
        private IMenuItem refreshItem;
        private MainFragmentPagerAdapter adapter;
        private ProgressDialogForm progressDialog;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SetupTabControl();
            SetupAvatarEdit();
            Init();
            progressDialog = new ProgressDialogForm(this);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (!string.IsNullOrEmpty(LocalData.Username.Trim()))
            {
                try
                {
                    progressDialog.ShowDialog("", "Checking user...");
                    mainPresenter.GetUser(LocalData.Username, LocalData.Password);
                }
                catch (Exception ex)
                {

                }
                
            }
            else
            {
                PromptLoginOrRegister();
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                Android.Net.Uri imageUri = data.Data;
                Bitmap bitmap = MediaStore.Images.Media.GetBitmap(this.ContentResolver, imageUri);
                using (var stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);

                    var bytes = stream.ToArray();
                    var str = Convert.ToBase64String(bytes);
                    mainPresenter.SetAvatar(LocalData.Username, str);
                }

                
                var imageView =
                    FindViewById<ImageView>(Resource.Id.avatar);
                imageView.SetImageURI(data.Data);
            }
        }

        private void SetupAvatarEdit()
        {
            var avatarImage = FindViewById<ImageView>(Resource.Id.avatar);
            avatarImage.Click += OpenAvatarSelectionScreen;
        }

        private void OpenAvatarSelectionScreen(object sender, EventArgs e)
        {
            var imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(
                Intent.CreateChooser(imageIntent, "Select photo"), 0);
        }

        private void PromptLoginOrRegister()
        {
            TextDialogBuilder builder = new TextDialogBuilder();
            var dialog = builder.BuildLoginDialog(this, Login, CreateUser);
            dialog.Show();
        }

        private void Login(string username, string hashedPassword)
        {
            progressDialog.ShowDialog("","Logging in...");
            mainPresenter.GetUser(username, hashedPassword);
        }

        private void CreateUser(string username, string hashedPassword)
        {
            progressDialog.ShowDialog("", "Creating user...");
            mainPresenter.CreateUser(username, hashedPassword);
        }

        private void SetupTabControl()
        {
            adapter = new MainFragmentPagerAdapter(SupportFragmentManager, this);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.currentFragmentViewPager);
            viewPager.Adapter = adapter;

            TabLayout tabLayout = FindViewById<TabLayout>(Resource.Id.tabControl);
            tabLayout.SetupWithViewPager(viewPager);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.mainMenu, menu);

            refreshItem = menu.FindItem(Resource.Id.refresh);
            refreshItem.SetIcon(Resource.Drawable.refresh);
            refreshItem.SetShowAsAction(ShowAsAction.Always);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            if (item == refreshItem)
            {
                var username = LocalData.Username;
                var password = LocalData.Password;

                mainPresenter.GetUser(LocalData.Username, LocalData.Password);
            }
            else
            {
                LocalData.Username = "";
                LocalData.Password = "";
                UpdateAllRequested();
                PromptLoginOrRegister();
            }
            return true;
        }

        private void Init()
        {
            mainThread = new MainThreadImpl(this);

            mainPresenter = new MainPresenterImpl(TaskExecutor.GetInstance(), mainThread, this, new UserRepositoryImpl());
        }

        public void HideProgress()
        {
            progressDialog.Dismiss();
        }

        public void OnUserCreated(User user)
        {
            progressDialog.Dismiss();
            Toast.MakeText(this, string.Format("User {0} created!", user.Username), ToastLength.Short).Show();

            mainPresenter.StoreUserLocal(user);
        }

        public void ShowProgress()
        {
            progressDialog.ShowDialog("", "Loading...");
        }

        public void OnUserRetrieved(User user)
        {
            RunOnUiThread(() =>
            {
                progressDialog.Dismiss();
            });

            if (user == null || string.IsNullOrEmpty(user.Username))
            {
                RunOnUiThread(() =>
                {
                    PromptLoginOrRegister();
                });
                return;
            }
            if (user.Username == LocalData.Username)
            {
                UpdateInterfaceWithUser(user);
                adapter.ResetAllFrags();
            } else
            {
                mainPresenter.StoreUserLocal(user);
            }
        }

        private void UpdateInterfaceWithUser(User user)
        {
            RunOnUiThread(() =>
            {
                FindViewById<TextView>(Resource.Id.userNameText).Text = user.Username;
                FindViewById<TextView>(Resource.Id.pointsText).Text = user.Points.ToString();
                if (user.Avatar != null)
                {
                    var imageBitmap = BitmapFactory.DecodeByteArray(user.Avatar, 0, user.Avatar.Length);
                    FindViewById<ImageView>(Resource.Id.avatar).SetImageBitmap(imageBitmap);
                } else
                {
                    FindViewById<ImageView>(Resource.Id.avatar).SetImageResource(Resource.Drawable.avatar_test);
                }
                progressDialog.Dismiss();
            });

            adapter.UpdateFragments();
        }

        public void OnUserStored(User user)
        {
            progressDialog.Dismiss();
            Toast.MakeText(this, "User stored locally", ToastLength.Short).Show();
            UpdateInterfaceWithUser(user);
        }

        public void UserUpdateRequested()
        {
            mainPresenter.GetPoints(LocalData.Username, LocalData.Password);
        }

        public void OnPointsRetrieved(int points)
        {
            FindViewById<TextView>(Resource.Id.pointsText).Text = points.ToString();
        }

        public void UpdateAllRequested()
        {
            adapter.UpdateFragments();
        }

        public void OnAvatarSet(string imageString)
        {
            var imageBytes = Convert.FromBase64String(imageString);
            var imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            FindViewById<ImageView>(Resource.Id.avatar).SetImageBitmap(imageBitmap);
        }

        public void OnError(string message)
        {
            RunOnUiThread(() => {
                if (string.IsNullOrEmpty(LocalData.Username) || string.IsNullOrEmpty(LocalData.Username))
                {
                    PromptLoginOrRegister();
                }
                Toast.MakeText(this, message, ToastLength.Short);
            });
        }

        public void ShowPointsUpdate(int pointsAdded)
        {
            var pointsView = FindViewById<TextView>(Resource.Id.pointsText);
            var points = Int32.Parse(pointsView.Text);
            
            RunOnUiThread(() => {
                pointsView.Text = (points + pointsAdded).ToString();
            });
            
        }
    }
}

