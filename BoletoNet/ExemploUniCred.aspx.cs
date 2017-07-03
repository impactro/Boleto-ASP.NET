using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploUniCred : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "TESTE";
        Cedente.Banco = "136";
        Cedente.Agencia = "123-4";
        Cedente.Conta = "12345678-9";

        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "299621";
        Boleto.NumeroDocumento = Boleto.NossoNumero;
        Boleto.ValorDocumento = 50.67;
        Boleto.DataVencimento = new DateTime(2017, 3, 5);
        
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "Fábio Teste";
 
        // Define as instruções para o atendente do caixa
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);

        lblCodBarra.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 4, 10, 10, 1 });

    }
}