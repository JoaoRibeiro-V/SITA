namespace SITA.src.Model
{
    public class Receita : Financeiro
    {
        private static readonly Dictionary<int, string> StatusList = new Dictionary<int, string>
        {
            { 0, "Em andamento" },
            { 1, "Pago" },
            { 2, "Em atraso" }
        };

        public string Origem { get; set; } = "Mensalidade";

        public Guid AlunoId { get; set; }
        public string? ResponsavelCPF { get; set; }

        public int Status { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; } 

        public Receita() : base() 
        {
            Status = 0; // Em andamento
        }

        // Método auxiliar para o Dashboard exibir o texto correto
        public string GetStatusTexto() => StatusList.ContainsKey(Status) ? StatusList[Status] : "Indefinido";
    }
}