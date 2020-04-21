using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiUserMVC.Models;
namespace MultiUserMVC.Controllers
{
    public class StocksController : Controller
    {
        private readonly ApplicationDBContext _db;

        public StocksController(ApplicationDBContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Stock Stock { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Stock = new Stock();
            if (id == null)
            {
                //Create
                return View(Stock);
            }

            //Update
            Stock = _db.Stocks.FirstOrDefault(u => u.Id == id);
            if (Stock == null)
            {
                return NotFound();
            }
            return View(Stock);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Scrape()
        {

            var scraper = new Scraper();

            scraper.saveStocks();

            //Pushes changes to database
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Stocks.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var stockFromDb = await _db.Stocks.FirstOrDefaultAsync(u => u.Id == id);
            if (stockFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Stocks.Remove(stockFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}