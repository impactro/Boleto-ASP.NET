using System;
using Impactro.Cobranca;

public partial class BoletoCaixa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do sacado
        SacadoInfo Sacado=new SacadoInfo();
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";
        Sacado.Avalista = "Banco XPTO - CNPJ: 123.456.789/00001-23";

        // Definição dos dados do cedente
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Exemplo de empresa cedente";
        Cedente.Banco = "104";
        Cedente.Agencia = "0183";
        Cedente.Conta = "003.00000622-0";
        Cedente.Carteira = "2"; // 1-Registrada ou 2-Sem registro
        Cedente.CodCedente = "035187000000055";

        // Definição dos dados do boleto
        BoletoInfo Boleto=new BoletoInfo();
        Boleto.NossoNumero = "8210000030";
        Boleto.NumeroDocumento = Boleto.NossoNumero;
        Boleto.ValorDocumento = 18;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = new DateTime(2015,11,30);

        // monta o boleto com os dados específicos nas classes
        bltPag.ExibeEnderecoReciboSacado = true;
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);

        // Exibe o código de barras
        CodBar.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 10, 4, 11 });
    }
}
