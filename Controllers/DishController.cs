using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;

public class DishController : Controller
{
    private MyContext db;

    public DishController(MyContext context)
    {
        db = context;
    }

    [HttpGet("dishes")]
    public IActionResult DishIndex()
    {
        List<Dish> AllDishes = db.Dishes.Include(dish=>dish._Chef).ToList(); 
        return View("DishIndex", AllDishes);
    }

    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {
        ViewBag.AllChefs = db.Chefs.ToList();
        return View("NewDish");
    }

    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine(newDish.ChefId);
            db.Add(newDish);
            db.SaveChanges();
            return RedirectToAction("DishIndex");
        }
        else
        {
            return NewDish();
        }
    }
}