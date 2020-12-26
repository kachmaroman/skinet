using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace API.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly ITokenService _tokenService;
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;

		public AccountController(IMapper mapper,
								 ITokenService tokenService,
								 UserManager<AppUser> userManager,
								 SignInManager<AppUser> signInManager) : base(mapper)
		{
			_tokenService = tokenService;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
		{
			AppUser user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

			UserDto userDto = Map<UserDto>(user);
			userDto.Token = _tokenService.CreateToken(user);

			return userDto;
		}

		[HttpGet]
		[Route("emailexists")]
		public async Task<ActionResult<bool>> CheckEmailExistAsync([FromQuery] string email)
		{
			return await _userManager.FindByNameAsync(email) != null;
		}

		[HttpGet]
		[Authorize]
		[Route("address")]
		public async Task<ActionResult<AddressDto>> GetUserAddressAsync()
		{
			AppUser user = await _userManager.FinByClaimsPrincipleWithAddressAsync(HttpContext.User);

			return Map<AddressDto>(user.Address);
		}

		[HttpPut]
		[Authorize]
		[Route("address")]
		public async Task<ActionResult<AddressDto>> UpdateAddressAsync([FromBody] AddressDto addressDto)
		{
			AppUser user = await _userManager.FinByClaimsPrincipleWithAddressAsync(HttpContext.User);

			user.Address = Map<Address>(addressDto);

			IdentityResult result = await _userManager.UpdateAsync(user);

			if (!result.Succeeded)
			{
				return BadRequest("Problem updateing the user");
			}

			return Map<AddressDto>(user.Address);
		}

		[HttpPost]
		[Route("login")]
		public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginDto loginDto)
		{
			AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);

			if (user == null)
			{
				return Unauthorized(new ApiResponse(401));
			}

			SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

			if (!result.Succeeded)
			{
				return Unauthorized(new ApiResponse(401));
			}

			UserDto userDto = Map<UserDto>(user);
			userDto.Token = _tokenService.CreateToken(user);

			return userDto;
		}

		[HttpPost]
		[Route("register")]
		public async Task<ActionResult<UserDto>> RegisterAsync([FromBody] RegisterDto registerDto)
		{
			AppUser user = Map<AppUser>(registerDto);

			IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

			if (!result.Succeeded)
			{
				return BadRequest(new ApiResponse(400));
			}

			UserDto userDto = Map<UserDto>(user);
			userDto.Token = _tokenService.CreateToken(user);

			return userDto;
		}
	}
}
