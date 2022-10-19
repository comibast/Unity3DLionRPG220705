using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;      //引用編輯器命名空間
#endif

namespace Comibast
{
    /// <summary>
    /// 血量資料
    /// </summary>
    [CreateAssetMenu(menuName ="Comibast/Data Health", fileName = "Data Health")]
    public class DataHealth : ScriptableObject
    {
        [Header("血量"), Range(0, 10000)]
        public float hp;
        [HideInInspector]
        public float hpMax => hp;
        [Header("是否掉落寶物")]
        public bool isDropProp;
        [HideInInspector, Header("寶物預製物")]
        public GameObject goProp;
        [HideInInspector, Header("寶物掉落機率"), Range(0f, 1f)]
        public float propProbability;
    }

#if UNITY_EDITOR
    //自訂編輯器(類型(要自訂編輯器的類別))
    [CustomEditor(typeof(DataHealth))]
    public class DataHealthEditor : Editor
    {
        //序列化屬性 自訂名稱
        SerializedProperty spIsDropProp;
        SerializedProperty spGoProp;
        SerializedProperty spPropProbability;

        //啟動事件：該物件或元件顯示時執行一次
        private void OnEnable()
        {
            //序列化物件.尋找屬性(名稱(類別.資料名稱))
            spIsDropProp = serializedObject.FindProperty(nameof(DataHealth.isDropProp));
            spGoProp = serializedObject.FindProperty(nameof(DataHealth.goProp));
            spPropProbability = serializedObject.FindProperty(nameof(DataHealth.propProbability));
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();

            if (spIsDropProp.boolValue)
            {
                EditorGUILayout.PropertyField(spGoProp);
                EditorGUILayout.PropertyField(spPropProbability);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
