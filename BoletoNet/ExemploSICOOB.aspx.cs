using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploSICOOB : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do cedente
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Impactro Informática (teste)";
        Cedente.Banco = "756-0";
        Cedente.Agencia = "1234";
        Cedente.Conta = "56789";
        // ATENÇÃO: Geralmebte o banco informa a Carteira da segunte forma: 16/019
        // Para o gerador isso sognifica sempre CARTEIRA/MODALIDADE, e ambas com apenas 2 digitos
        // E estes devem ser configurados separadamente com oindicado abaixo neste exemplo
        Cedente.Carteira = "1";
        Cedente.Convenio = "4071";      // Código da Cooperativa
        Cedente.CodCedente = "217018";   //Código do Cliente
        Cedente.Modalidade = "02";

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
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "9367231";
        Boleto.NumeroDocumento = "9367231";
        Boleto.ParcelaNumero = 1;
        Boleto.ValorDocumento = 25;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = DateTime.Parse("10/06/2011");

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);
    }
}
