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
        // A "agência" e "conta" não são usado para nada no boleto, o que vale é o "Convenio", "Modalidade", e "Carteira"
        // Cedente.Agencia = "1234";           
        // Cedente.Conta = "56789";
        // ATENÇÃO: Geralmebte o banco informa a Carteira da segunte forma: 16/019
        // Para o gerador isso significa sempre CARTEIRA/MODALIDADE, e ambas com apenas 2 digitos
        // E estes devem ser configurados separadamente como indicado abaixo neste exemplo
        Cedente.Carteira = "1";
        Cedente.Modalidade = "02"; 
        Cedente.Convenio = "4071";          // Código da Cooperativa (será exibido como valor para a "agência"
        Cedente.CodCedente = "217018";      // Código do Cliente

        // Para saber quais são os campos fundamentais para a geração do boleto, 
        // veja a assinatura da função estatica que calcula o campo livre do banco em questão
        // Neste caso: Banco_SICOOB.CampoLivre(...);

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
