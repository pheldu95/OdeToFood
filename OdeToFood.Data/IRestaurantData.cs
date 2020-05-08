using System.Collections.Generic;
using OdeToFood.Core;
namespace OdeToFood.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        //method that will let the user update
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant newRestaurant);
        Restaurant Delete(int id);
        //after you make a change to an entity, need to commit?
        int Commit();

    }
}
