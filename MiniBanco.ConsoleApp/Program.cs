using MiniBanco.ConsoleApp.Models;

Conta conta = new Conta(100);

conta.Depositar(50);
conta.Sacar(30);

Console.WriteLine($"Saldo atual: R$ {conta.Saldo:F2}");
Console.WriteLine($"Transações realizadas:{conta.Transacoes.Count}");
