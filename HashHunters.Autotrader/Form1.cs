using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HashHunters.Autotrader.MarketsAPI;

namespace HashHunters.Autotrader
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<string, IMarketAPI> Markets;
        private IMarketAPI Market;

        public Form1(IEnumerable<IMarketAPI> markets )
        {
            InitializeComponent();
            Markets = markets.ToDictionary(x => x.Name);

            cbMarkets.SelectedIndexChanged += CbMarkets_SelectedIndexChanged;
            cbMarkets.DataSource = Markets.Keys.ToList();
        }

        private void CbMarkets_SelectedIndexChanged(object sender, EventArgs e)
        {
            var marketName = cbMarkets.SelectedItem as string;
            if (string.IsNullOrEmpty(marketName))
                return;
            Market = Markets[marketName];
        }

        private void bGetTicker_Click(object sender, EventArgs e)
        {
            var res = Market.GetTicker(tbMarketName.Text);
        }

        private void bGetSummary_Click(object sender, EventArgs e)
        {
            Market.GetMarketSummary(tbMarketName.Text);
        }
    }
}
