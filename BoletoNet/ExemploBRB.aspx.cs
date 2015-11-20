using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploBRB : System.Web.UI.Page
{

    // ATENÇÃO: Este exemplo funciona apenas no .Net 3.0!
    // Por usar instaciamento direto no construtor e expressão lambida

    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do cedente

        CedenteInfo Cedente = new CedenteInfo()
        {
            Cedente = "Exemplo de empresa cedente",
            Banco = "070-1",
            Agencia = "058",
            Conta = "6002006",
            Carteira= "1"
        };

        // Definição dos dados do sacado

        SacadoInfo Sacado = new SacadoInfo()
        {
            Sacado = "(Teste para homologação)",
            Documento = "123.456.789-99",
            Endereco = "Av. Paulista, 1234",
            Cidade = "São Paulo",
            Bairro = "Centro",
            Cep = "12345-123",
            UF = "SP"
        };

        // Definição dos dados do boleto

        BoletoInfo Boleto = new BoletoInfo()
        {
            NossoNumero = "000001",
            NumeroDocumento = "1",
            ValorDocumento = 1,
            //DataDocumento = DateTime.Now,
            //DataVencimento = new DateTime(2008, 9, 8),
        };
        
        // é possivel customizar a geração do campo livre
        // desta forma você poderá implementar outros bancos ou carteiras
        // (exemplo usando expressão lambida)
        /*
        bltPag.MontaCampoLivre += (Boleto blt) =>
        {
            return new string('0', 25); // importante é sempre retornar 25 caracteres
        };
        */

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);
        
        // Exibe o codigo de barras devidamente formatado e separado
        lblCodBar.Text = bltPag.Boleto.CodigoBarrasFormatado(
            new int[] { 3, 3, 7, 1, 6, 3, 1, 1 });

    }
}
