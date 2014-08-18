using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialData.Manager.Shared;
using FinancialData.Shared;
using TradingConfiguration.Shared;

namespace FinancialData.Manager {
	public class FinancialDataManager : IFinancialDataStore {

		public Dictionary<Asset, List<DataSource>> AssetDataSources { get; set; }

		public FinancialDataManager() {
			AssetDataSources = new Dictionary<Asset, List<DataSource>>();
		}

		public List<Asset> GetAssets() {
			return AssetDataSources.Keys.ToList();
		}

		public List<Asset> GetAssets(DataSource source) {
			return AssetDataSources
				.Where(pair => pair.Value.Contains(source))
				.Select(pair => pair.Key)
				.ToList();
		}

		public List<DataSource> GetDataSources() {
			return AssetDataSources.SelectMany(pair => pair.Value).Distinct().ToList();
		}

		public List<DataSource> GetDataSources(Asset asset) {
			return AssetDataSources[asset];
		}

		public void AddQuote(IQuote quote) {
			if (!AssetDataSources.ContainsKey(quote.Asset))
				AssetDataSources[quote.Asset] = new List<DataSource>();
			if (!AssetDataSources[quote.Asset].Contains(quote.Source))
				AssetDataSources[quote.Asset].Add(quote.Source);
			StoreQuote(quote);
		}

		private void StoreQuote(IQuote quote) {
			throw new NotImplementedException();
		}

		public void ClearQuotes() {
			throw new NotImplementedException();
		}

		public void ClearQuotes(Asset asset) {
			throw new NotImplementedException();
		}

		public void ClearQuotes(Asset asset, DataSource source) {
			throw new NotImplementedException();
		}

		public void ClearQuotes(DataSource source) {
			throw new NotImplementedException();
		}

		public int GetQuotesCount() {
			throw new NotImplementedException();
		}

		public int GetQuotesCount(Asset asset) {
			throw new NotImplementedException();
		}

		public int GetQuotesCount(Asset asset, DataSource source) {
			throw new NotImplementedException();
		}

		public int GetQuotesCount(DataSource source) {
			throw new NotImplementedException();
		}
	}
}
