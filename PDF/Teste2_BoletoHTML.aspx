<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Teste2_BoletoHTML.aspx.cs" Inherits="PDF_Teste2" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Boleto</title>
</head>
<body>
    <form id="form1" runat="server">
         <cob:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" ImageCorte="corte.gif"></cob:BoletoWeb>
    </form>
</body>
</html>