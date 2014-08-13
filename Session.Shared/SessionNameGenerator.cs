using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Session.Shared {
	public class SessionNameGenerator {
		public static string CreateName() {
			return DateTime.UtcNow.ToString("yyyy.MM.dd.hh.mm.ss.ffffff");
		}
	}
}
