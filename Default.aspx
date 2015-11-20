<%@ Page Language="VB" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Exemplos BoletoASP (Código fonte dos exemplos incluso) </title>
    <meta name="description" content="Este sub-dominio (site), será fornecido na integra, com os fontes, como exemplo de como você pode implementar suas soluções de cobrança seja por boleto Bancário com ou sem registro, Cartão de Credito Cielo, e ainda gerar Nota fiscais de serviço por meio de RPS" />
</head>
<body>
<script type="text/javascript">

    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-2170502-3']);
    _gaq.push(['_setDomainName', 'boletoasp.com.br']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

</script>
<form id="form1" runat="server">
    <div>
        <h1>Demostração BoletoASP.Net (Impactro.Cobranca.DLL)</h1>
        <p>Esta única DLL é bem completa, com diversas rotinas e utlitários inclusos, e está sendo consantemente melhorada.</p>
        <ul>
            <li><a href="BoletoNet">Boleto ASP.Net</a> - Conheça os diversos exemplos inclusos no produto</li>
            <li><a href="Registro/CNAB-Form.aspx">Arquivo de Remessa, e Tratamento de Retorno</a> - Layout para ITAU-CNB400 e BRADESCO-CNAB240</li>
            <li><a href="NFe/RPS-NFe1.aspx">Geração de Lote de RPS para NF-e SP</a> - Versão 1.0 da nota Fiscal de Serviços para São Paulo/SP</li>
            <li><a href="Cielo/Cielo-Direto.aspx">Gateway Cielo WebServices</a> - Encapsulamento XML das chamadas</li>
            <li><a href="BoletoASP">Boleto ASP3 Clássico</a> - Antigo, mas muito poderoso gerador de boleto em ASP</li>
        </ul>
        [ <a href="http://www.boletoasp.com.br">clique aqui para comprar a 'Impactro.Cobranca.DLL' que contem o componente <b>BoletoASP</b></a> ]
        <p>Infelizmente meu tempo é bem limitado, então problemas mais complexos só posso resolver de final de semana, o restante que já está ponto é só usar, e tirar duvidas comigo por e-mail que geralmente eu respondo bem rápido.</p>
    </div>
</form>
</body>
</html>
