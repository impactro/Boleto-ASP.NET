<%@ Control Language="VB" ClassName="Rodape" %>
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
<div style="background-color:lightblue; border: solid 1px red; padding: 10px; margin: 10px auto 5px auto; width:90%; overflow: auto;">
    <div style="float:left;">
    Conheça outros exemplos:<br/>
    <ul>
        <li><a href="BoletoCustomizado.aspx" title="É possivel sair completamente do padrão visual e criar seu proprio layout de boleto, sem ter que se preoculpar com as rotinas de calculo, veja esse exemplo">Boleto com Layout Customizado</a></li>
        <li><a href="BoletoImagem.aspx" title="É possivel criar uma imagem do boleto para enviar por e-mail, ou exibi-lá como imagem usando arquivos .ASHX">Boleto em Imagem GIF e ASHX</a></li>
        <li><a href="Consessionaria.aspx" title="Boleto de arrecadação de consessionária (água, luz, telefone)">Arrecadação de Consessionária</a></li>
        <li><a href="eCommerce1.aspx" title="Exemplo basico de uma simples implementação de e-commerce">Exemplo de e-commerce</a></li>
        <li><a href="FuncTeste_CodigoBarras.aspx" title="Geração do código de barras dentro da imagem">Geração de Código de Barras</a></li>
        <li><a href="FuncTeste_FatVenc.aspx" title="O Calculo do Modulo 10 é usado para calcular os dígitos verificadores dos Campos 1,2,3 da linha digitável (IPTE), e por alguns bancos na montagem da linha digitável">Calculo do Fator de Vencimento</a></li>
        <li><a href="FuncTeste_Modulo10.aspx" title="O Calculo do Modulo 10 é usado para calcular os dígitos verificadores dos Campos 1,2,3 da linha digitável (IPTE), e por alguns bancos na montagem da linha digitável">Calculo Modulo 10</a></li>
        <li><a href="FuncTeste_Modulo11.aspx" title="O Calculo Modulo 11 para emissão de boletos é relativamente complexo, pois cada banco usa de forma diferente, mas basicamente nossa rotina em C# suporta as 4 formas">Calculo Modulo 11</a></li>
        <li><a href="FuncTeste_IPTE.aspx" title="Veja como converter o código de barras de um boleto em linha digitável (IPTE), este exemplo acompanha os fontes em C#">Gerando o IPTE</a></li>
        <li><a href="FuncTeste_DecodIPTE.aspx" title="Veja como converter o IPTE (linh adigitável) em código de barras, este exemplo acompanha os fontes em C#">Decodificando o IPTE</a></li>
        <li><a href="BaixaAutomaticaER.aspx" title="Este exemplo demostra como executar uma baixa automática por meio de reconhecimento com expressão regulares (Regex.Match) de titulos de cobrança (boletos) inseridas por colagem (ctrl+c/ctrl+v) de um extrato de movimentação bancário">Baixa Automatica por Expressão Regular</a></li>
        <li><a href="ImagesResources.aspx" title="É possivel inserir imagens dentro de DLL, e usalas em HTML atraz do 'GetWebResourceUrl' e usando 'GetManifestResourceNames' obter uma lista completa do que está embutido em uma DLL">Imagens embutidas em Resources</a></li>
        <li><a href="VB6.htm" title="Integração com ActiveX para compatibilidade com Vb6, Delphi, C++, e outras linguagens que aceitem 'Type Library'">TLB/ActiveX VB6, Delphi, C++</a></li>
        <li>Exemplo de Implementações em Bancos
        <ul>
            <li><a href="ExemploBanespa.aspx">Banespa</a></li>
            <li><a href="ExemploUnibanco.aspx">Unibanco</a></li>
            <li><a href="ExemploBanestes.aspx">Banestes</a></li>
            <li><a href="ExemploBradesco.aspx">Bradesco</a></li>
            <li><a href="ExemploCaixa.aspx">Caixa</a></li>
            <li><a href="ExemploCitiBank.aspx">CitiBank</a></li>
            <li><a href="ExemploHSBC.aspx">HSBC</a></li>
            <li><a href="ExemploItau.aspx">Itau</a></li>
            <li><a href="ExemploNossaCaixa.aspx">Nossa Caixa</a></li>
            <li><a href="ExemploReal.aspx">Real</a></li>
            <li><a href="ExemploSantander.aspx">Santander</a></li>
        </ul>
        </li>        
    </ul>
    </div>
    <div style="text-align: right;">
        Este exemplo vem com o código fonte ao adquirir o componente de geração de 
        boletos.<br/><br/>
        <b>Donwload: <br/>
        <a href="http://www.boletoasp.com.br/BoletoNet2-Demo.zip" title="VERSÃO DEMOSTRAÇÃO">Versão de Demostração</a><br/>
        <a href="http://www.boletoasp.com.br/Impactro.Cobranca.xml" title="DOCUMENTAÇÃO XML - Impactro.Cobranca.DLL"><i>XML de Documentação</i></a><br/>
        <a href="http://www.boletoasp.com.br/Impactro.Cobranca-HELP.zip" title="DOCUMENTAÇÃO HELP (.chm)"><i>Documentação em formato HELP do Windows</i></a><br/>
        </b>
        <br/>
        Veja também: <br/>
        <a href="../Cielo/Cielo-Direto.aspx" title="Exemplo de Gateway em ASP.NET / C# de request para pagamentos usando os componentes do Cielo/VisaNet">Gateway Cielo</a> para pagamento com cartões de credito, via WebServices da Cielo<br/>
        <a href="../Registro/CNAB-Form.aspx">Arquivo de Remessa, e Tratamento de Retorno</a> - Layout para ITAU-CNB400 e BRADESCO-CNAB240<br/>
        <a href="../NFe/RPS-NFe1.aspx">Geração de Lote de RPS para NF-e SP</a> - Versão 1.0 da nota Fiscal de Serviços para São Paulo/SP<br/>
        <a href="../BoletoASP">ASP Classico</a> Versão antiga em ASP3<br/>
        <br/>
        <b>Conheça nosso site <a href="http://www.boletoasp.com.br">http://www.boletoasp.com.br</a></b>
        <br/>
        <br/>
        <p style="text-align: center; color: Red; font-weight: bold;">Adquira todos esses exemplos<br/>
        <br/><br/>Clique aqui para <a href="http://www.boletoasp.com.br/FAQ.aspx">comprar ou tirar suas dúvidas</a>
        </p>
    </div>
<script type="text/javascript">
try{
    var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
    document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
}
catch(x)
{
}
</script>
<script type="text/javascript">
try{
    var pageTracker = _gat._getTracker("UA-2170502-3");
    pageTracker._initData();
    pageTracker._trackPageview();
}
catch(x)
{
}
</script>
</div>