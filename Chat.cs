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

        AsyncSocket mAsyncSocket = null;                // 동기 소켓으로 작동하는 사용자 정의 소켓 클래스 

        /// <summary>
        /// Chat 클래스 생성자
        /// </summary>
        public Chat()
        {
            InitializeComponent();

            mAsyncSocket = new AsyncSocket(this);       // 사용자 정의 소켓 클래스 생성
            mTxtMyIP.Text = mAsyncSocket.GetMyIPAddress();
            mTxtMyPortNum.Text = AsyncSocket.DEFAULT_PORT_NUM.ToString();
            mTxtServerIP.Text = mAsyncSocket.GetMyIPAddress();
            mTxtServerPortNum.Text = AsyncSocket.DEFAULT_PORT_NUM.ToString();
        }

        /// <summary>
        /// 서버 시작 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnStartServer_Click(object sender, EventArgs e)
        {
            int portNum = 0;                                        // 서버 port 번호
            portNum = Int32.Parse(mTxtMyPortNum.Text.Trim());       // 자신의 port 번호를 입력
            mAsyncSocket.StartServer(portNum);                       // 서버 시작
        }

        /// <summary>
        /// 서버 접속 버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnConnectToServer_Click(object sender, EventArgs e)
        {
            String serverIPAddress = "";                // 서버 IP 주소
            int portNum = 0;                            // 서버 port 번호

            serverIPAddress = mTxtServerIP.Text.Trim(); // 접속할 서버 IP 주소 획득
            portNum = Int32.Parse(mTxtServerPortNum.Text.Trim());   // 접속할 서버 port 번호를 가져옴

            mAsyncSocket.Connect(serverIPAddress, portNum);
        }

        /// <summary>
        /// 보내기 버튼 클릭
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

            message = "나 (" + myIPAddress + ")\r\n" + message + "\r\n";

            AppendMessage(message);

            mTxtInputMessage.Text = "";
            mTxtInputMessage.Focus();
        }

        /// <summary>
        /// 클라이언트로부터 메시지를 수신할 경우 호출
        /// </summary>
        /// <param name="message">클라이언트로부터 수신한 메시지</param>
        public void ReceiveMessage(String message)
        {
            String correspondentIPAddress = "";
            correspondentIPAddress = mAsyncSocket.GetCorrespondentIPAddress();

            message = "상대방 (" + correspondentIPAddress + ")\r\n" + message + "\r\n";

            AppendMessage(message);
        }

        /// <summary>
        /// SyncSockets 클래스로부터 시스템적 전달 사항 수신
        /// </summary>
        /// <param name="message">SyncSockets 클래스로부터 수신한 공지 사항</param>
        public void NotifyMessage(String message)
        {
            message = "------알림!------\r\n" + message + "\r\n---------------\r\n";

            AppendMessage(message);
        }

        /// <summary>
        /// 메시지 전송
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(String message)
        {
            mAsyncSocket.Send(message);
        }

        /// <summary>
        /// 키보드 입력 이벤트 발생 처리
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

                message = "나 (" + myIPAddress + ")\r\n" + message;

                AppendMessage(message);

                mTxtInputMessage.Text = "";
                mTxtInputMessage.Focus();
            }
        }

        /// <summary>
        /// TextBox 컨트롤러에 메시지 추가
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