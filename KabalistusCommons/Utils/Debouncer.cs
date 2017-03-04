using System;
using System.Timers;

namespace KabalistusCommons.Utils {
    public class Debouncer<T> {

        private bool _counting;
        private readonly Timer _timer;
        private T _currentData;


        public Debouncer(double interval, Action<T> action)
        {
            _timer = new Timer(interval) {AutoReset = false};
            _timer.Elapsed += (sender, args) => {
                action(_currentData);
                _currentData = default(T);
                _counting = false;
            };
        }

        public void Tick(T data) {
            _currentData = data;
            if (!_counting) {
                _counting = true;
                _timer.Start();
            } else {
                _timer.Stop();
                _timer.Start();
            }
        }
    }
}
