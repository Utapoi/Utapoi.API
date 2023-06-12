﻿using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Karaoke.Application.Auth.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Karaoke.Infrastructure.Options.JWT;

public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtSettings;

    public ConfigureJwtBearerOptions(IOptions<JwtOptions> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        Configure(string.Empty, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme)
        {
            return;
        }

        var key = SHA512.HashData(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidIssuer = _jwtSettings.ValidIssuer,
            ValidAudience = _jwtSettings.ValidAudience,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (!context.Request.Cookies.TryGetValue("AuthToken", out var authToken))
                {
                    return Task.CompletedTask;
                }

                if (string.IsNullOrEmpty(authToken))
                {
                    return Task.CompletedTask;
                }

                var info = JsonSerializer.Deserialize<TokenResponse>(authToken);

                if (info == null)
                {
                    return Task.CompletedTask;
                }

                context.Token = info.Token;

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }

                return Task.CompletedTask;
            }
        };
    }
}