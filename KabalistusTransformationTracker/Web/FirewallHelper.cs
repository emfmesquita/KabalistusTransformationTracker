using System;
using NetFwTypeLib;
using static KabalistusTransformationTracker.Web.WebConstants;

namespace KabalistusTransformationTracker.Web {
    public class FirewallHelper {
        private const int Tcp = 6;
        private const int Udp = 17;

        public static void AddRules() {
            var tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            var fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);
            var currentProfiles = fwPolicy2.CurrentProfileTypes;

            var firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            var hasTcp = false;
            var hasUdp = false;
            foreach (var ruleObj in firewallPolicy.Rules) {
                var rule = (INetFwRule)ruleObj;
                if (rule.Name == null || !rule.Name.StartsWith("KabalistusTT")) continue;
                hasTcp = hasTcp || (rule.Protocol == Tcp && StringPort.Equals(rule.LocalPorts));
                hasUdp = hasUdp ||  (rule.Protocol == Udp && StringPort.Equals(rule.LocalPorts));
            }

            if (!hasTcp) {
                CreateRule(Tcp, "KabalistusTT Tcp ", currentProfiles);
            }

            if (!hasUdp) {
                CreateRule(Udp, "KabalistusTT Udp ", currentProfiles);
            }
        }

        private static void CreateRule(int protocol, string namePrefix, int currentProfiles) {
            var inboundRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            inboundRule.Enabled = true;
            inboundRule.Protocol = protocol;
            inboundRule.LocalPorts = StringPort;
            inboundRule.Profiles = currentProfiles;
            inboundRule.Name = namePrefix + Port;

            var firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Add(inboundRule);
        }
    }
}
