using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HanLearning.Data;

namespace HanLearning.Admin
{
    interface ICharDataImporter
    {
        /// <summary>
        /// Parses character data from file specified
        /// </summary>
        /// <param name="path">Path of file.</param>
        /// <returns>An enumerable list of character data</returns>
        IEnumerable<CharacterData> GetCharacterData(string path);
    }
}
