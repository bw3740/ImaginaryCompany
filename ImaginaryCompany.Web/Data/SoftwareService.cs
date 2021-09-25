using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ImaginaryCompany.Services;
using ImaginaryCompany.Web.Data.Models;

namespace ImaginaryCompany.Web.Data
{
    public class SoftwareService
    {
        ISoftwareManager _softwareManager;

        public SoftwareService(ISoftwareManager softwareManager)
        {
            _softwareManager = softwareManager;
        }

        public async Task<IEnumerable<Software>> GetAll()
        {
            var softwareList = _softwareManager.GetAllSoftware();
            if (softwareList == null) return null;

            var result = new List<Software>();
            result.AddRange(
                from s in softwareList
                select new Software { Name = s.Name, Version = new SoftwareVersion(s.Version) }
            );

            return result;
        }

        public async Task<PagedResults<Software>> GetFilteredSoftware(string version, int recordStart, int pageSize)
        {
            var softwareList = await GetAll();

            IEnumerable<Software> results = softwareList;

            var searchVersion = new SoftwareVersion(version);

            //check if we have a valid version we should filter by
            if (searchVersion.Major > 0 || searchVersion.Minor > 0 || searchVersion.Patch > 0)
            {
                results = softwareList.Where(s => s.Version.IsGreater(searchVersion)).ToList();
            }

            return new PagedResults<Software>(results, results.Count(), pageSize, recordStart);
        }
    }
}