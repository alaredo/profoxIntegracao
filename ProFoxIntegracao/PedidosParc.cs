using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProFoxIntegracao
{
    public class PedidosParc
    {
        public string SeqPedido         { get; set; }
        public string Parcela           { get; set; }
        public decimal Valor            { get; set; }
        public DateTime Dattavenc       { get; set; }
        public string Natureza          { get; set; }
        public string Portador          { get; set; }
        public string Especie           { get; set; }
        public decimal Descincond       { get; set; }
        public string Flagpaga          { get; set; }
        public decimal Xcomissao        { get; set; }
        public decimal TaxaPermanencia  { get; set; }
        public decimal DiasPermanencia  { get; set; }
        public decimal TaxaAdminist     { get; set; }
        public decimal VrVariacao       { get; set; }
        public string Empresa           { get; set; }
        public string Edicao            { get; set; }
        public string Operador          { get; set; }
        public string FormaReajuste     { get; set; }
        public string Indice            { get; set; }
        public decimal VrIndexado       { get; set; }
        public decimal VrDespesas       { get; set; }
        public decimal PercMulta        { get; set; }
    }
}
