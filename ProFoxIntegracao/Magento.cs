using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace ProFoxIntegracao
{
    public class Magento
    {
        string user = "Faleiro";
        string apiKey = "faleiro";

        public void Login() {
           
            MagentoService.PortTypeClient portTypeClient = new MagentoService.PortTypeClient();
            String mSession = portTypeClient.login(user, apiKey);

            MagentoService.catalogCategoryTree catalogCategoryTree = new MagentoService.catalogCategoryTree();
            catalogCategoryTree = portTypeClient.catalogCategoryTree(mSession, "1","");

        }

        public void CadastrarProdutos()
        {
            try
            {
                MagentoService.PortTypeClient portTypeClient = new MagentoService.PortTypeClient();
                String mSession = portTypeClient.login(user, apiKey);

                MagentoService.filters filters = new MagentoService.filters();

                //  List <MagentoService.catalogProductAttributeSetEntity> lstCatalogAttributeEntity = new List<MagentoService.catalogProductAttributeSetEntity>();
                //  lstCatalogAttributeEntity = portTypeClient.catalogProductAttributeSetList(mSession).ToList<MagentoService.catalogProductAttributeSetEntity>();

                //  MagentoService.catalogCategoryTree catalogCategoryTree = new MagentoService.catalogCategoryTree();
                //  catalogCategoryTree = portTypeClient.catalogCategoryTree(mSession, "1", "");

                //  List<MagentoService.catalogProductTypeEntity> lstTypes = new List<MagentoService.catalogProductTypeEntity>();
                //  lstTypes = portTypeClient.catalogProductTypeList(mSession).ToList<MagentoService.catalogProductTypeEntity>();

                // List<MagentoService.anymarketAnymarketproductsListEntity> listProducts = new List<MagentoService.anymarketAnymarketproductsListEntity>();
                // listProducts = portTypeClient.anymarketAnymarketproductsList(mSession, filters).ToList<MagentoService.anymarketAnymarketproductsListEntity>();

                MagentoService.catalogProductEntity[] lstProducts = new MagentoService.catalogProductEntity[] { };

               // portTypeClient.catalogProductList(out lstProducts, mSession, filters, "1");
                portTypeClient.catalogProductList(out lstProducts, mSession, filters, "default");


                List<MagentoService.storeEntity> lstStore = new List<MagentoService.storeEntity>();
                lstStore = portTypeClient.storeList(mSession).ToList<MagentoService.storeEntity>();


                Db db = new Db();
                DataTable dataTable = new DataTable();
                dataTable = db.GetNovosProdutos();
                foreach( DataRow r in dataTable.Rows)
                {
                    try
                    {
                        MagentoService.catalogInventoryStockItemUpdateEntity catalogInventoryStockItemUpdateEntity = new MagentoService.catalogInventoryStockItemUpdateEntity();
                        catalogInventoryStockItemUpdateEntity.qty = r["saldo"].ToString().Trim();

                        MagentoService.catalogProductCreateEntity catalogProductCreateEntity = new MagentoService.catalogProductCreateEntity();
                        catalogProductCreateEntity.name = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.description = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.meta_description = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.meta_title = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.short_description = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.weight = r["peso"].ToString().Trim();
                        catalogProductCreateEntity.price = r["precoVenda1"].ToString().Trim();
                        catalogProductCreateEntity.stock_data = catalogInventoryStockItemUpdateEntity;
                        catalogProductCreateEntity.status = "Habilitado";
                        

                        // catalogProductCreateEntity.additional_attributes = lstCatalogAttributeEntity[0];
                        int result = portTypeClient.catalogProductCreate(mSession, "simple", "4", r["codigo"].ToString().Trim(), catalogProductCreateEntity, "default");

                        Log.Set("Sucesso Cadastro Produto: " + r["nome"].ToString().Trim());
                    } catch (Exception ex)
                    {
                        string message = ex.Message;
                        Log.Set("ERRO CadastrarProdutos: " + message);
                    }
                }

            } catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO CadastrarProdutos: " + message);
            }
        }

        public void AtualizarPreco(string tabPrecos)
        {
            try
            {
                MagentoService.PortTypeClient portTypeClient = new MagentoService.PortTypeClient();
                String mSession = portTypeClient.login(user, apiKey);
                string codigo = "";
                MagentoService.filters filters = new MagentoService.filters();

                Db db = new Db();
                DataTable dataTable = new DataTable();
                dataTable = db.GetPrecosProdutos(tabPrecos);
                foreach (DataRow r in dataTable.Rows)
                {
                    try
                    {
                        codigo = r["codigo"].ToString().Trim();
                        MagentoService.catalogInventoryStockItemUpdateEntity catalogInventoryStockItemUpdateEntity = new MagentoService.catalogInventoryStockItemUpdateEntity();
                        catalogInventoryStockItemUpdateEntity.qty = r["saldo"].ToString().Trim();

                        MagentoService.catalogProductCreateEntity catalogProductCreateEntity = new MagentoService.catalogProductCreateEntity();
                        catalogProductCreateEntity.name = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.description = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.meta_description = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.meta_title = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.short_description = r["nome"].ToString().Trim();
                        catalogProductCreateEntity.weight = r["peso"].ToString().Trim();
                        catalogProductCreateEntity.price = r["preco"].ToString().Trim();
                        catalogProductCreateEntity.stock_data = catalogInventoryStockItemUpdateEntity;


                        bool result = portTypeClient.catalogProductUpdate(mSession, r["codigo"].ToString().Trim(), catalogProductCreateEntity, "default", "sku");

                        Log.Set("Sucesso Atualizar Preço: " + r["codigo"].ToString().Trim() + " - " + r["preco"].ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        Log.Set("ERRO AtualizarPreco: " + codigo  + " - " + message);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO AtualizarPreco: " + message);
            }
        }

        public void AtualizarEstoque()
        {
            try
            {
                MagentoService.PortTypeClient portTypeClient = new MagentoService.PortTypeClient();
                String mSession = portTypeClient.login(user, apiKey);
                string codigo = "";
                MagentoService.filters filters = new MagentoService.filters();

                Db db = new Db();
                DataTable dataTable = new DataTable();
                dataTable = db.GetProdutosCadastrados();
                foreach (DataRow r in dataTable.Rows)
                {
                    try
                    {
                        MagentoService.catalogInventoryStockItemUpdateEntity catalogInventoryStockItemUpdateEntity = new MagentoService.catalogInventoryStockItemUpdateEntity();
                        catalogInventoryStockItemUpdateEntity.qty = r["saldo"].ToString().Trim();

                        codigo = r["codigo"].ToString().Trim();

                        int result = portTypeClient.catalogInventoryStockItemUpdate(mSession, r["codigo"].ToString().Trim(), catalogInventoryStockItemUpdateEntity);

                        // catalogProductCreateEntity.additional_attributes = lstCatalogAttributeEntity[0];
                        //int result = portTypeClient.catalogProductCreate(mSession, "simple", "4", r["codigo"].ToString().Trim(), catalogProductCreateEntity, "default");

                        Log.Set("Sucesso Atualizar Preço: " + r["codigo"].ToString().Trim() + " - " + r["saldo"].ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                        Log.Set("ERRO AtualizarEstoque: " + codigo + " - " + message);
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO AtualizarEstoque: " + message);
            }
        }

        public List<MagentoService.salesOrderListEntity> BuscarPedidos()
        {
            List<MagentoService.salesOrderListEntity> listOrder = new List<MagentoService.salesOrderListEntity>();
            try
            {
                MagentoService.PortTypeClient portTypeClient = new MagentoService.PortTypeClient();
                String mSession = portTypeClient.login(user, apiKey);

                MagentoService.filters filters = new MagentoService.filters();
                List<MagentoService.associativeEntity> theEntities = new List<MagentoService.associativeEntity>
                {
                    new MagentoService.associativeEntity { key = "status", value = "Pending" }
                };

                filters.filter = theEntities.ToArray();

                listOrder = portTypeClient.salesOrderList(mSession, filters).ToList<MagentoService.salesOrderListEntity>();
            } catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO BuscarPedidos: " + message);
            }
           
            return listOrder;
        }

        public MagentoService.salesOrderEntity GetInfoPedido(string increment_id)
        {
            MagentoService.salesOrderEntity order = new MagentoService.salesOrderEntity();
            try
            {
                MagentoService.PortTypeClient portTypeClient = new MagentoService.PortTypeClient();
                String mSession = portTypeClient.login(user, apiKey);
                
                order = portTypeClient.salesOrderInfo(mSession, increment_id);
                
                
                
            } catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO GetInfoPedido: " + message);
            }
            return order;
        }

        public MagentoService.customerCustomerEntity GetCustomerInfo(string customer_id)
        {
            MagentoService.customerCustomerEntity customer = new MagentoService.customerCustomerEntity();
            try
            {
                MagentoService.PortTypeClient portTypeClient = new MagentoService.PortTypeClient();
                String mSession = portTypeClient.login(user, apiKey);
                
                customer = portTypeClient.customerCustomerInfo(mSession, Convert.ToInt32(customer_id), null);
            } catch (Exception ex) {
                string messagem = ex.Message;
                Log.Set("ERRO GetCustomerInfo: " + messagem);
                
            }

            return customer;
        }

        public void BuscarSalvarPedidos(IntegracaoConfig integracaoConfig)
        {
            List<MagentoService.salesOrderListEntity> lstPedidos;
            Db db = new Db();

            Magento magento = new Magento();
            lstPedidos = magento.BuscarPedidos();
            foreach (MagentoService.salesOrderListEntity p in lstPedidos)
            {
                string incrementId = "";
                incrementId = p.increment_id;
                
                db.savePedido(incrementId, integracaoConfig);
            }   
            
        }
    }
}
