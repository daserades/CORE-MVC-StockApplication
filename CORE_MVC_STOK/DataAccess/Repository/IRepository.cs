using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CORE_MVC_STOK.DataAccess.Repository
{
    /// <summary>
    /// Model katmanında bulunan her bir Tablo tipi için aşağıda tanımladığımız fonksiyonları gerçekleştirebilcek generic bir repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Tüm verileri getirir.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();
        
        /// <summary>
        /// Sorgudaki geçerli tüm veriyi getirir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Tek bir veri getirir.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verilen entity nesnesini ekler.
        /// </summary>
        /// <param name="entity"></param>
        void Create(T entity);

        /// <summary>
        /// Verilen entity nesnesini günceller.
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Verilen entity nesnesini siler.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);
        IQueryable<T> Include(params Expression<Func<T, object>>[] includes);
    }
}
