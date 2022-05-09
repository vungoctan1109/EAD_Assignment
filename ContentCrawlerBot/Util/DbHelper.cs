using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCrawlerBot
{
    internal class DbHelper
    {
        private static readonly string server = @"tcp:t2012e-sem3.database.windows.net,1433";
        private static readonly string db = "t2012e-sem3";
        private static readonly string id = "vntan1109";
        private static readonly string password = "Gichadc1109";
        private static SqlConnection cnn;

        public static SqlConnection connect()
        {
            if (cnn == null || cnn.State == System.Data.ConnectionState.Closed)
            {
                var connectionString = @"Server=" + server + ";Initial Catalog=" + db + ";Persist Security Info=False;User ID=" + id + ";Password=" + password + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                cnn = new SqlConnection(connectionString);
            }
            return cnn;
        }
    }
}