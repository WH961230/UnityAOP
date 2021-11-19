using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Microsoft.Practices.Unity.Configuration;
using Unity;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
        fileMap.ExeConfigFilename =
            Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName +
                         "//Unity.Config");
        Configuration configuration =
            ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
        UnityConfigurationSection configSection =
            (UnityConfigurationSection)configuration.GetSection(UnityConfigurationSection.SectionName);
        configSection.Configure(container, "aopContainer");

        IUserInfoProcessor processor = container.Resolve<IUserInfoProcessor>();
        processor.RegUser(user);
        
        Debug.Log("**************** 结束 **************");
    }
}

public class UserInfo {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
}

/***
 * 玩家信息接口
 */
public interface IUserInfoProcessor {
    void RegUser(UserInfo userinfo);
    UserInfo GetUser(UserInfo userInfo);
}

/***
 * 玩家信息
 */
public class UserInfoProcessor : IUserInfoProcessor{
    public void RegUser(UserInfo userinfo) {
        Debug.Log($"创建角色：{userinfo.Name} id：{userinfo.Id}");
    }

    public UserInfo GetUser(UserInfo userInfo) {
        return userInfo;
    }
}

/***
 * 方法执行前日志
 */
public class LogBehavior : IInterceptionBehavior {
    public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext) {
        throw new NotImplementedException();
    }

    public IEnumerable<Type> GetRequiredInterfaces() {
        throw new NotImplementedException();
    }

    public bool WillExecute { get; }
}