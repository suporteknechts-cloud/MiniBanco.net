using System;

namespace MiniBanco.ConsoleApp.Exceptions
{
    public class SaldoInsuficienteException : Exception
    {
        public SaldoInsuficienteException() : base("Saldo insuficiente para realizar o saque.")
        {
        }
    }
}

