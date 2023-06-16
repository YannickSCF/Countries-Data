/**
 * Author:      Yannick Santa Cruz Feuillias
 * Created:     15/06/2023
 **/

/// Dependencies
using UnityEngine;

namespace YannickSCF.CountriesData.Objects {
    [System.Serializable]
    public class CountryNameTranslations {

        [SerializeField] private string spanishName;
        [SerializeField] private string englishName;
        [SerializeField] private string italianName;
        [SerializeField] private string frenchName;

        public string GetTranslatedName(SystemLanguage language) {
            switch (language) {
                case SystemLanguage.Spanish:
                    if (string.IsNullOrEmpty(spanishName)) return englishName;
                    else return spanishName;
                case SystemLanguage.Italian:
                    if (string.IsNullOrEmpty(italianName)) return englishName;
                    else return italianName;
                case SystemLanguage.French:
                    if (string.IsNullOrEmpty(frenchName)) return englishName;
                    else return frenchName;
                case SystemLanguage.English:
                default:
                    if (string.IsNullOrEmpty(englishName)) return "ERROR";
                    else return englishName;
            }
        }
        public bool HasTranslation(SystemLanguage language) {
            switch (language) {
                case SystemLanguage.English:
                    return !string.IsNullOrEmpty(englishName);
                case SystemLanguage.French:
                    return !string.IsNullOrEmpty(frenchName);
                case SystemLanguage.Italian:
                    return !string.IsNullOrEmpty(italianName);
                case SystemLanguage.Spanish:
                    return !string.IsNullOrEmpty(spanishName);
                default:
                    return false;
            }
        }

        public bool HasCoincidence(string name) {
            bool res = false;

            res |= spanishName.Contains(name);
            res |= englishName.Contains(name);
            res |= italianName.Contains(name);
            res |= frenchName.Contains(name);

            return res;
        }
        public bool HasCoincidence(string name, SystemLanguage language) {
            switch (language) {
                case SystemLanguage.Spanish:
                    return spanishName.Contains(name);
                case SystemLanguage.Italian:
                    return italianName.Contains(name);
                case SystemLanguage.French:
                    return frenchName.Contains(name);
                case SystemLanguage.English:
                    return englishName.Contains(name);
                default:
                    return false;
            }
        }

        public bool HasPartialCoincidence(string partialName) {
            bool res = false;

            res |= spanishName.Contains(partialName);
            res |= englishName.Contains(partialName);
            res |= italianName.Contains(partialName);
            res |= frenchName.Contains(partialName);

            return res;
        }
        public bool HasPartialCoincidence(string partialName, SystemLanguage language) {
            switch (language) {
                case SystemLanguage.Spanish:
                    return spanishName.Contains(partialName);
                case SystemLanguage.Italian:
                    return italianName.Contains(partialName);
                case SystemLanguage.French:
                    return frenchName.Contains(partialName);
                case SystemLanguage.English:
                    return englishName.Contains(partialName);
                default:
                    return false;
            }
        }

        public void Mirame() {

        }
    }
}
