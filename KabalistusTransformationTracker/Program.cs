using System;
using System.Timers;
using System.Windows.Forms;
using KabalistusCommons.Utils;
using Timer = System.Timers.Timer;

namespace KabalistusTransformationTracker {
    public static class Program {

        private static MainForm _mainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _mainForm = new MainForm();

            MemoryReader.Init((status) => {
                FormUtils.SetStatusAsync(status, _mainForm.statusLabel, _mainForm);
                _mainForm.UpdateTransformationsView();
            });

            Application.Run(_mainForm);
        }
    }
}
