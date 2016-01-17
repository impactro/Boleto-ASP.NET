using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

public partial class PDF_Teste3 : System.Web.UI.Page
{
    // Veja algumas referencias:
    // http://www.c-sharpcorner.com/UploadFile/f2e803/basic-pdf-creation-using-itextsharp-part-i/
    // http://www.codeproject.com/Articles/686994/Create-Read-Advance-PDF-Report-using-iTextSharp-in
    // http://www.mikesdotnetting.com/article/88/itextsharp-drawing-shapes-and-graphics
    // http://stackoverflow.com/questions/4325151/adding-an-image-to-a-pdf-using-itextsharp-and-scale-it-properly
    // http://stackoverflow.com/questions/23598192/changing-font-size-in-itextsharp-table
    protected void Page_Load(object sender, EventArgs e)
    {
        // Limpa qualquer coisa já previamente renderizada!
        Response.ClearContent();

        // Altera o tipo de documento
        Response.ContentType = "application/pdf";

        // Novo PDF
        Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
        // Create an instance to the PDF file by creating an instance of the PDF 
        // Writer class using the document and the filestrem in the constructor.
        PdfWriter writer = PdfWriter.GetInstance(doc, Response.OutputStream);
        // comaça a gerar o PDF
        doc.Open();
        
        // Add a simple and wellknown phrase to the document in a flow layout manner
        doc.Add(new Paragraph("Hello World!"));

        // desenha uma linha
        PdfContentByte cb = writer.DirectContent;
        cb.MoveTo(doc.PageSize.Width / 2, doc.PageSize.Height / 2); // posiciona no centro da página
        cb.LineTo(doc.PageSize.Width, doc.PageSize.Height); // Linha diagonal até o canto superior
        cb.Stroke(); // Efetiva o desenho

        // cria uma linha separadora
        LineSeparator line = new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1);
        doc.Add(new Chunk(line));

        // Adiciona uma imagem
        // Atemção para conflitos com o nome 'Image' (existe dentro do ASP.Net, Web.UI, e no iTextsharp)
        System.Drawing.Bitmap img = new System.Drawing.Bitmap(MapPath("../BoletoNet/Imagens/237.gif")); // Isso é apenas um teste, não estou validando a existencia do arquivo
        Image pic = iTextSharp.text.Image.GetInstance(img, System.Drawing.Imaging.ImageFormat.Jpeg);
        doc.Add(pic);

        Font fontTable = FontFactory.GetFont("Arial", 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        PdfPTable table = new PdfPTable(5);
        table.SpacingBefore = 45f;
        table.TotalWidth = 216f;
        table.DefaultCell.Phrase = new Phrase() { Font = fontTable };

        for (int j = 0; j < 5; j++)
            table.AddCell(new Phrase("col" + j));

        table.HeaderRows = 1;
        for (int i = 1; i < 15; i++)
        {
            for (int k = 0; k < 5; k++)
            {
                Phrase p = new Phrase("cell(" + i + "," + k + ")");
                p.Font.Size = i;
                table.AddCell(p);
            }
        }

        doc.Add(table);

        // Close the document
        doc.Close();
        // Close the writer instance
        writer.Close();
        // Finaliza tudo! 
        Response.End();

        // Acho que estes são os elementos basicos que preciso para desenhar boletos 100% em PDF
        // O Teste2_BoletoHTML.aspx usa um conversor obsoleto de HTML/CSS para PDF
    }
}