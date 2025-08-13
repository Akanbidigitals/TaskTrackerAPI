using Microsoft.AspNetCore.Mvc;

namespace TaskTrackerAPI.Controllers;

public class TaskTrackerController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}