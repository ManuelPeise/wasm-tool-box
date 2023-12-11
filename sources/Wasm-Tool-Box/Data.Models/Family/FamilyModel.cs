namespace Data.Models.Family
{
    public class FamilyModel
    {
        public Guid FamilyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int MembersCount { get; set; }
    }
}
