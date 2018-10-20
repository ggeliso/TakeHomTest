using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TakeHomeTestGGO.Controllers;

namespace TakeHomeTestGGO.Models
{
    public class IndividualAddress
    {
        public int RecordNumber { get => _recordNumber; set => _recordNumber = value; }
        public int AddressId { get => _addressId; set => _addressId = value; }

        static IndividualAddress()
        {
            loadIndividualAddress();
        }

        public static List<IndividualAddress> listIndividualAddress = new List<IndividualAddress>();
        private int _recordNumber;
        private int _addressId;

        public static IEnumerable<IndividualAddress> loadIndividualAddress()
        {
            IndividualAddress[] result = JsonConvert.DeserializeObject<IndividualAddress[]>(IndividualApiController.loadDataFromJsonFile("IndividualAddress.json"));
            foreach (IndividualAddress item in result)
            {
                listIndividualAddress.Add(item);
            }
            return listIndividualAddress;
        }
    }
}