using MediatR;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Questao5.Application.Handlers;
using Questao5.Application.Queries.Requests;
using Questao5.Domain.Constants;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Database.QueryStore.Requests;
using Questao5.Infrastructure.Repositories.Queries;
using Questao5.Tests.Builders.QueriesBuilders.Requests;
using Questao5.Tests.Builders.QueriesBuilders.Responses;
using Xunit;

namespace Questao5.Tests.HandlersTests
{
    public class ConsultHandlerTest
    {
        private IConsultHandler _handlerMock;
        private IAccountQueriesRepository _repositoryMock = Substitute.For<IAccountQueriesRepository>();

        public ConsultHandlerTest()
        {
            _handlerMock = new ConsultHandler(_repositoryMock);
        }

        [Fact]
        public async Task SendCommand_Consult_MustCallHandlerCorrect()
        {
            // Arrange
            var mediator = Substitute.For<IMediator>();
            var comando = new ConsultRequestBuilder().Build();

            // Act
            await mediator.Send(comando);

            // Assert
            await mediator.Received().Send(Arg.Is<ConsultRequest>(c => c.Number == comando.Number), default);
        }

        [Fact]
        public async Task Consult_ReturnConsultBalanceResponse_Success()
        {
            // Arrange
            var request = new ConsultRequestBuilder().Build();
            var responseConsultaDadosBancarios = new ConsultAccountResponseBuilder(request.Number, AccountSituation.Ativa).Build();
            var requestConsultaSaldoQuery = new ConsultBalanceQueryRequestBuilder(responseConsultaDadosBancarios.IdAccount).Build();
            var responseConsultaSaldoQuery = new ConsultBalanceQueryResponseBuilder().Build();
            var response = new ConsultBalanceResponseBuilder(
                responseConsultaDadosBancarios.Number,
                responseConsultaDadosBancarios.Name,
                responseConsultaSaldoQuery.ValueBalance).Build();

            _repositoryMock.ConsultAccountAsync(request.Number).Returns(responseConsultaDadosBancarios);
            _repositoryMock.ConsultBalanceAsync(Arg.Any<ConsultBalanceQueryRequest>()).Returns(responseConsultaSaldoQuery);

            // Act
            var handleResult = await _handlerMock.Handle(request);

            // Assert
            Assert.Equal(expected: response.Number, actual: handleResult.Number);
            Assert.Equal(expected: response.Name, actual: handleResult.Name);
            Assert.Equal(expected: response.ValueBalance, actual: handleResult.ValueBalance);
        }

        [Fact]
        public async Task Consult_Return_InactveAccount()
        {
            // Arrange
            var request = new ConsultRequestBuilder().Build();
            var responseConsultaDadosBancarios = new ConsultAccountResponseBuilder(request.Number, AccountSituation.Inativa).Build();
            var erroMessage = ErrorMessages.INACTIVE_ACCOUNT_CONSULT;
            var response = new ConsultBalanceResponseBuilder(erroMessage).Build();

            _repositoryMock.ConsultAccountAsync(request.Number).Returns(responseConsultaDadosBancarios);

            // Act
            var handleResult = await _handlerMock.Handle(request);

            // Assert
            Assert.Equal(expected: response.ErrorMessage, actual: handleResult.ErrorMessage);
        }

        [Fact]
        public async Task Consult_Return_InvalidAccount()
        {
            // Arrange
            var request = new ConsultRequestBuilder().Build();
            var erroMessage = ErrorMessages.INVALID_ACCOUNT_CONSULT;
            var response = new ConsultBalanceResponseBuilder(erroMessage).Build();

            _repositoryMock.ConsultAccountAsync(request.Number).ReturnsNull();

            // Act
            var handleResult = await _handlerMock.Handle(request);

            // Assert
            Assert.Equal(expected: response.ErrorMessage, actual: handleResult.ErrorMessage);
        }

        [Fact]
        public async Task Consult_Return_ErrorConsult()
        {
            // Arrange
            var request = new ConsultRequestBuilder().Build();
            var responseConsultaDadosBancarios = new ConsultAccountResponseBuilder(request.Number, AccountSituation.Ativa).Build();
            var erroMessage = ErrorMessages.ERROR_CONSULT;
            var response = new ConsultBalanceResponseBuilder(erroMessage).Build();

            _repositoryMock.ConsultAccountAsync(request.Number).Returns(responseConsultaDadosBancarios);
            _repositoryMock.ConsultBalanceAsync(Arg.Any<ConsultBalanceQueryRequest>()).ReturnsNull();

            // Act
            var handleResult = await _handlerMock.Handle(request);

            // Assert
            Assert.Equal(expected: response.ErrorMessage, actual: handleResult.ErrorMessage);
        }
    }
}
