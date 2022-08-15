using UnityEngine;

namespace Comibast
{
    /// <summary>
    /// 敵人資料
    /// </summary>
    [CreateAssetMenu(menuName = "Comibast/Data Enemy", fileName = "Data Enemy", order = 1)]
    public class DataEnemy : ScriptableObject
    {
        [Header("血量"), Range(0, 2000)]
        public float hp;
        [Header("攻擊力"), Range(0, 200)]
        public float attack;
        [Header("追蹤距離"), Range(0, 200)]
        public float rangeTrack;
        [Header("攻擊距離"), Range(0, 10)]
        public float rangeAttack;
        [Header("走路速度"), Range(0, 100)]
        public float speedWalk;
        [Header("掉落道具機率"), Range(0, 1)]
        public float propbilityProp;
        [Header("掉落道具")]
        public float goProp;
        [Header("等待時間範圍")]
        public Vector2 timeIdleRange;
        [Header("要追蹤的目標圖層")]
        public LayerMask layerTarget;
        [Header("攻擊間隔")]
        public float intervalAttack;
    }


}


