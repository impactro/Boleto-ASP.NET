using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Impactro.WebControls;
using Impactro.Cobranca;

public partial class GeraVarios : System.Web.UI.Page
{
    // Este exemplo foi baseado no exemplo: DirectPrint.aspx
    // Para criação de controles, os objetos devem ser incluidos sempre no Page_Init para não corromper a viewstate
    protected void Page_Init(object sender, EventArgs e)
    {

        DataTable tbDados;
        BoletoWeb blt;
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

        int nBoleto = 0;
        foreach (DataRow row in tbDados.Rows)
        {
            // o principal é inserir o componente dinakicamente dentro de um container (controle que suporte outros controles, exemplo: Form, Panel)
            blt = new BoletoWeb();

            // para caber 2 em uma página não será usado o recibo do sacado
            blt.ExibeReciboSacado = false;
            
            // configura as propriedades de estilo
            blt.CssCell = "BolCell";
            blt.CssField = "BolField";

            // Adiciona a instancia na tela
            form1.Controls.Add(blt);
            nBoleto++;


            // Mude o ZOOM do CSS BoletoWeb no arquivo .aspx para caber até 3 boletos, ai o módulo da divisão passa a ser 3
            //if (nBoleto % 3 == 0) // Mas aplicar muito zoom pode dar problema na leitura do código de barras
            if (nBoleto % 2 == 0)
                // somente nos boletos pares a iniciar de 2 força uma quebra de linha
                form1.Controls.Add(new LiteralControl("<div style='page-break-after: always'><br/></div>"));
            else
                // nos boletos impares adiciona a imagem de recorte
                form1.Controls.Add(new LiteralControl("<img src='imagens/corte.gif'>"));

            // Note que na página ASPX, existe uma referencia a classe "BoeltoWeb"
            // Nesta classe CSS é definido o espaçamento entra cada boleto e a quebra de página

            // Depois segue normalmente as definições estaticas ou vindas do banco de dados
            
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
            Sacado.Sacado = (string)row["Nome"];

            // Definição das Variáveis do boleto
            BoletoInfo Boleto = new BoletoInfo();
            Boleto.DataVencimento = (DateTime)row["Vencimento"];
            Boleto.ValorDocumento = (double)row["Valor"];
            Boleto.NossoNumero = row["NossoNumero"].ToString();
            Boleto.NumeroDocumento = Boleto.NossoNumero;

            blt.MakeBoleto(Cedente, Sacado, Boleto);

        }
    }
}
