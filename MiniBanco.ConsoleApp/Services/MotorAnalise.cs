using System;
using MiniBanco.ConsoleApp.Models;

namespace MiniBanco.ConsoleApp.Services
{
    public class MotorAnalise
    {
        public int CalcularPontuacao(Conta conta)
        {
            int pontuacao = 0;

            foreach (Transacao transacao in conta.Extrato)
            {
                if (transacao.Tipo == "Deposito")
                {
                    pontuacao += 10;
                }
                else if (transacao.Tipo == "Saque")
                {
                    pontuacao += 5;
                }
            }

            return pontuacao;
        }
        public double CalcularPercentual(Conta conta)
        {
            int pontuacaoMaxima = conta.Extrato.Count * 10;

            if (pontuacaoMaxima == 0)
            {
                return 0;
            }

            int pontuacaoObtida = CalcularPontuacao(conta);

            double percentual =
                (double)pontuacaoObtida / pontuacaoMaxima * 100;

            return percentual;
        }
        public string ClassificarConta(double percentual)
        {
            if (percentual >= 90)
            {
                return "Conta Saudável";
            }
            else if (percentual >= 60)
            {
                return "Conta em Atenção";
            }
            else
            {
                return "Conta em Risco";
            }
        }
        public string ObterRecomendacao(double percentual)
        {
            if (percentual >= 90)
            {
                return "Oferecer investimentos e avaliar aumento de limite.";
            }
            else if (percentual >= 60)
            {
                return "Contatar o cliente para orientação financeira e revisão de tarifas.";
            }
            else
            {
                return "Bloquear aumento de limite e abrir análise de crédito.";
            }
        }
        public void ExibirItensCriticos(Conta conta)
        {
            Console.WriteLine("\nITENS CRÍTICOS — TARIFAS:");

            bool encontrouTarifa = false;

            foreach (Transacao transacao in conta.Extrato)
            {
                if (transacao.Tipo == "Tarifa")
                {
                    encontrouTarifa = true;

                    Console.WriteLine(
                        $"- {transacao.Descricao}: R$ {transacao.Valor:F2}. " +
                        "Avaliar revisão ou isenção da cobrança.");
                }
            }

            if (!encontrouTarifa)
            {
                Console.WriteLine("Nenhuma tarifa registrada.");
            }
        }
        public void ExibirItensAtencao(Conta conta)
        {
            Console.WriteLine("\nITENS DE ATENÇÃO — SAQUES:");

            bool encontrouSaque = false;

            foreach (Transacao transacao in conta.Extrato)
            {
                if (transacao.Tipo == "Saque")
                {
                    encontrouSaque = true;

                    Console.WriteLine(
                        $"- {transacao.Descricao}: R$ {transacao.Valor:F2}. " +
                        "Acompanhar as retiradas e orientar o cliente.");
                }
            }

            if (!encontrouSaque)
            {
                Console.WriteLine("Nenhum saque registrado.");
            }
        }
        public void ExibirRelatorio(Conta conta)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("RELATÓRIO DA CONTA");
            Console.WriteLine("========================================");

            Console.WriteLine($"Titular: {conta.Titular}");
            Console.WriteLine($"Número: {conta.Numero}");
            
            if (conta is ContaCorrente corrente)
            {
                Console.WriteLine("Tipo: Conta Corrente");
                Console.WriteLine(
                    $"Limite do cheque especial: R$ {corrente.LimiteChequeEspecial:F2}");
            }
            else if (conta is ContaPoupanca poupanca)
            {
                Console.WriteLine("Tipo: Conta Poupança");
                Console.WriteLine(
                    $"Rendimento mensal: {poupanca.TaxaRendimentoMensal}%");
                Console.WriteLine(
                    $"Dia de aniversário: {poupanca.DiaAniversario}");
            }
            else if (conta is ContaSalario salario)
            {
                Console.WriteLine("Tipo: Conta Salário");
                Console.WriteLine($"Empregador: {salario.Empregador}");
                Console.WriteLine(
                    $"Limite de saques gratuitos: {salario.LimiteSaquesMensais}");
                Console.WriteLine(
                    $"Saques realizados no mês: {salario.SaquesRealizadosNoMes}");
            }

            conta.ExibirExtrato();

            int pontos = CalcularPontuacao(conta);
            int pontosMaximos = conta.Extrato.Count * 10;
            double percentual = CalcularPercentual(conta);

            Console.WriteLine("\nRESUMO DA ANÁLISE:");
            Console.WriteLine($"Pontuação: {pontos} de {pontosMaximos}");
            Console.WriteLine($"Índice de saúde: {percentual:F1}%");
            Console.WriteLine($"Classificação: {ClassificarConta(percentual)}");

            Console.WriteLine(
                $"Recomendação: {ObterRecomendacao(percentual)}");

            ExibirItensCriticos(conta);
            ExibirItensAtencao(conta);
        }
    }
}