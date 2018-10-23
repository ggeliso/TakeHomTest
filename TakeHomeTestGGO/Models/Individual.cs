using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TakeHomeTestGGO.Controllers;

namespace TakeHomeTestGGO.Models
{
    public class Individual
    {
        public int RecordNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public string addresses;

        static Individual()
        {
            loadIndividual();
        }

        private static void loadIndividual()
        {
            Individual[] result = JsonConvert.DeserializeObject<Individual[]>(IndividualApiController.loadDataFromJsonFile("Individual.json"));

        }

        public string GetAddresses()
        {
            return addresses;
        }

        public void SetAddresses(string value)
        {
            addresses = value;
        }
    }
}