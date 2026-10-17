using MiniBanco.ConsoleApp.Exceptions;
using MiniBanco.ConsoleApp.Models;

Conta conta = new Conta(100);

//conta.Depositar(50);
//conta.Sacar(30);



//Console.WriteLine($"Saldo atual: R$ {conta.Saldo:F2}");
//Console.WriteLine($"Transações realizadas:{conta.Transacoes.Count}");

conta.ExibirExtrato();

while (true)
{
    Console.WriteLine("\n===== MINIBANCO =====");
    Console.WriteLine("1 - CONSULTAR SALDO");  
    Console.WriteLine("2 - DEPOSITAR");  
    Console.WriteLine("3 - SACAR");  
    Console.WriteLine("4 - VER EXTRATO");  
    Console.WriteLine("5 - SAIR");  
    Console.Write("Escolha uma opção: ");

    string? opcao = Console.ReadLine();

     if (opcao == "0")
    {
        Console.WriteLine("Obrigado por utilizar o MiniBanco. Até logo!");
        break;
        
    }

    if (opcao == "1")
    {
        Console.WriteLine($"Saldo atual: R$ {conta.Saldo:F2}");
        
    }

    if (opcao == "2")
    {
        Console.Write("Digite o valor do depósito: R$ ");
        string? entrada = Console.ReadLine();
        if (decimal.TryParse(entrada, out decimal valor))
        {
            try
            {
                conta.Depositar(valor);
                Console.WriteLine($"Depósito realizado com sucesso.");
                Console.WriteLine($"Novo saldo: R$ {conta.Saldo:F2}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Valor inválido. Por favor, digite um número válido.");
        }
        
    }

    if (opcao == "3")
    {
        Console.Write("Digite o valor do saque: R$ ");
        string? entrada = Console.ReadLine();
        if (decimal.TryParse(entrada, out decimal valor))
        {
            try
            {
                conta.Sacar(valor);
                Console.WriteLine($"Saque realizado com sucesso.");
                Console.WriteLine($"Novo saldo: R$ {conta.Saldo:F2}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            catch (SaldoInsuficienteException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Valor inválido. Por favor, digite um número válido.");
        }
        
    }

    if (opcao == "4")
    {
        conta.ExibirExtrato();
        
    }

}