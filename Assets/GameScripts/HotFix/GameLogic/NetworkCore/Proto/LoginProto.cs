using System.Collections;
using UnityEngine;

namespace GameLogic.NetworkCore.Proto
{
    public class LoginProto
    {
        public readonly string port = "/user/login";

        public class LoginRequest
        {
            /// <summary>
            /// 微信登陆的code
            /// </summary>
            public string code = "";
            /// <summary>
            /// device_id
            /// </summary>
            public string device_id = "";
            /// <summary>
            /// 登录模式 游客 device_id phone auto_login taptap wechat
            /// </summary>
            public string mode = "";

            /// <summary>
            /// 密码
            /// </summary>
            public string password = "";

            /// <summary>
            /// 手机号
            /// </summary>
            public string phone = "";

            /// <summary>
            /// steam_token
            /// </summary>
            public string steam_token = "";

            /// <summary>
            /// taptap唯一标识
            /// </summary>
            public string taptap_token = "";

            /// <summary>
            /// yid
            /// </summary>
            public string yid = "";
        }
        public class LoginResponses 
        {

        }
    }
}