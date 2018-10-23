using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TakeHomeTestGGO.Models;

namespace TakeHomeTestGGO.Controllers
{
    public class IndividualApiController : ApiController
    {
        public static List<Individual> ListIndividual = new List<Individual>();

        // This method load all data to Individual
        [HttpGet]
        public IEnumerable<Individual> LoadIndividual()
        {
            if (ListIndividual.Count == 0)
            {
                
                Individual[] result = JsonConvert.DeserializeObject<Individual[]>(loadDataFromJsonFile("Individual.json"));

                Address address = new Address();
                
                IndividualAddress individualAddress = new IndividualAddress();

                foreach (Individual item in result)
                {
                    
                    List<IndividualAddress> individualAddresses = new List<IndividualAddress>();
                    individualAddresses.Add(IndividualAddress.listIndividualAddress.Find(x => x.RecordNumber.Equals(item.RecordNumber)));

                    Console.WriteLine("individualAddresses " + individualAddresses[0].AddressId);

                    List<Address> addresses = new List<Address>();
                    addresses.Add(Address.listAddress.Find(x => x.AddressId.Equals(individualAddresses[0].AddressId)));
                    
                    item.SetAddresses(addresses[0].StreetName);

                    ListIndividual.Add(item);
                }
            }

            return ListIndividual;
        }

        // This method allow load data from json file
        public static string loadDataFromJsonFile(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);

            string result = File.ReadAllText(filePath).ToString();
            return (result);
        }

        // This method should be merging between two individual
        [HttpPost]
        public int Merge(int[] array)
        {
            int firstPerson = 0;
            int secondPerson = 0;
            string streetName = "";

            List<Individual> individuals = new List<Individual>();

            foreach (var itemPersons in array)
            {
                if(firstPerson == 0)
                {

                    firstPerson = itemPersons;
                } else
                {
                    List<IndividualAddress> individualAddresses = new List<IndividualAddress>();
                    List<Address> addresses = new List<Address>();

                    individualAddresses.Add(IndividualAddress.listIndividualAddress.Find(x => x.RecordNumber.Equals(itemPersons)));
                    IndividualAddress.listIndividualAddress.Remove(individualAddresses[0]);

                    addresses.Add(Address.listAddress.Find(x => x.AddressId.Equals(individualAddresses[0].AddressId)));

                    ListIndividual.Remove(ListIndividual.Find(x => x.RecordNumber.Equals(itemPersons)));
                    streetName += " - " + addresses[0].StreetName;
                }
            }

            individuals.Add(ListIndividual.Find(x => x.RecordNumber.Equals(firstPerson)));
            
            foreach (Individual item in individuals)
            {
                streetName = item.GetAddresses() + streetName; 
            }

            ListIndividual.Find(x => x.RecordNumber.Equals(firstPerson)).addresses = streetName;
           
            return 1;
        }
        

        
    }
}
