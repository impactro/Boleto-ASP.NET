using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploCaixaSIGCB : System.Web.UI.Page
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
        Cedente.CodCedente = "209898";

        // Definição dos dados do boleto
        BoletoInfo Boleto=new BoletoInfo();
        Boleto.NossoNumero = "8";
        Boleto.NumeroDocumento = "8";
        Boleto.ValorDocumento = 1;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = new DateTime(2009,9,3);

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);

        CodBar.Text = bltPag.Boleto.CodigoBarrasFormatado(new int[] { 6, 1, 3, 1, 3, 1, 9, 1 });

        /*  POSIÇÃO TAMANHO PICTURE CONTEÚDO (página 5 do arquivo de documentação)
            20 – 25 6 9 (6) Código do Cedente
            26 – 26 1 9 (1) Dígito Verificador do Código do Cedente
            27 – 29 3 9 (3) Nosso Número – Seqüência 1
            30 – 30 1 9 (1) Constante 1
            31 – 33 3 9 (3) Nosso Número – Seqüência 2
            34 – 34 1 9 (1) Constante 2
            35 – 43 9 9 (9) Nosso Número – Seqüência 3
            44 – 44 1 9 (1) Dígito Verificador do Campo Livre
         */
    }
}
