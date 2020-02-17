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
            string url = "https://www.marketwatch.com/investing/index/comp?mod=us-markets";


            HtmlWeb webNav = new HtmlWeb();
            HtmlDocument document = webNav.Load(url);

            HtmlNodeCollection stockTable = document.DocumentNode.SelectNodes("/html/body/div[1]/div[5]/div[3]/div[1]/div/table/tbody");
            Console.WriteLine(stockTable);

            HAPStock stock;

            List<HAPStock> stockData = new List<HAPStock>();

            foreach (var tableRow in stockTable)
            {
                DateTime timeScraped = DateTime.Now;
                string stockSymbol = tableRow.SelectSingleNode("/html/body/div[1]/div[5]/div[3]/div[1]/div/table/tbody/tr[1]/td[1]").InnerText;
                Console.WriteLine(stockSymbol);
                string lastPrice = tableRow.SelectSingleNode("/html/body/div[1]/div[5]/div[3]/div[1]/div/table/tbody/tr[1]/td[2]").InnerText.Replace("&nbsp;", string.Empty);
                Console.WriteLine(lastPrice);
                string change = tableRow.SelectSingleNode("/html/body/div[1]/div[5]/div[3]/div[1]/div/table/tbody/tr[1]/td[3]").InnerText.Replace("&nbsp;", "").Replace(" ", "").Replace("&#9650;", " ");
                Console.WriteLine(change);
                string changePercent = tableRow.SelectSingleNode("/html/body/div[1]/div[5]/div[3]/div[1]/div/table/tbody/tr[1]/td[4]").InnerText;
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
  