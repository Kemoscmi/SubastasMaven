using Maven.Application.DTOs.Reportes;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Maven.Web.Documents
{
    public class ReporteSubastasFinalizadasDocument : IDocument
    {
        private readonly ICollection<ReporteSubastasFinalizadasDTO> _items;
        private readonly DateTime _fechaInicio;
        private readonly DateTime _fechaFin;

        public ReporteSubastasFinalizadasDocument(
            ICollection<ReporteSubastasFinalizadasDTO> items,
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            _items = items;
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
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

                        header.Item().Text("Reporte de Subastas Finalizadas")
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
                    col.Item().PaddingBottom(10).Row(row =>
                    {
                        row.RelativeItem().Background("#F3F4F6").Padding(10).Column(box =>
                        {
                            box.Item().Text("Resumen")
                                .Bold()
                                .FontColor("#111827");

                            box.Item().Text($"Total de subastas incluidas: {_items.Count}")
                                .FontColor("#374151");
                        });
                    });

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(45);
                            columns.RelativeColumn(2.4f);
                            columns.RelativeColumn(2f);
                            columns.ConstantColumn(90);
                            columns.ConstantColumn(75);
                            columns.ConstantColumn(95);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("ID");
                            header.Cell().Element(HeaderCellStyle).Text("Objeto");
                            header.Cell().Element(HeaderCellStyle).Text("Ganador");
                            header.Cell().Element(HeaderCellStyle).Text("Monto final");
                            header.Cell().Element(HeaderCellStyle).Text("Cierre");
                            header.Cell().Element(HeaderCellStyle).Text("Estado pago");
                        });

                        int index = 0;

                        foreach (var item in _items)
                        {
                            var backgroundColor = index % 2 == 0 ? "#FFFFFF" : "#F9FAFB";

                            table.Cell().Element(c => BodyCellStyle(c, backgroundColor))
                                .Text(item.SubastaId.ToString());

                            table.Cell().Element(c => BodyCellStyle(c, backgroundColor))
                                .Text(item.ObjetoSubastado);

                            table.Cell().Element(c => BodyCellStyle(c, backgroundColor))
                                .Text(item.UsuarioGanador);

                            table.Cell().Element(c => BodyCellStyle(c, backgroundColor))
                                .Text(item.MontoFinal.HasValue ? $"₡{item.MontoFinal.Value:N0}" : "Sin monto");

                            table.Cell().Element(c => BodyCellStyle(c, backgroundColor))
                                .Text(item.FechaCierre.ToString("dd/MM/yyyy"));

                            table.Cell().Element(c => EstadoPagoCellStyle(c, item.EstadoPago))
                             .Text(NormalizarEstadoPago(item.EstadoPago));

                            index++;
                        }
                    });
                });

                page.Footer().PaddingTop(10).Column(col =>
                {
                    col.Item().LineHorizontal(1).LineColor("#D1D5DB");

                    col.Item()
                        .PaddingTop(6)
                        .AlignCenter()
                        .DefaultTextStyle(x => x.FontSize(9).FontColor(Colors.Grey.Darken1))
                        .Text(x =>
                        {
                            x.Span("Página ");
                            x.CurrentPageNumber();
                            x.Span(" de ");
                            x.TotalPages();
                        });
                });
            });
        }

        static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Background("#111827")
                .Border(1)
                .BorderColor("#111827")
                .PaddingVertical(8)
                .PaddingHorizontal(6)
                .AlignMiddle()
                .DefaultTextStyle(x => x.FontColor(Colors.White).Bold().FontSize(10));
        }

        static IContainer BodyCellStyle(IContainer container, string backgroundColor)
        {
            return container
                .Background(backgroundColor)
                .BorderBottom(1)
                .BorderColor("#E5E7EB")
                .PaddingVertical(7)
                .PaddingHorizontal(6)
                .AlignMiddle();
        }

        static IContainer EstadoPagoCellStyle(IContainer container, string estadoPago)
        {
            var texto = (estadoPago ?? "Sin pago").Trim().ToUpper();

            string fondo;
            string colorTexto;

            switch (texto)
            {
                case "CONFIRMADO":
                    fondo = "#DCFCE7";
                    colorTexto = "#166534";
                    break;

                case "PENDIENTE":
                    fondo = "#FEF3C7";
                    colorTexto = "#92400E";
                    break;

                default:
                    fondo = "#E5E7EB";
                    colorTexto = "#374151";
                    break;
            }

            return container
                .Background(fondo)
                .BorderBottom(1)
                .BorderColor("#E5E7EB")
                .PaddingVertical(7)
                .PaddingHorizontal(6)
                .AlignCenter()
                .AlignMiddle()
                .DefaultTextStyle(x => x.FontSize(9).SemiBold().FontColor(colorTexto));
        }

        static string NormalizarEstadoPago(string estadoPago)
        {
            var texto = (estadoPago ?? "Sin pago").Trim().ToUpper();

            return texto switch
            {
                "CONFIRMADO" => "CONFIRMADO",
                "PENDIENTE" => "PENDIENTE",
                _ => "SIN PAGO"
            };
        }
    }
}