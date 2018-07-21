
#region Using

using System;
using System.Collections.Generic;
using PureMVC.Interfaces;

#endregion

namespace PureMVC.Core
{
    public class Model : IModel
    {
        protected IDictionary<string, IProxy> m_proxyMap;
        protected static volatile IModel m_instance;
        protected readonly object m_syncRoot = new object();
        protected static readonly object m_staticSyncRoot = new object();

        #region 初始化
        protected Model()
        {
            m_proxyMap = new Dictionary<string, IProxy>();
            InitializeModel();
        }
        public static IModel Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new Model();
                    }
                }
                return m_instance;
            }
        }
        static Model()
        {
        }
        protected virtual void InitializeModel()
        {
        }
        #endregion

        //注册Proxy
        public virtual void RegisterProxy(IProxy proxy)
		{
			lock (m_syncRoot)
			{
				m_proxyMap[proxy.ProxyName] = proxy;
			}
			proxy.OnRegister();
		}

        //取出Proxy
		public virtual IProxy RetrieveProxy(string proxyName)
		{
			lock (m_syncRoot)
			{
				if (!m_proxyMap.ContainsKey(proxyName)) return null;
				return m_proxyMap[proxyName];
			}
		}

		public virtual bool HasProxy(string proxyName)
		{
			lock (m_syncRoot)
			{
				return m_proxyMap.ContainsKey(proxyName);
			}
		}

		public virtual IProxy RemoveProxy(string proxyName)
		{
			IProxy proxy = null;
			lock (m_syncRoot)
			{
				if (m_proxyMap.ContainsKey(proxyName))
				{
					proxy = RetrieveProxy(proxyName);
					m_proxyMap.Remove(proxyName);
				}
			}
			if (proxy != null) proxy.OnRemove();
			return proxy;
		}
    }
}
