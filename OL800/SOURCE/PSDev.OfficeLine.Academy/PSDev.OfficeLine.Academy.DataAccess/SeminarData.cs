using Sagede.Core.Tools;
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
        public static Seminarbuchung GetSeminarbuchung(Mandant mandant, int buchungID)
        {
            var qry = "SELECT * FROM PSDSeminarbuchungen WHERE Mandant=@mandant AND BuchungID=@buchungid";
            var command = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(qry);
            command.AppendInParameter("mandant", typeof(short), mandant.Id);
            command.AppendInParameter("buchungid", typeof(int), buchungID);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    // Variante 1 instanziierung und Zuweisung
                    //var seminarbuchung = new Seminarbuchung();
                    //seminarbuchung.Adresse = reader.GetInt32("Adresse");
                    //seminarbuchung.AnsprechpartnerEmail = reader.GetString("AnsprechpartnerEmail");
                    //seminarbuchung.AnsprechpartnerNachname = reader.GetString("AnsprechpartnerNachname");

                    // Variante 2 über Class-Initializer
                    return new Seminarbuchung()
                    {
                        Adresse = reader.GetInt32("Adresse"),
                        AnsprechpartnerEmail = reader.GetString("AnsprechpartnerEmail"),
                        AnsprechpartnerNachname = reader.GetString("AnsprechpartnerNachname"),
                        Ansprechpartnernummer = reader.GetInt32("Ansprechpartnernummer"),
                        AnsprechpartnerVorname = reader.GetString("AnsprechpartnerVorname"),
                        BelID = reader.GetInt32("BelID"),
                        BelPosID = reader.GetInt32("BelPosID"),
                        BuchungID = reader.GetInt32("BuchungID"),
                        EmailBestaetigungGesendet = reader.GetDBBoolean("EmailBestaetigungGesendet"),
                        Konto = reader.GetString("Konto"),
                        KontoMatchcode = reader.GetString("KontoMatchcode"),
                        Mandant = reader.GetInt16("Mandant"),
                        SeminarterminID = reader.GetString("SeminarterminID"),
                        Timestamp = reader.GetBytes("Timestamp"),
                        VorPosID = reader.GetInt32("VorPosID")
                    };
                }
                else
                {
                    throw new RecordNotFoundException("Seminarbuchung", buchungID.ToString());
                }

            }

        }

        /// <summary>
        /// Aktualisiert oder legt eine neue Seminarbuchung an
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="buchung"></param>
        /// <returns></returns>
        /// <exception cref="Exception">wird bei allgemeinen Fehler geworfen.</exception>
        /// <exception cref="RecordUpdateException">wird bei Fehlern im DB-Update geworfen.</exception>
        public static Seminarbuchung UpdateOrInsertSeminarbuchung(Mandant mandant, Seminarbuchung buchung)
        {
            var qry = new StringBuilder();

            if (buchung.BuchungID == 0)
            {
                // Neuanlage
                buchung.BuchungID = mandant.MainDevice.GetTan("PSDSeminarbuchungen", mandant.Id);
                buchung.Mandant = buchung.Mandant == 0 ? mandant.Id : buchung.Mandant;
                qry.AppendLine("INSERT INTO PSDSeminarbuchungen ");
                qry.AppendLine("(BuchungID, Mandant, SeminarterminID, BelID, BelPosID, VorPosID, Adresse, ");
                qry.AppendLine("Konto, KontoMatchcode, Ansprechpartnernummer, AnsprechpartnerVorname, ");
                qry.AppendLine("AnsprechpartnerNachname, AnsprechpartnerEmail, EmailBestaetigungGesendet)");
                qry.AppendLine("VALUES");
                qry.AppendLine("(@buchungid, @mandant, @seminarterminid, @belid, @belposid, @vorposid, @adresse, ");
                qry.AppendLine("@konto, @kontomatchcode, @ansprechpartnernummer, @ansprechpartnervorname, ");
                qry.AppendLine("@ansprechpartnernachname, @ansprechpartneremail, @emailbestaetigunggesendet)");
            }
            else
            {
                // Aktualisierung
                qry.AppendLine("UPDATE PSDSeminarbuchungen SET ");
                qry.AppendLine("SeminarterminID=@seminarterminid, BelID=@belid, BelPosID=@belposid, VorPosID=@vorposid, ");
                qry.AppendLine("Adresse=@adresse, Konto=@konto, KontoMatchcode=@kontomatchcode, Ansprechpartnernummer=@ansprechpartnernummer, AnsprechpartnerVorname=@ansprechpartnervorname, ");
                qry.AppendLine("AnsprechpartnerNachname=@ansprechpartnernachname, AnsprechpartnerEmail=@ansprechpartneremail, EmailbestaetigungGesendet=@emailbestaetigunggesendet ");
                qry.AppendLine("WHERE Mandant=@mandant AND BuchungID=@buchungID");
            }

            var command = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(qry.ToString());
            command.AppendInParameter("buchungid", typeof(int), buchung.BuchungID);
            command.AppendInParameter("mandant", typeof(short), buchung.Mandant);
            command.AppendInParameter("seminarterminid", typeof(string), buchung.SeminarterminID);
            command.AppendInParameter("belid", typeof(int), buchung.BelID);
            command.AppendInParameter("belposid", typeof(int), buchung.BelPosID);
            command.AppendInParameter("vorposid", typeof(int), buchung.VorPosID);
            command.AppendInParameter("adresse", typeof(int), buchung.Adresse);
            command.AppendInParameter("konto", typeof(string), buchung.Konto);
            command.AppendInParameter("kontomatchcode", typeof(string), buchung.KontoMatchcode);
            command.AppendInParameter("ansprechpartnernummer", typeof(int), buchung.Ansprechpartnernummer);
            command.AppendInParameter("ansprechpartnervorname", typeof(string), buchung.AnsprechpartnerVorname);
            command.AppendInParameter("ansprechpartnernachname", typeof(string), buchung.AnsprechpartnerNachname);
            command.AppendInParameter("ansprechpartneremail", typeof(string), buchung.AnsprechpartnerEmail);
            command.AppendInParameter("emailbestaetigunggesendet", typeof(short), ConversionHelper.ToDBBoolean(buchung.EmailBestaetigungGesendet));

            var result = command.TryExecuteNonQuery();

            if (result.State.IsSucceeded)
            {
                buchung = SeminarData.GetSeminarbuchung(mandant, buchung.BuchungID);
                return buchung;
            }
            else
            {
                TraceLog.LogException(result.State.ExceptionOccurred);
                throw new RecordUpdateException("Seminarbuchung", buchung.BuchungID.ToString());
            }
        }

        /// <summary>
        /// Löscht eine Seminarbuchung in der Datenbank
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="buchungID"></param>
        public static void DeleteSeminarbuchung(Mandant mandant, int buchungID)
        {
            var qry = $"DELETE FROM PSDSeminarbuchungen WHERE Mandant={mandant.Id} AND BuchungID={buchungID}";
            var result = mandant.MainDevice.GenericConnection.TryExecuteNonQuery(qry);
            if (!result.State.IsSucceeded)
            {
                throw new RecordUpdateException("Seminarbuchung", buchungID.ToString());
            }
        }

        /// <summary>
        /// Löscht die Seminarbuchungen zu einer BelegPosition
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="belID"></param>
        /// <param name="belPosID"></param>
        public static void DeleteSeminarbuchungen(Mandant mandant, int belID, int belPosID)
        {
            try
            {
                var qry = "DELETE FROM PSDSeminarbuchungen WHERE Mandant=@mandant AND BelID=@belid";
                var command = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(qry);
                command.AppendInParameter("mandant", typeof(short), mandant.Id);
                command.AppendInParameter("belid", typeof(int), belID);
                command.AppendInParameter("belposid", typeof(int), belPosID);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Gibt eine Liste aller Seminarbuchungen zu einer BelegPosition zurück
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="belID"></param>
        /// <param name="belPosID"></param>
        /// <returns></returns>
        public static Seminarbuchungen GetSeminarbuchungen(Mandant mandant, int belID, int belPosID)
        {
            try
            {
                var list = new Seminarbuchungen();
                var qry = "SELECT BuchungID FROM PSDSeminarbuchungen WHERE Mandant=@mandant AND BelID=@belid AND BelPosID=@belposid";
                var command = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(qry);
                command.AppendInParameter("mandant", typeof(short), mandant.Id);
                command.AppendInParameter("belid", typeof(int), belID);
                command.AppendInParameter("belposid", typeof(int), belPosID);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(SeminarData.GetSeminarbuchung(mandant, reader.GetInt32("BuchungID")));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Gibt eine Liste aller Seminarbuchungen zu einer BelegPosition anhand der VorPosID zurück
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="vorPosID"></param>
        /// <returns></returns>
        public static Seminarbuchungen GetSeminarbuchungen(Mandant mandant, int vorPosID)
        {
            try
            {
                var list = new Seminarbuchungen();
                var qry = "SELECT BuchungID FROM PSDSeminarbuchungen WHERE Mandant=@mandant AND VorPosID=@vorposid";
                var command = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(qry);
                command.AppendInParameter("mandant", typeof(short), mandant.Id);
                command.AppendInParameter("vorposid", typeof(int), vorPosID);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(SeminarData.GetSeminarbuchung(mandant, reader.GetInt32("BuchungID")));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
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

        /// <summary>
        /// Prüft, ob ein Ansprechpartner mit Email und Adressnummer vorhanden ist
        /// </summary>
        /// <param name="mandant">Mandant</param>
        /// <param name="adresse">Adresse des Ansprechpartners</param>
        /// <param name="email">Email des Ansprechpartners</param>
        /// <returns></returns>
        public static bool AnsprechpartnerExists(Mandant mandant, int adresse, string email)
        {
            return mandant.MainDevice.Lookup.RowExists("Nummer", "KHKAnsprechpartner", $"Mandant={mandant.Id} AND Adresse={adresse} AND EMail={SqlStrings.ToSqlString(email)}");
        }

        /// <summary>
        /// Prüft, ob ein Kontokorrent vorhanden ist
        /// </summary>
        /// <param name="mandant">Mandant</param>
        /// <param name="kto">zu prüfendes Konto</param>
        /// <param name="istDebitor">Angabe, ob Debitor oder Kreditor</param>
        /// <returns>true - Konto vorhanden, false - Konto nicht vorhanden</returns>
        public static bool KontokorrentExists(Mandant mandant, string kto, bool istDebitor)
        {
            var ktoArt = istDebitor ? "D" : "K";
            return mandant.MainDevice.Lookup.RowExists("Kto", "KHKKontokorrent",
                $"Mandant={mandant.Id} AND Kto={SqlStrings.ToSqlString(kto)} AND KtoArt={SqlStrings.ToSqlString(ktoArt)}");
        }
    }

    #endregion Kunden
}