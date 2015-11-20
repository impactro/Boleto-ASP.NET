using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Layout;

public partial class Registro_Exemplo3_Template : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // o Generics foi uma das maiores revoluções inseridas no .Net a partir da versão 2.0
        // é muito comum o uso de classes genericas em listas, mas o nome verdadeiro do conceito é 'Template', por isso que se defin como <T>
        // onde o T, é o Tipo de classe usado sempre que for referenciado, veja alguns:
        //  http://msdn.microsoft.com/pt-br/library/6sh2ey19.aspx
        //  http://www.codeguru.com/csharp/.net/article.php/c19413/
        //  http://www.macoratti.net/09/09/net_ug1.htm

        // Exemplo 3 )
        // Aqui apenas mostro o simples uso de uma lista com alguns tipos de registros baseado no enumerador de tipo de dados

        lblOut.Text = "";

        List<RegType> lr = new List<RegType>();

        // adiciona alguns tipos de registros em um array
        lr.Add(RegType.PD);
        lr.Add(RegType.P9);
        lr.Add(RegType.PX);
        lr.Add(RegType.PH);

        foreach (RegType rt in lr)
        {
            lblOut.Text += rt.ToString() + "<br>\r\n"; // apenas le o tipo no array, e gera uma quebra de linha
        }
    }
}