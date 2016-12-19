using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Layout;
using Impactro.Cobranca;

public partial class CNAB_RetornoSimples : System.Web.UI.Page
{
    protected void btnTest_Click(object sender, EventArgs e)
    {
        lblOut.Text = "";
        try
        {
            /* Exemplo de retorno BRADESCO
            10205491613000192000000900462012390740000000000000000000000004000000000000000000530000000000000000000000000903190515000000000500000000000000000053040615000000001547823703152  000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          0800000000                                                                  000006
            10205491613000192000000900462012390740000000000000000000000005000000000000000000610000000000000000000000000903190515000000000600000000000000000061050615000000001557823703152  000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          0800000000                                                                  000007
            102054916130001920000009004620123907400000000000000000000000060000000000000000007P000000000000000000000000090319051500000000070000000000000000007P060615000000001567823703152  000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          0800000000                                                                  000008
            10205491613000192000000900462012390740000000000000000000000007000000000000000000880000000000000000000000000903190515000000000800000000000000000088070615000000001577823703152  000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          0800000000                                                                  000009
            102005478600001400000009004620124576700000000000000000000069050000000009000006905300000000000000000000000009322307150000006905000000000900000690530207150000000009540237       000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          3300000000                                                                  000036
            102005478600001400000009004620124576700000000000000000000069410000000009000006941P000000000000000000000000093223071500000069410000000009000006941P0907150000000048058237       000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          3300000000                                                                  000037
            102005478600001400000009004620124576700000000000000000000069680000000009000006968100000000000000000000000009322307150000006968000000000900000696811307150000000009800237       000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000                          3300000000                                                                  000038
            */

            /* Exemplo de retorno SICOOB
            02RETORNO01COBRANÇA       30090000198889145823ELTON JOHN GOMES DA SILVA ME  756 - BANCOOB S/A 2203160000694                                                                                                                                                                                                                                                                                               000001
            1020569299500011230090000145823000000                         000000056978010000000OU00000000000000000000 0205210316                              210316000000001040010407190992103160000160000000000000000000000000000000000000000000000000000000000000000000000000010400000000000000000000000000000000000000000000000000000010000000000000          0000000000000000000000000000000000000000000000000000000002
            1020569299500011230090000145823000000                         000000056082010000000OU00000000000000000000 0205210316                              200316000000002200010401160992103160000160000000000000000000000000000000000000000000000000000000000000000000000000022000000000000000000000000000000000000000000000000000000010000000000000          0000000000000000000000000000000000000000000000000000000003
            1020569299500011230090000145823000000                         000000057281010000000OU00000000000000000000 0205210316                              210316000000031850000108330992103160000160000000000000000000000000000000000000000000000000000000000000000000000000318500000000000000000000000000000000000000000000000000000010000000000000          0000000000000000000000000000000000000000000000000000000004
            9027563009SICOOB NORTE             AV. JONES DOS SANTOS NEVES                        CENTRO                        29800000Barra de São Francisco        ES220320160000000300001969859                                                                                                                                                                                                                    000005
            */

            LayoutBancos r = new LayoutBancos(); // classe genérica para qualquer banco, compatível até com ActiveX
            // é necessário apenas informar o numero do banco do cedente para usar a rotina de retorno do banco correto
            //r.Init(new CedenteInfo { Banco = "001" }); // Banco do Brasil
            //r.Init(new CedenteInfo { Banco = "033" }); // Banespa (Santander)
            //r.Init(new CedenteInfo { Banco = "104" }); // Caixa
            //r.Init(new CedenteInfo { Banco = "237" }); // Brtadesco
            //r.Init(new CedenteInfo { Banco = "341" }); // Itau
            //r.Init(new CedenteInfo { Banco = "353" }); // Santander
            r.Init(new CedenteInfo { Banco = Request["banco"] ?? "341", Layout=LayoutTipo.Auto }); // Sicoob

            // Processar e identificar os registros
            Layout ret = r.Retorno(txtIn.Text);

            // Renderiza o conteudo lido
            //gv.DataSource = ret.Table(typeof(CNAB400Retorno1Bradesco));
            //gv.DataBind();

            // O resultado estará dentro de um array de boletos
            BoletoInfo Boleto;
            foreach (string nn in r.Boletos.NossoNumeros)
            {
                Boleto = r.Boletos[nn];
                lblOut.Text += string.Format("{0} {1:dd/MM/yyyy} {2:C} <br/>\r\n", Boleto.NossoNumero, Boleto.DataVencimento, Boleto.ValorDocumento);
            }

            // uma opção mas simples é ler diretamente as linhas de um tipo de arquivo, mas devem ser exatamento do tipo correto
            // Tipo de estrutura a ser decodificada (enumerador de layout)
            //Type tp = typeof(CNAB240CobrancaRetorno);
            //Type tp = typeof(CNAB400Retorno1Bradesco);
            //Type tp = typeof(CNAB400Retorno1Itau);
            //Type tp = typeof(CNAB240SegmentoTCaixa);

            // A classe Layout tem diversos metodos genericos para fazer qualquer codificação e decodificação de textos de acordo com os tipos de enumeradores passados em seu contrutor
            //Layout lay = new Layout(tp);

            // Coloca o texto em questão para ser interpretado
            //lay.Conteudo = txtIn.Text;

            // Internamente a classe de Layour armazena todos os dados e por gerar outros objetos como um DataTable com uma das estruturas
            //gv.DataSource = lay.Table(tp);

            // Renderiza o conteudo lido
            //gv.DataBind();
        }
        catch(Exception ex)
        {
            lblOut.Text += "<br/>ERRO: " + ex.Message;
        }
    }
}