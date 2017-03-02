using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace YSVintageShop.Models
{
    public class YSDbConfiguration : DbConfiguration
    {
        public YSDbConfiguration()
        {
            //SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}
