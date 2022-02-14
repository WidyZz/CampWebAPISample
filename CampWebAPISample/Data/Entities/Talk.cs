namespace CampWebAPISample.Data.Entities
{
    public class Talk
    {
        public int TalkId { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public Camp Camp { get; set; }
        public Speaker Speaker { get; set; }
    }
}
