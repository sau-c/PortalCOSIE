using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalCOSIE.Application.Features.Bitacoras.Queries.Listar;

namespace PortalCOSIE.Web.Controllers
{
    [Authorize(Roles = "Administrador, Personal")]
    public class BitacoraController : Controller
    {
        private readonly IMediator _mediator;

        public BitacoraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _mediator.Send(new ListarBitacoraQuery()));
        }
    }
}
