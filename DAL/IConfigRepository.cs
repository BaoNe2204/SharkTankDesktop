using System.Collections.Generic;
using SharkTank.Core.Models;

namespace SharkTank.DAL
{
    public interface ICompanyRepository
    {
        Company GetCurrent();
        void Save(Company company);
    }

    public interface ISystemConfigRepository
    {
        SystemConfig GetByKey(string key);
        IEnumerable<SystemConfig> GetByGroup(string group);
        IEnumerable<SystemConfig> GetAll();
        void Save(SystemConfig config);
        void SaveBatch(IEnumerable<SystemConfig> configs);
    }
}
