using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.ConceptSamples
{
    class ObseverPattern
    {
        internal interface IUser
        {
            Guid Id { get; set; }
            void Update();
        }

        internal interface IShoeRetailer<T>
        {
            void SubscribeUser(T user);
            void UnSubscribeUser(Guid userId);
            void NotifyUsers();           
        }

        internal class User : IUser
        {
            public Guid Id { get; set; }
            public void Update()
            {
            }
        }

        internal class ShoeRetiler<T>: IShoeRetailer<T> where T:IUser
        {
            List<T> UsersList { get; set; }
            public void SubscribeUser(T user)
            {
                UsersList.Add(user);
            }
            public void UnSubscribeUser(Guid userId)
            {
                var user = UsersList.Find(m => m.Id == userId);
                if(user != null)
                {
                    UsersList.Remove(user);
                }
            }
            public void NotifyUsers()
            {
                foreach(var user in UsersList)
                {
                    user.Update();
                }
            }
            public void AddCampaign()
            {
                NotifyUsers();
            }
        }
    }
}
