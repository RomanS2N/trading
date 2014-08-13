
//
//  Copyright (c) 2007-2008, Corey Goldberg (corey@goldb.org)
//
//  license: GNU LGPL
//
//  This library is free software; you can redistribute it and/or
//  modify it under the terms of the GNU Lesser General Public
//  License as published by the Free Software Foundation; either
//  version 2.1 of the License, or (at your option) any later version.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebReader;

namespace YahooStockQuote {
  public class YSQReader {

    private static string Request(string symbol, string stat) {
      var url = string.Format("http://finance.yahoo.com/d/quotes.csv?s={0}&f={1}", symbol, stat);
      return Reader.ReadString(url);
    }

    public static Dictionary<string, string> GetAll(string symbol) {
      var values = Request(symbol, "l1c1va2xj1b4j4dyekjm3m4rr5p5p6s7").Split(new char[] { ',' });
      Dictionary<string, string> data = new Dictionary<string, string>();
      data["price"] = values[0];
      data["change"] = values[1];
      data["volume"] = values[2];
      data["avg_daily_volume"] = values[3];
      data["stock_exchange"] = values[4];
      data["market_cap"] = values[5];
      data["book_value"] = values[6];
      data["ebitda"] = values[7];
      data["dividend_per_share"] = values[8];
      data["dividend_yield"] = values[9];
      data["earnings_per_share"] = values[10];
      data["52_week_high"] = values[11];
      data["52_week_low"] = values[12];
      data["50day_moving_avg"] = values[13];
      data["200day_moving_avg"] = values[14];
      data["price_earnings_ratio"] = values[15];
      data["price_earnings_growth_ratio"] = values[16];
      data["price_sales_ratio"] = values[17];
      data["price_book_ratio"] = values[18];
      data["short_ratio"] = values[19];
      return data;
    }

    public static string GetPrice(string symbol) {
      return Request(symbol, "l1");
    }

    public static string GetChange(string symbol) {
      return Request(symbol, "c1");
    }

    public static string GetVolume(string symbol) {
      return Request(symbol, "v");
    }

    public static string GetAvgDailyVolume(string symbol) {
      return Request(symbol, "a2");
    }

    public static string GetStockExchange(string symbol) {
      return Request(symbol, "x");
    }

    public static string GetMarketCap(string symbol) {
      return Request(symbol, "j1");
    }

    public static string GetBookValue(string symbol) {
      return Request(symbol, "b4");
    }

    public static string GetEbitda(string symbol) {
      return Request(symbol, "j4");
    }

    public static string GetDividendPerShare(string symbol) {
      return Request(symbol, "d");
    }

    public static string GetDividendYield(string symbol) {
      return Request(symbol, "y");
    }

    public static string GetEarningsPerShare(string symbol) {
      return Request(symbol, "e");
    }

    public static string Get52WeekHigh(string symbol) {
      return Request(symbol, "k");
    }

    public static string Get52WeekLow(string symbol) {
      return Request(symbol, "j");
    }

    public static string Get50DayMovingAvg(string symbol) {
      return Request(symbol, "m3");
    }

    public static string Get200DayMovingAvg(string symbol) {
      return Request(symbol, "m4");
    }

    public static string GetPriceEarningsRatio(string symbol) {
      return Request(symbol, "r");
    }

    public static string GetpriceEarningsGrowthRatio(string symbol) {
      return Request(symbol, "r5");
    }

    public static string GetpriceSalesRatio(string symbol) {
      return Request(symbol, "p5");
    }

    public static string GetPriceBookRatio(string symbol) {
      return Request(symbol, "p6");
    }

    public static string GetShortRatio(string symbol) {
      return Request(symbol, "s7");
    }

    public static List<string> GetHistoricalPrices(string symbol, DateTime start, DateTime end) {
      var url = string.Format("http://ichart.yahoo.com/table.csv?s={0}&", symbol) +
            string.Format("d={0}&", end.Month - 1) +
            string.Format("e={0}&", end.Day) +
            string.Format("f={0}&", end.Year) +
            "g=d&" +
            string.Format("a={0}&", start.Month - 1) +
            string.Format("b={0}&", start.Day) +
            string.Format("c={0}&", start.Year) +
            "ignore=.csv";
      var days = Reader.ReadString(url).Split(new char[] { '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
      return days.ToList();
    }
  }
}
