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

    
    private static StringField _recordIdentifier =
        new StringField(name:"_recordIdentifier",recordStart: 1, recordLength: 3, requiredField: true, "RCA");
    private EinField _submittersEin = new EinField("submitterEin", 4, 9, true);
    private StringField _userID;
    private StringField _softwareVendor;
    private StringField _blankOne;
   
    //TODO Complete Submitter class constructor
    public Submitter(
        int submitterEin, 
        string userId = default, 
        string softwareVendor = default)
    {
        _submittersEin.FieldValue = submitterEin;
        _userID = new StringField("userID", recordStart: 13, recordLength: 8, requiredField:false, fieldValue: userId);
        _softwareVendor = new StringField("softwareVendor", recordStart: 21, recordLength: 4, requiredField: false,
            fieldValue: softwareVendor);


    }
    //TODO complete WriteLine Method
    public override void WriteLine()
    {
        Console.WriteLine(_recordIdentifier.FieldFormatted +
                          _submittersEin.FieldFormatted+
                          _userID.FieldFormatted+
                          _softwareVendor.FieldFormatted);
        
    }
    }
}
