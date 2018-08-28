using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ProFoxIntegracao
{
    [Serializable]
    public class IntegracaoConfig
    {
        [DescriptionAttribute("Código da Tabela de Preços ProFox."),
        CategoryAttribute("Produtos")]
        public string TabelaPrecos { get; set; }

        [DescriptionAttribute("Código da Filial ProFox."),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute("01")]
        public string Filial { get; set; }

        [DescriptionAttribute("Chave da Forma de Negociacao ProFox."),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute("Transacao")]
        public string FormaNegociacao { get; set; }

        [DescriptionAttribute("Chave da Condição de Pagamento ProFox."),
        CategoryAttribute("Pedidos")]
        public string CPG { get; set; }

        [DescriptionAttribute("Chave da Transportadora ProFox."),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Transportadora { get; set; }

        [DescriptionAttribute("Valor do campo Liberação ( S/N )"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Liberacao { get; set; }

        [DescriptionAttribute("Operador da Liberação"),
        CategoryAttribute("Pedidos")]
        public string LiberacaoOperador { get; set; }

        [DescriptionAttribute("CFOP dentro do estado"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string CfopDentroEstado { get; set; }

        [DescriptionAttribute("CFOP fora do estado"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string CfopForaEstado { get; set; }

        [DescriptionAttribute("Código de destino C-Consumo R-Revenda I-Industrializacao"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Destinação { get; set; }

        [DescriptionAttribute("Código da Natureza Operação"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Natureza { get; set; }

        [DescriptionAttribute("Tic do Pedido"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Tic { get; set; }

        [DescriptionAttribute("Código do Municipio Prestador de Serviço"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string CodigoMunicPrestServ { get; set; }

        [DescriptionAttribute("Código do Centro de Custo"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Ccusto { get; set; }

        [DescriptionAttribute("Código Cifop: 0 ou 1"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(0)]
        public int Cifop { get; set; }

        [DescriptionAttribute("Código Cfop: 0 ou 1"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Cfop { get; set; }

        [DescriptionAttribute("Chave da Origem ProFox."),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Origem { get; set; }

        [DescriptionAttribute("Chave do Vendedor ProFox."),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Vendedor { get; set; }

        [DescriptionAttribute("Chave da Equipe ProFox."),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Equipe { get; set; }

        [DescriptionAttribute("Codigo Cest"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Cest { get; set; }

        [DescriptionAttribute("Codigo Portador Parcelas do Pedido"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Portador { get; set; }

        [DescriptionAttribute("Codigo Especie Parcelas do Pedido"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Especie { get; set; }

        [DescriptionAttribute("Codigo Operador"),
        CategoryAttribute("Pedidos"),
        DefaultValueAttribute(" ")]
        public string Operador { get; set; }

        //Cliente
        [DescriptionAttribute("Regiao de Vendas"),
        CategoryAttribute("Cliente"),
        DefaultValueAttribute(" ")]
        public string RegiaoVendas { get; set; }

        [DescriptionAttribute("Regiao Fiscal"),
        CategoryAttribute("Cliente"),
        DefaultValueAttribute(" ")]
        public string RegiaoFiscal { get; set; }

        [DescriptionAttribute("Grupo Impostos"),
        CategoryAttribute("Cliente"),
        DefaultValueAttribute(" ")]
        public string GrupoImpostos { get; set; }

        [DescriptionAttribute("Grupo Preços"),
        CategoryAttribute("Cliente"),
        DefaultValueAttribute(" ")]
        public string GrupoPrecos { get; set; }

        [DescriptionAttribute("Categoria de Crédito"),
        CategoryAttribute("Cliente"),
        DefaultValueAttribute(" ")]
        public string CategCredito { get; set; }

        [DescriptionAttribute("Categoria de Crédito"),
        CategoryAttribute("Cliente"),
        DefaultValueAttribute(" ")]
        public string Tipo { get; set; }


        // Magento

        [DescriptionAttribute("Usuário Magento"),
        CategoryAttribute("Magento")]
        public string User { get; set; }

        [DescriptionAttribute("ApiKey Magento"),
        CategoryAttribute("Magento")]
        public string ApiKey { get; set; }


    }
}
