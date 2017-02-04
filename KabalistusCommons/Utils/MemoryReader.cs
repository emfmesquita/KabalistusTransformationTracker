using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace KabalistusCommons.Utils {
    public class MemoryReader {

        private const int ProcessWmRead = 0x0010;

        private static int _isaacPid;
        private static ProcessModule _module;
        private static int _baseAddr;
        private static int _moduleMemSize;
        private static IntPtr _processHandle;

        private static int _playerManagerInstructPointer;
        private static int _playerManagerPlayerListOffset;

        private static IsaacVersion? _version;

        private static bool _loadingMemory;

        public static MemoryQuery VersionQuery = new MemoryQuery() {
            SearchChar = new[] { 'B', 'i', 'n', 'd', 'i', 'n', 'g', ' ', 'o', 'f', ' ', 'I', 's', 'a', 'a', 'c', ':' },
            SearchPattern = "bbbbbbbbbbbbbbbbb"
        };

        public static MemoryQuery PlayerManagerInstructPointerQuery = new MemoryQuery() {
            SearchInt = new[] { 0x33, 0xC0, 0xC7, 0x45, 0xFC, 0xFF, 0xFF, 0xFF, 0xFF, 0xA3, 0x00, 0x00, 0x00, 0x00, 0xE8 },
            SearchPattern = "bbbbbbbbbbvvvvb"
        };

        public static MemoryQuery PlayerManagerPlayerListOffsetQuery = new MemoryQuery() {
            SearchInt = new[] { 0x8B, 0x35, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x86, 0x00, 0x00, 0x00, 0x00, 0x2B, 0x86, 0x00, 0x00, 0x00, 0x00 },
            SearchPattern = "bb????bb????bbvv??"
        };

        public static void Init(Action<Status> callback, double interval = 1000) {
            var checkIsaacRunningTimer = new Timer(interval);
            checkIsaacRunningTimer.Elapsed += (source, e) => {
                Update(callback);
            };
            checkIsaacRunningTimer.AutoReset = true;
            checkIsaacRunningTimer.Enabled = true;
        }

        private static void Update(Action<Status> callback) {
            var processArray = Process.GetProcessesByName("isaac-ng");
            if (processArray.Length == 0) {
                _isaacPid = 0;
                _version = null;

                CallbackAsync(callback, Status.ProcessNotFound);
                return;
            }

            var process = processArray[0];
            var isaacPid = process.Id;

            if (_loadingMemory) {
                return;
            }

            if (isaacPid == _isaacPid) {
                CallbackAsync(callback, Status.ReadyStatus);
                return;
            }

            _loadingMemory = true;
            _isaacPid = isaacPid;

            CallbackAsync(callback, Status.LoadingAddresses);

            _module = process.MainModule;
            _baseAddr = _module.BaseAddress.ToInt32();
            _moduleMemSize = _module.ModuleMemorySize;
            _processHandle = OpenProcess(ProcessWmRead, false, isaacPid);

            var versionAddress = Search(VersionQuery, true, 800000).QueryResultAddress;
            var versionChar = InnerReadInt(versionAddress + 18, 1, true);

            var isAfterbirth = versionChar == 'A';
            if (isAfterbirth) {
                versionChar = InnerReadInt(versionAddress + 28, 1, true);
                _version = versionChar == '+' ? IsaacVersion.AfterbirthPlus : IsaacVersion.Afterbirth;
            } else {
                var isAntibirth = process.Modules.Cast<ProcessModule>().Any(module => "zhlRemix2.dll".Equals(module.ModuleName));
                _version = isAntibirth ? IsaacVersion.Antibirth : IsaacVersion.Rebirth;
            }

            var instructSearchOffset = isAfterbirth ? 1500000 : 1100000;
            _playerManagerInstructPointer = Search(PlayerManagerInstructPointerQuery, false, instructSearchOffset).QueryResult;

            var playerListSearchOffset = isAfterbirth ? 50000 : 120000;
            _playerManagerPlayerListOffset = Search(PlayerManagerPlayerListOffsetQuery, false, playerListSearchOffset).QueryResult;
            _loadingMemory = false;

            CallbackAsync(callback, Status.ReadyStatus);
        }

        public static int GetNumberOfPlayers(int playerManagetInstruct = -1) {
            if (playerManagetInstruct == -1) {
                playerManagetInstruct = ReadInt(_playerManagerInstructPointer, 4);
                if (playerManagetInstruct == 0) {
                    return 0;
                }
            }

            var playerListPointer = playerManagetInstruct + _playerManagerPlayerListOffset;
            var numberOfPlayersX4 = ReadInt(playerListPointer + 4, 4) - ReadInt(playerListPointer, 4);
            return numberOfPlayersX4 / 4;
        }

        public static IsaacVersion? GetVersion() {
            return _version;
        }

        public static int GetPlayerInfo(int offset) {
            return GetPlayerInfo(offset, 0);
        }

        public static int GetPlayer2Info(int offset) {
            return GetPlayerInfo(offset, 4);
        }

        public static int GetPlayerManagerInfo(int offset, int size) {
            var playerManagetInstruct = ReadInt(_playerManagerInstructPointer, 4);
            if (playerManagetInstruct == 0) {
                return 0;
            }
            var numberOfPlayers = GetNumberOfPlayers(playerManagetInstruct);
            return numberOfPlayers == 0 ? 0 : ReadInt(playerManagetInstruct + offset, size);
        }

        public static int ReadInt(int addr, int size) {
            return InnerReadInt(addr, size);
        }
        public static byte[] Read(int addr, int size) {
            return InnerRead(addr, size);
        }

        private static int InnerReadInt(int addr, int size, bool forceRead = false) {
            return MemoryReaderUtils.ConvertLittleEndian(InnerRead(addr, size, forceRead));
        }

        private static int GetPlayer(int playerOffset) {
            var playerManagetInstruct = ReadInt(_playerManagerInstructPointer, 4);
            if (playerManagetInstruct == 0) {
                return 0;
            }

            var numberOfPlayers = GetNumberOfPlayers(playerManagetInstruct);
            if (numberOfPlayers == 0) {
                return 0;
            }

            var playerPointer = ReadInt(playerManagetInstruct + _playerManagerPlayerListOffset, 4);
            return playerPointer == 0 ? 0 : ReadInt(playerPointer + playerOffset, 4);
        }

        private static int GetPlayerInfo(int offset, int playerOffset) {
            var player = GetPlayer(playerOffset);
            return player == 0 ? 0 : ReadInt(player + offset, 4);
        }

        private static byte[] InnerRead(int addr, int size, bool forceRead = false) {
            if (_isaacPid == 0) {
                return new byte[0];
            }

            if (!forceRead && _loadingMemory) {
                return new byte[0];
            }

            var bytesRead = 0;
            var buffer = new byte[size];
            ReadProcessMemory((int)_processHandle, addr, buffer, buffer.Length, ref bytesRead);
            return buffer;
        }

        private static MemoryQuery Search(MemoryQuery query, bool fromEndToStart = false, int offsetAddress = 0) {
            query.QueryResult = -1;
            query.QueryResultAddress = -1;

            if (query?.Search == null || query.Search.Length == 0 || string.IsNullOrEmpty(query.SearchPattern)) {
                return query;
            }

            if (query.Search.Length != query.SearchPattern.Length) {
                return query;
            }

            var searchSize = query.Search.Length;
            var pattern = query.SearchPattern;
            for (
                var i = !fromEndToStart ? offsetAddress : _moduleMemSize - offsetAddress;
                (!fromEndToStart && i < _moduleMemSize - searchSize) || (fromEndToStart && i > searchSize);
                i = !fromEndToStart ? i + 1 : i - 1) {

                var addr = _baseAddr + i;
                var read = InnerRead(addr, searchSize, true);
                var result = new List<byte>();

                if (!MemoryReaderUtils.Match(pattern, read, query.Search, result)) continue;

                query.QueryResult = MemoryReaderUtils.ConvertLittleEndian(result.ToArray());
                query.QueryResultAddress = addr;
                return query;
            }

            return query;
        }

        private static void CallbackAsync(Action<Status> callback, Status status) {
            new Task(() => callback(status)).Start();
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
    }
}
