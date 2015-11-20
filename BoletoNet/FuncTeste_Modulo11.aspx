<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuncTeste_Modulo11.aspx.cs" Inherits="FuncTeste_Modulo11" %>
<%@ Register src="Rodape.ascx" tagname="Rodape" tagprefix="uc1" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>BoletoASP.Net: Calculo do Modulo 11 em C# base 7 ou 9</title>
    <meta name="Description" content="O Calculo Modulo 11 para emissão de boletos é relativamente complexo, pois cada banco usa de forma diferente, mas basicamente nossa rotina em C# suporta as 4 formas" />
    <meta name="keywords" content="BoletoASP.Net, Boleto ASP, Boleto, ASP.Net, Título de Cobrança, C#, Modulo 11, código de barras"/>
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
    <p>O Calculo do modulo 11 precisa de 2 parametros, o primeiro é o numero a ser usado para se gerar o calculo, e o segundo é a BASE que geramente é 7 ou 9<br/>
        Algusn bancos também usam logicas diferentes quando o resto do calculo da 10, algusn bancos transformam isso em ZERO e outro em UM, por isso segue abaixo o resultado com os 2 tipos de rotina</p>
        Valor: <asp:TextBox ID="txtValor" runat="server" Width="200px">739</asp:TextBox> 
        Modulo: <asp:TextBox ID="txtModulo" runat="server" Width="30px">9</asp:TextBox>
        <asp:Button ID="btnPadrao" runat="server" Text="Modulo 11 Padrão" OnClick="btnPadrao_Click" />
        <asp:Button ID="btnEspecial" runat="server" Text="Modulo 11 Especial" OnClick="btnEspecial_Click" /><br>
        <br>
        <b>Resultado: <asp:Label ID="lblDigito" runat="server" Text="?"></asp:Label></b>
        <br><uc1:Rodape ID="Rodape1" runat="server" />
        <hr>
    <asp:Label ID="lblResult" runat="server" Font-Names="Courier New"></asp:Label>
        
    </form>
</body>
</html>
