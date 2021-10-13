using System.Collections.Generic;

namespace w2cdotnet
{
    public class Employer:W2C
    {
        
        public List<Employee> Employees { get; private set; }
        private string _recordId = "RE";
        private int _taxYear;
        private int _agentIndicatorCode;
        private int _agentEIN;

        private EinField _employerEinField =
            new EinField("employerEin", recordStart: 4, recordLength: 9, requiredField: true);
        

        public Employer(int employerein)
        {
            _employerEinField.SetFieldValue(employerein);

        }

        public override void WriteLine()
        {
            throw new System.NotImplementedException();
        }
    }
}