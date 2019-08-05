using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Habitual.Core.Entities;
using Habitual.Core.Entities.Base;
using Habitual.Droid.Util;

namespace Habitual.Droid.Helpers
{

    public class TextDialogBuilder
    {
        public AlertDialog BuildNewTaskDialog(Activity activity, Action<BaseTask> newTaskMethod)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);

            LayoutInflater inflater = activity.LayoutInflater;

            builder.SetTitle("Add New Task");

            var view = inflater.Inflate(Resource.Layout.NewTaskDialog, null);

            view = NewTaskDialog.SetupDialogFunctionality(view);

            builder.SetView(view);
            builder.SetPositiveButton("SAVE", new EventHandler<DialogClickEventArgs>((s, args) =>
            {
                var taskToAdd = NewTaskDialog.GenerateTaskFromDialog(view);
                newTaskMethod(taskToAdd);
            }));
            builder.SetNegativeButton("CANCEL", new EventHandler<DialogClickEventArgs>((s, args) =>
            {
                //exit
            }));

            return builder.Create();
        }

        public AlertDialog BuildNewRewardDialog(Activity activity, Action<Reward> newRewardMethod)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);

            LayoutInflater inflater = activity.LayoutInflater;

            builder.SetTitle("Add New Reward");

            var view = inflater.Inflate(Resource.Layout.NewRewardDialog, null);


            builder.SetView(view);
            builder.SetPositiveButton("SAVE", new EventHandler<DialogClickEventArgs>((s, args) =>
            {
                var rewardToAdd = NewRewardDialog.GenerateRewardFromDialog(view);
                newRewardMethod(rewardToAdd);
            }));
            builder.SetNegativeButton("CANCEL", new EventHandler<DialogClickEventArgs>((s, args) =>
            {
                //exit
            }));

            return builder.Create();
        }

        public AlertDialog BuildLoginDialog(Activity activity, Action<string, string> loginMethod, Action<string,string> registerMethod)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);

            LayoutInflater inflater = activity.LayoutInflater;

            builder.SetTitle("Login / Registration Required");

            var view = inflater.Inflate(Resource.Layout.LoginDialog, null);
            var username = view.FindViewById<EditText>(Resource.Id.login_username);
            var password = view.FindViewById<TextView>(Resource.Id.login_password);

            builder.SetView(view);
            builder.SetPositiveButton("LOGIN", new EventHandler<DialogClickEventArgs>((s, args) =>
            {
                if (username.Text == null || password.Text == null)
                {
                    // do nothing
                }
                else
                {
                    PasswordHasher hasher = new PasswordHasher();
                    var hashedPassword = hasher.HashPassword(password.Text);
                    loginMethod(username.Text, hashedPassword);
                }
            }));
            builder.SetNegativeButton("REGISTER", new EventHandler<DialogClickEventArgs>((s, args) =>
            {
                if (username.Text == null || password.Text == null)
                {
                    // do nothing
                }
                else
                {
                    PasswordHasher hasher = new PasswordHasher();
                    var hashedPassword = hasher.HashPassword(password.Text);
                    registerMethod(username.Text, hashedPassword);
                }
                
            }));

            return builder.Create();
        }

        public AlertDialog BuildStandardYesNoDialog(Activity activity, string title, string message, Action<int, AdapterView.ItemClickEventArgs> resultMethod, AdapterView.ItemClickEventArgs e = null, string positiveButton = "YES", string negativeButton = "NO")
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(activity);

            LayoutInflater inflater = activity.LayoutInflater;

            builder.SetTitle(title);

            var view = inflater.Inflate(Resource.Layout.StandardDialog, null);
            view.FindViewById<TextView>(Resource.Id.dialogText).Text = message;

            builder.SetView(view);
            builder.SetPositiveButton(positiveButton, new EventHandler<DialogClickEventArgs>((s, args) =>
            {
                resultMethod(1, e);
            }));
            builder.SetNegativeButton(negativeButton, new EventHandler<DialogClickEventArgs>((s, args) =>
            {
                resultMethod(0, e);
            }));

            return builder.Create();
        }

    }
}