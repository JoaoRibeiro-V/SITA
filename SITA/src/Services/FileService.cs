using Microsoft.AspNetCore.Components.Forms;

namespace SITA.src.Services
{
    public class FileService
    {
        private readonly string[] extensoesPermitidas =
        {
            ".pdf",
            ".png",
            ".jpg",
            ".jpeg"
        };

        public async Task<string?> SalvarComprovante(
            IBrowserFile arquivo)
        {
            if (arquivo == null)
                return null;

            var extensao =
                Path.GetExtension(arquivo.Name)
                    .ToLower();

            if (!extensoesPermitidas.Contains(extensao))
                return null;

            var nomeArquivo =
                $"{Guid.NewGuid()}{extensao}";

            var pasta =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "comprovantes");

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            var caminhoFisico =
                Path.Combine(pasta, nomeArquivo);

            await using FileStream fs =
                new(caminhoFisico, FileMode.Create);

            await arquivo
                .OpenReadStream(5 * 1024 * 1024)
                .CopyToAsync(fs);

            return Path.Combine(
                "comprovantes",
                nomeArquivo)
                .Replace("\\", "/");
        }

        public void RemoverArquivo(string? caminho)
        {
            if (string.IsNullOrWhiteSpace(caminho))
                return;

            var caminhoFisico =
                Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    caminho);

            if (File.Exists(caminhoFisico))
            {
                File.Delete(caminhoFisico);
            }
        }
    }
}