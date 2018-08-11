using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace ProFoxIntegracao
{
    public partial class frmAgenda : Form
    {
        DataTable dtAgenda = new DataTable();
        List<MagentoService.salesOrderListEntity> lstPedidos;
        IntegracaoConfig integracaoConfig = new IntegracaoConfig();

        public frmAgenda()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Magento magento = new Magento();
            magento.Login();
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {
            Db db = new Db();
            dtAgenda = db.GetAgenda();
            dataGridView1.DataSource = dtAgenda;

            timer1.Enabled = true;

            lstPedidos = new List<MagentoService.salesOrderListEntity>();
            loadConfig();
        }

        private void loadConfig()
        {
            try
            {
                FileStream fs = new FileStream(@"Config.Dat", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                integracaoConfig = (IntegracaoConfig)bf.Deserialize(fs);
                fs.Close();
            } catch ( Exception ex )
            {
                string mensagem = ex.Message;
                Log.Set("ERRO: " + mensagem);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAgendamento frmAgendamento = new FrmAgendamento();
            frmAgendamento.ShowDialog();

            Db db = new Db();
            dtAgenda = db.GetAgenda();
            dataGridView1.DataSource = dtAgenda;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridView1.SelectedRows;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                
                string hora = row.Cells[1].Value.ToString();
                string acao = row.Cells[2].Value.ToString();
                string msg = "Deseja realmente excluir este registro: \n\n " + hora + " - " + acao;
                DialogResult result = MessageBox.Show(msg, "Excluir Registro", MessageBoxButtons.YesNo);
                if ( result == DialogResult.Yes)
                {
                    string id = row.Cells[0].Value.ToString();
                    Db db = new Db();
                    db.DeleteAgenda(id);

                    dtAgenda = db.GetAgenda();
                    dataGridView1.DataSource = dtAgenda;
                }
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(DataRow r in dtAgenda.Rows)
            {
                string hora = r[1].ToString();
                var horarioA = TimeSpan.Parse(hora);
                var horarioB = TimeSpan.Parse(DateTime.Now.ToShortTimeString());

                if ( horarioA == horarioB)
                {
                    string acao = r[2].ToString();
                    
                    Magento magento = new Magento();
                    if ( acao == "Atualizar Preços")
                    {
                        magento.AtualizarPreco();
                    }
                    if (acao == "Cadastrar Produtos")
                    {
                        magento.CadastrarProdutos();
                    }
                    if (acao == "Atualizar Estoque")
                    {
                        magento.AtualizarEstoque();
                    }
                    if ( acao == "Buscar Pedidos")
                    {
                        magento.BuscarSalvarPedidos(integracaoConfig);
                    }
                }
            }
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Magento magento = new Magento();
            lstPedidos = magento.BuscarPedidos();
            dataGridView2.DataSource = lstPedidos;
            this.Cursor = Cursors.Default;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                int rowIndex = dataGridView2.CurrentCell.RowIndex;
                Magento magento = new Magento();

                MagentoService.salesOrderEntity salesOrderEntity = magento.GetInfoPedido(lstPedidos[rowIndex].increment_id);
                propertyGrid1.SelectedObject = salesOrderEntity;

                MagentoService.salesOrderAddressEntity billingAddress = salesOrderEntity.billing_address;
                MagentoService.customerCustomerEntity customerEntity = magento.GetCustomerInfo(salesOrderEntity.customer_id);

            } catch (Exception ex)
            {
                string mensagem = ex.Message;
            }
            this.Cursor = Cursors.Default;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Db db = new Db();
            string incrementId = "";
            try
            {
                int rowIndex = dataGridView2.CurrentCell.RowIndex;
                incrementId = lstPedidos[rowIndex].increment_id;

                db.savePedido(incrementId, integracaoConfig);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log.Set("ERRO Salvar Pedido: " + incrementId + " - " + message);
            }
            this.Cursor = Cursors.Default;
        }

        private void configuraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConfig frmConfig = new FrmConfig();
            frmConfig.ShowDialog();
            loadConfig();
        }
    }
}
