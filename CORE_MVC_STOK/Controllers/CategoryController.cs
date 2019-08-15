using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE_MVC_STOK.Data.Context;
using CORE_MVC_STOK.DataAccess.UnitOfWork;
using CORE_MVC_STOK.Models;
using Microsoft.AspNetCore.Mvc;

namespace CORE_MVC_STOK.Controllers
{
    // Kategori işlemlerinin gerçekleştiği sınıf.
    public class CategoryController : Controller
    {
        #region Member
        private MasterContext _masterContext;
        #endregion

        #region Constructor
        public CategoryController(MasterContext masterContext)
        {
            _masterContext = masterContext;
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Kategori listesi actionu.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var categoryList = uow.GetRepository<Category>().GetAll().ToList();
                return View(categoryList);
            }
        }

        /// <summary>
        /// Kategori oluşturma actionu.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Viewden dönen kategori bilgisini kaydeden action.
        /// </summary>
        /// <param name="newCategory">Eklenecek Kategori modeli.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Category newCategory)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork(_masterContext))
                {
                    uow.GetRepository<Category>().Create(newCategory);
                    uow.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View();
            }
        }

        /// <summary>
        /// Kategori silme actionu.
        /// </summary>
        /// <param name="id">Silinecek kategori id'si.</param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var deleteCategory = uow.GetRepository<Category>().Get(x => x.CategoryId == id);
                var result = uow.GetRepository<Product>().Get(x => x.CategoryId == id);
                if (result != null)
                {
                    TempData["Warning"] = deleteCategory.CategoryName + " Kategorisini Silemezsiniz.! Kullanımda.";
                    return RedirectToAction("Index");
                }
                uow.GetRepository<Category>().Delete(deleteCategory);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Kategori güncelleme ekranı actionu.
        /// </summary>
        /// <param name="id">Güncellenecek kategori id'si.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var updateCategory = uow.GetRepository<Category>().Get(x => x.CategoryId == id);
                return View(updateCategory);
            }
        }

        /// <summary>
        /// Güncellenen kategori bilgilerini veri tabanına kaydeden action.
        /// </summary>
        /// <param name="updateCategory">Güncellenen kategori modeli.</param>
        /// <returns></returns>
        public IActionResult Update(Category updateCategory)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow = new UnitOfWork(_masterContext))
                {
                    uow.GetRepository<Category>().Update(updateCategory);
                    uow.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(updateCategory);
        }
    }
    #endregion
}