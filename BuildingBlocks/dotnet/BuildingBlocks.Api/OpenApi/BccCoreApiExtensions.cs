using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BuildingBlocks.Api.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuildingBlocks.Api.OpenApi
{
    public static class BccCoreApiExtensions
    {
        public static void AddBccAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = configuration.GetSection("Auth").Get<AuthOptions>();
            if (authOptions == null)
            {
                throw new ArgumentNullException(nameof(AuthOptions));
            }
            if (string.IsNullOrEmpty(authOptions.Audience))
            {
                throw new ArgumentNullException(nameof(authOptions.Audience));
            }   
            if (string.IsNullOrEmpty(authOptions.Authority))
            {
                throw new ArgumentNullException(nameof(authOptions.Authority));
            }
            if (authOptions.Authority.StartsWith("http"))
            {
                throw new ArgumentException("Authority shouldn't contain https://");
            }
            services.AddSingleton(s => authOptions);

            var openApiOptions = ValidateOpenApiOptions(configuration);
            
            if (openApiOptions.AuthenticationType is WebAuthenticationType.Test or WebAuthenticationType.LoadTest)
            {
                services.AddAuthentication(o =>
                    {
                        o.DefaultAuthenticateScheme = TestAuthenticationScheme.AuthenticationScheme;
                        o.DefaultChallengeScheme = TestAuthenticationScheme.AuthenticationScheme;
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                        TestAuthenticationScheme.AuthenticationScheme, o => { });
            }

            else
            {
                services.AddAuthentication(o =>
                    {
                        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.Authority = $"https://{authOptions.Authority}";
                        options.Audience = authOptions.Audience;
                        options.Events = new JwtBearerEvents
                        {
                            OnTokenValidated = async (o) =>
                            {
                                var id = o.Principal?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
                                var memoryCache = o.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();

                                var userChurchId = await memoryCache.GetOrCreateAsync($"USER_{id}", async c =>
                                {
                                    var httpClient = o.HttpContext.RequestServices
                                        .GetRequiredService<IHttpClientFactory>().CreateClient();
                                    var accessToken = o.SecurityToken as JwtSecurityToken;
                                    var message = new HttpRequestMessage
                                    {
                                        RequestUri = new Uri($"https://{authOptions.Authority.TrimEnd('/')}/userinfo"),
                                        Method = HttpMethod.Post,
                                    };
                                    if (accessToken != null)
                                    {
                                        message.Headers.Add("Authorization", "Bearer " + accessToken.RawData);
                                    }
                                    var test = (await httpClient.SendAsync(message));
                                    var result = await test.Content.ReadAsStringAsync();
                                   
                                    var x = JsonConvert.DeserializeObject<JObject>(result);

                                    c.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6);

                                    var churchId = x.GetValue(Claims.OrganizationId)?.ToString() ??
                                                   "0";
                                    if (churchId == "452" || churchId == "459")
                                    {
                                        churchId = "287";
                                    }

                                    return churchId;
                                });
                                
                                o.Principal?.AddIdentity(new ClaimsIdentity(new List<Claim>
                                {
                                    new Claim(Claims.OrganizationId, userChurchId)
                                }));
                            }
                        };
                    })                
                    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationScheme.AuthenticationScheme, o => { });
                ;
            }
        }

        public static void AddBccSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var options = ValidateOpenApiOptions(configuration);
            services.AddSingleton(s => options);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(options.Version,
                    new OpenApiInfo {Title = options.Title, Version = options.Version});
                
                c.OperationFilter<QueryableParameters>();

                switch (options.AuthenticationType)
                {
                    case WebAuthenticationType.Test:
                    {
                        c.OperationFilter<TestHeaderFilter>();
                        break;
                    }
                    
                    default:
                    {
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description =
                                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey
                        });

                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                    }
                                },
                                new string[] { }
                            }
                        });
                        break;
                    }
                }
            });
        }

        public static void UseBccSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            var options = ValidateOpenApiOptions(configuration);
            
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{options.Title} {options.Version}"); });
        }

        public static OpenApiOptions ValidateOpenApiOptions(IConfiguration configuration)
        {
            var options = configuration.GetSection("Api").Get<OpenApiOptions>();
            if (options == null)
            {
                throw new ArgumentNullException(nameof(OpenApiOptions));
            }
            if (string.IsNullOrEmpty(options.Title))
            {
                throw new ArgumentNullException(nameof(options.Title));
            }
            if (string.IsNullOrEmpty(options.Version))
            {
                throw new ArgumentNullException(nameof(options.Version));
            }
            if (string.IsNullOrEmpty(options.AuthenticationType))
            {
                throw new ArgumentNullException(nameof(options.AuthenticationType));
            }

            return options;
        }
    }
}