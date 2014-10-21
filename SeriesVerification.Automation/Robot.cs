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
using System.IO;
using FinancialConfiguration;
using System.Threading;
using SeriesReading.Descriptor.Quotes;
using System.Threading.Tasks;
using SeriesAcquisition.Shared;

namespace SeriesVerification.Automation {
	public class Robot {

		private List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

		private FileSystemWatcher CreateWatcher(string path) {
			Console.WriteLine("Creating watcher for {0}", path);
			FileSystemWatcher watcher = new FileSystemWatcher();
			// esto no funciona
			//_watcher.IncludeSubdirectories = true;
			watcher.Path = path;
			watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
			watcher.Filter = "*.csv";
			watcher.Created += new FileSystemEventHandler(watcher_Created);
			watcher.Deleted += new FileSystemEventHandler(watcher_Deleted);
			watcher.Renamed += new RenamedEventHandler(watcher_Renamed);
			watcher.EnableRaisingEvents = true;
			return watcher;
		}

		private void VerifyOldFiles(SeriesDescriptor seriesDescriptor) {
			seriesDescriptor.InstrumentDescriptors.ForEach(instrumentDescriptor => {
				instrumentDescriptor.ProviderDescriptors.ForEach(providerDescriptor => {
					// si no tiene VerificationReport, lo verifico
					Directory.GetFiles(providerDescriptor.Path, "*.csv").ToList().ForEach(csvFileName => {
						string reportFileName = csvFileName.Replace(".csv", ".xml");
						VerificationReport report = VerificationReport.LoadFromFile(reportFileName);
						if (report == null || !report.Verified) {
							Verify(csvFileName);
						}
					});
				});
			});
		}

		private void SubscribeWatchers(SeriesDescriptor seriesDescriptor) {
			// un watcher por cada carpeta de proveedor de cada instrumento (porque no me anda el watcher con subdirectorios)
			seriesDescriptor.InstrumentDescriptors.ForEach(instrumentDescriptor => {
				instrumentDescriptor.ProviderDescriptors.ForEach(providerDescriptor => {
					// me quedo monitoreando el directorio
					_watchers.Add(CreateWatcher(providerDescriptor.Path));
				});
			});
		}

		public void Start() {
			Task.Factory.StartNew(() => {
				while (true) {
					VerifyOldFiles(new SeriesDescriptor());
					Thread.Sleep(1000);
				}
			});
			//SubscribeWatchers(seriesDescriptor);
		}

		#region Events

		void watcher_Renamed(object sender, RenamedEventArgs e) {
			Console.WriteLine("{0}: {1} to {2}", e.ChangeType, e.OldName, e.Name);
			if (e.Name.EndsWith(".csv")) {
				Verify(e.FullPath);
			}
		}

		void watcher_Deleted(object sender, FileSystemEventArgs e) {
			Console.WriteLine("{0}: {1}", e.ChangeType, e.Name);
			DeleteVerificationReport(e.FullPath);
		}

		void watcher_Created(object sender, FileSystemEventArgs e) {
			Console.WriteLine("{0}: {1}", e.ChangeType, e.Name);
			Verify(e.FullPath);
		}

		#endregion

		#region Verification

		private bool HasVerificationReport(string filename) {
			if (!filename.EndsWith(".csv")) throw new Exception("I expect a csv file");
			string reportFileName = filename.Replace(".csv", ".xml");
			return File.Exists(reportFileName);
		}

		private void Verify(string filename) {
			if (!filename.EndsWith(".csv")) throw new Exception("I expect a csv file");
			VerificationReport report = new VerificationReport();
			report.Verified = Verifier.VerifySequentiality(filename);
			string reportFileName = filename.Replace(".csv", ".xml");
			Console.WriteLine("Creating report with SequentialityOk = {0} in {1}", report.Verified, reportFileName);
			report.SaveToFile(reportFileName);
		}

		private void DeleteVerificationReport(string filename) {
			if (!filename.EndsWith(".csv")) throw new Exception("I expect a csv file");
			string reportFileName = filename.Replace(".csv", ".xml");
			Console.WriteLine("Deleting report in {0}", reportFileName);
			if (File.Exists(reportFileName)) {
				File.Delete(reportFileName);
			}
			else {
				Console.WriteLine("Report {0} not found", reportFileName);
			}
		}

		#endregion
	}
}
