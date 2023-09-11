using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CodeTest.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using CodeTest.Services;
using CodeTest.Entities;
using CodeTest.Models.Users;
using CodeTest.Models.Product;
using System.Collections.Generic;

namespace CodeTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/cartitems")]
    public class CartItemsController : BaseController
    {
        private ICartService _cartService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CartItemsController(
            ICartService cartService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _cartService = cartService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [Authorize]
        [HttpPost("")]
        public IActionResult Create([FromBody]CartItemModel model)
        {
            // map model to entity
            var cartItem = _mapper.Map<CartItem>(model);

            try
            {
                // create user
                _cartService.Create(cartItem);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("")]
        public IActionResult Delete([FromBody] CartItemModel model)
        {
            // map model to entity
            var cartItem = _mapper.Map<CartItem>(model);

            try
            {
                // create user
                _cartService.Update(cartItem);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [Authorize]
        [HttpGet("user/{userId}")]
        public IActionResult GetUserCart(int userId)
        {
            var cartItems = _cartService.GetUserCart(userId);
            var cartItemModels = _mapper.Map<List<CartItem>>(cartItems);
            return Ok(cartItemModels);
        }
    }
}
