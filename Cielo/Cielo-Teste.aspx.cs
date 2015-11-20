using System;
using Impactro.Cobranca;

public partial class Cielo_Teste : System.Web.UI.Page
{

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.Now;

            string cResult = Cielo.Teste(MapPath("requisicao-consulta.xml"), true);
            CieloTransacao trans = new CieloTransacao(cResult);

            txt.Text = trans.Text;
            lbl.Text = DateTime.Now.ToLongTimeString() + ": " + DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("##,##0ms ") +
               "<br/>ERRO: " + trans.ErroCodigo + " : " + trans.ErroMensagem +
               "<br/>TID: " + trans.TID + " Status: " + trans.Status.ToString() +
               "<br/>Autenticacao: " + trans.Autenticacao.Codigo +
               "<br/>Autorizacao: " + trans.Autorizacao.Codigo +
               "<br/>Captura: " + trans.Captura.Codigo +
               "<br/>PAN: " + trans.PAN +
               "<br/>UrlAutenticacao: " + string.Format("<a href='{0}'>{0}</a>", trans.UrlAutenticacao);

        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message;
        }
    }

}