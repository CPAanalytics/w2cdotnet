#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;


namespace w2cdotnet
{
    public class Submitter : W2C

        //RCA Submitter Record
    {
        //5.5 Submitter Record
        

        private Dictionary<string, IField> SubmitterFields = new Dictionary<string, IField>
        {
            {"recordIdentifier",new StringField(name: "_recordIdentifier", recordStart: 1, recordLength: 3, requiredField: true)},
            {"submitterEin",new EinField("submitterEin", 4, 9, true)},
            {"userId",new StringField("userId",13,8,false)},
            {"softwareVendor",new StringField("softwareVendor",21,4,false)}
        };

        private IField testvalue = new StringField(name: "_recordIdentifier", recordStart: 1, recordLength: 3,
            requiredField: true);
    //TODO Complete Submitter class constructor
    public Submitter(
        int submitterEin, 
        string? userId = default, 
        string? softwareVendor = default)
    {
        SubmitterFields["recordIdentifier"].SetFieldValue("RCA");
        SubmitterFields["userId"].SetFieldValue(userId);
        SubmitterFields["softwareVendor"].SetFieldValue(softwareVendor);
        testvalue.SetFieldValue("RCA");
        
        

    }
    //TODO complete WriteLine Method
    public override void WriteLine()
    {
        foreach (KeyValuePair<string, IField> keyValuePair in SubmitterFields)
        {
            Console.Write(keyValuePair.Value.FieldFormatted);
        }
        
    }
    }
}
