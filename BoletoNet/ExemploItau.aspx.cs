using System;
using Impactro.Cobranca;

public partial class ExemploItau : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do cedente
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Teste de Cedente";
        Cedente.Banco = "341";
        Cedente.Agencia = "0646";
        Cedente.Conta = "9105-8";
        Cedente.Carteira = "167";
        Cedente.Endereco = "endereço do recebedor";
        Cedente.CNPJ = "12.345.678/0000-12";

        // Definição dos dados do sacado
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "Fabio Ferreira (Teste)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";

        // Definição das Variáveis do boleto
        BoletoInfo Boleto =new BoletoInfo();
        Boleto.NossoNumero = Boleto.NumeroDocumento = "00046356";
        Boleto.ValorDocumento = 3070.14;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = DateTime.Parse("10/07/2008");
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);

        String cDados = bltPag.Boleto.CodigoBarras;

        // Exibe o campo livre decodificado
        lblCodBar.Text = cDados + "<br/>" + 
            cDados.Substring(0, 19) + "-" + 
            cDados.Substring(19, 3) + "." + 
            cDados.Substring(22, 8) + "." + 
            cDados.Substring(30, 1) + "." + 
            cDados.Substring(31, 4) + "." + 
            cDados.Substring(35, 5) + "." + 
            cDados.Substring(40);
    }
}