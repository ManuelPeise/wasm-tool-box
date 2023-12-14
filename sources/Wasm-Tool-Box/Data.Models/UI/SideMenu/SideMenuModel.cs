namespace Data.Models.UI.SideMenu
{
    public class SideMenuModel
    {
        public string? Header { get; set; }
        public List<SideMenuItemModel> MenuItems { get; set; } = new List<SideMenuItemModel>();
    }
}
