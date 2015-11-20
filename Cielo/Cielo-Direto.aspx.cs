using System;
using Impactro.Cobranca;

public partial class Cielo_Direto : System.Web.UI.Page
{
    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.Now;

            CieloTransacao trans = Cielo.TransacaoDireta( Cielo.testeLojaNumero, Cielo.testeLojaChave,
                Int32.Parse(txtPedido.Text),
                Double.Parse(txtValor.Text),
                0,
                DateTime.Now,
                (CieloBandeiras)Enum.Parse(typeof(CieloBandeiras), ddlCartao.SelectedValue, true),
                (chkDebito.Checked ? CieloProdutos.Debito : CieloProdutos.Parcelado),
                Int32.Parse(txtParcelas.Text),
                txtCartao.Text,
                txtValidade.Text,
                txtCodSeg.Text,
                chkCapturar.Checked,
                "http://exemplos.boletoasp.com.br/retorno.aspx");

            txt.Text = trans.XML;
            lbl.Text = DateTime.Now.ToLongTimeString() + ": " + DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("##,##0ms ") +
                "<br/>ERRO: " + trans.ErroCodigo + " : " + trans.ErroMensagem +
                "<br/>TID: " + trans.TID + " Status: " + trans.Status.ToString() +
                "<br/>Autenticacao: " + trans.Autenticacao.Codigo +
                "<br/>Autorizacao: " + trans.Autorizacao.Codigo + 
                "<br/>Captura: " + trans.Captura.Codigo + 
                "<br/>PAN: " + trans.PAN +
                "<br/>UrlAutenticacao: " + string.Format("<a href='{0}' target='_blank'>{0}</a>", trans.UrlAutenticacao);

            if (trans.Status == CieloStatus.Criada)
            {
                this.Session["TID"] = trans.TID;
                lbl.Text += "<p>[ <a href='Cielo-Consulta.aspx'>Consulta</a> | <a href='Cielo-Captura.aspx'>Capturar</a> | <a href='Cielo-Cancelar.aspx'>Cancelar</a>]</p>";
            }

        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message;
        }
    }
}