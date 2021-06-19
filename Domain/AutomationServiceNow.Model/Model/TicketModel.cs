namespace AutomationServiceNow.Model.Model
{
    public class TicketModel
    {
        public TicketModel()
        {
            u_type = "incident";
        }

        public string short_description { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string u_type { get; }
        public string caller_id { get; set; }
        public string u_dsp_affected_person { get; set; }
        public string assignment_group { get; set; }
    }
}