namespace SITA.src.Model
{
   
    public class Salario : Despesa
    {
        public Guid FuncionarioId { get; set; }
        public string MesReferencia { get; set; } = string.Empty;

        public Salario() : base()
        {
            Descricao = "Pagamento de Folha Salarial";
            Categoria = "Recursos Humanos";
        }
    }
}