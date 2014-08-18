using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialData.Shared;
using TradingConfiguration.Shared;

namespace FinancialData.Manager.Shared {
	public interface IFinancialDataStore {

		List<Asset> GetAssets();
		List<Asset> GetAssets(DataSource source);
		List<DataSource> GetDataSources();
		List<DataSource> GetDataSources(Asset asset);

		void AddQuote(IQuote quote);

		void ClearQuotes();
		void ClearQuotes(Asset asset);
		void ClearQuotes(Asset asset, DataSource source);
		void ClearQuotes(DataSource source);

		int GetQuotesCount();
		int GetQuotesCount(Asset asset);
		int GetQuotesCount(Asset asset, DataSource source);
		int GetQuotesCount(DataSource source);
	}
}
