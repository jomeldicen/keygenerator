using System;
using FoxLearn.License;
using System.Windows.Forms;

namespace KeyGenerator
{
    public partial class frmRegistration : Form
    {
        public frmRegistration()
        {
            InitializeComponent();
        }

        const int ProductCode = 130;

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            txtProdId.Text = ComputerInfo.GetComputerId();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(txtProdId.Text);
            string productKey = txtProdKey.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Lear Corporation";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }
                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);
                    MessageBox.Show("You have been successfully registered.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
                MessageBox.Show("Your product key is invalid.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {

            using (frmAbout frm = new frmAbout())
            {
                frm.ShowDialog();
            }
        }
    }
}
