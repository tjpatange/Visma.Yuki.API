using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace API.Extensions
{
    public class ResponseFormatFilter : Attribute, IActionFilter
    {
        private readonly string[] supportedFormats = { "json", "xml" };

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var format = context.HttpContext.Request.Headers["Accept"].ToString().ToLower();

            if (string.IsNullOrWhiteSpace(format) || !Array.Exists(supportedFormats, f => f.Equals(format)))
            {
                format = "json"; // Default to JSON if no valid format is specified
            }

            context.HttpContext.Items["ResponseFormat"] = format;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var format = context.HttpContext.Items["ResponseFormat"].ToString();

            if (format == "xml")
            {
                context.Result = new ObjectResult(context.Result)
                {
                    ContentTypes = new MediaTypeCollection { new MediaTypeHeaderValue("application/xml") }
                };
            }
            // JSON is the default, no action needed for it
        }
    }
}

