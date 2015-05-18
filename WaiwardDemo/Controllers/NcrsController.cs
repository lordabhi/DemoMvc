using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WaiwardDemo.Models;

namespace WaiwardDemo.Controllers
{
    public class NcrsController : Controller
    {
        private WaiwardDemoContext db = new WaiwardDemoContext();

        // GET: Ncrs
        public ActionResult Index()
        {
            var ncrs = db.Ncrs.Include(n => n.DiscrepancyType).Include(n => n.HseqCaseFile);
            return View(ncrs.ToList());
        }

        // GET: Ncrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ncr ncr = db.Ncrs.Find(id);
            if (ncr == null)
            {
                return HttpNotFound();
            }
            return View(ncr);
        }

        // GET: Ncrs/Create
        public ActionResult Create()
        {
            ViewBag.DiscrepancyTypeID = new SelectList(db.DiscrepancyTypes, "DiscrepancyTypeID", "Name");
            ViewBag.HseqCaseFileID = new SelectList(db.HseqCaseFiles, "HseqCaseFileID", "HseqCaseFileID");
            
            ViewBag.RecordType = "NCR";
            ViewBag.EnteredBy = "Test User";
            ViewBag.ReportedBy = "Test User, Sales";
            ViewBag.QualityCoordinator = "Mr. Paul Smith";
            ViewBag.Status = "Pending";
            
            return View();
        }

        // POST: Ncrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NcrID,NcrSource,NcrState,DiscrepancyTypeID,HseqCaseFileID,Title,Description,RecordType,EnteredBy,ReportedBy,QualityCoordinator,Status")] Ncr ncr)
        {
            if (ModelState.IsValid)
            {

                //Create the HseqCaseFile

                int caseNo = 1;

                //if (db.HseqCaseFiles.ToList() != null && db.HseqCaseFiles.ToList().LongCount() > 0)
                //{

                //    caseNo = db.HseqCaseFiles.ToList().Last().CaseNo + 1;
                //}


                IList<HseqCaseFile> hseqCaseFilesList = db.HseqCaseFiles.ToList();

                if (hseqCaseFilesList != null && hseqCaseFilesList.LongCount() > 0)
                {

                    caseNo = hseqCaseFilesList.Max(p => p.CaseNo) + 1;

                }

                HseqCaseFile hseqCaseFile = new HseqCaseFile();
                
                hseqCaseFile.CaseNo = caseNo;

                db.HseqCaseFiles.Add(hseqCaseFile);

                ncr.HseqCaseFile = hseqCaseFile;
                ncr.HseqCaseFileID = hseqCaseFile.HseqCaseFileID;

                db.Ncrs.Add(ncr);
                db.SaveChanges();


                hseqCaseFile.NcrID = ncr.NcrID;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.DiscrepancyTypeID = new SelectList(db.DiscrepancyTypes, "DiscrepancyTypeID", "Name", ncr.DiscrepancyTypeID);
            ViewBag.HseqCaseFileID = new SelectList(db.HseqCaseFiles, "HseqCaseFileID", "HseqCaseFileID", ncr.HseqCaseFileID);
            return View(ncr);
        }

        // GET: Ncrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ncr ncr = db.Ncrs.Find(id);
            if (ncr == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiscrepancyTypeID = new SelectList(db.DiscrepancyTypes, "DiscrepancyTypeID", "Name", ncr.DiscrepancyTypeID);
            ViewBag.HseqCaseFileID = new SelectList(db.HseqCaseFiles, "HseqCaseFileID", "HseqCaseFileID", ncr.HseqCaseFileID);
            return View(ncr);
        }

        // POST: Ncrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NcrID,NcrSource,NcrState,DiscrepancyTypeID,HseqCaseFileID,Title,Description,RecordType,EnteredBy,ReportedBy,QualityCoordinator,Status")] Ncr ncr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ncr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiscrepancyTypeID = new SelectList(db.DiscrepancyTypes, "DiscrepancyTypeID", "Name", ncr.DiscrepancyTypeID);
            ViewBag.HseqCaseFileID = new SelectList(db.HseqCaseFiles, "HseqCaseFileID", "HseqCaseFileID", ncr.HseqCaseFileID);
            return View(ncr);
        }

        // GET: Ncrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ncr ncr = db.Ncrs.Find(id);
            if (ncr == null)
            {
                return HttpNotFound();
            }
            return View(ncr);
        }

        // POST: Ncrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ncr ncr = db.Ncrs.Find(id);

            //Abhi
            if (ncr.HseqCaseFileID != null) {

                HseqCaseFile hseqCaseFile = db.HseqCaseFiles.Find(ncr.HseqCaseFileID);
                if (hseqCaseFile != null) {
                    db.HseqCaseFiles.Remove(hseqCaseFile);
                }
            }

            db.Ncrs.Remove(ncr);
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
