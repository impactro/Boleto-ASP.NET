using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FuncTeste_CodigoBarras : System.Web.UI.Page
{
    protected void btnGerar_Click(object sender, EventArgs e)
    {
        img.Visible = true;
        img.ImageUrl = "812-698-4138.ashx?c=" + txtCodBat.Text;
    }
}
