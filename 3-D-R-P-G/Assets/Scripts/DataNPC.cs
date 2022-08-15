using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// NPC ��ơG�W�١A��ܤ��e�A����
    /// ScriptableObject �}���ƪ���
    /// �N�{�����e�x�s�������b Project ��
    /// </summary>
    [CreateAssetMenu(menuName = "Comibast/Data NPC", fileName = "Data NPC", order = 2)]
    public class DataNPC : ScriptableObject
    {
        [Header("NPC �W��")]
        public string nameNPC;
        [Header("�Ҧ����"), NonReorderable]    //�[NonReorderable�ѨM�}�C�b�ݩʭ��O��ܿ��~�����D
        public DataDialogue[] dataDialogue;

    }

    //���O�ǦC��
    [System.Serializable]
    public class DataDialogue
    {
        [Header("��ܤ��e")]
        public string content;
        [Header("��ܭ���")]
        public AudioClip sound;
    }
}


