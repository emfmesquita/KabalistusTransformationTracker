namespace KabalistusCommons.Utils {
    public class Status {

        public static readonly Status ProcessNotFound = new Status(false, "Isaac proccess not found. Still searching...");
        public static readonly Status LoadingAddresses = new Status(false, "Isaac proccess found. Loading memory addresses...");
        public static readonly Status ReadyStatus = new Status(true, "Ready.");

        private Status(bool ready, string message) {
            Ready = ready;
            Message = message;
        }

        public bool Ready { get; private set; }
        public string Message { get; private set; }
    }
}
