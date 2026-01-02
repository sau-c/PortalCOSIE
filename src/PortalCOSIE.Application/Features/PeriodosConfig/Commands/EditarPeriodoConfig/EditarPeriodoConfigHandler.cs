using PortalCOSIE.Domain.Entities.PeriodosConfig;
using PortalCOSIE.Domain.Interfaces;

namespace PortalCOSIE.Application.Features.PeriodosConfig.Commands.EditarPeriodoConfig
{
    public class EditarPeriodoConfigHandler : IRequestHandler<EditarPeriodoConfigCommand, PeriodoConfig>
    {
        private readonly IBaseRepository<PeriodoConfig, int> _periodoRepo;
        private readonly IUnitOfWork _unitOfWork;
        public EditarPeriodoConfigHandler(
            IBaseRepository<PeriodoConfig, int> periodoRepo,
            IUnitOfWork unitOfWork
            )
        {
            _periodoRepo = periodoRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<PeriodoConfig> Handle(EditarPeriodoConfigCommand command)
        {
            var config = await _periodoRepo.GetByIdAsync(1);
            if (config == null)
                throw new ApplicationException("No se encontró configuración de periodos.");
            config.SetAnioInicio(command.AnioInicio);
            config.SetPeriodoInicio(command.PeriodoInicio);
            config.SetAnioActual(command.AnioActual);
            config.SetPeriodoActual(command.PeriodoActual);

            await _unitOfWork.SaveChangesAsync();
            return config;
        }
    }
}
