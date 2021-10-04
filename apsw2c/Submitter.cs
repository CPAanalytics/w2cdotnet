#nullable enable
using System;

namespace apsw2c
{
    public class Submitter : W2C

    //RCA Submitter Record
    {
    //5.5 Submitter Record
    private struct RecordIdentifier
    {
        private const int RecordStart = 1;
        private const int RecordLength = 3;

        private string _fieldValue;

        public RecordIdentifier(string fieldValue)
        {
            if (fieldValue.Length != RecordLength)
            {
                throw new Exceptions.InvalidRecordLenEqException(RecordLength);
            }
            _fieldValue = fieldValue;
        }
        
    }
    private struct SubmittersEIN
    {
        private const int RecordStart = 4;
        private const int RecordLength = 9;

        private string _fieldValue;

        public SubmittersEIN(int fieldValue)
        {
            int
            if (fieldValue.Length != RecordLength)
            {
                throw new Exceptions.InvalidRecordLenEqException(RecordLength);
            }
            _fieldValue = fieldValue;
        }
        
    }
    }
}
