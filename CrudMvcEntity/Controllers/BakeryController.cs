using CrudMvcEntity.Data;
using CrudMvcEntity.Models;
using CrudMvcEntity.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudMvcEntity.Controllers
{
    public class BakeryController : Controller
    {
        private readonly MvcDemoDBContext mvcDemoDBContext;

        public BakeryController(MvcDemoDBContext mvcDemoDbContext)
        {
            this.mvcDemoDBContext = mvcDemoDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var bakeries = await mvcDemoDBContext.Bakery.ToListAsync();
            return View(bakeries);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBakeryViewModel addBakeryRequest)
        {
            var bakery = new Bakery()
            {
                Id = Guid.NewGuid(),
                Name = addBakeryRequest.Name,
                Description = addBakeryRequest.Description,
            };

            await mvcDemoDBContext.Bakery.AddAsync(bakery);
            await mvcDemoDBContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public  async Task<IActionResult> View(Guid id)
        {
            var bakery = await mvcDemoDBContext.Bakery.FirstOrDefaultAsync(x => x.Id == id);

            if (bakery != null)
            {
                var viewModel = new UpdateBakeryViewModel()
                {
                    Id = bakery.Id,
                    Name = bakery.Name,
                    Description = bakery.Description,
                };
                return await Task.Run(() => View("View" , viewModel));
            }

            return Redirect("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateBakeryViewModel model)
        {
            var bakery = await mvcDemoDBContext.Bakery.FindAsync(model.Id);

            if (bakery != null)
            {
                bakery.Name = model.Name;
                bakery.Description = model.Description;

                await mvcDemoDBContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateBakeryViewModel model)
        {
            var bakery = await mvcDemoDBContext.Bakery.FindAsync(model.Id);

            if(bakery != null)
            {
                mvcDemoDBContext.Bakery.Remove(bakery);
                await mvcDemoDBContext.SaveChangesAsync();

                return RedirectToAction("Index");   
            }
            return RedirectToAction("Index");
        }
    }
}
