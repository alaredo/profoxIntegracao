using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProFoxIntegracao
{
    public class PedidoItens
    {
        public string   Seq                   { get; set; }
        public string   SeqPedido             { get; set; }
        public decimal  Item                 { get; set; }
        public string   SeqProduto            { get; set; }
        public string   Alternativo           { get; set; }
        public string   Nome                  { get; set; }
        public string   Unidade               { get; set; }
        public decimal  Proporcao            { get; set; }
        public DateTime DattaPrevEntrega    { get; set; }
        public DateTime DattaUltEntrega     { get; set; }
        public string   Estatus               { get; set; }
        public decimal  QPedida              { get; set; }
        public decimal  QExpedida            { get; set; }
        public decimal  QFaturada            { get; set; }
        public decimal  QCancelada           { get; set; }
        public string   MotivoCancelamento    { get; set; }
        public decimal  PrecoUnit            { get; set; }
        public decimal  Precounittab         { get; set; }
        public decimal  Precounittabmin      { get; set; }
        public decimal  Precounittabmax      { get; set; }
        public string   Cst                   { get; set; }
        public string   Bonificacao           { get; set; }
        public string   Texto                 { get; set; }
        public decimal  Valor                { get; set; }
        public decimal  Peso                 { get; set; }
        public string   Liberacao             { get; set; }
        public string   LiberacaoOperador     { get; set; }
        public DateTime   LiberacaoDatta        { get; set; }
        public string   Cfop                  { get; set; }
        public string   Destinacao            { get; set; }
        public string   Propriedade           { get; set; }
        public decimal  Descontovalor        { get; set; }
        public decimal  Descontoperc         { get; set; }
        public decimal  Descontocalc         { get; set; }
        public decimal  Descontocred         { get; set; }
        public decimal  Ipi                  { get; set; }
        public decimal  Icms                 { get; set; }
        public decimal  Icmr                 { get; set; }
        public decimal  Iss                  { get; set; }
        public decimal  Ordem                { get; set; }
        public string   Vendedor              { get; set; }
        public string   Empresa               { get; set; }
        public string   Edicao                { get; set; }
        public string   Operador              { get; set; }
        public string   NomeExtra             { get; set; }
        public string   Rrimposto             { get; set; }
        public decimal   Custo                 { get; set; }
        public decimal   CustoVenda            { get; set; }
        public decimal   CustoMedio            { get; set; }
        public decimal   Qualidade             { get; set; }
        public string   Tipo                  { get; set; }
        public string   CodigoExternoProduto  { get; set; }
        public string   Cest                  { get; set; }

    }
}
