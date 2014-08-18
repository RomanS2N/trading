/*
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
