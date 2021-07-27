using BPDTS_Test_API.Models;
using BPDTS_Test_API.Models.Interfaces;
using BPDTS_Test_API.Tests.MockData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPDTS_Test.API.Tests.Services
{
    public class BPDTSTestAppServiceFake : IBPDTSTestApiService
    {
        public async Task<List<User>> GetUsers()
        {
            return await Task.FromResult(LondonUsers.MockUsers);
        }

        public async Task<List<User>> GetUsersByCity(string city)
        {
            if (city != "London")
            {
                return null;
            }
            return await Task.FromResult(LondonUsers.MockLondonCityUsers);
        }

        public async Task<List<User>> GetUsersByLondonProximity()
        {
            return await Task.FromResult(LondonUsers.MockLondonCoordinatesUsers);
        }

        public async Task<List<User>> GetLondonUsersByCityNameAndCoordinates()
        {
            List<User> concatUsers = LondonUsers.MockLondonCityUsers.Union(LondonUsers.MockLondonCoordinatesUsers).ToList();
            return await Task.FromResult(concatUsers);
        }
    }
}