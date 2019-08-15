using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE_MVC_STOK.Data.Context;
using CORE_MVC_STOK.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace CORE_MVC_STOK.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Member

        public MasterContext MasterContext { get; set; }

        private DbContext dbContext;

        #endregion
        #region Constructor

        /// <summary>
        /// UnitOfWork kullanmak için çağırıldıgından gönderilen context parametresi buradakine aktarılır.
        /// </summary>
        /// <param name="masterContext"></param>
        public UnitOfWork(MasterContext masterContext)
        {
            MasterContext = masterContext;
        }
        #endregion
        /// <summary>
        /// MasterContexti contextimize bağlama işlemi.
        /// </summary>
        private DbContext DbContext
        {
            get
            {
                if (dbContext == null)
                {
                    dbContext = MasterContext;
                }
                return dbContext;
            }
            set { dbContext = value; }
        }



        /// <summary>
        /// Repository örneğini başlatmak için kullanılır.
        /// </summary>
        /// <typeparam name="T">Herhangi bir veri tabanı tablosu.</typeparam>
        /// <returns>Verilen tablo ile ilgili repository.</returns>
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(DbContext);
        }

        /// <summary>
        /// Değişiklikleri kaydet.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            try
            {
                int result = 0;
                result = DbContext.SaveChanges();
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                    DbContext = null;
                }
                disposedValue = true;
            }
        }


        public void Dispose()
        {
            Dispose(true);

        }
        #endregion
    }
}
