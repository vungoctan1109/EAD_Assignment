using LinkCrawlerBot.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkCrawlerBot.Service
{
    class ArticleService
    {
        private readonly string SelectAllQuery = "SELECT * FROM Articles";
        private readonly string GetArticleByUrlQuery = "SELECT * FROM Articles WHERE Url = @Url";
        public List<Article> GetAll()
        {
            List<Article> articles = new List<Article>();
            try
            {
                using (var cnn = DbHelper.connect())
                {
                    cnn.Open();
                    var command = new SqlCommand(SelectAllQuery, cnn);
                    var data = command.ExecuteReader();
                    while (data.Read())
                    {
                        articles.Add(CreateArticle(data));
                    }
                    return articles;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;

            }
        }

        public bool GetArticleByUrl(string url)
        {
            try
            {
                using (var cnn = DbHelper.connect())
                {
                    cnn.Open();
                    var command = new SqlCommand(GetArticleByUrlQuery, cnn);
                    command.Prepare();
                    command.Parameters.AddWithValue("@Url", url);
                    var data = command.ExecuteReader();
                    return (data.Read());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        private Article CreateArticle(SqlDataReader data)
        {
            return new Article()
            {
                Url = data.GetString(0),
                Title = data.GetString(1),
                ImageUrl = data.GetString(2),
                Detail = data.GetString(3),
                Description = data.GetString(4),
                CreatedAt = data.GetDateTime(6),
                CategoryId = data.GetInt32(8),
                Status = 0
            };
        }
    }
}
