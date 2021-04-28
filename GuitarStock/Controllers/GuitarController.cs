using GuitarStock.Data;
using GuitarStock.Models;
using GuitarStock.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GuitarStock.Controllers
{
    public class GuitarController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GuitarController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<GuitarViewModel> objList = new List<GuitarViewModel>();

            var allGuitars = _db.Guitars;

            foreach (var g in allGuitars)
            {
                objList.Add(new GuitarViewModel(g,
                                      _db.Images.Where(i => i.GuitarID == g.Id.ToString())));
            }

            return View(objList);
        }

        //Get Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        //Post Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Guitar obj)
        {
            if (ModelState.IsValid)
            {
                _db.Guitars.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                GuitarViewModel obj = new GuitarViewModel(_db.Guitars.Find(id),
                                                          _db.Images.Where(i => i.GuitarID == id.ToString()));
                if (obj.Guitar.Id > 0)
                    return View(obj);
                else
                    return Ok("Item not found");
            }
            else
                return Ok("No details passed to controller");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GuitarViewModel obj)
        {
            _db.Guitars.Update(obj.Guitar);
            _db.SaveChanges();
            return RedirectToAction("Index", "Guitar");
        }

        [HttpPost("FileUpload")]
        public async Task<IActionResult> UploadFile(List<IFormFile> files, int? Id)
        {
            long size = files.Sum(f => f.Length);

            foreach (var formFile in files)
            {

                if (formFile.Length > 0)
                {
                    Image thisImage = new Image("images/guitars/" + Guid.NewGuid().ToString() + ".jpg", Id.ToString());

                    // add image to database
                    _db.Images.Add(thisImage);
                    _db.SaveChanges();

                    //Save file to wwwroot/images folder
                    using (var stream = new FileStream(Path.Combine("wwwroot/" + thisImage.URL), FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return RedirectToAction("Index", "Guitar");
        }

        // Get Delete
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var obj = _db.Guitars.Find(id);
            if (obj == null) return NotFound();
            return View(obj);
        }

        // PostDelete
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Guitars.Find(id);
            if (obj == null) return NotFound();
            _db.Guitars.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Get Search
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(SearchModel sm)
        {
            List<GuitarViewModel> objList = new List<GuitarViewModel>();
            
            if(sm.SearchProperty.ToUpper() == "MODEL")
            {
                var foundGuitars = _db.Guitars.Where(i => i.Model.Contains(sm.SearchTerm));
                foreach (var g in foundGuitars)
                {
                    objList.Add(new GuitarViewModel(g, _db.Images.Where(i => i.GuitarID == g.Id.ToString())));
                }
            }
            else if (sm.SearchProperty.ToUpper() == "BRAND")
            {
                var foundGuitars = _db.Guitars.Where(i => i.Brand.Contains(sm.SearchTerm));
                foreach (var g in foundGuitars)
                {
                    objList.Add(new GuitarViewModel(g, _db.Images.Where(i => i.GuitarID == g.Id.ToString())));
                }
            }
            else
            {
                return View("Index", _db.Guitars);

            }
            return View("Index", objList);
        }

        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                GuitarViewModel obj = new GuitarViewModel(_db.Guitars.Find(id),
                                                          _db.Images.Where(i => i.GuitarID == id.ToString()));
                if (obj.Guitar.Id > 0)
                    return View(obj);
                else
                    return Ok("Item not found");
            }
            else
                return Ok("No details passed to controller");
        }

        [HttpPost]
        public IActionResult AddToComparison(int? id)
        {
            // Find the object by id in the db (with its images)
            GuitarViewModel obj = new GuitarViewModel(_db.Guitars.Find(id),
                                                      _db.Images.Where(i => i.GuitarID == id.ToString()));

            // Get the current session comparison model
            GuitarComparisonViewModel compModel = HttpContext.Session.Get<GuitarComparisonViewModel>(SiteConstants.SessionName);

            if (compModel == null)
            {
                HttpContext.Session.Set(SiteConstants.SessionName, new GuitarComparisonViewModel());
                compModel = HttpContext.Session.Get<GuitarComparisonViewModel>(SiteConstants.SessionName);
            }

            // Check if the obj found is already part of the comparison. (Early Out)
            if (compModel.IsInComparison(obj)) return RedirectToAction("Index", "Guitar");

            // If the comparison is full, replace guitar 1 with 2, then set 2 to the new object
            if (compModel.IsFull())
            {
                compModel.Guitar1 = compModel.Guitar2;
                compModel.Guitar2 = obj;
            }
            else if (compModel.Guitar1 == null) compModel.Guitar1 = obj;
            else if (compModel.Guitar2 == null) compModel.Guitar2 = obj;
            
            // Set the session and redirect
            HttpContext.Session.Set(SiteConstants.SessionName, compModel);

            return RedirectToAction("Compare", "Guitar");
        }

        [HttpPost]
        public IActionResult RemoveFromComparison(int? id)
        {
            // Find the object by id in the db (with its images)
            GuitarViewModel obj = new GuitarViewModel(_db.Guitars.Find(id),
                                                      _db.Images.Where(i => i.GuitarID == id.ToString()));

            GuitarComparisonViewModel compModel = HttpContext.Session.Get<GuitarComparisonViewModel>(SiteConstants.SessionName);

            if (compModel.IsInComparison(obj))
            {
                if (compModel.Guitar1.Guitar.Id == obj.Guitar.Id) compModel.Guitar1 = null;
                else compModel.Guitar2 = null;
            }

           HttpContext.Session.Set<GuitarComparisonViewModel>(SiteConstants.SessionName, compModel);

            return RedirectToAction("Index", "Guitar");
        }

        public IActionResult Compare()
        {
            return View();
        }
    }
}
