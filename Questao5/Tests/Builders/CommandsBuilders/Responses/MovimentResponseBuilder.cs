using Questao5.Application.Commands.Responses;

namespace Questao5.Tests.Builders.CommandsBuilders.Responses
{
    public class MovimentResponseBuilder
    {
        private readonly MovimentResponse instance;

        public MovimentResponseBuilder()
        {
            instance = new MovimentResponse()
            {
                IdMoviment = Guid.NewGuid()
            };
        }

        public MovimentResponseBuilder(Guid id)
        {
            instance = new MovimentResponse()
            {
                IdMoviment = id
            };
        }

        public MovimentResponseBuilder(string erroMessage)
        {
            instance = new MovimentResponse()
            {
                ErrorMessage = erroMessage
            };
        }

        public MovimentResponse Build() => instance;
    }
}
