using System.Text;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Mvc;

namespace StorageDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class StorageController : ControllerBase
{

    private readonly ILogger<StorageController> _logger;

    public StorageController(ILogger<StorageController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetFile(string fileName)
    {
        var client = StorageClient.Create();
        var stream = new MemoryStream();
        var obj = await client.DownloadObjectAsync("scriptbytes-storagedemo", fileName, stream);
        var val = Encoding.UTF8.GetBytes("Hello World");
        Console.WriteLine(val);
        stream.Position = 0;

        return File(stream, obj.ContentType, obj.Name);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddFile([FromBody] FileUpload fileUpload)
    {
        var client = StorageClient.Create();
        var obj = await client.UploadObjectAsync(
            "scriptbytes-storagedemo",
            fileUpload.Name,
            fileUpload.Type,
            new MemoryStream(fileUpload.File));

        return Ok();
    }

    public class FileUpload
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] File { get; set; }
    }
}