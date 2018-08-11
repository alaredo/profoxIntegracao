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
    public partial class FrmConfig : Form
    {
        IntegracaoConfig integracaoConfig = new IntegracaoConfig();
        public FrmConfig()
        {
            InitializeComponent();
        }

        
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"Config.Dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            integracaoConfig = (IntegracaoConfig)bf.Deserialize(fs);
            fs.Close();

            propertyGrid1.SelectedObject = integracaoConfig;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("Config.Dat", FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, integracaoConfig);
            fs.Close();

            this.Close();
        }
    }
}
