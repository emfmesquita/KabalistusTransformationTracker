using System.Threading;
using Open.Nat;
using static KabalistusTransformationTracker.Web.WebConstants;

namespace KabalistusTransformationTracker.Web {
    public class NatHelper {
        public static async void PortFoward() {
            var discoverer = new NatDiscoverer();
            var cts = new CancellationTokenSource(10000);
            var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);
            await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, Port, Port, "KabalistusTT Tcp " + StringPort));
            await device.CreatePortMapAsync(new Mapping(Protocol.Udp, Port, Port, "KabalistusTT Udp " + StringPort));
        }
    }
}
