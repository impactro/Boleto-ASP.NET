using System;
using Impactro.Cobranca;

public partial class Cielo_Cancelar : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack && this.Session["TID"] != null)
            txtTID.Text = (string)this.Session["TID"];
    }

    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime dt = DateTime.Now;
            
            CieloTransacao trans = Cielo.Cancela( 
                Cielo.testeLojaNumero,
                Cielo.testeLojaChave, // usando a chave de teste a transação ocorre no ambiente de teste
                txtTID.Text);

            txt.Text = trans.XML;
            lbl.Text = DateTime.Now.ToLongTimeString() + ": " + DateTime.Now.Subtract(dt).TotalMilliseconds.ToString("##,##0ms ") +
                "ERRO: " + trans.ErroCodigo + " : " + trans.ErroMensagem + "<br/>" +
                "TID: " + trans.TID + " Status: " + trans.Status.ToString() + "<br/>" +
                "UrlAutenticacao: " + string.Format("<a href='{0}'>{0}</a>", trans.UrlAutenticacao);

        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message;
        }
    }

}