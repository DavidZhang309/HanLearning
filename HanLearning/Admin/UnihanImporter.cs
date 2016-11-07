using System;
using System.Collections.Generic;
using HanLearning.Data;

namespace HanLearning.Admin
{
    public class UnihanImporter : ICharDataImporter
    {
        public IEnumerable<CharacterData> GetCharacterData(string path)
        {
            Dictionary<int, CharacterData> chars = new Dictionary<int, CharacterData>();

            

            return chars.Values;
        }
    }
}