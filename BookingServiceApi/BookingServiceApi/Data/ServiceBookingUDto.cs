namespace BookingServiceApi.Data
{
    public class ServiceBookingUDto
    {
        public int ID { get; set; }
        public string Status { get; set; }
        public DateTimeOffset ExecuteDateTime { get; set; }
        public string TotalPrice { get; set; }
        public string Currency { get; set; }
        public string SystemValue { get; set; }
        public string ChangedBy { get; set; }
    }
}

