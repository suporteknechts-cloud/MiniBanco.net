using System.Collections.Generic;

namespace MiniBanco.ConsoleApp.Models
{
    public class Conta
    {
        public decimal Saldo { get; private set; }

        public List<Transacao> Transacoes { get; private set; }

        public Conta(decimal saldoInicial)
        {
            Saldo = saldoInicial;
            Transacoes = new List<Transacao>();
            
        }
        public void Depositar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do depósito deve ser maior que zero.");
            }

            Saldo += valor;
            Transacoes.Add(new Transacao("Depósito", valor));
        }
        public void Sacar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor do saque deve ser maior que zero.");
            }

            if (valor > Saldo)
            {
                throw new InvalidOperationException("Saldo insuficiente para realizar o saque.");
            }

            Saldo -= valor;
            Transacoes.Add(new Transacao("Saque", valor));
        }
        public void ExibirExtrato()
        {
            Console.WriteLine("=== EXTRATO BANCÁRIO ===");

            foreach (var transacao in Transacoes)
            {
                Console.WriteLine(
                    $"{transacao.Data:dd/MM/yyyy HH:mm} | "
                + 
                    $" {transacao.Tipo} | " + $"R$ {transacao.Valor:F2}");
            }
            Console.WriteLine($"Saldo atual: R$ {Saldo:F2}");
        }
    }
}