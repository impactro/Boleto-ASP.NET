using System;
using Impactro.Layout;

public partial class Registro_Exemplo4_Registro : System.Web.UI.Page
{

    protected void btn_Click(object sender, EventArgs e)
    {
        // Assim juntando os 3 conceitos: Reflection, Attributs, e Generics a classe Reg<T> é criada
        // Veja aqui alguns breves exemplos de tradução de valores com a Reg<T>
        IReg ir=null;
        try
        {
            switch (rblTipo.SelectedValue)
            {
                case "0":   // Traduz linhas de Header CNAB400
                    Reg<CNAB400Header1Bradesco> r0 = new Reg<CNAB400Header1Bradesco>();
                    ir = r0;
                    r0.Line = txt.Text;
                    break;

                case "1":   // Traduz linhas de registro Bradesco
                    Reg<CNAB400Remessa1Bradesco> r1 = new Reg<CNAB400Remessa1Bradesco>();
                    ir = r1;
                    r1.Line = txt.Text;
                    break;

                case "2":   // Traduz linhas de registro Bradesco (tipo 2 apenas como sendo expemplo para simplificar)
                    Reg<CNAB400Retorno1Bradesco> r2 = new Reg<CNAB400Retorno1Bradesco>();
                    ir = r2;
                    r2.Line = txt.Text;
                    break;

                case "9":   // Traduz linhas de rodape CNAB400
                    Reg<CNAB400ArquivoTrailer> r9 = new Reg<CNAB400ArquivoTrailer>();
                    ir = r9;
                    r9.Line = txt.Text;
                    break;
            }
            lbl.Text = "OK";
        }
        catch (Exception ex)
        {
            // Se o layout não estiver de acordo com o tipo de dado esperado pode haver erros de conversão.
            // Imagine por exemplo a conversão de textos em campo numericos de data ou valor
            lbl.Text = ex.Message;
        }
        lbl.Text += "<pre>" + ir.Dump + "</pre>";
    }
}