using Capela.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;
namespace Capela.Web.Business
{
        public class LocalDatabase: IdentityDbContext<ApplicationUser>
        {
                private const string productionServer = @"ea5e1895-5cab-43b0-adba-a47d00e679e1.sqlserver.sequelizer.com";
                private const string productionDatabase = @"dbea5e18955cab43b0adbaa47d00e679e1";
                private const string stagggingServer = @"bc98f96a-841b-49a6-bb4a-a483000d75b7.sqlserver.sequelizer.com";
                private const string staggingDatabase = @"dbbc98f96a841b49a6bb4aa483000d75b7";
                private const string userId = @"kezyyntwfwtazfgs";
                private const string social = @"QorzC2xr5ghAvZVwg846ztikZP8i7MrxoVpxKPNGUMcZytEugy26rtswffHV3psr";

                public LocalDatabase()
                        : base("DefaultConnection")
                {

                }

                public static LocalDatabase Create()
                {
                        return new LocalDatabase();
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
        }
}