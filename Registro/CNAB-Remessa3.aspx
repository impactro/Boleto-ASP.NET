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
        <asp:Panel runat="server" ID="pnlLogin">
            <h1>Geração de Arquivo de Remessa CNAB240/CNAB400 para Boletos/DDA</h1>
            <p>
                O código fonte deste programa está disponivel no <a href="https://github.com/impactro/Boleto-ASP.NET/tree/master/Registro" target="_blank">GITHUB.com/IMPACTRO</a>.<br/>
                <a href="https://github.com/impactro/Boleto-Test/wiki/Exemplo-de-Remessa-CNAB">Acesse a WIKI para mais informações</a><br/>
                <small>Essa é a versão resumida onde todas váriáveis estão fixas no código, <a href="CNAB-Remessa.aspx">veja o exemplo completo aqui</a></small>
            </p>
            Chave de acesso: <asp:TextBox runat="server" ID="txtSenha" /> (senha padrão: 123)
            <asp:Button runat="server" id="btnEntrar" OnClick="btnEntrar_Click" Text="Entrar" />
            <br/><asp:Label runat="server" ID="lblInfo" EnableViewState="false" Font-Bold="true"/>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlFormulario" Visible="false">
            <h1>Geração de Arquivo de Remessa</h1>
            <br/>Período de <asp:TextBox runat="server" ID="txtInicio" /> a <asp:TextBox runat="server" ID="txtFim" />
            <asp:Button runat="server" id="btnGerar" OnClick="btnGerar_Click" Text="Filtrar" />
            <blockquote>
                <asp:CheckBox runat="server" ID="chkDados" Text="Exibir resultado do SQL" /><br/>
                <asp:CheckBox runat="server" ID="chkRemessa" Text="Exibir conteudo do arquivo de remessa" Checked="true" /><br/>
                <asp:CheckBox runat="server" ID="chkCampos" Text="Exibir dados completos da remessa" /><br/>
                <asp:CheckBox runat="server" ID="chkBoletos" Text="Exibir boletos" /><br/>
                <asp:CheckBox runat="server" ID="chkDownload" Text="Somente baixar a remessa" /><br/>
            </blockquote>
            <asp:Label runat="server" ID="lblErro" EnableViewState="false"/>
            <br/>
            <hr/>
            <!-- Cada painel abaixo só será exibdo se seu respectivo checkbox for habilitado -->
            <!-- A 'viewstate' é responsável por memorizar dados dos componetes da tela, já como tudo é gerado dinamizamente não é necessário, e essa configuração afeta todos os controles filhos -->

            <asp:Panel runat="server" ID="pnlDados" Visible="false" EnableViewState="false">
                <h3>Resultado do SQL</h3>
                <asp:GridView runat="server" ID="gvDados" EnableViewState="false"/>
                <br/>
            </asp:Panel>
            
            <asp:Panel runat="server" ID="pnlRemessa" Visible="false" EnableViewState="false">
                <h3>Visualização do arquivo de remessa gerado</h3>
                <asp:TextBox runat="server" ID="txtRemessa" TextMode="MultiLine" Width="90%" Height="150" Wrap="false" EnableViewState="false" />
                <br/>
            </asp:Panel>
            
            <asp:Panel runat="server" ID="pnlCampos" Visible="false" EnableViewState="false">
                <h3>Nome e Valores dos todos os campos internos</h3>
                <asp:GridView runat="server" ID="gvCampos" EnableViewState="false"/>
                <br/>
            </asp:Panel>
            
            <asp:Panel runat="server" ID="pnlBoletos" Visible="false" EnableViewState="false">
                <h3>Previa (visualização dos boletos)</h3>
            </asp:Panel>

        </asp:Panel>
    </div>
    </form>
</body>
</html>
