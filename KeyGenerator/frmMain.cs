using FoxLearn.License;
using System;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class frmMain : Form
    {
        const int ProductCode = 130;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            cmbLicenseType.SelectedIndex = 0;
            txtProdId.Text = ComputerInfo.GetComputerId();
        }

        private void btnGenerate_Click(object sender, System.EventArgs e)
        {
            KeyManager km = new KeyManager(txtProdId.Text);
            KeyValuesClass kv;
            string productKey = string.Empty;
            if (cmbLicenseType.SelectedIndex == 0)
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.FULL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)ProductCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1
                };
                if (!km.GenerateKey(kv, ref productKey))
                    txtProdKey.Text = "ERROR";
            }
            else
            {
                kv = new KeyValuesClass()
                {
                    Type = LicenseType.TRIAL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)ProductCode,
                    Edition = Edition.ENTERPRISE,
                    Version = 1,
                    Expiration = DateTime.Now.Date.AddDays(Convert.ToInt32(txtExpDays.Text))
                };
                if (!km.GenerateKey(kv, ref productKey))
                    txtProdKey.Text = "ERROR";
            }
            txtProdKey.Text = productKey;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            using (frmRegistration frm = new frmRegistration())
            {
                frm.ShowDialog();
            }
        }
    }
}
