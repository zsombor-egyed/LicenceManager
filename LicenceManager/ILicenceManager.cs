using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenceManager
{
    /// <summary>
    /// Describes an interface for a Licence Manager
    /// </summary>
    public interface ILicenceManager
    {
        void generateProductKeys();
        Boolean validateProductKey();
    }
}
