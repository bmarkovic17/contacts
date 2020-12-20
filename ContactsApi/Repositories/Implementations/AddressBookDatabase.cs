using System.Threading.Tasks;
using ContactsApi.Database;
using ContactsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace ContactsApi.Repositories.Implementations
{
    public class AddressBookDatabase : IAddressBookDatabase
    {
        private readonly AddressBookContext _addressBookContext;

        public AddressBookDatabase(AddressBookContext addressBookContext) =>
            _addressBookContext = addressBookContext;

        public Task<IDbContextTransaction> BeginTransactionAsync() =>
            _addressBookContext.Database.BeginTransactionAsync();

        public Task CommitAsync() =>
            _addressBookContext.Database.CommitTransactionAsync();

        public Task SaveChangesAsync() =>
            _addressBookContext.SaveChangesAsync();
    }
}
