﻿using Sagede.Core.Tools;
using Sagede.OfficeLine.Data;
using Sagede.OfficeLine.Data.Entities.Main;
using Sagede.OfficeLine.Engine;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PSDev.OfficeLine.Academy.DataAccess
{
    /// <summary>
    /// Datenzugriff auf eigene Tabellen der Seminarverwaltung
    /// </summary>
    public static class SeminarData
    {
        #region Seminarbuchung

        /// <summary>
        /// GetSeminarbuchung
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="buchungID"></param>
        /// <returns></returns>
        /// <exception cref="Exception">wird bei allgemeinen Fehler geworfen.</exception>
        /// <exception cref="RecordNotFoundException">wird geworfen, wenn Datensatz nicht in Datenbank vorhanden.</exception>
        public static object GetSeminarbuchung(Mandant mandant, int buchungID)
        {
            throw new NotImplementedException("GetSeminarbuchung");
        }

        /// <summary>
        /// Aktualisiert oder legt eine neue Seminarbuchung an
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="buchung"></param>
        /// <returns></returns>
        /// <exception cref="Exception">wird bei allgemeinen Fehler geworfen.</exception>
        /// <exception cref="RecordUpdateException">wird bei Fehlern im DB-Update geworfen.</exception>
        public static object UpdateOrInsertSeminarbuchung(Mandant mandant, object buchung)
        {
            throw new NotImplementedException("UpdateOrInsertSeminarbuchung");
        }

        /// <summary>
        /// Löscht eine Seminarbuchung in der Datenbank
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="buchungID"></param>
        public static void DeleteSeminarbuchung(Mandant mandant, int buchungID)
        {
            throw new NotImplementedException("DeleteSeminarbuchung");
        }

        /// <summary>
        /// Löscht die Seminarbuchungen zu einer BelegPosition
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="belID"></param>
        /// <param name="belPosID"></param>
        public static void DeleteSeminarbuchungen(Mandant mandant, int belID, int belPosID)
        {
            throw new NotImplementedException("DeleteSeminarbuchungen");
        }

        /// <summary>
        /// Gibt eine Liste aller Seminarbuchungen zu einer BelegPosition zurück
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="belID"></param>
        /// <param name="belPosID"></param>
        /// <returns></returns>
        public static object GetSeminarbuchungen(Mandant mandant, int belID, int belPosID)
        {
            throw new NotImplementedException("GetSeminarbuchungen");
        }

        /// <summary>
        /// Gibt eine Liste aller Seminarbuchungen zu einer BelegPosition anhand der VorPosID zurück
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="vorPosID"></param>
        /// <returns></returns>
        public static object GetSeminarbuchungen(Mandant mandant, int vorPosID)
        {
            throw new NotImplementedException("GetSeminarbuchungen");
        }

        public static string GetSeminarterminByBuchungID(Mandant mandant, int buchungID)
        {
            throw new NotImplementedException("GetSeminarterminByBuchungID");
        }

        #endregion Seminarbuchung

        #region Seminar-/Seminartermine

        public static object GetSeminartermin(Mandant mandant, string seminarterminID)
        {
            throw new NotImplementedException("GetSeminartermin");
        }

        public static object UpdateOrCreateSeminartermin(Mandant mandant, object seminartermin)
        {
            throw new NotImplementedException("UpdateOrCreateSeminartermin");
        }

        public static object GetSeminartermine(Mandant mandant, string artikelnummer)
        {
            throw new NotImplementedException("UpdateOrCreateSeminartermin");
        }

        public static object GetSeminar(Mandant mandant, string artikelnummer)
        {
            throw new NotImplementedException("GetSeminar");
        }

        public static object UpdateSeminar(Mandant mandant, object seminar)
        {
            throw new NotImplementedException("UpdateSeminar");
        }

        public static object GetSeminare(Mandant mandant)
        {
            throw new NotImplementedException("GetSeminare");
        }

        public static void UpdateSeminarterminTeilnehmer(Mandant mandant, string seminarterminID)
        {
            throw new NotImplementedException("UpdateSeminarterminTeilnehmer");
        }

        #endregion Seminar-/Seminartermine

        #region Kunden

        public static object GetKunden(Mandant mandant, string filter)
        {
            throw new NotImplementedException("GetKunden");
        }

        public static object GetKunde(Mandant mandant, string kto)
        {
            throw new NotImplementedException("GetKunde");
        }

        public static object GetAnsprechpartner(Mandant mandant, int adresse, string email)
        {
            throw new NotImplementedException("GetAnsprechpartner");
        }

        public static object GetAnsprechpartner(Mandant mandant, int nummer)
        {
            throw new NotImplementedException("GetAnsprechpartner");
        }

        public static List<object> GetAnsprechpartnerList(Mandant mandant, int adresse)
        {
            throw new NotImplementedException("GetAnsprechpartnerList");
        }

        public static object UpdateAnsprechpartner(Mandant mandant, object ansprechpartner)
        {
            throw new NotImplementedException("UpdateAnsprechpartner");
        }

        public static bool AnsprechpartnerExists(Mandant mandant, int adresse, string email)
        {
            throw new NotImplementedException("AnsprechpartnerExists");
        }

        public static bool KontokorrentExists(Mandant mandant, string kto, bool istDebitor)
        {
            throw new NotImplementedException("KontokorrentExists");
        }
    }

    #endregion Kunden
}