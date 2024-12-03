using AutoMapper;
using GuitarShopApp.Application.DTO;
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
    [AllowAnonymous]
    public async Task<IActionResult> GetProduct(int? id)
    {
        return Ok(_mapper.Map<ProductDTO>(await _productService.GetById(id)));
    }
    
    [HttpGet("get-by-category/{name?}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetProductsByCategory(string name="")
    {
        return Ok(_mapper.Map<IEnumerable<ProductDTO>>(await _productService.GetProductsByCategory(name)));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductDTO model)
    {
        var newProduct = _mapper.Map<ProductDTO>(await _productService.CreateAsync(_mapper.Map<Product>(model)));
        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductDTO model)
    {
        await _productService.GetById(model.Id);
        await _productService.UpdateAsync(_mapper.Map<Product>(model));
        return Ok(model);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int? id)
    {
        _productService.Delete(await _productService.GetById(id));
        return NoContent();
    }

}