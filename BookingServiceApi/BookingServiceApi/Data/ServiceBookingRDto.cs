namespace BookingServiceApi.Data
{
    public class ServiceBookingRDto
    {
        public Int64 ID { get; set; }
        public string Status { get; set; }
        public string AddressLine { get; set; }
        public DateTimeOffset ExecuteDateTime { get; set; }
        public string Phone { get; set; }
        public string ServicesCsv { get; set; }
        public bool Paid { get; set; }
        public string PaymentType { get; set; }
        public string TotalPrice { get; set; }
        public string Currency { get; set; }
        public string SystemValue { get; set; }
        public DateTimeOffset LastChanged { get; set; }
        public string ChangedBy { get; set; }
    }
}
