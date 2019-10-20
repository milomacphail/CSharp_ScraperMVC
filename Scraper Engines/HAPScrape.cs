using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CSharp_Scraper
{
    public class HAPScrape : DatabaseControl
    {
        public void Scrape()
        {
            string nasdaqUrl = "https://www.nasdaq.com/market-activity/stocks";


            HtmlWeb webNav = new HtmlWeb();
            HtmlDocument document = webNav.Load(nasdaqUrl);

            HtmlNodeCollection dataTable = document.DocumentNode.SelectNodes("/html/body/div[4]/div/main/div/article/div[2]/div/section[2]/div/div/div[2]/div[1]/table/tbody");

            HAPStock stock;

            List<HAPStock> stockData = new List<HAPStock>();

            foreach (var tableRow in dataTable)
            {
                DateTime timeScraped = DateTime.Now;
                string stockSymbol = tableRow.SelectSingleNode("td/h3/a").InnerText;
                string lastPrice = tableRow.SelectSingleNode("td[4]").InnerText.Replace("&nbsp;", string.Empty);
                string InitChange = tableRow.SelectSingleNode("td[5]/span").InnerText.Replace("&nbsp;", "").Replace(" ", "").Replace("&#9650;", " ");

                int changeLength = InitChange.Length;

                int cutString = 4;
                string change = InitChange.Substring(0, cutString).Trim();
                string changePercent = InitChange.Substring(cutString).Trim();

                stock = new HAPStock(timeScraped, stockSymbol, lastPrice, change, changePercent);

                stockData.Add(stock);
                InsertScrapeToDatabase(stock);

            }
        }
    }
}
  