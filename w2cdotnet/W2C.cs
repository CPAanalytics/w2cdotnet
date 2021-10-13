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
        

        protected abstract class Fields
        {
            public string FieldName { get; protected init; }
            public int FieldStart { get; protected init; }
            public int FieldLength { get; protected init; }
            public bool RequiredField { get; protected init; }
            protected string? _fieldFormat = default;

            public virtual string? FieldFormatted
            {
                get
                {
                    if (_fieldFormat == default && RequiredField)
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

           public Fields(string name, int recordStart, int recordLength, bool requiredField)
            {
                FieldName = name;
                FieldStart = recordStart;
                FieldLength = recordLength;
                RequiredField = requiredField;
            }


           public virtual void SetFieldValue(string? stringField)
           {
           }

           public virtual void SetFieldValue(int ein)
           {
           }

           public virtual void SetFieldValue(decimal moneyField)
           {
           }

           public virtual decimal GetFieldValue()
           {
               return 0;
           }
        }

        protected class EinFields:Fields
        {
            private int _fieldValue;
            public EinFields(string name, int recordStart, int recordLength, bool requiredField) : 
                base(name, recordStart, recordLength, requiredField)
            {
            }

            private static bool EinValidator(int ein)
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
            
            public override void SetFieldValue(int fieldValue)
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
            
            
        }

        protected class StringFields:Fields
        {

            public StringFields(string name, int recordStart, int recordLength, bool requiredField) : base(name, recordStart, recordLength, requiredField)
            {
  
            }

            private string? _fieldValue;
            public override void SetFieldValue(string? fieldValue)
            {
                
                if (fieldValue == default && RequiredField)
                {
                    throw new Exceptions.RequiredFieldException(FieldName);
                }
                else if (fieldValue != null && fieldValue.Length > FieldLength)
                {
                    throw new Exceptions.InvalidRecordLenException(FieldLength, FieldName);
                }

                _fieldValue = fieldValue;
                FieldFormatted = fieldValue;
               
            }

        
            public override string? FieldFormatted
            {
                get
                {
                    if (_fieldValue == default && RequiredField)
                    {
                        throw new Exceptions.RequiredFieldException(FieldName);
                    }
                    else if(_fieldValue == default && !RequiredField)
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
                    _fieldFormat = string.Format("{0,-"+FieldLength.ToString()+"}",value);
                }
            }
            
        }

        protected class MoneyFields:Fields
        {
            private decimal _fieldValue;
            public MoneyFields(string name, int recordStart, int recordLength, bool requiredField) : 
                base(name, recordStart, recordLength, requiredField)
            {
            }

            //TODO implement MoneyField getter and setter
            public override void SetFieldValue(decimal fieldValue)
            {
                throw new NotImplementedException();
            }

            public override decimal GetFieldValue()
            {
                return _fieldValue;
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