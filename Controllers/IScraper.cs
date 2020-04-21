using OpenQA.Selenium;
using System.Collections.Generic;

namespace MultiUserMVC.Controllers
{
    public interface IScraper
    {
        System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> GetYahooWatchlistData();
        List<string> ParseData();
        void saveStocks();
    }
}