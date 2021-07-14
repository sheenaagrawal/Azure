using AzureBlob.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlob.Controllers
{
    [ApiController]
    public class AzureBlobController : ControllerBase
    {
        private readonly IBlobClient _blobClient;

        public AzureBlobController(IBlobClient blobClient)
        {
            _blobClient = blobClient;
        }

        [HttpGet]
        [Route("/api/files/{blobName}/sas")]
        public IActionResult GetSasForBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                return BadRequest();
            }
            var sas = _blobClient.GetReadOnlySas(blobName);
            if(string.IsNullOrEmpty(sas))
            {
                return NotFound();
            }
            return Ok(sas);
        }
    }
}
