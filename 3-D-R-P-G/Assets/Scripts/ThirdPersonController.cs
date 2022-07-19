using UnityEngine;


namespace Comibast
{
    /// <summary>
    /// 第三人稱控制器
    /// 移動與跳躍基本控制、動畫更新
    /// </summary>
    public class ThirdPersonController : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("移動速度"), Range(0, 50)]
        private float speed = 3.5f;
        [SerializeField, Header("旋轉速度"), Range(0, 50)]
        private float turn = 5f;
        [SerializeField, Header("跳躍速度"), Range(0, 50)]
        private float jump = 7f;
        private Animator ani;
        private CharacterController controller;
        private Vector3 direction;
        private Transform traCamera;
        private string parRun = "浮點數跑步";
        private string parJump = "觸發跳躍";


        #endregion


        #region 事件
        private void Awake()
        {
            ani = GetComponent<Animator>();        //GetComponent要寫在Awake或Start,否則一直抓會lag.
            controller = GetComponent<CharacterController>();

            //GameObject.Find透過名稱搜尋物件，很耗效能，建議放在Awake或Start或執行一次的架構內。
            traCamera = GameObject.Find("Main Camera").transform;

        }

        private void Update()
        {
            Move();
            Jump();
        }
        #endregion



        #region 方法
        /// <summary>
        /// 移動
        /// </summary>
        private void Move()
        {
            float v = Input.GetAxisRaw("Vertical");                 //取得前後按鍵值：WS、↑↓
            float h = Input.GetAxisRaw("Horizontal");        //取得左右按鍵值：AD、←→
            //print("<color=yellow>垂直軸向:" + v + "</color>");

            #region 旋轉
            //transform.rotation = traCamera.rotation;　　　　//沒有過渡的旋轉
            //有過渡的旋轉：玩家角度=四元數.插值(玩家角度.攝影機角度.速度*每幀時間)
            transform.rotation = Quaternion.Lerp(transform.rotation, traCamera.rotation, turn * Time.deltaTime);

            //歐拉角 euler Angles 0 - 45 - 90 - 180 - 360
            //固定 X 與 Z 角度為零
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            
            #endregion

            direction.z = v;　　　　　　　　　　　　　　　　//物件前後軸為 z 軸：指定為 v 前後按鍵
            direction.x = h;                             //物件左右軸為 x 軸：指定為 h 左右按鍵

            direction = transform.TransformDirection(direction);  //將角色的區域座標轉為世界座標

            //角色控制器.移動(方向*速度)
            //Time.deltaTime 每幀的時間
            controller.Move(direction * speed * Time.deltaTime);

            //動畫更新
            float vAxis = Input.GetAxis("Vertical");
            float hAxis = Input.GetAxis("Horizontal");

            if(Mathf.Abs(vAxis)>0.1f)
            {
                ani.SetFloat(parRun, Mathf.Abs(vAxis));
            }
            else if (Mathf.Abs(hAxis) > 0.1f)
            {
                ani.SetFloat(parRun, Mathf.Abs(hAxis));
            }
            else
            {
                ani.SetFloat(parRun, 0);
            }
                
        }


        /// <summary>
        /// 跳躍
        /// </summary>
        private void Jump()
        {
            //如果 在地面上 並且 按下空白鍵 就跳躍
            if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                direction.y = jump;
                ani.SetTrigger(parJump);
            }

            //地心引力 (預設-9.81)
            direction.y += Physics.gravity.y * Time.deltaTime;
        }
        #endregion
    }

}


