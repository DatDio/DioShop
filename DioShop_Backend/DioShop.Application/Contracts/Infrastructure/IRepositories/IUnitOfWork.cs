using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DioShop.Application.Contracts.Infrastructure.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {

        //Start the database Transaction
        void CreateTransaction();

        //Commit the database Transaction
        void Commit();

        //Rollback the database Transaction
        void Rollback();

        IBrandRepository BrandRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        ICouponRepository CouponRepository { get; }
        IOrderItemRepository OrderItemRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductItemRepository ProductItemRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductTagRepository ProductTagRepository { get; }
        ITagRepository TagRepository { get; }
        IShippingMethodRepository ShippingMethodRepository { get; }
        ICartRepository CartRepository { get; }



        Task Save();
    }
}
