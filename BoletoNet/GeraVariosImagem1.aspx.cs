using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Impactro.WebControls;
using Impactro.Cobranca;
using System.Drawing.Imaging;
using System.Drawing;

public partial class GeraVariosImagem1 : System.Web.UI.Page
{
    // Neste primeiro exemplo o boleto será inserido como imagem embutida no HTML em base64
    protected void Page_Init(object sender, EventArgs e)
    {
        // Definição dos dados do cedente, que será comum para todos os boletos
        CedenteInfo Cedente = new CedenteInfo();
        Cedente.Cedente = "outro cedente!";
        Cedente.Banco = "237";
        Cedente.Agencia = "1234-5";
        Cedente.Conta = "123456-7";
        Cedente.Carteira = "06";
        Cedente.Modalidade = "11";

        // Cria uma tabela em memoria
        DataTable tbDados = new DataTable(); 

        // Estrutura da tabela
        tbDados.Columns.Add("Nome", typeof(string));
        tbDados.Columns.Add("Vencimento", typeof(DateTime));
        tbDados.Columns.Add("Valor", typeof(double));
        tbDados.Columns.Add("NossoNumero", typeof(int));

        // Insere os dados
        tbDados.Rows.Add("Fábio", new DateTime(2015, 12, 30), 123.45, 345678);
        tbDados.Rows.Add("Érika", new DateTime(2015, 7, 25), 60, 12332);
        tbDados.Rows.Add("Milena", new DateTime(2015, 10, 20), 10.30, 234);
        tbDados.Rows.Add("Cecília", new DateTime(2015, 3, 4), 20.53, 456445);
        tbDados.Rows.Add("Roberto", new DateTime(2015, 6, 5), 32.78, 47319);
        tbDados.Rows.Add("Marcelo", DateTime.MinValue, 20320.23, 18445);
        tbDados.Rows.Add("Ricardo", DateTime.MinValue, 97023.51, 2465445);
        tbDados.Rows.Add("Maria", new DateTime(2016, 9, 12), 7890.23, 61756);
        tbDados.Rows.Add("Samara", new DateTime(2015, 8, 12), 78.1, 656);
        tbDados.Rows.Add("Marcio", new DateTime(2015, 2, 10), 790.3, 5672);

        int nBoleto = 0;
        foreach (DataRow row in tbDados.Rows)
        {
            // Instancia do 'Boleto', não o 'BoletoWeb', pois a ideia é renderizar imagem
            // O BoletoWeb usa a classe 'Boleto' para fazer todos os calculos, e depois desenha em html o boleto
            Boleto blt = new Boleto();
            blt.Carne = true; // Formato de Carne, neste exemplo será colocardo 3 boletos por página

            // Definição dos dados do sacado
            SacadoInfo Sacado = new SacadoInfo();
            Sacado.Sacado = (string)row["Nome"];

            // Definição das Variáveis do boleto
            BoletoInfo Boleto = new BoletoInfo();
            Boleto.DataVencimento = (DateTime)row["Vencimento"];
            Boleto.ValorDocumento = (double)row["Valor"];
            Boleto.NossoNumero = row["NossoNumero"].ToString();
            Boleto.NumeroDocumento = Boleto.NossoNumero;

            // Calcula os dados do boleto
            blt.MakeBoleto(Cedente, Sacado, Boleto);
            
            // Obtem a imagem do boleto
            Bitmap img = blt.ImageBoleto();
            
            // Adiciona a imagem do boleto em base64 no HTML
            form1.Controls.Add(new LiteralControl(CobUtil.ToBase64ImageTag(img, ImageFormat.Png)));

            // incrementa o contador de boletos
            nBoleto++;
            if (nBoleto % 3 == 0) // Mas aplicar muito zoom pode dar problema na leitura do código de barras
                // somente nos boletos pares a iniciar de 2 força uma quebra de linha
                form1.Controls.Add(new LiteralControl("<div style='page-break-after: always'><br/></div>"));
            else //  if (nBoleto<tbDados.Rows.Count) // caso não queira imprimir a ultima imagem de tesoura
                // nos boletos impares adiciona a imagem de recorte, ou um HR
                // form1.Controls.Add(new LiteralControl("<img src='imagens/corte.gif' style='margin: 5px 0 5px 0;'/>"));
                form1.Controls.Add(new LiteralControl("<hr size='1' style='color: gray; margin: 5px 0 5px 0;'/>"));
        }
    }
}
