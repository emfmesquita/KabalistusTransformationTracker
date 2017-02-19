using KabalistusCommons.Isaac;
using KabalistusIsaacTools.TransformationTracker.Model;

namespace KabalistusIsaacTools.TransformationTracker.Providers {
    public abstract class AfterbirthBaseInfoProvider : BaseInfoProvider {

        protected virtual void UpdateSuperBumTransformation(Transformation superBumTransformation) {
            var counter = 0;
            superBumTransformation.Items.ForEach(item => {
                var hasBumItem = HasBumItem(item);
                if (hasBumItem) {
                    counter++;
                }
                UpdateTransformationItem(item, hasBumItem, IsItemBlacklisted(item));
            });
            superBumTransformation.ShowTransformationImage = counter == 3;
            superBumTransformation.Count = counter.ToString();
        }

        protected virtual bool HasBumItem(TransformationItem item) {
            return GetReader().HasItem(new Item(item.Tooltip, item.Id));
        }
    }
}
