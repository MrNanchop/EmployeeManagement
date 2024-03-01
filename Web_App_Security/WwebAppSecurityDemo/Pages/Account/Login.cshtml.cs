using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace WwebAppSecurityDemo.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credentials Credentials { get; set; }=new Credentials();
        public void OnGet()
        {
            
        }
       
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid) return Page();

            // Creating the credentials
            if(Credentials.UserName=="admin" && Credentials.Password=="password")
            {
                //Creating the security context
                var claims = new List<Claim>()
                { 
                    new Claim(ClaimTypes.Name, Credentials.UserName),
                    new Claim(ClaimTypes.Email,"test@user.com"),
                };

                var identity=new ClaimsIdentity(claims,"MyCookieAuth");
                ClaimsPrincipal claimsPrincipal=new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                return RedirectToPage("/Index");
            }

            return Page();
        }
    }


   public class Credentials
    {
        [Display(Description ="User Name")]
        [Required]
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
