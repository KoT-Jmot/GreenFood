using GreenFood.Domain.Contracts;

namespace GreenFood.Infrastructure.Repositories
{
    public class RepositoryManager
    {
        private readonly ApplicationContext _context;
        private IProductRepository? _product;
        private IOrderRepository? _order;
        private ITypeOfProductRepository? _typeOfProduct;
        public RepositoryManager(ApplicationContext context)
        {
            _context = context;
        }

        public IProductRepository Products()
        {
            if (_product == null)
                _product = new ProductRepository(_context);

            return _product;
        }

        public IOrderRepository Orders()
        {
            if (_order == null)
                _order = new OrderRepository(_context);

            return _order;
        }

        public ITypeOfProductRepository TypesOfProducts()
        {
            if (_typeOfProduct == null)
                _typeOfProduct = new TypeOfProductRepository(_context);

            return _typeOfProduct;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
