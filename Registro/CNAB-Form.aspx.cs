using System;
using System.Web.UI.WebControls;
using Impactro.Cobranca;
using Impactro.WebControls;
using Impactro.Layout;

public partial class CNAB_Form : System.Web.UI.Page
{
    // Definição dos dados do cedente - QUEM RECEBE / EMITE
    CedenteInfo Cedente = new CedenteInfo();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int nDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            txtVencimento.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, nDay).ToShortDateString();
        }

        // Para usar na remessa e retorno
        Cedente.Cedente = Request["Cedente"] ?? "Exemplo de empresa cedente";
        Cedente.Banco = Request["Banco"] ?? "237"; // Bradesco
        Cedente.Agencia = Request["Agencia"] ?? "1510-0";
        Cedente.Conta = Request["Conta"] ?? "1466-4";
        Cedente.Carteira = Request["Carteira"] ?? "09";
        Cedente.Modalidade = Request["Modalidade"] ?? "05";
        Cedente.Convenio = Request["Convenio"] ?? "05";
        Cedente.CedenteCOD = Request["CedenteCOD"] ?? "00000000000004047726"; // 20 digitos (bradesco)
    }

    protected void btnRemessa_Click(object sender, EventArgs e)
    {   // (Não é o foco validar dados de entrada, e sim testar a geração de registro)

        //Definição dos dados do sacado
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "Pedro Alvarez Cabral";
        Sacado.Documento = "123.123.134-12";
        Sacado.Endereco = "Rua 21 de Abril";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Vera Cruz";
        Sacado.Cep = "01500-000";
        Sacado.UF = "SP";
        Sacado.Email = "fabio@impactro.com.br";

        // Usando a classe bradesco diretamente
        // CNAB400Bradesco r = new CNAB400Bradesco();
        // r.Cedente = Cedente;
        // r.NumeroLote += 2000000; // inicia com 3 o numero do lote! (soma 20 anos)

        var r = new LayoutBancos();
        r.Init(Cedente);
        r.Lote = 123456; // é o NumeroLote do CNAB: é preciso gerar uma sequencia armazenada em banco que não se repita
        // O lote padrão gera AADDDHH (Ano, Dia do ano, Hora)

        //r.ShowDumpReg = true;

        // customiza campos
        r.onRegBoleto = CustomRegBoleto;

        for (int n = 0; n < Int32.Parse(txtQTD.Text); n++)
        {
            //Definição das Variáveis do boleto
            var Boleto = new BoletoInfo();
            Boleto.BoletoID = n;
            Boleto.NossoNumero = (Int32.Parse(txtNossoNumero.Text) + n).ToString();
            Boleto.NumeroDocumento = Boleto.NossoNumero;
            Boleto.ValorDocumento = double.Parse(txtValor.Text) + n;
            Boleto.DataDocumento = DateTime.Now;
            Boleto.DataVencimento = DateTime.Parse(txtVencimento.Text).AddDays(n);
            Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente";

            // WebControl
            var blt = new BoletoWeb();
            dvBoletos.Controls.Add(blt);

            blt.ImagePath = "../imagens/"; // Define o diretório de imagens
            blt.ExibeReciboSacado = false; // Apenas para exibir a parte que interessa do boleto
            blt.CssCell = "BolCell";
            blt.CssField = "BolField";

            // Gera um boleto
            blt.MakeBoleto(Cedente, Sacado, Boleto);

            // Gera um registro
            Boleto.SacadoInit(Sacado); // obrigatório para o registro
            r.Boletos.Add(Boleto, null);

        }

        // o numero de exemplo '123' é apenas um numero de teste
        // este numero é muito importante que seja gerado de forma exclusiva e sequencial
        txtRemessa.Text = r.Remessa(); //r.CNAB400(123);

    }

    void CustomRegBoleto(CNAB cnab, IReg reg, BoletoInfo boleto)
    {
        // é possivel definir campos adicionais como descontos, jurus, protesto e quaisquer outros via evento
        // estes campos adicionais não estão na classe basica de emissão sem registro: BoletoInfo, e devem ser definidos via evento
        // veja a documentação de cada banco e atribua os campos necessários
        // o arqumento "reg" é um tipo de Registro baseado no enumerador do banco registro: Reg<T> (é um template cuidado)
        if (reg.GetType() == typeof(Reg<CNAB400Remessa1Bradesco>)) // Já como esse é um exemplo generico apenas valido o tipo
        {
            var regBoleto = reg as Reg<CNAB400Remessa1Bradesco>;
            regBoleto[CNAB400Remessa1Bradesco.Condicao] = 1; // Para bo banco imprimir e enviar o boleto
            regBoleto[CNAB400Remessa1Bradesco.Instrucao1] = 6; // Indica para Protestar
            regBoleto[CNAB400Remessa1Bradesco.Instrucao2] = 15; // Numero de dias apos o vencimento para o protesto
            regBoleto[CNAB400Remessa1Bradesco.Avalista] = "!Alterado!";
        }
    }

    protected void btnRetorno_Click(object sender, EventArgs e)
    {   // apenas alguns campos serão exibidos, e serão re-gerados os boletos baseados nos dados do retorno

        if (string.IsNullOrEmpty(txtRetorno.Text.Trim()))
            return;

        //CNAB400Bradesco r = new CNAB400Bradesco(); //MapPath("CB210900.RET"));
        // CNAB400Itau r = new CNAB400Itau(); //MapPath("CB210900.RET"));
        // r.Retorno(txtRetorno.Text);
        //CedenteInfo cedente = r.Cedente;
        Label lbl = new Label();
        //dvBoletos.Controls.Add(lbl);
        //lbl.Text = "Cedente: " + cedente.Cedente + "<br/>";

        var r = new LayoutBancos();
        r.Init(Cedente);

        // ATENÇÃO: Um boleto pago vencido as vezes pode conter 2 registor, um altedando a data de vencimento e outro efetivando a baixa
        // r.ErroType = BoletoDuplicado.Ignore
        r.ErroType = BoletoDuplicado.Lista;


        int nLoops = 0;
        string cLinhas = txtRetorno.Text;
        do
        {
            nLoops++;
            lbl = new Label();
            dvBoletos.Controls.Add(lbl);
            lbl.Text = "<b>Loop: " + nLoops + "</b><br/>";

            r.Boletos.Clear();
            r.Retorno(cLinhas);

            foreach (string nn in r.Boletos.NossoNumeros)
            {
                lbl = new Label();
                dvBoletos.Controls.Add(lbl);

                var Boleto = r.Boletos[nn];
                lbl.Text = string.Format("{0} {1:dd/MM/yyyy} {2:C} {3}<br/>\r\n", Boleto.NossoNumero, Boleto.DataVencimento, Boleto.ValorDocumento, Boleto.Ocorrencia);
            }

            if (r.Boletos.Duplicados != null && r.Boletos.Duplicados.Count>0)
            {
                lbl = new Label();
                dvBoletos.Controls.Add(lbl);
                lbl.Text = "<b>Duplicados:</b><br/>";
                foreach (BoletoInfo Boleto in r.Boletos.Duplicados)
                {
                    lbl = new Label();
                    dvBoletos.Controls.Add(lbl);
                    lbl.Text = string.Format("{0} {1:dd/MM/yyyy} {2:C} {3}<br/>\r\n", Boleto.NossoNumero, Boleto.DataVencimento, Boleto.ValorDocumento, Boleto.Ocorrencia);
                }
                break;
            }
            else
            {
                // Verifica se mudou alguma coisa  (r.ErroType = BoletoDuplicado.Ignore)
                if (cLinhas != r.Boletos.ErroLinhas)
                    // linhas que precisariam ser reprocessadas separadamente por qualquer motivo
                    cLinhas = r.Boletos.ErroLinhas;
                else
                    break;
            }

        } while (!string.IsNullOrEmpty(cLinhas) && nLoops < 10); // Coloca um valor maximo por segurança
    }
}