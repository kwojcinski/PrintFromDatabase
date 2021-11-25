using System;
using System.Collections.Generic;
using System.Text;

namespace PrintFromDatabase
{
    public class Player
    {
        public int id_zawodnika { get; set; }
        public int id_trenera { get; set; }
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string kraj { get; set; }
        public DateTime data_ur { get; set; }
        public int wzrost { get; set; }
        public double waga { get; set; }
    }
}
