using Microsoft.EntityFrameworkCore;
using StudentHousingHub.Core.Entities;
using StudentHousingHub.Core.Repository.Interface;
using StudentHousingHub.Core.Specifications;
using StudentHousingHub.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // if Found navigation property
            if (typeof(TEntity) == typeof(Apartment)) // if return Apartment
            {
                return (IEnumerable<TEntity>)await _context.Apartments./*Skip(5).Take(50).*/OrderBy(P => P.UniversityName).Include(r => r.Owner).Include(a => a.Rooms).ThenInclude(a => a.Beds).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetUserByNationalIDAsync(string NationalID)
        {
            // if Found navigation property
            if (typeof(TEntity) == typeof(Students)) // if return Apartment
            {
                //return await _context.Apartments.Include(R => R.owner).Include(A => A.admin).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
                return await _context.Students.Where(P => P.NationalID == NationalID ).FirstOrDefaultAsync() as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(NationalID);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            // if Found navigation property
            if (typeof(TEntity) == typeof(Apartment)) // if return Apartment
            {
                //return await _context.Rooms.Include(R => R.owner).Include(A => A.admin).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
                return await _context.Apartments.Where(P => P.id == id as int?).Include(r => r.Owner).Include(a => a.Rooms).ThenInclude(a => a.Beds).FirstOrDefaultAsync() as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
        

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> Spec)
        {
            return await ApplySpecifications(Spec).ToListAsync();
        }

        public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> Spec)
        {
            return await ApplySpecifications(Spec).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> Spec)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), Spec);
        }
        
        public async Task<int> GetCountAsync(ISpecifications<TEntity, TKey> Spec)
        {
            return await ApplySpecifications(Spec).CountAsync();
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(e => e.id.Equals(id));
        }

        //async Task<int> Commit(TEntity entity)
        //{
        //    return await _context.SaveChangesAsync();
        //}
    }
}
