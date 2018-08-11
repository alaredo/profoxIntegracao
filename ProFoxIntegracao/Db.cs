using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace ProFoxIntegracao
{
    public class Db
    {

        string connectionStr =
        ConfigurationManager.ConnectionStrings["MinhaStringDeConexao"].ConnectionString;

        //string connectionStr = "Data Source=DESKTOP-J70378C\\SQLEXPRESS;Initial Catalog=DBIX;Integrated Security=True";

        public DataTable Search(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection(connectionStr);
                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataReader dr = cmd.ExecuteReader();

                dt.Load(dr);

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return dt;
        }

        public DataTable GetAgenda()
        {
            string strQuery = "select * from agendaintegracao order by hora";
            DataTable dt = new DataTable();
            dt = Search(strQuery);
            return dt;
        }

        public void InsertAgenda(string hora, string acao)
        {
            try
            {
                string strQuery = "insert into agendaintegracao values ( '" + hora + "', '" + acao + "' )";
                SqlConnection sqlConn = new SqlConnection(connectionStr);
                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(strQuery, sqlConn);
                cmd.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO InsertAgenda: " + message);
            }
        }

        public void DeleteAgenda(string id)
        {
            try
            {
                string strQuery = "delete from agendaintegracao where id = " + id;
                SqlConnection sqlConn = new SqlConnection(connectionStr);
                sqlConn.Open();

                SqlCommand cmd = new SqlCommand(strQuery, sqlConn);
                cmd.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO DeleteAgenda: " + message);
            }
        }

        public DataTable GetNovosProdutos()
        {
            string strQuery = "select a.codigo, a.Nome, a.texto, a.grupo, a.subgrupo, a.familia, a.peso, a.unidade, a.PRECOVENDA1, a.saldo, a.grafico from produtos a, alternativos b where a.seq = b.seqproduto and b.VaiParaECommerce = 'S'";
            DataTable dt = new DataTable();
            dt = Search(strQuery);
            return dt;
        }

        public DataTable GetProdutosCadastrados()
        {
            string strQuery = "select a.codigo, a.Nome, a.texto, a.grupo, a.subgrupo, a.familia, a.peso, a.unidade, a.PRECOVENDA1, a.saldo, a.grafico from produtos a, alternativos b where a.seq = b.seqproduto and b.VaiParaECommerce = 'S'";
            DataTable dt = new DataTable();
            dt = Search(strQuery);
            return dt;
        }

        public bool InsertCliente(Cliente cliente, SqlConnection sqlConn, SqlTransaction transacao)
        {
            bool retorno = true;
            string strCode = "W" + cliente.Codigo; // getNextClienteId();
            cliente.Empresa = "001";
            cliente.Cnpj = cliente.Cnpj.Replace(".", "");
            cliente.Cnpj = cliente.Cnpj.Replace("-", "");

            cliente.Cep = cliente.Cep.Replace("-", "");

            string[] endereco;
            decimal enderecoNumero = 0;
            endereco = cliente.Endereco.Split('\n');
            enderecoNumero = Convert.ToDecimal(endereco[1]);
            //cliente.Codigo = strCode;

            string strQuery = $@"insert into clientes (Empresa, 
                Codigo,
                Nome, 
                NomeReduzido, 
                Cnpj, 
                Endereco,
                Bairro,
                Cidade, 
                Cep,
                Pais,
                Pessoa, 
                Estatus, 
                Email, 
                Telefone,
                Celular,
                Fax,
                DattaInclusao,
                TipoContribuinte,
                Qualidade,
                Essencia,
                CContabil,
                Sexo,
                Email60,
                EmailFinanceiro, 
                EnderecoNumero,
                InscEst,             
                Doc,                 
                RegiaoVendas,        
                RegiaoFiscal,        
                GrupoImpostos,       
                GrupoPrecos,         
                CategCredito,       
                Vendedor,            
                Portador,            
                Holding,             
                Renda,               
                Compras,             
                DiasAtraso,          
                Moratraso,          
                Contato,             
                DattaNascimento,     
                Xcomissao,           
                Texto,               
                Arquivo,             
                Filiacao,            
                Edicao,              
                Operador,            
                Ficha,               
                Xcusto,              
                Bairro35,            
                Cidade35,            
                PontoReferencia,     
                Distrito,            
                HorarioEntrega,      
                EnderecoComplemento, 
                InscMunicipal,       
                HabilitacaoSituacao, 
                HabilitacaoDatta,    
                HabilitacaoCodigo,   
                EmailNFE ,
                Tipo,
                TipoDoc,
                Estado,
                Datta
                )
            values (
                '{cliente.Empresa}',
                '{cliente.Codigo}',
                '{cliente.Nome}',         
                '{cliente.NomeReduzido}', 
                '{cliente.Cnpj}', 
                '{endereco[0]}',
                '{endereco[3]}',
                '{cliente.Cidade}', 
                '{cliente.Cep}',
                '{cliente.Pais}',
                '{cliente.Pessoa}',       
                '{cliente.Estatus}',
                '{cliente.Email}', 
                '{cliente.Telefone}',
                '{cliente.Celular}',
                '?',
                getdate(),
                9,
                0, 
                'C',
                '999',
                'M',
                '{cliente.Email}',
                '{cliente.Email}',
                '{enderecoNumero}',
                '{cliente.InscEst}',             
                '{cliente.Doc}',                 
                '{cliente.RegiaoVendas}',        
                '{cliente.RegiaoFiscal}',        
                '{cliente.GrupoImpostos}',       
                '{cliente.GrupoPrecos}',         
                '{cliente.CategCredito}',        
                '{cliente.Vendedor}',            
                '{cliente.Portador}',            
                '{cliente.Holding}',             
                {cliente.Renda},               
                {cliente.Compras},             
                {cliente.DiasAtraso},          
                {cliente.MoraAtraso},          
                '{cliente.Contato}',             
                getdate(),     
                '{cliente.Xcomissao}',          
                '{cliente.Texto}',               
                '{cliente.Arquivo}',             
                '{cliente.Filiacao}',            
                '{cliente.Edicao}',              
                '{cliente.Operador}',            
                '{cliente.Ficha}',               
                '{cliente.Xcusto}',              
                '{cliente.Bairro35}',            
                '{cliente.Cidade35}',            
                '{cliente.PontoReferencia}',     
                '{cliente.Distrito}',            
                '{cliente.HorarioEntrega}',      
                '{cliente.EnderecoComplemento}', 
                '{cliente.InscMunicipal}',       
                '{cliente.HabilitacaoSituacao}', 
                getdate(),    
                '{cliente.HabilitacaoCodigo}',   
                '{cliente.EmailNFE}',
                '{cliente.Tipo}',
                ' ',
                'MG',
                getdate()
            )";

            try
            {
                SqlCommand cmd = new SqlCommand(strQuery, sqlConn, transacao);
                cmd.ExecuteNonQuery();

                strQuery = $"insert into diario2 values ( '001', '1313', '201808090958', 'fmclientes','01','INCL', '{cliente.Codigo}')";
                cmd = new SqlCommand(strQuery, sqlConn, transacao);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO InsertCliente: " + message);
                retorno = false;
            }

            return retorno;
        }

        public string getNextClienteId()
        {
            string strId = "";

            //string strQuery = "select max(codigo) from clientes where codigo like 'W%'";
            string strQuery = "select Registro from Diario2 where Numero = (Select Max(Numero) from Diario2 where Tela = 'fmclientes' and Operacao = 'INCL' and Empresa = '001')";
            DataTable dt = new DataTable();
            dt = Search(strQuery);
            string codigo = dt.Rows[0][0].ToString();
            if (codigo == "")
            {
                strId = "00001";
            }
            else
            {
                strId = Convert.ToString(Convert.ToInt16(codigo.Substring(1, 5)) + 1).PadLeft(5, '0');
            }
            //strId = "W" + strId;
            return strId;
        }

        public bool insertPedido(Pedido pedido, SqlConnection sqlConn, SqlTransaction transacao)
        {
            bool retorno = true;
            string strId = "";

            pedido.Empresa = "001";

            string strQuery = $@"insert into Pedidos (
                Empresa,
                Seq,
                Fato,
                Filial,
                Codigo,
                Texto,
                Observacao,
                Tipo,
                Datta,
                Dattapreventrega,
                Estatus,
                Percentrega,
                Valor,
                Liberacao,
                DescontoValor,
                Razao,
                RazaoPositiva,
                RazaoNegativa,
                NumeroExterno,
                Edicao,
                Operador,
                Origem,
                Ccusto,
                Cifob,
                FormaNegociacao,
                cpg,
                Transportadora,
                Cfop,
                CfopDescricao,
                Destinacao,
                Natureza,
                DescontoPerc,
                DescontoCalc,
                Deducoes,
                DespesaDentro,
                DespesaFora,
                Ipi,
                Icms,
                Icmr,
                Iss,
                Padrao,
                Tic,
                HorarioEntrega,
                SubstitutoTributario,
                Posicao
            ) values (
                '{pedido.Empresa}',
                '{pedido.Seq}',
                '{pedido.Fato}',
                '{pedido.Filial}',
                '{pedido.Codigo}',
                '{pedido.Texto}',
                '{pedido.Observação}',
                '{pedido.Tipo}',
                '{pedido.Datta}',
                '{pedido.Dattapreventrega}',
                '{pedido.Estatus}',
                {pedido.Percentrega},
                {pedido.Valor.ToString().Replace(',','.')},
                '{pedido.Liberacao}',
                {pedido.DescontoValor.ToString().Replace(',', '.')},
                {pedido.Razao},
                {pedido.RazaoPositiva},
                {pedido.RazaoNegativa},
                '{pedido.NumeroExterno}',
                '{pedido.Edicao}',
                '{pedido.Operador}',
                '{pedido.Origem}',
                '{pedido.Ccusto}',
                1,
                '{pedido.Formanegociacao}',
                '{pedido.Cpg}',
                '{pedido.Transportadora}',
                '{pedido.Cfop}',
                '{pedido.Cfopdescricao}',
                '{pedido.Destinacao}',
                '{pedido.Natureza}',
                {pedido.DescontoPerc.ToString().Replace(',', '.')},
                {pedido.DescontoCalc.ToString().Replace(',', '.')},
                {pedido.Deducoes.ToString().Replace(',', '.')},
                {pedido.DespesaDentro},
                {pedido.DespesaFora},
                {pedido.Ipi},
                {pedido.Icms},
                {pedido.Icmr},
                {pedido.Iss},
                '{pedido.Padrao}',
                '{pedido.Tic}',
                '{pedido.HorarioEntrega}',
                '{pedido.SubstitutoTributario}',
                '{pedido.Posicao}'
            )";

            try
            {

                SqlCommand cmd = new SqlCommand(strQuery, sqlConn, transacao);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO insertPedido: " + pedido.Codigo + " - " + message);
                retorno = false;
            }

            return retorno;
        }

        public string getNextPedidoId()
        {
            string strId = "";

            string strQuery = "select max(Seq) from pedidos where seq like 'W%'";
            DataTable dt = new DataTable();
            dt = Search(strQuery);
            string codigo = dt.Rows[0][0].ToString();
            if (codigo == "")
            {
                strId = "0000000001";
            }
            else
            {
                strId = Convert.ToString(Convert.ToInt16(codigo.Substring(1, 10)) + 1).PadLeft(10, '0');
            }
            strId = "W" + strId;
            return strId;
        }

        public bool insertPedidoParc(PedidosParc pedidoParc, SqlConnection sqlConn, SqlTransaction transacao)
        {
            bool retorno = true;

            string strQuery = $@"insert into PedidosParc (
                                    SeqPedido,      
                                    Parcela,        
                                    Valor,       
                                    DattaVenc,         
                                    Natureza,        
                                    Portador,         
                                    Especie,       
                                    DescIncond,
                                    FlagPaga,           
                                    Xcomissao,       
                                    TaxaPermanencia,         
                                    DiasPermanencia,         
                                    TaxaAdminist,            
                                    VrVariacao,         
                                    Empresa,
                                    Edicao,
                                    Operador,
                                    FormaReajuste,
                                    Indice,
                                    VrIndexado,
                                    VrDespesas,
                                    PercMulta
                                ) values (
                                    '{pedidoParc.SeqPedido}',      
                                    '{pedidoParc.Parcela}',        
                                    {pedidoParc.Valor.ToString().Replace(',', '.')},       
                                    '{pedidoParc.Dattavenc}',      
                                    '{pedidoParc.Natureza}',
                                    '{pedidoParc.Portador}',       
                                    '{pedidoParc.Especie}',       
                                    {pedidoParc.Descincond},
                                    '{pedidoParc.Flagpaga}',       
                                    {pedidoParc.Xcomissao},      
                                    {pedidoParc.TaxaPermanencia},
                                    {pedidoParc.DiasPermanencia},
                                    {pedidoParc.TaxaAdminist},   
                                    {pedidoParc.VrVariacao},     
                                    '{pedidoParc.Empresa}',
                                    '{pedidoParc.Edicao}',
                                    '{pedidoParc.Operador}',
                                    '{pedidoParc.FormaReajuste}',
                                    '{pedidoParc.Indice}',
                                    {pedidoParc.VrIndexado},
                                    {pedidoParc.VrDespesas},
                                    {pedidoParc.PercMulta}
                                   )";
            try
            {
                SqlCommand cmd = new SqlCommand(strQuery, sqlConn, transacao);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO insertPedidoParc: " + pedidoParc.SeqPedido + " - " + message);
                retorno = false;
            }

            return retorno;
        }

        public bool insertPedidoCliente(PedidoCliente pedidoCliente, SqlConnection sqlConn, SqlTransaction transacao)
        {
            bool retorno = true;
            string[] endereco;
            decimal enderecoNumero = 0;
            endereco = pedidoCliente.Endereco.Split('\n');
            enderecoNumero = Convert.ToDecimal(endereco[1]);

            string strQuery = $@"insert into PedidosClientes (
                                    SeqPedido,      
                                    Cliente,        
                                    Vendedor,       
                                    Equipe,         
                                    Empresa,        
                                    Edicao,         
                                    Operador,       
                                    ClienteGenerico,
                                    Nome,           
                                    Endereco,       
                                    Bairro,         
                                    Cidade,         
                                    Cep,            
                                    Estado,         
                                    Telefone
                                ) values (
                                    '{pedidoCliente.SeqPedido}',      
                                    '{pedidoCliente.Cliente}',        
                                    '{pedidoCliente.Vendedor}',       
                                    '{pedidoCliente.Equipe}',         
                                    '{pedidoCliente.Empresa}',        
                                    '{pedidoCliente.Edicao}',         
                                    '{pedidoCliente.Operador}',       
                                    '{pedidoCliente.ClienteGenerico}',
                                    '{pedidoCliente.Nome}',           
                                    '{endereco[0]}',       
                                    '{endereco[3]}',         
                                    '{pedidoCliente.Cidade}',         
                                    '{pedidoCliente.Cep}',            
                                    '{pedidoCliente.Estado}',         
                                    '{pedidoCliente.Telefone}'
                                )";
            try
            {
                SqlCommand cmd = new SqlCommand(strQuery, sqlConn, transacao);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO insertPedidoCliente: " + pedidoCliente.SeqPedido + " - " + message);
                retorno = false;
            }
            return retorno;
        }

        public bool insertPedidoItem(PedidoItens pedidoItens, SqlConnection sqlConn, SqlTransaction transacao)
        {
            bool retorno = true;
            string strQuery = $@"insert into pedidosItens (
                Seq,                 
                SeqPedido,           
                Item,                
                SeqProduto,          
                Alternativo,         
                Nome,                
                Unidade,             
                Proporcao,           
                DattaPrevEntrega,    
                DattaUltEntrega,     
                Estatus,             
                QPedida,             
                QExpedida,           
                QFaturada,           
                QCancelada,          
                PrecoUnit,           
                Precounittab,        
                Precounittabmin,     
                Precounittabmax,     
                Cst,                 
                Bonificacao,         
                Texto,               
                Valor,                    
                Liberacao,           
                LiberacaoOperador,   
                LiberacaoDatta,      
                Cfop,                
                Destinacao,          
                Propriedade,         
                Descontovalor,       
                Descontoperc,        
                Descontocalc,        
                Descontocred,        
                Ipi,                 
                Icms,                
                Icmr,                
                Iss,                 
                Ordem,               
                Vendedor,            
                Empresa,             
                Edicao,              
                Operador,            
                NomeExtra,          
                Custo,               
                CustoVenda,          
                CustoMedio,          
                Qualidade,           
                Tipo,                
                CodigoExternoProduto,
                Cest                 
            ) values (
                '{pedidoItens.Seq}',
                '{pedidoItens.SeqPedido}',           
                {pedidoItens.Item},                
                '{pedidoItens.SeqProduto}',          
                '{pedidoItens.Alternativo}',         
                '{pedidoItens.Nome}',                
                '{pedidoItens.Unidade}',             
                {pedidoItens.Proporcao},           
                '{pedidoItens.DattaPrevEntrega}',    
                '{pedidoItens.DattaUltEntrega}',     
                '{pedidoItens.Estatus}',             
                {pedidoItens.QPedida.ToString().Replace(',', '.')},             
                {pedidoItens.QExpedida.ToString().Replace(',', '.')},           
                {pedidoItens.QFaturada.ToString().Replace(',', '.')},           
                {pedidoItens.QCancelada.ToString().Replace(',', '.')},          
                {pedidoItens.PrecoUnit.ToString().Replace(',', '.')},           
                {pedidoItens.Precounittab.ToString().Replace(',', '.')},        
                {pedidoItens.Precounittabmin.ToString().Replace(',', '.')},     
                {pedidoItens.Precounittabmax.ToString().Replace(',', '.')},     
                '{pedidoItens.Cst}',                 
                '{pedidoItens.Bonificacao}',         
                '{pedidoItens.Texto}',               
                {pedidoItens.Valor.ToString().Replace(',', '.')},                      
                '{pedidoItens.Liberacao}',           
                '{pedidoItens.LiberacaoOperador}',   
                '{pedidoItens.LiberacaoDatta}',      
                '{pedidoItens.Cfop}',                
                '{pedidoItens.Destinacao}',          
                '{pedidoItens.Propriedade}',         
                {pedidoItens.Descontovalor.ToString().Replace(',', '.')},       
                {pedidoItens.Descontoperc.ToString().Replace(',', '.')},        
                {pedidoItens.Descontocalc.ToString().Replace(',', '.')},        
                {pedidoItens.Descontocred.ToString().Replace(',', '.')},        
                {pedidoItens.Ipi},                 
                {pedidoItens.Icms},                
                {pedidoItens.Icmr},                
                {pedidoItens.Iss},                 
                {pedidoItens.Ordem},               
                '{pedidoItens.Vendedor}',            
                '{pedidoItens.Empresa}',             
                '{pedidoItens.Edicao}',              
                '{pedidoItens.Operador}',            
                '{pedidoItens.NomeExtra}',                      
                {pedidoItens.Custo},               
                {pedidoItens.CustoVenda},          
                {pedidoItens.CustoMedio},          
                {pedidoItens.Qualidade},           
                '{pedidoItens.Tipo}',                
                '{pedidoItens.CodigoExternoProduto}',
                '{pedidoItens.Cest}'                 
            )";

            try
            {
                SqlCommand cmd = new SqlCommand(strQuery, sqlConn, transacao);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO insertPedidoItem: " + pedidoItens.SeqProduto + " - " + message);
                retorno = false;
            }

            return retorno;
        }

        public string getNextPedidoItemId()
        {
            string strId = "";

            string strQuery = "select max(Seq) from pedidosItens where seq like 'W%'";
            DataTable dt = new DataTable();
            dt = Search(strQuery);
            string codigo = dt.Rows[0][0].ToString();
            if (codigo == "")
            {
                strId = "0000000001";
            }
            else
            {
                strId = Convert.ToString(Convert.ToInt16(codigo.Substring(1, 10)) + 1).PadLeft(10, '0');
            }
            strId = "W" + strId;
            return strId;
        }

        public DataTable getProduto(string codigoAlternativo)
        {
            DataTable retorno = new DataTable();
            string strQuery = $"select * from produtos, alternativos where alternativos.codigo = '{codigoAlternativo}' and alternativos.seqproduto = produtos.seq";
            try
            {
                retorno = Search(strQuery);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO getProduto: " + codigoAlternativo + " - " + message);
            }

            return retorno;
        }

        public void savePedido(string increment_id, IntegracaoConfig integracaoConfig)
        {

            SqlConnection sqlConn = new SqlConnection(connectionStr);
            sqlConn.Open();
            SqlTransaction transacao;

            transacao = sqlConn.BeginTransaction();
            try
            {

                Magento magento = new Magento();

                MagentoService.salesOrderEntity salesOrderEntity = magento.GetInfoPedido(increment_id);
                //propertyGrid1.SelectedObject = salesOrderEntity;

                MagentoService.salesOrderAddressEntity billingAddress = salesOrderEntity.billing_address;
                MagentoService.customerCustomerEntity customerEntity = magento.GetCustomerInfo(salesOrderEntity.customer_id);

                Cliente cliente = new Cliente();

                cliente.Codigo = getNextClienteId(); //"W" + Convert.ToString(customerEntity.customer_id);
                cliente.Nome = customerEntity.firstname.Trim() + " " + customerEntity.lastname.Trim();
                cliente.NomeReduzido = customerEntity.firstname.Trim();
                cliente.Cnpj = customerEntity.taxvat;
                cliente.Pessoa = "F";
                cliente.Estatus = "A";
                cliente.Email = customerEntity.email.Trim();
                string strDate = customerEntity.created_at.ToString();
               // cliente.Datta = Convert.ToDateTime(DateTime.Now.ToString("yyyyMMddHHmmss")).ToString("yyyyMMddHHmmss");
                cliente.DattaInclusao = DateTime.Now.ToString();

                cliente.Endereco = billingAddress.street;
                cliente.Bairro = "?";
                cliente.Cidade = billingAddress.city;
                cliente.Cep = billingAddress.postcode;
                cliente.Cidade = billingAddress.city;
                cliente.Pais = "Brasil";

                cliente.Telefone = "?";
                cliente.Celular = "?";


                cliente.InscEst = "ISENTO";
                cliente.Doc = " ";
                cliente.Tipo = integracaoConfig.Tipo;
                cliente.RegiaoVendas = integracaoConfig.RegiaoVendas;
                cliente.RegiaoFiscal = integracaoConfig.RegiaoFiscal;
                cliente.GrupoImpostos = integracaoConfig.GrupoImpostos;
                cliente.GrupoPrecos = integracaoConfig.GrupoPrecos;
                cliente.CategCredito = integracaoConfig.CategCredito;
                cliente.Vendedor = integracaoConfig.Vendedor;
                cliente.Portador = integracaoConfig.Portador;
                cliente.Holding = cliente.Codigo;
                cliente.Renda = 0;
                cliente.Compras = 0;
                cliente.DiasAtraso = 0;
                cliente.MoraAtraso = 0;
                cliente.Contato = " ";
                cliente.DattaNascimento = cliente.Datta;
                cliente.Xcomissao = 1;
                cliente.Texto = "Magento";
                cliente.Arquivo = " ";
                cliente.Filiacao = " ";
                cliente.Edicao = cliente.Datta;
                cliente.Operador = integracaoConfig.Operador;
                cliente.Ficha = " ";
                cliente.Xcusto = 1;
                cliente.Bairro35 = cliente.Bairro;
                cliente.Cidade35 = cliente.Cidade;
                cliente.PontoReferencia = " ";
                cliente.Distrito = " ";
                cliente.HorarioEntrega = " ";
                cliente.EnderecoComplemento = " ";
                cliente.InscMunicipal = " ";
                cliente.HabilitacaoSituacao = "S";
                cliente.HabilitacaoDatta = DateTime.Now;
                cliente.HabilitacaoCodigo = " ";
                cliente.EmailNFE = cliente.Email;
                cliente.EmailFinanceiro = " ";
                cliente.CContabil = cliente.Codigo;



                bool error = false;
                if ( InsertCliente(cliente, sqlConn, transacao)) { 

                    Pedido pedido = new Pedido();
                    pedido.Seq = getNextPedidoId();
                    pedido.Fato = "SC";
                    pedido.Filial = integracaoConfig.Filial;
                    pedido.Codigo = salesOrderEntity.increment_id;
                    pedido.Tipo = "N";
                    pedido.Datta = Convert.ToDateTime(salesOrderEntity.created_at.ToString());
                    pedido.Dattapreventrega = DateTime.Now;
                    pedido.Estatus = "A";
                    pedido.Percentrega = 0;
                    pedido.Valor = Convert.ToDecimal(salesOrderEntity.grand_total.Replace('.', ','));
                    pedido.Liberacao = integracaoConfig.Liberacao;
                    pedido.DescontoValor = Convert.ToDecimal(salesOrderEntity.discount_amount.Replace('.', ','));
                    pedido.Razao = 1;
                    pedido.RazaoPositiva = 1;
                    pedido.RazaoNegativa = 1;
                    pedido.NumeroExterno = salesOrderEntity.order_id;
                    pedido.Edicao = DateTime.Now.ToString("yyyyMMddHHmmss");
                    pedido.Operador = "1313";
                    pedido.Origem = integracaoConfig.Origem;

                    pedido.Ccusto = integracaoConfig.Ccusto;

                    pedido.Formanegociacao = integracaoConfig.FormaNegociacao;
                    pedido.Cpg = integracaoConfig.CPG;
                    pedido.Transportadora = integracaoConfig.Transportadora;
                    pedido.Cfop = integracaoConfig.Cfop;
                    pedido.Cfopdescricao = "Venda";
                    pedido.Destinacao = "C";
                    pedido.Natureza = integracaoConfig.Natureza;
                    pedido.DescontoPerc = 0;
                    pedido.DescontoCalc = 0;
                    pedido.Deducoes = 0;
                    pedido.DespesaDentro = 0;
                    pedido.DespesaFora = 0;
                    pedido.Ipi = 0;
                    pedido.Icms = 0;
                    pedido.Icmr = 0;
                    pedido.Iss = 0;
                    pedido.Padrao = "X";
                    pedido.Tic = integracaoConfig.Tic;
                    pedido.HorarioEntrega = DateTime.Now.ToString("yyyyMMddHHmm");
                    pedido.SubstitutoTributario = "";
                    pedido.Posicao = "1";


                    


                    if (insertPedido(pedido, sqlConn, transacao))
                    {
                        PedidoCliente pedidoCliente = new PedidoCliente();
                        pedidoCliente.SeqPedido = pedido.Seq;
                        pedidoCliente.Cliente = cliente.Codigo;
                        pedidoCliente.Empresa = "001";
                        pedidoCliente.Edicao = DateTime.Now.ToString("yyyyMMddHHmmss");
                        pedidoCliente.Operador = "1313";
                        pedidoCliente.ClienteGenerico = "N";
                        pedidoCliente.Nome = cliente.Nome;
                        pedidoCliente.Endereco = cliente.Endereco;
                        pedidoCliente.Bairro = cliente.Bairro;
                        pedidoCliente.Cidade = cliente.Cidade;
                        pedidoCliente.Cep = cliente.Cep;
                        pedidoCliente.Estado = cliente.Estado;
                        pedidoCliente.Telefone = cliente.Telefone;

                        pedidoCliente.Vendedor = integracaoConfig.Vendedor;
                        pedidoCliente.Equipe = integracaoConfig.Equipe;

                        if (insertPedidoCliente(pedidoCliente, sqlConn, transacao))
                        {

                            PedidosParc pedidosParc = new PedidosParc();
                            pedidosParc.SeqPedido = pedido.Seq;
                            pedidosParc.Parcela = "001";
                            pedidosParc.Valor = pedido.Valor;
                            pedidosParc.Dattavenc = DateTime.Now.AddDays(30);
                            pedidosParc.Natureza = integracaoConfig.Natureza;
                            pedidosParc.Portador = integracaoConfig.Portador;
                            pedidosParc.Especie = integracaoConfig.Especie;
                            pedidosParc.Descincond = 0;
                            pedidosParc.Flagpaga = "N";
                            pedidosParc.Xcomissao = 0;
                            pedidosParc.TaxaPermanencia = 0;
                            pedidosParc.DiasPermanencia = 0;
                            pedidosParc.TaxaAdminist = 0;
                            pedidosParc.VrVariacao = 0;
                            pedidosParc.Empresa = "001";
                            pedidosParc.Edicao = "Edicao";
                            pedidosParc.Operador = integracaoConfig.Operador;
                            pedidosParc.FormaReajuste = " ";
                            pedidosParc.Indice = "0";
                            pedidosParc.VrIndexado = 0;
                            pedidosParc.VrIndexado = 0;
                            pedidosParc.VrDespesas = 0;
                            pedidosParc.PercMulta = 0;

                            if (insertPedidoParc(pedidosParc, sqlConn, transacao))
                            {
                                int i = 1;
                                foreach (MagentoService.salesOrderItemEntity item in salesOrderEntity.items)
                                {

                                    DataTable dtProduto = getProduto(item.sku);
                                    if (dtProduto.Rows.Count > 0)
                                    {

                                        PedidoItens pedidoItens = new PedidoItens();
                                        pedidoItens.Seq = getNextPedidoItemId();
                                        pedidoItens.SeqPedido = pedido.Seq;
                                        pedidoItens.Item = i;
                                        pedidoItens.SeqProduto = Convert.ToString(dtProduto.Rows[0]["SEQ"]);
                                        pedidoItens.Alternativo = item.sku;
                                        pedidoItens.Nome = item.name.Length > 40 ? item.name.Substring(0, 39) : item.name;
                                        pedidoItens.Unidade = "UN";
                                        pedidoItens.Proporcao = 1;
                                        pedidoItens.DattaPrevEntrega = DateTime.Now;
                                        pedidoItens.DattaUltEntrega = DateTime.Now;
                                        pedidoItens.Estatus = "A";
                                        pedidoItens.QPedida = Convert.ToDecimal(item.qty_ordered.Replace('.',','));
                                        pedidoItens.QExpedida = 0;
                                        pedidoItens.QFaturada = 0;
                                        pedidoItens.QCancelada = Convert.ToDecimal(item.qty_canceled.Replace('.', ','));
                                        pedidoItens.MotivoCancelamento = "";
                                        pedidoItens.PrecoUnit = Convert.ToDecimal(item.price.Replace('.', ','));
                                        pedidoItens.Precounittab = Convert.ToDecimal(item.price.Replace('.', ',')); ;
                                        pedidoItens.Precounittabmin = Convert.ToDecimal(item.price.Replace('.', ',')); ;
                                        pedidoItens.Precounittabmax = Convert.ToDecimal(item.price.Replace('.', ',')); ;
                                        pedidoItens.Cst = "010";
                                        pedidoItens.Bonificacao = "N";
                                        pedidoItens.Texto = "";
                                        pedidoItens.Valor = Convert.ToDecimal(item.row_total.Replace('.', ','));
                                        pedidoItens.Peso = Convert.ToDecimal(item.row_weight.Replace('.', ','));
                                        pedidoItens.Liberacao = "N";
                                        pedidoItens.LiberacaoOperador = "";
                                        pedidoItens.LiberacaoDatta = DateTime.Now;
                                        pedidoItens.Destinacao = "R";
                                        pedidoItens.Propriedade = "P";
                                        pedidoItens.Descontovalor = Convert.ToDecimal(item.discount_amount.Replace('.', ','));
                                        pedidoItens.Descontoperc = Convert.ToDecimal(item.discount_percent.Replace('.', ','));
                                        pedidoItens.Descontocalc = 0;
                                        pedidoItens.Descontocred = 0;
                                        pedidoItens.Ipi = 0;
                                        pedidoItens.Icms = 0;
                                        pedidoItens.Iss = 0;
                                        pedidoItens.Ordem = i;
                                        pedidoItens.Empresa = "001";
                                        pedidoItens.Edicao = DateTime.Now.ToString("yyyyMMddHHmmss");
                                        pedidoItens.Operador = "1313";
                                        //pedidoItens.Rrimposto = "";
                                        pedidoItens.Qualidade = 0;
                                        pedidoItens.Tipo = "Normal";
                                        pedidoItens.CodigoExternoProduto = item.sku;
                                        pedidoItens.Cest = integracaoConfig.Cest;

                                        pedidoItens.Cfop = integracaoConfig.CfopDentroEstado;
                                        pedidoItens.Vendedor = integracaoConfig.Vendedor;

                                        if (!insertPedidoItem(pedidoItens, sqlConn, transacao))
                                        {
                                            transacao.Rollback();
                                            error = true;
                                            break;
                                        }
                                        i++;
                                    }
                                    else
                                    {
                                        string mensagem = "Produto nao encontrado - " + item.sku;
                                        Log.Set("ERRO GravarPedidos: " + pedido.Seq + " - " + mensagem);
                                        transacao.Rollback();
                                        error = true;
                                    }

                                }
                            } else
                            {
                                transacao.Rollback();
                                error = true;
                            }
                        } else
                        {
                            transacao.Rollback();
                            error = true;
                        }
                    } else
                    {
                        transacao.Rollback();
                        error = true;
                    }
                } else
                {
                    transacao.Rollback();
                    error = true;
                }
                if (!error)
                {
                    transacao.Commit();
                    MessageBox.Show("Pedido Importado com sucesso");
                } else
                {
                    MessageBox.Show("Não foi possivel importar o pedido");
                }
                    

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                string message = ex.Message;
                Log.Set("ERRO savePedido: " + message + " - " + message);
            }

        }
    }
}
