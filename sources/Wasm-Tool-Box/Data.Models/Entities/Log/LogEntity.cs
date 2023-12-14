namespace Data.Models.Entities.Log
{
    public class LogEntity: AEntity
    {
        public string Message { get; set; } = string.Empty;
        public string ExMessage { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Trigger { get; set; } = string.Empty;

    }
}
