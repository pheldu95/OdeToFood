using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;
namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
    }
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
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }
    }
}
