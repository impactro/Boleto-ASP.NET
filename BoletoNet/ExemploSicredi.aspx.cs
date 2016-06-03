using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploSicredi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        

        // Definição dos dados do cedente (do exemplo página 36 sem registro)
        // Linha digitável: 74891.11422 00001.039544 02000.921078 9 61870000010000
        // Código de Barras: 748.9.9.6187.0000010000-1.1.142000010.3954.02.00092.1.0.7
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "FRANCISCO ERDOS";
        Cedente.CNPJ = "72.106.461/0001-54";
        Cedente.Banco = "748-X";
        Cedente.Agencia = "0723";
        Cedente.Conta = Cedente.CodCedente = "89159-2";
        Cedente.Modalidade = "23"; // UA/POSTO:
        Cedente.Carteira = "3"; // 1-Com Registro, 3-Sem registro

        // Definição dos dados do sacado
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "(Teste para homologação)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";

        // Definição dos dados do boleto
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.DataDocumento = DateTime.Now;
        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        Boleto.NossoNumero = Boleto.NumeroDocumento = "­16204962";
        Boleto.ValorDocumento = 46.85;
        Boleto.DataVencimento = new DateTime(2016, 6, 1);
        

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);
        
        // Exibe o codigo de barras devidamente formatado e separado
        lblCodBar.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 1, 1, 9, 4, 2, 5, 1, 1, 1 });
        
    }
}
