using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using UnityEngine;
using Microsoft.Practices.Unity.InterceptionExtension;
using Unity;

public class A : MonoBehaviour
{
    void Start() {
        UserInfo user = new UserInfo() {
            Id = 1,
            Name = "WH",
            Password = "123456",
        };
        IUnityContainer container = new UnityContainer();
        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
        fileMap.ExeConfigFilename = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName + "//Unity.Config")
    }
}

public class UserInfo {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}

public interface IUserInfoProcessor {
    void RegUser(UserInfo userinfo);
    UserInfo GetUser(UserInfo userInfo);
}

public class UserInfoProcessor : IUserInfoProcessor{
    public void RegUser(UserInfo userinfo) {
        Debug.Log($"创建角色：{userinfo.Name} id：{userinfo.Id}");
    }

    public UserInfo GetUser(UserInfo userInfo) {
        return userInfo;
    }
}

public class LogBehavior : IInterceptionBehavior {
    public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {
        
    }

    public IEnumerable<Type> GetRequiredInterfaces() {
        
    }

    public bool WillExecute { get; }
}