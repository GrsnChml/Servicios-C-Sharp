using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MySql.Data.Entity;


namespace HolaMundo.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        //This 'DefaultConnection' should be equal to the connection string name on Web.config.
        public DataContext() : base("DefaultConnection") 
        {
            this.Configuration.ValidateOnSaveEnabled = false;
        }
    }

}