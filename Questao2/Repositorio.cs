using Newtonsoft.Json;

namespace Questao2
{
    public class Repositorio
    {
        private const string apiUrl = "https://jsonmock.hackerrank.com/api/football_matches";
        public static async Task<int> ObterGolsAsync(ObterGolsRequisicao requisicao)
        {
            using (HttpClient client = new())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"{apiUrl}?year={requisicao.Ano}&team1={requisicao.Time}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        FootballMatchesResponse? result = JsonConvert.DeserializeObject<FootballMatchesResponse>(responseData);
                        int totalPaginas = result!.Total_pages;
                        List<ObterGolsResposta> respostas = new();
                        respostas.AddRange(result!.Data!);

                        for (int i = 2; i <= totalPaginas; i++)
                        {
                            HttpResponseMessage responseComplement = await client.GetAsync($"{apiUrl}?year={requisicao.Ano}&team1={requisicao.Time}&page={i}");
                            var responseDataComplement = await responseComplement.Content.ReadAsStringAsync();
                            FootballMatchesResponse? resultComplement = JsonConvert.DeserializeObject<FootballMatchesResponse>(responseDataComplement);
                            respostas.AddRange(resultComplement!.Data!);
                        }

                        return respostas.Sum(x => x.Team1Goals);

                    }
                    else
                    {
                        Console.WriteLine($"Falha ao obter dados. Status code: {response.StatusCode}");
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ocorreu um erro ao executar: {ex.Message}");
                    return 0;
                }
            }
        }


    }
}
