// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using RandomPasscode.Models;

// namespace RandomPasscode.Controllers;

// public class HomeController : Controller
// {
//     private readonly ILogger<HomeController> _logger;

//     public HomeController(ILogger<HomeController> logger)
//     {
//         _logger = logger;
//     }

//     public IActionResult Index()
//     {
//         return View();
//     }

//     public IActionResult Privacy()
//     {
//         return View();
//     }

//     [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//     public IActionResult Error()
//     {
//         return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//     }
// }
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
 #pragma warning  disable C58618

public class HomeController : Controller
{
    private static Random random = new Random();

    public IActionResult Index()
    {
        int count = HttpContext.Session.GetInt32("count") ?? 0;
        ViewBag.Count = count;
        return View();
    }
    [HttpPost]
    public IActionResult GeneratePasscode()
    {
        int count = HttpContext.Session.GetInt32("count") ?? 0;

        count++;
        HttpContext.Session.SetInt32("count", count);

        string passcode = GeneratePasscode(14);
        return Json(new{ count, passcode});
    }
    
    private string GeneratePasscode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }
        return result.ToString();
    }
}
