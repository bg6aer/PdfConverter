﻿using System;
using Microsoft.Office.Interop.Word;

namespace PdfConverter.Service.Converters
{
    public class XslxConverter : Converter
    {
        protected override void Convert()
        {
            https://code.msdn.microsoft.com/windowsapps/Convert-Power-Point-c88aed9d
            SautinSoft.UseOffice u = new SautinSoft.UseOffice();

            u.InitExcel();


            Application app = new Microsoft.Office.Interop.Word.Application();
            Document doc = null;

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
