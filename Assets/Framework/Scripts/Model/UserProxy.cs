//[lzh]
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class UserProxy : Proxy, IProxy
{
    public new const string NAME = "UserProxy";

    public IList<UserVO> Users
    {
        get { return (IList<UserVO>)base.Data; }
    }

    public UserProxy() : base(NAME, new List<UserVO>())
    {
        AddItem(new UserVO("lstooge", "Larry", "Stooge", "larry@stooges.com", "ijk456", "ACCT"));
        AddItem(new UserVO("cstooge", "Curly", "Stooge", "curly@stooges.com", "xyz987", "SALES"));
        AddItem(new UserVO("mstooge", "Moe", "Stooge", "moe@stooges.com", "abc123", "PLANT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT")); 
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));




    }


    public void AddItem(UserVO user)
    {
        Users.Add(user);
    }


    public void UpdateItem(UserVO user)
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users[i].UserName.Equals(user.UserName))
            {
                Users[i] = user;
                break;
            }
        }
    }



    public void DeleteItem(UserVO user)
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users[i].UserName.Equals(user.UserName))
            {
                Users.RemoveAt(i);
                break;
            }
        }
    }
}
