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

    }
}