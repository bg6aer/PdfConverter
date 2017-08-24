﻿using System;
using Application = Microsoft.Office.Interop.PowerPoint.Application;

namespace PdfConverter.Service.Converters
{
    public class PptxConverter : Converter
    {
        protected override void Convert()
        {
            Application app = new Microsoft.Office.Interop.PowerPoint.Application();
            PPTXDocument doc = null;

            do
            {
                try
                {
                    var document = this.ConversionQueue.Dequeue();

                    string newPath = "";

                    if (document.Name.EndsWith(".docx"))
                    {
                        var newName = document.Name.Replace(".docx", ".pdf");
                        newPath = document.FullPath.Replace(document.Name, newName);
                    }

                    doc = app.Documents.Open(document.FullPath);
                    doc.SaveAs2(newPath, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                    doc.Close();
                }
                catch (Exception)
                {
                    doc?.Close();
                }

            } while (this.ConversionQueue.Count > 0);

            app?.Quit();
            this.ConversionThread.Abort();
        }
    }
}
