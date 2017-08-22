using System.Windows.Forms;

namespace HashHunters.SafeTemperature
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public double MaxTempValue => (double)nuMaxTemp.Value;

        public void SwitchAlert(bool ebabled)
        {
            lAlert.Visible = ebabled;
        }
    }
}
