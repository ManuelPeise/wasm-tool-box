using Data.Models.UI.SideMenu;
using Microsoft.AspNetCore.Components;

namespace Web.Core.Client.Shared.Components.Menus
{
    public partial class SideMenu : ComponentBase
    {
        private SideMenuModel _sideMenu = new SideMenuModel();

        protected override void OnInitialized()
        {
            LoadSideMenue();
        }

        private void LoadSideMenue()
        {
            _sideMenu = new SideMenuModel
            {
                Header = "Menu",
                MenuItems = new List<SideMenuItemModel>
                {
                    new SideMenuItemModel
                    {
                         Id = Guid.NewGuid().ToString(),
                         Value = "Test",
                         Expanded = false,
                         Disabled = false,
                         Icon = "",
                         Items = new List<SideMenuSubItemModel>
                         {
                             new SideMenuSubItemModel
                             {
                                  To = "/test-1",
                                  Value = "Test-1",
                             },
                              new SideMenuSubItemModel
                             {
                                  To = "/test-2",
                                  Value = "Test-2",
                             }
                         }
                    },
                    new SideMenuItemModel
                    {
                         Id = Guid.NewGuid().ToString(),
                         Value = "Test-1",
                         Expanded = false,
                         Disabled = false,
                         Icon = "",
                         Items = new List<SideMenuSubItemModel>
                         {
                             new SideMenuSubItemModel
                             {
                                  To = "/testx-1",
                                  Value = "Testx-1",
                             },
                              new SideMenuSubItemModel
                             {
                                  To = "/testx-2",
                                  Value = "Testx-2",
                             }
                         }
                    }
                }
            };
        }
    }
}
