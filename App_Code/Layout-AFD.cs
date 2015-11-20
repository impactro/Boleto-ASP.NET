using Impactro.Layout;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

// Exemplo de implementação de tratamento de layout de AFD (Arquivo Fonte de Dados) originario de qualquer relogio de ponto portaria 1510
///namespace Impactro.Layout
//{
// http://tools.lymas.com.br/regexp_br.php
// http://pt.wikipedia.org/wiki/Expressão_regularhp

/// <summary>
/// Cabeçalho do Arquivo
/// </summary>
    [RegLayout(@"^000000000[1]", DateFormat8="ddMMyyyy")]
    [ComVisible(false)]
    public enum AFD1
    {

        /// <summary>
        /// 000000000 Zeros Fixo
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        Zeros,

        /// <summary>
        /// Tipo de registro "1" (Cabeçalho)
        /// </summary>
        [RegFormat(RegType.P9, 1, Default = "1")]
        Tipo,

        /// <summary>
        /// Tipo de identificador do empregador "1" para CNPJ ou "2" para CPF
        /// </summary>
        [RegFormat(RegType.P9, 1)]
        EmpregadorTipo,

        /// <summary>
        /// CNPJ ou CPF do empregador
        /// </summary>
        [RegFormat(RegType.P9, 14)]
        CNPJ_CPF,

        /// <summary>
        /// CEI (Cadastro Específico do INSS) do empregador
        /// </summary>
        [RegFormat(RegType.P9, 12)]
        CEI,

        /// <summary>
        /// Razão Social do Empregador
        /// </summary>
        [RegFormat(RegType.PX, 150)]
        RazaoSocial,

        /// <summary>
        /// Número de fabricação do REP
        /// </summary>
        [RegFormat(RegType.P9, 17)]
        REP,

        /// <summary>
        /// Data de inicial dos registros no arquivo
        /// </summary>
        [RegFormat(RegType.PD, 8)]
        DataInicio,

        /// <summary>
        /// Data final dos registros no arquivo
        /// </summary>
        [RegFormat(RegType.PD, 8)]
        DataFinal,

        /// <summary>
        /// Data e Hora de Geração do arquivo
        /// </summary>
        [RegFormat(RegType.PD, 12)]
        DataHora,

    }
    
    /// <summary>
    /// Alteração do empregador
    /// </summary>
    [RegLayout(@"^\d{9}[2]")]
    [ComVisible(false)]
    public enum AFD2
    {
        /// <summary>
        /// NÚmero Sequencial de Registro
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        NSR,

        /// <summary>
        /// Tipo de registro "2" (Alteração do Empregador)
        /// </summary>
        [RegFormat(RegType.P9, 1, Default = "2")]
        Tipo,

        /// <summary>
        /// Data/Hora da gravação
        /// </summary>
        [RegFormat(RegType.PD, 12)]
        DataHora,

        /// <summary>
        /// Tipo de identificador do empregador "1" para CNPJ ou "2" para CPF
        /// </summary>
        [RegFormat(RegType.P9, 1)]
        EmpregadorTipo,

        /// <summary>
        /// CNPJ ou CPF do empregador
        /// </summary>
        [RegFormat(RegType.P9, 14)]
        CNPJ_CPF,

        /// <summary>
        /// CEI (Cadastro Específico do INSS) do empregador
        /// </summary>
        [RegFormat(RegType.P9, 12)]
        CEI,

        /// <summary>
        /// Razão Social do Empregador
        /// </summary>
        [RegFormat(RegType.PX, 150)]
        RazaoSocial,

        /// <summary>
        /// Local de prestação dos serviços
        /// </summary>
        [RegFormat(RegType.P9, 100)]
        Local
    }

    /// <summary>
    /// Marcação de ponto
    /// </summary>
    [RegLayout(@"^\d{9}[3]")]
    [ComVisible(false)]
    public enum AFD3
    {
        /// <summary>
        /// Número Sequencial de Registro
        /// </summary>
        [RegFormat(RegType.P9,9)]
        NSR,

        /// <summary>
        /// Tipo de Registro "3" (Marcação do ponto)
        /// </summary>
        [RegFormat(RegType.P9, 1, Default = "3")]
        Tipo,

        /// <summary>
        /// Data/Hora de geração
        /// </summary>
        [RegFormat(RegType.PD, 12)]
        DataHora,

        /// <summary>
        /// Numero do PIS do empregado
        /// </summary>
        [RegFormat(RegType.P9, 12)]
        PIS
    }

    /// <summary>
    /// Registro de ajuste do relogio
    /// </summary>
    [RegLayout(@"^\d{9}[4]")]
    [ComVisible(false)]
    public enum AFD4
    {
        /// <summary>
        /// Número Sequencial de Registro
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        NSR,

        /// <summary>
        /// Tipo de registro "4" (alteração de horario)
        /// </summary>
        [RegFormat(RegType.P9, 1, Default = "4")]
        Tipo,

        /// <summary>
        /// Data/Hora anterior
        /// </summary>
        [RegFormat(RegType.PD, 12)]
        DataHoraAntes,

        /// <summary>
        /// Data/Hora posteior
        /// </summary>
        [RegFormat(RegType.PD, 12)]
        DataHoraAjustado
    }

    /// <summary>
    /// Inclusão ou alteração do empregado
    /// </summary>
    [RegLayout(@"^\d{9}[5]")]
    [ComVisible(false)]
    public enum AFD5
    {
        /// <summary>
        /// Número Sequencial de Registro
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        NSR,

        /// <summary>
        /// Tipo de registro "4" (Inclusão/Alteração/Exclusão de empregado)
        /// </summary>
        [RegFormat(RegType.P9, 1, Default = "4")]
        Tipo,

        /// <summary>
        /// Data/Hora do registro
        /// </summary>
        [RegFormat(RegType.PD, 12)]
        DataHora,

        /// <summary>
        /// Tipo de operação do registro: I=> inclusão, A=> Alteração, E=>Exclusão
        /// </summary>
        [RegFormat(RegType.PX, 1)]
        Operacao,
        
        /// <summary>
        /// PIS do empregado
        /// </summary>
        [RegFormat(RegType.P9, 12)]
        PIS,

        /// <summary>
        /// Nome do empregado
        /// </summary>
        [RegFormat(RegType.PX, 52)]
        Nome
    }

    /// <summary>
    /// Eventos de abertura do REP
    /// </summary>
    [RegLayout(@"^\d{9}[6]")]
    [ComVisible(false)]
    public enum AFD6
    {
        /// <summary>
        /// Número Sequencial de Registro
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        NSR,

        /// <summary>
        /// Tipo de registro "6" (Inclusão/Alteração/Exclusão de empregado)
        /// </summary>
        [RegFormat(RegType.P9, 1, Default = "6")]
        Tipo,

        /// <summary>
        /// Data/Hora do registro
        /// </summary>
        [RegFormat(RegType.PD, 12)]
        DataHora,
      
    }

    /// <summary>
    /// Trailer do arquivo
    /// </summary>
    [RegLayout(@"^999999999")]
    [ComVisible(false)]
    public enum AFD9
    {
        /// <summary>
        /// 999999999 Trailer
        /// </summary>
        [RegFormat(RegType.P9, 9, Default="999999999")]
        Noves,

        /// <summary>
        /// Quantidade de registros do tipo 2
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        QtdTipo2,

        /// <summary>
        /// Quantidade de registros do tipo 3
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        QtdTipo3,

        /// <summary>
        /// Quantidade de registros do tipo 4
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        QtdTipo4,

        /// <summary>
        /// Quantidade de registros do tipo 5
        /// </summary>
        [RegFormat(RegType.P9, 9)]
        QtdTipo5,
        
        /// <summary>
        /// Tipo de registro "9"
        /// </summary>
        [RegFormat(RegType.P9, 1, Default = "9")]
        Tipo,
    }

    /// <summary>
    /// Validação do arquivo AFD
    /// </summary>
    [ComVisible(false)]
    public class LayoutAFD : Layout
    {
        // contador de registros
        private int n;
        // Ultimo NSR lido, para garantir a sequencia
        private int nsr;
        // Ultima data lida
        private DateTime dt;
        // contadores de registros
        private int qtd2, qtd3, qtd4, qtd5;

        /// <summary>
        /// Cria uma construtora lá configurando a classe base de layout
        /// </summary>
        public LayoutAFD()
            : base(typeof(AFD1), typeof(AFD2), typeof(AFD3), typeof(AFD4), typeof(AFD5), typeof(AFD6), typeof(AFD9))
        {
            this.onAfterReadLine += this.Validate;
        }

        /// <summary>
        /// Evento de validação do Layout "onAfterReadLine"
        /// </summary>
        private void Validate(Layout layout, object reg)
        {
            n++; // incrementa o contador de registro
            Type tp = reg.GetType();
            if (tp == typeof(Reg<AFD1>))
            {
                // header
                nsr = qtd2 = qtd3 = qtd4 = qtd5 = 0;
                n = 1;
                Reg<AFD1> r = (Reg<AFD1>)reg;
                dt = (DateTime)r[AFD1.DataInicio];

            }
            else if (tp == typeof(Reg<AFD2>))
            {
                // alteração de data/hora
                Reg<AFD2> r = (Reg<AFD2>)reg;
                qtd2++;

                if ((int)r[AFD2.NSR] <= nsr)
                    throw new Exception("NSR Invalido na linha " + n);
                nsr = (int)r[AFD2.NSR];

            }
            else if (tp == typeof(Reg<AFD3>))
            {
                // registro de ponto
                Reg<AFD3> r = (Reg<AFD3>)reg;
                qtd3++;

                if ((int)r[AFD3.NSR] <= nsr)
                    throw new Exception("NSR Invalido na linha " + n);
                nsr = (int)r[AFD3.NSR];

                if (dt > (DateTime)r[AFD3.DataHora]) // TODO: cuidado com alteração de data
                    throw new Exception("Data do ponto menor que a ultima data lida, ou especificada no arquivo");

            }
            else if (tp == typeof(Reg<AFD4>))
            {
                // Alteração de data/hora
                Reg<AFD4> r = (Reg<AFD4>)reg;
                qtd4++;
                if ((int)r[AFD4.NSR] <= nsr)
                    throw new Exception("NSR Invalido na linha " + n);
                nsr = (int)r[AFD4.NSR];

            }
            else if (tp == typeof(Reg<AFD5>))
            {
                // Alteração do empregado
                Reg<AFD5> r = (Reg<AFD5>)reg;
                qtd5++;
                if ((int)r[AFD5.NSR] <= nsr)
                    throw new Exception("NSR Invalido na linha " + n);
                nsr = (int)r[AFD5.NSR];

            }
            else if (tp == typeof(Reg<AFD9>))
            {
                // Trailer
                Reg<AFD9> r = (Reg<AFD9>)reg;
                int q;
                if ((q = (int)r[AFD9.QtdTipo2]) != qtd2)
                    throw new Exception("Número de registros tipo 2 contados: " + qtd2 + " lido: " + q);
                else if ((q = (int)r[AFD9.QtdTipo3]) != qtd3)
                    throw new Exception("Número de registros tipo 3 contados: " + qtd3 + " lido: " + q);
                else if ((q = (int)r[AFD9.QtdTipo4]) != qtd4)
                    throw new Exception("Número de registros tipo 4 contados: " + qtd4 + " lido: " + q);
                else if ((q = (int)r[AFD9.QtdTipo5]) != qtd5)
                    throw new Exception("Número de registros tipo 5 contados: " + qtd5 + " lido: " + q);
            }
            else
                throw new Exception("Tipo de registro invalido: " + tp.FullName);
        }

    }

//}
