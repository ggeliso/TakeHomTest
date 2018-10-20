using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TakeHomeTestGGO.Models;

namespace TakeHomeTestGGO.Controllers
{
    public class UserController : ApiController
    {
        private List<UserModel> listUsers = new List<UserModel>();

        // Getting information about user, who wants to connect to the application
        private IEnumerable<UserModel> GetUsers()
        {
            UserModel userModel = new UserModel { userId = "admin", firstName = "Jesús", lastName = "Rodriguez", email = "jrodriguez@ideaware.net", password = "123" };

            listUsers.Add(userModel);

            return listUsers;
        }

        // This methos recieve all petitions from whole user that wants to entry the application
        [HttpPost]
        public bool Login([FromBody] UserModel user)
        {
            bool isValidUser = false;

            foreach (UserModel userModel in GetUsers())
            {
                if (userModel.userId.Equals(user.userId) && userModel.password.Equals(user.password))
                {
                    isValidUser = true;
                }
            }
            return isValidUser;
        }
    }
}
