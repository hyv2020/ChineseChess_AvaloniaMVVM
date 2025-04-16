using NetworkCommons;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GameClient
{
    public class AsynchronousClient : NetworkObserver
    {
        string serverIP;
        TcpClient tcpClient;
        NetworkStream stream;
        bool streamOpened = false;

        public AsynchronousClient(string server)
        {
            this.serverIP = server;
        }
        public async Task ConnectAsync()
        {
            // Create a TcpClient.
            // Note, for this client to work you need to have a TcpServer
            // connected to the same address as specified by the server, port
            // combination.
            Int32 port = Ports.remotePort;

            // Not a using declaration to the instance stays connected.
            Debug.WriteLine($"Client connecting to: {serverIP}");
            tcpClient = new TcpClient(serverIP, port);
            while (true)
            {
                try
                {
                    if (!tcpClient.Connected)
                    {
                        tcpClient.Connect(serverIP, port);
                    }
                    else
                    {
                        Debug.WriteLine($"Client connected to: {serverIP}");

                    }
                    await Listen();
                    // Send and receive data here...
                    //await SendMessageAsync(new Turn());
                    // Close the stream and the client when finished
                    //stream.Close();
                }
                catch
                {
                    // Handle any exceptions here...
                    // If the connection was lost, the loop will start over
                }
            }

        }

        public async Task SendMessageAsync(object message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.

                // Prefer a using declaration to ensure the instance is Disposed later.
                // Translate the passed message and store it as a Byte array.
                byte[] data = message.ToByteArray();

                // Get a client stream for reading and writing.
                stream = tcpClient.GetStream();
                // Send the message to the connected TcpServer.
                await stream.WriteAsync(data, 0, data.Length);

                Debug.WriteLine($"Sent: {message}", this.serverIP.ToString());


                // Explicit close is not necessary since TcpClient.Dispose() will be
                // called automatically.
                // stream.Close();
                // client.Close();

            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }

        }
        private async Task Listen()
        {
            streamOpened = true;
            while (streamOpened)
            {
                stream = tcpClient.GetStream();
                // Buffer to store the response bytes.
                var data = new byte[Ports.bufferSize];
                // Read the first batch of the TcpServer response bytes.
                int bytes = await stream.ReadAsync(data, 0, data.Length);
                NotifyObservers(data);
                // data recieved from server
                var responseData = data.FromByteArray();
                Debug.WriteLine($"Received: {responseData}", this.serverIP.ToString());

            }

        }
        public void Disconnect()
        {
            streamOpened = false;
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
                stream = null;
            }
            if (tcpClient != null)
            {
                tcpClient.Close();
                tcpClient.Dispose();
                tcpClient = null;
            }
        }


    }
}
