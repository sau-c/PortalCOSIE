using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Features.Tramites.Queries.VerificarAcusePublico;

namespace PortalCOSIE.Web.Controllers;

[AllowAnonymous]
public class VerificarController : Controller
{
    private readonly IMediator _mediator;

    public VerificarController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet("/verificar/acuse/{token}")]
    public async Task<IActionResult> Acuse(string token)
    {
        var resultado = await _mediator.Send(new VerificarAcusePublicoQuery(token));
        if (resultado == null)
            return View("AcuseNoEncontrado");

        return View("Acuse", resultado);
    }
}