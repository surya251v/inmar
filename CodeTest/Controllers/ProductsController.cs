using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CodeTest.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using CodeTest.Services;
using CodeTest.Entities;
using CodeTest.Models.Users;
using CodeTest.Models.Product;

namespace CodeTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]
    public class ProductsController : BaseController
    {
        private IProductService _productService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ProductsController(
            IProductService productService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _productService = productService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [Authorize]
        [HttpPost("")]
        public IActionResult Create([FromBody]ProductModel model)
        {
            // map model to entity
            var product = _mapper.Map<Product>(model);

            try
            {
                // create user
                _productService.Create(product);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
