using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;
namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
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
        public IEnumerable<Restaurant> GetAll()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }
    }
}
