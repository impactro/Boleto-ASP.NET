using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Impactro.Cobranca;

public partial class ExemploBancoDoBrasil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do cedente
        CedenteInfo Cedente =new CedenteInfo();
        Cedente.Cedente = "Impactro Informática (teste)";
        Cedente.Banco = "001-9";
        Cedente.Agencia = "294-1";
        Cedente.Conta = "004570-6";
        // ATENÇÃO: Geralmente o banco informa a Carteira da seguinte forma: 16/019
        // Para o gerador isso significa sempre CARTEIRA/MODALIDADE, e ambas com apenas 2 dígitos
        // E estes devem ser configurados separadamente como indicado abaixo neste exemplo
        Cedente.Carteira = "18";
        Cedente.Modalidade = "21";
        Cedente.Convenio = "859120";
        
        // Definição dos dados do sacado
        SacadoInfo Sacado =new SacadoInfo();
        Sacado.Sacado = "Fabio Ferreira (Teste)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";
        Sacado.Avalista = "Banco / Empresa - CNPJ: 123.456.789/00001-23";

        // Definição das Variáveis do boleto
        BoletoInfo Boleto=new BoletoInfo ();
        Boleto.NossoNumero = "131872";
        Boleto.NumeroDocumento = "131872";
        Boleto.ValorDocumento = 0;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = DateTime.Parse("10/03/2014");
        Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // Desabilita a parte superior do boleto (Recibo do Sacado) que pode ter um layout livre
        // bltPag.ExibirReciboSacado = false;

        // Configura as imagens
        bltPag.ImagePath = "imagens/";
        bltPag.ImageType = Impactro.WebControls.BoletoImageType.gif;
        bltPag.ImageLogo = "Impactro-Logo.gif"; //Define a imagem do logotipo da sua empresa
        bltPag.ExibeEnderecoReciboSacado = true;

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);
    }
}
