using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Personal.MyApp.Data;
using Personal.MyApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace Personal.MyApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MyAppContext _context;
        private readonly ILogger<ItemsController> _logger;
        public ItemsController(MyAppContext context, ILogger<ItemsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        
        //GET: /Items
        public async Task <IActionResult> Index()
        {
            var item = await _context.Items.ToListAsync();
            return View(item);
            
        }

        //GET: /Items/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create([Bind("Id, Name, Description, Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(item);
        }

        //GET: Items/Edit/{id}
        public async Task <IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Return 404 if no id is provided
            }

            var item = await _context.Items.FindAsync(id); // Find the item in the database by id
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        //POST: Items/Edit/{id}
        [HttpPost]
        public async Task <IActionResult> Edit (int id, [Bind("Id, Name, Description, Price")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound(); // Check if the id passed in the URL matches the item Id
            }

            if (ModelState.IsValid)
            {
                _context.Update(item); // Update the item in the database
                await _context.SaveChangesAsync();  // Save the changes    
                return RedirectToAction("Index");
            }
            return View(item); 
        }

        //GET: Items/Delete/{Id}
        public async Task <IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);  // Find the item by its ID

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        //POST: Items/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation($"DeleteConfrmed call for ID: {id}");

            var item = await _context.Items.FindAsync(id); //Find the item by its id

            if (item == null)
            {
                _logger.LogWarning($"Item with ID {id} not found.");
                return NotFound(); // Return a 404 if the item is not found
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Item deleted. Redirecting to Index...");

            return RedirectToAction("Index");

           
        }
    }
}
