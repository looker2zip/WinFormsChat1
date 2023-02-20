using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsChat1
{
    public partial class Chat : Form
    {
        delegate void UpdateTextCallback(String message);

        AsyncSocket mAsyncSocket = null;                // ���� �������� �۵��ϴ� ����� ���� ���� Ŭ���� 

        /// <summary>
        /// Chat Ŭ���� ������
        /// </summary>
        public Chat()
        {
            InitializeComponent();

            mAsyncSocket = new AsyncSocket(this);       // ����� ���� ���� Ŭ���� ����
            mTxtMyIP.Text = mAsyncSocket.GetMyIPAddress();
            mTxtMyPortNum.Text = AsyncSocket.DEFAULT_PORT_NUM.ToString();
            mTxtServerIP.Text = mAsyncSocket.GetMyIPAddress();
            mTxtServerPortNum.Text = AsyncSocket.DEFAULT_PORT_NUM.ToString();
        }

        /// <summary>
        /// ���� ���� ��ư Ŭ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnStartServer_Click(object sender, EventArgs e)
        {
            int portNum = 0;                                        // ���� port ��ȣ
            portNum = Int32.Parse(mTxtMyPortNum.Text.Trim());       // �ڽ��� port ��ȣ�� �Է�
            mAsyncSocket.StartServer(portNum);                       // ���� ����
        }

        /// <summary>
        /// ���� ���� ��ư Ŭ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnConnectToServer_Click(object sender, EventArgs e)
        {
            String serverIPAddress = "";                // ���� IP �ּ�
            int portNum = 0;                            // ���� port ��ȣ

            serverIPAddress = mTxtServerIP.Text.Trim(); // ������ ���� IP �ּ� ȹ��
            portNum = Int32.Parse(mTxtServerPortNum.Text.Trim());   // ������ ���� port ��ȣ�� ������

            mAsyncSocket.Connect(serverIPAddress, portNum);
        }

        /// <summary>
        /// ������ ��ư Ŭ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnSendMessage_Click(object sender, EventArgs e)
        {
            String message = "";
            String myIPAddress = "";
            message = mTxtInputMessage.Text;
            myIPAddress = mAsyncSocket.GetMyIPAddress();

            SendMessage(message);

            message = "�� (" + myIPAddress + ")\r\n" + message + "\r\n";

            AppendMessage(message);

            mTxtInputMessage.Text = "";
            mTxtInputMessage.Focus();
        }

        /// <summary>
        /// Ŭ���̾�Ʈ�κ��� �޽����� ������ ��� ȣ��
        /// </summary>
        /// <param name="message">Ŭ���̾�Ʈ�κ��� ������ �޽���</param>
        public void ReceiveMessage(String message)
        {
            String correspondentIPAddress = "";
            correspondentIPAddress = mAsyncSocket.GetCorrespondentIPAddress();

            message = "���� (" + correspondentIPAddress + ")\r\n" + message + "\r\n";

            AppendMessage(message);
        }

        /// <summary>
        /// SyncSockets Ŭ�����κ��� �ý����� ���� ���� ����
        /// </summary>
        /// <param name="message">SyncSockets Ŭ�����κ��� ������ ���� ����</param>
        public void NotifyMessage(String message)
        {
            message = "------�˸�!------\r\n" + message + "\r\n---------------\r\n";

            AppendMessage(message);
        }

        /// <summary>
        /// �޽��� ����
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(String message)
        {
            mAsyncSocket.Send(message);
        }

        /// <summary>
        /// Ű���� �Է� �̺�Ʈ �߻� ó��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mTxtInputMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String message = "";
                String myIPAddress = "";
                message = mTxtInputMessage.Text;
                myIPAddress = mAsyncSocket.GetMyIPAddress();

                SendMessage(message);

                message = "�� (" + myIPAddress + ")\r\n" + message;

                AppendMessage(message);

                mTxtInputMessage.Text = "";
                mTxtInputMessage.Focus();
            }
        }

        /// <summary>
        /// TextBox ��Ʈ�ѷ��� �޽��� �߰�
        /// </summary>
        /// <param name="message"></param>
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