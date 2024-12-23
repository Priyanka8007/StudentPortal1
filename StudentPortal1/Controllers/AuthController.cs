﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal1.Data;
using StudentPortal1.Models.Dtos;
using StudentPortal1.Repositories;
using System.Linq;
using System.Threading.Tasks;
using StudentPortal1.Models.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
//using StudentPortal1.Filters;

namespace StudentPortal1.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
       private readonly EmployeeDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository,EmployeeDbContext context, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Register")]
     //   [ServiceFilter(typeof(ActivityLoggingFilter))]
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
               

                // Return the view with the ModelState errors
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
                        // Store the plain text password in the database
                        //var connectionString = _configuration.GetConnectionString("StudentAuthConnectionString");
                        //using (var connection = new SqlConnection(connectionString))
                        //{
                            //await connection.OpenAsync();
                            //var command = new SqlCommand("UPDATE AspNetUsers SET PasswordStored = @Password WHERE UserName = @UserName", connection);
                            //command.Parameters.AddWithValue("@Password", registerdto.Password);
                            //command.Parameters.AddWithValue("@UserName", registerdto.UserName);
                            //int rowsAffected = await command.ExecuteNonQueryAsync();

                            // You can use rowsAffected variable to check the number of rows updated
                            //if (rowsAffected > 0)
                            //{
                                TempData["Message"] = "User was registered! Please login.";
                                return RedirectToAction("Login", "Auth");
                           // }
                           // else
                           // {
                                //TempData["Error"] = "Something went wrong. Please try again.";
                                //return View(registerdto);
                           // }
                        
                    }
                }
            }

            TempData["Error"] = "Something went wrong. Please try again.";
            return View(registerdto);
        }

        [HttpGet]
        [Route("Login")]
       // [ServiceFilter(typeof(ActivityLoggingFilter))]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
       // [ServiceFilter(typeof(ActivityLoggingFilter))]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequestDto);
            }

            //-------------------------------Stored Procedure calls---------------------------------------

            // Retrieve connection string from configuration
            //var connectionString = _configuration.GetConnectionString("StudentAuthConnectionString");

            //var passwordIsValid = false;
            //var errorMessage = string.Empty;

            //try
            //{
            //    using (var connection = new SqlConnection(connectionString))
            //    {
            //        await connection.OpenAsync();

            //        using (var command = connection.CreateCommand())
            //        {
            //            command.CommandText = "ValidatePassword";
            //            command.CommandType = System.Data.CommandType.StoredProcedure;

            //            // Adding parameters
            //            command.Parameters.Add(new SqlParameter("@Password", loginRequestDto.Password));
            //            command.Parameters.Add(new SqlParameter("@UserName", loginRequestDto.UserName));
            //            var isValidParam = new SqlParameter("@IsValid", System.Data.SqlDbType.Bit) { Direction = System.Data.ParameterDirection.Output };
            //            var errorParam = new SqlParameter("@ErrorMessage", System.Data.SqlDbType.NVarChar, 200) { Direction = System.Data.ParameterDirection.Output };

            //            command.Parameters.Add(isValidParam);
            //            command.Parameters.Add(errorParam);

            //            // Execute stored procedure
            //            await command.ExecuteNonQueryAsync();

            //            // Retrieve output parameters
            //            passwordIsValid = (bool)isValidParam.Value;
            //            errorMessage = errorParam.Value != DBNull.Value ? (string)errorParam.Value : string.Empty;
            //        }

            //        ViewBag.ModelState = ModelState;

            //        if (!passwordIsValid)
            //        {
            //            ModelState.AddModelError(string.Empty, errorMessage);
            //            return View(loginRequestDto);
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Handle exception, log error, or return a specific view with an error message
            //    ModelState.AddModelError(string.Empty, "An error occurred while validating the password.");
            //    return View(loginRequestDto);
            //}
            //////-------------------------------Stored Procedure Ends---------------------------------------


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

