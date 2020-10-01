using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediOps.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet]
        [Route("error")]
        public IActionResult Error()
        {
            // Logging Can be done here but returning direct error due to time deficiency
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return ValidationProblem(new ValidationProblemDetails()
            {
                Detail = context.Error.Message
                ,
                Title = "UserGenError"
                ,
            });
        }
    }
}