using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kolosok.Infrastructure.Repositories;

public class ContactRepository : BaseRepository<Contact>, IContactRepository
{
    public ContactRepository(KolosokDbContext context) : base(context)
    {
    }

    public async Task<Contact> GetContactByLoginAndPasswordAsync(string email, string password)
    {
        var contact = await DbSet.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        return contact;
    }
}