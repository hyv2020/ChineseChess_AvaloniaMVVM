using GameCommons;
using NetworkCommons;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GameServer
{
    public class AsynchronousTCPListener : NetworkObserver
    {
        TcpListener server = new TcpListener(IPAddress.Any, Ports.remotePort);
        public Turn hostStartTurn;
        public Side? hostStartSide;
        public AsynchronousTCPListener()
        {

        }

        public static HashSet<TcpClient> clients = new HashSet<TcpClient>();
        public async Task StartListeningAsync()
        {
            try
            {
                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Debug.WriteLine("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = await server.AcceptTcpClientAsync().ConfigureAwait(true);

                    // Add the new client to the list of clients
                    clients.Add(client);
                    Debug.WriteLine("Client Connected!");
                    var clientCount = clients.Count.ToByteArray();
                    // tell second player start state
                    await RedirectToClientAsync(clients.Count, client);
                    await RedirectToClientAsync(hostStartTurn, client);
                    await RedirectToClientAsync(hostStartSide.ToString(), client);
                    // Start a new thread to handle communication
                    // with connected client
                    while (client.Connected)
                    {
                        var message = await ListenClientAsync(client);
                        await RedirectToClientAsync(message, client);
                        // tell clients how many is connected to server
                        Debug.WriteLine("Continue listening... ");
                    }

                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                //server.Stop();
            }

            Debug.WriteLine("\nHit enter to continue...");
        }
        public void StopListening() => server.Stop();

        private async Task<object> ListenClientAsync(object obj)
        {
            // Retrieve the client from the parameter passed to the thread
            TcpClient client = (TcpClient)obj;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[Ports.bufferSize];
            //data = Encoding.Default.GetBytes("Server");
            // recieve data from client
            int bytes = await stream.ReadAsync(data, 0, data.Length);
            var clientData = data.FromByteArray();
            NotifyObservers(data);
            Debug.WriteLine($"Received: {clientData}", "Server");
            return clientData;
        }
        public async Task RedirectToClientAsync(object obj)
        {
            var data = obj.ToByteArray();
            try
            {
                foreach (TcpClient otherClient in clients)
                {
                    NetworkStream stream = otherClient.GetStream();
                    // send data to different client
                    NetworkStream otherStream = otherClient.GetStream();
                    Debug.WriteLine($"Send: {obj}", "Server");
                    await otherStream.WriteAsync(data, 0, data.Length);
                }
            }
            catch
            {
                Debug.WriteLine("Server Return/Redirect message failed");
            }

        }

        public async Task RedirectToClientAsync(object obj, TcpClient client)
        {
            var data = obj.ToByteArray();
            try
            {
                foreach (TcpClient otherClient in clients)
                {
                    NetworkStream stream = client.GetStream();
                    if (!otherClient.Equals(client))
                    {
                        // send data to different client
                        NetworkStream otherStream = otherClient.GetStream();
                        Debug.WriteLine($"Redirect: {obj}", "Server");
                        await otherStream.WriteAsync(data, 0, data.Length);
                    }
                    else
                    {
                        // return message back to same client
                        Debug.WriteLine($"Return: {obj}", "Server");
                        await stream.WriteAsync(data, 0, data.Length);
                    }

                }
            }
            catch
            {
                Debug.WriteLine("Server Return/Redirect message failed");
            }

        }
    }

}
