using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using TestJSONConvertWebApp.Model;
using TestJSONConvertWebApp.Repositories;

namespace TestJSONConvertWebApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        
        public string postResult { get; set; }

        public void OnGet()
        {

        }

       
        public async Task<IActionResult> OnPostAsync()
        {
            CustomerRepo repo = new CustomerRepo();
            postResult = await repo.addCustomer(Customer);
            return Page();
        }
    }
}
