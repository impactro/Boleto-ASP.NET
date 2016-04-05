using System;
using System.Collections.Generic;
using System.Web.UI;
using Impactro.Cobranca;
using Impactro.WebControls;

public partial class BoletoNet_HomologaCaixaCS : System.Web.UI.Page
{
    // Mesma logica do exemplo 'geravarios'
    protected void Page_Load(object sender, EventArgs e)
    {
        // Definição dos dados do cedente
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Exemplo de empresa cedente";
        Cedente.Endereco = "rua Qualquer no Bairro da Cidade";
        Cedente.CNPJ = "12.345.678/00001-12";
        Cedente.Banco = "104";
        Cedente.Agencia = "4353";
        Cedente.Conta = "00000939-9";
        Cedente.Carteira = "2"; // 1-Registrada ou 2-Sem registro
        Cedente.CodCedente = "658857";
        Cedente.Convenio = "1234"; // CNPJ do PV da conta do cliente = 00.360.305/4353-48 (usado em alguns casos)
        Cedente.Informacoes =
            "SAC CAIXA: 0800 726 0101 (informações, reclamações, sugestões e elogios)<br/>" +
            "Para pessoas com deficiência auditiva ou de fala: 0800 726 2492<br/>" +
            "Ouvidoria: 0800 725 7474 (reclamações não solucionadas e denúncias)<br/>" +
            "<a href='http://caixa.gov.br' target='_blank'>caixa.gov.br</a>";

        BoletoTextos.LocalPagamento = "PREFERENCIALMENTE NAS CASAS LOTÉRICAS ATÉ O VALOR LIMITE";

        // Definição dos dados do sacado
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "Fabio Ferreira (Teste para homologação)";
        Sacado.Documento = "123.456.789-99";
        Sacado.Endereco = "Av. Paulista, 1234";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Centro";
        Sacado.Cep = "12345-123";
        Sacado.UF = "SP";
        Sacado.Avalista = "CNPJ: 123.456.789/00001-23";

        // Para aprovar a homologação junto a caixa é necessário apresentar 10 boletos com os 10 digitos de controle da linha digitável diferentes
        // E mais outros 10 com o digito de controle do código de barras
        // Assim a ideia é criar 2 listas para ir memorizando os boletos já validos e deixa-los entrar em tela

        List<int> DAC1 = new List<int>();
        List<int> DAC2 = new List<int>();

        for (int nBoleto = 1001; nBoleto < 1100; nBoleto++)
        {
            // Definição dos dados do boleto de forma sequencial
            BoletoInfo Boleto = new BoletoInfo()
            {
                NumeroDocumento = nBoleto.ToString(),
                NossoNumero = nBoleto.ToString(),
                ValorDocumento = 123.45,
                DataVencimento = DateTime.Now,
                DataDocumento = DateTime.Now,
            };

            // Componente HTML do boleto que poderá ser ou não colocado em tela
            BoletoWeb blt = new BoletoWeb();

            // Junta as informações para fazer o calculo
            blt.MakeBoleto(Cedente, Sacado, Boleto);

            // A instancia 'blt' é apenas un Webcontrol que renderiza o boleto HTML, tudo fica dentro da propriedade 'Boleto'
            blt.Boleto.CalculaBoleto();
            // 10491.23456 60000.200042 00000.000844 4 67410000012345
            // 012345678901234567890123456789012345678901234567890123
            // 000000000111111111122222222223333333333444444444455555
            int D1 = int.Parse(blt.Boleto.LinhaDigitavel.Substring(38, 1));
            int D2 = int.Parse(blt.Boleto.LinhaDigitavel.Substring(35, 1));
            // De acordo com o banco:
            // Todos os Dígitos Verificadores Geral do Código de Barras possíveis(de 1 a 9) ou seja, campo 4 da Representação Numérica
            // Todas os Dígitos Verificadores do Campo Livre possíveis(de 0 a 9), 10ª posição   do campo 3 da Representação Numérica

            bool lUsar = false;
            if (!DAC1.Contains(D1))
            {
                lUsar = true;
                DAC1.Add(D1);
            }
            if (!DAC2.Contains(D2))
            {
                lUsar = true;
                DAC2.Add(D2);
            }

            if (lUsar)
            {
                // Apenas configura o As classe CSS de layout
                blt.CssCell = "BolCell";
                blt.CssField = "BolField";
                // Adiciona a instancia na tela do boleto valido para uso
                form1.Controls.Add(blt);
            }

            // Quando todas as possibilidades concluidas em até 100 boletos, já pode terminar...
            if (DAC1.Count == 9 && DAC2.Count == 10)
                break; // o Modulo 11 padrão não tem o digito Zero, mas o especial para calculo do nosso numero tem

            // Se o boleto foi usado e não acabou, então gera uma quebra de linha
            if (lUsar)
                form1.Controls.Add(new LiteralControl("<div style='page-break-after: always'><br/></div>"));
        }

        // Em geral esse teste gera 11 ou mais boletos contemplando todos os casos
        // Salve como PDF e envie para homologação
    }
}