using System;
using System.Collections.Generic;
using System.Threading;

namespace apsw2c
{
    public abstract class W2C
    {
        public const int RecordLength = 1024;
        public const int MaxEmployeeRecords = 25000;
        public const int MaxEmployerRecords = 500000;
        protected class Field<T>
        {
            protected int RecordStart;
            protected int RecordLength;
            protected bool RequiredField;
            protected  T FieldValue { get; set; }

            public Field(int recordStart, int recordLength, bool requiredField, T fieldValue)
            {
                RecordStart = recordStart;
                RecordLength = recordLength;
                RequiredField = requiredField;
                FieldValue = fieldValue;
            }

        }

        protected class EinField:Field<int>
        {
            
            protected static bool EinValidator(int? ein)
            {
                List<string> invalidPrefix = new List<string>() 
                    {"07", "08", "09", "17", "18", "19", "28", "29", "49", "69", "70", "78", "79","89"};
                string str_ein = ein.ToString();
                if (str_ein.Length != 9)
                {
                    return false;
                }
                if (invalidPrefix.Contains(str_ein.Substring(0, 2)))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            private int? _fieldValue;
            protected new int? FieldValue
            {
                get => _fieldValue;
                set
                {
                    if (EinValidator(value))
                    {
                        _fieldValue = value;
                    }
                    else
                    {
                        throw new Exceptions.InvalidEin(value);
                    }
                }
            }

            public EinField(int recordStart, int recordLength, bool requiredField, int fieldValue) : 
                base(recordStart, recordLength, requiredField, fieldValue)
            {
                RecordStart = recordStart;
                RecordLength = recordLength;
                RequiredField = requiredField;
                FieldValue = fieldValue;
            }
        }
        protected abstract void WriteLine();
        
    }

    
    
}