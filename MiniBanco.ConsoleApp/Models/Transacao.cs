namespace MiniBanco.ConsoleApp.Models
{
        public class Transacao
        {
            public string? Tipo { get; set; } = "";
            public decimal Valor { get; set; }
            public DateTime Data { get; set; }
            public Transacao(string tipo, decimal valor)
        {
            Tipo = tipo;
            Valor = valor;
            Data = DateTime.Now;
        }
   } 
}