#nullable enable
using System;

namespace apsw2c
{
    public class Submitter : W2C

    //RCA Submitter Record
    {
    //5.5 Submitter Record



    private EinField _submittersEin;
   

    public Submitter(int ein)
    {
        _submittersEin = new EinField(recordStart: 4, recordLength: 9, requiredField: true, ein);
    }

    protected override void WriteLine()
    {
        
    }
    }
}
