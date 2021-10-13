using System;

namespace w2cdotnet
{
    public class Exceptions
    {
       //Base field exceptions
        public class InvalidRecordLenEqException : Exception
            {
                public InvalidRecordLenEqException(int recordLen, string fieldName)
                    : base($"Expected Length of {fieldName} = {recordLen} Field Size")
                {
                    
                }
            }

        public class InvalidRecordLenException : Exception
        {
            public InvalidRecordLenException(int recordLen, string fieldName)
                : base($"Expected Length of {fieldName} >= {recordLen}")
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

            public InvalidEin(int? ein)
                : base($"Invalid EIN {ein} check length and prefix")
            {
                    
            }
        }

        public class PositionCheckError : Exception
        {
            public PositionCheckError(string fieldname):base($"Position Check Failed on {fieldname}"){}
        }
    }
}