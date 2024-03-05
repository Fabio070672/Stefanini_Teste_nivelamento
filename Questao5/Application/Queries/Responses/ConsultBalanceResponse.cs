namespace Questao5.Application.Queries.Responses
{
    public class ConsultBalanceResponse
    {
        public string? Number { get; set; }
        public string? Name { get; set; }
        public DateTime? DateTimeConsult { get; set; }
        public decimal? ValueBalance { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
