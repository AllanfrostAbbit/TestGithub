namespace BookingServiceApi.Data
{
    public class ServiceBookingCDto
    {
        public Guid SBGuid { get; set; }
        public int ID { get; set; } = 0;
        public string Status { get; set; }
        public int CompanyID { get; set; }
        public int CustomerID { get; set; }
        public string ISCountry { get; set; }
        public string ISAddressLine { get; set; }
        public DateTimeOffset ExecuteDateTime { get; set; }
        public string Phone { get; set; }
        public string ServicesCsv { get; set; }
        public bool Paid { get; set; }
        public string PaymentType { get; set; }
        public string TotalPrice { get; set; }
        public string Currency { get; set; }
        public string SystemValue { get; set; }
        public string ChangedBy { get; set; }

    }
}
