namespace Domain.Common
{
    public class Tracker
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? CreatedDate { get; set; }
        public string? ModifiedDate { get; set; }
    }
}
