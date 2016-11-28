using System;
using System.Timers;
using System.Windows.Forms;
using KabalistusTransformationTracker.Utils;
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

            var updateTransformationsViewTimer = new Timer(1000);
            updateTransformationsViewTimer.Elapsed += UpdateTransformationsViewEvent;
            updateTransformationsViewTimer.AutoReset = true;
            updateTransformationsViewTimer.Enabled = true;

            var checkIsaacRunningTimer = new Timer(1000);
            checkIsaacRunningTimer.Elapsed += CheckIsaacRunningEvent;
            checkIsaacRunningTimer.AutoReset = true;
            checkIsaacRunningTimer.Enabled = true;

            Application.Run(_mainForm);
        }

        private static void UpdateTransformationsViewEvent(object source, ElapsedEventArgs e) {
            _mainForm.UpdateTransformationsView();
        }

        private static void CheckIsaacRunningEvent(object source, ElapsedEventArgs e) {
            MemoryReader.Init(_mainForm);
        }
    }
}
