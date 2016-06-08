using PublishedBooksDAL.Entities;
using PublishedBooksDAL.Repositories;
using System.Web.Security;

namespace PublishedBooks.Infrastructure.Security
{

    public class MongoAuthProvider : IAuthProvider
    {
        IRepository<User> usersRepository;

        public MongoAuthProvider(IRepository<User> repo)
        {
            usersRepository = repo;
        }

        public bool Authenticate(string username, string password)
        {
            var users = usersRepository.SearchFor(u => u.Username == username && u.Password == password);

            if (users.Count != 0)
                return true;
            else
                return false;
        }
    }
}
