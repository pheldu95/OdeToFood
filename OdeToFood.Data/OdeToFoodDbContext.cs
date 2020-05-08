using System;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class OdeToFoodDbContext : DbContext
    {
        public OdeToFoodDbContext(DbContextOptions<OdeToFoodDbContext> options)
            : base(options)
        {
            
        }

        //now we add properties for the data we want to store in the db
        //Dbset makes it so we can update delete insert etc.
        public DbSet<Restaurant> Restaurants { get; set; }
        
    }
}
