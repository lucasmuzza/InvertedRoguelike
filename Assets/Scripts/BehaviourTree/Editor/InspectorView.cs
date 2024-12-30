using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using System.Linq;
public class InspectorView : VisualElement {
    public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

    private Editor editor;
    private Button conditionButton;
    private List<Type> conditionTypes = new List<Type>();

    private SerializedObject serializedObject;
    private SerializedProperty conditionsProperty;

    public void UpdateSelection(NodeView nodeView) {
        Clear();

        // Destroy existing editor and create a new one
        UnityEngine.Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(nodeView.node);

        // Create and add the IMGUIContainer for custom Inspector
        IMGUIContainer container = new IMGUIContainer(() => {
            if (editor && editor.target) {
                editor.OnInspectorGUI();
            }
        });
        Add(container);

        if (nodeView.node is OptionNode optionNode) {
            // Setup custom UI for conditions
            SetupUI();

            // Initialize serialized properties
            serializedObject = new SerializedObject(optionNode);
            conditionsProperty = serializedObject.FindProperty("conditions");

            DisplayConditions(optionNode);
        }

        // Load condition types
        LoadConditionTypes();
    }

    private void SetupUI() {
        // Button to trigger the dropdown
        conditionButton = new Button(() => ShowConditionDropdown()) {
            text = "Add Condition"
        };
        Add(conditionButton);
    }

    private void LoadConditionTypes() {
        string folderPath = "Assets/Scripts/BehaviourTree/Conditions";
        conditionTypes = ConditionUtility.GetConditionTypes(folderPath);
    }

    private void ShowConditionDropdown() {
        if (conditionTypes.Count == 0) {
            Debug.LogWarning("No conditions found. Ensure you have condition scripts in the specified folder.");
            return;
        }

        var conditionNames = conditionTypes.Select(type => type.Name).ToList();
        UnityEditor.PopupWindow.Show(new Rect(0, 0, 200, 300), new ConditionDropdown(conditionNames, OnConditionSelected));
    }

    private void OnConditionSelected(string conditionName) {
        if (editor?.target is OptionNode optionNode) {
            Type selectedType = conditionTypes.FirstOrDefault(type => type.Name == conditionName);
            if (selectedType != null) {
                // Add the new condition
                Condition newCondition = (Condition)Activator.CreateInstance(selectedType);
                optionNode.conditions.Add(newCondition);

                // Update serialized object and apply changes
                serializedObject.Update();
                conditionsProperty.serializedObject.Update();
                serializedObject.ApplyModifiedProperties();

                // Mark the node dirty and refresh Inspector
                EditorUtility.SetDirty(optionNode);
                UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            }
        }
    }

    private void DisplayConditions(OptionNode optionNode) {
        // Display existing conditions with foldout
        EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);

        for (int i = 0; i < conditionsProperty.arraySize; i++) {
            SerializedProperty conditionProperty = conditionsProperty.GetArrayElementAtIndex(i);
            EditorGUILayout.PropertyField(conditionProperty, new GUIContent($"Condition {i + 1}"), true);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private class ConditionDropdown : PopupWindowContent {
        private List<string> conditionNames;
        private Action<string> onSelectCondition;

        public ConditionDropdown(List<string> conditionNames, Action<string> onSelectCondition) {
            this.conditionNames = conditionNames;
            this.onSelectCondition = onSelectCondition;
        }

        public override void OnGUI(Rect rect) {
            GUILayout.Label("Select a Condition", EditorStyles.boldLabel);
            foreach (var conditionName in conditionNames) {
                if (GUILayout.Button(conditionName)) {
                    onSelectCondition?.Invoke(conditionName);
                    editorWindow.Close();
                }
            }
        }
    }
}