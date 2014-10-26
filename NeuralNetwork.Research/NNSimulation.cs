using FinancialData.Shared;
using Simulation;
using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork.Research {
  enum Trend {
    Up, Down, Undefined
  }
  enum Operation {
    Stay = 0,
    GoLong = 1,
    GoShort = 2
  }
  class NNSimulation : BaseSimulation {
    private Trend _trend = Trend.Undefined;
    private bool _longEnabled = true;
    private bool _shortEnabled = true;
    public NeuralNetwork NeuralNetwork { get; private set; }
    public decimal IntensityThreshold { get; private set; }
    public NNSimulation(NeuralNetwork neuralNetwork, decimal intensityThreshold, decimal? takeProfitPoints, decimal? stopLossPoints, bool isTrailingStop = false)
      : base(takeProfitPoints, stopLossPoints, isTrailingStop) {
      NeuralNetwork = neuralNetwork;
      IntensityThreshold = intensityThreshold;
    }
    public override void OnBar_UserCode(Simulation.Shared.IBarContext context) {
      IBar bar = context.Bar;
      var seriesValues = context.SeriesValues;
      IInstantValue<double> indicator0 = seriesValues["INDICATOR_0"];
      IInstantValue<double> indicator1 = seriesValues["INDICATOR_1"];
      IInstantValue<double> indicator2 = seriesValues["INDICATOR_2"];
      IInstantValue<double> indicator3 = seriesValues["INDICATOR_3"];
      var values = new double[] { 
        indicator0.Value, 
        indicator1.Value, 
        indicator2.Value, 
        indicator3.Value
      };
      var valuesArray = new List<double[]> { values }.ToArray();
      Trainer.Normalize(valuesArray, new int[] { 0, 1, 2, 3 });
      double[] output = NeuralNetwork.ComputeOutputs(valuesArray[0]);
      Console.WriteLine("{0}, {1}, {2}, {3} => {4}, {5}, {6}",
        indicator0.Value,
        indicator1.Value,
        indicator2.Value,
        indicator3.Value,
        output[0], output[1], output[2]);
      int maxIndex = NeuralNetwork.MaxIndex(output);
      Operation operation = (Operation)maxIndex;
      decimal intensity = (decimal)output[maxIndex];
      TryToOpenLongPositions(bar, operation, intensity);
      TryToOpenShortPositions(bar, operation, intensity);
      ManageSideways(bar, operation, intensity);
    }

    private void TryToOpenLongPositions(IBar bar, Operation operation, decimal intensity) {
      // up trend detected
      if (operation == Operation.GoLong && intensity > IntensityThreshold) {
        // is trend change
        if (_trend != Trend.Up) {
          _trend = Trend.Up;
          if (_longEnabled && LongPositions.Count == 0) {
            CreatePosition(PositionSide.Long, bar.DateTime, bar.Close, 1);
          }
        }
      }
    }
    private void TryToOpenShortPositions(IBar bar, Operation operation, decimal intensity) {
      // down trend detected
      if (operation == Operation.GoShort && intensity > IntensityThreshold) {
        // is trend change
        if (_trend != Trend.Down) {
          _trend = Trend.Down;
          if (_shortEnabled && ShortPositions.Count == 0) {
            CreatePosition(PositionSide.Short, bar.DateTime, bar.Close, 1);
          }
        }
      }
    }
    private void ManageSideways(IBar bar, Operation operation, decimal intensity) {
      // sideways trend detection
      if (operation == Operation.Stay) {
        // is trend change
        if (_trend != Trend.Undefined) {
          _trend = Trend.Undefined;
          // do nothing
        }
      }
    }
  }
}
