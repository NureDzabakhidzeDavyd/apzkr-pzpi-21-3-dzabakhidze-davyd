using Kolosok.Domain.Entities;

namespace Kolosok.Application.Interfaces.Infrastructure;

public interface IContactRepository : IRepository<Contact>
{
    public Task<Contact> GetContactByLoginAndPasswordAsync(string login, string password);
}