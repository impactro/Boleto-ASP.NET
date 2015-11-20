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
using System.Text;

public partial class FuncTeste_Modulo11 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            int n, nResult1, nResult2;
            string cValor;
            StringBuilder sb = new StringBuilder();

            // Testes genéricos
            cValor = "29875782123";
            nResult1 = Funcoes.Modulo11Padrao(cValor, 9);
            nResult2 = Funcoes.Modulo11Especial(cValor, 9);
            sb.AppendFormat("<b>Exemplo Unibanco Pag 55-56: {0} => {1} , {2}</b><br>", cValor, nResult1, nResult2);

            // Teste de Geração em Massa
            sb.Append("<br><hr>");
            for (n = 0; n < 1000; n++)
            {
                cValor = n.ToString();
                nResult1 = Funcoes.Modulo11Padrao(cValor, 9);
                nResult2 = Funcoes.Modulo11Especial(cValor, 9);
                if (nResult1 == nResult2)
                    sb.AppendFormat("{0} => {1} , {2}<br>", cValor, nResult1, nResult2);
                else
                    sb.AppendFormat("<b>{0} => {1} , {2} </b><br>", cValor, nResult1, nResult2);
            }
            this.lblResult.Text = sb.ToString();
        }
    }
    protected void btnPadrao_Click(object sender, EventArgs e)
    {
        try
        {
            string cValor=txtValor.Text.Replace(".","").Replace(" ","").Replace("-","");
            lblDigito.Text = Funcoes.Modulo11Padrao(cValor, Int32.Parse(txtModulo.Text)).ToString();
            lblDigito.Text += string.Format("    (length:{0})", txtValor.Text.Length);
        }
        catch (Exception ex)
        {
            lblDigito.Text = ex.Message;
        }
    }
    protected void btnEspecial_Click(object sender, EventArgs e)
    {
        try
        {
            string cValor=txtValor.Text.Replace(".","").Replace(" ","").Replace("-","");
            lblDigito.Text = Funcoes.Modulo11Especial(cValor, Int32.Parse(txtModulo.Text)).ToString();
            lblDigito.Text += string.Format("    (length:{0})", txtValor.Text.Length);
        }
        catch (Exception ex)
        {
            lblDigito.Text = ex.Message;
        }

    }
}
