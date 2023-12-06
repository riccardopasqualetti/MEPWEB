using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mep01Web.Type.Dropdown
{
    public class TbcpList
    {
        public List<SelectListItem> TbcpListLight;

        public TbcpList()
        {
            TbcpListLight = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "",
                    Value = "null"
                }
            };
        }
    }
}
