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
public class ShopController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public ShopController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpPost("get-by-userid")]
    public async Task<IActionResult> GetByUserId(int id)
    {
        return Ok(await _userService.GetById(id));
    }

    [HttpGet("get-by-email/{email?}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        return Ok(await _userService.GetUserByEmail(email));
    }

    [HttpPost("check-passwordtouser")]
    public async Task<IActionResult> CheckPassword(LoginDTO model)
    {
        return Ok(await _userService.CheckPassword(_mapper.Map<User>(model)));
    }
    
    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(UserDTO model)
    {
        try
        {
            await _userService.CreateAsync(_mapper.Map<User>(model));
            return StatusCode(201);
        }
        catch
        {
           return BadRequest();
        }
    }

    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser(User model)
    {
        await _userService.GetById(model.Id);
        await _userService.UpdateAsync(model);
        return NoContent();
    }
}