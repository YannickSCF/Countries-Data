/**
 * Author:      Yannick Santa Cruz Feuillias
 * Created:     15/06/2023
 **/

/// Dependencies
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
/// Custom Dependencies
using YannickSCF.CountriesData.Objects;

namespace YannickSCF.CountriesData.Editor {
    public class CustomCountryDataListAttribute : PropertyAttribute {
        public string methodName;
        public CustomCountryDataListAttribute(string methodNameNoArguments) {
            methodName = methodNameNoArguments;
        }
    }

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(CustomCountryDataListAttribute))]
    public class CustomCountryDataListPropertyDrawer : PropertyDrawer {

        private const float FOLDOUT_HEIGHT = 21f;
        private const float FIELD_HEIGHT = 19f;
        private const float SEPARATION = 2f;
        private float labelSize = 100f;

        private bool isInitialized = false;

        private List<string> transalationsFields;

        private void Initialize() {
            transalationsFields = new List<string>();

            FieldInfo[] allFields = typeof(CountryNameTranslations).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in allFields) {
                transalationsFields.Add(field.Name);
            }

            labelSize = EditorGUIUtility.labelWidth * 0.5f;

            isInitialized = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (!isInitialized) {
                Initialize();
            }

            float height = FOLDOUT_HEIGHT;

            if (property.isExpanded) {
                height += transalationsFields.Count * FOLDOUT_HEIGHT;
            }

            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginChangeCheck();

            if (!isInitialized) {
                Initialize();
            }

            EditorGUI.BeginProperty(position, label, property);
            Rect foldoutRect = new Rect(position.x, position.y, position.width, FIELD_HEIGHT);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);

            if (property.isExpanded) {
                CreateCodesSection(position, property);

                CreateTranslationsSection(position, property);

                CreateFlagSection(position, property);
            }

            EditorGUI.EndProperty();

            if (EditorGUI.EndChangeCheck()) {
                Debug.Log("Holi");
                OnModifiedObject(property);
            }
        }

        private void OnModifiedObject(SerializedProperty property) {
            CustomCountryDataListAttribute at = attribute as CustomCountryDataListAttribute;
            MethodInfo method = property.serializedObject.targetObject.GetType().GetMethods().Where(m => m.Name == at.methodName).First();

            if (method != null && method.GetParameters().Count() == 0)// Only instantiate methods with 0 parameters
                method.Invoke(property.serializedObject.targetObject, null);
        }

        private void CreateCodesSection(Rect position, SerializedProperty property) {
            float codeXPos = position.x + position.width * 0.6f;
            float codeYPos = position.y + FIELD_HEIGHT + SEPARATION;

            float codeWidth = position.width * 0.4f;

            Rect codeTwoRect = new Rect(codeXPos, codeYPos, codeWidth, FIELD_HEIGHT);
            EditorGUIUtility.labelWidth = 80;
            EditorGUI.PropertyField(codeTwoRect, property.FindPropertyRelative("countryId"), new GUIContent("Alpha-2 Code"));

            Rect codeThreeRect = new Rect(codeXPos, codeYPos + FIELD_HEIGHT + SEPARATION, codeWidth, FIELD_HEIGHT);
            EditorGUIUtility.labelWidth = 80;
            EditorGUI.PropertyField(codeThreeRect, property.FindPropertyRelative("longCountryId"), new GUIContent("Alpha-3 Code"));
        }

        private void CreateTranslationsSection(Rect position, SerializedProperty property) {
            float addY = FIELD_HEIGHT + SEPARATION;

            SerializedProperty translationsProperty = property.FindPropertyRelative("countryName");

            for (int i = 0; i < transalationsFields.Count; ++i) {
                Rect rect = new Rect(position.x, position.y + addY, (position.width * 0.6f) - 10, FIELD_HEIGHT);

                addY += rect.height + SEPARATION;

                string labelName = transalationsFields[i].Replace("Name", "").Substring(0, 1).ToUpper() +
                    transalationsFields[i].Replace("Name", "").Substring(1);

                EditorGUIUtility.labelWidth = labelSize;
                EditorGUI.PropertyField(rect,
                    translationsProperty.FindPropertyRelative(transalationsFields[i]),
                    new GUIContent(labelName));
            }
        }

        private void CreateFlagSection(Rect position, SerializedProperty property) {
            SerializedProperty countryFlagProp = property.FindPropertyRelative("countryFlag");

            Rect spriteRect = new Rect(position.xMax - 60, position.yMax - 42, 60, 42);
            countryFlagProp.objectReferenceValue =
                EditorGUI.ObjectField(spriteRect, countryFlagProp.objectReferenceValue, typeof(Sprite), false);
        }
    }

#endif

}
