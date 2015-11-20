<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GeraLayoutTXT-CSV.aspx.cs" Inherits="Registro_GeraLayoutTXT_CSV" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Converte o texto colado do PDF em um CSV com o layout</h1>
        <asp:TextBox runat="server" ID="txtLayout" Wrap="false" TextMode="MultiLine" Width="95%" Height="200"></asp:TextBox><br/>
        Tipo: 
        <asp:RadioButtonList runat="server" ID="rblTipo" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="l" Selected="True">Linhas</asp:ListItem>
            <asp:ListItem Value="s">Separador</asp:ListItem>
        </asp:RadioButtonList>
        Parametro: 
        <asp:TextBox runat="server" ID="txtPrm" MaxLength="1" Width="20px">5</asp:TextBox>
        <asp:Button runat="server" ID="btnRun" Text="Processar" OnClick="btnRun_Click"/><br/>
        <asp:Literal runat="server" ID="ltrOut"></asp:Literal>
        <asp:DataGrid runat="server" ID="gvCSV">
            <HeaderStyle BackColor="#aaaaaa" Font-Bold="true" />
        </asp:DataGrid>
    </div>
    </form>
</body>
</html>
