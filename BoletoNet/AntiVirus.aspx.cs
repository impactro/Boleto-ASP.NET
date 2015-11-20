using Impactro.Cobranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BoletoNet_AntiVirus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // baseado no exemplo basico: BoletoCS
        // Definição dos dados do cedente - QUEM RECEBE / EMITE
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Exemplo de empresa cedente<br/>Endereço yyy";
        Cedente.Banco = "237";
        Cedente.Agencia = "1234";
        Cedente.Conta = "45678-9";
        Cedente.Carteira = "6";

        // Definição dos dados do sacado -  QUEM PAGA
        SacadoInfo Sacado = new SacadoInfo();
        //Sacado.SacadoCOD = "123"; // Código interno de controle
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";

        // Definiçào dos dados do boleto
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "123400";
        Boleto.NumeroDocumento = "123400";
        Boleto.ValorDocumento = 423.45;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = new DateTime(2006, 5, 31); 

        // Obrigatório para o UNIBANCO
        Boleto.LocalPagamento = "Pagável em qualquer agência bancária";
        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        bltPag.MakeBoleto(Cedente, Sacado, Boleto);
        //bltPag.RenderImage = true; // isso está no aspx (html)
    }
}