using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// �������
    /// </summary>
    [CreateAssetMenu(menuName = "Comibast/Data Attack", fileName = "DataAttack")]
    public class DataAttack : ScriptableObject
    {
        [Header("�����O"), Range(0, 1000)]
        public float attack;
        [Header("�����ϰ�]�w")]
        public Color attackAreaColor = new Color(1, 0, 0, 0.5f);
        public Vector3 attackAreaSize = Vector3.one;
        public Vector3 attackAreaOffset;
        [Header("�����ؼйϼh")]
        public LayerMask layerTarget;
        [Header("��������ɶ�"), Range(0, 3)]
        public float delayAttack = 1f;            //1f=�w�]����1��(�e�m�ʧ@)
        [Header("�����ʵe��")]
        public AnimationClip animationAttack;

        /// <summary>
        /// ���ݧ��������G�ʵe�ɶ� - ��������
        /// �Ҧp�G���Y�H�G4 - 1.5 = 2.5��
        /// </summary>
        public float waitAttackEnd => animationAttack.length - delayAttack;
    }

}
