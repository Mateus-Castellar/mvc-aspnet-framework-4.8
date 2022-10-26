using App.Infra.Data;
using System.Data.Entity.Migrations;

namespace App.Infra.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MvcContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}