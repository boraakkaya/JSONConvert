using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestJSONConvertWebApp.Model;
using TestJSONConvertWebApp.Repositories;

namespace TestJSONConvertWebApp.Pages
{
    public class CustomerModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CustomerRepo repo = new CustomerRepo();
            Customer = await repo.getCustomer(id);
            return Page();
        }

    }
}