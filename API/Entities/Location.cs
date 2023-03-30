namespace API.Entities
{
    public class Location
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; } 

        public virtual MeetUp MeetUp { get; set; }
        public int MeetUpId { get; set; }
    }
}
