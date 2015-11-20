<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuncTeste_FatVenc.aspx.cs" Inherits="FuncTeste_FatVenc" %>
<%@ Register src="Rodape.ascx" tagname="Rodape" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BoletoASP.Net: Calculo do Fator de Vencimento com 4 digitos (uma bomba)</title>
    <meta name="Description" content="Saiba como calcular o fator de vencimento, que é representado por apenas 4 digitos, e saiba quando todos os sistemas de gerção de boletos irão parar de funcionar" />
    <meta name="keywords" content="BoletoASP.Net, Boleto ASP, Boleto, ASP.Net, Título de Cobrança, Calculo do Fator de Vencimento, Fator de Vencimento, vencimento, Calculo do Vencimento"/>
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
    <p>É facil calcular o fator de vencimento, é só contar o numero de dias desde 07/10/1997 e transformar isso em 4 digitos.<br/>
    Mas o que acontecerá, por exemplo no ano de <a href="#rodape">2025</a> ?!<br/>
    Extamente em <a href="#rodape">21/2/2025</a> será emitido o ultimo boleto, e a partir desta das todo sistema atual de boletos deixará de funcionar!<br/>
    Cabe a <a href="http://www.febraban.org.br/" target="_blank">FEBRABAN</a>, divulgar a nova formula para gerar o fator de vencimento.
    </p>
    <asp:Label ID="lblResult" runat="server" Font-Names="Courier New"></asp:Label>
    </form>
    <a name="rodape"></a>
<uc1:Rodape ID="Rodape1" runat="server" />
</body>
</html>
