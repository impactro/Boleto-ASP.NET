using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Layout;
using System.Reflection;

public partial class Registro_Exemplo2_Atributo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Atributos é outro recurso avançado do .Net.
        // É comum ve-los nas difinições de seguranças de metodos e parametros de web services

        // Exemplo 2 )
        // Aqui é mostrado a listagem da estrutura de registros de um enumerador com atributos do tipo 'RegFormat'
        // será usado 

        lblOut.Text = "";

        // é possivel obtem as definições sem haver uma instancia, chamando o typeof de um definição qualquer (class, struc, enum)
        Type tp=typeof(CNAB400Remessa1Bradesco);

        int n = 1;

        // aqui será listado os campos do enumerador via 'GetFields()' generico, mas a classe Enum tem metodos mais eficases
        foreach(FieldInfo fi in tp.GetFields())
        {
            // em um enumerador, o campo valor, é uma variável especial, oculto na programação, mas visivel pela reflection
            if (fi.IsSpecialName)
                continue;

            // A classe 'Attribute', obtem um atributo de um campo
            // E por poder haver mais de um atributo, deve-se especificar qual o tipo de atributo estamos querendo obter
            RegFormat rf = (RegFormat)Attribute.GetCustomAttribute(fi, typeof(RegFormat));

            // se o atributo não for encontrado, retorna NULL
            if (rf == null)
                continue;

            // Formata o resultado
            lblOut.Text += string.Format("<i>{0:000}</i> <b>{1}({2:00})</b> {3}<br/>\r\n", n, rf.Type, rf.Length, fi.Name);
            n += rf.Length; // calcula a posição de forma incremental com a soma de todos os comprimentos
        }

        lblOut.Text += "<b>TOTAL: " + (n - 1) + " caracteres</b>"; // apenas remove 1 para ajustar ao inicio que é base 1
    }
}