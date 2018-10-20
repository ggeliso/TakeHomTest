using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TakeHomeTestGGO.Controllers;
using Newtonsoft.Json;

namespace TakeHomeTestGGO.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public static List<Address> listAddress = new List<Address>();

        // Constructo class
        static Address()
        {
            LoadAddress();
        }

        // Charge data into listAddress from json file
        public static IEnumerable<Address> LoadAddress()
        {
            Address[] result = JsonConvert.DeserializeObject<Address[]>(IndividualApiController.loadDataFromJsonFile("Address.json"));
            foreach (Address item in result)
            {
                listAddress.Add(item);
            }
            return listAddress;
        }

       
    }
}