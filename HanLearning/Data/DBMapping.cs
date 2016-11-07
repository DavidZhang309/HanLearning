using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CoreFramework.Extensions;

namespace HanLearning.Data
{
    /// <summary>
    /// Maps data from spCharacterLookup to defined entities
    /// </summary>
    public class CharacterLookupMapping
    {
        public Dictionary<int, CharacterData> Characters { get; private set; } = new Dictionary<int, CharacterData>();

        public CharacterLookupMapping(HanDatabase db, IEnumerable<int> utfCodes, string culture, int userID)
        {
            DataTable utfCodeTable = new DataTable();
            utfCodeTable.Columns.Add("UTFCode");

            foreach(int utf in utfCodes)
            {
                DataRow row = utfCodeTable.NewRow();
                row["UTFCode"] = utf;

                utfCodeTable.Rows.Add(row);
            }
            SqlParameter[] parameters =
            {
                new SqlParameter("UTFCodes", utfCodeTable) { TypeName = "tvpUTFCodes" },
                new SqlParameter("Culture", culture),
                new SqlParameter("UserID", userID)
            };

            DataSet result = db.ExecuteStoredProcedure("spCharacterLookup", parameters);
            ParseData(result.Tables[0], result.Tables[1], result.Tables[2], result.Tables[3]);
            
        }

        public CharacterLookupMapping(DataTable readingData, DataTable definitionData, DataTable characterData, DataTable variantData)
        {
            ParseData(readingData, definitionData, characterData, variantData);
        }

        protected CharacterData GetCharacter(string utfData)
        {
            int utf = -1;
            if (!int.TryParse(utfData, out utf))
            {
                return null;
            }
            if (!Characters.ContainsKey(utf))
            {
                Characters.Add(utf, new CharacterData(utf));
            }
            return Characters[utf];
        }

        protected void ParseData(DataTable readingData, DataTable definitionData, DataTable characterData, DataTable variantData)
        {
            //insert reading data
            foreach (DataRow row in readingData.Rows)
            {
                CharacterData character = GetCharacter(row["UTFCode"].ForceToString());
                if (character == null) { continue; }

                CharacterReading reading = new CharacterReading(
                    row["ReadingCulture"].ForceToString(),
                    row["ReadingSystem"].ForceToString(),
                    row["ReadingSource"].ForceToString(),
                    row["Reading"].ForceToString()
                );

                character.Readings.Add(reading);
            }

            //insert definition data
            foreach (DataRow row in definitionData.Rows)
            {
                CharacterData character = GetCharacter(row["UTFCode"].ForceToString());
                if (character == null) { continue; }

                CharacterDefinition definition = new CharacterDefinition(
                    row["DefinitionCulture"].ForceToString(),
                    row["DefinitionSource"].ForceToString(),
                    row["Definition"].ForceToString()
                );

                character.Definitions.Add(definition);
            }

            foreach(DataRow row in characterData.Rows)
            {
                //Character data
                CharacterData character = GetCharacter(row["UTFCode"].ForceToString());
                if (character == null) { continue; }
                


                //Optional learning data
                if (characterData.Columns.Contains("LearningStatus"))
                {
                    int learningNum = -1;
                    if (!int.TryParse(row["LearningStatus"].ForceToString(), out learningNum))
                    {
                        continue;
                    }
                    character.LearningStatus = (LearningStatus)learningNum;
                }
            }

            foreach(DataRow row in variantData.Rows)
            {
                CharacterData character = GetCharacter(row["UTFCode"].ForceToString());
                if (character == null) { continue; }

                int varType = 0;
                if (int.TryParse(row["VariantType"].ForceToString(), out varType))
                {
                    character.VariantType = (VariantType)varType;
                }

                int code = -1;
                if (int.TryParse(row["ResultUTFCode"].ForceToString(), out code))
                {
                    character.Variants.Add(code);
                }
            }
        }
    }

    public class LearningCharacterMapping
    {
        public CharacterLookupMapping CharacterData { get; private set; }

        public LearningCharacterMapping(HanDatabase db, int userID, string culture)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("UserID", userID),
                new SqlParameter("Culture", culture)
            };

            DataSet result = db.ExecuteStoredProcedure("spLearningCharacterData", parameters);

            CharacterData = new CharacterLookupMapping(result.Tables[0], result.Tables[1], result.Tables[2], result.Tables[3]);
        }
    }

    public class WordGroupMapping
    {
        public List<WordGroupData> Groups { get; private set; }
        public WordGroupMapping(HanDatabase db)
        {
            Groups = new List<WordGroupData>();
            DataTable query = db.ExecuteQuery("SELECT GroupID, GroupName, GroupContent FROM tblWordGroups ORDER BY GroupName");

            foreach (DataRow row in query.Rows)
            {
                int id = -1;
                if (!int.TryParse(row["GroupID"].ForceToString(), out id))
                {
                    continue;
                }

                Groups.Add(new WordGroupData(
                    id,
                    row["GroupName"].ForceToString(),
                    row["GroupContent"].ForceToString()
                ));
            }
        }
    }
}