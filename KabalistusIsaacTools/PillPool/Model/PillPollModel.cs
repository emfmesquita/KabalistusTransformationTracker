using System.Windows;
using KabalistusCommons.Model;

namespace KabalistusIsaacTools.PillPool.Model {
    public class PillPollModel : BaseModel {
        private Visibility _lastPillVisibility = Visibility.Hidden;

        public Visibility LastPillVisibility {
            get {
                return _lastPillVisibility;
            }

            set {
                if (value == _lastPillVisibility) return;
                _lastPillVisibility = value;
                NotifyPropertyChanged();
            }
        }
    }
}
