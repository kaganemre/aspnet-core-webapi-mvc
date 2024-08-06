using AutoMapper;
using GuitarShopApp.Application.DTO;
using GuitarShopApp.Infrastructure.Attributes;
using GuitarShopApp.Application.Interfaces.Services;
using GuitarShopApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShopApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _productService.GetAll()));
    }

    [HttpGet("{id}")]
    [ExceptionFilter]
    [AllowAnonymous]
    public async Task<IActionResult> GetProduct(int? id)
    {
        return Ok(_mapper.Map<ProductDTO>(await _productService.GetById(id)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductDTO model)
    {
        await _productService.CreateAsync(_mapper.Map<Product>(model));
        return NoContent();
    }

    [HttpPut("{id}")]
    [ExceptionFilter]
    public async Task<IActionResult> UpdateProduct(int id, ProductDTO model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }
        await _productService.GetById(id);
        await _productService.UpdateAsync(_mapper.Map<Product>(model));
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ExceptionFilter]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        _productService.Delete(await _productService.GetById(id));
        return NoContent();
    }

}