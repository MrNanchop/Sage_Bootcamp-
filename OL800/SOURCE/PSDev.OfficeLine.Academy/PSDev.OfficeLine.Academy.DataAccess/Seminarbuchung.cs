using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSDev.OfficeLine.Academy.DataAccess
{
    public class Seminarbuchung
    {
        /// <summary>
        /// BuchungID
        /// </summary>
        public int BuchungID { get; set; }

        /// <summary>
        /// Mandant
        /// </summary>
        public short Mandant { get; set; }

        /// <summary>
        /// SeminarterminID
        /// </summary>
        public string SeminarterminID { get; set; }

        /// <summary>
        /// BelID
        /// </summary>
        public int BelID { get; set; }

        /// <summary>
        /// BelPosID
        /// </summary>
        public int BelPosID { get; set; }

        /// <summary>
        /// VorPosID
        /// </summary>
        public int VorPosID { get; set; }

        /// <summary>
        /// Adresse
        /// </summary>
        public int Adresse { get; set; }

        /// <summary>
        /// Konto
        /// </summary>
        public string Konto { get; set; }

        /// <summary>
        /// KontoMatchcode
        /// </summary>
        public string KontoMatchcode { get; set; }

        /// <summary>
        /// Ansprechpartnernummer
        /// </summary>
        public int Ansprechpartnernummer { get; set; }

        /// <summary>
        /// AnsprechpartnerVorname
        /// </summary>
        public string AnsprechpartnerVorname { get; set; }

        /// <summary>
        /// AnsprechpartnerNachname
        /// </summary>
        public string AnsprechpartnerNachname { get; set; }

        /// <summary>
        /// AnsprechpartnerEmail
        /// </summary>
        public string AnsprechpartnerEmail { get; set; }

        /// <summary>
        /// EmailBestaetigungGesendet
        /// </summary>
        public Boolean EmailBestaetigungGesendet { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public byte[] Timestamp { get; set; }

    }





}
