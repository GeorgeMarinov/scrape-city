using NUnit.Framework;
using ScrapeCity.Data;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;

namespace ScrapeCity.Tests
{
    [Order(0)]
    public class DatabaseSetup
    {
        // you don't want any of these executed automatically
        [OneTimeSetUp] 
        public void WipeAndCreateDatabase()
        {
            // drop database first
            ReallyDropDatabase(ConnectionString);

            // Now time to create the database from migrations
            // MyApp.Data.Migrations.Configuration is migration configuration class 
            // this class is crated for you automatically when you enable migrations
            var initializer = new MigrateDatabaseToLatestVersion<ScrapeCityDbContext, ScrapeCity.Data.Migrations.Configuration>();

            // set DB initialiser to execute migrations
            Database.SetInitializer(initializer);

            // now actually force the initialisation to happen
            using (var context = new ScrapeCityDbContext())
            {
                context.Database.Initialize(true);
            }

            // And after the DB is created, you can put some initial base data 
            // for your tests to use
            //using (var context = new ScrapeCityDbContext(/*connectionString*/))
            //{
            //    SeedContextForTests(context);
            //}
        }

        // this method is only updates your DB to latest migration.
        // does the same as if you run "Update-Database" in nuget console in Visual Studio
        [Test, Ignore("Only for manual execution")]
        public void UpdateDatabase()
        { 
            var migrationConfiguration = new ScrapeCity.Data.Migrations.Configuration();

            migrationConfiguration.TargetDatabase = new DbConnectionInfo(ConnectionString, "System.Data.SqlClient");

            var migrator = new DbMigrator(migrationConfiguration);

            migrator.Update();
        }


        /// <summary>
        /// Drops the database that is specified in the connection string.
        /// 
        /// Drops the database even if the connection is open. Sql is stolen from here:
        /// http://daniel.wertheim.se/2012/12/02/entity-framework-really-do-drop-create-database-if-model-changes-and-db-is-in-use/
        /// </summary>
        /// <param name="connectionString"></param>
        private static void ReallyDropDatabase(String connectionString)
        {
            const string DropDatabaseSql =
            @"if (select DB_ID('{0}')) is not null 
             begin
                alter database [{0}] set offline with rollback immediate;
                alter database [{0}] set online
                drop database [{0}];
             end";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    var sqlToExecute = String.Format(DropDatabaseSql, connection.Database);

                    var command = new SqlCommand(sqlToExecute, connection);

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlException)
            {
                if (sqlException.Message.StartsWith("Cannot open database"))
                {
                    Console.WriteLine("Database does not exist.");
                    return;
                }
                throw;
            }
        }

        private static string ConnectionString => ConfigurationManager.ConnectionStrings["ScrapeCityDbContext"].ConnectionString;
    }
}
