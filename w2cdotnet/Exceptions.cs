using System;

namespace w2cdotnet
{
    public class Exceptions
    {
        public class InvalidRecordLenEqException : Exception
            {
                public InvalidRecordLenEqException(){}
        
                public InvalidRecordLenEqException(int recordLen)
                    : base(String.Format("Expected Length of Field = {0} Field Size", recordLen))
                {
                    
                }
            }

        public class InvalidRecordLenException : Exception
        {
            public InvalidRecordLenException(){}
            public InvalidRecordLenException(int recordLen)
                : base(String.Format("Expected Length of Field >= {0}", recordLen))
            {
                    
            }
        }
        public class InvalidEin : Exception
        {
            public InvalidEin(){}
            public InvalidEin(int? ein)
                : base(String.Format("Invalid EIN {0} check length and prefix.", ein))
            {
                    
            }
        }
    }
}