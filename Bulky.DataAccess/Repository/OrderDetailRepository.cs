using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext db;
        public OrderDetailRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(OrderDetail obj) 
        {
            db.OrderDetails.Update(obj);
        }
    }
}
