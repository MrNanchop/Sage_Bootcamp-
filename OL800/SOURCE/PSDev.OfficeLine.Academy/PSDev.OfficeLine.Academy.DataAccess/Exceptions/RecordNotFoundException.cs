using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSDev.OfficeLine.Academy.DataAccess
{
    [Serializable]
    public class RecordNotFoundException : Exception
    {
        /// <summary>
        /// Type (z.B. Seminar, Seminartermin oder Artikel)
        /// </summary>
        private string _type;

        /// <summary>
        /// Key / Schlüssel nach dem gesucht wurde
        /// </summary>
        private string _keyValue;

        public RecordNotFoundException(string type, string keyValue)
            : base(string.Format(Properties.Resources.RecordNotFoundException, type, keyValue))
        {
            _type = type;
            _keyValue = keyValue;
        }

        public RecordNotFoundException(string type, string keyValue, Exception inner) : base(string.Format(Properties.Resources.RecordNotFoundException, type, keyValue), inner)
        {
            _type = type;
            _keyValue = keyValue;
        }

        protected RecordNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}