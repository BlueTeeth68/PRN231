using Application;
using DataAccess.Models;
using DataAccess.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly ILogger<UnitOfWork> _logger;

    public ICustomerRepository CustomerRepository { get; }

    public ICarInformationRepository CarInformationRepository { get; }

    public IManufacturerRepository ManufacturerRepository { get; }

    public IRentingDetailRepository RentingDetailRepository { get; }

    public IRentingTransactionRepository RentingTransactionRepository { get; }

    public ISupplierRepository SupplierRepository { get; }

    public UnitOfWork(AppDbContext context, ILogger<UnitOfWork> logger, ICustomerRepository customerRepository, ICarInformationRepository carInformationRepository, IManufacturerRepository manufacturerRepository, IRentingDetailRepository rentingDetailRepository, IRentingTransactionRepository rentingTransactionRepository, ISupplierRepository supplierRepository)
    {
        _context = context;
        _logger = logger;
        CustomerRepository = customerRepository;
        CarInformationRepository = carInformationRepository;
        ManufacturerRepository = manufacturerRepository;
        RentingDetailRepository = rentingDetailRepository;
        RentingTransactionRepository = rentingTransactionRepository;
        SupplierRepository = supplierRepository;
    }

    public async Task<int> SaveChangeAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error when commit transaction.\nDate:{DateTime.UtcNow}");
            throw new Exception("Transaction has not been created yet.");
        }

    }

    public async Task RollbackAsync()
    {


        await _transaction.RollbackAsync();
    }

    //implement Dispose pattern
    private bool _disposed;


    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
        }

        await _context.DisposeAsync();
    }
    //implement Dispose pattern
}