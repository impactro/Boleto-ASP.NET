<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CNAB-Remessa3.aspx.vb" Inherits="Registro_CNAB_Remessa3" %>
<%@ Register Assembly="Impactro.Cobranca" Namespace="Impactro.WebControls" TagPrefix="cob" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Geração de Arquivos de Remessa para Boletos com Registro e DDA (exemplo funcional)</title>
    <style type="text/css">
		.BolCell { font-size: 7pt; font-family: Verdana; }
	    .BolField { font-weight: bold; font-size: 8pt; font-family: Verdana; }
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Geração de Arquivo de Remessa CNAB240/CNAB400 para Boletos/DDA</h1>
        <p>
            O código fonte deste programa está disponivel no <a href="https://github.com/impactro/Boleto-ASP.NET/tree/master/Registro" target="_blank">GITHUB.com/IMPACTRO</a>.<br/>
            <a href="https://github.com/impactro/Boleto-Test/wiki/Exemplo-de-Remessa-CNAB">Acesse a WIKI para mais informações</a><br/>
            <small>Essa é a versão resumida onde todas váriáveis estão fixas no código, <a href="CNAB-Remessa.aspx">veja o exemplo completo aqui</a></small>
        </p>

        <asp:Panel runat="server" ID="pnlLogin">
            Chave de acesso: <asp:TextBox runat="server" ID="txtSenha" />
            <asp:Button runat="server" id="btnEntrar" OnClick="btnEntrar_Click" Text="Entrar" />
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlDados" Visible="false">
            <asp:Label runat="server" ID="lblInfoSQL"/>
            <br/>Periodo de <asp:TextBox runat="server" ID="txtInicio" /> a <asp:TextBox runat="server" ID="txtFim" />
            <asp:Button runat="server" id="btnGerar" OnClick="btnGerar_Click" Text="Filtrar" />
            <asp:GridView runat="server" ID="gvBanco" EnableViewState="false"/>
            <h3>Visualização do Arquivo e Boleto</h3>
            <asp:TextBox runat="server" ID="txtRemessa" TextMode="MultiLine" Width="90%" Height="100" Wrap="false" EnableViewState="false" />
            <br/>
            Nome e Valores dos campos internos:<br />
            <asp:GridView runat="server" ID="gvCampos" EnableViewState="false"/>
            <br/>
            <asp:Panel runat="server" ID="pnlBoletos" EnableViewState="false"/>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
