using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace ContactsApi.Repositories.Interfaces
{
    public interface IAddressBookDatabase
    {
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task SaveChangesAsync();
    }
}
