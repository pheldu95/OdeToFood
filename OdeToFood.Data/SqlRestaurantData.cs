using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

//this using all our same InMemoryRestaurantData stuff, but now it's modifying the db
//no changes will be made until someone calls the Commit 
namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            //Find is built in. will just find the single restaurant in the db. or else return null
            return db.Restaurants.Find(id);
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            //the entity framework translates this into a sql query
            var query = from r in db.Restaurants
                         where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                         orderby r.Name
                         select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            //attach tells the entity framework that we are giving an object that has information
            //that is already in the database. but we want you (the entity framework) to track changes made
            //The entity framework will know that you arent adding a new restaurant.
            //the db context will then manage the specific restaurant
            //when save changes is clicked, the framework will know it needs to issue an update to the db
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}
