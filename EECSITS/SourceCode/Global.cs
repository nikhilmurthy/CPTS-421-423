using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace NewITS
{
    // contains global data and methods
    class Global
    {
        public const int usrNotLogged = 0;
        public const int usrNormal = 1;
        public const int usrAdmin = 2;
        public static string loginName;
        public static int loginState;

        // Exports data from the datagrid to a newly created CSV file
        // Filename is generated using the passed name and current timestamp
        // Each field in the data grid is written out with a tab seperator
        // Returns:
        //      The name of the file created -- if no errors and the datagrid is not empty
        //      An empty string -- if data grid is empty
        //      A null string -- if any error is encountered


        public static string exportToCsvFile(DataGridView dGV, string filename, string filter)
        {
            string sHeaders = "";
            byte[] buffer;


            Stream myStream;
            string newFileName = "ExportFiles\\" + filename + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".csv";

            if (!Directory.Exists("ExportFiles"))
                 Directory.CreateDirectory("ExportFiles");

            if (dGV.RowCount == 0)
                return "";


            myStream = new FileStream(newFileName, FileMode.Create);
            if (myStream == null)
                return null;

            // Write Report Heading

            sHeaders = "Report: " + "\t" + filename  + "\r\n";
            sHeaders += "Date:" + "\t" + DateTime.Now.ToString("yyyyMMdd-HHmmssfff") + "\r\n\n";

            buffer = System.Text.Encoding.ASCII.GetBytes(sHeaders);
            myStream.Write(buffer, 0, buffer.Length);

            // write the filter

            buffer = System.Text.Encoding.ASCII.GetBytes(filter);
            myStream.Write(buffer, 0, buffer.Length);

            // Write Column Headings
            sHeaders = "";
            for (int j = 0; j < dGV.Columns.Count; j++)
                if (dGV.Columns[j].Visible)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            sHeaders +=  "\r\n";
            buffer = System.Text.Encoding.ASCII.GetBytes (sHeaders);
            myStream.Write(buffer, 0, buffer.Length);

            // Export Row Data

            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    if (dGV.Rows[i].Cells[j].Visible)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";

                stLine +=  "\r\n";
                buffer = System.Text.Encoding.ASCII.GetBytes(stLine);
                myStream.Write(buffer, 0, buffer.Length);
            }

            myStream.Close();
            return newFileName;
        }  

        // Exports data from the datagrid to a newly created PDF file
        // Filename is generated using the passed name and current timestamp
        // iText - an open source library is used to provide this functionality
        // Returns:
        //      The name of the file created -- if no errors and the datagrid is not empty
        //      An empty string -- if data grid is empty
        //      A null string -- if any error is encountered

         public static string exportToPdfFile(DataGridView dGV, string name, string filter)
        {
 
            Stream myStream;
            string newFileName = "ExportFiles\\" + name + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
 
            if (!Directory.Exists("ExportFiles"))
                 Directory.CreateDirectory("ExportFiles");

            if (dGV.RowCount == 0)
                return "";

            myStream = new FileStream(newFileName, FileMode.Create);
            if (myStream == null)
                return null;

             //Using iTextSharp - data structures

            int colCount = 0; 

            foreach (DataGridViewColumn column in dGV.Columns)
            {
                if (column.Visible)
                    colCount++;
            }

            PdfPTable pdfTbl = new PdfPTable(colCount);

            pdfTbl.DefaultCell.Padding = 3;
            pdfTbl.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTbl.DefaultCell.BorderWidth = 1;

            //Set the column widths 

            int[] widths = new int[colCount];

            //Adding Header row
             int x = 0;
            foreach (DataGridViewColumn column in dGV.Columns)
            {
                if (column.Visible)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdfTbl.AddCell(cell);
                    widths[x++] = column.Width;
                    colCount++;
                }
            }
            pdfTbl.SetWidths(widths);
            //Add the data rows
            foreach (DataGridViewRow row in dGV.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible)
                    {
                        PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Value.ToString()));
                        pdfTbl.AddCell(pdfCell);
                    }
                }
            }

            //Create and write the document

            Document pdfDoc = new Document(PageSize.A2, 100f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, myStream);
            pdfDoc.Open();
            pdfDoc.AddTitle(newFileName);
            pdfDoc.Add(new Paragraph(newFileName));
            pdfDoc.Add(new Paragraph(" "));
            pdfDoc.Add(new Paragraph(filter));
            pdfDoc.Add(pdfTbl);
            pdfDoc.Close();

            myStream.Close();
            return newFileName;
        }  

    }
}
