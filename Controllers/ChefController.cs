using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers;

public class ChefController : Controller
{
    private MyContext db;
    public ChefController(MyContext context)
    {
        db = context;
    }

    [HttpGet("")]
    public IActionResult ChefIndex()
    {
        List<Chef> AllChefs = db.Chefs.Include(chef=>chef.Dishes).ToList();
        return View("ChefIndex", AllChefs);
    }

    [HttpGet("chefs/new")]
    public IActionResult NewChef()
    {
        return View("NewChef");
    }

    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (ModelState.IsValid)
        {
            db.Add(newChef);
            db.SaveChanges();
            return RedirectToAction("ChefIndex");
        }
        else
        {
            return NewChef();
        }
    }
}