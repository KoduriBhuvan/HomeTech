﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HomeTech.Services.ComplaintAPI.JwExtension
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            var settingsSection = builder.Configuration.GetSection("ApiSettings");
            var jwtOptionsSection = settingsSection.GetSection("JwtOptions");



            var secret = jwtOptionsSection.GetValue<string>("Secret");
            var issuer = jwtOptionsSection.GetValue<string>("Issuer");
            var audience = jwtOptionsSection.GetValue<string>("Audience");



            var key = Encoding.ASCII.GetBytes(secret);





            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });



            return builder;
        }
    }
}
