using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository
{
	public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDbContext db;
		public OrderHeaderRepository(ApplicationDbContext _db) : base(_db)
		{
			db = _db;
		}

		public void Update(OrderHeader obj)
		{
			db.OrderHeaders.Update(obj);
		}

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = db.OrderHeaders.FirstOrDefault(x => x.Id == id);
			if (orderFromDb != null)
			{
				orderFromDb.OrderStatus = orderStatus;
				if (!string.IsNullOrEmpty(paymentStatus))
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}

		public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
		{
			var orderFromDb = db.OrderHeaders.FirstOrDefault(x => x.Id == id);
			if (orderFromDb != null)
			{
				if (!string.IsNullOrEmpty(sessionId))
				{
					orderFromDb.SessionId = sessionId;
				}
				if (!string.IsNullOrEmpty(paymentIntentId))
				{
					orderFromDb.PaymentIntentId = paymentIntentId;
					orderFromDb.PaymentDate = DateTime.Now;
				}
			}
		}
	}
}
