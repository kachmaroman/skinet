using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;

		private readonly SymmetricSecurityKey _key;

		public TokenService(IConfiguration config)
		{
			_config = config;
			_key = new(Encoding.UTF8.GetBytes(_config["Token:Key"]));
		}

		public string CreateToken(AppUser user)
		{
			List<Claim> claims = new List<Claim>
			{
				new(JwtRegisteredClaimNames.Email, user.Email),
				new(JwtRegisteredClaimNames.GivenName, user.DisplayName)
			};

			SigningCredentials creds = new(_key, SecurityAlgorithms.HmacSha512Signature);

			SecurityTokenDescriptor tokenDescriptor = new()
			{
				SigningCredentials = creds,
				Issuer = _config["Token:Issuer"],
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(int.Parse(_config["Token:ExpiresInDays"] ?? "7")),
			};

			JwtSecurityTokenHandler tokenHandler = new();

			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}