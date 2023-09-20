using DataAccess.Repositories.Interfaces;

namespace DataAccess.UnitOfWork.Interfaces;

public interface IUnitOfWork: IAsyncDisposable
{
    public ICarInformationRepository CarInformationRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
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