using WMPLib;

namespace IsaacFun.Player {
    public class FunPlayer {
        public static void PlaySound() {
            var wplayer = new WindowsMediaPlayer { URL = "f:/a.mp3" };
            //var wplayer = new WindowsMediaPlayer { URL = "f:/b.wav" };
            //var wplayer = new WindowsMediaPlayer { URL = "f:/c.ogg" };
            //var wplayer = new WindowsMediaPlayer { URL = "f:/d.mp4" };
            wplayer.controls.play();
        }
    }
}
