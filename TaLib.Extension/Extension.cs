﻿/*
   Copyright 2014 Samuel Pets (internetuser0x00@gmail.com)

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using FinancialData;
using FinancialData.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacTec.TA.Library;

namespace TaLib.Extension {
  public static class Extension {
    delegate TicTacTec.TA.Library.Core.RetCode TA7(int startIdx, int endIdx, double[] inReal, int optInTimePeriod, out int outBegIdx, out int outNbElement, double[] outReal);
    private static TaResult RunTA7(this double[] series, TA7 handler, int period) {
      int startIdx = 0;
      int endIdx = series.Length - 1;
      double[] inReal = new double[series.Length];
      double[] outReal = new double[series.Length];
      series.Reverse().ToArray().CopyTo(inReal, 0);
      int optInTimePeriod = period;
      int outBegIdx;
      int outNbElement;
      handler(startIdx, endIdx, inReal, optInTimePeriod, out outBegIdx, out outNbElement, outReal);
      return new TaResult(outReal.Reverse().ToArray(), outBegIdx/*, outNbElement*/);
    }

    delegate TicTacTec.TA.Library.Core.RetCode TA12(int startIdx, int endIdx, double[] inReal, int optInTimePeriod, double optInNbDevUp, double optInNbDevDn,
        Core.MAType optInMAType, out int outBegIdx, out int outNBElement, double[] outRealUpperBand, double[] outRealMiddleBand, double[] outRealLowerBand);
    private static Tuple<double[], double[], double[]> RunTA12(this double[] series, TA12 handler, int period, double devUp, double devDn, Core.MAType maType) {
      int startIdx = 0;
      int endIdx = series.Length - 1;
      double[] inReal = new double[series.Length];
      double[] outRealUp = new double[series.Length];
      double[] outRealMid = new double[series.Length];
      double[] outRealLo = new double[series.Length];
      series.CopyTo(inReal, 0);
      int optInTimePeriod = period;
      int outBegIdx;
      int outNbElement;
      handler(startIdx, endIdx, inReal, optInTimePeriod, devUp, devDn, maType, out outBegIdx, out outNbElement, outRealUp, outRealMid, outRealLo);
      return new Tuple<double[], double[], double[]>(outRealUp, outRealMid, outRealLo);
    }

    //private static TaResult LinearRegressionSlope(this double[] series, int period = 0) {
    //  return series.RunTA7(TicTacTec.TA.Library.Core.LinearRegSlope, period);
    //}

    #region SMA

    //private static TaResult SMA(this IEnumerable<double> series, int period) {
    //  return series.ToArray().RunTA7(TicTacTec.TA.Library.Core.Sma, period);
    //}

    public static TaResult SMA(this IEnumerable<IQuote> series, int period) {
      var seriesArray = series.ToArray();
      var result = seriesArray
        .Select(x => (double)x.Ask)
        .ToArray()
        .RunTA7(TicTacTec.TA.Library.Core.Sma, period);
      result.SetDateTimes(seriesArray.Select(x => x.DateTime).ToList());
      return result;
    }

    public static TaResult SMA(this IEnumerable<IBar> series, int period) {
      var seriesArray = series.ToArray();
      var result = seriesArray
        .Select(x => (double)x.Close)
        .ToArray()
        .RunTA7(TicTacTec.TA.Library.Core.Sma, period);
      result.SetDateTimes(seriesArray.Select(x => x.DateTime).ToList());
      return result;
    }

    #endregion

    #region EMA

    //public static TaResult EMA(this IEnumerable<double> series, int period) {
    //  return series.ToArray().RunTA7(TicTacTec.TA.Library.Core.Ema, period);
    //}

    public static TaResult EMA(this IEnumerable<IQuote> series, int period) {
      var seriesArray = series.ToArray();
      var result = seriesArray
        .Select(x => (double)x.Ask)
        .ToArray()
        .RunTA7(TicTacTec.TA.Library.Core.Ema, period);
      result.SetDateTimes(seriesArray.Select(x => x.DateTime).ToList());
      return result;
    }

    public static TaResult EMA(this IEnumerable<IBar> series, int period) {
      var seriesArray = series.ToArray();
      var result = seriesArray
        .Select(x => (double)x.Close)
        .ToArray()
        .RunTA7(TicTacTec.TA.Library.Core.Ema, period);
      result.SetDateTimes(seriesArray.Select(x => x.DateTime).ToList());
      return result;
    }

    #endregion

    #region RSI

    //public static TaResult RSI(this IEnumerable<double> series, int period) {
    //  return series.ToArray().RunTA7(TicTacTec.TA.Library.Core.Rsi, period);
    //}

    public static TaResult RSI(this IEnumerable<IQuote> series, int period) {
      var seriesArray = series.ToArray();
      var result = seriesArray
        .Select(x => (double)x.Ask)
        .ToArray()
        .RunTA7(TicTacTec.TA.Library.Core.Rsi, period);
      result.SetDateTimes(seriesArray.Select(x => x.DateTime).ToList());
      return result;
    }

    public static TaResult RSI(this IEnumerable<IBar> series, int period) {
      var seriesArray = series.ToArray();
      var result = seriesArray
        .Select(x => (double)x.Close)
        .ToArray()
        .RunTA7(TicTacTec.TA.Library.Core.Rsi, period);
      result.SetDateTimes(seriesArray.Select(x => x.DateTime).ToList());
      return result;
    }

    #endregion

    //private static Tuple<double[], double[], double[]> Bbands(this IEnumerable<double> series, int period, double devUp, double devDn, Core.MAType maType) {
    //  return series.ToArray().RunTA12(TicTacTec.TA.Library.Core.Bbands, period, devUp, devDn, maType);
    //}

    //public static double[] AD(this IEnumerable<Bar>)
    /*
    AD                  Chaikin A/D Line
    ADOSC               Chaikin A/D Oscillator
    ADX                 Average Directional Movement Index
    ADXR                Average Directional Movement Index Rating
    APO                 Absolute Price Oscillator
    AROON               Aroon
    AROONOSC            Aroon Oscillator
    ATR                 Average True Range
    AVGPRICE            Average Price
    BBANDS              Bollinger Bands
    BETA                Beta
    BOP                 Balance Of Power
    CCI                 Commodity Channel Index
    CDL2CROWS           Two Crows
    CDL3BLACKCROWS      Three Black Crows
    CDL3INSIDE          Three Inside Up/Down
    CDL3LINESTRIKE      Three-Line Strike 
    CDL3OUTSIDE         Three Outside Up/Down
    CDL3STARSINSOUTH    Three Stars In The South
    CDL3WHITESOLDIERS   Three Advancing White Soldiers
    CDLABANDONEDBABY    Abandoned Baby
    CDLADVANCEBLOCK     Advance Block
    CDLBELTHOLD         Belt-hold
    CDLBREAKAWAY        Breakaway
    CDLCLOSINGMARUBOZU  Closing Marubozu
    CDLCONCEALBABYSWALL Concealing Baby Swallow
    CDLCOUNTERATTACK    Counterattack
    CDLDARKCLOUDCOVER   Dark Cloud Cover
    CDLDOJI             Doji
    CDLDOJISTAR         Doji Star
    CDLDRAGONFLYDOJI    Dragonfly Doji
    CDLENGULFING        Engulfing Pattern
    CDLEVENINGDOJISTAR  Evening Doji Star
    CDLEVENINGSTAR      Evening Star
    CDLGAPSIDESIDEWHITE Up/Down-gap side-by-side white lines
    CDLGRAVESTONEDOJI   Gravestone Doji
    CDLHAMMER           Hammer
    CDLHANGINGMAN       Hanging Man
    CDLHARAMI           Harami Pattern
    CDLHARAMICROSS      Harami Cross Pattern
    CDLHIGHWAVE         High-Wave Candle
    CDLHIKKAKE          Hikkake Pattern
    CDLHIKKAKEMOD       Modified Hikkake Pattern
    CDLHOMINGPIGEON     Homing Pigeon
    CDLIDENTICAL3CROWS  Identical Three Crows
    CDLINNECK           In-Neck Pattern
    CDLINVERTEDHAMMER   Inverted Hammer
    CDLKICKING          Kicking
    CDLKICKINGBYLENGTH  Kicking - bull/bear determined by the longer marubozu
    CDLLADDERBOTTOM     Ladder Bottom
    CDLLONGLEGGEDDOJI   Long Legged Doji
    CDLLONGLINE         Long Line Candle
    CDLMARUBOZU         Marubozu
    CDLMATCHINGLOW      Matching Low
    CDLMATHOLD          Mat Hold
    CDLMORNINGDOJISTAR  Morning Doji Star
    CDLMORNINGSTAR      Morning Star
    CDLONNECK           On-Neck Pattern
    CDLPIERCING         Piercing Pattern
    CDLRICKSHAWMAN      Rickshaw Man
    CDLRISEFALL3METHODS Rising/Falling Three Methods
    CDLSEPARATINGLINES  Separating Lines
    CDLSHOOTINGSTAR     Shooting Star
    CDLSHORTLINE        Short Line Candle
    CDLSPINNINGTOP      Spinning Top
    CDLSTALLEDPATTERN   Stalled Pattern
    CDLSTICKSANDWICH    Stick Sandwich
    CDLTAKURI           Takuri (Dragonfly Doji with very long lower shadow)
    CDLTASUKIGAP        Tasuki Gap
    CDLTHRUSTING        Thrusting Pattern
    CDLTRISTAR          Tristar Pattern
    CDLUNIQUE3RIVER     Unique 3 River
    CDLUPSIDEGAP2CROWS  Upside Gap Two Crows
    CDLXSIDEGAP3METHODS Upside/Downside Gap Three Methods
    CMO                 Chande Momentum Oscillator
    CORREL              Pearson's Correlation Coefficient (r)
    DEMA                Double Exponential Moving Average
    DX                  Directional Movement Index
    EMA                 Exponential Moving Average
    HT_DCPERIOD         Hilbert Transform - Dominant Cycle Period
    HT_DCPHASE          Hilbert Transform - Dominant Cycle Phase
    HT_PHASOR           Hilbert Transform - Phasor Components
    HT_SINE             Hilbert Transform - SineWave
    HT_TRENDLINE        Hilbert Transform - Instantaneous Trendline
    HT_TRENDMODE        Hilbert Transform - Trend vs Cycle Mode
    KAMA                Kaufman Adaptive Moving Average
    LINEARREG           Linear Regression
    LINEARREG_ANGLE     Linear Regression Angle
    LINEARREG_INTERCEPT Linear Regression Intercept
    LINEARREG_SLOPE     Linear Regression Slope
    MA                  All Moving Average
    MACD                Moving Average Convergence/Divergence
    MACDEXT             MACD with controllable MA type
    MACDFIX             Moving Average Convergence/Divergence Fix 12/26
    MAMA                MESA Adaptive Moving Average
    MAX                 Highest value over a specified period
    MAXINDEX            Index of highest value over a specified period
    MEDPRICE            Median Price
    MFI                 Money Flow Index
    MIDPOINT            MidPoint over period
    MIDPRICE            Midpoint Price over period
    MIN                 Lowest value over a specified period
    MININDEX            Index of lowest value over a specified period
    MINMAX              Lowest and highest values over a specified period
    MINMAXINDEX         Indexes of lowest and highest values over a specified period
    MINUS_DI            Minus Directional Indicator
    MINUS_DM            Minus Directional Movement
    MOM                 Momentum
    NATR                Normalized Average True Range
    OBV                 On Balance Volume
    PLUS_DI             Plus Directional Indicator
    PLUS_DM             Plus Directional Movement
    PPO                 Percentage Price Oscillator
    ROC                 Rate of change : ((price/prevPrice)-1)*100
    ROCP                Rate of change Percentage: (price-prevPrice)/prevPrice
    ROCR                Rate of change ratio: (price/prevPrice)
    ROCR100             Rate of change ratio 100 scale: (price/prevPrice)*100
    RSI                 Relative Strength Index
    SAR                 Parabolic SAR
    SAREXT              Parabolic SAR - Extended
    SMA                 Simple Moving Average
    STDDEV              Standard Deviation
    STOCH               Stochastic
    STOCHF              Stochastic Fast
    STOCHRSI            Stochastic Relative Strength Index
    SUM                 Summation
    T3                  Triple Exponential Moving Average (T3)
    TEMA                Triple Exponential Moving Average
    TRANGE              True Range
    TRIMA               Triangular Moving Average
    TRIX                1-day Rate-Of-Change (ROC) of a Triple Smooth EMA
    TSF                 Time Series Forecast
    TYPPRICE            Typical Price
    ULTOSC              Ultimate Oscillator
    VAR                 Variance
    WCLPRICE            Weighted Close Price
    WILLR               Williams' %R
    WMA                 Weighted Moving Average
    */
  }
}
