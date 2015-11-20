<%@ Page Language="VB" %>
<%@ Register src="Rodape.ascx" tagname="Rodape" tagprefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BoletoASP.Net: Criando um Boletos em imagens com arquivos ASHX</title>
    <meta name="Description" content="É possivel criar uma imagem do boleto para enviar por e-mail, ou exibi-lá como imagem usando arquivos .ASHX" />
    <meta name="keywords" content="BoletoASP.Net, Boleto ASP, Boleto, ASP.Net, Título de Cobrança, IPTE, ASHX, imagens, boleto imagem, imagem do boleto"/>
    <style type="text/css">
        body
        {
        	font-family: Verdana;
        	font-size: 10pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p>Você pode precisar gerar o boleto dentro de uma imagem, seja para enviar por e-mail, ou por qualquer outro motivo, temos aqui um exemplo do boleto fechado dentro de um GIF<br>
    Recomendamos o uso do GIF, por ser um arquivo com compactação de precisão, não recomandamos o uso de JPEG pois o código de barras fica ilegicel para uso de leitoras</p>
    <img src="BoletoImagem1.ashx" alt="Imagem do Boleto, compre o componente e adquira o fonte deste gerador de imagens" />
    </form>
    <uc1:Rodape ID="Rodape1" runat="server" />
</body>
</html>
