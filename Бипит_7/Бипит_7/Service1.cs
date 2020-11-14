using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Бипит_7
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };

            nextId++;
            SendMsg(" " + user.Name + " подключился к чату.", 0);
            users.Add(user);
            return user.ID; 
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if(user != null)
            {
                users.Remove(user);
                SendMsg(" " + user.Name + " покинул чат.", 0);
            }
        }

        public List<string> isroria = new List<string>();

        public List<string> Istoria_list()
        {
            return isroria;
        }

        public void SendMsg(string messag, int id)
        {
            bool metka = true;
            foreach(var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == id);

                if (user != null)
                {
                    answer += ": " + user.Name+ ": ";
                }

                answer += messag;

                if (metka)
                {
                    metka = false;
                    isroria.Add(answer);
                }

                item.operationContext.GetCallbackChannel<IServerChatCallBack>().MsgCallBack(answer);
            }
        }
    }
}
