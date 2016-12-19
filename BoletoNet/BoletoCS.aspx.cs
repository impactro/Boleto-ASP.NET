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
using Impactro.WebControls;

public partial class BoletoCS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do cedente - QUEM RECEBE / EMITE
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Exemplo de empresa cedente";
        Cedente.Banco = "237-";
        Cedente.Agencia = "1234";       // use somente "-" para separa o código da agencia e digito
        Cedente.Conta = "56789-1";      // use somente "-" para separa o código da conta e digito
        Cedente.Carteira = "6";
        Cedente.Modalidade = "";        // Em geral faz parte do nosso numero
        Cedente.Convenio = "1878794";   // ATENÇÃO: Alguns Bancos usam um código de convenio para remapear a conta do clientes
        Cedente.CodCedente = "1878794"; // outros bancos chama isto de Codigo do Cedente ou Código do Cliente

        // Novas Exigencias da FREBABAN: Exibir endereço e CNPJ no campo de emitente!
        //Cedente.ExibirCedenteDocumento = true; // Não é mais necessário pois agora é obrigatório para homologar
        Cedente.CNPJ = "12.345.678/0001-12";
        //Cedente.ExibirCedenteEndereco = true; // Não é mais necessário pois agora é obrigatório para homologar
        Cedente.Endereco = "Rua Sei lá aonde, 123 - Brás, São Paulo/SP";

        // outros usam os 2 campos para controles distintos!
        // Veja com atenção qual é o seu caso e qual destas variáveis deve ser usadas!
        // Olhe sempre os exemplos em ASP.Net se tiver dúvidas, pois lá há um exemplo para cada banco

        // Para casos especiais
        Cedente.UsoBanco = "123";
        Cedente.CIP = "456";
        Cedente.UsoBanco = "CVT 7744-5";

        // Definição dos dados do sacado -  QUEM PAGA
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.SacadoCodigo = "123"; // Código interno de controle
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";
        Sacado.Avalista = "Banco XPTO - CNPJ: 123.456.789/00001-23";

        // Definiçào dos dados do boleto
        BoletoInfo Boleto = new BoletoInfo();
        Boleto.NossoNumero = "123400";
        Boleto.NumeroDocumento = "123400";
        Boleto.ValorDocumento = 423.45;
        Boleto.DataDocumento = DateTime.Now;
        Boleto.DataVencimento = new DateTime(2006, 5, 31);
        Boleto.ParcelaNumero = 2;
        Boleto.ParcelaTotal = 10;

        // Obrigatório para o UNIBANCO
        Boleto.LocalPagamento = "Pagável em qualquer agência bancária";
        Boleto.Especie = Especies.RC;
        Boleto.DataDocumento = DateTime.Now.AddDays(-2);     // Por padrão é a data atual, geralmente é a data em que foi feita a compra/pedido, antes de ser gerado o boleto para pagamento
        Boleto.DataProcessamento = DateTime.Now.AddDays(-1); // Por padrão é a data atual, pode ser usado como a data em que foi impresso o boleto
        
        Boleto.Instrucoes = "Todas as as informações deste bloqueto são de exclusiva responsabilidade do cedente";

        // é possivel alterar alguns textos padrões conforme abaixo
        // conforme circulares BACEN 3.598 e 3.656 em vigor a partir de 28/06/2013 titulo fora mudados de cedente para beneficiário, e sacado para pagador
        // BoletoTextos.Cedente= "Cedente"; 
        //BoletoTextos.Sacado = "Sacador";

        // é possive exibir a linha digitável também no recibo do sacado
        //bltPag.ExibeReciboIPTE = true;

        // É possivel ocultar totalmente o recibo do sacado e customizar o layout deste
        //bltPag.ExibeReciboSacado = false;

        // personalize com o logo de sua empresa, e o caminho base das imagens
        bltPag.ImagePath = "imagens/";
        bltPag.ImageLogo = "Impactro-Logo.gif";
        bltPag.ImageType = BoletoImageType.gif; // Define que as imagens virão de arquivos esternos 

        //  // opções especiais: use com cuidado
        //  bltPag.Boleto.Especie = "?USD?";    // é possivel fazer cobranças em outras moedas
        //  bltPag.Boleto.Moeda = "3";          // mas deve ser analisado o código da moeda
        //  bltPag.ImageType = BoletoImageType.gif; // você pode customizar as imagens dos bancos
        //  bltPag.ImagePath = "imagens/";          // desde que informe um diretório onde as imagens estarão

        // monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto);
    }
}