using System.Security.Principal;

namespace KabalistusTransformationTracker.Web {
    public class WebHelper {
        public static void StartWeb() {
            if (!IsElevated()) {
                return;
            }
            NatHelper.PortFoward();
            FirewallHelper.AddRules();
            NancyHelper.StartNancyHost();
        }

        private static bool IsElevated() {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}
