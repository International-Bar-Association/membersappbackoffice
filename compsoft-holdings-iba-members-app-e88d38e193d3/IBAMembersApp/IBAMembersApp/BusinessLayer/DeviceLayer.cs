using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using IBAMembersApp.BusinessLayer.Models;
using IBA_Common;
using IBA_Common.Models;

namespace IBAMembersApp.BusinessLayer
{
    public class DeviceLayer
    {
        public static DeviceUpdateResponseModel UpdateDevice(IbaCmsDbContext cmsDb, DeviceUpdateRequestModel model, decimal ibaRecordId)
        {
            const string version = "2";
            var result = new DeviceUpdateResponseModel();

            var device = cmsDb.Devices.FirstOrDefault(d => d.DeviceUUID == model.DeviceUUID);

            if (device != null)
            {
                if(device.DeviceOwner.IbaId != ibaRecordId)
                {
                    //note: Update device to new owner.
                    var newOwner = cmsDb.DeviceOwners.FirstOrDefault(o => o.IbaId == ibaRecordId);
                    if (newOwner == null)
                    {
                        newOwner = new DeviceOwner
                        {
                            IbaId = ibaRecordId,
                            Devices = new List<Device> { device },
                            LastDeviceUpdate = DateTime.UtcNow,
                            NamedUserId = ibaRecordId.ToString()
                        };
                        cmsDb.DeviceOwners.Add(newOwner);
                    }

                    device.DeviceOwner = newOwner;
                }
                device.DeviceOwner.NamedUserId = ibaRecordId.ToString();

                if (device.PushToken == model.PushToken)
                {
                    device.DeviceOwner.LastDeviceUpdate = DateTime.UtcNow;
                    cmsDb.SaveChanges();
                    return result;
                }
                try
                {
                    device.PushToken = model.PushToken;
                    device.DeviceType = model.DeviceType;
                    
                    device.ApiVersion = version;
                    device.UpdatedOn = DateTime.UtcNow;
                    device.DeviceOwner.LastDeviceUpdate = DateTime.UtcNow;
                    cmsDb.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    result.SetError(HttpStatusCode.InternalServerError, "Could not update the Push Token");
                }
            }
            var owner = cmsDb.DeviceOwners.FirstOrDefault(o => o.IbaId == ibaRecordId);

            device = new Device
            {
                DeviceUUID = model.DeviceUUID,
                ApiVersion = version,
                DeviceType = model.DeviceType,
                PushToken = model.PushToken,
                UpdatedOn = DateTime.UtcNow
            };
            if (owner == null)
            {
                owner = new DeviceOwner
                {
                    IbaId = ibaRecordId,
                    Devices = new List<Device> { device },
                    LastDeviceUpdate = DateTime.UtcNow
                };
                cmsDb.DeviceOwners.Add(owner);
            }
            else
            {
                owner.Devices.Add(device);
                owner.LastDeviceUpdate = DateTime.UtcNow;
            }
            try
            {
                cmsDb.SaveChanges();
            }
            catch (Exception)
            {
                result.SetError(HttpStatusCode.InternalServerError, "Could not update the Push Token");
            }
            return result;
        }

    }
}