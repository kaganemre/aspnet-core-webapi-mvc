using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShopApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StreamController : ControllerBase
{
    [HttpGet("{imageName}")]
    public IActionResult DownloadImage(string imageName)
    {
        return File($"/img/{imageName}", "image/png");
    }

    [HttpPost("upload-image")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        var randomFileName="";

        if (file.Length > 0)
        {
            randomFileName = string.Format($"{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
            using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);
        }

        return Ok(new { randomFileName });
    }
}