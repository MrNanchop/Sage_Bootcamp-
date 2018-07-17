using PSDev.OfficeLine.Academy.DataAccess;
using Sagede.Core.Tools;
using Sagede.OfficeLine.Data.Entities.Main;
using Sagede.OfficeLine.Engine;
using Sagede.OfficeLine.Wawi.BelegEngine;
using Sagede.OfficeLine.Wawi.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PSDev.OfficeLine.Academy.BusinessLogic
{
    /// <summary>
    /// Die Klasse SeminarbuchungManager stellt sämtliche Methoden zur Verbuchung von Seminaren zur Verfügung.
    /// </summary>
    public class SeminarbuchungManager : BaseManager
    {
        #region Private Member

        private SeminarManager _seminarManager;

        #endregion Private Member

        #region Konstruktor

        /// <summary>
        /// Konstruktor der Klasse
        /// </summary>
        /// <param name="mandant">Mandanten Objekt</param>
        public SeminarbuchungManager(Mandant mandant)
            : base(mandant)
        {
            _seminarManager = new SeminarManager(mandant);
        }

        #endregion Konstruktor

        #region Public Implementierung

        /// <summary>
        /// Führt eine Seminarbuchung durch bzw. aktualisiert diese
        /// </summary>
        /// <param name="seminarbuchung"></param>
        /// <returns></returns>
        public Seminarbuchung UpdateBuchung(Seminarbuchung seminarbuchung)
        {
            try
            {
                Seminarbuchung buchungUpdated;

                // Validierung des Objektes
                validateSeminarbuchung(seminarbuchung);
                buchungUpdated = SeminarData.UpdateOrInsertSeminarbuchung(Mandant, seminarbuchung);

                /// Aktualisierung erst, wenn Buchung erfolgreich committed
                SeminarData.UpdateSeminarterminTeilnehmer(Mandant, seminarbuchung.SeminarterminID);

                return buchungUpdated;
            }
            catch (RecordUpdateException)
            {
                throw;
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Gibt eine Seminarbuchung zurück
        /// </summary>
        /// <param name="buchungID"></param>
        /// <returns></returns>
        public Seminarbuchung GetBuchung(int buchungID)
        {
            try
            {
                return SeminarData.GetSeminarbuchung(Mandant, buchungID);
            }
            catch (RecordNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Gibt alle Seminarbuchungen für eine Belegposition zurück
        /// </summary>
        /// <param name="belegHandle"></param>
        /// <param name="belegPositionHandle"></param>
        /// <returns></returns>
        public Seminarbuchungen GetBuchungen(int belegHandle, int belegPositionHandle)
        {
            try
            {
                return SeminarData.GetSeminarbuchungen(Mandant, belegHandle, belegPositionHandle);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Gibt alle Seminarbuchungen für eine Belegposition zurück
        /// </summary>
        /// <param name="vorPosHandle"></param>
        /// <returns></returns>
        public Seminarbuchungen GetBuchungen(int vorPosHandle)
        {
            try
            {
                return SeminarData.GetSeminarbuchungen(Mandant, vorPosHandle);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Löscht alle Seminarbuchungen zu einer BelegPosition
        /// </summary>
        /// <param name="belegHandle"></param>
        /// <param name="belegPositionHandle"></param>
        public void DeleteBuchungen(int belegHandle, int belegPositionHandle)
        {
            try
            {
                SeminarData.DeleteSeminarbuchungen(Mandant, belegHandle, belegPositionHandle);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Löscht eine Buchung
        /// </summary>
        /// <param name="BuchungID"></param>
        public void DeleteBuchung(int buchungID)
        {
            try
            {
                var seminarterminList = new List<string>();
                SeminarData.DeleteSeminarbuchung(Mandant, buchungID);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Führt eine Seminarbuchung durch und legt den Warenwirtschaftsbeleg
        /// in der Office Line an.
        /// </summary>
        /// <param name="seminarbuchung"></param>
        /// <returns></returns>
        public Seminarbuchung CreateOrUpdateBuchungsbeleg(Seminarbuchung seminarbuchung)
        {
            // TODO: Implementierung
            throw new NotImplementedException("CreateOrUpdatebuchungsbeleg");
        }

        /// <summary>
        /// Gibt alle Ansprechpartner zu einer Adresse zurück
        /// </summary>
        /// <param name="adresse"></param>
        /// <returns></returns>
        public AnsprechpartnerSet ListAnsprechpartner(int adresse)
        {
            try
            {
                return _seminarManager.GetAnsprechpartner(adresse);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Gibt einen Ansprechpartner zurück
        /// </summary>
        /// <param name="ansprechpartnernummer"></param>
        /// <returns></returns>
        public AnsprechpartnerItem GetAnsprechpartner(int ansprechpartnernummer)
        {
            try
            {
                return SeminarData.GetAnsprechpartner(Mandant, ansprechpartnernummer);
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
        /// Ermittelt einen Ansprechpartner zu einer Adresse über den Namen und Vornamen
        /// </summary>
        /// <param name="adresse"></param>
        /// <param name="nachname"></param>
        /// <param name="vorname"></param>
        /// <returns></returns>
        public AnsprechpartnerItem CreateOrGetAnsprechpartner(int adresse, string nachname, string vorname, string email)
        {
            try
            {
                if (SeminarData.AnsprechpartnerExists(Mandant, adresse, email))
                {
                    var ansprechpartner = SeminarData.GetAnsprechpartner(Mandant, adresse, email);
                    ansprechpartner.Nachname = nachname;
                    ansprechpartner.Vorname = vorname;
                    return SeminarData.UpdateAnsprechpartner(Mandant, ansprechpartner);
                }
                else
                {
                    var ansprechpartner = new AnsprechpartnerItem();
                    ansprechpartner.Adresse = adresse;
                    ansprechpartner.Nachname = nachname;
                    ansprechpartner.Vorname = vorname;
                    ansprechpartner.EMail = email;
                    ansprechpartner.Ansprechpartner = $"{vorname} {nachname}";
                    return SeminarData.UpdateAnsprechpartner(Mandant, ansprechpartner);
                }
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Aktualisiert einen Ansprechpartner
        /// </summary>
        /// <param name="ansprechpartner"></param>
        /// <returns></returns>
        public AnsprechpartnerItem UpdateAnsprechpartner(AnsprechpartnerItem ansprechpartner)
        {
            try
            {
                if (ansprechpartner.Adresse == 0) { throw new Exception("Speicherung von Ansprechpartner ohne Adresse nicht möglich."); }

                return SeminarData.UpdateAnsprechpartner(Mandant, ansprechpartner);
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        #endregion Public Implementierung

        #region Private Implementierung

        private static BelegPosition appendNewBelegPosition(Seminartermin seminartermin, Beleg beleg)
        {
            var belegPosition = new BelegPosition(beleg);
            belegPosition.Initialize(Positionstyp.Artikel);

            if (!belegPosition.SetArtikel(seminartermin.Artikelnummer, 0))
            {
                throw new Exception(beleg.Errors.GetDescriptionSummary());
            }

            belegPosition.Menge = 1;
            belegPosition.RefreshBasismenge(true, 2);

            // Preisfindung der OL deaktivieren
            //belegPosition.Einzelpreis = 1000;
            //belegPosition.IstEinzelpreisManuell = true;

            belegPosition.Calculate();
            beleg.Positionen.Add(belegPosition);
            return belegPosition;
        }

        /// <summary>
        /// Prüft eine Seminarbuchung und sorgt für die notwendigen Plausibilitäten (z.B. Anlage Ansprechpartner etc.)
        /// </summary>
        /// <param name="seminarbuchung">Seminarbuchung</param>
        private void validateSeminarbuchung(Seminarbuchung seminarbuchung)
        {
            if (SeminarData.KontokorrentExists(Mandant, seminarbuchung.Konto, true))
            {
                var konto = SeminarData.GetKunde(Mandant, seminarbuchung.Konto);
                if (seminarbuchung.Adresse == 0) seminarbuchung.Adresse = konto.Adresse;
                if (string.IsNullOrWhiteSpace(seminarbuchung.KontoMatchcode)) seminarbuchung.KontoMatchcode = konto.Matchcode;

                var ansprechpartner = this.CreateOrGetAnsprechpartner(seminarbuchung.Adresse, seminarbuchung.AnsprechpartnerNachname, seminarbuchung.AnsprechpartnerVorname, seminarbuchung.AnsprechpartnerEmail);
                seminarbuchung.Ansprechpartnernummer = ansprechpartner.Nummer;
                var seminartermin = this._seminarManager.GetSeminartermin(seminarbuchung.SeminarterminID);
                if (seminartermin.AnzahlTeilnehmer.GetValueOrDefault() >= seminartermin.AnzahlTeilnehmerMax)
                {
                    throw new BuchungValidationException(Properties.Resources.BuchungValidationSeminarterminSoldOut);
                }
            }
            else
            {
                throw new BuchungValidationException(Properties.Resources.BuchungValidationException);
            }
        }

        #endregion Private Implementierung
    }
}