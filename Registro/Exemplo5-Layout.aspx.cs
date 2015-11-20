using System;
using Impactro.Layout;

public partial class Registro_Exemplo5_Layout : System.Web.UI.Page
{
    protected void btn_Click(object sender, EventArgs e)
    {
        try
        {
            // Para finalizar o Reg<T> trata um unico registro
            // a Classe 'Layout' é capaz de identificar e gerar multiplos tipos de registros      
            // este exemplo é otimo para comparar linhas geradas e assim fazer um DEBUG

            Layout l;
            if (rblTipo.SelectedIndex == 0) // remessa
                //l = new Layout(typeof(CNAB400Header1Bradesco), typeof(CNAB400Remessa1Bradesco), typeof(CNAB400ArquivoTrailer));
                l = new Layout(typeof(CNAB240HeaderLoteCaixa), typeof(CNAB240SegmentoPCaixa), typeof(CNAB240SegmentoQCaixa));
            else // retorno
                // l = new Layout(typeof(CNAB400Header1Bradesco), typeof(CNAB400Retorno1Bradesco), typeof(CNAB400ArquivoTrailer));
                l = new Layout(typeof(CNAB400SantanderHeader), typeof(CNAB400SantanderRemessa1), typeof(CNAB400SantanderTrailer));

            l.Conteudo = txt.Text;
            lbl.Text = "Processo OK";

            dtg1.DataSource = l.Table(l.GetLayoutType(0));
            dtg1.DataBind();

            dtg2.DataSource = l.Table(l.GetLayoutType(1));
            dtg2.DataBind();

            dtg3.DataSource = l.Table(l.GetLayoutType(2));
            dtg3.DataBind();

            lbl.Text = "Grids OK";
        }
        catch (Exception ex)
        {
            lbl.Text = ex.Message + "<pre>" + ex.StackTrace + "</pre>";
        }
    }
}