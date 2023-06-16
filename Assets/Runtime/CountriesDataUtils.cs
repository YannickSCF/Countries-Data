/**
 * Author:      Yannick Santa Cruz Feuillias
 * Created:     15/06/2023
 **/

/// Dependencies
using System.Collections.Generic;
using UnityEngine;

namespace YannickSCF.CountriesData {
    public static class CountriesDataUtils {

        private static CountriesDataList _data;

        private static void LoadCountriesData() {
            _data = Resources.Load("Countries Data/Countries Data") as CountriesDataList;
        }

        public static Sprite GetFlagByCode(string flagCode) {
            if (_data == null) LoadCountriesData();
            return _data.GetFlagByCode(flagCode);
        }

        public static string GetNameByCode(string code, SystemLanguage? language) {
            if (_data == null) LoadCountriesData();
            return _data.GetNameByCode(code, language);
        }

        public static string GetCodeByName(string name) {
            if (_data == null) LoadCountriesData();
            return _data.GetCodeByName(name);
        }

        public static bool IsCountryCodeInList(string countryCode) {
            if (_data == null) LoadCountriesData();
            return _data.IsCountryCodeInList(countryCode);
        }

        public static bool IsCountryNameInList(string countryName) {
            if (_data == null) LoadCountriesData();
            return _data.IsCountryNameInList(countryName);
        }

        public static Dictionary<string, Sprite> SearchCountriesByCode(string code) {
            if (_data == null) LoadCountriesData();
            return _data.SearchCountriesByCode(code);
        }

        public static Dictionary<string, Sprite> SearchCountriesByPartialName(string partialName) {
            if (_data == null) LoadCountriesData();
            return _data.SearchCountriesByPartialName(partialName);
        }
    }
}