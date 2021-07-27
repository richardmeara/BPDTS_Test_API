using System.Net.Http;
using System.Threading.Tasks;

namespace BPDTS_Test_API.Models.Interfaces
{
    public interface IHttpPipeline
    {
        Task<HttpResponseMessage> Get(string uri);
    }
}