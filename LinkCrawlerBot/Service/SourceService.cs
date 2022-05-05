using LinkCrawlerBot.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawlerBot.Service
{
    class SourceService
    {
        private string query = "SELECT * FROM Sources";
        public List<Source> GetAll()
        {
            List<Source> sources = new List<Source>();
            try
            {
                using (var cnn = DbHelper.connect())
                {
                    cnn.Open();
                    var command = new SqlCommand(query, cnn);
                    var data = command.ExecuteReader();
                    while (data.Read())
                    {
                        var source = new Source()
                        {
                            Id = data.GetInt32(0),
                            Url = data.GetString(2),
                            LinkSelector = data.GetString(3),
                            TitleDetailSelector = data.GetString(4),
                            ContentDetailSelector = data.GetString(5),
                            ImageDetailSelector = data.GetString(6),
                            CategoryId = data.GetInt32(7),
                        };
                        sources.Add(source);
                        Console.WriteLine(source.ToString());
                    }
                    return sources;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;

            }
        }
        //public HashSet<Articles>
    }
}
