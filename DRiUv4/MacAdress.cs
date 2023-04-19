using System.Linq;
using System.Net.NetworkInformation;

namespace DRiUv4
{
    internal class MacAdress
    {
#pragma warning disable

        public static string GetIPAddress() => NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up 
                    && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault().ToString();

        public static string GetMacAdress() => NetworkInterface.GetAllNetworkInterfaces()
                                                .OrderBy(intf => intf.NetworkInterfaceType)
                                                 .FirstOrDefault(intf => intf.OperationalStatus == OperationalStatus.Up
                                                  && (intf.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
                                                 || intf.NetworkInterfaceType == NetworkInterfaceType.Ethernet)).ToString();
    }
}
