using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5Tests
{
    public class AccountMovementTest
    {
        private readonly string _uri;

        public AccountMovementTest()
        {
            // HTTPS na porta 7140; HTTP na porta 5189
            _uri = @"https://localhost:7140/api/CheckingAccount/AccountMovement";
        }

        /// <summary>
        /// Caso os dados sejam recebidos e estejam válidos, deve retornar HTTP 200
        /// Caso os dados estejam inconsistentes, deve retornar falha HTTP 400 (Bad Request)
        /// </summary>
        [Theory]
        [InlineData(123, "C", 10.00, "OK")]
        [InlineData(123, "D", 10.00, "OK")]
        [InlineData(123, "X", 10.00, "BadRequest")]
        [InlineData(123, "C",  0.00, "BadRequest")]
        [InlineData(741, "C", 10.00, "BadRequest")]
        [InlineData(999, "C", 10.00, "BadRequest")]
        public async Task ValidateHttpStatusReturn(int AccountNumber, string MovementType, double MovementValue, string ExpectedHttpStatusReturn)
        {
            HttpClient client = new HttpClient();
            AccountMovementRequest accountMovementRequest = new(){ AccountNumber = AccountNumber, MovementType = MovementType, MovementValue = MovementValue };
            
            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(accountMovementRequest), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_uri, jsonContent);
            Assert.Equal(ExpectedHttpStatusReturn, response.StatusCode.ToString());
        }

        /// <summary>
        /// Apenas contas correntes cadastradas podem receber movimentação; TIPO: INVALID_ACCOUNT
        /// Apenas contas correntes ativas podem receber movimentação; TIPO: INACTIVE_ACCOUNT
        /// Apenas valores positivos podem ser recebidos; TIPO: INVALID_VALUE
        /// Apenas os tipos “débito” ou “crédito” podem ser aceitos; TIPO: INVALID_TYPE
        /// </summary>
        [Theory]
        [InlineData(123, "C", 10.00, "SUCCESS")]
        [InlineData(456, "C", 10.00, "SUCCESS")]
        [InlineData(789, "C", 10.00, "SUCCESS")]
        [InlineData(123, "D", 10.00, "SUCCESS")]
        [InlineData(456, "D", 10.00, "SUCCESS")]
        [InlineData(789, "D", 10.00, "SUCCESS")]
        [InlineData(999, "C", 10.00, "INVALID_ACCOUNT")]
        [InlineData(741, "D", 10.00, "INACTIVE_ACCOUNT")]
        [InlineData(852, "D", 10.00, "INACTIVE_ACCOUNT")]
        [InlineData(963, "D", 10.00, "INACTIVE_ACCOUNT")]
        [InlineData(123, "C",  0.00, "INVALID_VALUE")]
        [InlineData(123, "C", -1.00, "INVALID_VALUE")]
        [InlineData(123, "D",  0.00, "INVALID_VALUE")]
        [InlineData(123, "D", -1.00, "INVALID_VALUE")]
        [InlineData(123, "X", 10.00, "INVALID_TYPE")]
        public async Task ValidateReturnStatus(int AccountNumber, string MovementType, double MovementValue, string ExpectedStatusReturn)
        {
            HttpClient client = new HttpClient();
            AccountMovementRequest updatedData = new() { AccountNumber = AccountNumber, MovementType = MovementType, MovementValue = MovementValue };

            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(updatedData), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_uri, jsonContent);
            AccountMovementResponse? accountMovementResponse = response.Content.ReadFromJsonAsync<AccountMovementResponse>().Result;
            
            Assert.Equal(ExpectedStatusReturn, accountMovementResponse?.ResultType);
        }

        /// <summary>
        /// Caso os dados sejam recebidos e estejam válidos, devem ser persistidos na tabela 
        /// MOVIMENTO e deve retornar HTTP 200 e retornar no body Id do movimento gerado.
        /// </summary>
        [Theory]
        [InlineData(123, "C", 10.00, false)]
        [InlineData(456, "C", 10.00, false)]
        [InlineData(789, "C", 10.00, false)]
        [InlineData(123, "D", 10.00, false)]
        [InlineData(456, "D", 10.00, false)]
        [InlineData(789, "D", 10.00, false)]
        [InlineData(999, "C", 10.00, true)]
        [InlineData(741, "D", 10.00, true)]
        [InlineData(852, "D", 10.00, true)]
        [InlineData(963, "D", 10.00, true)]
        [InlineData(123, "C",  0.00, true)]
        [InlineData(123, "C", -1.00, true)]
        [InlineData(123, "D",  0.00, true)]
        [InlineData(123, "D", -1.00, true)]
        [InlineData(123, "X", 10.00, true)]
        public async Task ValidateUidReturn(int AccountNumber, string MovementType, double MovementValue, bool ExpectedUidReturn)
        {
            HttpClient client = new HttpClient();
            AccountMovementRequest updatedData = new() { AccountNumber = AccountNumber, MovementType = MovementType, MovementValue = MovementValue };

            StringContent jsonContent = new StringContent(JsonConvert.SerializeObject(updatedData), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_uri, jsonContent);
            AccountMovementResponse? accountMovementResponse = response.Content.ReadFromJsonAsync<AccountMovementResponse>().Result;

            Assert.Equal(ExpectedUidReturn, string.IsNullOrEmpty(accountMovementResponse?.MovementId));
        }
    }
}