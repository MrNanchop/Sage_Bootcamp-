using PSDev.OfficeLine.Academy.DataAccess;
using Sagede.OfficeLine.Data.Entities.Main;
using Sagede.OfficeLine.Engine;
using System;
using System.Collections.Generic;

namespace PSDev.OfficeLine.Academy.BusinessLogic
{
    public class SeminarManager : BaseManager
    {
        /// <summary>
        /// Konstruktor der Klasse
        /// </summary>
        /// <param name="mandant"></param>
        public SeminarManager(Mandant mandant)
            : base(mandant)
        {
        }

        /// <summary>
        /// Listet alle aktiven Seminare mit Terminen auf
        /// </summary>
        /// <returns></returns>
        public List<Seminar> ListSeminare()
        {
            try
            {
                return SeminarData.GetSeminare(Mandant);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Listet ein Seminar inklusive Termine auf
        /// </summary>
        /// <param name="artikelnummer"></param>
        /// <exception cref="RecordNotFoundException">Wird ausgelöst, wenn keine Daten gefunden wurden.</exception>
        /// <returns></returns>
        public Seminar GetSeminar(string artikelnummer)
        {
            try
            {
                return SeminarData.GetSeminar(Mandant, artikelnummer);
            }
            catch (RecordNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Aktualisiert ein Seminar inklusive der Termine
        /// </summary>
        /// <param name="Seminar"></param>
        /// <returns></returns>
        public Seminar UpdateSeminar(Seminar seminar)
        {
            try
            {
                if (seminar == null) { throw new ArgumentNullException("Seminar"); }
                return SeminarData.UpdateSeminar(Mandant, seminar);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Gibt einen Seminartermin zurück
        /// </summary>
        /// <param name="seminarterminID"></param>
        /// <exception cref="RecordNotFoundException">Wird ausgelöst, wenn keine Daten gefunden wurden.</exception>
        /// <returns></returns>
        public Seminartermin GetSeminartermin(string seminarterminID)
        {
            try
            {
                return SeminarData.GetSeminartermin(Mandant, seminarterminID);
            }
            catch (RecordNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Gibt eine Liste von Kunden zurück
        /// </summary>
        /// <param name="filter">Filter für Matchcode</param>
        /// <returns></returns>
        public List<Kunde> GetKunden(string filter)
        {
            try
            {
                return SeminarData.GetKunden(Mandant, filter);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Gibt eine Liste von Ansprechpartner für eine Adresse zurück
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public AnsprechpartnerSet GetAnsprechpartner(int adresse)
        {
            try
            {
                return SeminarData.GetAnsprechpartnerList(Mandant, adresse);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }
    }
}