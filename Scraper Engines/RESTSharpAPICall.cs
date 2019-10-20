using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CSharp_Scraper
{
    public class ApiCall
    {
        public void RestScrape()
        {
            var client = new RestClient("https://morning-star.p.rapidapi.com/market/get-summary");

            RestRequest request = new RestRequest(Method.GET);
            request.AddHeader("X-RapidAPI-Host", "morning-star.p.rapidapi.com");
            request.AddHeader("X-RapidAPI-Key", "766253babbmsh7dc7313fc6fb941p1f3b2fjsn3e5e1b4f90ba");
            request.AddHeader("content-type", "application/json");

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            dynamic JsonObject = JsonConvert.DeserializeObject(content);

            JArray USAStocks = JsonObject["MarketRegions"]["USA"];

            string connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RestSharpTable;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            using (SqlConnection dbConnection = new SqlConnection(connection))
            {
                dbConnection.Open();
                Console.WriteLine("Database Open");

                foreach (JToken stock in USAStocks)
                {
                    SqlCommand insertCommand = new SqlCommand("INSERT into dbo.RestSharpTable (TimeScraped, Symbol, LastPrice, Change, ChangePercent) VALUES (@time_scraped, @symbol, @last_price, @change, @change_percent)", dbConnection);

                    insertCommand.Parameters.AddWithValue("@time_scraped", DateTime.Now);
                    insertCommand.Parameters.AddWithValue("@symbol", stock["RegionAndTicker"].ToString());
                    insertCommand.Parameters.AddWithValue("@last_price", stock["Price"].ToString());
                    insertCommand.Parameters.AddWithValue("@change", stock["PriceChange"].ToString());
                    insertCommand.Parameters.AddWithValue("@change_percent", stock["PercentChange"].ToString());

                    insertCommand.ExecuteNonQuery();
                }

                Console.WriteLine("Database Updated");
                dbConnection.Close();
            }
        }
    }

    class CallData
    {
        private List<string> stocks;
        private List<RSStock> stockList;
        public List<string> Stocks { get => stocks; set => stocks = value; }
        internal List<RSStock> StockList { get => stockList; set => stockList = value; }

        public CallData()
        {
            this.Stocks = new List<string>();
            this.StockList = new List<RSStock>();
        }
    }
}