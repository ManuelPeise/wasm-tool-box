using Data.Models.UI.SideMenu;
using Microsoft.AspNetCore.Components;

namespace Web.Core.Client.Shared.Components.Menus
{
    public partial class SideMenuItem: ComponentBase
    {
        [Parameter]
        public SideMenuItemModel ItemModel { get; set; } = new SideMenuItemModel();
    }
}
