using Common.Model;
using System.Collections.Generic;

namespace ALP.Service.Interface
{
    public interface IImportService
    {
        List<ImportedItem> ImportFromXls();
    }
}
