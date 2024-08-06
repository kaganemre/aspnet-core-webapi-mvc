using GuitarShopApp.Application.Interfaces.Services;
using GuitarShopApp.Domain.Entities;
using GuitarShopApp.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace GuitarShopApp.WebUI.Controllers;

[Authorize(Roles = "admin")]
public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    public HomeController(IProductService productService, ICategoryService categoryService, IMapper mapper)
    {
        _productService = productService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        return View(await _productService.GetHomePageProducts());
    }

    [AllowAnonymous]
    public async Task<IActionResult> List(string category)
    {
        ViewBag.Categories = await _categoryService.GetAll();
        ViewBag.SelectedCategory = RouteData?.Values["category"];

        return View(await _productService.GetProductsByCategory(category));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        
        await CategoryList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel model, IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {
            
            try
            {
                var entity = _mapper.Map<Product>(model);
                entity.Image = await AddImage(imageFile);
                await _productService.CreateAsync(entity);
                return RedirectToAction("List");
            }
            catch (Exception)
            {

                throw;
            }
        }

        await CategoryList();
        return View(model);
    }

    public async Task<IActionResult> Edit(int? id)
    {

        var entity = await _productService.GetById(id);
        var model = _mapper.Map<ProductViewModel>(entity);
        await CategoryList();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductViewModel model, IFormFile imageFile)
    {
        if (ModelState.IsValid)
        {
            var entity = await _productService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            try
            {
                entity = _mapper.Map<Product>(model);
                entity.Image = await AddImage(imageFile);
                await _productService.UpdateAsync(entity);
                return RedirectToAction("List");
            }
            catch (Exception)
            {
                throw;
            }

        }
        
        await CategoryList();
        return View(model);
    }

    [HttpPost]
    public async Task<JsonResult> Delete([FromBody] ProductViewModel model)
    {
        var entity = await _productService.GetById(model.Id);

        if (entity != null)
        {
            _productService.Delete(entity);
        }

        return Json(new { model.Id });

    }
    private async Task<string> AddImage(IFormFile imageFile)
    {
        var extension = Path.GetExtension(imageFile.FileName);
        var randomFileName= string.Format($"{Guid.NewGuid()}{extension}");
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
        using var stream = new FileStream(path, FileMode.Create);
        await imageFile.CopyToAsync(stream);

        return randomFileName;
    }

    private async Task CategoryList()
    {
        ViewBag.Categories = new SelectList(await _categoryService.GetAll(), "Id", "Name");
    }
    


}