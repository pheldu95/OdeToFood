using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData restaurantData;
        private readonly IHtmlHelper htmlHelper;

        //Restaurant property that holds the restaurant we are editing
        //BindProperty binds this property to the post. so that we can save what the user has typed in?
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        //property that contains the list of cuisines.
        //this will help us build our html select 
        public  IEnumerable<SelectListItem> Cuisines { get; set; }

        //this is a constructor
        public EditModel(IRestaurantData restaurantData,
                         IHtmlHelper htmlHelper)
        {
            this.restaurantData = restaurantData;
            this.htmlHelper = htmlHelper;
        }

        //return type is changed from void to IActionResult
        //the ? after int means its ok if it is null. doesnt need a restaurant id
        //this is so we can use this same page to post a new restaurant. instead of only editing
        public IActionResult OnGet(int? restaurantId)
        {
            //we can use Cuisines over on our view to make our select list. using Model.Cuisines
            Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();

            //we will only go get the restaurant if the id is not null.
            if(restaurantId.HasValue)
            {
                //the restaurant is what happens when we go to restaurantData and get the restaurant with the specific id
                Restaurant = restaurantData.GetById(restaurantId.Value);
            }
            else
            {
                //if the id is null, then it means the user is trying to create a new
                //restaurant. so we make a new one for them
                Restaurant = new Restaurant();
            }
     
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            //use Model State to check if all the information that was sent from the post
            //is valid or not. did it pass the Required and StringLength data annotations?
            //if it is valid, then we save it. if not valid, present the edit form again for user to fix errors
           if(!ModelState.IsValid)
            {
                //need to build the Cuisine select items again.
                //asp.net is stateless, so will not automatically have all the
                //info for cuisine still. need to build it again
                Cuisines = htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();

            }

            //if the id is greater than 0, that means its an edit. so we Update
            if (Restaurant.Id > 0)
            {
                restaurantData.Update(Restaurant);

            }
            //id isn't greater than 0, so we know we are adding a restaurant, not editing
            else
            {
                restaurantData.Add(Restaurant);
            }
            restaurantData.Commit();
            //we need to send a confirmation message to the user
            //we will use TempData. Add an object called Message to tempdata
            //tempdata is temporary. after the next request, the temp data disappears 
            TempData["Message"] = "Restaurant Saved!";
            //after save is clicked, the user will be redirected to the detail page
            //the details page needs the restaurant id to get the info for it
            //so we send an anonymous object with the restaurant id in it
            return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id });

            //need some kind of form validation in case user leaves fields empty
            //asp.net core has an easy way to do this. no if else statements
            //add attributes to your model objects. Add attributes to Restaurant in OdeToFood.Core
            //we added the [Required] and [StringLength] data annotations to the Restaurant.cs file in OdeToFood.Core
            //look in that file to find more info on what that does.


        }
    }
}