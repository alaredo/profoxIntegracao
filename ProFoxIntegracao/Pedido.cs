using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProFoxIntegracao
{
    public class Pedido
    {
        public string   Seq                   { get; set; }
        public string   Empresa               { get; set; }
        public string   Fato                  { get; set; }
        public string   Filial                { get; set; }
        public string   Codigo                { get; set; }
        public string   Tipo                  { get; set; }
        public string   Formanegociacao       { get; set; }
        public DateTime Datta               { get; set; }
        public DateTime Dattapreventrega    { get; set; }
        public DateTime Dattaultentrega     { get; set; }
        public string   Estatus               { get; set; }
        public Decimal  Percentrega          { get; set; }
        public string   Cpg                   { get; set; }
        public string   Transportadora        { get; set; }
        public string   Texto                 { get; set; }
        public Decimal  Valor                { get; set; }
        public string   Liberacao             { get; set; }
        public string   LiberacaoOperador     { get; set; }
        public DateTime Liberacaodatta      { get; set; }
        public string Cfop                  { get; set; }
        public string Cfopdescricao         { get; set; }
        public string Destinacao            { get; set; }
        public string Natureza              { get; set; }
        public Decimal DescontoValor        { get; set; }
        public Decimal DescontoPerc         { get; set; }
        public Decimal DescontoCalc         { get; set; }
        public Decimal Deducoes             { get; set; }
        public Decimal DespesaDentro        { get; set; }
        public Decimal DespesaFora          { get; set; }
        public Decimal Razao                { get; set; }
        public Decimal RazaoPositiva        { get; set; }
        public Decimal RazaoNegativa        { get; set; }
        public Decimal Ipi                  { get; set; }
        public Decimal Icms                 { get; set; }
        public Decimal Icmr                 { get; set; }
        public Decimal Iss                  { get; set; }
        public string Padrao                { get; set; }
        public string NumeroExterno         { get; set; }
        public string Tic                   { get; set; }
        public string Edicao                { get; set; }
        public string Operador              { get; set; }
        public string HorarioEntrega        { get; set; }
        public string CodigoMunicPrestServ  { get; set; }
        public string SubstitutoTributario  { get; set; }
        public string Observação            { get; set; }
        public string Posicao               { get; set; }
        public Decimal Qualidade            { get; set; }
        public string DespesaComercial      { get; set; }
        public string Ccusto                { get; set; }
        public string Origem                { get; set; }

    }
}
