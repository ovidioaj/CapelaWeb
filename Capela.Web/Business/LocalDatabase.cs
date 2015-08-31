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
                private const string productionServer = @"dcaeb7d0-f53b-43cd-af5c-a50500de5173.sqlserver.sequelizer.com";
                private const string productionDatabase = @"dbdcaeb7d0f53b43cdaf5ca50500de5173";
                private const string userId = @"zzytpfmbsphvfsgb";
                private const string social = @"nHQHvnPrVWjUTxwPCJD5SPD8n6EanwQLYGUgT8SQoP8XcQsumLvKotsFeRuyQPdy";

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
                        Database.SetInitializer(new MigrateDatabaseToLatestVersion<LocalDatabase, Migrations.Configuration>());
                }

                private static string GetConnection()
                {
                        var connection = new SqlConnectionStringBuilder();

                        connection.DataSource = productionServer;
                        connection.InitialCatalog = productionDatabase;
                        connection.UserID = userId;
                        connection.Password = social;

                        return connection.ConnectionString;
                }

                public DbSet<CapelaEvents> CapelaEvents { get; set; }
        }
}