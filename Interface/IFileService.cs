using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public interface IFileService
    {
        List<T> Open<T>(string filename);
        void Save<T>(string filename, List<T> accountsList);
    }
}
