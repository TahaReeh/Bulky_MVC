using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {

        private readonly ApplicationDbContext db;
        public CompanyRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }
        public void Update(Company obj)
        {
            db.Companies.Add(obj);
        }

    }
}
