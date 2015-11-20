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
using Impactro.Cobranca;

public partial class FuncTeste_CampoLivre : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Apenas para achar o posicionamento do conteudo padrão: Banco/Vencimento/Valor 44 - 25 = 19 posiçoes iniciais
            // ------------------------1234567890123456789_______CAMPO_LIVRE_______
            // ------------------------_BANCO_VENC_VALOR__1234567890123456789012345
            string cCodBarrasCoreto = "03392642100000001009717792500000019720301101";
            // Comparativo...........: 03392642100000001009717792500000019720301101 
            logOut.InnerText = "iniciando\r\n";

            // Usa uma rotina pré existente para montar só o campo livre
            string cCampoLivre = Banco_Banespa.CampoLivre(new Boleto(), "97177925000", "1972030");
            CampoLivre.Text = cCampoLivre;

            // Se a ideia é validar o campo livre pose-se pegar direto o inicio da código de barras direto sem o digito verificador
            string cCodBarras = 
                cCodBarrasCoreto.Substring(0, 4) + 
                cCodBarrasCoreto.Substring(5, 14) + 
                cCampoLivre;
                //cCodBarrasCoreto.Substring(19, 25); 

            string cDV = CobUtil.Modulo11Padrao(cCodBarras, 9).ToString(); // Só a Caixa calcula direfente!
            cCodBarras = cCodBarras.Substring(0, 4) + cDV + cCodBarras.Substring(4);

            // Exibe o Código de barras
            CodigoBarras.Text = CobUtil.CodigoBarrasFormatado(cCodBarras, new int[] { 25 });

            // Veja o Exemplo: BoletoCustomizado.aspx 
            LinhaDigitavel.Text = CobUtil.CalcLinDigitavel(cCodBarras);

            // Gera o Código de barras no padrão: pf, pl, bf, bl (preto fino, preto largo, branco fino branco largo)
            Barras.Text = CobUtil.BarCode(cCodBarras)
                .Replace("bf", "<img src='imagens/b.gif' width=1 height=50>")
                .Replace("bl", "<img src='imagens/b.gif' width=3 height=50>")
                .Replace("pf", "<img src='imagens/p.gif' width=1 height=50>")
                .Replace("pl", "<img src='imagens/p.gif' width=3 height=50>");
        }
        catch(Exception ex)
        {
            logOut.InnerText += "\r\n<b>" + ex.Message + "</b>\r\n" + ex.StackTrace;
        }
    }
}
