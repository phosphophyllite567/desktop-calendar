using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCalendar
{
    public class ClientData
    {
        int id=0;
        List<User> users = new List<User>();

        public User FindUser(int id)
        {
            return users.Find(o => o.ID == id);
        }

        public bool Loggin(int id, string pwd)
        {
            if(FindUser(id)!=null)
            return FindUser(id).Pwd == pwd;
            return false;

        }

        public User Register(string pwd)
        {
            User newUser = new User();
            newUser.ID = id;
            id++;
            newUser.Pwd = pwd;
            users.Add(newUser);
            return newUser;
        }
    }
}
