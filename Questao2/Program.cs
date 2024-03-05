using Questao2;

public class Program
{
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int? totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Galatasaray";
        year = 2015;
        totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    private static async Task<int> GetTotalScoredGoals(string team, int year)
    {
        return await Repositorio.ObterGolsAsync(new ObterGolsRequisicao { Time = team, Ano = year });
    }
}