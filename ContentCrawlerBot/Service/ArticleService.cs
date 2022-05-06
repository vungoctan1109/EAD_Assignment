using ContentCrawlerBot.Models;
using HtmlAgilityPack;
using LinkCrawlerBot.Queue;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentCrawlerBot.Service
{
    class ArticleService
    {
        private readonly string SelectAllQuery = "SELECT * FROM Articles";
        private readonly string GetArticleByUrlQuery = "SELECT * FROM Articles WHERE Url = @Url";
        private readonly string InsertArticleQuery = "INSERT INTO" +
            " Articles(Url, Title, ImageUrl, Detail, Description, CategoryId, CreatedAt, UpdatedAt, Status)" +
            " VALUES ( @Url, @Title, @ImageUrl, @Detail, @Description, @CategoryId, @CreatedAt, @UpdatedAt, @Status) ";
        private readonly HtmlWeb web;
        public ArticleService()
        {
            web = new HtmlWeb();
        }
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
        public Article GetArticleByUrl(string url)
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
                    return data.Read() ? CreateArticle(data) : null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Article Save(Article article)
        {
            try
            {
                using (var cnn = DbHelper.connect())
                {
                    cnn.Open();
                    var command = new SqlCommand(InsertArticleQuery, cnn);
                    command.Prepare();
                    command.Parameters.AddWithValue("@Url", article.Url);
                    command.Parameters.AddWithValue("@Title", article.Title);
                    command.Parameters.AddWithValue("@ImageUrl", article.ImageUrl);
                    command.Parameters.AddWithValue("@Detail", article.Detail);
                    command.Parameters.AddWithValue("@Description", article.Description);
                    command.Parameters.AddWithValue("@CategoryId", article.CategoryId);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@Status", 0);
                    command.ExecuteNonQuery();
                    return article;
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
        public Article GetArticleFromQueue(Event eventQueue)
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                HtmlDocument doc = web.Load(eventQueue.Url);
                string title = doc.QuerySelector(eventQueue.TitleDetailSelector)?.InnerText;
                string description = doc.QuerySelector(eventQueue.DescriptionDetailSelector)?.InnerText;
                var image = doc.QuerySelector(eventQueue.ImageDetailSelector)?.GetAttributeValue("data-src", string.Empty);
                var contentNode = doc.QuerySelectorAll(eventQueue.ContentDetailSelector);
                StringBuilder contentSB = new StringBuilder();
                foreach (var content in contentNode)
                {
                    contentSB.Append(content.InnerHtml);
                }

                Article article = new Article()
                {
                    Url = eventQueue.Url,
                    Title = title,
                    ImageUrl = image,
                    Description = description,
                    Detail = contentSB.ToString(),
                    CategoryId = eventQueue.CategoryId
                };
                if (article == null || article.Title == null || article.ImageUrl == null || article.Detail == null || article.Description == null)
                {
                    return null;
                }
                return article;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
