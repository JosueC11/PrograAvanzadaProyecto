using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Proyecto.Data;

namespace Proyecto.Service
{
    public class PDFService
    {
        public string GeneratePDF(IEnumerable<tarea> tasks)
        {
            string pdfPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "generatedarchives", "tasks.pdf");
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(pdfPath));

            using (var writer = new PdfWriter(pdfPath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf, PageSize.A4);
                    document.SetMargins(40, 40, 40, 40);

                    // Fuentes
                    var boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    var normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                    var italicFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

                    // Título principal
                    var title = new Paragraph("📋 Listado de Tareas Fallidas")
                        .SetFont(boldFont)
                        .SetFontSize(20)
                        .SetFontColor(ColorConstants.DARK_GRAY)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetMarginBottom(30);
                    document.Add(title);

                    foreach (var task in tasks)
                    {
                        var taskBlock = new Div()
                            .SetBorder(new SolidBorder(ColorConstants.LIGHT_GRAY, 1))
                            .SetPadding(10)
                            .SetMarginBottom(15);

                        taskBlock.Add(new Paragraph($"🆔 Título: {task.id}")
                            .SetFont(boldFont)
                            .SetFontSize(12)
                            .SetFontColor(ColorConstants.BLUE));

                        taskBlock.Add(new Paragraph($"📌 Descripción: {task.nombre}")
                            .SetFont(normalFont)
                            .SetFontSize(11));

                        taskBlock.Add(new Paragraph($"🚦 Prioridad: {task.prioridad}")
                            .SetFont(normalFont)
                            .SetFontSize(11)
                            .SetFontColor(ColorConstants.RED));

                        taskBlock.Add(new Paragraph($"📅 Fecha de creación: {task.fecha_creada:dd/MM/yyyy}")
                            .SetFont(italicFont)
                            .SetFontSize(11));

                        document.Add(taskBlock);
                    }

                    document.Close();
                }
            }
            return pdfPath;
        }
    }
}
