using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class FuncTeste_IPTE : System.Web.UI.Page
{
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        try
        {
            // Remove os espaços do código de barras
            string cCodBarras = this.txtCodBar.Text.Replace(" ", "").Replace(".", "");

            // Calcula a linha digitável
            this.lblIPTE.Text = Funcoes.CalcLinDigitavel(cCodBarras);

            // Calcula a string representativa do código de barras
            string cBarras = Funcoes.BarCode(cCodBarras);
            //substiue-se as duplas de caracteres que representam as barras por suas respectivas imagens
            cBarras = cBarras.Replace("bf", "<img src='imagens/b.gif' width=1 height=50>");
            cBarras = cBarras.Replace("bl", "<img src='imagens/b.gif' width=3 height=50>");
            cBarras = cBarras.Replace("pf", "<img src='imagens/p.gif' width=1 height=50>");
            cBarras = cBarras.Replace("pl", "<img src='imagens/p.gif' width=3 height=50>");

            this.lblCodBar.Text = cBarras;
        }
        catch (Exception ex)
        {
            this.lblIPTE.Text = ex.Message;
        }
    }
}
