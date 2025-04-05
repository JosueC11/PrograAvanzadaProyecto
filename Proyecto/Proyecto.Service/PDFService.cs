using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Proyecto.Data;

namespace Proyecto.Service
{
    public class PDFService
    {
        public string GeneratePDF(IEnumerable<tarea> tasks)
        {
            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "generatedarchives", "tasks.pdf");
            Directory.CreateDirectory(Path.GetDirectoryName(pdfPath));

            using (var writer = new PdfWriter(pdfPath))
            {
                using (var pdf = new PdfDocument(writer))
                {
                    var document = new Document(pdf);
                    document.Add(new Paragraph("List of failed tasks").SetFontSize(16));

                    foreach (var task in tasks)
                    {
                        document.Add(new Paragraph($"Título: {task.id}"));
                        document.Add(new Paragraph($"Descripción: {task.nombre}"));
                        document.Add(new Paragraph($"Prioridad: {task.prioridad}"));
                        document.Add(new Paragraph($"Fecha: {task.fecha_creada:dd/MM/yyyy}"));
                        document.Add(new Paragraph("---------------------------------------------------\n"));
                    }
                    document.Close();
                }
            }
            return pdfPath;
        }
    }
}
