using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private readonly ApplicationDbContext db;
        public ProductImageRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(ProductImage obj) 
        {
            db.ProductImages.Update(obj);
        }
    }
}
