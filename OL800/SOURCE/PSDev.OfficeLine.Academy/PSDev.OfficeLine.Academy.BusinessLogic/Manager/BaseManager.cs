using PSDev.OfficeLine.Academy.DataAccess;
using Sagede.OfficeLine.Engine;
using System;

namespace PSDev.OfficeLine.Academy.BusinessLogic
{
    /// <summary>
    /// Basisklasse für alle Manager-Implementierungen
    /// </summary>
    public abstract class BaseManager : IDisposable
    {
        /// <summary>
        /// Office Line Mandanten-Objekt
        /// </summary>
        public Mandant Mandant { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="mandant"></param>
        public BaseManager(Mandant mandant)
        {
            try
            {
                TraceLog.LogVerbose("Konstruktor Basisklasse aufgerufen.");
                Mandant = mandant;
                TraceLog.LogVerbose("Konstruktor Ende / Erfolg.");
            }
            catch (Exception ex)
            {
                TraceLog.LogException(ex);
                throw;
            }
        }

        /// <summary>
        /// Dispose-Methode
        /// </summary>
        public void Dispose()
        {
        }
    }
}