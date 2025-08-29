using FluentValidation;
using Microsoft.Extensions.Logging;
using Motorcycles.Domain.Exceptions;
using System.Net;

namespace MotorcycleRent.API.Middleware;

internal sealed class GlobalExceptionHandler(
    RequestDelegate next,
    ILogger<GlobalExceptionHandler> logger
    )
{

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException)
        {
            logger.LogWarning("Error na Validação");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new { mensagem = "Dados inválidos" });
        }
        catch (NotFoundException ex)
        {
            logger.LogWarning("Recurso não encontrado");
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsJsonAsync(new { mensagem = ex.Message });
        }
        catch (BadHttpRequestException)
        {
            logger.LogWarning("Bad request");
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new { mensagem = "Request mal informada" });
        }
        catch(Exception ex)
        {
            logger.LogError("Error não tratado");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new { mensagem = ex.Message });
        }
    }

}
