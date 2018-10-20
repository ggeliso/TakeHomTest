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
        public List<Individual> ListIndividual = new List<Individual>();

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
        [HttpGet]
        public int Merge(int firstPerson , int secondPerson)
        {

            Console.WriteLine(ListIndividual.Count);
            List<IndividualAddress> individualAddresses = new List<IndividualAddress>();
            individualAddresses.Add(IndividualAddress.listIndividualAddress.Find(x => x.RecordNumber.Equals(secondPerson)));

            IndividualAddress.listIndividualAddress.Remove(individualAddresses[0]);

            List<Address> addresses = new List<Address>();
            addresses.Add(Address.listAddress.Find(x => x.AddressId.Equals(individualAddresses[0].AddressId)));

            List<Individual> individuals = new List<Individual>();
            individuals.Add(ListIndividual.Find(x => x.RecordNumber.Equals(firstPerson)));

            string streetName = "";
            foreach (Individual item in individuals)
            {
                streetName = item.GetAddresses() + " - " + addresses[0].StreetName;
            }

            ListIndividual.Find(x => x.RecordNumber.Equals(individuals[0].RecordNumber));
            ListIndividual[0].addresses = streetName;


            return 1;
        }
        

        
    }
}
