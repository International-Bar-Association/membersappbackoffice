using System;
using System.Linq;
using System.Net.Http;
using IBA_Common;

namespace IBAMembersApp.Utilities
{
    public static class RecordSessionUtilities
    {
        public static record_sessions ActiveSessionByUserId(IBAEntities1 context, decimal recordId)
        {
            return context.record_sessions.OrderByDescending(r => r.session_expiry).FirstOrDefault(
                r => r.record_id == recordId
                && r.logout_time == null
                && r.session_expiry > DateTime.UtcNow);
        }

        public static record_sessions ActiveSessionByToken(IBAEntities1 context, string token)
        {
            return context.record_sessions.FirstOrDefault(
                r => r.session_token.ToString() == token
                && r.logout_time == null
                && r.session_expiry > DateTime.UtcNow);
        }

        public static record_sessions SessionByToken(IBAEntities1 context, string token)
        {
            return context.record_sessions.FirstOrDefault(r => r.session_token.ToString() == token);
        }

        public static record_sessions SessionByToken(IBAEntities1 context, HttpRequestMessage request)
        {
            var sessionToken = request.Headers.GetValues("UserKey").FirstOrDefault();
            return SessionByToken(context,sessionToken);
        }

        private static TimeSpan _slidingSessionExpiry;

        public static TimeSpan SlidingSessionExpiryInDays
        {
            get
            {
                if (_slidingSessionExpiry == TimeSpan.FromDays(0))
                {
                    if (!TimeSpan.TryParse(AppSettings.SlidingSessionExpirationInDays, out _slidingSessionExpiry))
                    {
                        _slidingSessionExpiry = TimeSpan.FromDays(10);
                    }
                }
                return _slidingSessionExpiry;
            }
        }
    }
}
