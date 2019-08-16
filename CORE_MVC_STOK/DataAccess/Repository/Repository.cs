using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CORE_MVC_STOK.DataAccess.Repository
{
    /// <summary>
    /// IRepository ınterface'mizi implement ettiğimiz generic repository sınıfımız.Interfacede bulunan metodların (iş yapan)iç kodlarını yazdığımız yer.
    ///  Bu şekilde tanımlamamızın sebebi veritabanına  independent (bağımsız) bir durumda kalabilmek.
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        // İçeride işlemleri yaparken kullanacağımız dışarıdan bağımsız değişkenler.(Dependency İnjection)
        #region Member

        private readonly DbContext dbContext;
        private readonly DbSet<T> dbSet;

        #endregion

        #region Constructor

        /// <summary>
        /// Parametre olarak Contextimizi aldığımız yapıcı metod.
        /// </summary>
        /// <param name="_dbContext">Bağlantımızdan gelen contex nesnesi.</param>
        public Repository(DbContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = dbContext.Set<T>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Verilen veriyi context üzerinden veri tabanına ekler.
        /// </summary>
        /// <param name="entity">Eklenecek entity nesnesi.</param>
        public void Create(T entity)
        {
            dbSet.Add(entity);
        }


        /// <summary>
        /// Verilen veriyi context üzerinden veri tabanından siler.
        /// </summary>
        /// <param name="entity">Silinecek entity nesnesi.</param>
        public void Delete(T entity)
        {
            EntityEntry<T> entityEntry = dbContext.Entry(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                dbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Şarta göre tek veri getirir.
        /// </summary>
        /// <param name="predicate">Veri şartı.</param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = dbSet.Where(predicate);
            return iQueryable.ToList().FirstOrDefault();
        }

        /// <summary>
        /// Tüm verileri getirir.
        /// Select * From T
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> iQueryable = dbSet;
            return iQueryable;
        }

        /// <summary>
        /// Sorguya ait tüm verileri getirir.
        /// Select * From T Where ---predicate
        /// </summary>
        /// <param name="predicate">Veri şartı</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = dbSet.Where(predicate);
            return iQueryable;
        }

        /// <summary>
        /// Verilen veriyi context üzerinden veri tabanında günceller.
        /// </summary>
        /// <param name="entity">Güncellenecek entity nesnesi.</param>
        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }


        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            IIncludableQueryable<T, object> query = null;

            if (includes.Length > 0)
            {
                query = dbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? dbSet : (IQueryable<T>)query;
        }
        #endregion
    }
}
