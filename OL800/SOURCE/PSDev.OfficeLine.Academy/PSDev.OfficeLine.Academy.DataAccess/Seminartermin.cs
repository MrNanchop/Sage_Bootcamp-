using System;

namespace PSDev.OfficeLine.Academy.DataAccess
{
    public class Seminartermin
    {
        /// <summary>
        /// SeminarterminId
        /// </summary>
        public string SeminarterminID { get; set; }

        /// <summary>
        /// Mandant
        /// </summary>
        public short Mandant { get; set; }

        /// <summary>
        /// Matchcode
        /// </summary>
        public string Matchcode { get; set; }

        /// <summary>
        /// Artikelnummer
        /// </summary>
        public string Artikelnummer { get; set; }

        /// <summary>
        /// TrainerIDEins
        /// </summary>
        public string TrainerIDEins { get; set; }

        /// <summary>
        /// TrainerIDZwei
        /// </summary>
        public string TrainerIDZwei { get; set; }

        /// <summary>
        /// Startdatum
        /// </summary>
        public DateTime? Startdatum { get; set; }

        /// <summary>
        /// Endedatum
        /// </summary>
        public DateTime? Endedatum { get; set; }

        /// <summary>
        /// Startzeit
        /// </summary>
        public string Startzeit { get; set; }

        /// <summary>
        /// Endezeit
        /// </summary>
        public string Endezeit { get; set; }

        /// <summary>
        /// AnzahlTeilnehmer
        /// </summary>
        public short? AnzahlTeilnehmer { get; set; }

        /// <summary>
        /// AnzahlTeilnehmerMax
        /// </summary>
        public short? AnzahlTeilnehmerMax { get; set; }

        /// <summary>
        /// AnzahlTeilnehmerMin
        /// </summary>
        public short? AnzahlTeilnehmerMin { get; set; }

        /// <summary>
        /// Stornofrist
        /// </summary>
        public short? Stornofrist { get; set; }

        /// <summary>
        /// Adresse
        /// </summary>
        public int? Adresse { get; set; }

        /// <summary>
        /// PLZ
        /// </summary>
        public string PLZ { get; set; }

        /// <summary>
        /// Ort
        /// </summary>
        public string Ort { get; set; }

        /// <summary>
        /// Absagetermin
        /// </summary>
        public DateTime? Absagetermin { get; set; }

        /// <summary>
        /// Abgesagt
        /// </summary>
        public Boolean Abgesagt { get; set; }

        /// <summary>
        /// Absagegrund
        /// </summary>
        public string Absagegrund { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Memo
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// Aktiv
        /// </summary>
        public Boolean Aktiv { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public byte[] Timestamp { get; set; }
    }
}