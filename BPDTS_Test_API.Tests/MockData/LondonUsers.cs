using BPDTS_Test_API.Models;
using System.Collections.Generic;

namespace BPDTS_Test_API.Tests.MockData
{
    internal static class LondonUsers
    {
        public static List<User> MockUsers { get; set; }
        public static List<User> MockLondonCityUsers { get; set; }
        public static List<User> MockLondonCoordinatesUsers { get; set; }

        public static List<User> MockEmptyList { get; set; }

        static LondonUsers()
        {
            MockUsers = new List<User>()
            {
                new User {
                    id = "1",
                    email = "testuser_1@richardmeara.com",
                    first_name = "richard",
                    last_name = "meara",
                    ip_address = "192.168.0.1",
                    latitude = "51.4671",
                    longitude = "-0.1202"
                },
                new User {
                    id = "2",
                    email = "testuser_3@richardmeara.com",
                    first_name = "joe",
                    last_name = "bloggs",
                    ip_address = "192.168.0.2",
                    latitude = "26.0000",
                    longitude = "72.0000"
                },
                new User
                {
                    id = "3",
                    email = "testuser_3@richardmeara.com",
                    first_name = "jane",
                    last_name = "doe",
                    ip_address = "192.168.0.3",
                    latitude = "27.0000",
                    longitude = "73.0000"
                }
            };

            MockLondonCityUsers = new List<User>()
            {
                new User {
                    id = "3",
                    email = "testuser_3@richardmeara.com",
                    first_name = "jane",
                    last_name = "doe",
                    ip_address = "192.168.0.3",
                    latitude = "27.0000",
                    longitude = "73.0000"
                }
            };

            MockLondonCoordinatesUsers = new List<User>()
            {
                new User {
                    id = "1",
                    email = "testuser_1@richardmeara.com",
                    first_name = "richard",
                    last_name = "meara",
                    ip_address = "192.168.0.1",
                    latitude = "51.4671",
                    longitude = "-0.1202"
                }
            };

            MockEmptyList = new List<User>();
        }
    }
}