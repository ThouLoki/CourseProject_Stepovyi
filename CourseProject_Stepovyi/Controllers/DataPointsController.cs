using System;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandomData(RandomDataViewModel rnd)
        {
            Random random = new Random();
            List<DataPoint> temp2 = new List<DataPoint> { };
            double rndtempx = 0;
            double rndtempy = 0;
            for (int i = 0; i < rnd.DotsCount; i++)
            {
                rndtempx += Math.Round(random.NextDouble(0.11, 1.99), 3);
                rndtempy += Math.Round(random.NextDouble(0.11, 1.99), 3);
                temp2.Add(new DataPoint { x = rndtempx, y = rndtempy });
            }
            _context.DataPoint.AddRange(temp2);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Graph()
        {
            List<DataPoint> temp = new List<DataPoint> { };
            temp = _context.DataPoint.ToList<DataPoint>();
            ViewBag.DataPoints = temp;
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
        public async Task<IActionResult> Create([Bind("x,y,ID")] DataPoint dataPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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