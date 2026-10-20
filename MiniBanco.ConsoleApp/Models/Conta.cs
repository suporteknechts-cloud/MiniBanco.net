
using System;
using System.Collections.Generic;
using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
    public class Conta
    {
        public string Titular { get; }
        public int Numero { get; }
        public decimal Saldo { get; protected set; }
        public List<Transacao> Extrato { get; }

        public Conta(string titular, int numero, decimal saldoInicial)
        {
            if (string.IsNullOrWhiteSpace(titular))
                throw new ArgumentException("O titular é obrigatório.");

            if (numero <= 0)
                throw new ArgumentException("O número da conta deve ser positivo.");

            if (saldoInicial < 0)
                throw new ArgumentException("O saldo inicial não pode ser negativo.");

            this.Titular = titular;
            this.Numero = numero;
            this.Saldo = saldoInicial;
            this.Extrato = new List<Transacao>();
        }

        public void RegistrarTransacao(
            string descricao, string tipo, decimal valor)
        {
            Transacao transacao =
                new Transacao(descricao, tipo, valor);

            Extrato.Add(transacao);
        }

        public void Depositar(decimal valor, string descricao)
        {
            if (valor <= 0)
                throw new ArgumentException(
                    "O depósito deve ser maior que zero.");

            RegistrarTransacao(descricao, "Deposito", valor);
            Saldo += valor;
        }

        public virtual void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException(
                    "O saque deve ser maior que zero.");

            if (valor > Saldo)
                throw new SaldoInsuficienteException();

            RegistrarTransacao("Saque", "Saque", valor);
            Saldo -= valor;
        }

        public virtual void AplicarFechamentoMensal()
        {
            // Cada subclasse terá sua própria regra.
        }

        public void ExibirExtrato()
        {
            Console.WriteLine("=== EXTRATO BANCÁRIO ===");

            foreach (Transacao transacao in Extrato)
            {
                Console.WriteLine(
                    $"{transacao.Descricao} | " +
                    $"{transacao.Tipo} | " +
                    $"R$ {transacao.Valor:F2}");
            }

            Console.WriteLine($"Saldo atual: R$ {Saldo:F2}");
        }
    }
}
