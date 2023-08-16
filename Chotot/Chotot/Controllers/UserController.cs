using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Chotot.ChototMOD.UserMOD;
using System.Runtime.Serialization;

namespace Chotot.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		public enum Role
		{
			[EnumMember(Value = "Admin")]
			Admin,
			[EnumMember(Value = "QLSanpham")]
			QLSP,
			[EnumMember(Value = "QLDonhang")]
			QLDH,
			[EnumMember(Value = "User")]
			User,
		}

		[HttpPost]
		[Route("Login")]
		public IActionResult Login([FromForm] AccLoginMOD login)
		{
			if (login == null)
			{
				return BadRequest();
			}
			var Result = new AccBUS().LoginBUS(login);
			if (Result != null)
			{
				return Ok(Result);
			}
			else
			{
				return BadRequest();
			}
		}

		// API Res
		/// <summary>
		/// Register a new user.
		/// </summary>
		/// <param name="item">User information.</param>
		/// <param name="a">A value for demonstration purposes. Default value is "Tomcat".</param>
		/// <returns>Result of the registration.</returns>
		[HttpPost]
		[Route("Register")]
		public IActionResult Register(AccRegisMOD item)
		{
			/*
			int IdRole = 0;

			switch (role)
			{
				case Role.Admin:
					IdRole = 1; break;
				case Role.QLSP:
					IdRole = 2; break;
				case Role.QLDH:
					IdRole = 3; break;
				case Role.User:
					IdRole = 4; break;
			}
			*/

			if (item == null)
			{
				return BadRequest("Dữ liệu không được được để trống");
			}
			var Result = new AccBUS().RegisterBUS(item);
			if (Result != null)
			{
				return Ok(Result);
			}
			else
			{
				return NotFound("");
			}
		}
	}
}
