using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

/// <summary>
/// Filtro de autenticação JWT global.
/// </summary>
public class JwtAuthenticationFilter : IAuthorizationFilter
{
    /// <inheritdoc />
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Filtro de autenticação JWT - atualmente comentado conforme solicitado
        // var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        // if (authHeader is null || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        // {
        //     context.Result = new UnauthorizedResult();
        //     return;
        // }
        //
        // var token = authHeader["Bearer ".Length..].Trim();
        // var tokenHandler = new JwtSecurityTokenHandler();
        // var key = Encoding.ASCII.GetBytes("SuaChaveSecretaMuitoGrandeParaFuncionarComHS256!");
        //
        // try
        // {
        //     tokenHandler.ValidateToken(token, new TokenValidationParameters
        //     {
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = new SymmetricSecurityKey(key),
        //         ValidateIssuer = false,
        //         ValidateAudience = false,
        //         ClockSkew = TimeSpan.Zero
        //     }, out var validatedToken);
        //
        //     if (validatedToken is not JwtSecurityToken jwtToken || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //     {
        //         context.Result = new UnauthorizedResult();
        //     }
        // }
        // catch
        // {
        //     context.Result = new UnauthorizedResult();
        // }
    }
}
