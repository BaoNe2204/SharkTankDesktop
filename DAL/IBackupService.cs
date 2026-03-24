using System;
using System.Threading.Tasks;

namespace SharkTank.DAL
{
    public interface IBackupService
    {
        Task<bool> CreateBackupAsync(string folder, string fileName, string dataType, Action<int, string> progressCallback);
        Task<bool> RestoreBackupAsync(string filePath, bool overwrite, Action<int, string> progressCallback);
    }
}
