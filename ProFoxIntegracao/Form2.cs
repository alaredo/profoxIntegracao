using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProFoxIntegracao
{
    public partial class FrmAgendamento : Form
    {
        public FrmAgendamento()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string hora = dateTimePicker1.Value.ToString("t");
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Escolha uma ação", "Ops!!!");
                return;
            }
            string acao = Convert.ToString(comboBox1.SelectedItem.ToString());
            
            Db db = new Db();
            db.InsertAgenda(hora, acao);
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
