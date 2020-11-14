using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Бипит_7
{
    [ServiceContract(CallbackContract = typeof(IServerChatCallBack))]
    public interface IService1
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        List<string> Istoria_list();

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string messag, int id);

       
    }

    public interface IServerChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallBack(string messag);
        
    }
}
