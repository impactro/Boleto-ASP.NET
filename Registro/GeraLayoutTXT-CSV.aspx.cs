using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Impactro.Cobranca;
using System.IO;

public partial class Registro_GeraLayoutTXT_CSV : System.Web.UI.Page
{
    protected void btnRun_Click(object sender, EventArgs e)
    {
        try
        {
            string[] linhas = txtLayout.Text.Replace("\r", "").Split('\n');
            DataTable tb = new DataTable();
            int n, c;
            DataRow row;
            if (rblTipo.SelectedValue == "l")
            {
                int nColunas = CobUtil.GetInt(txtPrm.Text);
                if (nColunas < 4)
                {
                    ltrOut.Text = "É preciso ter pelo menos 4 campos: Nome, Comentário, Tamanho/Posições, Tipo";
                    return;
                }
                if (linhas.Length / nColunas < 3)
                {
                    ltrOut.Text = "O arquivo precisa conter pelo menos 3 registros alem do cabeçalho";
                    return;
                }

                for (n = 0; n < nColunas; n++)
                    tb.Columns.Add(linhas[n], typeof(string));

                row = tb.NewRow();
                n = nColunas;
                c = 0;
                while (n < linhas.Length)
                {
                    row[c] = linhas[n].Trim();
                    n++;
                    c++;
                    if (c == nColunas)
                    {
                        c = 0;
                        tb.Rows.Add(row);
                        row = tb.NewRow();
                    }
                }
                ltrOut.Text = "OK";
                gvCSV.DataSource = tb;
                gvCSV.DataBind();

                string cFile = "layout-" + rblTipo.SelectedValue + ".csv";
                ltrOut.Text = "OK arquivo gerado: <a href='" + cFile + "' download>" + cFile + "</a>";
                string cCSV = CSV.TableCSV(tb, "|");
                File.WriteAllText(MapPath(cFile), cCSV);

            }
            else // por separador ... tipo csv...
            {
                if (linhas.Length < 3)
                {
                    ltrOut.Text = "O arquivo precisa conter pelo menos 3 registros alem do cabeçalho";
                    return;
                }

                char sep = txtPrm.Text[0];
                string[] col = linhas[0].Split(sep);
                int nColunas = col.Length;
                if (nColunas < 4)
                {
                    ltrOut.Text = "É preciso haver pelo menos os 4 campos basicos";
                    return;
                }

                for (c = 0; c < nColunas; c++)
                    tb.Columns.Add(col[c], typeof(string));

                n = 1;
                while (n < linhas.Length)
                {
                    col = CSV.SepararCampos(linhas[n],sep);
                    row = tb.NewRow();
                    for (c = 0; c < nColunas && c<col.Length; c++)
                        row[c] = col[c];

                    n++;
                    tb.Rows.Add(row);
                }
                ltrOut.Text = "OK";
                gvCSV.DataSource = tb;
                gvCSV.DataBind();

                string cFile = "layout-" + rblTipo.SelectedValue + ".csv";
                ltrOut.Text = "OK arquivo gerado: <a href='" + cFile + "' download>" + cFile + "</a>";
                string cCSV = CSV.TableCSV(tb, "|");
                File.WriteAllText(MapPath(cFile), cCSV);
            }
        }
        catch (Exception ex)
        {
            ltrOut.Text = "<b>" + ex.Message + "</b><br/><pre>" + ex.StackTrace + "</pre>";
        }
    }
}