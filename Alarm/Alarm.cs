using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alarm {
    internal class Alarm {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public bool Mon { get; set; }
        public bool Tue { get; set; }
        public bool Wed { get; set; }
        public bool Thu { get; set; }
        public bool Fri { get; set; }
        public bool Sat { get; set; }
        public bool Sun { get; set; }
        public bool Enabled { get; set; }

        public Alarm() {
            ID = -1;
            Name = "";
            Hours = -1;
            Minutes = -1;
            Seconds = -1;
            Mon = false;
            Tue = false;
            Wed = false;
            Thu = false;
            Fri = false;
            Sat = false;
            Sun = false;
            Enabled = false;
        }

        public Alarm(int id, string name, int hours, int minutes, int seconds, bool mon, bool tue, bool wed, bool thu, bool fri, bool sat, bool sun, bool enabled) {
            ID = id;
            Name = name;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Mon = mon;
            Tue = tue;
            Wed = wed;
            Thu = thu;
            Fri = fri;
            Sat = sat;
            Sun = sun;
            Enabled = enabled;
        }
    }
}
