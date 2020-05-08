using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        //we are binding tempdata to the Message string
        //ASP.NET Core will go automatically look for something with key of Message in tempdata
        [TempData]
        public string Message { get; set; }

        public Restaurant Restaurant { get; set; }

        public DetailModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId);
            //if the restaurant doesn't exist, we dont want to attempt to render the page
            if(Restaurant == null)
            {
                //use the RedirectToPage IACtionREsult
                //go to the Not Found page
                return RedirectToPage("./NotFound");
            }
            //Page is the IActionResult. telling ASP.NET Core to please render the page
            return Page();
        }
    }
}