using BL.Data;
using ECommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // the app redirect to login page & return returnUrl (when the user is not authenticated) as orderSucess page is protected
        public IActionResult Login(string returnUrl)
        {
            UserLogin user = new UserLogin
            {
                ReturnUrl = returnUrl
            };
            return View(user);
        }

        public async Task<IActionResult> LoginOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult Register()
        {
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            if (!ModelState.IsValid)
                return View("Register", model);

            ApplicationUser user = new ApplicationUser()
            {
                firstName = model.FirstName,
                lastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                //create user                                    * will be hashing *
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                                                            // for the login in the register page
                    var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                    
                    //add role to the user
                    var myuser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(myuser, "Customer");

                    if (loginResult.Succeeded)
                       return Redirect("~/");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin model)
        { 
            if(!ModelState.IsValid)
                return View("Login", model);

            //ApplicationUser user = new ApplicationUser()
            //{
            //    Email = model.Email,
            //    UserName = model.Email
            //};

            try
            {
                                                       // for the login in the login page
                var loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, true);
                if (loginResult.Succeeded)
                {
                    //to prevent open redirect attacks(security risks!)
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        return Redirect(model.ReturnUrl); // Redirect to original page
                    else
                        return RedirectToAction("Index", "Home"); // Home page
                }
                if (loginResult.IsLockedOut)
                { 
                    ModelState.AddModelError(string.Empty, "Your account is locked. please try again later.");
                    return View(new UserLogin());
                }
                //if login fails (wrong password or email), show error
                ModelState.AddModelError(string.Empty, "Invalid login attempt. please check your email and password.");
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
            // if loginResult is not succeeded
            return View(new UserLogin());
        }
        // not athorized
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
