using System;
using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaSalario : Conta
    {
        public string Empregador { get; }
        public int LimiteSaquesMensais { get; }
        public int SaquesRealizadosNoMes { get; private set; }

        public ContaSalario(
            string titular,
            int numero,
            decimal saldoInicial,
            string empregador,
            int limiteSaquesMensais)
            : base(titular, numero, saldoInicial)
        {
            if (string.IsNullOrWhiteSpace(empregador))
            {
                throw new ArgumentException(
                    "O empregador é obrigatório.");
            }

            if (limiteSaquesMensais < 0)
            {
                throw new ArgumentException(
                    "O limite de saques não pode ser negativo.");
            }

            this.Empregador = empregador;
            this.LimiteSaquesMensais = limiteSaquesMensais;
            this.SaquesRealizadosNoMes = 0;
        }
        public override void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException(
                    "O saque deve ser maior que zero.");
            }

            bool cobrarTarifa =
                SaquesRealizadosNoMes >= LimiteSaquesMensais;

            decimal tarifa = 0m;

            if (cobrarTarifa)
            {
                tarifa = 5m;
            }

            if (valor > Saldo || tarifa > Saldo - valor)
            {
                throw new SaldoInsuficienteException();
            }

            base.Sacar(valor);

            if (cobrarTarifa)
            {
                RegistrarTransacao(
                    "Tarifa por saque excedente",
                    "Tarifa",
                    tarifa);

                Saldo -= tarifa;
            }

            SaquesRealizadosNoMes++;
        }
        public override void AplicarFechamentoMensal()
        {
            SaquesRealizadosNoMes = 0;
        }
    }
}