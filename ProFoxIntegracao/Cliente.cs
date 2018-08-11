using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProFoxIntegracao
{
    public class Cliente
    {
        public string   Empresa             { get; set; }
        public string   Codigo              { get; set; }
        public string   Nome                { get; set; }
        public string   NomeReduzido        { get; set; }
        public string   Cnpj                { get; set; }
        public string   Endereco            { get; set; }
        public string   Bairro              { get; set; }
        public string   Cidade              { get; set; }
        public string   Cep                 { get; set; }
        public string   Estado              { get; set; }
        public string   Pais                { get; set; }
        public string   Pessoa              { get; set; }
        public string   Estatus             { get; set; }
        public string   Tipo                { get; set; }
        public string   Telefone            { get; set; }
        public string   Celular             { get; set; }
        public string   Email               { get; set; }
        public string   Datta               { get; set; }
        public string   DattaInclusao       { get; set; }

        public string   InscEst             { get; set; }
        public string   Doc                 { get; set; }
        public string   RegiaoVendas        { get; set; }
        public string   RegiaoFiscal        { get; set; }
        public string   GrupoImpostos       { get; set; }
        public string   GrupoPrecos         { get; set; }
        public string   CategCredito        { get; set; }
        public string   Vendedor            { get; set; }
        public string   Portador            { get; set; }
        public string   Holding             { get; set; }
        public int      Renda               { get; set; }
        public int      Compras             { get; set; }
        public int      DiasAtraso          { get; set; }
        public int      MoraAtraso          { get; set; }
        public string   Contato             { get; set; }     
        public string   DattaNascimento     { get; set; }
        public int      Xcomissao           { get; set; }
        public string   Texto               { get; set; }
        public string   Arquivo             { get; set; }
        public string   Filiacao            { get; set; }
        public string   Edicao              { get; set; }
        public string   Operador            { get; set; }
        public string   Ficha               { get; set; }
        public int      Xcusto              { get; set; }
        public string   Bairro35            { get; set; }
        public string   Cidade35            { get; set; }
        public string   PontoReferencia     { get; set; }
        public string   Distrito            { get; set; }
        public string   HorarioEntrega      { get; set; }
        public string   EnderecoComplemento { get; set; } 
        public string   InscMunicipal       { get; set; }
        public string   HabilitacaoSituacao { get; set; }
        public DateTime HabilitacaoDatta    { get; set; }
        public string   HabilitacaoCodigo   { get; set; }
        public string   EmailNFE            { get; set; }
        public string   EmailFinanceiro     { get; set; }
        public string   CContabil           { get; set; }
    }
}
