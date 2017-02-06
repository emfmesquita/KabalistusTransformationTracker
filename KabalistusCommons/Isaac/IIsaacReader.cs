using System.Collections.Generic;

namespace KabalistusCommons.Isaac {
    public interface IIsaacReader {
        bool HasItem(Item item);

        List<int> GetItemsTouchedList();

        List<Trinket> GetSmeltedTrinkets(); 

        bool IsItemBlacklisted(Item item);

        int GetFloorCurses();

        int GetTimeCounter();

        bool IsGamePaused();

        List<Pill> GetPillPool();

        Dictionary<int, int> GetPillCount();

        int IndexOfLastPillTaken();
    }
}
