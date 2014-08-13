using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialIndicator.Shared {
	public interface IFinancialIndicator {
		void Update(decimal price);
		decimal Value { get; }
		bool HasValue { get; }
	}
}
