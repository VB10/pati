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
		NotificationService notificationService = new NotificationService();
		Rasperry rasperryService = new Rasperry();

		[HttpGet]
		public async Task<ActionResult> GetAsync()
		{
			var data = await rasperryService.getAnimalaDataAsync();
			if (data != null)
			{
				data.Percent = Rasperry.getFinalWeight(data.Weight);
				if (data.Percent < 20)
				{
					string[] devices = { "fwPpnEkX-eM:APA91bGW36ASISUZ9FBaRMWKkvWZs0tNJGlDoiEvafMaDIDDO5_LmYuF1kJ00bnVlpAi4nmv86yrXS2B19uW5q8zutnjfUe1THzaXUjp58pY1T3d_2RMwFhtRGPXzHtNHUjkBCO1JPh7" };
					await notificationService.postNotifiactionAsync("Tekir", "Mamam azaldi. Biraz mama getirebilir misin?", data, devices);
				}
				return Ok(data);
			}
			return NotFound();
		}
	}


}