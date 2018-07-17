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

        /// <summary>
        /// Ermittelt die SeminarterminID anhand der BuchungID
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="buchungID"></param>
        /// <returns></returns>
        public static string GetSeminarterminByBuchungID(Mandant mandant, int buchungID)
        {
            return mandant.MainDevice.Lookup.GetString("SeminarterminID", "PSDSeminarbuchungen", $"Mandant={mandant.Id} AND BuchungID={buchungID}", string.Empty);
        }

        #endregion Seminarbuchung

        #region Seminar-/Seminartermine

        public static Seminartermin GetSeminartermin(Mandant mandant, string seminarterminID)
        {
            var qry = "SELECT * FROM PSDSeminartermine WHERE Mandant=@mandant AND SeminarterminID=@seminarterminid";
            var command = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(qry);
            command.AppendInParameter("mandant", typeof(short), mandant.Id);
            command.AppendInParameter("seminarterminid", typeof(string), seminarterminID);

            try
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Seminartermin()
                        {
                            Abgesagt = reader.GetDBBoolean("Abgesagt"),
                            Absagegrund = reader.GetString("Absagegrund"),
                            Absagetermin = reader.GetNullableDateTime("Absagetermin"),
                            Adresse = reader.GetNullableInt32("Adresse"),
                            Aktiv = reader.GetDBBoolean("Aktiv"),
                            AnzahlTeilnehmer = reader.GetNullableInt16("AnzahlTeilnehmer"),
                            AnzahlTeilnehmerMax = reader.GetNullableInt16("AnzahlTeilnehmerMax"),
                            AnzahlTeilnehmerMin = reader.GetNullableInt16("AnzahlTeilnehmerMin"),
                            Artikelnummer = reader.GetString("Artikelnummer"),
                            Endedatum = reader.GetNullableDateTime("Endedatum"),
                            Endezeit = reader.GetString("Endezeit"),
                            Mandant = reader.GetInt16("Mandant"),
                            Matchcode = reader.GetString("Matchcode"),
                            Memo = reader.GetString("Memo"),
                            Ort = reader.GetString("Ort"),
                            PLZ = reader.GetString("PLZ"),
                            SeminarterminID = reader.GetString("SeminarterminID"),
                            Startdatum = reader.GetNullableDateTime("Startdatum"),
                            Startzeit = reader.GetString("Startzeit"),
                            Status = reader.GetString("Status"),
                            Stornofrist = reader.GetNullableInt16("Stornofrist"),
                            TrainerIDEins = reader.GetString("TrainerIDEins"),
                            TrainerIDZwei = reader.GetString("TrainerIDZwei"),
                            Timestamp = reader.GetBytes("Timestamp")
                        };
                    }
                    else
                    {
                        throw new RecordNotFoundException("Seminartermin", seminarterminID);
                    }
                }
            }
            catch (RecordNotFoundException ex)
            {
                TraceLog.LogVerbose(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        public static object UpdateOrCreateSeminartermin(Mandant mandant, Seminartermin seminartermin)
        {
            var newRecord = false;
            var qry = new StringBuilder();
            try
            {
                var loadTermin = SeminarData.GetSeminartermin(mandant, seminartermin.SeminarterminID);
            }
            catch (RecordNotFoundException)
            {
                newRecord = true;
            }

            if (newRecord)
            {
                qry.AppendLine("INSERT INTO PSDSeminartermine ");
                qry.AppendLine("(SeminarterminID, Mandant, Matchcode, Artikelnummer, TrainerIDEins, TrainerIDZwei, Startdatum, Endedatum, Startzeit, Endezeit, ");
                qry.AppendLine("AnzahlTeilnehmer, AnzahlTeilnehmerMax, AnzahlTeilnehmerMin, Stornofrist, Adresse, PLZ, Ort, Absagetermin, Abgesagt,  ");
                qry.AppendLine("Absagegrund, Status, Memo, Aktiv)");
                qry.AppendLine("VALUES");
                qry.AppendLine("@seminarterminid, @mandant, @matchcode, @artikelnummer, @trainerideins, @traineridzwei, @startdatum, @endedatum, @startzeit, @endezeit, ");
                qry.AppendLine("@anzahlteilnehmer, @anzahlteilnehmermax, @anzahlteilnehmermin, @stornofrist, @adresse, @plz, @ort, @absagetermin, @abgesagt, ");
                qry.AppendLine("@absagegrund, @status, @memo, @aktiv)");
            }
            else
            {
                qry.AppendLine("UPDATE PSDSeminartermine ");
                qry.AppendLine("SET ");
                qry.AppendLine("Matchcode=@matchcode, Artikelnummer=@artikelnummer, TrainerIDEins=@trainerideins, TrainerIDZwei=@traineridzwei, Startdatum=@startdatum, Endedatum=@endedatum, Startzeit=@startzeit, Endezeit=@endezeit, ");
                qry.AppendLine("AnzahlTeilnehmer=@anzahlteilnehmer, AnzahlTeilnehmerMax=@anzahlteilnehmermax, AnzahlTeilnehmerMin=@anzahlteilnehmermin, Stornofrist=@stornofrist, Adresse=@adresse, PLZ=@plz, Ort=@ort, Absagetermin=@absagetermin, Abgesagt=@abgesagt,  ");
                qry.AppendLine("Absagegrund=@absagegrund, Status=@status, Memo=@memo, Aktiv=@aktiv");
                qry.AppendLine("WHERE");
                qry.AppendLine("SeminarterminID=@seminarterminid AND Mandant=@mandant");
            }

            try
            {
                var command = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(qry.ToString());
                command.AppendInParameter("seminarterminid", typeof(string), seminartermin.SeminarterminID);
                command.AppendInParameter("mandant", typeof(short), mandant.Id);
                command.AppendInParameter("matchcode", typeof(string), seminartermin.Matchcode);
                command.AppendInParameter("artikelnummer", typeof(string), seminartermin.Artikelnummer);
                command.AppendInParameter("trainerideins", typeof(string), seminartermin.TrainerIDEins);
                command.AppendInParameter("traineridzwei", typeof(string), seminartermin.TrainerIDZwei);
                command.AppendInParameter("startdatum", typeof(DateTime), seminartermin.Startdatum);
                command.AppendInParameter("endedatum", typeof(DateTime), seminartermin.Endedatum);
                command.AppendInParameter("startzeit", typeof(string), seminartermin.Startzeit);
                command.AppendInParameter("endezeit", typeof(string), seminartermin.Endezeit);
                command.AppendInParameter("anzahlteilnehmer", typeof(short), seminartermin.AnzahlTeilnehmer);
                command.AppendInParameter("anzahlteilnehmermin", typeof(short), seminartermin.AnzahlTeilnehmerMin);
                command.AppendInParameter("anzahlteilnehmermax", typeof(short), seminartermin.AnzahlTeilnehmerMax);
                command.AppendInParameter("stornofrist", typeof(short), seminartermin.Stornofrist);
                command.AppendInParameter("adresse", typeof(int), seminartermin.Adresse);
                command.AppendInParameter("plz", typeof(string), seminartermin.PLZ);
                command.AppendInParameter("ort", typeof(string), seminartermin.Ort);
                command.AppendInParameter("absagetermin", typeof(DateTime), seminartermin.Absagetermin);
                command.AppendInParameter("abgesagt", typeof(short), ConversionHelper.ToDBBoolean(seminartermin.Abgesagt));
                command.AppendInParameter("absagegrund", typeof(string), seminartermin.Absagegrund);
                command.AppendInParameter("status", typeof(string), seminartermin.Status);
                command.AppendInParameter("memo", typeof(string), seminartermin.Memo);
                command.AppendInParameter("aktiv", typeof(short), ConversionHelper.ToDBBoolean(seminartermin.Aktiv));
                command.ExecuteNonQuery();

                seminartermin = SeminarData.GetSeminartermin(mandant, seminartermin.SeminarterminID);

                return seminartermin;
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        public static List<Seminartermin> GetSeminartermine(Mandant mandant, string artikelnummer)
        {
            var list = new List<Seminartermin>();
            var qry = "SELECT SeminarterminID FROM PSDSeminartermine WHERE Mandant=@mandant AND Artikelnummer=@artikelnummer";
            var command = mandant.MainDevice.GenericConnection.CreateSqlStringCommand(qry);
            command.AppendInParameter("mandant", typeof(short), mandant.Id);
            command.AppendInParameter("artikelnummer", typeof(string), artikelnummer);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(SeminarData.GetSeminartermin(mandant, reader.GetString("SeminarterminID")));
                }
            }
            return list;
        }

        public static Seminar GetSeminar(Mandant mandant, string artikelnummer)
        {
            var artikelItem = mandant.MainDevice.Entities.Artikel.GetItem(artikelnummer, mandant.Id);

            if (artikelItem == null)
            {
                throw new RecordNotFoundException("Seminar/Artikel", artikelnummer);
            }

            var seminar = new Seminar()
            {
                Aktiv = artikelItem.Aktiv,
                Artikelgruppe = artikelItem.Artikelgruppe,
                Artikelnummer = artikelItem.Artikelnummer,
                Bezeichnung1 = artikelItem.Bezeichnung1,
                Bezeichnung2 = artikelItem.Bezeichnung2,
                Dimensionstext = artikelItem.Dimensionstext,
                DimensionstextHTML = artikelItem.DimensionstextHTML,
                DimensionstextRTF = artikelItem.DimensionstextRTF,
                Langtext = artikelItem.Langtext,
                LangtextHTML = artikelItem.LangtextHTML,
                LangtextRTF = artikelItem.LangtextRTF,
                Mandant = artikelItem.Mandant,
                Matchcode = artikelItem.Matchcode,
                Memo = artikelItem.Memo,
                Timestamp = artikelItem.Timestamp,
                USER_AnzahlTage = ConversionHelper.ToInt16(artikelItem.UserDefinedFields["USER_AnzahlTage"].Value),
                USER_Seminar = ConversionHelper.ToBoolean(artikelItem.UserDefinedFields["USER_Seminar"].Value),
                USER_UhrzeitBis = ConversionHelper.ToString(artikelItem.UserDefinedFields["USER_UhrzeitBis"].Value),
                USER_UhrzeitVon = ConversionHelper.ToString(artikelItem.UserDefinedFields["USER_UhrzeitVon"].Value)
            };
            seminar.SeminarterminCollection = SeminarData.GetSeminartermine(mandant, seminar.Artikelnummer);
            return seminar;
        }

        public static Seminar UpdateSeminar(Mandant mandant, Seminar seminar)
        {
            try
            {
                var artikelItem = mandant.MainDevice.Entities.Artikel.GetItem(seminar.Artikelnummer, mandant.Id);
                if (artikelItem == null)
                {
                    artikelItem = mandant.MainDevice.Entities.Artikel.CreateItem();
                    artikelItem.Mandant = mandant.Id;
                    artikelItem.Artikelnummer = seminar.Artikelnummer;
                }

                artikelItem.Bezeichnung1 = seminar.Bezeichnung1;
                artikelItem.Bezeichnung2 = seminar.Bezeichnung2;
                artikelItem.Aktiv = seminar.Aktiv;
                artikelItem.Artikelgruppe = seminar.Artikelgruppe;
                artikelItem.Dimensionstext = seminar.Dimensionstext;
                artikelItem.DimensionstextHTML = seminar.DimensionstextHTML;
                artikelItem.DimensionstextRTF = seminar.DimensionstextRTF;
                artikelItem.Langtext = seminar.Langtext;
                artikelItem.LangtextHTML = seminar.LangtextHTML;
                artikelItem.LangtextRTF = seminar.LangtextRTF;
                artikelItem.Matchcode = seminar.Matchcode;
                artikelItem.Memo = seminar.Memo;
                artikelItem.UserDefinedFields["USER_AnzahlTage"].Value = seminar.USER_AnzahlTage;
                artikelItem.UserDefinedFields["USER_Seminar"].Value = ConversionHelper.ToDBBoolean(seminar.USER_Seminar);
                artikelItem.UserDefinedFields["USER_UhrzeitBis"].Value = seminar.USER_UhrzeitBis;
                artikelItem.UserDefinedFields["USER_UhrzeitVon"].Value = seminar.USER_UhrzeitVon;

                mandant.MainDevice.GenericConnection.BeginTransaction();
                seminar.SeminarterminCollection.ForEach(t => SeminarData.UpdateSeminarterminTeilnehmer(mandant, t.SeminarterminID));
                artikelItem.Save();
                mandant.MainDevice.GenericConnection.CommitTransaction();
                return seminar;
            }
            catch (Exception)
            {
                mandant.MainDevice.GenericConnection.RollbackTransaction();
                throw;
            }
        }

        public static List<Seminar> GetSeminare(Mandant mandant)
        {
            var list = new List<Seminar>();
            var qry = $"SELECT Artikelnummer FROM KHKArtikel WHERE Mandant={mandant.Id} AND USER_Seminar=-1";
            using (var reader = mandant.MainDevice.GenericConnection.ExecuteReader(qry))
            {
                while (reader.Read())
                {
                    list.Add(SeminarData.GetSeminar(mandant, reader.GetString("Artikelnummer")));
                }
            }
            return list;
        }

        /// <summary>
        /// Aktualisiert die Teilnehmer zu einem Termin
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="seminarterminID"></param>
        public static void UpdateSeminarterminTeilnehmer(Mandant mandant, string seminarterminID)
        {
            var seminartermin = SeminarData.GetSeminartermin(mandant, seminarterminID);
            var countTeilnehmer = mandant.MainDevice.Lookup.RowCount("PSDSeminarbuchungen", $"Mandant={mandant.Id} AND SeminarterminID={SqlStrings.ToSqlString(seminarterminID)}");
            seminartermin.AnzahlTeilnehmer = ConversionHelper.ToInt16(countTeilnehmer);
            SeminarData.UpdateOrCreateSeminartermin(mandant, seminartermin);
        }

        #endregion Seminar-/Seminartermine

        #region Kunden

        /// <summary>
        /// Liefert eine Liste von Kunden zu einem Matchcode Filter
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static List<Kunde> GetKunden(Mandant mandant, string filter)
        {
            var kunden = new List<Kunde>();
            var parameterList = new QueryParameterList();
            parameterList.AddClauseParameter("Mandant", mandant.Id);

            if (!filter.StartsWith("%"))
            {
                filter = "%" + filter;
            }

            if (!filter.EndsWith("%"))
            {
                filter = filter + "%";
            }

            parameterList.AddClauseParameter("Matchcode", filter, ClauseParameterComparisonType.Like);
            parameterList.AddClauseParameter("KtoArt", "D");

            var kontokorrentSet = mandant.MainDevice.Entities.Kontokorrent.GetList(parameterList);

            foreach (var k in kontokorrentSet)
            {
                kunden.Add(SeminarData.GetKunde(mandant, k.Kto));
            }

            return kunden;
        }

        /// <summary>
        /// Liefert ein Kunden-Objekt
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="kto"></param>
        /// <returns></returns>
        public static Kunde GetKunde(Mandant mandant, string kto)
        {
            var kontoItem = mandant.MainDevice.Entities.Kontokorrent.GetItem(kto, mandant.Id);
            if (kontoItem == null)
            {
                throw new RecordNotFoundException("Kontokorrent", kto);
            }

            var adressItem = mandant.MainDevice.Entities.Adressen.GetItem(kontoItem.AdresseValue, kontoItem.Mandant);

            return new Kunde()
            {
                Adresse = kontoItem.AdresseValue,
                Kundennummer = kontoItem.Kto,
                Land = adressItem.LieferLand,
                Matchcode = kontoItem.Matchcode,
                Name1 = adressItem.Name1,
                Name2 = adressItem.Name2,
                Ort = adressItem.LieferOrt,
                PLZ = adressItem.LieferPLZ,
                Strasse = adressItem.LieferStrasse,
                Zusatz = adressItem.LieferZusatz
            };
        }

        /// <summary>
        /// Liefert den Ansprechpartner zu einer Adresse und Email
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="adresse"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static AnsprechpartnerItem GetAnsprechpartner(Mandant mandant, int adresse, string email)
        {
            var queryParameterList = new Sagede.OfficeLine.Data.QueryParameterList();
            queryParameterList.AddClauseParameter("Mandant", mandant.Id);
            queryParameterList.AddClauseParameter("Adresse", adresse);
            queryParameterList.AddClauseParameter("EMail", email);

            var ansprechpartnerItem = mandant.MainDevice.Entities.Ansprechpartner.GetItem(queryParameterList);

            if (ansprechpartnerItem != null)
            {
                return ansprechpartnerItem;
            }
            else
            {
                throw new RecordNotFoundException("Ansprechpartner", $"{adresse}/{email}");
            }

        }

        /// <summary>
        /// Liefert den Ansprechpartner zur Nummer
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="nummer"></param>
        /// <returns></returns>
        public static AnsprechpartnerItem GetAnsprechpartner(Mandant mandant, int nummer)
        {
            var ansprechpartnerItem = mandant.MainDevice.Entities.Ansprechpartner.GetItem(nummer, mandant.Id);
            if (ansprechpartnerItem == null)
            {
                throw new RecordNotFoundException("Ansprechpartner", nummer.ToString());
            }
            return ansprechpartnerItem;

        }

        /// <summary>
        /// Liefert die Ansprechpartner zu einer Adresse
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="adresse"></param>
        /// <returns></returns>
        public static AnsprechpartnerSet GetAnsprechpartnerList(Mandant mandant, int adresse)
        {  
            return mandant.MainDevice.Entities.Ansprechpartner.GetList(adresse, mandant.Id, true);
        }

        /// <summary>
        /// Aktualisiert bzw. legt einen neuen Ansprechpartner an
        /// </summary>
        /// <param name="mandant"></param>
        /// <param name="ansprechpartner"></param>
        /// <returns></returns>
        public static AnsprechpartnerItem UpdateAnsprechpartner(Mandant mandant, AnsprechpartnerItem ansprechpartner)
        {
            AnsprechpartnerItem item = null;
            if (SeminarData.AnsprechpartnerExists(mandant, ansprechpartner.Adresse, ansprechpartner.EMail))
            {
                var parameterList = new QueryParameterList();
                parameterList.AddClauseParameter("Mandant", mandant.Id);
                parameterList.AddClauseParameter("Adresse", ansprechpartner.Adresse);
                parameterList.AddClauseParameter("EMail", ansprechpartner.EMail);
                item = mandant.MainDevice.Entities.Ansprechpartner.GetItem(parameterList);
            }
            else
            {
                item = mandant.MainDevice.Entities.Ansprechpartner.CreateItem();
                item.Mandant = mandant.Id;
            }

            item.Abteilung = ansprechpartner.Abteilung;
            item.Adresse = ansprechpartner.Adresse;
            item.Anrede = ansprechpartner.Anrede;
            item.Ansprechpartner = ansprechpartner.Ansprechpartner;
            item.Autotelefon = ansprechpartner.Autotelefon;
            item.Briefanrede = ansprechpartner.Briefanrede;
            item.EMail = ansprechpartner.EMail;
            item.Geburtsdatum = ansprechpartner.Geburtsdatum;
            item.Gruppe = ansprechpartner.Gruppe;
            item.Memo = ansprechpartner.Memo;
            item.Mobilfunk = ansprechpartner.Mobilfunk;
            item.Nachname = ansprechpartner.Nachname;
            item.Position = ansprechpartner.Position;
            item.Telefax = ansprechpartner.Telefax;
            item.Telefon = ansprechpartner.Telefon;
            item.TelefonPrivat = ansprechpartner.TelefonPrivat;
            item.Titel = ansprechpartner.Titel;
            item.Transferadresse = ansprechpartner.Transferadresse;
            item.Vorname = ansprechpartner.Vorname;
            item.ZuHaendenText = ansprechpartner.ZuHaendenText;
            ansprechpartner.UserDefinedFields.ToList().ForEach(u => item.UserDefinedFields[u.Name].Value = u.Value);
            item.Save();
            return item;
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