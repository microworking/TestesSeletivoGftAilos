
namespace Questao1.Domain
{
    public class ContaDTO
    {
        private const double _taxaSaque = 3.5;

        public ContaDTO(int Numero)
        {
            this.TaxaSaque = _taxaSaque;
            this.Numero = Numero;
        }

        public int Numero { get; }

        public string Titular { get; set; }

        public double Saldo { get; set; }

        public double TaxaSaque { get; }
    }
}