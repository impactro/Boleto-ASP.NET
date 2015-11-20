using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploNossaCaixa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do sacado
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "(Teste para homologação)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";

        /*

        // Definição dos dados do cedente
        // (de acordo com o exemplo fornecido na documentação)
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Exemplo de empresa cedente";
        Cedente.Banco = "151-1";
        Cedente.Agencia = "0001-9";
        Cedente.Conta = "005432-4";
        Cedente.Carteira = "9";
        Cedente.Modalidade = "04";

        // Definição dos dados do boleto
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "990000001";
        Boleto.NumeroDocumento = "1";
        Boleto.ValorDocumento = 350;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = new DateTime(2000, 7, 15);
 
        */

        // Definição dos dados do cedente

        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Exemplo de empresa cedente";
        Cedente.Banco = "151-1";
        Cedente.Agencia = "123-4";
        Cedente.Conta = "4455-1";
        Cedente.CodCedente = "4";
        Cedente.Carteira = "1";
        Cedente.Modalidade = "04";

        // Definição dos dados do boleto
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "990000003";
        Boleto.NumeroDocumento = "3";
        Boleto.ValorDocumento = 212.5;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = new DateTime(2008, 9, 8);
        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);

        // É possivel também customizar a linha referente o local de pagamento:
        bltPag.Boleto.LocalPagamento = "Pague Preferencialmente no BANCO NOSSA CAIXA S.A. ou na rede bancária até o vencimento";
        
        // Exibe o codigo de barras devidamente formatado e separado
        lblCodBar.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 1, 8, 4, 1, 6, 3, 1, 1 });


    }
}
