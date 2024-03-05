using MediatR;
using Questao5.Domain.Constants;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Application.Queries.Requests
{
    public class ConsultRequest : IRequest<int>
    {
        [Required(ErrorMessage = ErrorMessages.VALUE_REQUIDED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.INVALID_ACCOUNT_MOVEMENT)]
        public int Number { get; set; }
    }
}
