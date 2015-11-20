using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

public partial class Registro_GeraLayoutCSV_CS : System.Web.UI.Page
{
    protected void btnGerar_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtArquivo.Text))
            {
                ltrOut.Text = "Informe o nome do arquivo com o layout CSV editado";
                return;
            }
            string cFile = MapPath(txtArquivo.Text);
            if (!File.Exists(cFile))
            {
                ltrOut.Text = "Arquivo não existe";
                return;
            }
            CSV csv = new CSV();
            csv.Load(cFile);

            DataTable tb = csv.data;
            if (tb.Columns.Count < 4)
            {

                ltrOut.Text = "É preciso ter ao menos 4 colunas";
                return;
            }

            StringBuilder sb = new StringBuilder();
            string cCampo;
            string cTamanho;
            string cTipo;
            string cRegFormat;
            int nPos = 1;

            sb.AppendLine("[RegLayout(@\"^1\")]");
            sb.AppendLine("public enum GenerateLayout \n{");

            // local dos campos
            int nPosNome = 1;
            int nPosComentario = 2;
            int nPosTamanho = 0;
            int nPosTipo = 3;

            foreach (DataRow row in tb.Rows)
            {
                // Campo1: Nome do campo
                cCampo = (string)row[nPosNome];
                if (!Regex.IsMatch(cCampo, @"[A-Za-z]\w+$"))
                {
                    ltrOut.Text = "Nome invalido na posição " + nPos + " - Campo: " + cCampo;
                    return;
                }
                // Campo2: Comentário descritivo do campo
                if (!string.IsNullOrEmpty((string)row[nPosComentario]))
                    sb.AppendLine("\t/// &lt;summary&gt;\n\t/// " + row[nPosComentario] + " \n\t/// &lt;/summary&gt;");

                // Campo3: Posição Inicial/final
                cRegFormat = "[RegFormat(RegType.P";
                // Colocar o numero da linha que define o tamanho
                cTamanho = (string)row[nPosTamanho]; 
                // Há documentações que é informado o tamanho e outros que é informado o numero de caracteres iniciais e final, aqui vou tratar os 2 casos
                string[] c = cTamanho.Replace(" a ", " ").Split(' ');
                if (c.Length >= 2)
                    cTamanho = (CobUtil.GetInt(c[1]) - CobUtil.GetInt(c[0]) + 1).ToString();

                else if (c.Length == 1)
                    cTamanho = c[0];

                else
                {
                    ltrOut.Text = "O tamanho não pode ser identificado '" + cTamanho + "' no Campo: " + cCampo;
                    return;
                }

                // Campo4: Tipo de Campo
                cTipo = (string)row[nPosTipo]; // Só será usado a primeira letra, edite no Excel/google doscs, o no CSV direto possíveis conversões de numeros para data ou valor
                cTipo = cTipo.Substring(0, 1).ToUpper(); // Desde a época do Cobol os tipos de dado e a forma de representa-los é muito parecido
                // 9 - Valores numéricos inteiros
                // V - Valores mão inteiros (double)
                // D - Datas
                // X - Textos
                // H - Hora
                if (!Regex.IsMatch(cTipo, "[9VDXHNA]"))
                {
                    ltrOut.Text = "Tipo Inválido '" + cTipo + "' no Campo: " + cCampo;
                    return;
                }
                if (cTipo == "N")
                    cTipo = "9";
                else if (cTipo == "A")
                    cTipo = "X";

                cRegFormat += cTipo; // Sendo que os tipos válidos são: 
                cRegFormat += ", " + cTamanho;

                // Campo5: Valor padrão do campo (caso exista)
                //if (tb.Columns.Count > 4 && !string.IsNullOrEmpty((string)row[4]))
                //    cRegFormat += ", Default =\"" + row[4] + "\"";

                // Fecha o atributo e coloca um informativo dos tamanhos de posição atual
                cRegFormat += ")] // " + nPos;
                nPos += CobUtil.GetInt(cTamanho);
                cRegFormat += "-" + (nPos - 1);

                // Por fim adiciona o campo
                sb.AppendLine("\t" + cRegFormat + "\n\t" + cCampo + ",\n");
                
            }
            sb.AppendLine("}");
            ltrOut.Text = "Copie e cole o código abaixo em um arquivo .CS<br/><hr/><pre>" + sb.ToString() + "</pre>";
        }
        catch (Exception ex)
        {
            ltrOut.Text = "<b>" + ex.Message + "</b><br/><pre>" + ex.StackTrace + "</pre>";
        }
    }
}