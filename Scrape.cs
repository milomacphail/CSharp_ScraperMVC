using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Scraper
{
    class Scrape : Database
        {
            private string _id;
    private string _pw;
    public ChromeDriver driver;

    public Scrape(string id, string pw)
    {
        this._id = id;
        this._pw = pw;
        this.driver = new ChromeDriver();
    }


    public void Navigate()

    {
        //driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolios");
        driver.Navigate().GoToUrl("https://finance.yahoo.com/portfolio/p_0/view/v1");
    }

    public void Login()
    {
        driver.Navigate().GoToUrl("https://login.yahoo.com");
        driver.FindElement(By.Id("login-username")).SendKeys(this._id + Keys.Enter);
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        driver.FindElement(By.Id("login-passwd")).SendKeys(this._pw + Keys.Enter);
    }

        public void ScrapeTable()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            IWebElement table = driver.FindElement(By.XPath("//*[@id=\"pf-detail-table\"]/div[1]/table"));
            IList<IWebElement> table_rows = table.FindElements(By.TagName("tr"));

            int count = table_rows.Count;

            Console.WriteLine(count);

            string scrapeTime = Convert.ToString(DateTime.Now);
            IList<IWebElement> stockSymbol_header = driver.FindElements(By.XPath("//*[@aria-label='Symbol']"));
            IList <IWebElement> lastPrice_header = driver.FindElements(By.XPath("//*[@aria-label='Last Price']"));
            IList <IWebElement> change_header = driver.FindElements(By.XPath("//*[@aria-label='Change']"));
            IList <IWebElement> changePercent_header = driver.FindElements(By.XPath("//*[@aria-label='Chg %']"));
            IList <IWebElement> volume_header = driver.FindElements(By.XPath("//*[@aria-label='Volume']"));
            IList<IWebElement> shares_header = driver.FindElements(By.XPath("//*[@aria-label='Shares']"));
            IList <IWebElement> averageVolume_header = driver.FindElements(By.XPath("//*[@aria-label='Avg Vol (3m)']"));
            IList<IWebElement> marketCap_header = driver.FindElements(By.XPath("//*[@aria-label='Market Cap']"));

            
            /*var symbol = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + index + "]/td[1]/span/a")).GetAttribute("innerText");
            var price = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + index + "]/td[2]/span")).GetAttribute("innerText");
            var change = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + index + "]/td[3]/span")).GetAttribute("innerText");
            var pchange = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + index + "]/td[4]/span")).GetAttribute("innerText");
            var volume = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + index + "]/td[7]/span")).GetAttribute("innerText");
            var marketcap = driver.FindElement(By.XPath("//*[@id=\"main\"]/section/section[2]/div[2]/table/tbody/tr[" + index + "]/td[13]/span")).GetAttribute("innerText");*/

            ScrapeData scrapeInstance = new ScrapeData(scrapeTime, stockSymbol_header, lastPrice_header, change_header, changePercent_header,
                volume_header, shares_header, averageVolume_header, marketCap_header);

            readStocks(scrapeInstance);
            driver.Quit();
        }

            private static void readStocks(ScrapeData stockData)
            {
                int totalStocks = stockData.StockSymbol.Count;
                Console.WriteLine("Total stocks: {0}", totalStocks);

                List<DateTime> timeScraped = new List<DateTime>();
                List<string> stockSymbols = new List<string>();
                List<string> lastPrices = new List<string>();
                List<string> changes = new List<string>();
                List<string> changePercents = new List<string>();
                List<string> volumes = new List<string>();
                List<string> shares = new List<string>();
                List<string> avgVolumes = new List<string>();
                List<string> mktCaps = new List<string>();

                Stock stock = new Stock();

                for (int index = 0; index < totalStocks; index++)
                {
                    timeScraped.Insert(index, DateTime.Now);
                    stockSymbols.Insert(index, Convert.ToString(stockData.StockSymbol[index].Text));
                    lastPrices.Insert(index, Convert.ToString(stockData.StockLastPrice[index].Text));
                    changes.Insert(index, Convert.ToString(stockData.StockChange[index].Text));
                    changePercents.Insert(index, Convert.ToString(stockData.StockChangePercent[index].Text));
                    volumes.Insert(index, Convert.ToString(stockData.StockVolume[index].Text));
                    shares.Insert(index, Convert.ToString(stockData.StockShares[index].Text));
                    avgVolumes.Insert(index, Convert.ToString(stockData.StockAvgVol[index].Text));
                    mktCaps.Insert(index, Convert.ToString(stockData.StockMarketCap[index].Text));



                stock = new Stock(
                                   timeScraped[index],
                                   stockSymbols[index],
                                   lastPrices[index],
                                   changes[index],
                                   changePercents[index],
                                   volumes[index],
                                   shares[index],
                                   avgVolumes[index],
                                   mktCaps[index]);


                    Console.WriteLine("{0} added to database.", stockSymbols[index]);

                InsertScrapeToDatabase(stock);
                LatestScrapeToDatabase(stock);
                
                }
            }

        }
    }
