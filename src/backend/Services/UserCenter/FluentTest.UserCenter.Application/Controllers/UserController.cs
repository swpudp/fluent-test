using FluentTest.UserCenter.Model.Entity;
using FluentTest.UserCenter.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace FluentTest.UserCenter.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("total")]
        public int Total()
        {
            return Random.Shared.Next(0, 100);
        }

        [HttpGet]
        public User GetById(string id)
        {
            return _userService.GetById(id);
        }
    }
}
