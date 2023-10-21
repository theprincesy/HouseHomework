using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using houseHomework.Models;

namespace houseHomework.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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

    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ContactUs(ContactFormViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Process the form data and send the email here
            // You can use libraries like SmtpClient or SendGrid to send emails
            // Example: SendEmail(model.Name, model.Email, model.Message);
            // Redirect to a thank you page or show a success message
            return RedirectToAction("ThankYou");
        }

        // If the model is not valid, return to the contact page with validation errors
        return View("ContactUs", model);
    }


    public IActionResult BuyReviews()
    {
        return View();
    }

}
