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
        [HttpGet("GetGreetingById/{ID}")]
        public IActionResult GetGreetingById(int ID)
        {
            var response = new ResponseModel<GreetModel>();
            try
            {
                var result = _greetingBL.GreetMessagebyID(ID);
                if (result != null)
                {
                    response.Success = true;
                    response.Message = "Greeting Message is Found";
                    response.Data = result;
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "Greeting Message Not Found";
                return NotFound(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }
        /// <summary>
        /// User provides the input and it get inserted into database
        /// </summary>
        /// <param name="greetModel"></param>
        /// <returns></returns>
        [HttpPost("Greetingmessage")]
        public IActionResult GreetMessage(GreetModel greetModel)
        {
            var response = new ResponseModel<string>();
            try
            {
                bool isMessageGrret = _greetingBL.GreetMessage(greetModel);
                if (isMessageGrret)
                {
                    response.Success = true;
                    response.Message = "Greet Message!";
                    response.Data = greetModel.ToString();
                    return Ok(response);
                }
                response.Success = false;
                response.Message = "Greet Message Already Exist.";
                return Conflict(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
                return StatusCode(500, response);
            }
        }
    }
}
