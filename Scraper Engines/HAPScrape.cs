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
            string wsjUrl = "https://www.wsj.com/market-data/stocks";


            HtmlWeb webNav = new HtmlWeb();
            HtmlDocument document = webNav.Load(wsjUrl);

            HtmlNodeCollection dataTable = document.DocumentNode.SelectNodes("//*[@id='root']/div/div/div/div[2]/div[4]/div/div[1]/div[3]/div/div[1]/div/table/tbody");
            Console.WriteLine(dataTable);

            HAPStock stock;

            List<HAPStock> stockData = new List<HAPStock>();

            foreach (var tableRow in dataTable)
            {
                DateTime timeScraped = DateTime.Now;
                string stockSymbol = tableRow.SelectSingleNode("/html/body/div[1]/div/div/div/div[2]/div[4]/div/div[1]/div[3]/div/div[1]/div/table/tbody/tr[1]/td[1]").InnerText;
                Console.WriteLine(stockSymbol);
                string lastPrice = tableRow.SelectSingleNode("//*[@id='root']/div/div/div/div[2]/div[4]/div/div[1]/div[3]/div/div[1]/div/table/tbody/tr[1]/td[2]").InnerText.Replace("&nbsp;", string.Empty);
                Console.WriteLine(lastPrice);
                string change = tableRow.SelectSingleNode("/html/body/div[1]/div/div/div/div[2]/div[4]/div/div[1]/div[3]/div/div[1]/div/table/tbody/tr[1]/td[3]").InnerText.Replace("&nbsp;", "").Replace(" ", "").Replace("&#9650;", " ");
                Console.WriteLine(change);
                string changePercent = tableRow.SelectSingleNode("/html/body/div[1]/div/div/div/div[2]/div[4]/div/div[1]/div[3]/div/div[1]/div/table/tbody/tr[1]/td[4]").InnerText;
                Console.WriteLine(changePercent);

                /*int changeLength = InitChange.Length;

                int cutString = 4;
                string change = InitChange.Substring(0, cutString).Trim();
                string changePercent = InitChange.Substring(cutString).Trim();*/

                stock = new HAPStock(timeScraped, stockSymbol, lastPrice, change, changePercent);

                stockData.Add(stock);
                InsertScrapeToDatabase(stock);

            }
        }
    }
}
  