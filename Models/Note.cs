namespace OnlineFoodWebAPI.Models
{
    public class Note
    {
        public Guid OrderId { get; set; }
        public List<Guid> MenuId { get; set; }
        public List<int> Rating { get; set; }
        public string Review {get; set;}
    }
}
