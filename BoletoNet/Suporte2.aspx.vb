Imports Impactro.Cobranca
Imports System.Data.SqlClient
Imports System.Data

Partial Class BoletoNet_Suporte2
    Inherits System.Web.UI.Page

    'Dados de acordo com exemplo: http://rhs.bitcaseiro.com.br/Boleto/Boleto3.aspx?numeroInscricao=000018

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Request.QueryString.AllKeys.Contains("numeroInscricao") Then
        Dim numeroInscricao As String = "000018" ' Request.QueryString("numeroInscricao")

        'Definição dos dados do cedente
        Dim Cedente As New CedenteInfo
        Cedente.Cedente = "RHS CONSULT LTDA - EPP"
        Cedente.Endereco = "<br/> &nbsp;Estrada Elias Alves da Costa, nº 957 - Itapevi - SP"
        Cedente.CNPJ = "23047156000123"
        Cedente.Banco = "104-0"
        Cedente.Agencia = "4353"
        Cedente.Conta = "9399"
        Cedente.Carteira = "1"              ' 1-RG, 2-SR (Registrada ou Sem Registro)
        Cedente.Convenio = "00360305435348" ' CNPJ do PV da conta do cliente
        Cedente.CodCedente = "658857"       ' Código do Cliente(cedente)
        Cedente.Informacoes = _
            "SAC CAIXA: 0800 726 0101 (informações, reclamações, sugestões e elogios)<br/>" & _
            "Para pessoas com deficiência auditiva ou de fala: 0800 726 2492<br/>" & _
            "Ouvidoria: 0800 725 7474 (reclamações não solucionadas e denúncias)<br/>" & _
            "<a href='http://caixa.gov.br' target='_blank'>caixa.gov.br</a>"

        BoletoTextos.LocalPagamento = "PREFERENCIALMENTE NAS CASAS LOTÉRICAS ATÉ O VALOR LIMITE"


        'Codigo de cedente especial, com 15 Digitos (obrigatório Zeros a Frente)
        'Cedente.CodCedente = "059100300000608"


        'Definição dos dados do sacado
        Dim Sacado As New SacadoInfo
        Dim rg As String = ""
        Dim curso1 As String = ""
        Dim curso2 As String = ""
        'Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("RHSConsultConnection").ConnectionString)
        '    Try
        '        Using cmd As New SqlCommand
        '            With cmd
        '                .Connection = cn
        '                .Connection.Open()
        '                .CommandText = "SELECT Nome,CPF,Logradouro,Numero,Cidade,Bairro,CEP,Estado,RG,CursoOpcao1,CursoOpcao2 FROM FormularioPadrao WHERE NumeroInscricao=@NumeroInscricao AND ProjetoId=3"
        '                .CommandType = CommandType.Text
        '                .Parameters.Add("@NumeroInscricao", SqlDbType.NVarChar, 4000)
        '                .Parameters("@NumeroInscricao").Value = numeroInscricao
        '            End With

        '            Dim data As SqlDataReader = cmd.ExecuteReader()

        '            While data.Read
        '                If data.HasRows = True Then
        Sacado.Sacado = "Teste para homologação" 'data.GetString(0)
        Sacado.Documento = "123" ' data.GetString(1)
        Sacado.Endereco = "Endreço completo" ' data.GetString(2) & ", " & data.GetString(3)
        Sacado.Cidade = "Cidade" ' data.GetString(4)
        Sacado.Bairro = "Bairro" ' data.GetString(5)
        Sacado.Cep = "12345-678" ' data.GetString(6)
        Sacado.UF = "SP" ' data.GetString(7)
        rg = "123.456.678-9" ' data.GetString(8)
        curso1 = "Motorista de Ambulância" 'data.GetString(9)
        curso2 = "Curso 2" ' data.GetString(10)
        '                End If
        '            End While
        '            data.Close()
        '            data.Dispose()
        '            cn.Close()
        '        End Using
        '    Catch ex As Exception

        '    End Try
        'End Using


        'Definição das Variáveis do boleto
        Dim Boleto As New BoletoInfo
        Boleto.NossoNumero = "8210" & numeroInscricao
        Boleto.NumeroDocumento = Boleto.NossoNumero


        If curso1 = "Motorista de Ambulância" Then
            Boleto.ValorDocumento = 12
        End If

        If curso1 = "Auxiliar de Enfermagem" Then
            Boleto.ValorDocumento = 16.1
        End If

        If curso1 = "Enfermeiro" Then
            Boleto.ValorDocumento = 18.5
        End If

        If curso1 = "Professor de Educação Básica I (PEB I)" Then
            Boleto.ValorDocumento = 18.5
        End If


        Boleto.DataDocumento = Now
        Boleto.DataVencimento = CDate("30/03/2016")
        Boleto.Instrucoes = "<br />Nº Inscrição: " & numeroInscricao & "<br />Nome: " & Sacado.Sacado & "<br />Documento : " & rg & "<br />Cargo : " & curso1 & "<br /><br />***SR. CAIXA FAVOR NÃO RECEBER APÓS O VENCIMENTO***"""


        'Using cn As New SqlConnection(ConfigurationManager.ConnectionStrings("RHSConsultConnection").ConnectionString)
        '    Try
        '        Using cmd As New SqlCommand
        '            With cmd
        '                .Connection = cn
        '                .Connection.Open()
        '                .CommandText = "INSERT INTO Boleto(NossoNumero, NumeroDocumento, ValorDocumento, DataDocumento, DataVencimento, Instrucoes, ProjetoId,Chave)VALUES(@NossoNumero, @NumeroDocumento, @ValorDocumento, @DataDocumento, @DataVencimento, @Instrucoes, 3,@Chave)"
        '                .CommandType = CommandType.Text
        '                .Parameters.Add("@NossoNumero", SqlDbType.NVarChar, 4000)
        '                .Parameters("@NossoNumero").Value = Boleto.NossoNumero

        '                .Parameters.Add("@NumeroDocumento", SqlDbType.NVarChar, 4000)
        '                .Parameters("@NumeroDocumento").Value = Boleto.NumeroDocumento

        '                .Parameters.Add("@ValorDocumento", SqlDbType.Decimal)
        '                .Parameters("@ValorDocumento").Value = Boleto.ValorDocumento

        '                .Parameters.Add("@DataDocumento", SqlDbType.DateTime)
        '                .Parameters("@DataDocumento").Value = Boleto.DataDocumento

        '                .Parameters.Add("@DataVencimento", SqlDbType.DateTime)
        '                .Parameters("@DataVencimento").Value = Boleto.DataVencimento

        '                .Parameters.Add("@Instrucoes", SqlDbType.NVarChar, 4000)
        '                .Parameters("@Instrucoes").Value = Boleto.Instrucoes

        '                .Parameters.Add("@Chave", SqlDbType.NVarChar, 4000)
        '                .Parameters("@Chave").Value = numeroInscricao
        '            End With

        '            cmd.ExecuteNonQuery()
        '            cn.Close()
        '        End Using
        '    Catch ex As Exception

        '    End Try
        'End Using

        'Usando a rotina padrão interna! XYNNNNNNNNNNNNNNN-D para codigo de cedente com 6 posições
        'AddHandler bltPag.MontaCampoLivre, AddressOf MontaCampoLiveCaixa

        'monta o boleto com os dados específicos nas classes
        bltPag.MakeBoleto(Cedente, Sacado, Boleto)


        'End If

    End Sub

    Public Function MontaCampoLiveCaixa(ByVal blt As Impactro.Cobranca.Boleto) As String
        'Restaura as variáveis informados nas estruturas do boleto
        Dim cNossoNumero As String = blt.NossoNumero ' 10 digitos
        Dim cCodCedente As String = blt.CodCedente '15 digitos
        'Monta o campo livre
        Dim cLivre As String = cNossoNumero + cCodCedente ' totaliza 25 digitos

        'Valor a ser exibido no campo nosso numero
        blt.NossoNumeroExibicao = cNossoNumero & "-" & CobUtil.Modulo11Especial(cNossoNumero, 9)

        'Adiciona o digito final no código do cedente
        cCodCedente &= CobUtil.Modulo11Padrao(cCodCedente, 9)
        'Valor a ser exibido no campo Agencia/Conta
        'blt.AgenciaConta = _
        '    cCodCedente.Substring(0, 4) & "." & _
        '    cCodCedente.Substring(4, 3) & "-" & _
        '    cCodCedente.Substring(7, 8) & "." & _
        '    cCodCedente.Substring(15, 1)

        blt.AgenciaConta = _
            cCodCedente.Substring(0, 4) & "/658857-" & CobUtil.Modulo11Especial(cNossoNumero, 9)

        'Retorna o Camo Livre
        Return cLivre

    End Function


End Class
