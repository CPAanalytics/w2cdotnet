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

        protected interface IField<out T>
        {
            string Name { get; }
            int RecordStart { get; }
            int RecordLength { get;}
            bool RequiredField { get;}
            string FieldFormatted { get; }
            T? FieldValue { get; }
            
            
            
        }
        protected class Field<T>:IField<T>
        {
            public string Name { get; protected init; }
            public int RecordStart { get; protected init; }
            public int RecordLength { get; protected init; }
            public bool RequiredField { get; protected init; }
            public T? _fieldValue;
            private string _fieldFormat = String.Empty;

            public virtual string FieldFormatted
            {
                get
                {
                    if (_fieldFormat == String.Empty && RequiredField)
                    {
                        throw new Exceptions.RequiredFieldException(Name);
                    }
                    else
                    {
                        return _fieldFormat;
                    }
                }
                private set => _fieldFormat = value;
            }

            public T? FieldValue
            {
                get
                {
                    if (RequiredField && _fieldValue is default(T?))
                    {
                        throw new Exceptions.RequiredFieldException(Name);
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

        }

        protected class EinField:Field<int>
        {
            
            private static bool EinValidator(int? ein)
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
            

            private new int FieldValue
            {
                get
                {
                    if (RequiredField && _fieldValue)
                    {
                        return _fieldValue;
                    }
                    else
                    {
                        throw new W2Exceptions.RequiredFieldException();
                    }
                }
                set
                {
                    if (EinValidator(value))
                    {
                        _fieldValue = value;
                    }
                    else
                    {
                        throw new W2Exceptions.InvalidEin(value);
                    }
                }
            }

            public EinField(string name, int recordStart, int recordLength, bool requiredField, int fieldValue) : 
                base(name,recordStart, recordLength, requiredField, fieldValue)
            {
                Name = name;
                RecordStart = recordStart;
                RecordLength = recordLength;
                RequiredField = requiredField;
                FieldValue = fieldValue;
            }
        }
        protected abstract void WriteLine();
        
    }

    
    
}