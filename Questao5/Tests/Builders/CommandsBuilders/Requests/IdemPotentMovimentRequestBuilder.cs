using Questao5.Application.Commands.Requests;

namespace Questao5.Tests.Builders.CommandsBuilders.Requests
{
    public class IdemPotentMovimentRequestBuilder
    {
        private readonly IdemPotentMovimentRequest instance;

        public IdemPotentMovimentRequestBuilder()
        {
            instance = new IdemPotentMovimentRequest()
            {
                Chave_IdemPotencia = Guid.NewGuid(),
                Request = string.Empty,
                Result = string.Empty
            };
        }

        public IdemPotentMovimentRequestBuilder(Guid IdMovimento)
        {
            instance = new IdemPotentMovimentRequest()
            {
                Chave_IdemPotencia = Guid.NewGuid(),
                Request = string.Empty,
                Result = IdMovimento.ToString()
            };
        }

        public IdemPotentMovimentRequest Build() => instance;
    }
}
