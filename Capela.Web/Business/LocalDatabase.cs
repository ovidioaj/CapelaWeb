using Capela.Web.Entities;
using Capela.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
namespace Capela.Web.Business
{
        public class LocalDatabase: IdentityDbContext<ApplicationUser>
        {
                private const string productionServer = @"ea5e1895-5cab-43b0-adba-a47d00e679e1.sqlserver.sequelizer.com";
                private const string productionDatabase = @"dbea5e18955cab43b0adbaa47d00e679e1";
                private const string stagggingServer = @"3bfc5672-d21f-44e8-a5e0-a4d401664fb4.sqlserver.sequelizer.com";
                private const string staggingDatabase = @"db3bfc5672d21f44e8a5e0a4d401664fb4";
                private const string userId = @"ajzcpwocfejcokoo";
                private const string social = @"fRpJ8zT7xyCezec4QLEKY6HY5LhfMJHgqTWnzEXHJ5QmyEMhgAx735ShhxNusNtW";

                public DbSet<CapelaGroup> CapelaGroups { get; set; }

                public LocalDatabase()
                        : base(GetConnection())
                {
                        try {
                                Database.CommandTimeout = 30;
                        }
                        catch (Exception ex) {
                                throw new Exception(ex.ToString(), ex);
                        }
                }

                public static LocalDatabase Create()
                {

                        return new LocalDatabase();
                }

                static LocalDatabase()
                {
                        System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<LocalDatabase, Migrations.Configuration>());
                }

                private static string GetConnection()
                {
                        var connection = new SqlConnectionStringBuilder();

                        connection.DataSource = stagggingServer;
                        connection.InitialCatalog = staggingDatabase;
                        connection.UserID = userId;
                        connection.Password = social;

                        return connection.ConnectionString;
                }

                public System.Data.Entity.DbSet<Capela.Web.Entities.CapelaEvents> CapelaEvents { get; set; }
        }
}