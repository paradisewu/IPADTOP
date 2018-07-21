/* 
 PureMVC C# Port by Andy Adamczak <andy.adamczak@puremvc.org>, et al.
 PureMVC - Copyright(c) 2006-08 Futurescale, Inc., Some rights reserved. 
 Your reuse is governed by the Creative Commons Attribution 3.0 License 
*/

#region Using

using System;

#endregion

namespace PureMVC.Interfaces
{
    public interface IModel
    {
		void RegisterProxy(IProxy proxy);

		IProxy RetrieveProxy(string proxyName);

        IProxy RemoveProxy(string proxyName);

		bool HasProxy(string proxyName);
    }
}
