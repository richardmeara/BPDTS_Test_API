using BPDTS_Test_API.Models;
using BPDTS_Test_API.Models.Interfaces;
using GeoCoordinatePortable;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPDTS_Test_API.Services
{
    public class BPDTSTestApiService : IBPDTSTestApiService
    {
        private readonly IConfiguration _config;
        private readonly IHttpPipeline _httpPipeline;
        private readonly string _apiUri;

        private const double LongitudeMinimum = -180;
        private const double LongitudeMaximum = 180;
        private const double LatitudeMinimum = -90;
        private const double LatitudeMaximum = 90;
        private const double LondonLatitude = 51.5074;
        private const double LondonLongitude = 0.1277;
        private const double MetresToMiles = 0.00062137;

        private const double DistanceToLondonRequirement = 50;

        public BPDTSTestApiService(IConfiguration config, IHttpPipeline httpPipeline)
        {
            _config = config;            
            _apiUri = _config["TestApi:Endpoint"];
            _httpPipeline = httpPipeline;
        }

        public async Task<List<User>> GetUsersByCity(string city)
        {
            var responseMessage = await _httpPipeline.Get($"{_apiUri}/city/{city}/users");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseObject = await responseMessage.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(responseObject);
                return users;
            }
            return null;
        }

        public async Task<User> GetUser(string id)
        {
            var responseMessage = await _httpPipeline.Get($"{_apiUri}/user/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseObject = await responseMessage.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(responseObject);
                return user;
            }
            return null;
        }

        public async Task<List<User>> GetUsers()
        {
            var responseMessage = await _httpPipeline.Get($"{_apiUri}/users");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseObject = await responseMessage.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<User>>(responseObject);
                return users;
            }
            return null;
        }

        public async Task<List<User>> GetUsersByLondonProximity()
        {
            var allUsers = await GetUsers();
            if (allUsers == null)
            {
                return null;
            }
            List<User> usersWithinLondonLimit = new();
            GeoCoordinate londonCoordinates = new(LondonLatitude, LondonLongitude);

            //Loop over every user
            foreach (var usr in allUsers)
            {
                //If their latitude and longitudes are valid doubles continue
                if (double.TryParse(usr.latitude, out double castLat) && double.TryParse(usr.longitude, out double castLong))
                {
                    //Only add valid users with correct coordinates
                    if (castLat >= LatitudeMinimum && castLat <= LatitudeMaximum && castLong >= LongitudeMinimum && castLong <= LongitudeMaximum)
                    {
                        //Get the user location from API and compare against central London coordinates
                        GeoCoordinate userLocation = new(castLat, castLong);
                        double distanceInMetres = userLocation.GetDistanceTo(londonCoordinates);
                        //if the distance is valid
                        if (distanceInMetres > 0)
                        {
                            //convert the metres to miles
                            double distanceInMiles = (distanceInMetres * MetresToMiles);
                            if (distanceInMiles <= DistanceToLondonRequirement)
                            {
                                usersWithinLondonLimit.Add(usr);
                            }
                        }
                    }
                }
            }

            return usersWithinLondonLimit;
        }

        public async Task<List<User>> GetLondonUsersByCityNameAndCoordinates()
        {
            List<User> londonUsers = await GetUsersByCity("London");
            if (londonUsers == null)
            {
                return null;
            }

            List<User> usersWithinLondonLimit = await GetUsersByLondonProximity();
            if (usersWithinLondonLimit == null)
            {
                return null;
            }

            //combine the users from both lists and return
            List<User> totalUsers = londonUsers.Union(usersWithinLondonLimit).ToList();

            return totalUsers;
        }
    }
}