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
    public class SalesController : Controller
    {
        #region Member
        private MasterContext _masterContext;
        #endregion

        #region Constructor
        public SalesController(MasterContext masterContext)
        {
            _masterContext = masterContext;
        }
        #endregion

        #region Action Methods

        /// <summary>
        /// Satışlar listesi actionu.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var salesList = uow.GetRepository<Sales>().Include(x => x.Customer).ToList();
                salesList = uow.GetRepository<Sales>().Include(x => x.Product).ToList();
                return View(salesList);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<SelectListItem> getProduct = (from x in _masterContext.Products.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.ProductName,
                                                   Value = x.ProductId.ToString()
                                               }).ToList();
            ViewBag.productList = getProduct;

            List<SelectListItem> getCustomer = (from x in _masterContext.Customers.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.CustomerName + " " + x.CustomerSurname,
                                                    Value = x.CustomerId.ToString()
                                                }).ToList();
            ViewBag.customerList = getCustomer;
            return View();
        }

        [HttpPost]
        public IActionResult Create(Sales newSales)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var customer = uow.GetRepository<Customer>().Get(x => x.CustomerId == newSales.Customer.CustomerId);
                var product = uow.GetRepository<Product>().Get(x => x.ProductId == newSales.Product.ProductId);
                newSales.Product = product;
                newSales.Customer = customer;
                uow.GetRepository<Sales>().Create(newSales);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public IActionResult Delete(int id)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var deleteSales = uow.GetRepository<Sales>().Get(x => x.SaleId == id);
                uow.GetRepository<Sales>().Delete(deleteSales);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            List<SelectListItem> getProduct = (from x in _masterContext.Products.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.ProductName,
                                                   Value = x.ProductId.ToString()
                                               }).ToList();
            ViewBag.productList = getProduct;

            List<SelectListItem> getCustomer = (from x in _masterContext.Customers.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.CustomerName + " " + x.CustomerSurname,
                                                    Value = x.CustomerId.ToString()
                                                }).ToList();
            ViewBag.customerList = getCustomer;
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var updateSales = uow.GetRepository<Sales>().Get(x => x.SaleId == id);
                return View(updateSales);
            }
        }


        [HttpPost]
        public IActionResult Update(Sales updateSales)
        {
            using (UnitOfWork uow = new UnitOfWork(_masterContext))
            {
                var customer = uow.GetRepository<Customer>().Get(x => x.CustomerId == updateSales.Customer.CustomerId);
                var product = uow.GetRepository<Product>().Get(x => x.ProductId == updateSales.Product.ProductId);
                updateSales.Customer = customer;
                updateSales.Product = product;
                uow.GetRepository<Sales>().Update(updateSales);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        #endregion
    }
}