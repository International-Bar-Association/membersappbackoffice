using System.Collections;

namespace IBAMembersApp.Models
{
    public class ProfileAddress
    {
        public ArrayList AddressLines { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PcZip { get; set; }

        public ProfileAddress()
        {
            AddressLines = new ArrayList();
        }
    }
}