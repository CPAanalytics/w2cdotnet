#nullable enable
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace w2cdotnet
{
    public abstract class W2C
    {
        public const int RecordLength = 1024;
        public const int MaxEmployeeRecords = 25000;
        public const int MaxEmployerRecords = 500000;

        protected interface IField
        {
            string Name { get; }
            int RecordStart { get; }
            int RecordLength { get;}
            bool RequiredField { get;}
            string FieldFormatted { get; }
            




        }
        protected class Field<T>:IField
        {
            public string Name { get; protected init; }
            public int RecordStart { get; protected init; }
            public int RecordLength { get; protected init; }
            public bool RequiredField { get; protected init; }
            protected T? _fieldValue = default;
            private string _fieldFormat = String.Empty;

            public virtual string FieldFormatted
            {
                get
                {
                    if (_fieldFormat == string.Empty && RequiredField)
                    {
                        throw new Exceptions.RequiredFieldException(Name);
                    }
                    else
                    {
                        return _fieldFormat;
                    }
                }
                protected set => _fieldFormat = value;
            }

            public T? FieldValue
            {
                get
                {
                    if (RequiredField && object.Equals(_fieldValue, default))
                    {
                        throw new Exceptions.RequiredFieldException(Name);
                    }
                    else
                    {
                        return _fieldValue;
                    }
                }
                set
                { 
                    string strValue = value.ToString();
                    if (strValue.Length <= RecordLength)
                    {
                        _fieldValue = value;
                    }
                    else
                    {
                        throw new Exceptions.InvalidRecordLenException(RecordLength);
                    }

                    FieldFormatted = strValue;
                }
            }

            
           
            
            public Field(string name, int recordStart, int recordLength, bool requiredField, T fieldValue)
            {
                Name = name;
                RecordStart = recordStart;
                RecordLength = recordLength;
                RequiredField = requiredField;
                FieldValue = fieldValue;
            }

            protected Field(string name, int recordStart, int recordLength, int requiredField)
            {
              
            }
        }

        protected class EinField:Field<int>
        {
            private static bool EinValidator(int? ein)
            {
                List<string> invalidPrefix = new List<string>() 
                    {"07", "08", "09", "17", "18", "19", "28", "29", "49", "69", "70", "78", "79","89"};
                var strEin = ein.ToString();
                if (strEin is not {Length: 9})
                {
                    return false;
                }
                return !invalidPrefix.Contains(strEin[..2]);
            }
            

            private new int FieldValue
            {
                get
                {
                    if (RequiredField && object.Equals(_fieldValue,default))
                    {
                        
                        throw new Exceptions.RequiredFieldException(Name);
                    }
                    else
                    {
                        return _fieldValue;
                    }
                }
                set
                {
                    if (EinValidator(value))
                    {
                        _fieldValue = value;
                        FieldFormatted = value.ToString();
                    }
                    else
                    {
                        throw new Exceptions.InvalidEin(value);
                    }
                }
            }

            public EinField(string name, int recordStart, int recordLength, int fieldValue) : 
                base(name,recordStart, recordLength, fieldValue)
            {
                Name = name;
                RecordStart = recordStart;
                RecordLength = recordLength;
                FieldValue = fieldValue;
                RequiredField = true;
            }
        }

        public abstract void WriteLine();
        
    }

    
    
}