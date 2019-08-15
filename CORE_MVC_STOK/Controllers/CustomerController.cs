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
            if (ModelState.IsValid)
            {
                using (UnitOfWork uow =new UnitOfWork (_masterContext))
                {
                    uow.GetRepository<Customer>().Create(newCustomer);
                    uow.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(newCustomer);
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

        [HttpGet]
        public IActionResult Update(int id)
        {
            using (UnitOfWork uow=new UnitOfWork (_masterContext))
            {
                var updateCustomer = uow.GetRepository<Customer>().Get(x => x.CustomerId == id);
                return View(updateCustomer);
            }
        }

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
        #endregion
    }
}