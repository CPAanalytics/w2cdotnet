#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace w2cdotnet
{
    public class Submitter : W2C

        //RCA Submitter Record
    {
        //5.5 Submitter Record
        

        private Dictionary<string, Fields> SubmitterFields = new Dictionary<string, Fields>
        {
            {"recordIdentifier",new StringFields(name: "_recordIdentifier", recordStart: 1, recordLength: 3, requiredField: true)},
            {"submitterEin",new EinFields("submitterEin", 4, 9, true)},
            {"userId",new StringFields("userId",13,8,false)},
            {"softwareVendor",new StringFields("softwareVendor",21,4,false)}
        };
        
        
      
     

        public Submitter(
        int submitterEin, 
        string? userId = default, 
        string? softwareVendor = default)
    {
        SubmitterFields["recordIdentifier"].SetFieldValue("RCA");
        SubmitterFields["submitterEin"].SetFieldValue(submitterEin);
        SubmitterFields["userId"].SetFieldValue(userId);
        SubmitterFields["softwareVendor"].SetFieldValue(softwareVendor);


    }
    //TODO complete WriteLine Method
    public override void WriteLine()
    {
        foreach (KeyValuePair<string, Fields> keyValuePair in SubmitterFields)
        {
            Console.Write(keyValuePair.Value.FieldFormatted);
            
        }
        Console.WriteLine();
        
    }
    }
}
