using BPDTS_Test_API.Models.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BPDTS_Test_API.Pipelines
{
    public class HttpPipeline : IHttpPipeline
    {
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiTimeout;

        public HttpPipeline(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
            _apiTimeout = _config["TestApi:TimeoutLength"];
        }

        public async Task<HttpResponseMessage> Get(string uri)
        {
            HttpClient client = _clientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(double.Parse(_apiTimeout));
            HttpResponseMessage responseMessage = await client.GetAsync(uri);
            return responseMessage;
        }
    }
}