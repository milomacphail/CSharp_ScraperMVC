using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Scraper
{
    class ScrapeData
      {
        private string _time_scraped;
        private IList<IWebElement> _stockSymbol;
        private IList<IWebElement> _stockLastPrice;
        private IList<IWebElement> _stockChange;
        private IList<IWebElement> _stockChangePercent;
        private IList<IWebElement> _stockVolume;
        private IList<IWebElement> _stockShares;
        private IList<IWebElement> _stockAvgVolume;
        private IList<IWebElement> _stockMarketCap;

        public string StockTimeScraped { get =>_time_scraped; set => _time_scraped = value; }
        public IList<IWebElement> StockSymbol { get => _stockSymbol; set => _stockSymbol = value; }
        public IList<IWebElement> StockLastPrice { get => _stockLastPrice; set => _stockLastPrice = value; }
        public IList<IWebElement> StockChange { get => _stockChange; set => _stockChange = value; }
        public IList<IWebElement> StockChangePercent { get => _stockChangePercent; set => _stockChangePercent = value; }
        public IList<IWebElement> StockVolume { get => _stockVolume; set => _stockVolume = value; }
        public IList <IWebElement> StockShares { get => _stockShares; set => _stockShares = value; }
        public IList<IWebElement> StockAvgVol { get => _stockAvgVolume; set => _stockAvgVolume = value; }
        public IList<IWebElement> StockMarketCap { get => _stockMarketCap; set => _stockMarketCap = value; }

        public ScrapeData (string timeScraped, IList<IWebElement> stockSymbol, IList<IWebElement> lastPrice, IList<IWebElement> change,
            IList<IWebElement> changePercent, IList<IWebElement> volume, IList<IWebElement> shares, IList<IWebElement> avgVol, IList<IWebElement> marketCap)
        {
            this.StockTimeScraped = timeScraped;
            this.StockSymbol = stockSymbol;
            this.StockLastPrice = lastPrice;
            this.StockChange = change;
            this.StockChangePercent = changePercent;
            this.StockVolume = volume;
            this.StockShares = shares;
            this.StockAvgVol = avgVol;
            this.StockMarketCap = marketCap;
        }
    }
}
