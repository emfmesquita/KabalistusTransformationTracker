using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Nancy.Conventions;
using Nancy.Hosting.Self;
using static KabalistusTransformationTracker.Web.WebConstants;

namespace KabalistusTransformationTracker.Web {
    public class NancyHelper
    {

        private static readonly NancyHost NancyHost = new NancyHost(GetUriParams());

        public static void StartNancyHost() {
            StaticContentConventionBuilder.AddDirectory("imgs", "Resources/Images");
            NancyHost.Start();
        }

        private static Uri[] GetUriParams() {
            var uriParams = new List<Uri>();
            var hostName = Dns.GetHostName();

            // Host name URI
            var hostNameUri = $"http://{Dns.GetHostName()}:{Port}";
            uriParams.Add(new Uri(hostNameUri));

            // Host address URI(s)
            var hostEntry = Dns.GetHostEntry(hostName);
            foreach (var ipAddress in hostEntry.AddressList) {
                if (ipAddress.AddressFamily != AddressFamily.InterNetwork) continue;
                var addrBytes = ipAddress.GetAddressBytes();
                string hostAddressUri = $"http://{addrBytes[0]}.{addrBytes[1]}.{addrBytes[2]}.{addrBytes[3]}:{Port}";
                uriParams.Add(new Uri(hostAddressUri));
            }

            // Localhost URI
            uriParams.Add(new Uri($"http://localhost:{Port}"));
            return uriParams.ToArray();
        }
    }
}
