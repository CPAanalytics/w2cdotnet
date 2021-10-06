using System;

namespace w2cdotnet
{
    public class Exceptions
    {
       //Base field exceptions
        public class InvalidRecordLenEqException : Exception
            {
                public InvalidRecordLenEqException(){}
        
                public InvalidRecordLenEqException(int recordLen)
                    : base($"Expected Length of Field = {recordLen} Field Size")
                {
                    
                }
            }

        public class InvalidRecordLenException : Exception
        {
            public InvalidRecordLenException(){}
            public InvalidRecordLenException(int recordLen)
                : base($"Expected Length of Field >= {recordLen}")
            {
                    
            }
        }
        public class RequiredFieldException : Exception
        {
            public RequiredFieldException(string fieldname):base($"Attempted access to null required field {fieldname}"){}
            
        }
        //Specialized Field Exceptions
        public class InvalidEin : Exception
        {
            public InvalidEin(){}
            public InvalidEin(int? ein)
                : base($"Invalid EIN {ein} check length and prefix.")
            {
                    
            }
        }
    }
}