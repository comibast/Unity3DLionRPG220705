using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;      //�ޥνs�边�R�W�Ŷ�
#endif

namespace Comibast
{
    /// <summary>
    /// ��q���
    /// </summary>
    [CreateAssetMenu(menuName ="Comibast/Data Health", fileName = "Data Health")]
    public class DataHealth : ScriptableObject
    {
        [Header("��q"), Range(0, 10000)]
        public float hp;
        [HideInInspector]
        public float hpMax => hp;
        [Header("�O�_�����_��")]
        public bool isDropProp;
        [HideInInspector, Header("�_���w�s��")]
        public GameObject goProp;
        [HideInInspector, Header("�_���������v"), Range(0f, 1f)]
        public float propProbability;
    }

#if UNITY_EDITOR
    //�ۭq�s�边(����(�n�ۭq�s�边�����O))
    [CustomEditor(typeof(DataHealth))]
    public class DataHealthEditor : Editor
    {
        //�ǦC���ݩ� �ۭq�W��
        SerializedProperty spIsDropProp;
        SerializedProperty spGoProp;
        SerializedProperty spPropProbability;

        //�Ұʨƥ�G�Ӫ���Τ�����ܮɰ���@��
        private void OnEnable()
        {
            //�ǦC�ƪ���.�M���ݩ�(�W��(���O.��ƦW��))
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
