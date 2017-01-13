using System;
using System.Collections.Generic;
using System.Linq;
using KabalistusCommons.Utils;
using WMPLib;

namespace IsaacFun.Player {
    public class SoundFunPlayer {

        private static readonly WindowsMediaPlayer WmPlayer = new WindowsMediaPlayer();
        public static readonly Dictionary<int, SoundFunEntity> Entities = new Dictionary<int, SoundFunEntity>();
        private static readonly List<int> TouchedItems = new List<int>();

        private static bool _playedByUser;
        private static bool _pausedByUser;

        public static void PlaySound(string file, bool playedByUser = true) {
            _playedByUser = playedByUser;
            WmPlayer.URL = FileUtils.GetFullPath(file);
            WmPlayer.controls.play();
        }

        public static void PlayList(List<string> files, bool playedByUser = true) {
            _playedByUser = playedByUser;
            var playlist = WmPlayer.playlistCollection.newPlaylist("isaacSoundFun");
            var i = playlist.count;
            files.ForEach(file => {
                var media = WmPlayer.newMedia(FileUtils.GetFullPath(file));
                playlist.appendItem(media);
            });

            WmPlayer.currentPlaylist = playlist;
            WmPlayer.controls.play();
        }

        public static void Pause(bool pausedByUser = true) {
            if (IsPlaying() && !ShouldIgnorePause(pausedByUser)) {
                _pausedByUser = pausedByUser;
                WmPlayer.controls.pause();
            }
        }
        public static void Resume(bool playedByUser = true) {
            if (!IsPlaying() && Position() > 0 && !ShouldIgnoreResume(playedByUser)) {
                _playedByUser = playedByUser;
                WmPlayer.controls.play();
            }
        }

        public static void PausePlay(bool askedByUser = true) {
            if (IsPlaying() && !ShouldIgnorePause(askedByUser)) {
                _pausedByUser = askedByUser;
                WmPlayer.controls.pause();
            } else if (!IsPlaying()) {
                _playedByUser = askedByUser;
                WmPlayer.controls.play();
            }
        }

        public static void ResetTouchedItems() {
            TouchedItems.Clear();
        }

        public static bool IsPlaying() {
            return WmPlayer.playState == WMPPlayState.wmppsPlaying;
        }

        public static string LoadedSound() {
            return WmPlayer.currentMedia?.name;
        }

        public static string GetProgess() {
            return GetPosition() + " / " + GetDuration();
        }

        public static string GetDuration() {
            var duration = WmPlayer.currentMedia == null ? 0 : Math.Round(WmPlayer.currentMedia.duration);
            return ToDurationString(duration);
        }

        public static string GetPosition() {
            return ToDurationString(Position());
        }

        private static double Position() {
            return WmPlayer.currentMedia == null ? 0 : WmPlayer.controls.currentPosition;
        }

        public static void CheckPlaySound(List<int> currentTouchedItems, bool startingCheck = false) {
            var newTouchedItems = currentTouchedItems.Except(TouchedItems).ToList();
            if (!newTouchedItems.Any(itemId => Entities.Keys.Contains(itemId))) {
                TouchedItems.Clear();
                TouchedItems.AddRange(currentTouchedItems);
                return;
            }

            var toPlayId = newTouchedItems.First(itemId => Entities.Keys.Contains(itemId));
            TouchedItems.Clear();
            TouchedItems.AddRange(currentTouchedItems);

            if (startingCheck) return;
            PlaySound(Entities[toPlayId].SoundFile, false);
        }

        private static bool ShouldIgnorePause(bool pausedByUser) {
            return _playedByUser && !pausedByUser;
        }

        private static bool ShouldIgnoreResume(bool playedByUser) {
            return _pausedByUser && !playedByUser;
        }

        private static string ToDurationString(double duration) {
            var intDuration = (int)duration;
            var seconds = intDuration % 60;
            var minutes = intDuration / 60;
            return minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }
}
