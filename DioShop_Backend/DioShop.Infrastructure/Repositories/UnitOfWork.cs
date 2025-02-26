using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DioShop.Application.Contracts.Infrastructure.IRepositories;
using DioShop.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace DioShop.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IDbContextTransaction _objTran;
		private readonly IFileStoreageRepository _fileStoreageRepository;
		// Constructor nhận DI
		public UnitOfWork(ApplicationDbContext context,
						  IHttpContextAccessor httpContextAccessor,
						  IFileStoreageRepository fileStoreageRepository)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
			_fileStoreageRepository = fileStoreageRepository;
		}

		private ICouponRepository _couponRepository;

        public ICouponRepository CouponRepository =>
           _couponRepository ??= new CouponRepository(_context);

        private IBrandRepository _brandRepository;
        public IBrandRepository BrandRepository =>
            _brandRepository ??= new BrandRepository(_context);

        private ITagRepository _tagRepository;
        public ITagRepository TagRepository =>
            _tagRepository ??= new TagRepository(_context);

        private IShippingMethodRepository _shippingMethodRepository;
        public IShippingMethodRepository ShippingMethodRepository =>
            _shippingMethodRepository ??= new ShippingMethodRepository(_context);

        private ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository =>
            _categoryRepository ??= new CategoryRepository(_context);


		private IChatMessageRepository _chatMessageRepository;
		public IChatMessageRepository ChatMessageRepository =>
			_chatMessageRepository ??= new ChatMessageRepository(_context);

		private ICartItemRepository _cartItemRepository;
        public ICartItemRepository CartItemRepository =>
            _cartItemRepository ??= new CartItemRepository(_context);

        private IOrderItemRepository _orderItemRepository;
        public IOrderItemRepository OrderItemRepository =>
            _orderItemRepository ??= new OrderItemRepository(_context);

        private IOrderRepository _orderRepository;
        public IOrderRepository OrderRepository =>
            _orderRepository ??= new OrderRepository(_context);

        private IProductItemRepository _productItemRepository;
        public IProductItemRepository ProductItemRepository =>
            _productItemRepository ??= new ProductItemRepository(_context);

        private IProductRepository _productRepository;
        public IProductRepository ProductRepository =>
            _productRepository ??= new ProductRepository(_context);

		// Repository cho FileStorage
		public IFileStoreageRepository FileStoreageRepository => _fileStoreageRepository;


		private IProductTagRepository _productTagRepository;
        public IProductTagRepository ProductTagRepository =>
            _productTagRepository ??= new ProductTagRepository(_context);

        private ICartRepository _cartRepository;
        public ICartRepository CartRepository =>
         _cartRepository ??= new CartRepository(_context);

        public void CreateTransaction()
        {
            //It will Begin the transaction on the underlying store connection
            _objTran = _context.Database.BeginTransaction();
        }

        //If all the Transactions are completed successfully then we need to call this Commit() 
        //method to Save the changes permanently in the database
        public void Commit()
        {
            //Commits the underlying store transaction
            _objTran.Commit();
        }

        //If at least one of the Transaction is Failed then we need to call this Rollback() 
        //method to Rollback the database changes to its previous state
        public void Rollback()
        {
            //Rolls back the underlying store transaction
            _objTran.Rollback();
            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            _objTran.Dispose();
        }



        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            //var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid);
            await _context.SaveChangesAsync();
        }
    }
}
