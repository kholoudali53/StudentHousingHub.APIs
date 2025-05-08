using StudentHousingHub.Core.Entities;
using StudentHousingHub.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core
{
    public interface IUnitOfWork// design pattern : وسيط بيني وبين ال Repository
    {
        Task<int> CompleteAsync(); //هتعملي savechange()

        // Create Repository<T> and return it
        IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>; // اعملي repository من النوع اللي محتاجه بس

        //Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
