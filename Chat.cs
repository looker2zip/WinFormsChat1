using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;                               // Socket 클래스 사용
using System.Net;                                       // 네트워크 관련 클래스 사용

namespace WinFormsChat1
{
    public partial class Chat : Form
    {
        delegate void UpdateTextCallback(String message);

        /// <summary>
        /// Chat 클래스 생성자
        /// </summary>
        public Chat()
        {
            InitializeComponent();

            mTxtMyIP.Text = "127.0.0.1";                // 자신을 가르키는 IP 주소
            mTxtMyPortNum.Text = "3317";                // 채팅 프로그램에서 사용할 port 번호
            mTxtServerIP.Text = "127.0.0.1";            // 서버 IP 주소
            mTxtServerPortNum.Text = "3317";            // 서버 port 번호
        }

        /// <summary>
        /// 서버 시작 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnStartServer_Click(object sender, EventArgs e)
        {
            Socket server = null;                   // 서버로 사용할 소켓
            Socket client = null;                   // 접속한 클라이언트를 나타내는 소켓
            byte[] data = new byte[1024];           // 데이터를 수신할 byte 배열
            String message = "";                    // 상대방으로부터 수신한 메시지

            // 서버에서 사용할 Port 번호
            int portNum = Int32.Parse(mTxtMyPortNum.Text.ToString());

            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, portNum);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(ipep);
            server.Listen(10);                      // 처리 대기큐 최대 크기

            NotifyMessage("서버를 시작합니다.\n클라이언트의 접속을 대기합니다.");

            client = server.Accept();               // 클라이너트 접속 대기

            NotifyMessage("클라이언트가 접속하였습니다.");

            client.Receive(data);                   // 클라이언트로부터 데이터 수신

            message = Encoding.Default.GetString(data);
            message = "상대방: " + message;
            AppendMessage(message);

            client.Close();                         // 클라이언트 소켓 닫기
            server.Close();                         // 서버 소켓 닫기
        }

        /// <summary>
        /// 서버 접속 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mBtnConnectToServer_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1024];
            String serverIPAddress = "";                // 서버 IP 주소
            int portNum = 0;                            // 서버 port 번호

            serverIPAddress = mTxtServerIP.Text.Trim(); // 접속할 서버 IP 주소 획득
            portNum = Int32.Parse(mTxtServerPortNum.Text.Trim());   // 접속할 서버 port 번호를 가져옴

            IPAddress ipAddress = IPAddress.Parse(serverIPAddress); // 접속할 서버의 IP 주소
            IPEndPoint ipep = new IPEndPoint(ipAddress, portNum);   // 접속할 서버를 지정

            // 서버/ 소켓 생성
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            NotifyMessage("서버에 접속합니다.");

            server.Connect(ipep);                               // 서버에 접속

            NotifyMessage("서버에 접속하였습니다.");

            data = Encoding.Default.GetBytes("클라이언트에서 보내는 메시지입니다.");
            server.Send(data);                                  // 서버에 대이터 전송

            NotifyMessage("서버에 데이터를 전송하였습니다.");

            server.Close();                                     // 서버 소켓 닫기
        }

        /// <summary>
        /// 알림 메시지 전달
        /// </summary>
        /// <param name="message">SyncSockets 클래스로부터 수신한 공지 사항</param>
        public void NotifyMessage(String message)
        {
            message = "------알림!------\r\n" + message + "\r\n---------------------\r\n";

            AppendMessage(message);
        }

        /// <summary>
        /// 멀티 스레딩에서 사용할 delegate 함수
        /// </summary>
        /// <param name="message">출력할 메시지</param>
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