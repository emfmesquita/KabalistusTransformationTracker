using System.IO;
using Nancy;
using Nancy.Conventions;

namespace KabalistusTransformationTracker.Web {
    public class NancyBootstrapper : DefaultNancyBootstrapper {
        protected override void ConfigureConventions(NancyConventions nancyConventions) {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("/", "Resources"));
            base.ConfigureConventions(nancyConventions);
        }

        protected override byte[] FavIcon {
            get {
                var image = Properties.Resources.guppyshead;
                var ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
