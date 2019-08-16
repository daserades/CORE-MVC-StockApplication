using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CORE_MVC_STOK.Data.Context;
using CORE_MVC_STOK.DataAccess.UnitOfWork;
using CORE_MVC_STOK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CORE_MVC_STOK.Controllers
{
    //Ürün işlemlerinin gerçekleştiği sınıf.
    public class ProductController : Controller
    {
        #region Member
        private MasterContext _masterContext;
        #endregion

        #region Constructor
        public ProductController(MasterContext masterContext)
        {
            _masterContext = masterContext;
        }
        #endregion

        #region Action Methods
        /// <summary>
        /// Ürün listeleme sayfasını açan action.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {

                var productList = uow.GetRepository<Product>().Include(x => x.Category).ToList();
                return View(productList);
            }
        }

        /// <summary>
        /// Ürün oluşturma sayfasını açan action.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {

            List<SelectListItem> getCategory = (from x in _masterContext.Categories.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.CategoryName,
                                                    Value = x.CategoryId.ToString()
                                                }).ToList();
            ViewBag.categoryList = getCategory;
            return View();
        }

        /// <summary>
        /// Oluşturulan ürünü veri tabanına kaydeden action.
        /// </summary>
        /// <param name="newProduct">Kaydedilecek ürün modeli.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var category = uow.GetRepository<Category>().Get(x => x.CategoryId == newProduct.Category.CategoryId);
                newProduct.Category = category;
                uow.GetRepository<Product>().Create(newProduct);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Ürün silme işlemini gerçekleştiren action.
        /// </summary>
        /// <param name="id">Silinecek ürün id'si</param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var deleteProduct = uow.GetRepository<Product>().Get(x => x.ProductId == id);
                var result = uow.GetRepository<Sales>().Get(x => x.ProductId == id);
                if (result != null)
                {
                    TempData["Warning"] = deleteProduct.ProductName + " Adlı Ürünü silemezsiniz !.Kullanımda.";
                    return RedirectToAction("Index");
                }
                uow.GetRepository<Product>().Delete(deleteProduct);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Ürün güncelleme sayfasını açan action.
        /// </summary>
        /// <param name="id">Güncellenecek ürün id'si.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var updateProduct = uow.GetRepository<Product>().Get(x => x.ProductId == id);

                List<SelectListItem> getCategory = (from x in _masterContext.Categories.ToList()
                                                    select new SelectListItem
                                                    {
                                                        Text = x.CategoryName,
                                                        Value = x.CategoryId.ToString()
                                                    }).ToList();
                ViewBag.categoryList = getCategory;

                return View(updateProduct);
            }
        }

        /// <summary>
        /// Güncellenen ürünü veri tabanına kaydeden action.
        /// </summary>
        /// <param name="updateProduct">Güncellenen ürün modeli.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(Product updateProduct)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var category = uow.GetRepository<Category>().Get(x => x.CategoryId == updateProduct.Category.CategoryId);
                updateProduct.Category = category;
                uow.GetRepository<Product>().Update(updateProduct);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}