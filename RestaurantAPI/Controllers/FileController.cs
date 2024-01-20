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

        [HttpPost]
        [Authorize]
        public ActionResult Upload([FromForm] IFormFile file)
        {
            if(file != null && file.Length > 0) 
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var filePath = $"{rootPath}/PrivateFiles/{fileName}";
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                };

                return Ok();
            }
            else 
            { 
                return BadRequest(); 
            }
        }

    }
}
