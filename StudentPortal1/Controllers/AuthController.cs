using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Dtos;
using StudentPortal1.Repositories;
using System.Linq;
using System.Threading.Tasks;
using StudentPortal1.Models.Dtos;

namespace StudentPortal1.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
       private readonly EmployeeDbContext _context;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository,EmployeeDbContext context)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
          _context = context;
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto registerdto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerdto);
            }

            var identityUser = new IdentityUser
            {
                UserName = registerdto.UserName,
                Email = registerdto.UserName
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerdto.Password);

            if (identityResult.Succeeded)
            {
                if (registerdto.Roles != null && registerdto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerdto.Roles);

                    if (identityResult.Succeeded)
                    {
                        //ViewBag.Message = "User was registered! Please Login";
                        TempData["Message"] = "User was registered! Please login.";
                        return RedirectToAction("Login","Auth");
                    }
                }
            }

            TempData["Error"] = "Something went wrong. Please try again.";
            return View(registerdto);
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequestDto);
            }

            var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());


                        // Update LastLoginDate in Employee table using raw SQL query
                        //string sql = "UPDATE Employees SET LastLoginDate = @LastLoginDate WHERE EmpId = @UserId";
                        //var parameters = new[]
                        //{
                        //    new Microsoft.Data.SqlClient.SqlParameter("@LastLoginDate", DateTime.UtcNow),
                        //    new Microsoft.Data.SqlClient.SqlParameter("@UserId", user.Id)
                        //};

                        //var count=await _context.Database.ExecuteSqlRawAsync(sql, parameters);

                        var response = new LoginResponceDto
                        {
                            JwtToken = jwtToken,
                        };

                        // Save JWT in a cookie or session for use
                        // You can set a cookie like this
                        HttpContext.Response.Cookies.Append("JwtToken", jwtToken);
                      //  TempData["Message"] = "User was registered! Please login.";
                        return RedirectToAction("Index", "Employee"); // Redirect to some secure area
                    }
                }
            }

            TempData["Error"] = "Something went wrong. Please try again.";
            return View(loginRequestDto);
        }
    }
}

