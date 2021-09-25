using ImaginaryCompany.Services.Models;
using System.Collections.Generic;

namespace ImaginaryCompany.Services
{
    public interface ISoftwareManager
    {
        IEnumerable<Software> GetAllSoftware();
    }
}
