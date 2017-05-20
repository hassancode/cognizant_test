using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CustomerManager.Process {
	public class ThisAtThatDotCom : ILoginProcess {
		public async Task Execute() {
			Thread.Sleep(5000);
			File.AppendAllText(@"C:\logs\this@that.com.txt", "Logged in at " + DateTime.Now.ToString());

			await Task.Delay(1000);
		}
	}
}