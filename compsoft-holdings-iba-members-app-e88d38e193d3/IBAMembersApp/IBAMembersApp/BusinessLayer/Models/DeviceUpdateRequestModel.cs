using IBA_Common.Models;

namespace IBAMembersApp.BusinessLayer.Models
{
    public class DeviceUpdateRequestModel
    {

        // ReSharper disable once InconsistentNaming
        public string DeviceUUID { get; set; }

        public DeviceType DeviceType { get; set; }

        public string PushToken { get; set; }

    }

}
