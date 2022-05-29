using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockDemoMvc.Controllers
{
    public class ValueController : Controller
    {
        // GET: ValueController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ValueController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ValueController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ValueController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ValueController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ValueController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ValueController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ValueController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
