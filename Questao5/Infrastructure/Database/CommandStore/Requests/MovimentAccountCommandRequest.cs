using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class MovimentAccountCommandRequest
    {
        public Guid IdAccount { get; set; }
        public MovimentType MovimetType { get; set; }
        public decimal Value { get; set; }
    }
}
