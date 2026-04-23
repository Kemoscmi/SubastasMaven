using Maven.Application.DTOs.Reportes;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Maven.Web.Documents
{
    public class ReporteCategoriasDocument : IDocument
    {
        private readonly ICollection<ReporteCategoriasDTO> _items;
        private readonly DateTime _fechaInicio;
        private readonly DateTime _fechaFin;
        private readonly string? _graficoBase64;

        public ReporteCategoriasDocument(
            ICollection<ReporteCategoriasDTO> items,
            DateTime fechaInicio,
            DateTime fechaFin,
            string? graficoBase64)
        {
            _items = items;
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            _graficoBase64 = graficoBase64;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(28);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Arial"));

                page.Header().Column(col =>
                {
                    col.Item().Background("#1F2937").Padding(14).Column(header =>
                    {
                        header.Item().Text("MAVEN")
                            .FontSize(22)
                            .Bold()
                            .FontColor(Colors.White);

                        header.Item().Text("Reporte de Subastas por Categoría")
                            .FontSize(14)
                            .SemiBold()
                            .FontColor("#E5E7EB");
                    });

                    col.Item().PaddingTop(8).Row(row =>
                    {
                        row.RelativeItem().Text($"Periodo: {_fechaInicio:dd/MM/yyyy} - {_fechaFin:dd/MM/yyyy}")
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken2);

                        row.ConstantItem(180).AlignRight().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}")
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken2);
                    });

                    col.Item().PaddingTop(6).LineHorizontal(1).LineColor("#D1D5DB");
                });

                page.Content().PaddingVertical(14).Column(col =>
                {
                    col.Item().Background("#F3F4F6").Padding(10).Column(box =>
                    {
                        box.Item().Text("Resumen").Bold();
                        box.Item().Text($"Categorías incluidas: {_items.Count}");
                        box.Item().Text($"Total de subastas contabilizadas: {_items.Sum(x => x.CantidadSubastas)}");
                        box.Item().Text($"Total de subastas finalizadas: {_items.Sum(x => x.CantidadFinalizadas)}");
                    });

                    if (!string.IsNullOrWhiteSpace(_graficoBase64))
                    {
                        try
                        {
                            var base64Data = _graficoBase64.Contains(",")
                                ? _graficoBase64.Split(',')[1]
                                : _graficoBase64;

                            var imageBytes = Convert.FromBase64String(base64Data);

                            col.Item().PaddingTop(15).Text("Gráfico").Bold().FontSize(12);
                            col.Item().PaddingTop(8).Border(1).BorderColor("#E5E7EB").Padding(8)
                                .Image(imageBytes)
                                .FitWidth();
                        }
                        catch
                        {
                            col.Item().PaddingTop(15).Text("No fue posible cargar el gráfico en el PDF.")
                                .FontColor(Colors.Red.Darken1);
                        }
                    }

                    col.Item().PaddingTop(12).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.ConstantColumn(120);
                            columns.ConstantColumn(120);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCell).Text("Categoría");
                            header.Cell().Element(HeaderCell).AlignCenter().Text("Subastas");
                            header.Cell().Element(HeaderCell).AlignCenter().Text("Finalizadas");
                        });

                        int index = 0;
                        foreach (var item in _items)
                        {
                            var background = index % 2 == 0 ? "#FFFFFF" : "#F9FAFB";

                            table.Cell().Element(c => BodyCell(c, background)).Text(item.Categoria);
                            table.Cell().Element(c => BodyCell(c, background).AlignCenter()).Text(item.CantidadSubastas.ToString());
                            table.Cell().Element(c => BodyCell(c, background).AlignCenter()).Text(item.CantidadFinalizadas.ToString());

                            index++;
                        }
                    });
                });

                page.Footer().PaddingTop(10).Column(col =>
                {
                    col.Item().LineHorizontal(1).LineColor("#D1D5DB");
                    col.Item().PaddingTop(6).AlignCenter().Text(x =>
                    {
                        x.Span("Página ");
                        x.CurrentPageNumber();
                        x.Span(" de ");
                        x.TotalPages();
                    });
                });
            });
        }

        static IContainer HeaderCell(IContainer container)
        {
            return container
                .Background("#111827")
                .Border(1)
                .BorderColor("#111827")
                .PaddingVertical(8)
                .PaddingHorizontal(6)
                .DefaultTextStyle(x => x.FontColor(Colors.White).Bold());
        }

        static IContainer BodyCell(IContainer container, string background)
        {
            return container
                .Background(background)
                .BorderBottom(1)
                .BorderColor("#E5E7EB")
                .PaddingVertical(7)
                .PaddingHorizontal(6);
        }
    }
}