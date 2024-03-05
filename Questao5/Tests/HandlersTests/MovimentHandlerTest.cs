using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Constants;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.CommandStore.Requests;
using Questao5.Infrastructure.Repositories.Commands;
using Questao5.Infrastructure.Repositories.Queries;
using Questao5.Tests.Builders.CommandsBuilders.Requests;
using Questao5.Tests.Builders.CommandsBuilders.Responses;
using Questao5.Tests.Builders.QueriesBuilders.Requests;
using Questao5.Tests.Builders.QueriesBuilders.Responses;
using Xunit;

namespace Questao5.Tests.HandlersTests
{
    public class MovimentHandlerTest
    {
        private IMovimentHandler _handlerMock;
        private IAccountQueriesRepository _repositoryQueriesMock = Substitute.For<IAccountQueriesRepository>();
        private IAccountCommandsRepository _repositoryCommandsMock = Substitute.For<IAccountCommandsRepository>();

        public MovimentHandlerTest()
        {
            _handlerMock = new MovimentHandler(_repositoryCommandsMock, _repositoryQueriesMock);
        }

        [Fact]
        public async Task SendCommand_MovimentAccount_MustCallHandlerCorrect()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var comando = new MovimentRequestBuilder().Build();

            // Act
            await mediator.Send(comando);

            // Assert
            await mediator.Received().Send(Arg.Is<MovimentRequest>(c => c.Number == comando.Number), default);
        }

        [Fact]
        public async Task MovimentAccount_Return_Success()
        {
            // Arrange
            var request = new MovimentRequestBuilder().Build();
            var requestIdemPotencia = new ConsultIdemPotentRequestBuilder().Build();
            var responseIdemPotencia = new ConsultIdemPotentResponseBuilder().Build();
            var responseConsultaDadosBancarios = new ConsultAccountResponseBuilder(request.Number, AccountSituation.Ativa).Build();
            var requestMovimentarContaCommand = new MovimentAccountCommandRequestBuilder().Buid();
            var responseGuid = Guid.NewGuid();
            var responseMovimentacaoConta = new MovimentResponseBuilder(responseGuid).Build();

            _repositoryQueriesMock.ConsultIdemPotentMovimentAsync(requestIdemPotencia).ReturnsNull();
            _repositoryQueriesMock.ConsultAccountAsync(request.Number).Returns(responseConsultaDadosBancarios);
            _repositoryCommandsMock.MovimentAccountAsync(Arg.Any<MovimentAccountCommandRequest>()).Returns(responseGuid);

            // Act
            var handleResult = await _handlerMock.Handle(request);

            // Assert
            Assert.Equal(expected: responseMovimentacaoConta.IdMoviment, actual: handleResult.IdMoviment);
        }

        [Fact]
        public async Task MovimentAccount_Return_IdemPotent()
        {
            // Arrange
            var request = new MovimentRequestBuilder().Build();
            var requestIdemPotencia = new ConsultIdemPotentRequestBuilder(request.IdIdemPotent).Build();
            var responseIdemPotencia = new ConsultIdemPotentResponseBuilder().Build();

            var responseMovimentacaoConta = new MovimentResponseBuilder(responseIdemPotencia.IdMovimentProcessed).Build();

            _repositoryQueriesMock.ConsultIdemPotentMovimentAsync(Arg.Any<ConsultIdemPotentRequest>()).Returns(responseIdemPotencia);

            // Act
            var handleResult = await _handlerMock.Handle(request);

            // Assert
            Assert.Equal(expected: responseMovimentacaoConta.IdMoviment, actual: handleResult.IdMoviment);
        }

        [Fact]
        public async Task MovimentAccount_Return_ErrorInvalidAccount()
        {
            // Arrange
            var request = new MovimentRequestBuilder().Build();
            var requestIdemPotencia = new ConsultIdemPotentRequestBuilder().Build();
            var responseIdemPotencia = new ConsultIdemPotentResponseBuilder().Build();
            var erroMessage = ErrorMessages.INVALID_ACCOUNT_MOVEMENT;
            var responseMovimentacaoConta = new MovimentResponseBuilder(erroMessage).Build();

            _repositoryQueriesMock.ConsultIdemPotentMovimentAsync(requestIdemPotencia).Returns(responseIdemPotencia);
            _repositoryQueriesMock.ConsultAccountAsync(request.Number).ReturnsNull();

            // Act
            var handleResult = await _handlerMock.Handle(request);

            // Assert
            Assert.Equal(expected: responseMovimentacaoConta.ErrorMessage, actual: handleResult.ErrorMessage);
        }

        [Fact]
        public async Task MovimentAccount_Return_ErrorInactiveAccount()
        {
            // Arrange
            var request = new MovimentRequestBuilder().Build();
            var requestIdemPotencia = new ConsultIdemPotentRequestBuilder().Build();
            var responseIdemPotencia = new ConsultIdemPotentResponseBuilder().Build();
            var responseConsultaDadosBancarios = new ConsultAccountResponseBuilder(request.Number, AccountSituation.Inativa).Build();
            var erroMessage = ErrorMessages.INACTIVE_ACCOUNT_MOVIMENT;
            var responseMovimentacaoConta = new MovimentResponseBuilder(erroMessage).Build();

            _repositoryQueriesMock.ConsultIdemPotentMovimentAsync(requestIdemPotencia).Returns(responseIdemPotencia);
            _repositoryQueriesMock.ConsultAccountAsync(request.Number).Returns(responseConsultaDadosBancarios);

            // Act
            var handleResult = await _handlerMock.Handle(request);

            // Assert
            Assert.Equal(expected: responseMovimentacaoConta.ErrorMessage, actual: handleResult.ErrorMessage);
        }
    }
}
