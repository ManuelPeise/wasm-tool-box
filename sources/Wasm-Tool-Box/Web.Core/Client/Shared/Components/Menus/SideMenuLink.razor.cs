using Microsoft.AspNetCore.Components;

namespace Web.Core.Client.Shared.Components.Menus
{
    public partial class SideMenuLink : ComponentBase
    {
        [Parameter]
        public string? To { get; set; }
        [Parameter]
        public string? Value { get; set; }
    }
}
