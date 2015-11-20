using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;

public partial class ExemploRealCustomizado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do sacado
        SacadoInfo Sacado = new SacadoInfo();
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
        Cedente.Banco = "356";
        Cedente.Agencia = "1234";
        Cedente.Conta = "1234567-8";

        // Definiçào dos dados do boleto
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "123400";
        Boleto.NumeroDocumento = "123400";
        Boleto.ValorDocumento = 423.45;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = new DateTime(2006, 5, 31); 

        // Obrigatório para o UNIBANCO
        Boleto.LocalPagamento = "Pagável em qualquer agência bancária";
        Boleto.Especie = Especies.RC;
        Cedente.UsoBanco = "CVT 7744-5";

        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // veja o exemplo: BoletoCustomizado.aspx
        bltPag.MontaCampoLivre += new BoletoMontaCampoLivre(bltPag_MontaCampoLivre);
        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);
    }

    string bltPag_MontaCampoLivre(Boleto blt)
    {
        // obtem apenas o numero da conta sem o digito
        // executa a função de customização
        return meuBanco_Real.MeuCampoLivre(blt, blt.Agencia, blt.Conta.Split('-')[0], blt.NossoNumero);
        
        //A chamada abaixo comentada é o que existe dentro do componente (disponível somente nos fontes)
        //as várias rotianas abertas de cada banco
        //return Banco_Real.CampoLivre(blt, blt.Agencia, blt.Conta.Split('-')[0], blt.NossoNumero);
        
    }
}

public abstract class meuBanco_Real
{
    /// <summary>
    /// Digito do Código do Banco
    /// </summary>
    public const string BancoDigito = "5";

    /// <summary>
    /// Rotina de Geração do Campo livre usado no Código de Barras para formar o IPTE
    /// </summary>
    /// <param name="blt">Intancia da Classe de Boleto</param>
    /// <returns>String de 25 caractere que representa 'Campo Livre'</returns>
    public static string MeuCampoLivre(Boleto blt, string cAgenciaNumero, string cContaNumero, string cNossoNumero)
    {
        cAgenciaNumero = CobUtil.Right(cAgenciaNumero, 4);
        cContaNumero = CobUtil.Right(cContaNumero, 7);
        cNossoNumero = CobUtil.Right(cNossoNumero, 13);

        string cDAC = CobUtil.Modulo10(cNossoNumero + cAgenciaNumero + cContaNumero).ToString();

        string cLivre = cAgenciaNumero +
            cContaNumero +
            cDAC +
            cNossoNumero;

        blt.AgenciaConta = cAgenciaNumero + "/" + cContaNumero + "-" + cDAC;
        blt.NossoNumeroExibicao = cNossoNumero;

        return cLivre;

    }
}
