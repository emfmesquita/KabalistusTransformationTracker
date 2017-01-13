using System;
using System.Windows.Forms;
using IsaacFun.Player;
using KabalistusCommons.Isaac;
using KabalistusCommons.Utils;
using KabalistusCommons.View;

namespace IsaacFun {
    public static class Program {
        private static MainForm _mainForm;
        private static bool _firstCheck = true;

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
                SoundFunPlayer.ResetTouchedItems();
                _firstCheck = true;
                return;
            }

            var numberOfPlayers = MemoryReader.GetNumberOfPlayers();
            if (numberOfPlayers == 0) {
                SoundFunPlayer.ResetTouchedItems();
                _firstCheck = true;
                return;
            }

            var reseted = reader.GetTimeCounter() < 2;
            // resets on hold "r"
            if (reseted) {
                SoundFunPlayer.ResetTouchedItems();
            }

            if (reader.IsGamePaused()) {
                SoundFunPlayer.Pause(false);
                return;
            }

            // tries to resume
            SoundFunPlayer.Resume(false);

            var startCheck = reseted || _firstCheck;
            _firstCheck = false;

            SoundFunPlayer.CheckPlaySound(reader.GetItemsTouchedList(), startCheck);
        }
    }
}
