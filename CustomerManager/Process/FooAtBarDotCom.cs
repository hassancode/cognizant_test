using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace CustomerManager.Process {
	public class FooAtBarDotCom : ILoginProcess {
		public void Execute() {
			Thread.Sleep(5000);
			File.AppendAllText(@"C:\logs\foo@bar.com.txt", "Logged in at " + DateTime.Now.ToString());
		}
	}
}