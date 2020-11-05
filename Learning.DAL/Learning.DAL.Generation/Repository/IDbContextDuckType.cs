using System.Threading;
using System.Threading.Tasks;

namespace Learning.DAL.Generation.Repository {

    public interface IDbContextDuckType {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
