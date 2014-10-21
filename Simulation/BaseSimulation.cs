using FinancialData.Shared;
using Simulation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation {
  public abstract class BaseSimulation : ISimulation {
    private List<IPosition> _positions;
    private List<IPosition> _closedPositions;
    private IBar _lastBar;
    public BaseSimulation() {
      _positions = new List<IPosition>();
      _closedPositions = new List<IPosition>();
    }
    public List<IPosition> Positions {
      get { return _positions.ToList(); }
    }
    public List<IPosition> LongPositions {
      get { return _positions.Where(x => x.Side == PositionSide.Long).ToList(); }
    }
    public List<IPosition> ShortPositions {
      get { return _positions.Where(x => x.Side == PositionSide.Short).ToList(); }
    }
    public void CreatePosition(PositionSide positionSide, decimal price, int size) {
      _positions.Add(new Position(positionSide, price, size));
    }
    public void ClosePosition(IPosition position) {
      _positions.Remove(position);
      _closedPositions.Add(position);
      position.Close(_lastBar.Close);
    }
    public void OnBar(IBarContext context) {
      _lastBar = context.Bar;
      OnBar_UserCode(context);
    }
    public abstract void OnBar_UserCode(IBarContext context);
    public string GetReport() {
      StringBuilder stringBuilder = new StringBuilder();

      var longClosedPositions = _closedPositions.Where(x => x.Side == PositionSide.Long).ToList();
      var shortClosedPositions = _closedPositions.Where(x => x.Side == PositionSide.Short).ToList();

      stringBuilder.AppendLine("--------------------------------------------------------------------------------");
      stringBuilder.AppendLine(string.Format("Trades count: {0}", _closedPositions.Count));
      stringBuilder.AppendLine(string.Format("Long trades count: {0}", longClosedPositions.Count));
      stringBuilder.AppendLine(string.Format("Long trades balance: {0}", longClosedPositions.Sum(x => x.Earnings)));
      stringBuilder.AppendLine(string.Format("Short trades count: {0}", shortClosedPositions.Count));
      stringBuilder.AppendLine(string.Format("Short trades balance: {0}", shortClosedPositions.Sum(x => x.Earnings)));

      return stringBuilder.ToString();
    }
    public decimal Earnings {
      get {
        return _closedPositions.Sum(x => x.Earnings);
      }
    }
  }
}
