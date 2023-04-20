using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace DRiUv4
{
    public class DeviceId
    {
        public static string Id { get; set; }

        public DeviceId()
        {
            
        }

        public static string GetDeviceId()
        {
            Id = Preferences.Get("my_deviceId", string.Empty);
            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = System.Guid.NewGuid().ToString();
                Preferences.Set("my_deviceId", Id);
            }

            return Id;
        }
    }
}
