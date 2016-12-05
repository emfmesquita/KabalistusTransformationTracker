namespace KabalistusTransformationTracker.Web {
    public class KttNancyModule : Nancy.NancyModule {
        public KttNancyModule() {
            Get["/"] = _ => View["Resources/index.html"];
            Get["/status"] = _ => TransformationsWebHelper.GetTransformationWebInfo();
        }
    }
}
