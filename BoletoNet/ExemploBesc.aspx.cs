using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploBesc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "TESTE";
        Cedente.Banco = "027-2";
        Cedente.Agencia = "123-4";
        Cedente.Conta = "100046-0";
        Cedente.Convenio = "54321";
        Cedente.Carteira = "12";

        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "1234";
        Boleto.NumeroDocumento = Boleto.NossoNumero;
        Boleto.ValorDocumento = 50;
        Boleto.DataVencimento = new DateTime(2009, 3, 5);
        
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "Fábio Teste";
 
        // Define as instruções para o atendente do caixa
        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);

        lblCodBarra.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 5, 3, 2, 10, 3 });

    }
}
