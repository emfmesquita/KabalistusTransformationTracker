namespace IsaacFun.Player {
    public class SoundFunEntity {

        public SoundFunEntity(string soundFile, int itemId) {
            SoundFile = soundFile;
            ItemId = itemId;
        }

        public string SoundFile { get; private set; }
        public int ItemId { get; private set; }
    }
}
