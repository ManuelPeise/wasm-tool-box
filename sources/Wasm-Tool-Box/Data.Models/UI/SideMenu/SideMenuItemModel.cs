namespace Data.Models.UI.SideMenu
{
    public class SideMenuItemModel
    {
        public string Id { get; set; } = string.Empty;
        public string? Value { get; set; }
        public string? Icon { get; set; }
        public bool Disabled { get; set; }
        public bool Expanded { get; set; }
        public List<SideMenuSubItemModel> Items { get; set; } = new List<SideMenuSubItemModel>();

    }

    public class SideMenuSubItemModel
    {
        public string? Value { get; set; }
        public string? To  { get; set; }
    }
}
