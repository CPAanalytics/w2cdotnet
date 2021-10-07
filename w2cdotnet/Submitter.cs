﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace w2cdotnet
{
    public class Submitter : W2C

    //RCA Submitter Record
    {
    //5.5 Submitter Record

    private List<IField> FieldList = new List<IField>(); 
    private static Field<string> _recordIdentifier =
        new Field<string>(name:"RecordIdentifier",recordStart: 1, recordLength: 3, requiredField: true, "RCA");
    private EinField _submittersEin;
   

    public Submitter(int submitterEin)
    {
        //TODO Complete Submitter class constructor
        FieldList.Add(_recordIdentifier);
        FieldList.Add(new EinField(name: "submitterEin",recordStart:4,recordLength:9,submitterEin));
        
        
    }
    //TODO complete WriteLine Method
    public override void WriteLine()
    {
        int LineLength = 1;
        foreach (IField field in FieldList)
        {
            if (LineLength != field.RecordStart)
            {
                throw new Exceptions.PositionCheckError(field.Name);
            }
            Console.Write(field.FieldFormatted);
            LineLength += field.RecordLength;
        }
    }
    }
}
