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
    }
}