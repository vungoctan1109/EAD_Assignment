using EAD_Assignment.Models;
using Elasticsearch.Net;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAD_Assignment.Service
{
    public class ElasticSearchService
    {
        private static ElasticClient searchClient;
        private static string IndexName = "articles";
        private static string DefaultIndexName = "example-index";
        private static string ElasticSearchUser = "elastic";
        private static string ElasticSearchPassword = "vpyP78clGXaFpt8VcEZYqLyp";
        private static string CloudId = "My_deployment:YXNpYS1zb3V0aGVhc3QxLmdjcC5lbGFzdGljLWNsb3VkLmNvbSRmN2RjZTFiMDQ3ZTY0MTA2ODY0NjY5YTdhY2Q2MWYyZiRkZDQzZGIwNjAzZmQ0N2Y2YTAyZGQ5NmUzMTExNDUzOQ==";

        public static ElasticClient GetInstance()
        {
            if (searchClient == null)
            {
                var settings = new ConnectionSettings(CloudId,
                    new BasicAuthenticationCredentials(
                        ElasticSearchUser,
                        ElasticSearchPassword))
                        .DefaultIndex(DefaultIndexName)
                        .DefaultMappingFor<Article>(
                            i => i.IndexName(IndexName));
                searchClient = new ElasticClient(settings);
            }
            return searchClient;
        }
    }
}