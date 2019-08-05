using Android.App;
using Android.Content;

namespace Habitual.Droid.Util
{
    public class ProgressDialogForm
    {
        private ProgressDialog _progressDialog = null;
        private Context _activity = null;

        public ProgressDialogForm(Context activity)
        {
            _activity = activity;
            _progressDialog = new ProgressDialog(activity);
            _progressDialog.SetCancelable(false);
        }

        public void ShowDialog()
        {
            if (_progressDialog != null)
                _progressDialog.Show();
        }

        public void ShowDialog(string title, string message, Android.Graphics.Drawables.Drawable icon = null)
        {
            if (_progressDialog != null)
            {
                _progressDialog.SetTitle(title);
                _progressDialog.SetMessage(message);
                _progressDialog.SetIcon(icon);
                _progressDialog.Show();
            }
        }

        public void SetTitle(string message)
        {
            if (_progressDialog != null)
                _progressDialog.SetTitle(message);
        }

        public void SetIcon(global::Android.Graphics.Drawables.Drawable icon)
        {
            if (_progressDialog != null)
                _progressDialog.SetIcon(icon);
        }

        public void Dismiss()
        {
            if (_progressDialog != null)
                _progressDialog.Dismiss();
        }
    }
}