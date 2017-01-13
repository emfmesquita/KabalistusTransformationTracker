using System.Collections.Generic;

namespace KabalistusCommons.Isaac {
    public interface IIsaacReader {
        bool HasItem(Item item);

        List<int> GetItemsTouchedList();

        bool IsItemBlacklisted(Item item);

        int GetFloorCurses();

        int GetTimeCounter();

        bool IsGamePaused();
    }
}
