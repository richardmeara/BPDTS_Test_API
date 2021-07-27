using System.Collections.Generic;
using System.Threading.Tasks;

namespace BPDTS_Test_API.Models.Interfaces
{
    public interface IBPDTSTestApiService
    {
        Task<List<User>> GetUsers();

        Task<List<User>> GetUsersByCity(string city);

        Task<List<User>> GetUsersByLondonProximity();

        Task<List<User>> GetLondonUsersByCityNameAndCoordinates();
    }
}