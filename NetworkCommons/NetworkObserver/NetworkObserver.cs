using System.Collections.Generic;

namespace NetworkCommons
{
    /// <summary>
    /// Observer for one to many relationships
    /// </summary>
    public abstract class NetworkObserver
    {
        private readonly object lockObject = new object();
        private readonly List<INetworkObserver> observers = new List<INetworkObserver>();
        public void NotifyObservers(byte[] data)
        {
            lock (lockObject)
            {
                foreach (INetworkObserver observer in observers)
                {
                    object obj = data.FromByteArray();
                    observer.OnTcpDataReceived(obj);
                }
            }
        }

        public void RegisterObserver(INetworkObserver observer)
        {
            lock (lockObject)
            {
                observers.Add(observer);
            }
        }

        public void UnregisterObserver(INetworkObserver observer)
        {
            lock (lockObject)
            {
                observers.Remove(observer);
            }
        }
    }
}
