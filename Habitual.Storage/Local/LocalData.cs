using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Habitual.Storage.Local
{
    public static class LocalData
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        #region Setting Constants
        private const string UsernameKey = "username_key";
        private static readonly string UserNameValue = string.Empty;

        private const string PasswordKey = "password_key";
        private static readonly string PasswordValue = string.Empty;

        private const string TaskKey = "task_key";
        private static readonly string TaskValue = string.Empty;

        private const string AvatarKey = "avatar_storage";
        private static readonly string AvatarValue = string.Empty;

        private const string RewardsKey = "rewards_key";
        private static readonly string RewardsValue = string.Empty;
        #endregion

        public static string Username
        {
            get { return AppSettings.GetValueOrDefault<string>(UsernameKey, UserNameValue); }
            set { AppSettings.AddOrUpdateValue<string>(UsernameKey, value); }
        }

        public static string Password
        {
            get { return AppSettings.GetValueOrDefault<string>(PasswordKey, PasswordValue); }
            set { AppSettings.AddOrUpdateValue<string>(PasswordKey, value); }
        }

        // Store data locally before WebAPI is active
        public static string TaskContainer
        {
            get { return AppSettings.GetValueOrDefault<string>(TaskKey, TaskValue); }
            set { AppSettings.AddOrUpdateValue<string>(TaskKey, value); }
        }

        public static string AvatarStorage
        {
            get { return AppSettings.GetValueOrDefault<string>(AvatarKey, AvatarValue); }
            set { AppSettings.AddOrUpdateValue<string>(AvatarKey, value); }
        }

        public static string Rewards
        {
            get { return AppSettings.GetValueOrDefault<string>(RewardsKey, RewardsValue); }
            set { AppSettings.AddOrUpdateValue<string>(RewardsKey, value); }
        }
    }
}
