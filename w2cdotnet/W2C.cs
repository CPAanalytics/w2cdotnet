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

        protected interface IField
        {
            string FieldName { get; }
            int FieldStart { get; }
            int FieldLength { get;}
            bool RequiredField { get;}
            string FieldFormatted { get; }

            void SetFieldValue<T>(T fieldValue);
            T GetFieldValue<T>();
            
        }

        protected abstract class Field<T>:IField
        {
            public string FieldName { get; protected init; }
            public int FieldStart { get; protected init; }
            public int FieldLength { get; protected init; }
            public bool RequiredField { get; protected init; }
            protected T? _fieldValue;
            protected string _fieldFormat = String.Empty;
            public abstract void SetFieldValue<T>(T fieldValue);
            public abstract T GetFieldValue<T>();

            

            public virtual string FieldFormatted
            {
                get
                {
                    if (_fieldFormat == string.Empty && RequiredField)
                    {
                        throw new Exceptions.RequiredFieldException(FieldName);
                    }
                    else
                    {
                        return _fieldFormat;
                    }
                }
                protected set => _fieldFormat = value;
            }

           protected Field(string name, int recordStart, int recordLength, bool requiredField)
            {
                FieldName = name;
                FieldStart = recordStart;
                FieldLength = recordLength;
                RequiredField = requiredField;
            }
           
        }

        protected class EinField:Field<int?>
        {
            private Field<int?> _fieldImplementation;

            public EinField(string name, int recordStart, int recordLength, bool requiredField) : 
                base(name, recordStart, recordLength, requiredField)
            {
            }

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
            
            public override void SetFieldValue<T>(T fieldValue)
            {
                if (EinValidator(fieldValue))
                {
                    _fieldValue = fieldValue;
                    FieldFormatted = fieldValue.ToString();
                }
                else
                {
                    throw new Exceptions.InvalidEin(fieldValue);
                }
            }

            public int? GetFieldValue()
            {
                if (RequiredField && object.Equals(_fieldValue, default))
                {
                    throw new Exceptions.RequiredFieldException(FieldName);
                }
                else
                {
                    return _fieldValue;
                }
            }
            
        }

        protected class StringField:Field<string?>
        {
            public StringField(string name, int recordStart, int recordLength, bool requiredField) : base(name, recordStart, recordLength, requiredField)
            {
  
            }
            
            public void SetFieldValue(string? fieldValue)
            {
                if (fieldValue.Length<=FieldLength)
                {
                    _fieldValue = fieldValue;
                    FieldFormatted = fieldValue;
                }
                else
                {
                    throw new Exceptions.InvalidRecordLenException(FieldLength, FieldName);
                }
            }

            public string? GetFieldValue()
            {
                if (RequiredField && Equals(_fieldValue, default))
                {
                    throw new Exceptions.RequiredFieldException(FieldName);
                }
                else
                {
                    return _fieldValue;
                }
            }

            public override string FieldFormatted
            {
                get
                {
                    if (GetFieldValue() == default && RequiredField)
                    {
                        throw new Exceptions.RequiredFieldException(FieldName);
                    }
                    else if(GetFieldValue() == default && !RequiredField)
                    {
                        return new string(' ', FieldLength);
                    }
                    else
                    {
                        return _fieldFormat;
                    }
                }
                //TODO implement FieldFormatted setter
                protected set
                {
                    _fieldFormat = string.Format("{0,-"+FieldLength.ToString()+"}",GetFieldValue());
                }
            }
            
        }

        protected class MoneyField:Field<decimal>
        {
            public MoneyField(string name, int recordStart, int recordLength, bool requiredField) : 
                base(name, recordStart, recordLength, requiredField)
            {
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