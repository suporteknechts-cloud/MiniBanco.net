# MiniBanco .NET

Projeto desenvolvido por Vanderlei Knecht para o mini-projeto avaliativo do curso Desenvolvedor Back-End .NET do SENAI.

## Objetivo

Desenvolver uma aplicação de console em C# para gerenciar contas bancárias, registrar movimentações, aplicar regras de fechamento mensal e analisar a saúde das contas.

## Funcionalidades

- Abrir contas correntes, poupanças e salários.
- Impedir o cadastro de contas com números repetidos.
- Realizar depósitos e saques.
- Registrar movimentações no extrato.
- Aplicar o fechamento mensal de todas as contas.
- Calcular pontuação e percentual de saúde.
- Classificar as contas e apresentar recomendações para a gerência.
- Listar tarifas como itens críticos e saques como itens de atenção.
- Tratar entradas inválidas e operações não permitidas.

## Tecnologias e conceitos

- C# e .NET 10.
- Console Application.
- Programação Orientada a Objetos.
- Encapsulamento, herança e polimorfismo.
- Construtores, métodos virtuais e sobrescrita com override.
- Coleções List<T>.
- Laços foreach e estruturas condicionais, sem LINQ.
- Tratamento de exceções.
- Git e GitHub.

## Estrutura do projeto

```text
minibanco-dotnet/
├── MiniBanco.ConsoleApp/
│   ├── Exceptions/
│   │   └── SaldoInsuficienteException.cs
│   ├── Models/
│   │   ├── Conta.cs
│   │   ├── ContaCorrente.cs
│   │   ├── ContaPoupanca.cs
│   │   ├── ContaSalario.cs
│   │   └── Transacao.cs
│   ├── Services/
│   │   └── MotorAnalise.cs
│   ├── MiniBanco.ConsoleApp.csproj
│   └── Program.cs
├── .gitignore
└── README.md
```

## Como executar

É necessário ter o SDK do .NET 10 instalado.

Clone o repositório:

```bash
git clone https://github.com/suporteknechts-cloud/MiniBanco.net.git
```

Entre na pasta criada:

```bash
cd MiniBanco.net
```

Execute a aplicação:

```bash
dotnet run --project MiniBanco.ConsoleApp/MiniBanco.ConsoleApp.csproj
```

```

Para compilar sem executar:

```bash
dotnet build MiniBanco.ConsoleApp/MiniBanco.ConsoleApp.csproj
```

## Menu

```text
1 - Abrir nova conta
2 - Registrar movimentação
3 - Aplicar fechamento mensal
4 - Exibir relatório das contas
0 - Sair
```

Nos campos numéricos, utilize vírgula como separador decimal.
Exemplos: `150,50` para valores monetários e `0,5` para uma taxa de 0,5%.

## Regras de negócio

### Transações

Cada transação possui descrição, tipo e valor.

Os tipos permitidos são:

- Deposito.
- Saque.
- Tarifa.

O valor deve ser maior que zero. A descrição não pode estar vazia.

### Conta corrente

- Permite saques até o saldo disponível mais o limite do cheque especial.
- O uso do cheque especial pode deixar o saldo negativo.
- Cada fechamento mensal desconta e registra uma tarifa de manutenção de R$ 10,00.

### Conta poupança

- Permite saques somente até o saldo disponível.
- Possui taxa de rendimento mensal e dia de aniversário.
- No fechamento, calcula o rendimento pela fórmula:

```text
Rendimento = Saldo × Taxa / 100
```

O rendimento é arredondado para duas casas decimais.
Quando positivo, é creditado e registrado como depósito.

O dia de aniversário é armazenado e exibido no relatório.
O fechamento é acionado manualmente pelo menu, sem verificar a data atual.

### Conta salário

- Possui empregador e quantidade de saques gratuitos por mês.
- Cada saque que ultrapassa essa quantidade gera uma tarifa de R$ 5,00.
- O saldo deve ser suficiente para cobrir o saque e a eventual tarifa.
- Saques recusados não aumentam o contador.
- O fechamento mensal zera o contador de saques.
- Não há tarifa de manutenção no fechamento.

### Pontuação e saúde da conta

| Tipo de transação | Pontuação |
| --- | ---: |
| Depósito | 10 |
| Saque | 5 |
| Tarifa | 0 |

O percentual é calculado por:

```text
Pontuação máxima = Quantidade de transações × 10
Saúde (%) = Pontuação obtida / Pontuação máxima × 100
```

Contas sem transações recebem percentual de 0%, evitando divisão por zero.

| Percentual | Classificação | Recomendação |
| --- | --- | --- |
| 90% a 100% | Conta Saudável | Oferecer investimentos e avaliar aumento de limite. |
| 60% até menos de 90% | Conta em Atenção | Contatar o cliente para orientação financeira e revisão de tarifas. |
| Abaixo de 60% | Conta em Risco | Bloquear aumento de limite e abrir análise de crédito. |

Essas recomendações são apenas exibidas no relatório.
O programa não executa bloqueios nem análises de crédito reais.

## Organização com POO

A classe base `Conta` reúne os dados e comportamentos comuns.

As classes `ContaCorrente`, `ContaPoupanca` e `ContaSalario` herdam de `Conta` e implementam suas regras específicas.

O saldo utiliza `protected set`, permitindo alteração direta apenas pela classe base e suas subclasses.

No fechamento mensal, o programa percorre uma `List<Conta>` e chama `AplicarFechamentoMensal()` sem verificar o tipo de cada objeto, demonstrando polimorfismo.

A classe `MotorAnalise` concentra os cálculos e a exibição da análise.

## Tratamento de erros

O menu trata:

- Saldo insuficiente.
- Valores e argumentos inválidos.
- Texto digitado em campos numéricos.
- Números que excedem os limites dos tipos utilizados.

Após o tratamento, o programa apresenta uma mensagem e retorna ao menu.

## Testes manuais realizados

- Conta corrente: saque utilizando cheque especial e cobrança de manutenção.
- Poupança: rendimento de R$ 5,00 sobre saldo de R$ 1.000,00 com taxa de 0,5%.
- Conta salário: cobrança por saque excedente e liberação de novo saque gratuito após o fechamento.
- Conferência de pontuação, percentual e classificação nos relatórios.
- Rejeição de depósito negativo.
- Tratamento de texto em campo numérico.
- Rejeição de saque com saldo insuficiente.

## Limitações e melhorias futuras

- Os dados ficam somente na memória e são perdidos ao encerrar o programa.
- O saldo inicial não é registrado como transação.
- Cada confirmação de fechamento aplica novamente as regras a todas as contas.
- O fechamento não utiliza um calendário nem controla competências mensais.
- A análise considera todo o histórico disponível na execução.
- A classificação segue a pontuação didática do enunciado, sem avaliar risco financeiro real.
- Não há autenticação, banco de dados ou testes automatizados.

Como melhorias futuras, podem ser implementados persistência de dados, controle de fechamento por mês e testes automatizados.

## Apoio ao desenvolvimento

Foi utilizada inteligência artificial como apoio para esclarecer dúvidas de C#, compreender conceitos de orientação a objetos e orientar etapas do desenvolvimento.

A validação incluiu compilação e testes manuais das movimentações, fechamento mensal, relatórios e tratamento de erros.

## Vídeo de apresentação

https://youtu.be/ltAvnoMsyP0