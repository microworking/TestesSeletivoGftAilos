using Questao1.Domain;

namespace Questao1.Application
{
    public class ContaBancaria
    {
        ContaDTO contaDTO = null;

        public ContaBancaria(int Numero, string Titular)
        {
            contaDTO = new ContaDTO(Numero);
            contaDTO.Titular = Titular;
            contaDTO.Saldo = 0;
        }

        public ContaBancaria(int Numero, string Titular, double DepositoInicial)
        {
            contaDTO = new ContaDTO(Numero);
            contaDTO.Titular = Titular;
            contaDTO.Saldo = DepositoInicial;
        }

        public void Deposito(double Quantia)
        {
            contaDTO.Saldo += Quantia;
        }

        public void Saque(double Quantia)
        {
            contaDTO.Saldo -= Quantia;
            contaDTO.Saldo -= contaDTO.TaxaSaque;
        }

        public string ExibirDadosCliente()
        {
            return $"Conta {contaDTO.Numero}, Titular: {contaDTO.Titular}, Saldo: {contaDTO.Saldo.ToString("C")}";
        }
    }
}