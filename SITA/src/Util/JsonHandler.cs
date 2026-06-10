using SITA.src.Controller;
using SITA.src.Model;
using SITA.src.Util;
using System.Text.Json;

public static class JsonHandler
{
    private class ImportRoot
    {
        public List<UserDTO>? users { get; set; }
        public TurmaDTO? turma { get; set; }
        public List<ResponsavelDTO>? responsaveis { get; set; }
        public List<DespesaDTO>? despesas { get; set; }
        public List<FuncionarioDTO>? funcionarios { get; set; }
        public List<ItemEstoqueDTO>? estoque { get; set; }
        public decimal mensalidade { get; set; }
        public DateTime fimMatricula { get; set; }
    }
    private class ItemEstoqueDTO
    {
        public string nome { get; set; } = string.Empty;
        public string? descricao { get; set; }
        public string? categoria { get; set; }  // "Uniforme", "Material", "Outros"
        public int quantidade { get; set; }
        public float precoCusto { get; set; }   // preço de compra (gera despesa)
        public float precoVenda { get; set; }   // preço de venda (gera receita)
        public string? fornecedor { get; set; }
        public DateTime dataCadastro { get; set; }
    }
    private class FuncionarioDTO
    {
        public string? nome { get; set; }
        public string? cpf { get; set; }

        public string? email { get; set; }
        public string? senha { get; set; }

        public string? cargo { get; set; }
        public string? departamento { get; set; }

        public DateTime dataAdmissao { get; set; }
        public decimal salarioBase { get; set; }

        public string? telefone { get; set; }
        public string? chavePix { get; set; }

        public bool ativo { get; set; }

        public int accessLevel { get; set; }
    }
    private class DespesaDTO
    {
        public float valor { get; set; }
        public string? descricao { get; set; }
        public string? observacao { get; set; }

        public string? fornecedor { get; set; }
        public string? categoria { get; set; }

        public DateTime dataPagamento { get; set; }
        public DateTime dataVencimento { get; set; }

        public bool status { get; set; }

        public string? quemPagou { get; set; }

        public string? cnpjFornecedor { get; set; }
        public string? numeroNotaFiscal { get; set; }
        public string? chaveAcessoNF { get; set; }
        public string? anexoCaminho { get; set; }
    }
    private class UserDTO
    {
        public string? nome { get; set; }
        public string? cpf { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public int nivelAcesso { get; set; }
    }
    private class TurmaDTO
    {
        public string? nome { get; set; }
        public string? turno { get; set; }
    }
    private class ResponsavelDTO
    {
        public string? nome { get; set; }
        public string? cpf { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
        public string? telefone { get; set; }
        public string? endereco { get; set; }
        public List<AlunoDTO>? alunos { get; set; }
    }
    private class AlunoDTO
    {
        public string? nome { get; set; }
        public DateTime? dataNascimento { get; set; }
        public string? religiao { get; set; }
        public string? nomePediatra { get; set; }
        public string? orientacoesEmergencia { get; set; }
        public List<string>? alergias { get; set; }
        public List<string>? condicoesEspeciais { get; set; }
    }
    public static void ImportFull(string json)
    {
        var root = JsonSerializer.Deserialize<ImportRoot>(json);
        if (root == null)
            throw new Exception("JSON inválido");
        foreach (var despesaDTO in root.despesas ?? new())
        {
            var despesa = new Despesa
            {
                Valor = despesaDTO.valor,
                Descricao = despesaDTO.descricao,
                Observacao = despesaDTO.observacao,

                Fornecedor = despesaDTO.fornecedor,
                Categoria = despesaDTO.categoria,

                DataPagamento = despesaDTO.dataPagamento,
                DataVencimento = despesaDTO.dataVencimento,
                DataReferente = despesaDTO.dataPagamento,

                Status = despesaDTO.status ? Financeiro.FinanceStatus.Pago : Financeiro.FinanceStatus.Pendente,

                QuemPagou = despesaDTO.quemPagou,

                CnpjFornecedor = despesaDTO.cnpjFornecedor,
                NumeroNotaFiscal = despesaDTO.numeroNotaFiscal,
                ChaveAcessoNF = despesaDTO.chaveAcessoNF,
                AnexoCaminho = despesaDTO.anexoCaminho
            };

            DespesaController.Register(despesa);
        }
        foreach (var userDTO in root.users ?? new())
        {
            var existingUser = UserController.Get("CPF", userDTO.cpf);
            if (existingUser != null)
                throw new Exception($"CPF já cadastrado: {userDTO.cpf}");
            var user = new User
            {
                Nome = userDTO.nome,
                CPF = userDTO.cpf,
                Email = userDTO.email,
                DataCriacao = DateTime.Now,
                DataUltimoAcesso = DateTime.Now,
                Ativo = true,
                AccessType = new AccessType
                {
                    Level = userDTO.nivelAcesso     
                }
            };
            user.Senha = PasswordHandler.HashPassword(userDTO.senha ?? "password", user.Salt);
            UserController.Register(user);
        }

        if (root.turma == null)
            throw new Exception("Turma não informada");

        var turma = new Turma
        {
            Nome = root.turma.nome ?? "Turma",
            Turno = root.turma.turno ?? "Manhã"
        };

        TurmaController.Register(turma);
        foreach (var respDTO in root.responsaveis ?? new())
        {
            var existingUser = UserController.Get("CPF", respDTO.cpf);
            if (existingUser != null)
                throw new Exception($"CPF já cadastrado: {respDTO.cpf}");

            var user = new User
            {
                Nome = respDTO.nome,
                CPF = respDTO.cpf,
                Email = respDTO.email,
                DataCriacao = DateTime.Now,
                DataUltimoAcesso = DateTime.Now,
                Ativo = true,
                AccessType = new AccessType { Level = 1 }
            };
            user.Senha = PasswordHandler.HashPassword(respDTO.senha ?? "password", user.Salt);

            UserController.Register(user);

            var responsavel = ResponsavelController.CreateByUser(user);
            responsavel.Telefone = respDTO.telefone;
            responsavel.Endereco = respDTO.endereco;

            ResponsavelController.Register(responsavel);

            foreach (var alunoDTO in respDTO.alunos ?? new())
            {
                var aluno = new Aluno
                {
                    Nome = alunoDTO.nome,
                    DataNascimento = alunoDTO.dataNascimento,
                    Religiao = alunoDTO.religiao,
                    NomePediatra = alunoDTO.nomePediatra,
                    OrientacoesEmergencia = alunoDTO.orientacoesEmergencia,
                    Alergias = alunoDTO.alergias ?? new List<string>(),
                    CondicoesEspeciais = alunoDTO.condicoesEspeciais ?? new List<string>(),
                    Turma = turma,
                    DataCadastro = DateTime.Now
                };

                aluno.Idade = aluno.CalcularIdade();
                aluno.RA = (Random.Shared.Next(1, 9999)).ToString();


                AlunoController.Register(aluno);

                ResponsavelController.AddParentesco(responsavel, aluno, 1);
                TurmaController.AddAlunoToTurma(turma, aluno);

                ReceitaController.GerarCarneAluno(
                    aluno,
                    responsavel,
                    root.mensalidade,
                    root.fimMatricula
                );
                decimal taxaAleatoria = 0;
                taxaAleatoria += new Random().Next(100, 500);

                ReceitaController.GerarParcelaInicial(aluno, responsavel, taxaAleatoria);
                var matriculas = ReceitaController.GetAll()
                    .Where(r => r.Aluno.Id == aluno.Id && r.Type == Receita.ReceitaTipo.Mensalidade)
                    .ToList().FirstOrDefault();
                matriculas.Status = Receita.FinanceStatus.Pago;
                matriculas.DataPagamento = DateTime.Now;

                var taxa = ReceitaController.GetAll()
                    .Where(r => r.Aluno.Id == aluno.Id && r.Type == Receita.ReceitaTipo.Taxas)
                    .ToList().FirstOrDefault();
                bool randomPago = new Random().Next(0, 2) == 0;
                taxa.Status = randomPago ? Receita.FinanceStatus.Pago : Receita.FinanceStatus.Pendente;
                taxa.DataPagamento = randomPago? DateTime.Now.AddDays(new Random().Next(2,10)) : null;
            }
        }
        foreach(ItemEstoqueDTO itemEstoqueDTO in root.estoque ?? new()) {
            ItemEstoque item = new ItemEstoque
            {
                Nome = itemEstoqueDTO.nome,
                Descricao = itemEstoqueDTO.descricao,
                Categoria = itemEstoqueDTO.categoria,
                Quantidade = itemEstoqueDTO.quantidade,
                PrecoCusto = itemEstoqueDTO.precoCusto,
                PrecoVenda = itemEstoqueDTO.precoVenda,
                Fornecedor = itemEstoqueDTO.fornecedor
            };
            EstoqueController.Register(item);
        }
        foreach(FuncionarioDTO funcionarioDTO in root.funcionarios ?? new())
        {
            Funcionario newFuncionario = new Funcionario
            {
                Nome = funcionarioDTO.nome,
                CPF = funcionarioDTO.cpf,
                Email = funcionarioDTO.email,
                Senha = funcionarioDTO.senha,

                Cargo = funcionarioDTO.cargo,
                Departamento = funcionarioDTO.departamento,

                DataAdmissao = funcionarioDTO.dataAdmissao,
                SalarioBase = funcionarioDTO.salarioBase,

                Telefone = funcionarioDTO.telefone,
                ChavePix = funcionarioDTO.chavePix,

                Ativo = funcionarioDTO.ativo,
                AccessType = new AccessType
                {
                    Level = funcionarioDTO.accessLevel
                }
            };
            FuncionarioController.Register(newFuncionario);
            SalarioController.GerarSalarioFuncionario(newFuncionario, newFuncionario.SalarioBase);
        }
    }
}