using System;
using Impactro.Layout;

public partial class RPS_NFe2 : System.Web.UI.Page
{
    // Exemplo de geração registro a registro, usando as classes de NFe (veja mais informações no exemplo basico 1 em VB)
    // Neste exemplo está mesclado o uso das extruturas REG<NfeN...> e a utilizaçào da classe NFe
    protected void btnTest_Click(object sender, EventArgs e)
    {

        RPSLote rps = new RPSLote(36831018);

        rps.Itens.Add(1, "12345687912", "Fábio Ferreira de Souza", 2682, 0, "Teste RPS/NF-e", 10  , 0, false, DateTime.Now );
        rps.Itens.SetEndereco(1, "Rua Nome da Rua, 123 ap 12", null, null);
        rps.Itens[1][RPS2Detalhe.Bairro] = "Brás";
        rps.Itens[1][RPS2Detalhe.Cidade] = "São Paulo";
        rps.Itens[1][RPS2Detalhe.UF] = "SP";
        rps.Itens[1][RPS2Detalhe.CEP] = "12345123";

        txtOut.Text = rps.Cabecalho.Line + "\r\n";

        foreach (int n in rps.Itens.Numeros)
        {
            txtOut.Text += rps.Itens[n].Line + "\r\n";
        }

        txtOut.Text += rps.Rodape.Line + "\r\n";

    }
}