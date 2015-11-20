using System;
using System.Web.UI.WebControls;
using Impactro.Cobranca;
using Impactro.WebControls;
using Impactro.Layout;

public partial class CNAB_Form : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int nDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            txtVencimento.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, nDay).ToShortDateString();
        }
    }

    protected void btnRemessa_Click(object sender, EventArgs e)
    {   // (Não é o foco validar dados de entrada, e sim testar a geração de registro)

        // Definição dos dados do cedente - QUEM RECEBE / EMITE
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "Exemplo de empresa cedente";
        Cedente.Banco = "237";
        Cedente.Agencia = "1510";
        Cedente.Conta = "001466-4";
        Cedente.Carteira = "09";
        Cedente.Modalidade = "05";
        Cedente.CedenteCOD = "00000000000004047726"; // 20 digitos

        //Definição dos dados do sacado
        SacadoInfo Sacado = new SacadoInfo();
        Sacado.Sacado = "Pedro Alvarez Cabral";
        Sacado.Documento = "123.123.134-12";
        Sacado.Endereco = "Rua 21 de Abril";
        Sacado.Cidade = "São Paulo";
        Sacado.Bairro = "Vera Cruz";
        Sacado.Cep = "98765-000";
        Sacado.UF = "SP";
        Sacado.Email = "fabio@impactro.com.br";

        //Definição das Variáveis do boleto
        BoletoInfo Boleto;

        //WebControl
        BoletoWeb blt;

        // define o emissor da remessa
        // Remessa r = new Remessa(Cedente);
        CNAB400Bradesco r = new CNAB400Bradesco();
        r.Cedente = Cedente;
        // O lote padrão gera AADDDHH (Ano, Dia do ano, Hora)
        r.NumeroLote += 2000000; // inicia com 3 o numero do lote! (soma 20 anos)

        //r.ShowDumpReg = true;

        // customiza campos para Bradesco
        // r.onRegItem += new RemessaReg(r_onRegItem);

        for (int n = 0; n < Int32.Parse(txtQTD.Text); n++)
        {
            Boleto = new BoletoInfo();
            Boleto.BoletoID = n;
            Boleto.NossoNumero = (Int32.Parse(txtNossoNumero.Text) + n).ToString();
            Boleto.NumeroDocumento = Boleto.NossoNumero;
            Boleto.ValorDocumento = double.Parse(txtValor.Text) + n;
            Boleto.DataDocumento = DateTime.Now;
            Boleto.DataVencimento = DateTime.Parse(txtVencimento.Text).AddDays(n);
            Boleto.Instrucoes = "Todas as informações deste bloqueto são de exclusiva responsabilidade do cedente";

            blt = new BoletoWeb();
            dvBoletos.Controls.Add(blt);

            blt.ImagePath = "../imagens/"; // Define o diretório de imagens
            blt.ExibeReciboSacado = false; // Apenas para exibir a parte que interessa do boleto
            blt.CssCell = "BolCell";
            blt.CssField = "BolField";

            // Gera um boleto
            blt.MakeBoleto(Cedente, Sacado, Boleto);

            // Gera um registro
            Boleto.SacadoInit(Sacado); // obrigatório para o registro
            r.Boletos.Add(Boleto,null);

        }

        // o numero de exemplo '123' é apenas um numero de teste
        // este numero é muito importante que seja gerado de forma exclusiva e sequencial
        txtRemessa.Text = r.Remessa(); //r.CNAB400(123);
        
    }

    void r_onRegItem(BoletoInfo boleto, object eRegT)
    {
        // é possivel definir campos adicionais como descontos, jurus, protesto e quaisquer outros via evento
        // estes campos adicionais não está na classe basica de emissão sem registro: BoletoInfo, e devem ser definidos via evento
        // veja a documentação de cada banco e atribua os campos necessários
        // o arqumento "eReg" é um tipo de Registro baseado no enumerador do banco registro: Reg<T> (é um template cuidado)
        Reg<CNAB400Remessa1Bradesco> regBoleto = (Reg<CNAB400Remessa1Bradesco>)eRegT;
        regBoleto[CNAB400Remessa1Bradesco.Condicao] = 1; // Para bo banco imprimir e enviar o boleto
        regBoleto[CNAB400Remessa1Bradesco.Instrucao1] = 6; // Indica para Protestar
        regBoleto[CNAB400Remessa1Bradesco.Instrucao2] = 15; // Numero de dias apos o vencimento para o protesto
    }

    protected void btnRetorno_Click(object sender, EventArgs e)
    {   // apenas alguns campos serão exibidos, e serão re-gerados os boletos baseados nos dados do retorno

        if (string.IsNullOrEmpty(txtRetorno.Text.Trim()))
            return;

        //CNAB400Bradesco r = new CNAB400Bradesco(); //MapPath("CB210900.RET"));
        CNAB400Itau r = new CNAB400Itau(); //MapPath("CB210900.RET"));
        r.Retorno(txtRetorno.Text);
        //CedenteInfo cedente = r.Cedente;
        Label lbl=new Label();
        //dvBoletos.Controls.Add(lbl);
        //lbl.Text = "Cedente: " + cedente.Cedente + "<br/>";
        
        BoletoInfo Boleto;
        foreach (string nn in r.Boletos.NossoNumeros)
        {
            lbl = new Label();
            dvBoletos.Controls.Add(lbl);

            Boleto = r.Boletos[nn];
            lbl.Text = string.Format("{0} {1:dd/MM/yyyy} {2:C} <br/>\r\n", Boleto.NossoNumero, Boleto.DataVencimento, Boleto.ValorDocumento);

        }

    }
}
