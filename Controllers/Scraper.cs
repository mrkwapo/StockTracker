using System;
using System.Collections.Generic;
using MultiUserMVC.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Data.SqlClient;
using System.Threading;
using OpenQA.Selenium.Chrome;
namespace MultiUserMVC.Controllers
{
    public class Scraper : IScraper
    {

        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> GetYahooWatchlistData()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArgument("--headless");
            IWebDriver driver = new ChromeDriver(option);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Navigate().GoToUrl(@"https://login.yahoo.com/config/login?.intl=us&.lang=en-US&.src=finance&.done=https%3A%2F%2Ffinance.yahoo.com%2F");

            var userNameInputBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login-username")));

            userNameInputBox.SendKeys("mrkwapo@yahoo.com");

            var usernameNextButton = driver.FindElement(By.Id("login-signin"));

            usernameNextButton.SendKeys(Keys.Enter);

            var passwordInputBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("login-passwd")));

            passwordInputBox.SendKeys("Careerdevs1!");

            var passwordNextButton = driver.FindElement(By.Id("login-signin"));
            passwordNextButton.SendKeys(Keys.Enter);

            var watchlistLink = wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText("My Watchlist")));

            watchlistLink.SendKeys(Keys.Enter);

            var stocksTable = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("pf-detail-table")));

            System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> data = stocksTable.FindElements(By.TagName("td"));
            return data;
        }




        //This method scrapes, parses, adds, and returns the data in a List  
        public List<string> ParseData()
        {
            var data = GetYahooWatchlistData();

            Thread.Sleep(10000);

            List<string> parsedDataList = new List<string>();
            foreach (var item in data)
            {

                if ((!String.IsNullOrWhiteSpace(item.Text)) && item.Text != "Trade" && item.Text != "-")

                {
                    parsedDataList.Add(item.Text);
                }

            }
            return parsedDataList;
        }


        /*This method uses the parsed data
         * instantiates a new stock object, 
         * iterates through the parsed stock data, 
         * assings the data to corresponding properties of the new stock object 
         * and saves each stock object to the SQL database */
        public void saveStocks()
        {
            List<string> parsedData = ParseData();
            var stock = new Stock();

            int count = parsedData.Count;

            for (var i = 0; i <= count; i++)
            {
                stock.Symbol = parsedData[i];
                Console.WriteLine(stock.Symbol + " : Symbol");
                i++;
                stock.Last_Price = parsedData[i];
                Console.WriteLine(stock.Last_Price + " : Last_Price");
                i++;
                stock.Change = parsedData[i];
                Console.WriteLine(stock.Change + " : Change");
                i++;
                stock.Change_Percentage = parsedData[i];
                Console.WriteLine(stock.Change_Percentage + " : Change_Percentage");
                i++;
                stock.Currency = parsedData[i];
                Console.WriteLine(stock.Currency + " : Currency");
                i++;
                stock.Market_Time = parsedData[i];
                Console.WriteLine(stock.Market_Time + " : Market_Time");
                i++;
                stock.Volume = parsedData[i];
                Console.WriteLine(stock.Volume + " :Volume");
                i++;
                stock.Average_Volume = parsedData[i];
                Console.WriteLine(stock.Average_Volume + " : Average_Volume");
                i++;
                stock.Market_Cap = parsedData[i];
                Console.WriteLine(stock.Market_Cap + " : Market_Cap");
                i++;

                //Establishing a connection and inserting the data of each stock to the SQL database accordingly
                SqlConnection conn = new SqlConnection();

                conn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MultiUserMVCDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

                conn.Open();

                string query = "INSERT INTO [Stocks] (Symbol, Last_Price, Change, Change_Percentage, Currency, Market_Time, Volume, Average_Volume, Market_Cap, ScrapeDate) VALUES (@Symbol, @Last_Price, @Change, @Change_Percentage,@Currency, @Market_Time, @Volume, @Average_Volume, @Market_Cap, @ScrapeDate)";

                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@Symbol", stock.Symbol);
                command.Parameters.AddWithValue("@Last_Price", stock.Last_Price);
                command.Parameters.AddWithValue("@Change", stock.Change);
                command.Parameters.AddWithValue("@Change_Percentage", stock.Change_Percentage);
                command.Parameters.AddWithValue("@Currency", stock.Currency);
                command.Parameters.AddWithValue("@Market_Time", stock.Market_Time);
                command.Parameters.AddWithValue("@Volume", stock.Volume);
                command.Parameters.AddWithValue("@Average_Volume", stock.Average_Volume);
                command.Parameters.AddWithValue("@Market_Cap", stock.Market_Cap);
                command.Parameters.AddWithValue("@ScrapeDate", DateTime.Now.ToString());

                command.ExecuteNonQuery();
                conn.Close();

                if (i != count)
                    i--;
            }

        }
    }
}