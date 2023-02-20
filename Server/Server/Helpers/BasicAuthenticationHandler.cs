using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Server.Enums;
using Server.Models;
using Server.Services;
using System;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Server.Helpers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private string failReason;
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // skip authentication if endpoint has [AllowAnonymous] attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey("Authorization"))
                throw new AppException("Вы не вошли в систему!");

            User user = null;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];
                user = await _userService.Authenticate(username, password);
            }
            catch
            {
                throw new AppException("Неправильный Authorization Header");
            }

            if (user == null)
            {
                throw new AppException("Неправильный логин или пароль!");
            }

            var controllerName = Context.Request.RouteValues["controller"]?.ToString();

            if (user.Role == RoleType.Admin && controllerName == "Users")
            {
                var ticket = GetTicket(user);

                return AuthenticateResult.Success(ticket);
            }

            if (user.Role == RoleType.Manager && controllerName == "Orders")
            {
                var ticket = GetTicket(user);

                return AuthenticateResult.Success(ticket);
            }

            if (user.Role == RoleType.Accountant && controllerName == "Finance")
            {
                var ticket = GetTicket(user);

                return AuthenticateResult.Success(ticket);
            }

            failReason = "У вас нет доступа!"!;
            throw new AppException(failReason);
        }

        private AuthenticationTicket GetTicket(User user)
        {
            var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationTicket(principal, Scheme.Name);
        }
    }
}
