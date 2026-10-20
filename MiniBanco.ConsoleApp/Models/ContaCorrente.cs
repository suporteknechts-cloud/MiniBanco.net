using System;
using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaCorrente : Conta
    {
        public decimal LimiteChequeEspecial { get; }

        public ContaCorrente(
            string titular,
            int numero,
            decimal saldoInicial,
            decimal limiteChequeEspecial)
            : base(titular, numero, saldoInicial)
        {
            if (limiteChequeEspecial < 0)
            {
                throw new ArgumentException(
                    "O limite do cheque especial não pode ser negativo.");
            }

            this.LimiteChequeEspecial = limiteChequeEspecial;
        }
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException(
                    "O saque deve ser maior que zero.");
            }

            if (valor > Saldo + LimiteChequeEspecial)
            {
                throw new SaldoInsuficienteException();
            }

            RegistrarTransacao("Saque", "Saque", valor);
            Saldo -= valor;
        }
        public override void AplicarFechamentoMensal()
        {
            decimal tarifa = 10m;

            RegistrarTransacao(
                "Tarifa de manutenção",
                "Tarifa",
                tarifa);

            Saldo -= tarifa;
        }
    }
}