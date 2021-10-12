#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading;
using System.Xml;

namespace w2cdotnet
{
    public abstract class W2C
    {
        public const int LineLength = 1024;
        public const int MaxEmployeeRecords = 25000;
        public const int MaxEmployerRecords = 500000;

        protected interface IField<T>
        {
            string Name { get; }
            int RecordStart { get; }
            int RecordLength { get;}
            bool RequiredField { get;}
            string FieldFormatted { get; }
            T? FieldValue { get; }
            

        }

        protected abstract class Field<T>:IField<T>
        {
            public string Name { get; protected init; }
            public int RecordStart { get; protected init; }
            public int RecordLength { get; protected init; }
            public bool RequiredField { get; protected init; }
            protected T? _fieldValue = default;
            protected string _fieldFormat = String.Empty;

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
                    var strValue = value?.ToString();
                    if (strValue.Length <= RecordLength)
                    {
                        _fieldValue = value;
                    }
                    else
                    {
                        throw new Exceptions.InvalidRecordLenException(RecordLength, Name);
                    }

                    FieldFormatted = strValue;
                }
            }

            public Field(string name, int recordStart, int recordLength, bool requiredField, T? fieldValue)
            {
               
            }

            protected Field(string name, int recordStart, int recordLength, bool requiredField)
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
                    if (RequiredField && Equals(_fieldValue,default))
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

            public EinField(string name, int recordStart, int recordLength, bool requiredField) : 
                base(name, recordStart, recordLength, requiredField)
            {
                Name = name;
                RecordStart = recordStart;
                RecordLength = recordLength;
                requiredField = requiredField;
            }
        }

        protected class StringField:Field<string>
        {
            protected string _fieldFormat;
            public override string FieldFormatted
            {
                get
                {
                    if (FieldValue == default && RequiredField)
                    {
                        throw new Exceptions.RequiredFieldException(Name);
                    }
                    else if(FieldValue == default && !RequiredField)
                    {
                        return new string(' ', RecordLength);
                    }
                    else
                    {
                        return _fieldFormat;
                    }
                }
                //TODO implement FieldFormatted setter
                protected set
                {
                    _fieldFormat = string.Format("{0,-"+RecordLength.ToString()+"}",FieldValue);
                }
            }
            public StringField(string name, int recordStart, int recordLength, bool requiredField, string? fieldValue) : 
                base(name, recordStart, recordLength, requiredField, fieldValue)
            {
                Name = name;
                RecordStart = recordStart;
                RecordLength = recordLength;
                RequiredField = requiredField;
                FieldValue = fieldValue;
            }

         
        }

        protected class MoneyField:Field<float>
        {
            public MoneyField(string name, int recordStart, int recordLength, bool requiredField, float fieldValue) : 
                base(name, recordStart, recordLength, requiredField, fieldValue)
            {
                FieldValue = fieldValue;
            }
        }

        //TODO XML method;
        public W2C(Submitter submitter, int taxyear, XmlDocument xmldocument)
        {
            throw new NotImplementedException("XML not implmented");
        }

        //TODO JSON method;
        public W2C(Submitter submitter, int taxyear, JsonDocument jsondoc)
        {
            throw new NotImplementedException("JSON not implemented");
        }

        public W2C()
        {
            
        }
        
        public abstract void WriteLine();
        
        
    }
    
}