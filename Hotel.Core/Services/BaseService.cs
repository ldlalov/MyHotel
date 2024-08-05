//using Hotel.Infrastructure.Data.Repositories;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Hotel.Core.Services
//{
//    public class BaseService<TEntity>
//    {
//        private readonly ApplicatioDbRepository repo;

//        public BaseService(ApplicatioDbRepository _repo)
//        {
//            repo = _repo;
//        }

//        public IQueryable<TEntity> All<TEntity>() where TEntity : class
//        {
//            return _context.Set<TEntity>();
//        }
//        public async Task Add(TEntity entity)
//        {
//            // Validation or other business logic can go here.
//            repo.AddAsync(entity);
//            await repo.SaveChangesAsync();
//        }

//        public async Task Remove(TEntity entity)
//        {
//            repo.Remove(entity);
//            await repo.SaveChangesAsync();
//        }
//    }
//}
