using System.Net.Http.Json;
using Questao5.Application.Queries.Responses;

namespace Questao5Tests
{
    public class AccountBalanceTest
    {
        private readonly string _uri;

        public AccountBalanceTest()
        {
            // HTTPS na porta 7140; HTTP na porta 5189
            _uri = @"https://localhost:7140/api/CheckingAccount/AccountBalance/";
        }

        /// <summary>
        /// Caso os dados sejam recebidos e estejam válidos, deve retornar HTTP 200
        /// Caso os dados estejam inconsistentes, deve retornar falha HTTP 400 (Bad Request)
        /// </summary>
        [Theory]
        [InlineData(123, "OK")]
        [InlineData(999, "BadRequest")]
        public async Task ValidateHttpStatusReturn(int AccountNumber, string ExpectedHttpStatusReturn)
        {
            HttpClient client = new HttpClient();
            string query = $"{_uri}{AccountNumber}";

            HttpResponseMessage response = await client.GetAsync(query);
            GetBalanceResponse? getBalanceResponse = response.Content.ReadFromJsonAsync<GetBalanceResponse>().Result;

            Assert.Equal(ExpectedHttpStatusReturn, response.StatusCode.ToString());
        }

        /// <summary>
        /// Apenas contas correntes cadastradas podem consultar o saldo; TIPO: INVALID_ACCOUNT
        /// Apenas contas correntes ativas podem consultar o saldo; TIPO: INACTIVE_ACCOUNT
        /// </summary>
        [Theory]
        [InlineData(123, "SUCCESS")]
        [InlineData(456, "SUCCESS")]
        [InlineData(789, "SUCCESS")]
        [InlineData(741, "INACTIVE_ACCOUNT")]
        [InlineData(852, "INACTIVE_ACCOUNT")]
        [InlineData(963, "INACTIVE_ACCOUNT")]
        [InlineData(999, "INVALID_ACCOUNT")]
        public async Task ValidateReturnStatus(int AccountNumber, string ExpectedStatusReturn)
        {
            HttpClient client = new HttpClient();
            string query = $"{_uri}{AccountNumber}";

            HttpResponseMessage response = await client.GetAsync(query);
            GetBalanceResponse? getBalanceResponse = response.Content.ReadFromJsonAsync<GetBalanceResponse>().Result;

            Assert.Equal(ExpectedStatusReturn, getBalanceResponse?.ResultType);
        }
    }
}