using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Utility
{
    internal class Networking
    {
        public static void C(PingReply e)
        {
            if (e.Status == IPStatus.Success)
            {
                try
                {
                    Console.WriteLine("Thread: " + System.Threading.Thread.CurrentThread.Name);
                    Console.WriteLine("Host Name: " + Dns.GetHostEntry(e.Address).HostName);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Host name error");
                }
                finally
                {
                    Console.WriteLine("Address: {0}", e.Address.ToString());
                    Console.WriteLine("________________________________________________________________________________");
                }
            }
        }

        public static void GetHostNameFromIpAddress()
        {
            var netorkIp = GetInterfaceAddress();
            var tp = netorkIp[0] + "." + netorkIp[1] + "." + netorkIp[2] + ".";
            var t = new Task<PingReply>[256];
            for (var i = 0; i < 256; i++)
            {
                Ping pingSender = new Ping();
                //pingSender.PingCompleted += new PingCompletedEventHandler(C);
                PingOptions options = new PingOptions(64, true);

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "test";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                t[i] = pingSender.SendPingAsync(tp + i, timeout, buffer, options);
            }
            var w = Task.WhenAll(t);
            w.Wait();
            w.Result.ToList().ForEach(m => C(m));
        }

        public static void DisplayGatewayAddresses()
        {
            Console.WriteLine("Gateways");
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                GatewayIPAddressInformationCollection addresses = adapterProperties.GatewayAddresses;
                if (addresses.Count > 0)
                {
                    Console.WriteLine(adapter.Description);
                    foreach (GatewayIPAddressInformation address in addresses)
                    {
                        Console.WriteLine("  Gateway Address ......................... : {0}",
                            address.Address.ToString());
                    }
                    Console.WriteLine();
                }
            }
        }

        public static byte[] GetInterfaceAddress()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            var addressIpV4 = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(m => m.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
            byte[] subnetMask = new byte[4];
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
                    foreach (UnicastIPAddressInformation unicastIPAddressInformation in adapter.GetIPProperties().UnicastAddresses)
                    {
                        if (unicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            if (addressIpV4.Equals(unicastIPAddressInformation.Address))
                            {
                                subnetMask = unicastIPAddressInformation.IPv4Mask.GetAddressBytes();
                            }
                        }
                    }

                    byte[] networkIp = new byte[4];
                    var addressBytes = addressIpV4.GetAddressBytes();
                    for (var i = 0; i < 3; i++)
                    {
                        networkIp[i] = (byte)(addressBytes[i] & subnetMask[i]);
                    }

                    networkIp[3] = 0;

                    return networkIp;
                }
            }

            return null;
        }

        private void MicrosoftExample()
        {
            Networking.GetHostNameFromIpAddress();
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            Console.WriteLine("Interface information for {0}.{1}     ",
                    computerProperties.HostName, computerProperties.DomainName);
            if (nics == null || nics.Length < 1)
            {
                Console.WriteLine("  No network interfaces found.");
                return;
            }

            Console.WriteLine("  Number of interfaces .................... : {0}", nics.Length);
            foreach (NetworkInterface adapter in nics)
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                Console.WriteLine();
                Console.WriteLine(adapter.Description);
                Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));
                Console.WriteLine("  Interface type .......................... : {0}", adapter.NetworkInterfaceType);
                Console.WriteLine("  Physical Address ........................ : {0}",
                           adapter.GetPhysicalAddress().ToString());
                Console.WriteLine("  Operational status ...................... : {0}",
                    adapter.OperationalStatus);
                string versions = "";

                // Create a display string for the supported IP versions.
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    versions = "IPv4";
                }
                if (adapter.Supports(NetworkInterfaceComponent.IPv6))
                {
                    if (versions.Length > 0)
                    {
                        versions += " ";
                    }
                    versions += "IPv6";
                }
            }

            Console.WriteLine();
        }
    }
}
