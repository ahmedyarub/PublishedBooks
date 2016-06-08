namespace PublishedBooks.Infrastructure.Security {

    public interface IAuthProvider {
        bool Authenticate(string username, string password);
    }
}
