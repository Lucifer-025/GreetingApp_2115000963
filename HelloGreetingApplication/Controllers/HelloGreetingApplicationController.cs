using System.Security.Cryptography.X509Certificates;
using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Hello to the Greeting App API Endpoint hit";
            responseModel.Data = "Hello World";
            return Ok(responseModel);
        }

        [HttpPost]
        public IActionResult Post(RequestModel requestModel)
        {
            {
                ResponseModel<string> responseModel = new ResponseModel<string>();
                {
                    responseModel.Success = true;
                    responseModel.Message = "POST request received";
                    responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
                };
                return Ok(responseModel);
            }
        }
        [HttpPut]
        public IActionResult Put(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "Put request received";
            responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";

            return Ok(responseModel);

        }

        [HttpPatch]
        public IActionResult Patch(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "Post request received";
            responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";

            return Ok(responseModel);
        }

        [HttpDelete("{Key}")]
        public IActionResult Delete(RequestModel requestModel)
        {
            ResponseModel<string> responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "Post request received";
            responseModel.Data = $"Key: {requestModel.Key},Deleted Successfully.";

            return Ok(responseModel);
        }
    }
}
