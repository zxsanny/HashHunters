namespace HashHunters.MinerMonitor.Common.DTO
{
    public class Miner
    {
        public string ProgramName { get; set; }
        public string ProgramFolder { get; set; }
        public string Parameters { get; set; }

        public Miner(string programName, string parameters = "", string programFolder = "")
        {
            ProgramName = programName;
            ProgramFolder = programFolder;
            Parameters = parameters;
        }
    }
}
