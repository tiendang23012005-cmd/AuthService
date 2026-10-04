using System.IdentityModel.Tokens.Jwt;

namespace AuthService.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authorization =
                context.Request.Headers["Authorization"]
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(authorization) &&
                authorization.StartsWith("Bearer "))
            {
                var token = authorization.Substring(7);

                try
                {
                    var handler = new JwtSecurityTokenHandler();

                    if (handler.CanReadToken(token))
                    {
                        var jwtToken = handler.ReadJwtToken(token);

                        var userName = jwtToken.Claims
                            .FirstOrDefault(
                                x => x.Type == "name"
                            )?.Value;

                        Console.WriteLine(
                            $"Middleware - User: {userName}"
                        );
                    }
                }
                catch
                {
                    Console.WriteLine(
                        "Middleware - Token không đọc được"
                    );
                }
            }

            await _next(context);
        }
    }
}