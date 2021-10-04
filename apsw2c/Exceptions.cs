using System;

namespace apsw2c
{
    public class Exceptions
    {
        public class InvalidRecordLenEqException : Exception
            {
                public InvalidRecordLenEqException(){}
        
                public InvalidRecordLenEqException(int recordLen)
                    : base(String.Format("Expected Length of Field = {0}", recordLen))
                {
                    
                }
            }
    }
}