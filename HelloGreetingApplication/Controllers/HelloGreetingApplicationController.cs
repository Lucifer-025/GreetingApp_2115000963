using System.Security.Cryptography.X509Certificates;
using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Service;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Interface;


namespace HelloGreetingApplication.Controllers
{
    

    /// <summary>
    /// 
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private readonly IGreetingBL _greetingBL;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public HelloGreetingController(IGreetingBL greetingBL)
        {
            _greetingBL = greetingBL;
            _logger.Info("Logger Application has been started");
        }
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
        [HttpGet]
        [Route("GetGreeting")]
        public string GetHello()
        {
            return _greetingBL.GetGreet();

        }
        [HttpPost]
        [Route("PostGreet")]
        public IActionResult PostGreeting(UserModel userModel)
        {
            var result = _greetingBL.greeting(userModel);
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = "Greeting Message initiated With Name";
            responseModel.Data = result;
            return Ok(responseModel);
        }
    }
}
