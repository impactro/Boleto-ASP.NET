<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AntiVirus.aspx.cs" Inherits="BoletoNet_AntiVirus" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cob:BoletoWeb id="bltPag" runat="server" CssCell="BolCell" CssField="BolField" RenderCountImage="1" />
    </div>
    </form>
</body>
</html>
