using System.Collections.Generic;
using System.Linq;
using KabalistusTransformationTracker.Trans;

namespace KabalistusTransformationTracker.Providers {
    public abstract class AfterbirthBaseInfoProvider : BaseInfoProvider {

        protected abstract Transformation GetSuperBumTransformation();

        protected virtual TransformationInfo GetSuperBumInfo() {
            var itemsGot = new List<string>();
            var superBumTransformation = GetSuperBumTransformation();
            var counter = superBumTransformation.Items.Sum(item => {
                if (!GetReader().HasItem(item)) return 0;
                itemsGot.Add(item.Name);
                return 1;
            });
            var transformed = counter == 3;
            return new TransformationInfo(counter.ToString(), transformed, itemsGot, ItemsBlacklisted(superBumTransformation.Items));
        }
    }
}
