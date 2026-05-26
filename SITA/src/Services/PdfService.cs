using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SITA.src.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Colors = QuestPDF.Helpers.Colors;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace SITA.src.Services
{
    /*
     * PdfService
     * -----------------------------------------
     * Centraliza a geração de todos os PDFs do SITA.
     * Usa QuestPDF (licença Community — gratuita para projetos não comerciais).
     *
     * PDFs gerados:
     *   1. FichaAluno      — ficha completa do aluno com campos de assinatura
     *   2. RelatorioReceita — relatório de uma receita individual
     *   3. RelatorioDespesa — relatório de uma despesa individual
     *   4. RelatorioGeral   — resumo financeiro geral (saldo, receitas, despesas)
     */
    public static class PdfService
    {
        // Paleta da escola
        private static readonly string CorPrimaria = "#3650a1";
        private static readonly string CorSecundaria = "#f0f4ff";

        public static byte[] GerarFichaAluno(Aluno aluno, string nomeEscola = "SITA — Sistema de Gestão Escolar")
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(CabecalhoEscola(nomeEscola, "FICHA DE MATRÍCULA DO ALUNO"));

                    page.Content().Column(col =>
                    {
                        col.Spacing(8);

                        // Dados pessoais
                        col.Item().Element(SecaoTitulo("DADOS PESSOAIS"));
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(); });
                            CelulaTabela(t, "Nome Completo", aluno.Nome ?? "-");
                            CelulaTabela(t, "RA", aluno.RA ?? "-");
                            CelulaTabela(t, "Data de Nascimento", aluno.DataNascimento?.ToString("dd/MM/yyyy") ?? "-");
                            CelulaTabela(t, "Idade", $"{aluno.CalcularIdade()} anos");
                            CelulaTabela(t, "Religião", aluno.Religiao ?? "-");
                            CelulaTabela(t, "Turma", aluno.Turma?.Nome ?? "Sem turma");
                            CelulaTabela(t, "Data de Cadastro", aluno.DataCadastro.ToString("dd/MM/yyyy HH:mm"));
                        });

                        // Responsáveis
                        col.Item().Element(SecaoTitulo("RESPONSÁVEIS"));
                        if (aluno.Responsaveis.Any())
                        {
                            foreach (var r in aluno.Responsaveis)
                            {
                                col.Item().Table(t =>
                                {
                                    t.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(); });
                                    CelulaTabela(t, "Nome", r.Nome ?? "-");
                                    CelulaTabela(t, "CPF", r.CPF ?? "-");
                                    CelulaTabela(t, "E-mail", r.Email ?? "-");
                                    CelulaTabela(t, "Telefone", r.Telefone ?? "-");
                                });
                            }
                        }
                        else
                        {
                            col.Item().Text("Nenhum responsável cadastrado.").Italic();
                        }

                        // Contatos de emergência
                        col.Item().Element(SecaoTitulo("CONTATOS DE EMERGÊNCIA"));
                        if (aluno.ContatosEmergencia.Any())
                        {
                            col.Item().Table(t =>
                            {
                                t.ColumnsDefinition(c =>
                                {
                                    c.RelativeColumn(3);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                    c.RelativeColumn(2);
                                });

                                // Cabeçalho
                                t.Header(h =>
                                {
                                    h.Cell().Background(CorPrimaria).Padding(4).Text("Nome").FontColor(Colors.White).Bold();
                                    h.Cell().Background(CorPrimaria).Padding(4).Text("CPF").FontColor(Colors.White).Bold();
                                    h.Cell().Background(CorPrimaria).Padding(4).Text("Parentesco").FontColor(Colors.White).Bold();
                                    h.Cell().Background(CorPrimaria).Padding(4).Text("Telefone").FontColor(Colors.White).Bold();
                                });

                                foreach (var c in aluno.ContatosEmergencia)
                                {
                                    t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4).Text(c.Nome);
                                    t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4).Text(c.CPF);
                                    t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4).Text(c.GrauParentesco);
                                    t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4).Text(c.Telefone);
                                }
                            });
                        }
                        else
                        {
                            col.Item().Text("Nenhum contato de emergência cadastrado.").Italic();
                        }

                        // Saúde
                        col.Item().Element(SecaoTitulo("SAÚDE E EMERGÊNCIA"));
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(); });
                            CelulaTabela(t, "Pediatra", aluno.NomePediatra ?? "-");
                            CelulaTabela(t, "Orientações de Emergência", aluno.OrientacoesEmergencia ?? "-");
                            CelulaTabela(t, "Alergias", aluno.Alergias?.Any() == true ? string.Join(", ", aluno.Alergias) : "Nenhuma");
                            CelulaTabela(t, "Condições Especiais", aluno.CondicoesEspeciais?.Any() == true ? string.Join(", ", aluno.CondicoesEspeciais) : "Nenhuma");
                        });

                        // Assinaturas
                        col.Item().PaddingTop(30).Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().BorderBottom(1).BorderColor(Colors.Black).Height(30);
                                c.Item().AlignCenter().Text("Assinatura do Responsável").FontSize(9);
                            });

                            row.ConstantItem(40);

                            row.RelativeItem().Column(c =>
                            {
                                c.Item().BorderBottom(1).BorderColor(Colors.Black).Height(30);
                                c.Item().AlignCenter().Text("Assinatura da Diretoria").FontSize(9);
                            });
                        });

                        col.Item().PaddingTop(5)
                            .AlignCenter()
                            .Text($"Documento gerado em {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(8).FontColor(Colors.Grey.Medium);
                    });

                    page.Footer().AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ").FontSize(8);
                            x.CurrentPageNumber().FontSize(8);
                            x.Span(" de ").FontSize(8);
                            x.TotalPages().FontSize(8);
                        });
                });
            }).GeneratePdf();
        }

        public static byte[] GerarRelatorioReceita(Receita receita, string nomeEscola = "SITA — Sistema de Gestão Escolar")
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(CabecalhoEscola(nomeEscola, "COMPROVANTE DE RECEITA"));

                    page.Content().Column(col =>
                    {
                        col.Spacing(8);

                        col.Item().Element(SecaoTitulo("DADOS DA RECEITA"));
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(); });
                            CelulaTabela(t, "Descrição", receita.Descricao ?? "-");
                            CelulaTabela(t, "Tipo", receita.GetTypeTexto());
                            CelulaTabela(t, "Valor", $"R$ {receita.GetValorTotal():N2}");
                            CelulaTabela(t, "Status", receita.GetStatusTexto());
                            CelulaTabela(t, "Vencimento", receita.DataVencimento.ToString("dd/MM/yyyy"));
                            CelulaTabela(t, "Emissão", receita.DataEmissao.ToString("dd/MM/yyyy HH:mm"));
                            CelulaTabela(t, "Aluno", receita.Aluno?.Nome ?? "-");
                            CelulaTabela(t, "Responsável", receita.Responsavel?.Nome ?? "-");
                            CelulaTabela(t, "Observação", receita.Observacao ?? "-");
                        });

                        if (receita.Descontos.Any())
                        {
                            col.Item().Element(SecaoTitulo("DESCONTOS"));
                            foreach (var d in receita.Descontos)
                                col.Item().Text($"• {d.Descricao}: R$ {d.GetValorFinal(receita.Valor):N2}");
                        }

                        if (receita.Bonus.Any())
                        {
                            col.Item().Element(SecaoTitulo("ACRÉSCIMOS"));
                            foreach (var b in receita.Bonus)
                                col.Item().Text($"• {b.Descricao}: R$ {b.GetValorFinal(receita.Valor):N2}");
                        }

                        col.Item().PaddingTop(20)
                            .Background(CorSecundaria)
                            .Padding(10)
                            .Text($"TOTAL: R$ {receita.GetValorTotal():N2}")
                            .FontSize(14).Bold();

                        col.Item().PaddingTop(30).Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().BorderBottom(1).BorderColor(Colors.Black).Height(30);
                                c.Item().AlignCenter().Text("Assinatura do Responsável").FontSize(9);
                            });
                            row.ConstantItem(40);
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().BorderBottom(1).BorderColor(Colors.Black).Height(30);
                                c.Item().AlignCenter().Text("Assinatura da Diretoria").FontSize(9);
                            });
                        });

                        col.Item().PaddingTop(5).AlignCenter()
                            .Text($"Documento gerado em {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(8).FontColor(Colors.Grey.Medium);
                    });
                });
            }).GeneratePdf();
        }

        public static byte[] GerarRelatorioDespesa(Despesa despesa, string nomeEscola = "SITA — Sistema de Gestão Escolar")
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(CabecalhoEscola(nomeEscola, "COMPROVANTE DE DESPESA"));

                    page.Content().Column(col =>
                    {
                        col.Spacing(8);

                        col.Item().Element(SecaoTitulo("DADOS DA DESPESA"));
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(); });
                            CelulaTabela(t, "Descrição", despesa.Descricao ?? "-");
                            CelulaTabela(t, "Categoria", despesa.Categoria ?? "-");
                            CelulaTabela(t, "Valor", $"R$ {despesa.GetValorTotal():N2}");
                            CelulaTabela(t, "Status", despesa.GetStatusTexto());
                            CelulaTabela(t, "Vencimento", despesa.DataVencimento.ToString("dd/MM/yyyy"));
                            CelulaTabela(t, "Emissão", despesa.DataEmissao.ToString("dd/MM/yyyy HH:mm"));
                            CelulaTabela(t, "Fornecedor", despesa.Fornecedor ?? "-");
                            CelulaTabela(t, "Quem Pagou", despesa.QuemPagou ?? "-");
                            CelulaTabela(t, "CNPJ Escola", despesa.CnpjEscola ?? "-");
                            CelulaTabela(t, "CNPJ Fornecedor", despesa.CnpjFornecedor ?? "-");
                            CelulaTabela(t, "Nº Nota Fiscal", despesa.NumeroNotaFiscal ?? "-");
                            CelulaTabela(t, "Observação", despesa.Observacao ?? "-");
                        });

                        col.Item().PaddingTop(20)
                            .Background("#fff0f0")
                            .Padding(10)
                            .Text($"TOTAL: R$ {despesa.GetValorTotal():N2}")
                            .FontSize(14).Bold();

                        col.Item().PaddingTop(30).Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().BorderBottom(1).BorderColor(Colors.Black).Height(30);
                                c.Item().AlignCenter().Text("Assinatura do Responsável").FontSize(9);
                            });
                            row.ConstantItem(40);
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().BorderBottom(1).BorderColor(Colors.Black).Height(30);
                                c.Item().AlignCenter().Text("Assinatura da Diretoria").FontSize(9);
                            });
                        });

                        col.Item().PaddingTop(5).AlignCenter()
                            .Text($"Documento gerado em {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(8).FontColor(Colors.Grey.Medium);
                    });
                });
            }).GeneratePdf();
        }

        public static byte[] GerarRelatorioGeral(
            List<Receita> receitas,
            List<Despesa> despesas,
            string nomeEscola = "SITA — Sistema de Gestão Escolar",
            DateTime? dataInicio = null,
            DateTime? dataFim = null)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var inicio = dataInicio ?? DateTime.MinValue;
            var fim = dataFim ?? DateTime.MaxValue;

            var receitasFiltradas = receitas.Where(r => r.DataReferente >= inicio && r.DataReferente <= fim).ToList();
            var despesasFiltradas = despesas.Where(d => d.DataReferente >= inicio && d.DataReferente <= fim).ToList();

            float totalReceitas = receitasFiltradas.Sum(r => r.GetValorTotal());
            float totalDespesas = despesasFiltradas.Sum(d => d.GetValorTotal());
            float saldoLiquido = totalReceitas - totalDespesas;

            float receitasPagas = receitasFiltradas.Where(r => r.IsPago()).Sum(r => r.GetValorTotal());
            float receitasPendentes = receitasFiltradas.Where(r => !r.IsPago()).Sum(r => r.GetValorTotal());
            float despesasPagas = despesasFiltradas.Where(d => d.IsPago()).Sum(d => d.GetValorTotal());
            float despesasPendentes = despesasFiltradas.Where(d => !d.IsPago()).Sum(d => d.GetValorTotal());

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(CabecalhoEscola(nomeEscola, "RELATÓRIO FINANCEIRO GERAL"));

                    page.Content().Column(col =>
                    {
                        col.Spacing(10);

                        // Período
                        string periodo = dataInicio.HasValue || dataFim.HasValue
                            ? $"{dataInicio?.ToString("dd/MM/yyyy") ?? "início"} a {dataFim?.ToString("dd/MM/yyyy") ?? "hoje"}"
                            : "Todos os períodos";
                        col.Item().Text($"Período: {periodo}").Italic();

                        // Resumo executivo
                        col.Item().Element(SecaoTitulo("RESUMO EXECUTIVO"));
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c => { c.RelativeColumn(); c.RelativeColumn(); });
                            CelulaTabela(t, "Total de Receitas", $"R$ {totalReceitas:N2}");
                            CelulaTabela(t, "Total de Despesas", $"R$ {totalDespesas:N2}");
                            CelulaTabela(t, "Receitas Pagas", $"R$ {receitasPagas:N2}");
                            CelulaTabela(t, "Receitas Pendentes", $"R$ {receitasPendentes:N2}");
                            CelulaTabela(t, "Despesas Pagas", $"R$ {despesasPagas:N2}");
                            CelulaTabela(t, "Despesas Pendentes", $"R$ {despesasPendentes:N2}");
                        });

                        // Saldo
                        col.Item()
                            .Background(saldoLiquido >= 0 ? "#e8f5e9" : "#ffebee")
                            .Padding(10)
                            .Text($"SALDO LÍQUIDO: R$ {saldoLiquido:N2}")
                            .FontSize(14).Bold()
                            .FontColor(saldoLiquido >= 0 ? "#2e7d32" : "#c62828");

                        // Tabela de receitas
                        col.Item().Element(SecaoTitulo("RECEITAS"));
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3);
                                c.RelativeColumn(2);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });

                            t.Header(h =>
                            {
                                h.Cell().Background(CorPrimaria).Padding(4).Text("Descrição").FontColor(Colors.White).Bold();
                                h.Cell().Background(CorPrimaria).Padding(4).Text("Aluno").FontColor(Colors.White).Bold();
                                h.Cell().Background(CorPrimaria).Padding(4).Text("Valor").FontColor(Colors.White).Bold();
                                h.Cell().Background(CorPrimaria).Padding(4).Text("Status").FontColor(Colors.White).Bold();
                            });

                            foreach (var r in receitasFiltradas.OrderByDescending(x => x.DataVencimento))
                            {
                                t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(3).Text(r.Descricao ?? "-").FontSize(9);
                                t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(3).Text(r.Aluno?.Nome ?? "-").FontSize(9);
                                t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(3).Text($"R$ {r.GetValorTotal():N2}").FontSize(9);
                                t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(3).Text(r.GetStatusTexto()).FontSize(9);
                            }
                        });

                        // Tabela de despesas
                        col.Item().Element(SecaoTitulo("DESPESAS"));
                        col.Item().Table(t =>
                        {
                            t.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn(3);
                                c.RelativeColumn(2);
                                c.RelativeColumn(1);
                                c.RelativeColumn(1);
                            });

                            t.Header(h =>
                            {
                                h.Cell().Background("#c62828").Padding(4).Text("Descrição").FontColor(Colors.White).Bold();
                                h.Cell().Background("#c62828").Padding(4).Text("Categoria").FontColor(Colors.White).Bold();
                                h.Cell().Background("#c62828").Padding(4).Text("Valor").FontColor(Colors.White).Bold();
                                h.Cell().Background("#c62828").Padding(4).Text("Status").FontColor(Colors.White).Bold();
                            });

                            foreach (var d in despesasFiltradas.OrderByDescending(x => x.DataVencimento))
                            {
                                t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(3).Text(d.Descricao ?? "-").FontSize(9);
                                t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(3).Text(d.Categoria ?? "-").FontSize(9);
                                t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(3).Text($"R$ {d.GetValorTotal():N2}").FontSize(9);
                                t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(3).Text(d.GetStatusTexto()).FontSize(9);
                            }
                        });

                        col.Item().PaddingTop(5).AlignCenter()
                            .Text($"Documento gerado em {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(8).FontColor(Colors.Grey.Medium);
                    });

                    page.Footer().AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ").FontSize(8);
                            x.CurrentPageNumber().FontSize(8);
                            x.Span(" de ").FontSize(8);
                            x.TotalPages().FontSize(8);
                        });
                });
            }).GeneratePdf();
        }
        private static Action<IContainer> CabecalhoEscola(string nomeEscola, string titulo)
        {
            return container =>
            {
                container.Column(col =>
                {
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text(nomeEscola).FontSize(14).Bold().FontColor(CorPrimaria);
                            c.Item().Text(titulo).FontSize(11).FontColor(Colors.Grey.Darken1);
                        });
                    });
                    col.Item().PaddingTop(4).BorderBottom(2).BorderColor(CorPrimaria).Height(0);
                    col.Item().Height(8);
                });
            };
        }

        private static Action<IContainer> SecaoTitulo(string titulo)
        {
            return container =>
            {
                container
                    .Background(CorSecundaria)
                    .Padding(6)
                    .Text(titulo)
                    .Bold()
                    .FontColor(CorPrimaria);
            };
        }

        private static void CelulaTabela(TableDescriptor t, string label, string valor)
        {
            t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                .Text(label).Bold().FontSize(9);
            t.Cell().BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(4)
                .Text(valor).FontSize(9);
        }
    }
}