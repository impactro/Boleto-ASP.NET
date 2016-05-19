using System;
using System.Text;

public partial class FuncTeste_FatVenc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        DateTime dt = new DateTime(1997, 10, 7);
        int n,i;
        i = 0;
        do
        {
            n = Funcoes.CalcFatVenc(dt);
            sb.AppendFormat("{1:dd/MM/yyyy} - {0:0000}<br>", n, dt);
            dt = dt.AddDays(1);
        } while (i++<11000);
        this.lblResult.Text = sb.ToString();
    }
}
