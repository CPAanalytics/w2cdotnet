using System.Collections.Generic;

namespace w2cdotnet
{
    public class Employer:W2C
    {
        
        public List<Employee> Employees { get; private set; }
        private EinField _employerEinField;

        public Employer(int employerein)
        {
            _employerEinField = new EinField(name: "submitterEin",recordStart:4,recordLength:9,employerein);
            
        }

        public override void WriteLine()
        {
            throw new System.NotImplementedException();
        }
    }
}