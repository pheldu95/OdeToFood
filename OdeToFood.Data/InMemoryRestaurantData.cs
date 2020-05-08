using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;
namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian},
                new Restaurant {Id = 2, Name = "Azada", Location = "Colorado", Cuisine = CuisineType.Mexican},
                new Restaurant {Id = 3, Name = "Ganga", Location = "Yangshuo", Cuisine = CuisineType.Indian}


            };
        }
        public Restaurant GetById(int id)
        {
            //return a restaurant r if r.Id is equal to the incoming id value
            //SingleOrDefault means it will return a single match for this id, or it will default to returning a null if there is no match
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        //public method that will add the new restuarant
        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }


        public Restaurant Update(Restaurant updatedRestaurant)
        {
            //look for a match. will return one match, or if no matches, default to null
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if (restaurant != null)
            {
                //set the restaurants attributes to the updatedRestaurant's
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }
    }
}
