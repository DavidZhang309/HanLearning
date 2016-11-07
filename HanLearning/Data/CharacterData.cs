using System;
using System.Collections.Generic;
using System.Linq;

namespace HanLearning.Data
{
    public class CharacterReading
    {
        public string Culture { get; private set; }
        public string System { get; private set; }
        public string Source { get; private set; }

        public string Reading { get; private set; }

        public CharacterReading(string culture, string system, string source, string reading)
        {
            Culture = culture;
            System = system;
            Source = source;
            Reading = reading;
        }

    }

    public class CharacterDefinition
    {
        public string Culture { get; private set; }
        public string Source { get; private set; }

        public string Definition { get; private set; }
        

        public CharacterDefinition(string culture, string source, string definition)
        {
            Culture = culture;
            Source = source;
            Definition = definition;
        }
    }

    public enum VariantType { None = 0, Simplified = 1, Traditional = 2 }

    public class CharacterData
    {
        public int UTFCode { get; private set; }
        public int? HKGradeLevel { get; private set; }
        public LearningStatus LearningStatus { get; set; }
        public List<CharacterReading> Readings { get; private set; }
        public List<CharacterDefinition> Definitions { get; private set; }

        public VariantType VariantType { get; set; }
        public List<int> Variants { get; private set; }

        public CharacterData(int utfCode)
        {
            UTFCode = utfCode;

            Readings = new List<CharacterReading>();
            Definitions = new List<CharacterDefinition>();
            Variants = new List<int>();
        }
    }
    
    public enum LearningStatus{
        DoNotKnow = 0,
        Learning = 1,
        Failed = 2,
        Learned = 3
    }

    public class WordGroupData
    {
        public int GroupID { get; private set; }
        public string GroupName { get; private set; }
        public string Content { get; private set; }

        public WordGroupData(int id, string name, string content)
        {
            GroupID = id;
            GroupName = name;
            Content = content;
        }
    }
}