using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RestaurantAPI.Controllers
{
    [Route("file")]
    public class FileController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult GetFile([FromQuery] string FileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/PrivateFiles/{FileName}";

            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound();
            }

            

            var contentType = new FileExtensionContentTypeProvider();
            contentType.TryGetContentType(FileName, out var mimeType);

            var fileContents = System.IO.File.ReadAllBytes(filePath);

            return File(fileContents,mimeType,FileName);
        }
    }
}
