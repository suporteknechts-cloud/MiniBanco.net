using MiniBanco.ConsoleApp.Exceptions;
using MiniBanco.ConsoleApp.Models;

Conta conta = new Conta(100);

conta.Depositar(50);
conta.Sacar(30);

try
{
    conta.Sacar(1000);
}
catch (SaldoInsuficienteException ex)
{
    Console.WriteLine($"Erro: {ex.Message}");
}

Console.WriteLine($"Saldo atual: R$ {conta.Saldo:F2}");
Console.WriteLine($"Transações realizadas:{conta.Transacoes.Count}");

conta.ExibirExtrato();