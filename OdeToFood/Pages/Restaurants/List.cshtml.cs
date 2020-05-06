using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Core;
using OdeToFood.Data;

//this is the page model for List.cshtml
namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        //BindProperty says that this property expects some information from the request we are sending
        //this is model binding
        //we have to add SupportsGet = true because the default BindProperty only works for a Post request
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        //give page model a constructor
        public ListModel(IConfiguration config, IRestaurantData restaurantData)
        {
            this.config = config;
            this.restaurantData = restaurantData;
        }
        public void OnGet()
        {
            //model binding. tell the ASP.NET core framework that you need an input that is named searchTerm.
            //the framework will go out and look for the input named searchTerm
            
            Message = config["Message"];
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}