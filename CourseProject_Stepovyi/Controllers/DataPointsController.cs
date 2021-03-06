﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseProject_Stepovyi.Models;
using CourseProject_Stepovyi.ViewModels;
namespace CourseProject_Stepovyi.Controllers
{
    public class DataPointsController : Controller
    {
        private readonly CourseProject_StepovyiContext _context;

        public DataPointsController(CourseProject_StepovyiContext context)
        {
            _context = context;
        }

        // GET: DataPoints
        public async Task<IActionResult> Index()
        {
            return View(await _context.DataPoint.ToListAsync());
        }

        public IActionResult RandomData()
        {
            return View();
        }

        public IActionResult ComplexitySmall()
        {
            return View();
        }

        public IActionResult ComplexityBig()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandomData(RandomDataViewModel rnd,int kappa)
        {
            if (rnd.DotsCount == 0)
            {
                TempData["data"] = "Amount of Dots can be integer only";
                return RedirectToAction("RandomData",rnd);
            }
            ViewBag.kappa = "asdasd";    
            Random random = new Random();
            //rnd.x_start_point.Replace(".", ",");
            char dummyChar = '&'; //here put a char that you know won't appear in the strings
            rnd.x_start_point = rnd.x_start_point.Replace('.', dummyChar)
                                   .Replace(',', '.')
                                   .Replace(dummyChar, ',');
            rnd.y_start_point = rnd.y_start_point.Replace('.', dummyChar)
                                   .Replace(',', '.')
                                   .Replace(dummyChar, ',');
            //rnd.x_from = rnd.x_from.Replace('.', dummyChar)
            //                       .Replace(',', '.')
            //                       .Replace(dummyChar, ',');
            //rnd.x_to = rnd.x_to.Replace('.', dummyChar)
            //                       .Replace(',', '.')
            //                       .Replace(dummyChar, ',');
            //rnd.y_from = rnd.y_from.Replace('.', dummyChar)
            //                       .Replace(',', '.')
            //                       .Replace(dummyChar, ',');
            //rnd.y_to = rnd.y_to.Replace('.', dummyChar)
            //                       .Replace(',', '.')
            //                       .Replace(dummyChar, ',');
            rnd.from = rnd.from.Replace('.', dummyChar)
                                   .Replace(',', '.')
                                   .Replace(dummyChar, ',');
            rnd.to = rnd.to.Replace('.', dummyChar)
                                   .Replace(',', '.')
                                   .Replace(dummyChar, ',');
            List<DataPoint> temp2 = new List<DataPoint> { };
            double rndtempx = Convert.ToDouble(rnd.x_start_point);
            double rndtempy = Convert.ToDouble(rnd.y_start_point);
            temp2.Add(new DataPoint { x = rndtempx, y = rndtempy });
            for (int i = 0; i < rnd.DotsCount-1; i++)
            {
                rndtempx += Math.Round(random.NextDouble(Convert.ToDouble(rnd.from), Convert.ToDouble(rnd.to)), 3);
                rndtempy += Math.Round(random.NextDouble(Convert.ToDouble(rnd.from), Convert.ToDouble(rnd.to)), 3);
                temp2.Add(new DataPoint { x = rndtempx, y = rndtempy });
            }
            _context.DataPoint.AddRange(temp2);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Graph()
        {
            double error = 0,error_module=0,k_least,b_least;
            long olstime,moduletime;
            double k_module=0, b_module=0;
            List<DataPoint> temp = new List<DataPoint> { };
            List<DataPoint> temp1 = new List<DataPoint> { };
            List<DataPoint> temp2 = new List<DataPoint> { };
            temp = _context.DataPoint.ToList<DataPoint>();
            if (temp.Count() == 0)
                return RedirectToAction("Index");
            temp1 = Models.Methods.LeastSquare(temp, out error,out k_least, out b_least,out olstime);
            temp2 = Models.Methods.Module(temp, out k_module, out b_module,out error_module,out moduletime);
            ViewBag.LeastSquare = temp1;
            ViewBag.Module = temp2;
            ViewBag.DataPoints = temp;
            ViewBag.Error = error;
            ViewBag.k_least = k_least;
            ViewBag.b_least = b_least;
            ViewBag.k_module = k_module;
            ViewBag.b_module = b_module;
            ViewBag.ols_time = olstime;
            ViewBag.ModuleError = error_module;
            ViewBag.module_time = moduletime;
            return View();
        }




        // GET: DataPoints/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPoint = await _context.DataPoint
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dataPoint == null)
            {
                return NotFound();
            }

            return View(dataPoint);
        }

        // GET: DataPoints/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DataPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("x,y,ID")] DataPointsViewModel dataPoint)
        {

            char dummyChar = '&'; //here put a char that you know won't appear in the strings
            dataPoint.x = dataPoint.x.Replace('.', dummyChar)
                                   .Replace(',', '.')
                                   .Replace(dummyChar, ',');
            dataPoint.y = dataPoint.y.Replace('.', dummyChar)
                                   .Replace(',', '.')
                                   .Replace(dummyChar, ',');
            DataPoint temp = new DataPoint { };
            temp.x = Convert.ToDouble(dataPoint.x);
            temp.y= Convert.ToDouble(dataPoint.y);
                _context.Add(temp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            return View(dataPoint);
        }

        // GET: DataPoints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPoint = await _context.DataPoint.SingleOrDefaultAsync(m => m.ID == id);
            if (dataPoint == null)
            {
                return NotFound();
            }
            return View(dataPoint);
        }

        // POST: DataPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("x,y,ID")] DataPoint dataPoint)
        {
            if (id != dataPoint.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataPointExists(dataPoint.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dataPoint);
        }

        // GET: DataPoints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPoint = await _context.DataPoint
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dataPoint == null)
            {
                return NotFound();
            }

            return View(dataPoint);
        }

        // POST: DataPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataPoint = await _context.DataPoint.SingleOrDefaultAsync(m => m.ID == id);
            _context.DataPoint.Remove(dataPoint);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataPointExists(int id)
        {
            return _context.DataPoint.Any(e => e.ID == id);
        }

        public IActionResult Banish()
        {
            var itemsToDelete = _context.DataPoint.Where(x => x.x > -10000);
            _context.DataPoint.RemoveRange(itemsToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}