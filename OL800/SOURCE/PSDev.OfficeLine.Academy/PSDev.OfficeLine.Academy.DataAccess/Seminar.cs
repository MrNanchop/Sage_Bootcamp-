using System;
using System.Collections.Generic;

namespace PSDev.OfficeLine.Academy.DataAccess
{
    public class Seminar
    {
        public Seminar()
        {
            this.SeminarterminCollection = new List<Seminartermin>();
        }

        public string Artikelnummer { get; set; }

        public short Mandant { get; set; }

        public string Bezeichnung1 { get; set; }

        public string Bezeichnung2 { get; set; }

        public string Matchcode { get; set; }

        public string Langtext { get; set; }

        public string LangtextHTML { get; set; }

        public string LangtextRTF { get; set; }

        public string Dimensionstext { get; set; }

        public string DimensionstextHTML { get; set; }

        public string DimensionstextRTF { get; set; }

        public string Memo { get; set; }

        public string Artikelgruppe { get; set; }

        public Boolean Aktiv { get; set; }

        public byte[] Timestamp { get; set; }

        public short? USER_AnzahlTage { get; set; }

        public Boolean USER_Seminar { get; set; }

        public string USER_UhrzeitBis { get; set; }

        public string USER_UhrzeitVon { get; set; }

        public List<Seminartermin> SeminarterminCollection { get; set; }
    }
}