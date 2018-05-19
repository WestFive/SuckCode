using MessageHub.Model.Connection;
using MessageHub.Model.Message;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessageHub
{
    public class MessageHubClient
    {
        public string HubAddress { get; set; }
        public string HubName { get; set; }
        public Dictionary<string, string> HubConnectctParames { get; set; }
        #region HubReciveMessageCallBack
        public delegate void ReciveMessage(string str);
        /// <summary>
        /// 接收消息的回调
        /// </summary>
        public event ReciveMessage reciveMessage;
        #endregion
        #region HubStatusCallBack
        /// <summary>
        /// 接收状态的回调：例如连接成功
        /// </summary>
        /// <param name="str"></param>
        public delegate void ReciveStatus(string str);
        public event ReciveStatus reciveStatus;
        #endregion
        #region HubErrorCallBack
        /// <summary>
        /// 接收hub错误。
        /// </summary>
        public delegate void HubError(string str);
        public event HubError reciveHubError;
        #endregion


        public Quobject.SocketIoClientDotNet.Client.Socket socket;



        /// <summary>
        /// CreateHub
        /// </summary>
        /// <param name="HubAddress">地址</param>
        /// <param name="HubMethod">名称</param>
        /// <param name="HubParams">参数例如：传入一个Dictionary dic = {{"Name","xiamenxxxxx"},{"Type","client"}}</param>
        public MessageHubClient(string HubAddress, Dictionary<string, string> HubParams = null)
        {
            socket = IO.Socket(HubAddress);

            socket.On(Quobject.SocketIoClientDotNet.Client.Socket.EVENT_CONNECT, () =>
            {


                socket.Emit("authentication", (data) =>
                {
                    reciveStatus?.Invoke(JsonConvert.SerializeObject(data));

                }, JsonConvert.SerializeObject(new AuthenticationObject(HubParams.Last().Value, HubParams.First().Value)));



            });

            socket.On(HubParams.First().Value, (data) =>
            {
                reciveMessage?.Invoke(JsonConvert.SerializeObject(data));
            });



        }

        public void HubInit()
        {
            try
            {
                socket.Connect();
                socket.On(Quobject.SocketIoClientDotNet.Client.Socket.EVENT_ERROR, (data) =>
                {

                    reciveHubError?.Invoke(data.ToString());
                });
            }
            catch (Exception ex)
            {
                reciveHubError?.Invoke(ex.ToString());
            }
        }


        public void sendP2p2(MessageP2p p2p)
        {
            socket.Emit("p2p", JsonConvert.SerializeObject(p2p));
        }



        public bool flag = false;

        private void ReConnecting()
        {





        }
        private void ReConnected()
        {

        }
        /// <summary>
        /// SimulatorLaneSendMes
        /// </summary>
        public void AddMessage(MessageCUR messagecreate)
        {

            socket.Emit("createMessage", (data) =>
            {
                reciveStatus?.Invoke(JsonConvert.SerializeObject(data));
            }
            , JsonConvert.SerializeObject(messagecreate));


        }



     

      

        public void deleteMessage(MessageD messageDel)
        {
            socket.Emit("deleteMessage", JsonConvert.SerializeObject(messageDel));
            Console.WriteLine(JsonConvert.SerializeObject(messageDel));

        }
    }
}

