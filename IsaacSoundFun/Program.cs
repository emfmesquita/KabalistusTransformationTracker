using System;
using System.Windows.Forms;
using IsaacFun.Player;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;

namespace IsaacFun {
    public static class Program {

        private static MainForm _mainForm;
        private static bool _soundPlayed;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var reader = new AfterbirthPlusIsaacReader();

            _mainForm = new MainForm();
            MemoryReader.Init((status) => {
                FormUtils.SetStatusAsync(status, _mainForm.statusLabel, _mainForm);
                Update(status, reader);

            }, 500);

            Application.Run(_mainForm);
        }

        private static void Update(Status status, IIsaacReader reader) {
            if (!status.Ready) {
                _soundPlayed = false;
                return;
            }

            var numberOfPlayers = MemoryReader.GetNumberOfPlayers();
            if (numberOfPlayers == 0) {
                _soundPlayed = false;
                return;
            }

            var touchedList = reader.GetItemsTouchedList();
            if (!touchedList.Contains(404)) {
                _soundPlayed = false;
                return;
            }

            if (_soundPlayed) {
                return;
            }

            _soundPlayed = true;
            FunPlayer.PlaySound();
        }
    }
}
