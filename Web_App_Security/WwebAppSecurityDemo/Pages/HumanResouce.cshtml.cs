using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WwebAppSecurityDemo.Pages
{
    [Authorize]
    public class HumanResouceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
