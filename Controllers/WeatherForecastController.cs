using Microsoft.AspNetCore.Mvc;
using SMTPWebAPIExample.Abstract;
using SMTPWebAPIExample.Model;
using SMTPWebAPIExample.Models;

namespace SMTPWebAPIExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEmailService _emailService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,
IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost, Route("[action]")]
        public IActionResult SendEmail(IFormFile formFile)
        {
            List<string> toList = new()
            {
                "alonesufyan03@gmail.com",
                "malikmsufyan@gmail.com",
                "nurettinkaynar568@gmail.com"
            };
            var EmailAttachment = new EmailAttachment
            {
                FileName = formFile.FileName,
                Content = formFile.FormFileToByte()
            };
            var requestEmail = new EmailRequest(Guid.NewGuid(), "malikmsufyan@gmail.com", "sufyanardgrup@gmail.com", "SMTP Example", "Test from SMTP Web API Example with attachment", EmailAttachment);


            _emailService.SendEmail(requestEmail);
            return Ok();
        }
    }
}
