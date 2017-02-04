using KabalistusCommons.Model;

namespace KabalistusIsaacTools {
    public class StatusBarModel : BaseModel {
        private string _status = "";

        public StatusBarModel() {
            Status = "Isaac proccess not found. Still searching...";
        }

        public string Status {
            get {
                return _status;
            }

            set {
                if (value == _status) return;
                _status = value;
                NotifyPropertyChanged();
            }
        }
    }
}
