using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;

namespace RandomPasscode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        string? randomPassCode = HttpContext.Session.GetString("RandomPassCode");
        randomPassCode ??= "Nothing yet...";
        HttpContext.Session.SetString("RandomPassCode", randomPassCode);
        return View();
    }

    public IActionResult Generate ()
    {
        string? randomPassCode = HttpContext.Session.GetString("RandomPassCode");
        string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        var rand = new Random();
        var result = new string(
            Enumerable.Repeat(abc, 14)
                .Select(s => s[rand.Next(s.Length)])
                .ToArray()
        );
        HttpContext.Session.SetString("RandomPassCode", result);
        int count = HttpContext.Session.GetInt32("TimesGenerated") ?? 0;
        count++;
        HttpContext.Session.SetInt32("TimesGenerated", count);
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
