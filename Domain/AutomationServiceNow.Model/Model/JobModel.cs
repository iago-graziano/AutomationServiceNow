namespace AutomationServiceNow.Model.Model
{
    public class JobModel
    {
        public JobModel()
        {

        }

        public string Process { get; set; }
        public string JobSeq { get; set; }
        public string Job { get; set; }
        public string JobName { get; set; }
        public string ProcessName { get; set; }
        public string ProcessType { get; set; }
        public string RunStatus { get; set; }
        public int RetryCount { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
    }
}
