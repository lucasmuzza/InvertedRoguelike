using System;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;

public static class ConditionUtility
    {
        public static List<Type> GetConditionTypes(string folderPath)
        {
            List<Type> conditionTypes = new List<Type>();

            // Get all script paths in the specified folder
            string[] scriptGuids = AssetDatabase.FindAssets("t:Script", new[] { folderPath });
            foreach (string guid in scriptGuids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);

                // Get the type and check if it inherits from Condition
                Type scriptType = script?.GetClass();
                if (scriptType != null && typeof(Condition).IsAssignableFrom(scriptType) && !scriptType.IsAbstract)
                {
                    conditionTypes.Add(scriptType);
                }
            }

            return conditionTypes;
        }
    }