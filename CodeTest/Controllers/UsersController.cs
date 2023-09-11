using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using CodeTest.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CodeTest.Services;
using CodeTest.Entities;
using CodeTest.Models.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;

namespace CodeTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private IUserService _userService;
        private IMapper _mapper;
        private IConfiguration _config;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IConfiguration config)
        {
            _userService = userService;
            _mapper = mapper;
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            IActionResult response = Unauthorized();
            bool isUserValid = false;
            if (model.Username == "admin" && model.Password == "12345")
            {
                isUserValid = true;
            }
            else
            {
                var user = _userService.Authenticate(model.Username, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });
                else
                    isUserValid = true;
            }
            if (isUserValid)
            {
                var tokenString = GenerateJSONWebToken(model);
                response = Ok(new { Token = tokenString, Message = "Success" });
            }
            return response;
        }

        #region GenerateJWT
        /// <summary>
        /// Generate Json Web Token Method
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken(AuthenticateModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
        [Authorize]
        [HttpPost("")]
        public IActionResult Create([FromBody] RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            var model = _mapper.Map<UserModel>(user);
            return Ok(model);
        }

    }
}
