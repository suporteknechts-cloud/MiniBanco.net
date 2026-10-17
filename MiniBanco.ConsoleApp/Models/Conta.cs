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
    }
}