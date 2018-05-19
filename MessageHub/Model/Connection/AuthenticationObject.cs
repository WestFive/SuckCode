using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageHub.Model.Connection
{
    public class AuthenticationObject
    {
        public string clientCode { get; set; }

        public string clientName { get; set; }


        /// <summary>
        /// 连接消息服务的对象参数
        /// </summary>
        /// <param name="clientCode"></param>
        /// <param name="clientName"></param>
        public AuthenticationObject(string clientCode,string clientName)
        {
            this.clientCode = clientCode;
            this.clientName = clientName;
        }

    }
}
