using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationController : ControllerBase
    {
        [HttpPost("change")]
        public IActionResult ChangeLocalization([FromHeader(Name = "Accept-Language")] string language)
        {
            var supportedCultures = new List<string> { "en-US", "uk-UA" };

            if (!supportedCultures.Contains(language))
            {
                return BadRequest($"Unsupported language: {language}. Supported languages: {string.Join(", ", supportedCultures)}");
            }

            var localizationFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            var currentCulture = localizationFeature.RequestCulture.Culture;
            
            var newCulture = new RequestCulture(language);
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(newCulture),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Ok(new { CurrentCulture = currentCulture.Name, NewCulture = newCulture.Culture.Name });
        }
    }
}