using BPDTS_Test_API.Models.Interfaces;
using BPDTS_Test_API.Tests.MockData;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BPDTS_Test.API.Tests.Services
{
    public class HttpPipelineFake : IHttpPipeline
    {
        public async Task<HttpResponseMessage> Get(string uri)
        {
            var httpResponseMessage = new HttpResponseMessage();

            switch (uri)
            {
                case "/city/London/users":
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(LondonUsers.MockLondonCityUsers));
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                    break;

                case "/city/QWERTY/users":
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(LondonUsers.MockEmptyList));
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                    break;

                case "/user/1":
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(LondonUsers.MockUsers[0]));
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                    break;

                case "/user/0":
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.NotFound;
                    break;

                case "/users":
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(LondonUsers.MockUsers));
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.OK;
                    break;

                default:
                    httpResponseMessage.Content = new StringContent(JsonConvert.SerializeObject(LondonUsers.MockEmptyList));
                    httpResponseMessage.StatusCode = System.Net.HttpStatusCode.NotFound;
                    break;
            }

            return await Task.FromResult(httpResponseMessage);
        }
    }
}