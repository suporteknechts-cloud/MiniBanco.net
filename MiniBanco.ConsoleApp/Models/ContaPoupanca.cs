using System;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaPoupanca : Conta
    {
        public double TaxaRendimentoMensal { get; }
        public int DiaAniversario { get; }

        public ContaPoupanca(
            string titular,
            int numero,
            decimal saldoInicial,
            double taxaRendimentoMensal,
            int diaAniversario)
            : base(titular, numero, saldoInicial)
        {
            if (double.IsNaN(taxaRendimentoMensal) ||
                double.IsInfinity(taxaRendimentoMensal) ||
                taxaRendimentoMensal < 0)
            {
                throw new ArgumentException(
                    "A taxa de rendimento deve ser um número finito e não negativo.");
            }

            if (diaAniversario < 1 || diaAniversario > 31)
            {
                throw new ArgumentException(
                    "O dia de aniversário deve estar entre 1 e 31.");
            }

            this.TaxaRendimentoMensal = taxaRendimentoMensal;
            this.DiaAniversario = diaAniversario;
        }
        public override void AplicarFechamentoMensal()
        {
            decimal taxa = (decimal)TaxaRendimentoMensal;
            decimal rendimento = Saldo * taxa / 100m;

            rendimento = Math.Round(rendimento, 2);

            if (rendimento > 0)
            {
                Depositar(rendimento, "Rendimento mensal");
            }
        }
    }
}