using Microsoft.AspNetCore.Mvc;

namespace EonetViewer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProtosController(ILogger<ProtosController> Logger) : ControllerBase
{
    [HttpGet("{protoName}")]
    public IActionResult Get(string protoName)
    {
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Protos");
        var safeFileName = Path.GetFileName(protoName);
        var filePath = Path.Combine(folderPath, safeFileName);

        if (!System.IO.File.Exists(filePath))
        {
            var protos = Directory.GetFiles(folderPath).Select(file => Path.GetFileName(file));

            return NotFound(new
            {
                ErrorMessage = $"Proto is not found at '{protoName}'. Get one of the available protos.",
                AvailableProtos = protos,
            });
        }

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return File(stream, "text/plain", safeFileName);
    }
}
