using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;
using work_01_with_Image.Models;
using work_01_with_Image.Models.ViewModels;

namespace work_01_with_Image.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductDbContext _context;
        private readonly IWebHostEnvironment _he;

        public ProductsController(ProductDbContext _context, IWebHostEnvironment _he)
        {
            this._context = _context;
            this._he = _he;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.msg = TempData["msg"];
            return View(await _context.Products.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.msg = TempData["msg"];
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,Name,Unit,Price,Image")] ProductVM vM)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                Product p = new Product();
                p.Name = vM.Name;
                p.Unit = vM.Unit;
                p.Price = vM.Price;

                //Image
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string imgFileName = Guid.NewGuid()+"_"+Path.GetFileName(vM.Image.FileName);
                string fileToWrite = Path.Combine(webroot, folder, imgFileName);

                //for database
                using (MemoryStream ms=new MemoryStream())
                {
                    await vM.Image.CopyToAsync(ms);
                    p.Image = ms.ToArray();
                    p.ImageFile = "/" + folder + "/" + imgFileName;
                }
                //for folder
                using (var stream=new FileStream(fileToWrite, FileMode.Create))
                {
                    await vM.Image.CopyToAsync(stream);
                }
                _context.Add(p);
                await _context.SaveChangesAsync();
                msg = "Data inserted successfully!!!";
                TempData["msg"] = msg;
                return RedirectToAction("Index");
            }
            else
            {
                msg = "Product data is incomplete. Please try again...!!";
            }
            TempData["msg"] = msg;
            return RedirectToAction("Create");
        }
       
    }
}
