using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Geografia.API.Filters;

/// <summary>
/// Filtro de autenticação JWT.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class JwtAuthenticationAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// Executa a autorização da requisição.
    /// </summary>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // TODO: Implementar validação JWT
        // var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        // if (string.IsNullOrEmpty(token) || !IsValidToken(token))
        // {
        //     context.Result = new UnauthorizedResult();
        // }
    }

    // private bool IsValidToken(string token)
    // {
    //     // Implementar validação do token JWT
    //     return true;
    // }
}
