using System;
using System.Collections.Generic;
//Handles the news response from Google's News API
public class NewsResponse
{
    public string Status { get; set; }
    public int TotalResults { get; set; }
    public List<Article> Articles { get; set; }
}