﻿using System;

namespace AutoCSer.Example.TcpOpenServer
{
    /// <summary>
    /// 可读属性支持 示例
    /// </summary>
    [AutoCSer.Net.TcpOpenServer.Server(Host = "127.0.0.1", Port = 13003)]
    partial class Property
    {
        /// <summary>
        /// 只读属性支持
        /// </summary>
        [AutoCSer.Net.TcpOpenServer.Method]
        int GetProperty { get { return 2; } }
        /// <summary>
        /// 可写属性支持
        /// </summary>
        [AutoCSer.Net.TcpOpenServer.Method(IsOnlyGetMember = false)]
        int SetProperty { get; set; }
        /// <summary>
        /// 索引属性支持
        /// </summary>
        [AutoCSer.Net.TcpOpenServer.Method(IsOnlyGetMember = false)]
        int this[int index]
        {
            get { return SetProperty - index; }
            set { SetProperty = value + index; }
        }

        /// <summary>
        /// 可读属性支持 测试
        /// </summary>
        /// <returns></returns>
        //[AutoCSer.Metadata.TestMethod]
        internal static bool TestCase()
        {
            using (AutoCSer.Example.TcpOpenServer.Property.TcpOpenServer server = new AutoCSer.Example.TcpOpenServer.Property.TcpOpenServer())
            {
                if (server.IsListen)
                {
                    using (AutoCSer.Example.TcpOpenServer.TcpClient.Property.TcpOpenClient client = new AutoCSer.Example.TcpOpenServer.TcpClient.Property.TcpOpenClient())
                    {
                        AutoCSer.Net.TcpServer.ReturnValue<int> value = client.GetProperty;
                        if (value.Type != AutoCSer.Net.TcpServer.ReturnType.Success || value.Value != 2)
                        {
                            return false;
                        }

                        server.Value.SetProperty = 0;
                        client.SetProperty = 3;
                        if (server.Value.SetProperty != 3)
                        {
                            return false;
                        }

                        server.Value.SetProperty = 0;
                        client[3] = 5;
                        if (server.Value.SetProperty != 3 + 5)
                        {
                            return false;
                        }

                        value = client[2];
                        if (value.Type != AutoCSer.Net.TcpServer.ReturnType.Success || value.Value != 3 + 5 - 2)
                        {
                            return false;
                        }

                        return true;
                    }
                }
            }
            return false;
        }
    }
}
