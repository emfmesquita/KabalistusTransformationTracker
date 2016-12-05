using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Conventions;

namespace KabalistusTransformationTracker.Web {
    public class NancyBootstrapper : DefaultNancyBootstrapper {
        protected override void ConfigureConventions(NancyConventions nancyConventions) {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("res", "Resources"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}
