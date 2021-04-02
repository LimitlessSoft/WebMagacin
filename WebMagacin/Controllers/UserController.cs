using Microsoft.AspNetCore.Mvc;
using LimitlessSoft.AspNet.Authorization;
using WebMagacin.Models;
using System.Threading.Tasks;
using System;

namespace WebMagacin.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        [Route("/User/Create")]
        public async Task<IActionResult> Create(string username, string displayName, string password)
        {
            return await Task.Run<IActionResult>(() => {
                if (UserModel.Get(username) != null)
                    return StatusCode(400, "User with given username already exists!");

                if (UserModel.Insert(username, displayName, password) != null)
                    return StatusCode(201);
                else
                    return StatusCode(500);
            });
        }
        [HttpPost]
        [Route("/User/Login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    UserModel user = UserModel.Get(username);

                    if (user == null)
                        return StatusCode(400, "User with given name not found!");

                    if (LimitlessSoft.Hash.Generate(password) == user.Password)
                    {
                        Client.LogIn(Request.HttpContext, username, user.Role);
                        return StatusCode(200);
                    }
                    else
                    {
                        return StatusCode(400, "Incorrect password!");
                    }
                }
                catch (Exception)
                {
                    return StatusCode(500);
                }
            });
        }
        [HttpPost]
        [Route("/User/Logout")]
        public async Task<IActionResult> Logout()
        {
            return await Task.Run<IActionResult>(() =>
            {
                Client.LogOut(Request.HttpContext);
                return Redirect("/");
            });
        }
    }
}
