using System;
using System.Collections.Generic;
using System.Globalization;
using MiniBanco.ConsoleApp.Exceptions;
using MiniBanco.ConsoleApp.Models;
using MiniBanco.ConsoleApp.Services;

// Permite digitar valores como 150,50.
CultureInfo.CurrentCulture = new CultureInfo("pt-BR");

List<Conta> contas = new List<Conta>();
MotorAnalise motor = new MotorAnalise();

bool executando = true;

while (executando)
{
    Console.WriteLine("\n===== MINIBANCO =====");
    Console.WriteLine("1 - Abrir nova conta");
    Console.WriteLine("2 - Registrar movimentação");
    Console.WriteLine("3 - Aplicar fechamento mensal");
    Console.WriteLine("4 - Exibir relatório das contas");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");

    string? opcao = Console.ReadLine();

    try
    {
        switch (opcao)
        {
            case "1":
            {
                Console.WriteLine("\nTIPOS DE CONTA:");
                Console.WriteLine("1 - Conta Corrente");
                Console.WriteLine("2 - Conta Poupança");
                Console.WriteLine("3 - Conta Salário");
                Console.Write("Escolha o tipo: ");

                string tipo = Console.ReadLine() ?? "";

                if (tipo != "1" && tipo != "2" && tipo != "3")
                {
                    Console.WriteLine("Tipo de conta inválido.");
                    break;
                }

                Console.Write("Nome do titular: ");
                string titular = Console.ReadLine() ?? "";

                Console.Write("Número da conta: ");
                int numero = int.Parse(Console.ReadLine() ?? "");

                bool numeroExiste = false;

                foreach (Conta contaExistente in contas)
                {
                    if (contaExistente.Numero == numero)
                    {
                        numeroExiste = true;
                        break;
                    }
                }

                if (numeroExiste)
                {
                    Console.WriteLine(
                        "Já existe uma conta com esse número.");
                    break;
                }

                Console.Write("Saldo inicial: R$ ");
                decimal saldoInicial =
                    decimal.Parse(Console.ReadLine() ?? "");

                Conta novaConta;

                if (tipo == "1")
                {
                    Console.Write("Limite do cheque especial: R$ ");
                    decimal limite =
                        decimal.Parse(Console.ReadLine() ?? "");

                    novaConta = new ContaCorrente(
                        titular,
                        numero,
                        saldoInicial,
                        limite);
                }
                else if (tipo == "2")
                {
                    Console.Write(
                        "Taxa de rendimento mensal em % (ex.: 0,5): ");

                    double taxa =
                        double.Parse(Console.ReadLine() ?? "");

                    Console.Write("Dia de aniversário (1 a 31): ");
                    int dia =
                        int.Parse(Console.ReadLine() ?? "");

                    novaConta = new ContaPoupanca(
                        titular,
                        numero,
                        saldoInicial,
                        taxa,
                        dia);
                }
                else
                {
                    Console.Write("Nome do empregador: ");
                    string empregador = Console.ReadLine() ?? "";

                    Console.Write(
                        "Quantidade de saques gratuitos por mês: ");

                    int limiteSaques =
                        int.Parse(Console.ReadLine() ?? "");

                    novaConta = new ContaSalario(
                        titular,
                        numero,
                        saldoInicial,
                        empregador,
                        limiteSaques);
                }

                contas.Add(novaConta);

                Console.WriteLine("Conta aberta com sucesso!");
                break;
            }

            case "2":
            {
                if (contas.Count == 0)
                {
                    Console.WriteLine(
                        "Nenhuma conta aberta até o momento.");
                    break;
                }

                Console.Write("Número da conta: ");
                int numero =
                    int.Parse(Console.ReadLine() ?? "");

                Conta? contaSelecionada = null;

                foreach (Conta conta in contas)
                {
                    if (conta.Numero == numero)
                    {
                        contaSelecionada = conta;
                        break;
                    }
                }

                if (contaSelecionada == null)
                {
                    Console.WriteLine("Conta não encontrada.");
                    break;
                }

                Console.WriteLine("\n1 - Depósito");
                Console.WriteLine("2 - Saque");
                Console.Write("Escolha a operação: ");

                string operacao = Console.ReadLine() ?? "";

                if (operacao != "1" && operacao != "2")
                {
                    Console.WriteLine("Operação inválida.");
                    break;
                }

                Console.Write("Valor: R$ ");
                decimal valor =
                    decimal.Parse(Console.ReadLine() ?? "");

                if (operacao == "1")
                {
                    Console.Write("Descrição do depósito: ");
                    string descricao = Console.ReadLine() ?? "";

                    contaSelecionada.Depositar(valor, descricao);

                    Console.WriteLine(
                        "Depósito realizado com sucesso!");
                }
                else
                {
                    contaSelecionada.Sacar(valor);

                    Console.WriteLine(
                        "Saque realizado com sucesso!");
                }

                Console.WriteLine(
                    $"Saldo atual: R$ {contaSelecionada.Saldo:F2}");

                break;
            }

            case "3":
            {
                if (contas.Count == 0)
                {
                    Console.WriteLine(
                        "Nenhuma conta aberta até o momento.");
                    break;
                }

                Console.WriteLine(
                    "\nCada execução aplica um novo fechamento.");
                Console.Write(
                    "Confirma o fechamento de todas as contas? (S/N): ");

                string confirmacao = Console.ReadLine() ?? "";

                if (!confirmacao.Equals(
                    "S",
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Fechamento cancelado.");
                    break;
                }

                foreach (Conta conta in contas)
                {
                    // Cada objeto executa sua própria regra.
                    conta.AplicarFechamentoMensal();

                    Console.WriteLine(
                        $"Fechamento aplicado à conta {conta.Numero}.");
                }

                Console.WriteLine("Fechamento mensal concluído!");
                break;
            }

            case "4":
            {
                if (contas.Count == 0)
                {
                    Console.WriteLine(
                        "Nenhuma conta aberta até o momento.");
                    break;
                }

                foreach (Conta conta in contas)
                {
                    motor.ExibirRelatorio(conta);
                }

                break;
            }

            case "0":
            {
                executando = false;
                Console.WriteLine(
                    "Obrigado por utilizar o MiniBanco!");
                break;
            }

            default:
            {
                Console.WriteLine("Opção inválida.");
                break;
            }
        }
    }
    catch (SaldoInsuficienteException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
    catch (FormatException)
    {
        Console.WriteLine(
            "Entrada inválida. Digite um número válido.");
    }
    catch (OverflowException)
    {
        Console.WriteLine(
            "O número informado ou o resultado excede o limite permitido.");
    }
}