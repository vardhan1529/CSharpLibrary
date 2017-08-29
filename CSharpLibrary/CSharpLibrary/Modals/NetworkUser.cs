using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLibrary.Modals
{
    public class MockData
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public float Balance { get; set; }

        public bool Active { get; set; }

        public List<string> Plans { get; set; }

        public int LocalArea { get; set; }

        private static List<MockData> MockUsers = GenerateMockUsers();

        private static List<MockData> GenerateMockUsers()
        {
            return new List<MockData>() { new MockData() { Active = true, Balance = 100, Id = 1, LocalArea = 1, Name = "User 1" },
            new MockData() { Active = true, Balance = 100, Id = 1, LocalArea = 2, Name = "User 2" },
            new MockData() { Active = true, Balance = 100, Id = 1, LocalArea = 3, Name = "User 3" },
            new MockData() { Active = true, Balance = 100, Id = 1, LocalArea = 4, Name = "User 4" },
            new MockData() { Active = true, Balance = 100, Id = 1, LocalArea = 5, Name = "User 5" },
            new MockData() { Active = true, Balance = 100, Id = 1, LocalArea = 6, Name = "User 6" },
            new MockData() { Active = true, Balance = 100, Id = 1, LocalArea = 7, Name = "User 7" }};
        }

        public static List<MockData> GetUsers()
        {
            return MockUsers;
        }

        public static bool AddUser(MockData data)
        {
            try
            {
                data.Id = MockUsers.OrderByDescending(m => m.Id).First().Id + 1;
                MockUsers.Add(data);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static MockData FindById(int id)
        {
            return MockUsers.Find(m => m.Id == id);
        }

        public static List<MockData> FindByActiveStatus(bool status)
        {
            return MockUsers.FindAll(m => m.Active == status);
        }
    }
    public enum UserEvent
    {
        Call,
        Message,
        Internet
    }

    public class NetworkUser
    {
        public NetworkUser()
        {
            this.EventsRegister = RegisterEvents();
        }

        public int Id { get; set; }

        public UserEvent Event { get; set; }

        public bool IsValid
        {
            get
            {
                return Validation();
            }
        }

        private MockData UserData { get; set; }

        private Dictionary<UserEvent, Func<bool>> EventsRegister;

        private bool Validation()
        {
            UserData = MockData.FindById(Id);
            if(UserData == null || !UserData.Active)
            {
                return false;
            }

            var e = EventsRegister[Event];

            return e();
        }

        private Dictionary<UserEvent, Func<bool>> RegisterEvents()
        {
            return new Dictionary<UserEvent, Func<bool>>() { { UserEvent.Call, ValiateCallEvent }, { UserEvent.Internet, ValiateInternetEvent }, { UserEvent.Message, ValiateMessageEvent } };
        }

        private bool ValiateCallEvent()
        {
            if(UserData.Balance > 0)
            {
                return true;
            }

            return false;
        }

        private bool ValiateInternetEvent()
        {
            if(UserData.Plans.Contains("Internet"))
            {
                return true;
            }

            return false;
        }

        private bool ValiateMessageEvent()
        {
            if(UserData.Balance >= 1)
            {
                return true;
            }

            return false;
        }
    }
}
