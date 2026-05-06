using SITA.src.Controller;
using SITA.src.Model;
using SITA.src.Util;
using System.Text.Json;

public static class JsonHandler
{
    private class ImportRoot
    {
        public AdminDTO? admin { get; set; }
        public TurmaDTO? turma { get; set; }
        public List<ResponsavelDTO>? responsaveis { get; set; }
        public decimal mensalidade { get; set; }
        public DateTime fimMatricula { get; set; }
    }
    private class AdminDTO
    {
        public string? nome { get; set; }
        public string? cpf { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
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

        if (root.admin != null)
        {
            var existingAdmin = UserController.Get("CPF", root.admin.cpf);

            if (existingAdmin == null)
            {
                var admin = new User
                {
                    Nome = root.admin.nome,
                    CPF = root.admin.cpf,
                    Email = root.admin.email,
                    DataCriacao = DateTime.Now,
                    DataUltimoAcesso = DateTime.Now,
                    Ativo = true,
                    AccessType = new AccessType { Level = 5 }
                };
                admin.Senha = PasswordHandler.HashPassword(root.admin.senha ?? "admin", admin.Salt);

                UserController.Register(admin);
            }
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
                

                AlunoController.Register(aluno);

                ResponsavelController.AddParentesco(responsavel, aluno, 1);
                TurmaController.AddAlunoToTurma(turma, aluno);

                ReceitaController.GerarCarneAluno(
                    aluno,
                    responsavel,
                    root.mensalidade,
                    root.fimMatricula
                );
            }
        }
    }
}