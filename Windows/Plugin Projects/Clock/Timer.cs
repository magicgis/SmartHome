using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clock {
	public class Timer {
		public int ID { get; set; }
		public string Name { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public int Seconds { get; set; }

		public Timer() {
			ID = -1;
			Name = "";
			Hours = -1;
			Minutes = -1;
			Seconds = -1;
		}

		public Timer(int id, string name, int hours, int minutes, int seconds) {
			ID = id;
			Name = name;
			Hours = hours;
			Minutes = minutes;
			Seconds = seconds;
		}
	}
}
