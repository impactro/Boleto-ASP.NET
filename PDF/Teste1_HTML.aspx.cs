using System;
using System.IO;
using System.Web.UI;

// Referencias do iTexSharp (versão: 5.5.8.0 baixado: 21/12/2015)
// http://sourceforge.net/projects/itextsharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

public partial class PDF_Teste1 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ConverteAspx2Pdf();
    }

    // Versão Original em: https://social.msdn.microsoft.com/Forums/pt-BR/049133ce-2ce0-4b6e-9194-53b62e12ddbe/como-gerar-um-arquivo-pdf-a-partir-de-uma-pagina-aspx?forum=aspnetpt
    private void ConverteAspx2Pdf()
    {
        // Limpa qualquer coisa já previamente renderizada!
        Response.ClearContent();
        // Para fazer download direto é só descomentar a linha abaixo, caso contrario o PDF é exibido no navegador
        // Response.AddHeader("content-disposition", "attachment; filename=ExportacaoAspx2Pdf.pdf"); 
        
        // Altera o tipo de documento
        Response.ContentType = "application/pdf";

        // Prepara um buffer que conterá todo o HTML que é renderizado
        StringWriter stw = new StringWriter();
        HtmlTextWriter htextw = new HtmlTextWriter(stw);

        // Renderiza todo o HTML do ASPX no buffer (string)
        this.RenderControl(htextw);

        // Cria um novo documento PDF em branco
        Document document = new Document();
        
        // Define o local de saida (gravação) do PDF como o dispositivo de transmissão do ASP.Net que vai para o navegador
        PdfWriter.GetInstance(document, Response.OutputStream);
        document.Open();

        // Lê o HTML completo do buffer como uma String
        StringReader str = new StringReader(stw.ToString());

        // Chama um conversor interno de HTML para PDF
        HTMLWorker htmlworker = new HTMLWorker(document);
        // Transforma o HTML em PDF
        htmlworker.Parse(str);
        document.Close();

        // Transmite o HTML para o browser
        Response.Write(document);

        // Finaliza tudo! 
        Response.End();
    }
}