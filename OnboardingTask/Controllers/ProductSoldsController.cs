using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnboardingTask.Entities;

namespace OnboardingTask.Controllers
{
    public class ProductSoldsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: ProductSolds
        public ActionResult Index()
        {
            //var productSold = db.ProductSold.Include(p => p.Customer).Include(p => p.Product).Include(p => p.Store);
            return View(db.ProductSold.ToList());
        }

        // GET: ProductSolds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSold.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // GET: ProductSolds/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
            ViewBag.StoreId = new SelectList(db.Store, "Id", "Name");
            return View();
        }

        // POST: ProductSolds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.ProductSold.Add(productSold);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name", productSold.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", productSold.ProductId);
            ViewBag.StoreId = new SelectList(db.Store, "Id", "Name", productSold.StoreId);
            return View(productSold);
        }

        public ActionResult EditSales(int Id)
        {

            ProductSold model = new ProductSold();
            
            if (Id > 0)
            {
                //var productSold = db.ProductSold.Include(p => p.Customer).Include(x => x.Product).Include(y => y.Store);
                //List<ProductSold> sList = productSold.ToList();
                ProductSold sale = db.ProductSold
                                         .Include(x => x.Customer).Include(x => x.Product).Include(x => x.Store)
                                    //      .Where(i => i.Id == Id)
                                         .SingleOrDefault(x => x.Id == Id);
                //ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name", productSold.CustomerId);
                //ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", productSold.ProductId);
                //ViewBag.StoreId = new SelectList(db.Store, "Id", "Name", productSold.StoreId);
                //ProductSold sale = db.ProductSold.SingleOrDefault(x => x.Id == Id);
                model.Id = sale.Id;
                model.ProductId = sale.Product.Id;
                model.CustomerId = sale.Customer.Id;
                model.StoreId = sale.Store.Id;
                model.DateSold = sale.DateSold;
                //ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name");
                //ViewBag.ProductId = new SelectList(db.Product, "Id", "Name");
                //ViewBag.StoreId = new SelectList(db.Store, "Id", "Name");
                //ViewBag.DateSold = new SelectList(db.ProductSold, "DateSold", "DateSold");
                
            }

            return PartialView("SalesView", model);
        }

        // GET: ProductSolds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSold.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name", productSold.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", productSold.ProductId);
            ViewBag.StoreId = new SelectList(db.Store, "Id", "Name", productSold.StoreId);
            return View(productSold);
        }

        // POST: ProductSolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       // [HttpPost]
        
        public ActionResult EditConfirmed([Bind(Include = "Id,ProductId,CustomerId,StoreId,DateSold")] ProductSold productSold)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSold).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customer, "Id", "Name", productSold.CustomerId);
            ViewBag.ProductId = new SelectList(db.Product, "Id", "Name", productSold.ProductId);
            ViewBag.StoreId = new SelectList(db.Store, "Id", "Name", productSold.StoreId);
            return View(productSold);
        }

        // GET: ProductSolds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSold.Find(id);
            if (productSold == null)
            {
                return HttpNotFound();
            }
            return View(productSold);
        }

        // POST: ProductSolds/Delete/5
       // [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSold productSold = db.ProductSold.Find(id);
            db.ProductSold.Remove(productSold);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult SalesView()
        {
            ProductSold model = new ProductSold();
            return PartialView("SalesView", model);
        }

        [HttpPost]
        public JsonResult SalesView(ProductSold productSold)
        {
            db.Database.BeginTransaction();
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(productSold).State = EntityState.Modified;
                    if (db.SaveChanges() > 0)
                    {
                        db.Database.CurrentTransaction.Commit();

                        Response.StatusCode = (int)HttpStatusCode.OK;
                        return Json(new { Result = "OK" });
                    }
                }
            }
            catch (Exception oEx)
            {
                db.Database.CurrentTransaction.Rollback();
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(new { Result = "ERROR", Message = oEx.Message });
            }

            db.Database.CurrentTransaction.Rollback();
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Result = "ERROR" });
        }

        public JsonResult GetRecord(int? id)
        {
            if (id == null)
            {
                return Json(HttpStatusCode.BadRequest);
            }
            ProductSold productSold = db.ProductSold.Include(x => x.Product).Include(x => x.Customer).Include(x => x.Store).FirstOrDefault(r => r.Id == id);
            if (productSold == null)
            {
                return Json(HttpStatusCode.NotFound);
            }

            return Json(productSold, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSalesList()
        {
            var productSold = db.ProductSold.Include(p => p.Customer).Include(x => x.Product).Include(y => y.Store);
            List<ProductSold> SaleList = productSold.ToList();
            return Json(SaleList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerList()
        {
            List<Customer> CusList = db.Customer.ToList();
            return Json(CusList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductList()
        {
            List<Product> ProdList = db.Product.ToList();
            return Json(ProdList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStoreList()
        {
            List<Store> StorList = db.Store.ToList();
            return Json(StorList, JsonRequestBehavior.AllowGet);
        }
    }
}
