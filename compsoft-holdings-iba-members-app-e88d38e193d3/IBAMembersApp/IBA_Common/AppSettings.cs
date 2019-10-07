using System;
using System.Configuration;

namespace IBA_Common
{
    public static class AppSettings
    {
        private static int _maxThumbNailWidth = -1;
        private static int _maxThumbNailHeight = -1;
        private static int _minThumbNailWidth = -1;
        private static int _minThumbNailHeight = -1;

        private static int GetValue(string setting, int defaultValue)
        {
            int result;
            if (!int.TryParse(ConfigurationManager.AppSettings[setting], out result))
            {
                result = defaultValue;
            }
            return result;
        }

        private static DateTime? GetDate(string setting)
        {
            DateTime result;
            if (!DateTime.TryParse(ConfigurationManager.AppSettings[setting], out result))
            {
                return null;
            }
            return DateTime.SpecifyKind(result, DateTimeKind.Utc);
        }

        public static int MaxThumbNailWidth
        {
            get
            {
                if (_maxThumbNailWidth == -1)
                {
                    _maxThumbNailWidth = GetValue("MaxThumbNailWidth", 1400);
                }
                return _maxThumbNailWidth;
            }
        }

        public static int MinThumbNailWidth
        {
            get
            {
                if (_minThumbNailWidth == -1)
                {
                    _minThumbNailWidth = GetValue("MinThumbNailWidth", 400);
                }
                return _minThumbNailWidth;
            }
        }

        public static int MaxThumbNailHeight
        {
            get
            {
                if (_maxThumbNailHeight != -1) return _maxThumbNailHeight;
                _maxThumbNailHeight = GetValue("MaxThumbNailHeight", 0);
                return _maxThumbNailHeight;
            }
        }

        public static int MinThumbNailHeight
        {
            get
            {
                if (_minThumbNailHeight != -1) return _minThumbNailHeight;
                _minThumbNailHeight = GetValue("MinThumbNailHeight", 175);
                return _minThumbNailHeight;
            }
        }

        public static string ContentLibraryImagePath = "~/images/contentlibrary/";

        public static string ImagePlaceHolder = "~/content/images/no_image_placeholder.png";
        public static string GridImagePlaceHolder = "~/content/images/grid_no_image_placeholder.png";

        private static string _authApiKey;
        public static string AuthApiKey
        {
            get
            {
                _authApiKey = _authApiKey ?? ConfigurationManager.AppSettings["AuthAPIKey"];
                return _authApiKey;
            }
        }

        private static string _urbanAirshipKey;
        public static string UrbanAirshipKey
        {
            get {
                _urbanAirshipKey = _urbanAirshipKey ?? ConfigurationManager.AppSettings["UrbanAirshipAppKey"];
                return _urbanAirshipKey;
            }
        }

        private static string _urbanAirshipAppSecret;
        public static string UrbanAirshipAppSecret
        {
            get
            {
                _urbanAirshipAppSecret = _urbanAirshipAppSecret ?? ConfigurationManager.AppSettings["UrbanAirshipAppSecret"];
                return _urbanAirshipAppSecret;
            }
        }

        public static byte[] _messageKey;
        public static byte[] MessageKey
        {
            get
            {
                _messageKey = _messageKey ?? System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["MessageKey"]);
                return _messageKey;
            }
        }


        public static byte[] _messageIV;
        public static byte[] MessageIV
        {
            get
            {
                _messageIV = _messageIV ?? System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["MessageIV"]);
                return _messageIV;
            }
        }

        public static string SuperAdminName => ConfigurationManager.AppSettings["Username"];

        public static string SuperAdminPassword => ConfigurationManager.AppSettings["Password"];

        public static string DataDirectory => ConfigurationManager.AppSettings["DataDirectory"];

        public static string SlidingSessionExpirationInDays
            => ConfigurationManager.AppSettings["SlidingSessionExpirationInDays"];

        public static string ProfileImageAddress => ConfigurationManager.AppSettings["ImageAddress"];

        public static string RequestDifferenceMaxSeconds
            => ConfigurationManager.AppSettings["RequestDifferenceMaxSeconds"];

        public static int PushExpirationInSeconds => int.Parse(ConfigurationManager.AppSettings["PushExpirationInSeconds"]);

        public static string IoSAppCertificatePassword => ConfigurationManager.AppSettings["IoSAppCertificatePassword"];

        public static string AndroidAppPackage => ConfigurationManager.AppSettings["AndroidAppPackage"];

        public static string AndroidPushSenderId => ConfigurationManager.AppSettings["AndroidPushSenderId"];

        public static string AndroidPushSenderServerApiKey => ConfigurationManager.AppSettings["AndroidPushSenderServerApiKey"];

        public static bool AndroidDebugMode => bool.Parse(ConfigurationManager.AppSettings["AndroidDebugMode"]);

        public static bool IosDebugMode => bool.Parse(ConfigurationManager.AppSettings["IosDebugMode"]);

        public static bool ShouldSendEmails => bool.Parse(ConfigurationManager.AppSettings["ShouldSendEmails"]);

        public static bool IgnorePushingForTest => bool.Parse(ConfigurationManager.AppSettings["IgnorePushingForTest"]);

        public static int MessagingServiceIntervalSeconds => GetValue("MessagingServiceIntervalSeconds", 30);

        public static bool ShowConferenceDetails => bool.Parse(ConfigurationManager.AppSettings["ShowConferenceDetails"]);

        public static int ConferenceId => int.Parse(ConfigurationManager.AppSettings["ConferenceId"]);

        public static string ConferenceUrl => ConfigurationManager.AppSettings["ConferenceUrl"];

        public static DateTime? ConferenceStart => GetDate("ConferenceStart");

        public static DateTime? ConferenceEnd => GetDate("ConferenceEnd");

        public static int FailedPushRetryCount => GetValue("FailedPushRetryCount", 10);
        public static int FailedPushRetryIntervalMinutes => GetValue("FailedPushRetryIntervalMinutes", 120);
        public static int HungPushSendingRetryIntervalMinutes => GetValue("HungPushSendingRetryIntervalMinutes", 120);

        public static string ServiceName => ConfigurationManager.AppSettings["ServiceName"]?? "Iba Messaging Service";

        public static object EncodingConfigurationManager { get; private set; }
    }
}
