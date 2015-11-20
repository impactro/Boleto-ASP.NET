<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeraLayoutCSV-CS.aspx.cs" Inherits="Registro_GeraLayoutCSV_CS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Converte um Layout.CSV (editado) em um 'enum' de layout</h1>
        Nome do Arquivo: 
        <asp:TextBox runat="server" ID="txtArquivo" Width="50%">layout-s.csv</asp:TextBox>
        <asp:Button runat="server" ID="btnGerar" Text="Gerar Enumerador de layout" OnClick="btnGerar_Click" /><br/>
        <asp:Literal runat="server" ID="ltrOut"></asp:Literal>
    </div>
    </form>
</body>
</html>
