namespace SignalCore.Helpers
{
    public class ErrorInfo
    {
        public string Name { get; set; }
        public string MoreInfo { get; set; }
        public string StackTrace { get; set; }

        public ErrorInfo()
        {
            Name = "Unknown error.";
            MoreInfo = "";
            StackTrace = "";
        }

        public ErrorInfo(string name, string info = "", string? trace = "")
        {
            Name = name;
            MoreInfo = info;
            StackTrace = trace ?? "";
        }
    }
}
