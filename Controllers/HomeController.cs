using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegistration.Models;
using Microsoft.AspNetCore.Identity;
using LoginAndRegistration.DbConnection;
using Microsoft.AspNetCore.Http;

namespace LoginAndRegistration.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                string hashed = (new PasswordHasher<User>()).HashPassword(user, user.password);
                string query = $@"INSERT INTO USERS 
                                    (first_name
                                    ,last_name
                                    ,email
                                    ,password
                                    ,created_at)
                                VALUES
                                    ('{user.first_name}'
                                    ,'{user.last_name}'
                                    ,'{user.email}'
                                    ,'{user.password}'
                                    ,Now())";
                DbConnector.Execute(query);
                string query2 =$@"select * from users where email = '{user.email}'";
                Console.WriteLine("user.email");
                Console.WriteLine(" test {user.email}");
                Console.WriteLine(query2);
                List<Dictionary<string,object>> users = DbConnector.Query(query2);
                int id;
                if (users.Count==0)
                    id = Convert.ToInt32(users[0]["user_id"]);
                else
                    id = Convert.ToInt32(users[0]["user_id"]);
                HttpContext.Session.SetInt32("id", id);
                return RedirectToAction("Success");
            }
            return View("Index");
        }
        [HttpPost("login")]
        public IActionResult Login(Login user)
        {
            List<Dictionary<string,object>> users = DbConnector.Query($"SELECT user_id, password FROM users WHERE email = '{user.email}'");
            PasswordHasher<Login> hasher = new PasswordHasher<Login>();
            Console.WriteLine(users.Count);
            Console.WriteLine(user.password);
            Console.WriteLine((string)users[0]["password"]);
            if((users.Count == 0 || user.password == null))
            //|| hasher.VerifyHashedPassword(user, (string)users[0]["password"], user.password) == 0)
            {
                ModelState.AddModelError("email", "Invalid Email/Password");
            }
            if(ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("id", (int)users[0]["user_id"]);
                return RedirectToAction("Success");
            }
            return View("Index");
        }
        [HttpGet("success")]
        public string  Success()
        {
            return "Success!";
        }

        
    }
}