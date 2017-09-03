using HashHunters.SafeTemperature.Extensions;
using System.Windows.Forms;

namespace HashHunters.SafeTemperature
{
    public interface IIO
    {
        double GetMaxTemperature();
        void SwitchAlert(bool enabled);
        void ViewForm();
    }

    public class FormInputOutput : IIO
    {
        MainForm Form;

        public FormInputOutput(MainForm form)
        {
            Form = form;
        }

        public void ViewForm()
        {
            Application.Run(Form);
        }

        public double GetMaxTemperature()
        {
            return Form.MaxTempValue;
        }

        public void SwitchAlert(bool enabled)
        {
            Form.InvokeEx(f => f.SwitchAlert(enabled));
        }
    }
}