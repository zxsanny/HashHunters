using HashHunters.SafeTemperature.Extensions;

namespace HashHunters.SafeTemperature
{
    public interface IIO
    {
        double GetMaxTemperature();
        void SwitchAlert(bool enabled);
    }

    public class FormInputOutput : IIO
    {
        MainForm Form;

        public FormInputOutput(MainForm form)
        {
            Form = form;
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