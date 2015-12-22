using System;
using System.IO;
using System.Web.UI;

// Referencia para o meu componente
// No ASPX a segunda linha é a referencia do WebControl
using Impactro.Cobranca;
// Versão minima compativel: impactro.cobranca.dll 2.15.12.21 - foi necessário alterar algumas TAGs do Render HTML

// Referencias do iTexSharp (versão: 5.5.8.0 baixado: 21/12/2015)
// http://sourceforge.net/projects/itextsharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Collections.Generic;

// Este exemplo é baseado no exemplo PDF/teste1.aspx e BoeltoNET/BoletoCS.aspx
public partial class PDF_Teste2 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do cedente - QUEM RECEBE / EMITE
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Exemplo de empresa cedente";
        Cedente.Banco = "237-";
        Cedente.Agencia = "1234";       // use somente "-" para separa o código da agencia e digito
        Cedente.Conta = "56789-1";      // use somente "-" para separa o código da conta e digito
        Cedente.Carteira = "6";
        Cedente.CNPJ = "12.345.678/0001-12";
        Cedente.Endereco = "Rua Sei lá aonde, 123 - Centro, São Paulo/SP";

        // Definição dos dados do sacado -  QUEM PAGA
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.SacadoCodigo = "123"; // Código interno de controle
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";

        // Definiçào dos dados do boleto
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "123400";
        Boleto.NumeroDocumento = "123400";
        Boleto.ValorDocumento = 423.45;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = new DateTime(2006, 5, 31);
        Boleto.ParcelaNumero = 2;
        Boleto.ParcelaTotal = 10;
        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // Monta as informações do boleto
        bltPag.ImageType = Impactro.WebControls.BoletoImageType.gif;
        // O Conversor padrão só lê imagens absolutas, então as imagens devem ser geradas co o nome do host
        // veja mais em: http://stackoverflow.com/questions/924996/itextsharp-htmlworker-img-not-found-404
        bltPag.ImagePath = "http://" + Request.Url.Host + ":" + Request.Url.Port + "/BoletoNet/imagens/";
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);

        // O Calculo acontece no RENDER, que neste caso é chamado internamente ao converter para PDF
        // Comente/descomente a linha abaixo para ver em HTML ou em PDF!
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
        Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);

        // Define o local de saida (gravação) do PDF como o dispositivo de transmissão do ASP.Net que vai para o navegador
        PdfWriter.GetInstance(document, Response.OutputStream);
        document.Open();

        // Os estilos do boleto devem ser definidos desta forma
        // http://stackoverflow.com/questions/8414637/itextsharp-htmlworker-parsehtml-tablestyle-and-pdfstamper
        
        StyleSheet styles = new StyleSheet();

        Dictionary<string, string> BolCell = new Dictionary<string, string>();
        //styles.LoadStyle("BolCell", "size", "7px");
        BolCell.Add("size", "7pt");
        //BolCell.Add("face", "verdana");
        styles.LoadStyle("BolCell", BolCell);

        Dictionary<string, string> BolField = new Dictionary<string, string>();
        styles.LoadStyle("BolField", "size", "12px");
        //BolField.Add("weight", "bold");
        BolField.Add("size", "9pt");
        //BolField.Add("face", "arial");
        styles.LoadStyle("BolField", BolField);

        // Lê o HTML completo do buffer como uma String
        StringReader str = new StringReader(stw.ToString());

        // Chama um conversor interno de HTML para PDF
        HTMLWorker htmlworker = new HTMLWorker(document);
        // Transforma o HTML em PDF
        htmlworker.SetStyleSheet(styles);

        htmlworker.Parse(str);
        document.Close();

        // Transmite o HTML para o browser
        Response.Write(document);

        // Finaliza tudo! 
        Response.End();
    }
}