using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSDev.OfficeLine.Academy.BusinessLogic
{
    [Serializable]
    public class BuchungValidationException : Exception
    {
        public BuchungValidationException(string validationReason) : base(string.Format(Properties.Resources.BuchungValidationException, validationReason))
        {
        }

        public BuchungValidationException(string validationReason, Exception inner) : base(string.Format(Properties.Resources.BuchungValidationException, validationReason), inner)
        {
        }

        protected BuchungValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}