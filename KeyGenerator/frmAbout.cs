using FoxLearn.License;
using System;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class frmAbout : Form
    {
        const int ProductCode = 130;

        public frmAbout()
        {
            InitializeComponent();
        }

        private void FromAbout_Load(object sender, EventArgs e)
        {
            lblProdId.Text = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(lblProdId.Text);
            LicenseInfo lic = new LicenseInfo();
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    lblProdName.Text = "RenTrade Service";
                    lblProdKey.Text = productKey;
                    if (kv.Type == LicenseType.TRIAL)
                        lblLicenseType.Text = string.Format("{0} days", (kv.Expiration - DateTime.Now.Date).Days);
                    else
                        lblLicenseType.Text = "Full";
                }
            } else
            {
                MessageBox.Show("Invalid license.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
