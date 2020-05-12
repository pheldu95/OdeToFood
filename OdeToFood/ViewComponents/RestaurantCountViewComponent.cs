using System;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;

namespace OdeToFood.ViewComponents
{
    public class RestaurantCountViewComponent
        : ViewComponent
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        //IViewComponentResult incapsulates what will happen next. will just render
        public IViewComponentResult Invoke()
        {
            var count = restaurantData.GetCountOfRestaurants();
            //instead of returning Page(); we are returning a View. and we can pass it the count
            return View(count);
        }
    }
}
