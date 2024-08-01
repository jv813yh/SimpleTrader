using SimpleTrader.Domain.Models;
using SimpleTrader.EntityFramework.DbContexts;
using SimpleTrader.EntityFramework.Repositories;
using System.Reflection.Metadata;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BaseRepository<User> userRepository = new BaseRepository<User>(new DesignTimeSimpleTraderDbContextFactory());

            User userUpdate = new User()
            {
                Username = "UpdatedWithNewName",
                Email = "UpdatedWithNewName",
                Password = "UpdatedWithNewName",
                DateJoined = DateTime.Now
            };

            //User newUser = userRepository.CreateAsync(userUpdate).Result;
            

            //userRepository.UpdateAsync(3, userUpdate).Wait();
            

            //userRepository.DeleteAsync(2).Wait();
        }
    }
}
