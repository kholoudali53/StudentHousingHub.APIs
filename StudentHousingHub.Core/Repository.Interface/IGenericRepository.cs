using StudentHousingHub.Core.Entities;
using StudentHousingHub.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Repository.Interface
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TKey id);

        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> GetByIdAsync(TKey id, Func<IQueryable<TEntity>, IQueryable<TEntity>> include);


        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> Spec);
        Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> Spec);
        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> Spec);

        Task<TEntity> GetUserByNationalIDAsync(string NationalID);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        //Task<int> Commit();
    }
}
