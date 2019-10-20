using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CSharp_Scraper
{
    class Database
    {
        private const string _connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=StockTable;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void InsertScrapeToDatabase(Stock stock)
        {
            DataToTable(stock);
        }

        public static void LatestScrapeToDatabase(Stock stock)
        {
            LastScrapeToDatabase(stock);
        }

        public static void Clear_Reset()
        {
            DeleteTableData();
            ResetAutoIncrementer();
        }

        public static void DeleteTableData()
        {
            string reseed = "DBCC CHECKIDENT ('StockHistory', RESEED, 0);";

            using (SqlConnection con = new SqlConnection(_connection))
            {
                con.Open();

                if (con.State == System.Data.ConnectionState.Open)
                {
                    using (SqlCommand cmd = new SqlCommand(reseed, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                con.Close();
            }
        }
        public static void ResetAutoIncrementer()
        {
            string reseed = "DBCC CHECKIDENT ('StockHistory', RESEED, 0);";
        }
    

            private static void LastScrapeToDatabase(Stock stock)
            {
                string lastScrape = @"IF EXISTS(SELECT* FROM dbo.LastTable WHERE Stock_Symbol = @stock_symbol)
                                    UPDATE dbo.LastTable
                                    SET Time_Scraped = @time_scraped, Last_Price = @last_price, Change = @Change, Change_Percent = @change_percent,
                                    Volume = @volume, Shares = @shares, Average_Volume = @average_volume, Market_Cap = @market_cap
                                    WHERE Stock_Symbol = @stock_symbol
                                ELSE 
                                    INSERT INTO dbo.LastTable VALUES (@time_scraped, @stock_symbol, @last_price, @change, @change_percent, @volume, @shares, @average_volume, @market_cap);";

                using (SqlConnection db = new SqlConnection(_connection))
                {

                    db.Open();

                    Console.WriteLine("Database has been opened.");

                    if (db.State == System.Data.ConnectionState.Open)
                    {

                        using (SqlCommand command = new SqlCommand(lastScrape, db))
                        {
                            command.Parameters.AddWithValue("@time_scraped", stock.TimeScraped);
                            command.Parameters.AddWithValue("@stock_symbol", stock.StockSymbol);
                            command.Parameters.AddWithValue("@last_price", stock.LastPrice);
                            command.Parameters.AddWithValue("@change", stock.Change);
                            command.Parameters.AddWithValue("@change_percent", stock.ChangePercent);
                            command.Parameters.AddWithValue("@volume", stock.Volume);
                            command.Parameters.AddWithValue("@shares", stock.Shares);
                            command.Parameters.AddWithValue("@average_volume", stock.AvgVol);
                            command.Parameters.AddWithValue("@market_cap", stock.MarketCap);

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No database found. Please check database connection.");
                    }

                    db.Close();
                }
            }
            private static void DataToTable(Stock stock)
            {

                using (SqlConnection db = new SqlConnection(_connection))
                {
                    string insertToTable = "INSERT INTO dbo.StockTable (Time_Scraped, Stock_Symbol, Last_Price, Change, Change_Percent, Volume, Shares, Average_Volume, Market_Cap) VALUES (@time_scraped, @stock_symbol, @last_price, @change, @change_percent, @volume, @shares, @average_volume, @market_cap);";
                    {
                        db.Open();

                        Console.WriteLine("Database has been opened");

                        if (db.State == System.Data.ConnectionState.Open)
                        {

                            using (SqlCommand dataToTable = new SqlCommand(insertToTable, db))
                            {

                                dataToTable.Parameters.AddWithValue("@time_scraped", stock.TimeScraped);
                                dataToTable.Parameters.AddWithValue("@stock_symbol", stock.StockSymbol);
                                dataToTable.Parameters.AddWithValue("@last_price", stock.LastPrice);
                                dataToTable.Parameters.AddWithValue("@change", stock.Change);
                                dataToTable.Parameters.AddWithValue("@change_percent", stock.ChangePercent);
                                dataToTable.Parameters.AddWithValue("@volume", stock.Volume);
                                dataToTable.Parameters.AddWithValue("@shares", stock.Shares);
                                dataToTable.Parameters.AddWithValue("@average_volume", stock.AvgVol);
                                dataToTable.Parameters.AddWithValue("@market_cap", stock.MarketCap);

                                dataToTable.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No database found. Please check database connection.");
                        }

                        db.Close();

                    }
                }

            }
        }
    }
