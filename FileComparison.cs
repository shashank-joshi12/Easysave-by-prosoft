using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Easysave_v1._0_by_prosoft.model
{
    class FileComparison : IEqualityComparer<FileInfo>
    {
        public FileComparison() { }

        public bool Equals(FileInfo f1, FileInfo f2)
        {
            return (f1.Name == f2.Name &&
                    f1.Length == f2.Length);
        }


        public int GetHashCode(FileInfo fi) // Function to retrieve the hash of files
        {
            string s = $"{fi.Name}{fi.Length}";
            return s.GetHashCode(); // Return a hash that reflects the comparison criteria.  
        }
    }
}
