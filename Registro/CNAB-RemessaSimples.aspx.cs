using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;
using Impactro.Layout;
using System.IO;
using Impactro.WebControls;

public partial class CNAB_RemessaSimples : System.Web.UI.Page
{

    protected void btnTest_Click(object sender, EventArgs e)
    {

        // Definição dos dados do cedente - QUEM RECEBE / EMITE o boleto
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "TESTE QUALQUER LTDA";
        Cedente.CNPJ = "12123123/0001-01";

        // ABAIXO DESCOMENTE o bloco de dados do banco que pretende usar

        // SANTANDER
        //Cedente.Banco = "033";
        //Cedente.Agencia = "1234-1";
        //Cedente.Conta = "001234567-8";
        //Cedente.CodCedente = "1231230";
        //Cedente.CarteiraTipo = "5";
        //Cedente.Carteira = "101";
        //Cedente.CedenteCOD = "33333334892001304444"; // 20 digitos (note que o final, é o numero da conta, sem os ultios 2 digitos)
        //Cedente.Convenio = "0000000000000000002222220"; // 25 digitos
        //Cedente.useSantander = true; //importante para gerar o código de barras correto (por questão de compatibilidade o padrão é false)

        // BRADESCO
        //Cedente.Banco = "237-2";
        //Cedente.Agencia = "1510";
        //Cedente.Conta = "001466-4";
        //Cedente.Carteira = "09";
        //Cedente.CedenteCOD = "00000000000001111111"; // 20 digitos

        // ITAU
        //Cedente.CedenteCOD = "514432001";
        //Cedente.Banco = "341-1";
        //Cedente.Agencia = "6260";
        //Cedente.Conta = "01607-3";
        //Cedente.Carteira = "109";

        // Banco do Brasil
        //Cedente.Banco = "001-9";
        //Cedente.Agencia = "294-1";
        //Cedente.Conta = "004570-6";
        //Cedente.Carteira = "18";
        //Cedente.Modalidade = "21";
        //Cedente.Convenio = "859120";

        // CAIXA 
        //Cedente.Banco = "104";
        //Cedente.Agencia = "123-4";
        //Cedente.Conta = "5678-9";
        //Cedente.Carteira = "2";          // Código da Carteira
        //Cedente.Convenio = "02";         // CNPJ do PV da conta do cliente
        //Cedente.CodCedente = "455932";   // Código do Cliente(cedente)
        //Cedente.Modalidade = "14";       // G069 - CC = 14 (título Registrado emissão Cedente)
        //Cedente.Endereco = "Rua Sei la aonde";
        //Cedente.Informacoes =
        //    "SAC CAIXA: 0800 726 0101 (informações, reclamações, sugestões e elogios)<br/>" +
        //    "Para pessoas com deficiência auditiva ou de fala: 0800 726 2492<br/>" +
        //    "Ouvidoria: 0800 725 7474 (reclamações não solucionadas e denúncias)<br/>" +
        //    "<a href='http://caixa.gov.br' target='_blank'>caixa.gov.br</a>";
        //BoletoTextos.LocalPagamento = "PREFERENCIALMENTE NAS CASAS LOTÉRICAS ATÉ O VALOR LIMITE";

        // SICRED
        Cedente.Banco = "748-2";
        Cedente.Agencia = "1234-5";
        Cedente.Conta = "98765-1";
        Cedente.CodCedente = "12345";
        Cedente.Modalidade = "04";

        // Cria uma instancia do gerador de arquivo que abstrai as classes individuais de geração
        LayoutBancos r = new LayoutBancos(); // classe genérica para qualquer banco, compatível até com ActiveX
        r.Init(Cedente); // define o cedente

        // É quase que obrigatporio para o sicredi
        // r.onRegBoleto += r_onRegBoleto; // Para personalizar as linhas com os campos adicionais
        // Mas há outra forma usando o 'SetRegKeyValue(key, valor);' em cada boleto info

        // É possível configurar o lote, por padrão é gerado AADDDHH (Ano, Dia do ano, Hora) obrigatório para bradesco
        // r.Lote += 2000000; // Ou é possivel usar a logica que quiser, por exemplo, inicia com 3 o numero do lote! (soma 20 anos)

        // Definição dos dados do sacado -  QUEM PAGA
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "TESTE OK (Teste para homologação)";
        Sacado.Documento = "12321321000112";
        Sacado.Endereco = "RUA TESTE XXX";
        Sacado.Cidade = "SÃO PAULO";
        Sacado.Bairro = "JARDIM Y";
        Sacado.Cep = "12345-678";
        Sacado.UF = "SP";

        // Abaixo serão criados 5 boleto distintos para o mesmo sacado criado acima

        // Definição dos dados do boleto1
        BoletoInfo Boleto1 = new BoletoInfo();
        Boleto1.NossoNumero = "2265";
        Boleto1.BoletoID = 0000001;
        Boleto1.NumeroDocumento = Boleto1.NossoNumero;
        Boleto1.ValorDocumento = 12.34;
        Boleto1.DataDocumento = DateTime.Now;
        Boleto1.DataVencimento = DateTime.Now.AddDays(5);
        Boleto1.Ocorrencia = Ocorrencias.Remessa; // código 1

        // As linhas a seguir customiza qualquer valor sem precisar usar o evento 'r.onRegBoleto' o que torna a implementação mais simples
        // A forma mais pratica e segura é sempre usar os enumeradores
        Boleto1.SetRegEnumValue(CNAB400Remessa1Sicredi.TipoCarteira, "X"); // (posição 3)  
        Boleto1.SetRegEnumValue(CNAB400Remessa1Sicredi.TipoJuros, "Y");    // (posição 19) // Apenas se atente para a diferença do nome para SetRegEnumValue()
        Boleto1.SetRegKeyValue("CNAB400Remessa1Sicredi.Alteracao", "C");   // (posição 71) // É possivel adicionar o nome e valor do enumerador, isso é compativel com VB6
        Boleto1.SetRegKeyValue("Emissao", "A"); // posição 74 // ou simplesmente informar o nome do campo, mas cuidado pois há layouts que usam mais de um tipo de registro e as vezes tem nomes iguais mas as funções podem ser diferentes

        // Definição dos dados do boleto2
        BoletoInfo Boleto2 = new BoletoInfo();
        Boleto2.NossoNumero = "2266";
        Boleto2.BoletoID = 0000002;
        Boleto2.NumeroDocumento = Boleto2.NossoNumero;
        Boleto2.ValorDocumento = 123.45;
        Boleto2.DataDocumento = DateTime.Now;
        Boleto2.DataVencimento = DateTime.Now.AddDays(10);
        Boleto2.Ocorrencia = Ocorrencias.AlterarDados; // código 31
        Boleto2.Ocorrencia = (Ocorrencias)31; // código 31

        // Definição dos dados do boleto3
        BoletoInfo Boleto3 = new BoletoInfo();
        Boleto3.NossoNumero = "2267";
        Boleto3.BoletoID = 0000003;
        Boleto3.NumeroDocumento = Boleto3.NossoNumero;
        Boleto3.ValorDocumento = 2345.67;
        Boleto3.DataDocumento = DateTime.Now;
        Boleto3.DataVencimento = DateTime.Now.AddDays(15);
        Boleto3.Ocorrencia = Ocorrencias.Cancelamento; // 35
        Boleto3.PercentualMulta = 0.02;                             // Multa é quanto pagar a mais, apos o vencimento 2%)
        Boleto3.ValorMora = (0.01 / 30) * Boleto3.ValorDocumento;   // Mora é um valor a ser acrescido por dia apos o vencimento baseado (juros mensal total por mês 1%)

        // Definição dos dados do boleto4
        BoletoInfo Boleto4 = new BoletoInfo();
        Boleto4.NossoNumero = "2268";
        Boleto4.BoletoID = 0000004;
        Boleto4.NumeroDocumento = Boleto4.NossoNumero;
        Boleto4.ValorDocumento = 3456.78;
        Boleto4.DataDocumento = DateTime.Now;
        Boleto4.DataVencimento = DateTime.Now.AddDays(20);
        Boleto4.Ocorrencia = Ocorrencias.Remessa;
        Boleto4.PercentualMulta = 0.02;
        Boleto4.ValorMora = (0.01 / 30) * Boleto4.ValorDocumento;
        Boleto4.Instrucao2 = 6;         // Potestar (dependendo do banco esse é o código de protesto: Caixa, BB, Santander)
        Boleto4.DiasProtesto = 15;      // depois de 15 dias do vencimento

        // Definição dos dados do boleto5
        BoletoInfo Boleto5 = new BoletoInfo();
        Boleto5.NossoNumero = "2269";
        Boleto5.BoletoID = 0000005;
        Boleto5.NumeroDocumento = Boleto5.NossoNumero;
        Boleto5.ValorDocumento = 42.54;
        Boleto5.DataDocumento = DateTime.Now;
        Boleto5.DataVencimento = DateTime.Now.AddDays(25);
        Boleto5.Ocorrencia = Ocorrencias.Remessa;
        Boleto5.Instrucao2 = 6;         // Potestar (dependendo do banco esse é o código de protesto: Caixa, BB, Santander)
        Boleto5.DiasProtesto = 15;      // depois de 15 dias do vencimento
        // Protesta e sem mora

        // Adiciona os boletos previamente definidos no layout de registro
        r.Add(Boleto1, Sacado);
        r.Add(Boleto2, Sacado);
        r.Add(Boleto3, Sacado);
        r.Add(Boleto4, Sacado);
        r.Add(Boleto5, Sacado);

        // Se quiser exibir o processamento dos campos detalhados (para ajudar a identificar os campos) descomente a linha abaixo
        // r.ShowDumpLine = false;

        // Gera o texto de registro (com as informações de dump habilitadas acima)
        txtOut.Text = r.Remessa();

        // Grava um novo arquivo final no diretório atual (mas sem as informações de dump, caso tenha habilitado acima, desabilite aqui abaixo)
        // r.ShowDumpLine = false;
        r.RemessaTo(MapPath("remessa.txt"));
        // FIM DO PROGRAMA EXEMPLO
        // =======================

        // As linhas abaixão são apenas um exemplo para ilustrar o funcionamento dos dados processados
        // Muito util para conferencia do que está sendo gerado no arquivo

        //gvHeader1.DataSource = r.Table(typeof(CNAB240HeaderArquivoCaixa));
        //gvHeader2.DataSource = r.Table(typeof(CNAB240HeaderLoteCaixa));
        //gvItens1.DataSource = r.Table(typeof(CNAB240SegmentoPCaixa));
        //gvItens2.DataSource = r.Table(typeof(CNAB240SegmentoQCaixa));
        //gvFooter1.DataSource = r.Table(typeof(CNAB240TrailerLoteCaixa));
        //gvFooter2.DataSource = r.Table(typeof(CNAB240TrailerArquivoCaixa));

        //gvHeader1.DataBind();
        //gvHeader2.DataBind();
        //gvItens1.DataBind();
        //gvItens2.DataBind();
        //gvFooter1.DataBind();
        //gvFooter2.DataBind();

        // Exibe os boletos em tela (mesma lógica do exemplo 'GeraVarios.aspx')
        foreach (string cNN in r.Boletos.NossoNumeros)
        {
            BoletoWeb blt = new BoletoWeb();
            blt.CssCell = "BolCell";
            blt.CssField = "BolField";
            form1.Controls.Add(blt);
            r.Boletos[cNN].GeraInstrucoes();
            blt.MakeBoleto(Cedente, r.Boletos[cNN].Sacado, r.Boletos[cNN]);
        }
    }

    // Uso para personalizar os campos do sicred ou de qualquer outro que não seguem os padrão comum
    void r_onRegBoleto(CNAB cnab, IReg reg, BoletoInfo boleto)
    {
        Reg<CNAB400Remessa1Sicredi> regBoleto = (Reg<CNAB400Remessa1Sicredi>)reg;

        // A rotina padrão define estas variáveis comentadas abaixo:
        /*
        regBoleto[CNAB400Remessa1Sicredi.NumeroDocumento] = cnab.Cedente.DocumentoNumeros;
        regBoleto[CNAB400Remessa1Sicredi.NossoNumero] = boleto.NossoNumero;
        regBoleto[CNAB400Remessa1Sicredi.NumeroDocumento] = boleto.NumeroDocumento;
        regBoleto[CNAB400Remessa1Sicredi.DataVencimento] = boleto.DataVencimento;
        regBoleto[CNAB400Remessa1Sicredi.ValorDocumento] = boleto.ValorDocumento;
        regBoleto[CNAB400Remessa1Sicredi.Especie] = CNAB400Sicredi.EspecieSicred(boleto.Especie);
        regBoleto[CNAB400Remessa1Sicredi.Aceite] = boleto.Aceite == "A" ? "S" : "N";
        regBoleto[CNAB400Remessa1Sicredi.Data] = boleto.DataDocumento;
        regBoleto[CNAB400Remessa1Sicredi.DataEmissao] = boleto.DataDocumento;
        if (boleto.ParcelaTotal > 0)
        {
            regBoleto[CNAB400Remessa1Sicredi.TipoImpressao] = "B";
            regBoleto[CNAB400Remessa1Sicredi.ParcelaNumero] = boleto.ParcelaNumero;
            regBoleto[CNAB400Remessa1Sicredi.ParcelaTotal] = boleto.ParcelaTotal;
        }
        regBoleto[CNAB400Remessa1Sicredi.Instrucao] = boleto.Instrucao1;
        regBoleto[CNAB400Remessa1Sicredi.Protesto] = boleto.DiasProtesto > 6 ? "06" : "00";
        regBoleto[CNAB400Remessa1Sicredi.DiasProtesto] = boleto.DiasProtesto;
        regBoleto[CNAB400Remessa1Sicredi.PercentualMora] = boleto.PercentualMora;
        regBoleto[CNAB400Remessa1Sicredi.DataDesconto] = boleto.DataDesconto;
        regBoleto[CNAB400Remessa1Sicredi.ValorDesconto] = boleto.ValorDesconto;
        regBoleto[CNAB400Remessa1Sicredi.SacadoTipo] = boleto.Sacado.Tipo;
        regBoleto[CNAB400Remessa1Sicredi.SacadoDocumento] = boleto.Sacado.DocumentoNumeros;
        regBoleto[CNAB400Remessa1Sicredi.Endereco] = boleto.Sacado.Endereco;
        regBoleto[CNAB400Remessa1Sicredi.CEP] = boleto.Sacado.CepNumeros;
        */

        // Campos com certa particulariade no sicred
        regBoleto[CNAB400Remessa1Sicredi.TipoCarteira] = "X"; // posição 3
        regBoleto[CNAB400Remessa1Sicredi.TipoJuros] = "Y";    // posição 19
        regBoleto[CNAB400Remessa1Sicredi.Alteracao] = "C";    // posição 71 // Desconto por dia de antecipação; 
        regBoleto[CNAB400Remessa1Sicredi.Emissao] = "A";      // posição 74 // O padrão é que a emissão seja feito no cliente ("B")
    }
}