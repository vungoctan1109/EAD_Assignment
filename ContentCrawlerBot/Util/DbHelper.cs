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
            if (cnn == null)
            {
                var connectionString = @"Server=" + server + ";Initial Catalog=" + db + ";Persist Security Info=False;User ID=" + id + ";Password=" + password + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                cnn = new SqlConnection(connectionString);
                Console.WriteLine("Action success.");
            }
            return cnn;
        }

        internal static string checkExistUrl(string url, SqlConnection cnn)
        {
            SqlCommand command = new SqlCommand("SELECT Url FROM Articles WHERE Url = @url", cnn);
            command.Prepare();
            command.Parameters.AddWithValue("@url", url);
            string existUrl = (string)command.ExecuteScalar();
            return existUrl;
        }
    }
}