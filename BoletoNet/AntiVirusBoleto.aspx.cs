using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BoletoNet_AntiVirusBoleto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // em 2014 apareceu um virus que adultare boletos na internet.
        // o principal efeito é com boletos HTML, onde este virus altera o código de barras e linha digitável, 
        // e com isso o diheiro pago vai para a conta do bandido.
        // alguns clientes abriram boletim de cocorrencia informando fraude em seus sistemas mediante virus
        // eu juntamente com alguns clientes não temos muito o que fazer já que o problema ocorre na máquina do cliente.
        // O Uso de SSL pode ajudar, mas não resolve, já que muitas pessoas não saber valodar quando um site é ou não seguro de fato.
        // Por isso, o maximo que posso sugerir é o uso do boleto em forma de imagem seja dentro de um HTML ou PDF
        // Mas dentro de uma imagem, um virus mais elaborado poderia usar um OCR.
        // então para dificultar um pouco mais a ideia é quebrar um boleto em várias imagens

        // Etapa 1: Criar um boleto normal
        // Etapa 2: obter a imagem do boleto e savar em memória
        // Etapa 3: ler a imagem em memoria e remontar o boleto (aqui quanto mais completo for, melhor será quebra-cabeça)
    }
}