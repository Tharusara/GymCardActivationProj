using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtuagymDevTask.Models;
using VirtuagymDevTask.Services;

namespace VirtuagyDevTaskUnitTests.Services
{
    public class CheckInServiceFake : ICheckInService
    {
        public async Task<User> CheckUserValidity(int id)
        {   
            var newUser = new User
            {
                FullName = "fakeUser",
                DateOfBirth = DateTime.Today,
                CallingName = "faker",
                ContactNo = 887894542,
                Created = DateTime.Today,
            };
            switch (id)
            {
                case 0:
                    return null;
                case 1:
                    newUser.ContactNo = 1;
                    return newUser;
                case 2:
                    newUser.ContactNo = 2;
                    return newUser;
                case 3:
                    newUser.ContactNo = 3;
                    return newUser;
                case 4:
                    newUser.ContactNo = 0;
                    return newUser;
                default:
                    return newUser;
            }
        }
    }
}
