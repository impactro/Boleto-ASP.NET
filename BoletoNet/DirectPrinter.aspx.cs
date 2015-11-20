using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using System.Data;
using Impactro.WindowsControls;
using Impactro.Cobranca;

// este exemplo é baseado no "frmBasico.cs" para WindowsForm
// A classe BoletoForm é responsável em gerar boletos em GDI, que pode ser usado para geração de imagem em arquivo ou documento impresso: veja também o exemplo BoletoImagem.ashx
public partial class DirectPrinter : System.Web.UI.Page
{
    DataTable tbDados;
    int nReg = 0;
    BoletoForm blt;

    // Pode parecer estranho..., mas um site WEB pode imprimir conteudo direto na impressora
    // Isso pois o aplicativo é simplesmente um programa convencional.
    // Mas para que isso funciona deve existir uma impressora pdrão conectada ao servidor WEB
    // É logico que isso não irá funcionar em provedores de hostings, apenas em WEB SERVERs em redes locais
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        // Para imprimir dados vindo de uma tabela de um banco de dados
        // é preciso definir conexões ao banco, com senhas, executar SELECTs.
        // Neste exemplo abaixo estou criando 5 registros em memoria e 
        // recalculando o boleto para cada página impressa
        // Customize de acordo com suas necessidades, pois este é apenas um exemplo 
        // basico por isso serão utilizados apenas poucos campos.

        blt = new BoletoForm();
        tbDados = new DataTable(); // Cria  atabela em memoria

        // Cria as colunas nos respectivos tipos
        tbDados.Columns.Add("Nome", typeof(string));
        tbDados.Columns.Add("Vencimento", typeof(DateTime));
        tbDados.Columns.Add("Valor", typeof(double));
        tbDados.Columns.Add("NossoNumero", typeof(int));

        // insere os dados
        tbDados.Rows.Add("Fábio", new DateTime(2008, 12, 30), 123.45, 345678);
        tbDados.Rows.Add("Érika", new DateTime(2008, 7, 25), 60, 12332);
        tbDados.Rows.Add("Milena", new DateTime(2008, 10, 20), 10.30, 234);
        tbDados.Rows.Add("Cecília", DateTime.MinValue, 200.55, 456445);
        tbDados.Rows.Add("qualquer um", new DateTime(2008, 2, 12), 7890.5, 56756);

        // posiciona o registro atual
        nReg = 0;
        PrintDocument pDoc = new PrintDocument();

        // ATENÇÃO: IMPORTANTE!!!
        // ======================
        pDoc.PrinterSettings.PrinterName = "EPSON Stylus CX5600 Series";
        // É necessário definir o nome da impressora instlada, exatamente com o nome que é exibido no windows.
        // A impressora do usuários ASPNET é diferente do seu usuários atualmente logado!

        pDoc.PrintPage += new PrintPageEventHandler(pDoc_PrintPageTabela);
        pDoc.Print();
    }

    //// Para imprimir um unico boleto
    //void pDoc_PrintPageUnico(object sender, PrintPageEventArgs e)
    //{
    //    blt.PrintType = PrintTypes.Documet;
    //    blt.Print(e.Graphics);
    //}

    // Para imprimir uma serie de boletos onde os dados estão vindo de um datatable
    void pDoc_PrintPageTabela(object sender, PrintPageEventArgs e)
    {
        try
        {

            // Definição dos dados do cedente
            CedenteInfo Cedente = new CedenteInfo();
            Cedente.Cedente = "outro cedente!";
            Cedente.Banco = "237";
            Cedente.Agencia = "1234-5";
            Cedente.Conta = "123456-7";
            Cedente.Carteira = "06";
            Cedente.Modalidade = "11";

            // Definição dos dados do sacado
            SacadoInfo Sacado = new SacadoInfo();
            Sacado.Sacado = (string)tbDados.Rows[nReg]["Nome"];

            // Definição das Variáveis do boleto
            BoletoInfo Boleto = new BoletoInfo();
            Boleto.DataVencimento = (DateTime)tbDados.Rows[nReg]["Vencimento"];
            Boleto.ValorDocumento = (double)tbDados.Rows[nReg]["Valor"];
            Boleto.NossoNumero = tbDados.Rows[nReg]["NossoNumero"].ToString();
            Boleto.NumeroDocumento = Boleto.NossoNumero;

            // Cria uma nova instancia totalmente idependente
            BoletoForm bol = new BoletoForm();
            // monta o boleto com os dados específicos nas classes
            bol.MakeBoleto(Cedente, Sacado, Boleto);
            bol.PrintType = PrintTypes.Documet;
            bol.Print(e.Graphics);

            nReg++;
            e.HasMorePages = nReg < tbDados.Rows.Count;
        }
        catch (Exception)
        {
        }
    }
}
