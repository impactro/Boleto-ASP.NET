using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class Registro_Exemplo1_Reflection : System.Web.UI.Page
{
    // apenas para haver alguma variável disponivel na listagem de campos
    public string VariavelExemplo;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Usar reflexão (Reflection) não é muito comum, mas de grande poder, pois torna o código mais inteligente, e bem menor
        // Um exemplo pratico do uso de reflexão, é o proprio intelicense do Visual Studio.
        // que dado um certo objeto, variável, é possivel identificar todos os metodos, e campos desta instancia
        // veja mais estas referencias:
        //  http://msdn.microsoft.com/pt-br/library/system.reflection.aspx
        //  http://www.microsoft.com/brasil/msdn/tecnologias/vbnet/visualbasic_reflection.mspx
        //  http://www.codeguru.com/csharp/csharp/cs_misc/reflection/article.php/c4257

        // Se o conhecimento basico de reflection minhas classes de geração re registros se tornam magicas e ilegiveis
        // por isso aqui quero abordar 3 exemplos basicos a fim de deixar tudo mais legivel para usar minhas classes

        // Exemplo 1 )
        // Aqui estou apenas listando todos os metodos e campos da propria instancia desta página
        // algo parecido com o que alguem escrevesse `this.` e o intelicense abriria uma lista semelhante


        Type tp;                // a classe Type, contem todas as informações das definições de qualquer classe
        tp = this.GetType();    // para obter as definições de qualquer objeto basta usar a propria instancia e o metodo 'GetType()'

        // apenas zera a string de saida, e já defini o titulo
        lblOut.Text = "<h1>METODOS</h1>";      
        // aqui a ideia é listar todos os metodos (rotinas/funções)
        // informações do metodo são representadas pela classe 'MethodInfo'
        // o metodo 'GetMethods()' obtem um array com todas as definições de todos os metodos
        foreach (MethodInfo mi in tp.GetMethods())
        {
            lblOut.Text += mi.Name + "<br/>\r\n";   // Obtem o nome do metodo. e faz uma quebra de linha
        }

        // mesmo conceito mas agora com as propriedades (valoes encapsulados ou rotinas de obtemção e definiçào de valores via GET/SET)
        lblOut.Text += "<h1>PROPRIEDADES</h1>";
        foreach (PropertyInfo pi in tp.GetProperties())
        {
            lblOut.Text += pi.Name + "<br/>\r\n";   // Obtem o nome da propriedade. e faz uma quebra de linha
        }

        // mesmo conceito mas agora com os campos
        lblOut.Text += "<h1>VARIÁVEIS</h1>";
        foreach (FieldInfo fi in tp.GetFields())
        {
            lblOut.Text += fi.Name + "<br/>\r\n";   // Obtem o nome do campo. e faz uma quebra de linha
        }

        // Assim obtemdo o Type de uma instncia, é possivel obter um campo ou metodo
        // e invoca-lo, ou aribuir o valor de um campo, ler, etc...
 
        // o sistema de trabamento de layout baseados em 'Reg<T>' usa reflection em enumeradores
        // para montar as estruturas de arquivos internos, por meio de GET/SET (propriedades) dinamicas
        // baseado no template do tipo especificado em '<T>'
    }
}