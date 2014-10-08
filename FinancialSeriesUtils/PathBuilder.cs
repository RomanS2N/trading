﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialSeriesUtils {
	public class PathBuilder {
		public static void CreatePaths(string rootFolder, DateTime dateTime, out string folderPath, out string filePath) {
			folderPath = rootFolder + @"\" + dateTime.ToString("yyyy") + @"\" + dateTime.ToString("MM") + @"\";
			filePath = folderPath + dateTime.ToString("dd") + @".bin";
		}
	}
}
