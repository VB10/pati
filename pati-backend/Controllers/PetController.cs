using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using dotnetcore.Model;
using Microsoft.AspNetCore.Mvc;
using pati_backend.Controllers;

namespace dotnetcore.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PetController : ControllerBase
	{
		Notification notificationService = new Notification();
		Rasperry rasperryService = new Rasperry();

		[HttpGet]
		public async Task<ActionResult> GetAsync()
		{
			var data = await rasperryService.getAnimalaDataAsync();
			if (data != null)
			{
				return Ok(data);
			}
			return NotFound();
		}
	}


}