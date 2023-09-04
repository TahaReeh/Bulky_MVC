﻿using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {

        private readonly ApplicationDbContext db;
        public ApplicationUserRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;
        }

        public void Update(ApplicationUser applicationUser)
        {
           db.ApplicationUsers.Update(applicationUser);
        }
    }
}