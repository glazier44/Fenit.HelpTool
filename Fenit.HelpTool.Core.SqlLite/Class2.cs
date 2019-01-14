using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fenit.HelpTool.Core.SqlLite
{



    public class SqlLiteContext : DbContext
    {
        public SqlLiteContext():base()
        {
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<SqlLiteContext>(null);
        }

        public DbSet<AnimalType> AnimalTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
