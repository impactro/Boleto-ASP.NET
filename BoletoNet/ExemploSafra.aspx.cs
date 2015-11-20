using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploSafra : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "IMPACTRO TESTE";
        Cedente.Banco = "422-7";
        Cedente.Agencia = "12300";
        Cedente.Conta = "4321-6";
        Cedente.Carteira = "1";
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "949275603";
        Boleto.NumeroDocumento = "8400004006";
        Boleto.ValorDocumento = 3482.43;
        Boleto.DataVencimento = new DateTime(2008, 8, 18);
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "FABIO FERREIRA";
        Sacado.Documento = "123456789";

        // Define as instruções para o atendente do caixa
        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);
        bltPag.Boleto.CarteiraExibicao = "060";

        // exibe o código fe barras de acrodo com a carteira
        if (Cedente.Carteira == "1" || Cedente.Carteira == "2")
            lblInfo.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 1, 5, 9, 9, 1 });
        else
            lblInfo.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 1, 7, 16, 1 });
    }
}
