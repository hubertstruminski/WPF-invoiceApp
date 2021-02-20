namespace ClassLibrary
{
    public class Address
    {
        public long Id { get; set; }
        public string AddressName { get; set; }
        public string Country { get; set; }
        public Customer Customer { get; set; }
    }
}
