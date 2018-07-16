using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSDev.OfficeLine.Academy.BusinessLogic;
using PSDev.OfficeLine.Academy.DataAccess;
using Sagede.Core.Diagnostics;
using Sagede.Core.Tools;
using Sagede.OfficeLine.Engine;
using Sagede.OfficeLine.Shared;
using Sagede.Core.Tools;
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
            _mandant = Sagede.OfficeLine.Engine.ApplicationEngine.CreateSession(DATABASE, Sagede.OfficeLine.Shared.ApplicationToken.AddOn,
                null, new NamePasswordCredential(USERNAME, PASSWORD)).CreateMandant(MANDANT);

        }

        [TestMethod]
        public void Test_InitMandant()
        {
            Assert.IsNotNull(_mandant);
            Assert.IsTrue(_mandant.Id == MANDANT);
        }

    }
}