
using DataAccess.Repositories.Interface;

namespace Application;

public interface IUnitOfWork : IAsyncDisposable
{

    public ICustomerRepository CustomerRepository { get; }
    public ICarInformationRepository CarInformationRepository { get; }
    public IManufacturerRepository ManufacturerRepository { get; }
    public IRentingDetailRepository RentingDetailRepository { get; }
    public IRentingTransactionRepository RentingTransactionRepository { get; }
    public ISupplierRepository SupplierRepository { get; }

    public Task<int> SaveChangeAsync();

    public Task BeginTransactionAsync();
    public Task CommitAsync();
    public Task RollbackAsync();

    public ValueTask DisposeAsync();
}