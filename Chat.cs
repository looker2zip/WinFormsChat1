using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;                               // Socket Ŭ���� ���
using System.Net;                                       // ��Ʈ��ũ ���� Ŭ���� ���

namespace WinFormsChat1
{
    public partial class Chat : Form
    {
        delegate void UpdateTextCallback(String message);

        /// <summary>
        /// Chat Ŭ���� ������
        /// </summary>
        public Chat()
        {
            InitializeComponent();

            mTxtMyIP.Text = "127.0.0.1";                // �ڽ��� ����Ű�� IP �ּ�
            mTxtMyPortNum.Text = "3317";                // ä�� ���α׷����� ����� port ��ȣ
            mTxtServerIP.Text = "127.0.0.1";            // ���� IP �ּ�
            mTxtServerPortNum.Text = "3317";            // ���� port ��ȣ
        }

        /// <summary>
        /// ���� ���� ��ư Ŭ�� �̺�Ʈ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnStartServer_Click(object sender, EventArgs e)
        {
            Socket server = null;                   // ������ ����� ����
            Socket client = null;                   // ������ Ŭ���̾�Ʈ�� ��Ÿ���� ����
            byte[] data = new byte[1024];           // �����͸� ������ byte �迭
            String message = "";                    // �������κ��� ������ �޽���

            // �������� ����� Port ��ȣ
            int portNum = Int32.Parse(mTxtMyPortNum.Text.ToString());

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, portNum);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(ipep);
            server.Listen(10);                      // ó�� ���ť �ִ� ũ��

            NotifyMessage("������ �����մϴ�.\nŬ���̾�Ʈ�� ������ ����մϴ�.");

            client = server.Accept();               // Ŭ���̳�Ʈ ���� ���

            NotifyMessage("Ŭ���̾�Ʈ�� �����Ͽ����ϴ�.");

            client.Receive(data);                   // Ŭ���̾�Ʈ�κ��� ������ ����

            message = Encoding.Default.GetString(data);
            message = "����: " + message;
            AppendMessage(message);

            client.Close();                         // Ŭ���̾�Ʈ ���� �ݱ�
            server.Close();                         // ���� ���� �ݱ�
        }

        /// <summary>
        /// ���� ���� �̺�Ʈ ó��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnConnectToServer_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];
            String serverIPAddress = "";                // ���� IP �ּ�
            int portNum = 0;                            // ���� port ��ȣ

            serverIPAddress = mTxtServerIP.Text.Trim(); // ������ ���� IP �ּ� ȹ��
            portNum = Int32.Parse(mTxtServerPortNum.Text.Trim());   // ������ ���� port ��ȣ�� ������

            IPAddress ipAddress = IPAddress.Parse(serverIPAddress); // ������ ������ IP �ּ�
            IPEndPoint ipep = new IPEndPoint(ipAddress, portNum);   // ������ ������ ����

            // ����/ ���� ����
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            NotifyMessage("������ �����մϴ�.");

            server.Connect(ipep);                               // ������ ����

            NotifyMessage("������ �����Ͽ����ϴ�.");

            data = Encoding.Default.GetBytes("Ŭ���̾�Ʈ���� ������ �޽����Դϴ�.");
            server.Send(data);                                  // ������ ������ ����

            NotifyMessage("������ �����͸� �����Ͽ����ϴ�.");

            server.Close();                                     // ���� ���� �ݱ�
        }

        /// <summary>
        /// �˸� �޽��� ����
        /// </summary>
        /// <param name="message">SyncSockets Ŭ�����κ��� ������ ���� ����</param>
        public void NotifyMessage(String message)
        {
            message = "------�˸�!------\r\n" + message + "\r\n---------------------\r\n";

            AppendMessage(message);
        }

        /// <summary>
        /// ��Ƽ ���������� ����� delegate �Լ�
        /// </summary>
        /// <param name="message">����� �޽���</param>
        private void AppendMessage(String message)
        {
            try
            {
                if (mTxtInputMessage.InvokeRequired)
                {
                    UpdateTextCallback d = new UpdateTextCallback(AppendMessage);
                    Invoke(d, new object[] { message });
                }
                else
                {
                    mTxtChatWindow.AppendText(message + "\r\n");
                    mTxtChatWindow.ScrollToCaret();
                    mTxtInputMessage.Focus();
                }
            }
            catch { }
        }
    }
}