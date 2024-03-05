using Questao5.Application.Commands.Requests;
using Questao5.Infrastructure.Database.CommandStore.Requests;

namespace Questao5.Infrastructure.Repositories.Commands
{
    public interface IAccountCommandsRepository
    {
        Task<Guid> MovimentAccountAsync(MovimentAccountCommandRequest request);
        Task AddIdemPotentMovimentAsync(IdemPotentMovimentRequest request);
    }
}