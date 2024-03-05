namespace Questao2
{
    public class FootballMatchesResponse
    {
        public int Total_pages { get; set; }
        public IEnumerable<ObterGolsResposta>? Data { get; set; }
    }
}
