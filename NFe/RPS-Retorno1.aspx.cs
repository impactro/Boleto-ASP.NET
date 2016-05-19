using System;
using Impactro.Layout;

public partial class RPS_Retorno1 : System.Web.UI.Page
{
    protected void btnTest_Click(object sender, EventArgs e)
    {
        Layout lay = new Layout(typeof(NFeV2detalhe));
        lay.Conteudo = txtIn.Text;
        gv.DataSource = lay.Table(typeof(NFeV2detalhe));
        gv.DataBind();
    }
}