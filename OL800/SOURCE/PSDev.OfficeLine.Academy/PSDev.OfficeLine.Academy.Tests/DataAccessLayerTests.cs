using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSDev.OfficeLine.Academy.BusinessLogic;
using PSDev.OfficeLine.Academy.DataAccess;
using Sagede.Core.Diagnostics;
using Sagede.Core.Tools;
using Sagede.OfficeLine.Engine;
using Sagede.OfficeLine.Shared;
using System;
using System.Linq;

namespace PSDev.OfficeLine.Academy.Tests
{
    [TestClass]
    public class DataAccessLayerTests
    {
        private Mandant _mandant;

        public const string DATABASE = "OLDemoReweAbfD";
        public const string USERNAME = "sage";
        public const string PASSWORD = "";
        public const short MANDANT = 123;

        [TestInitialize]
        public void InitMandant()
        {
            var startTimer = Sagede.Core.Diagnostics.HighResolutionTimer.Now();
            TraceLog.LogVerbose("InitMandant => Testprojekt");
            _mandant = Sagede.OfficeLine.Engine.ApplicationEngine.CreateSession(DATABASE, Sagede.OfficeLine.Shared.ApplicationToken.AddOn,
                null, new NamePasswordCredential(USERNAME, PASSWORD)).CreateMandant(MANDANT);
            TraceLog.LogTime("Dauer InitMandant => Testprojekt ", startTimer);
        }

        [TestMethod]
        public void Test_InitMandant()
        {
            Assert.IsNotNull(_mandant);
            Assert.IsTrue(_mandant.Id == MANDANT);
        }

        [TestMethod]
        public void Test_SeminarData_KontokorrentExists()
        {
            var test1 = SeminarData.KontokorrentExists(_mandant, "D100000", true);
            Assert.IsTrue(test1);

            var test2 = SeminarData.KontokorrentExists(_mandant, "Dieses kOnto gibt es nicht", true);
            Assert.IsFalse(test2);
        }

        [TestMethod]
        public void Test_SeminarData_AnsprechpartnerExists()
        {
            var test1 = SeminarData.AnsprechpartnerExists(_mandant, 43, "HerrArber@Arber-Sauerlach.de");
            Assert.IsTrue(test1);

            var test2 = SeminarData.AnsprechpartnerExists(_mandant, 999, "email@gibtesnicht.de");
            Assert.IsFalse(test2);

        }

        [ExpectedException(typeof(RecordNotFoundException))]
        [TestMethod]
        public void Test_SeminarData_GetSeminarbuchung_And_ExpectException()
        {
            var buchung = SeminarData.GetSeminarbuchung(_mandant, -1);
            Assert.IsNull(buchung);
        }


        [ExpectedException(typeof(RecordNotFoundException))]
        [TestMethod]
        public void Test_SeminarData_CreateSeminarbuchung_Load_Delete()
        {
            var buchung = new Seminarbuchung()
            {
                Adresse = 10,
                AnsprechpartnerEmail = "test@test.de",
                AnsprechpartnerNachname = "Test",
                Ansprechpartnernummer = 1,
                AnsprechpartnerVorname = "Vorname",
                BelID = 0,
                BelPosID = 0,
                BuchungID = 0,
                EmailBestaetigungGesendet = false,
                Konto = "D100000",
                KontoMatchcode = "Testkunde",
                Mandant = _mandant.Id,
                SeminarterminID = "S100001",
                VorPosID = 0
            };

            var saveBuchung = SeminarData.UpdateOrInsertSeminarbuchung(_mandant, buchung);
            Assert.IsTrue(saveBuchung.BuchungID != 0);

            var loadedBuchung = SeminarData.GetSeminarbuchung(_mandant, saveBuchung.BuchungID);
            Assert.IsTrue(saveBuchung.SeminarterminID == loadedBuchung.SeminarterminID);
            //Assert.AreEqual(loadedBuchung, saveBuchung);
            //Assert.areEquals(saveBuchung, loadedBuchung);

            SeminarData.DeleteSeminarbuchung(_mandant, saveBuchung.BuchungID);
            loadedBuchung = SeminarData.GetSeminarbuchung(_mandant, saveBuchung.BuchungID);
        }

        [ExpectedException(typeof(RecordNotFoundException))]
        [TestMethod]
        public void Test_SeminarData_CreateSeminarbuchung_Update_Load_Delete()
        {
            var buchung = new Seminarbuchung()
            {
                Adresse = 10,
                AnsprechpartnerEmail = "test@test.de",
                AnsprechpartnerNachname = "Test",
                Ansprechpartnernummer = 1,
                AnsprechpartnerVorname = "Vorname",
                BelID = 0,
                BelPosID = 0,
                BuchungID = 0,
                EmailBestaetigungGesendet = false,
                Konto = "D100000",
                KontoMatchcode = "Testkunde",
                Mandant = _mandant.Id,
                SeminarterminID = "S100001",
                VorPosID = 0
            };

            var saveBuchung = SeminarData.UpdateOrInsertSeminarbuchung(_mandant, buchung);
            Assert.IsTrue(saveBuchung.BuchungID != 0);

            var loadedBuchung = SeminarData.GetSeminarbuchung(_mandant, saveBuchung.BuchungID);
            Assert.IsTrue(saveBuchung.SeminarterminID == loadedBuchung.SeminarterminID);
            //Assert.AreEqual(loadedBuchung, saveBuchung);

            loadedBuchung.AnsprechpartnerNachname = "AP NEU";

            saveBuchung = SeminarData.UpdateOrInsertSeminarbuchung(_mandant, loadedBuchung);
            Assert.IsTrue(saveBuchung.AnsprechpartnerNachname == loadedBuchung.AnsprechpartnerNachname);

            SeminarData.DeleteSeminarbuchung(_mandant, saveBuchung.BuchungID);
            loadedBuchung = SeminarData.GetSeminarbuchung(_mandant, saveBuchung.BuchungID);
        }

        [TestMethod]
        public void SeminarData_GetAnsprechpartner_ByNummer()
        {
            var result = SeminarData.GetAnsprechpartner(_mandant, 6);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Ansprechpartner.Contains("Arber"));
        }

        [TestMethod]
        public void SeminarbuchungManager_Create_Buchungsbeleg()
        {
            var buchung = new Seminarbuchung()
            {
                AnsprechpartnerEmail = "thomas.fritz@sage.com",
                AnsprechpartnerNachname = "Fritz",
                AnsprechpartnerVorname = "Thomas",
                Konto = "D100000",
                SeminarterminID = "S100001"
            };

            var manager = new SeminarbuchungManager(_mandant);

            buchung = manager.CreateOrUpdateBuchungsbeleg(buchung);
            Assert.IsTrue(buchung.BuchungID != 0);
            Assert.IsTrue(buchung.BelID != 0);
            Assert.IsTrue(buchung.VorPosID != 0);



        }

    }
}