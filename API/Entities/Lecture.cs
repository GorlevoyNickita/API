namespace API.Entities
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }

        public virtual MeetUp MeetUp { get; set; }
        public int MeetUpId { get; set; }
    }
}
