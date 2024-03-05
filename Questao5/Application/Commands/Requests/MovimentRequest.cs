using Questao5.Domain.Constants;
using Questao5.Domain.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentRequest
    {
        [Required(ErrorMessage = ErrorMessages.VALUE_REQUIDED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessages.INVALID_ACCOUNT_MOVEMENT)]
        public int Number { get; set; }

        [Required(ErrorMessage = ErrorMessages.INVALID_TYPE)]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MovimentType MovimentType { get; set; }

        [Required(ErrorMessage = ErrorMessages.VALUE_REQUIDED)]
        [Range(0, int.MaxValue, ErrorMessage = ErrorMessages.INVALID_VALUE)]
        public decimal Value { get; set; }

        [Required(ErrorMessage = ErrorMessages.VALUE_REQUIDED)]
        public Guid IdIdemPotent { get; set; }
    }
}
