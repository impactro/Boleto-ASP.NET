<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BoletoCaixa.aspx.cs" Inherits="BoletoCaixa" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Boleto</title>
    <style type="text/css">
		.BolCell { font-size: 7pt; font-family: Verdana; }
	    .BolField { font-weight: bold; font-size: 8pt; font-family: Verdana; }
	</style>
</head>
<body>
    <form id="form1" runat="server">
        Código de Barras: <asp:Label runat="server" ID="CodBar"/>
        <cob:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" ></cob:BoletoWeb>
    </form>
</body>
</html>
