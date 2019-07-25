using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace CSharp_Scraper
{
    public class Stock
    {
        private DateTime _timeScraped;
        private string _stockSymbol;
        private string _lastPrice;
        private string _change;
        private string _changePercent;
        private string _volume;
        private string _shares;
        private string _avgVol;
        private string _marketCap;
        private DateTime scrapeTime;
        private IList<IWebElement> stockSymbol_header;
        private IList<IWebElement> lastPrice_header;
        private IList<IWebElement> change_header;
        private IList<IWebElement> changePercent_header;
        private IList<IWebElement> volume_header;
        private IList<IWebElement> shares_header;
        private IList<IWebElement> averageVolume_header;
        private IList<IWebElement> marketCap_header;

        public DateTime TimeScraped { get => _timeScraped; set => _timeScraped = value; }
        public string StockSymbol { get => _stockSymbol; set => _stockSymbol = value; }
        public string LastPrice { get => _lastPrice  ; set => _lastPrice = value; }
        public string Change { get => _change; set => _change = value; }
        public string ChangePercent { get => _changePercent; set => _changePercent = value; }
        public string Volume { get => _volume; set => _volume = value; }
        public string Shares { get => _shares; set => _shares = value; } 
        public string AvgVol { get => _avgVol; set => _avgVol = value; }
        public string MarketCap { get => _marketCap; set => _marketCap = value; }
        

        public Stock()
        {

        }

        public Stock(DateTime timeScraped, string stockSymbol, string lastPrice, string change,
            string changePercent, string volume, string shares, string avgVol, string marketCap)
        {
            this.TimeScraped = timeScraped;
            this.StockSymbol = stockSymbol;
            this.LastPrice = lastPrice;
            this.Change = change;
            this.ChangePercent = changePercent;
            this.Volume = volume;
            this.Shares = shares;
            this.AvgVol = avgVol;
            this.MarketCap = marketCap;
        }

        public Stock(DateTime scrapeTime, IList<IWebElement> stockSymbol_header, IList<IWebElement> lastPrice_header, IList<IWebElement> change_header, IList<IWebElement> changePercent_header, IList<IWebElement> volume_header, IList<IWebElement> shares_header, IList<IWebElement> averageVolume_header, IList<IWebElement> marketCap_header)
        {
            this.scrapeTime = scrapeTime;
            this.stockSymbol_header = stockSymbol_header;
            this.lastPrice_header = lastPrice_header;
            this.change_header = change_header;
            this.changePercent_header = changePercent_header;
            this.volume_header = volume_header;
            this.shares_header = shares_header;
            this.averageVolume_header = averageVolume_header;
            this.marketCap_header = marketCap_header;
        }
    }
}

