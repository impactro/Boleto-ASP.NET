<%@ WebHandler Language="C#" Class="BoletoImagem2" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using Impactro.Cobranca;

public class BoletoImagem2 : IHttpHandler, System.Web.SessionState.IRequiresSessionState // Obrigatório
{
    public bool IsReusable { get { return false; } }

    public void ProcessRequest(HttpContext context)
    {
        try
        {
            // Definição dos dados do cedente, que será comum para todos os boletos
            CedenteInfo Cedente = new CedenteInfo();
            Cedente.Cedente = "outro cedente!";
            Cedente.Banco = "237";
            Cedente.Agencia = "1234-5";
            Cedente.Conta = "123456-7";
            Cedente.Carteira = "06";
            Cedente.Modalidade = "11";

            // Instancia do 'Boleto', não o 'BoletoWeb', pois a ideia é renderizar imagem
            // O BoletoWeb usa a classe 'Boleto' para fazer todos os calculos, e depois desenha em html o boleto
            Boleto blt = new Boleto();
            blt.Carne = true; // Formato de Carne, neste exemplo será colocardo 3 boletos por página

            // Obtem o numero do boleto
            string cID = context.Request["id"];
            
            // Obtem o objeto do boleto da sessão ou do cache
            // Para usar sessão é necessário que na classe tenha a chamada a interface 'System.Web.SessionState.IRequiresSessionState'
            BoletoInfo Boleto = (BoletoInfo)context.Session[cID]; 
            //BoletoInfo Boleto = (BoletoInfo)context.Cache[cID];

            // Calcula os dados do boleto
            blt.MakeBoleto(Cedente, Boleto.Sacado, Boleto);

            // Obtem a imagem do boleto
            Bitmap img = blt.ImageBoleto();
            img.Save(context.Response.OutputStream, ImageFormat.Png);

            // Se tudo deu certo apenas muda o tipo de saida
            context.Response.ContentType = "image/png";
        }
        catch (Exception ex)
        {
            // em caso de erro mostra o motivo
            context.Response.Write(ex.Message + "\r\n" + ex.StackTrace);
            context.Response.ContentType = "text/pain";
        }
    }
}