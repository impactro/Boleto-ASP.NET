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

public partial class FuncTeste_FatVenc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        DateTime dt = new DateTime(1997, 10, 7);
        int n;
        do
        {
            n = Funcoes.CalcFatVenc(dt);
            sb.AppendFormat("{1:dd/MM/yyyy} - {0:0000}<br>", n, dt);
            dt = dt.AddDays(1);
        } while (n != 9999);
        this.lblResult.Text = sb.ToString();
    }
}
