﻿using System;
using AutoCSer.Log;

namespace AutoCSer.Net.TcpStreamServer
{
    /// <summary>
    /// TCP 服务客户端
    /// </summary>
    /// <typeparam name="attributeType">TCP 服务配置类型</typeparam>
    public abstract class Client<attributeType> : TcpServer.ClientBase<attributeType>
        where attributeType : ServerAttribute
    {
        /// <summary>
        /// TCP 服务客户端
        /// </summary>
        /// <param name="attribute">TCP服务调用配置</param>
        /// <param name="log">日志接口</param>
        public Client(attributeType attribute, ILog log) : base(attribute, log) { }
        /// <summary>
        /// 释放资源
        /// </summary>
        public override void Dispose()
        {
            if (IsDisposed == 0)
            {
                base.Dispose();
                if (CreateSocket != null) (CreateSocket as ClientSocket<attributeType>).DisposeSocket();
                SocketWait.Set();
            }
        }
    }
}
