using System.Linq;
using KabalistusTransformationTracker.Providers;
using KabalistusTransformationTracker.Trans;
using Newtonsoft.Json.Linq;

namespace KabalistusTransformationTracker.Web {
    public class TransformationsWebHelper {

        public static string GetTransformationWebInfo() {
            var json = new JArray();
            var version = TransformationInfoProvider.GetVersion();
            if (version == null)
            {
                return json.ToString();
            }
            TransformationInfoProvider.GetTransformationsInfo().ToList().ForEach(pair => {
                json.Add(TransformationToJson(pair.Key, pair.Value));
            });
            return json.ToString();
        }

        public static JObject TransformationToJson(string name, TransformationInfo info) {
            var trans = TransformationInfoProvider.GetAllTransformations()[name];

            var missingItems = new JArray();
            var touchedItems = new JArray();
            var blacklistedItems = new JArray();

            trans.Items.ForEach(item => {
                if (info.TouchedItems.Contains(item.Name)) {
                    touchedItems.Add(item.I18N);
                } else if (info.BlacklistedItems.Contains(item.Name)) {
                    blacklistedItems.Add(item.I18N);
                } else {
                    missingItems.Add(item.I18N);
                }
            });

            var transJson = new JObject {
                ["class"] = trans.Name,
                ["name"] = trans.I18N,
                ["count"] = touchedItems.Count,
                ["missingitems"] = missingItems,
                ["toucheditems"] = touchedItems,
                ["blacklisteditems"] = blacklistedItems
            };

            return transJson;
        }
    }
}
