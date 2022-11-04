using ShoeBoxScan.Models.Repositories.Group.Base.Scan;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeBoxScan.Models.Services.Group.Base.Scan
{
    public class ImportService
    {
        private ImportRepository _ImportRepository;
        public ImportService()
        {
            _ImportRepository = new ImportRepository();
        }

        public bool ImportData(ObservableCollection<ImportDataModel> dataTable) => _ImportRepository.ImportTable(dataTable);
    }
}
