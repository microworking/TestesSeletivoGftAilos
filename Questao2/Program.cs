using System.Net.Http.Json;
using Questao2.Domain;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        static int getTotalScoredGoals(string team, int year)
        {
            HttpClient client = new HttpClient();
            int goalsInYear = 0;
            string query = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}&team1={team}";

            HttpResponseMessage response = client.GetAsync(query).Result;
            if (response != null && response.IsSuccessStatusCode)
            {
                MatchResultsDTO? natchResults = response.Content.ReadFromJsonAsync<MatchResultsDTO>().Result;
                int totalPages = natchResults == null ? 0 : natchResults.TotalPages;
                for (int page = 1; page <= totalPages; page++)
                {
                    response = client.GetAsync(query + $"&page={page}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MatchResultsDTO? matchPartialResult = response.Content.ReadFromJsonAsync<MatchResultsDTO>().Result;
                        goalsInYear += matchPartialResult == null ? 0 : matchPartialResult.Data == null ? 0 : matchPartialResult.Data.Sum(x => x.Team1goals);
                    }
                }
            }

            return goalsInYear;
        }

    // Output expected:
    // Team Paris Saint - Germain scored 109 goals in 2013
    // Team Chelsea scored 92 goals in 2014
    }
}