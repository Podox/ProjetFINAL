using Microsoft.AspNetCore.Mvc;
using Projet1.Data;
using Projet1.Models;
using System.Linq;

public class AccountController : Controller
{
    private readonly Projet1Context _context;

    public AccountController(Projet1Context context)
    {
        _context = context;
    }

    // GET: CreateAccount
    public IActionResult CreateAccount()
    {
        return View("~/Views/Account/CreateAccount.cshtml");
    }

    // POST: CreateAccount
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateAccount(Utilisateur utilisateur)
    {
        if (ModelState.IsValid)
        {
            // Set the EntrepriseUId to 0 or a default value
    
            // Add the user to the database
            _context.Utilisateur.Add(utilisateur);
            _context.SaveChanges();

            // After creating the user, redirect to the login page
            return RedirectToAction("Login");
        }

        // If the model is invalid, return to the CreateAccount view with the current model
        return View("~/Views/Account/CreateAccount.cshtml", utilisateur);
    }

    // GET: Login
    public IActionResult Login()
    {
        return View("~/Views/Account/Login.cshtml");
    }

    // POST: Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string email, string motDePasse)
    {
        // Check if the user exists with the given email and password
        var utilisateur = _context.Utilisateur
            .FirstOrDefault(u => u.Email == email && u.MotDePasse == motDePasse);

        if (utilisateur != null)
        {
            // Store user email or other relevant info in session
            HttpContext.Session.SetString("UserEmail", utilisateur.Email);

            HttpContext.Session.SetString("UserId", utilisateur.Id.ToString());

            HttpContext.Session.SetString("Username", utilisateur.Nom);
            if (utilisateur.Email == "badramripodoxadmin@gmail.com")
            {
                // Redirect to a different view if the user is this specific admin
              
                return View("~/Views/Account/AdminDashboard.cshtml");
            }
            // Redirect to the Home page after successful login
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // Add a model error if login credentials are incorrect
            ModelState.AddModelError("", "Invalid email or password.");
            return View("~/Views/Account/Login.cshtml");
        }
    }

    // POST: Logout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        // Clear session data when logging out
        HttpContext.Session.Clear();

        // Redirect to the login page after logging out
        return RedirectToAction("Login", "Account");
    }
}
