using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploBanese : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "TESTE PARA HOMOLOGAÇÃO";
        Cedente.Banco = "047-7";
        Cedente.Agencia = "059-3";
        Cedente.Conta = "100046-0";
        Cedente.CodCedente = "031000460";
        Cedente.Carteira = "SR";

        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "1431";
        Boleto.NumeroDocumento = Boleto.NossoNumero;
        Boleto.ValorDocumento = 50;
        Boleto.DataVencimento = new DateTime(2009, 3, 5);
        
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "Fábio Teste";
        Sacado.Documento = "123.456.789-01";

        // Define as instruções para o atendente do caixa
        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);

        lblCodBarra.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 2, 9, 9, 3, 1, 1 });

    }
}
