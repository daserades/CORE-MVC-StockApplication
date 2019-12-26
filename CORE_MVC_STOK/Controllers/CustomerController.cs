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
    //Müşteri işlemlerinin gerçekleştiği sınıf.
    public class CustomerController : Controller
    {
        #region Member
        private MasterContext _masterContext;
        #endregion

        #region Constructor
        public CustomerController(MasterContext masterContext)
        {
            _masterContext = masterContext;
        }
        #endregion

        #region Action Methods

        /// <summary>
        /// Müşteri listesi actionu.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var customerList = uow.GetRepository<Customer>().GetAll().ToList();
                return View(customerList);
            }
        }

        /// <summary>
        /// Müşteri oluşturma sayfasını açan action.
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
        /// Müşteri modelini veritabanına ekleyen action.
        /// </summary>
        /// <param name="newCustomer">Eklenecek müşteri modeli.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(Customer newCustomer)
        {
            
                using (UnitOfWork uow =new UnitOfWork (_masterContext))
                {
                    var category = uow.GetRepository<Category>().Get(x => x.CategoryId == newCustomer.Category.CategoryId);
                    newCustomer.Category = category;
                    uow.GetRepository<Customer>().Create(newCustomer);
                    uow.SaveChanges();
                    return RedirectToAction("Index");
                }
            
        }

        /// <summary>
        /// Müşteri silen action.
        /// </summary>
        /// <param name="id">Silinecek müşteri id'si</param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            using (UnitOfWork uow =new UnitOfWork (_masterContext))
            {
                var result = uow.GetRepository<Sales>().Get(x => x.CustomerId == id);
                var deleteCustomer = uow.GetRepository<Customer>().Get(x => x.CustomerId == id);
                if (result!=null)
                {
                    TempData["Warning"] = deleteCustomer.CustomerName + " Adlı müşteriyi silemezsiniz !.Kullanımda.";
                    return RedirectToAction("Index");
                }
                uow.GetRepository<Customer>().Delete(deleteCustomer);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Müşteri bilgilerini güncelleme sayfasını açan action.
        /// </summary>
        /// <param name="id">Güncellenecek müşteri id'si.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (UnitOfWork uow=new UnitOfWork (_masterContext))
            {
                var updateCustomer = uow.GetRepository<Customer>().Get(x => x.CustomerId == id);
                return View(updateCustomer);
            }
        }

        /// <summary>
        /// Müşterinin güncellenen bilgilerini veri tabanına kaydeden action.
        /// </summary>
        /// <param name="updateCustomer">Güncellenen müşteri modeli.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(Customer updateCustomer)
        {
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow=new UnitOfWork (_masterContext))
                {
                    uow.GetRepository<Customer>().Update(updateCustomer);
                    uow.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(updateCustomer);
        }


        public IActionResult test()
        {

            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
               
                var category = uow.GetRepository<Category>().Include(x=>x.Customer).ToList();
                int [] categoryList = category.Select(x => x.CategoryId).Distinct().ToArray();
                var products = uow.GetRepository<Product>().GetAll(x => categoryList.Contains(x.CategoryId)).ToList();
              
                List<CustomerDto> xxx = new List<CustomerDto>();

                foreach (var bp00 in category)
                {
                    xxx.Add(new CustomerDto(bp00));

                }

            }


            return RedirectToAction("Index");
        }



        #endregion
    }
}