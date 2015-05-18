using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WaiwardDemo.Models;

namespace WaiwardDemo.Controllers
{
    public class HseqCaseFilesController : Controller
    {
        private WaiwardDemoContext db = new WaiwardDemoContext();

        // GET: HseqCaseFiles
        public ActionResult Index()
        {
            return View(db.HseqCaseFiles.ToList());
        }

        // GET: HseqCaseFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HseqCaseFile hseqCaseFile = db.HseqCaseFiles.Find(id);
            if (hseqCaseFile == null)
            {
                return HttpNotFound();
            }
            return View(hseqCaseFile);
        }

        // GET: HseqCaseFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HseqCaseFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HseqCaseFileID,CaseNo,NcrID")] HseqCaseFile hseqCaseFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.HseqCaseFiles.Add(hseqCaseFile);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    Debug.WriteLine(ex.Message);
                    
                    return RedirectToAction("Create");
                }
            }

            return View(hseqCaseFile);
        }

        // GET: HseqCaseFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HseqCaseFile hseqCaseFile = db.HseqCaseFiles.Find(id);
            if (hseqCaseFile == null)
            {
                return HttpNotFound();
            }
            return View(hseqCaseFile);
        }

        // POST: HseqCaseFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HseqCaseFileID,CaseNo,NcrID")] HseqCaseFile hseqCaseFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hseqCaseFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hseqCaseFile);
        }

        // GET: HseqCaseFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HseqCaseFile hseqCaseFile = db.HseqCaseFiles.Find(id);
            if (hseqCaseFile == null)
            {
                return HttpNotFound();
            }
            return View(hseqCaseFile);
        }

        // POST: HseqCaseFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HseqCaseFile hseqCaseFile = db.HseqCaseFiles.Find(id);
            
            //Abhi
            if (hseqCaseFile != null) {

                if (hseqCaseFile.NcrID != null) {

                    Ncr ncr = db.Ncrs.Find(hseqCaseFile.NcrID);

                    if (ncr != null) {
                        db.Ncrs.Remove(ncr);
                    }
                }
            }
            
            db.HseqCaseFiles.Remove(hseqCaseFile);
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
    }
}
