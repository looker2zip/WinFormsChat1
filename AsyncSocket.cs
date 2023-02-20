using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;                                               // 네트워크 관련 클래스 사용
using System.Net.Sockets;                                       // Socket 클래스 사용
using System.Threading;                                         // Thread 사용

namespace WinFormsChat1
{
    class AsyncSocket
    {

        private const int BUFFER_SIZE = 1024;                   // 송·수신 버퍼 크기

        public const int DEFAULT_PORT_NUM = 3317;               // 기본 포트 번호

        private Chat mChatWnd = null;                           // chat form

        private String mMyIPAddress = "";                       // 자신의 IP Address
        private String mCorrespondentIPAddress = "";            // 상대방의 IP Address
        private String mServerIPAddress = "";                   // 서버 IP Address
        private String mClientIPAddress = "";                   // 클라이언트 IP Address

        private int mServerPortNum = 0;                         // 서버 port num

        private Socket mServerSocket = null;                    // 접속 대기용 소켓
        private Socket mClientSocket = null;                    // 클라이언트용 소켓

        private Thread mServerThread = null;                    // 서버 시작 스레드
        private Thread mReceiverThread = null;                  // 클라이언트로부터 수신 스레드 

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public AsyncSocket()
        {

        }

        /// <summary>
        /// chat창 객체를 파라미터로 받는 생성자
        /// </summary>
        /// <param name="chat">chat창 객체 변수</param>
        public AsyncSocket(Chat chat)
        {
            mChatWnd = chat;                                    // main chat form의 객체를 연결
            Init();                                             // 초기화 작업
        }

        /// <summary>
        /// 소켓이 생성될때 필요한 초기화 작업을 수행
        /// </summary>
        private void Init()
        {
            SetMyIPAddress();                                   // 내 IP address 설정
        }

        /// <summary>
        /// 서버에 접속
        /// </summary>
        /// <param name="address">서버의 IP 주소</param>
        /// <param name="portNum">서버의 port 번호</param>
        public void Connect(String address, int portNum)
        {
            mServerIPAddress = address;
            mServerPortNum = portNum;

            // 접속할 서버의 IPEndPoint 객체 생성
            IPEndPoint serverIpep = new IPEndPoint(IPAddress.Parse(mServerIPAddress), mServerPortNum);
            // 데이터 통신에 사용할 클라이언트용 소켓 생성
            mClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 서버에 접속
            mClientSocket.Connect(serverIpep);
            mChatWnd.NotifyMessage("서버에 접속하였습니다.");

            mCorrespondentIPAddress = mServerIPAddress;             // 상대방 IP 주소를 저장
            // 메시지 수신을 전담할 스레드 생성
            mReceiverThread = new Thread(new ThreadStart(Receive));
            mReceiverThread.Start();                                // 메시지 수신 스레드 시작
            mChatWnd.NotifyMessage("서버로부터 메시지 수신을 시작합니다.");
        }

        /// <summary>
        /// 서버와 접속 종료
        /// </summary>
        public void Disconnect()
        {
            // 클라이언트용 소켓을 닫음. 소켓을 닫기 전에 발생할 수 있는 error 처리
            if (mClientSocket == null)              // 소켓 객체가 null일 경우. error 처리
            {
                mChatWnd.NotifyMessage("에러!\r\n 클라이언트 소켓 객체가 null입니다.");
                return;
            }
            if (mClientSocket.Connected == false)   // 서버에 연결이 되어 있지 않을 경우. error 처리
            {
                mChatWnd.NotifyMessage("에러!\r\n 클라이언트 소켓이 접속되어 있지 않습니다.");
            }
            mClientSocket.Close();                  // 클라이언트용 소켓을 닫음. 소켓 자원을 반환

            // 데이터 수신 스레드를 종료함. 종료하기 전에 발생할 수 있는 error 처리
            if (mReceiverThread == null)            // 수신 스레드 객체가 null일 경우. error 처리
            {
                mChatWnd.NotifyMessage("에러!\r\n 수신 스레드 객체가 null입니다.");
                return;
            }
            if (mReceiverThread.IsAlive == false)   // 수신 스레드가 동작하지 않을 경우. error 처리
            {
                mChatWnd.NotifyMessage("에러!\r\n 수신 스레드 객체가 동작하고 있지 않습니다.");
                return;
            }
            mReceiverThread.Abort();                // 데이터 수신 스레드를 종료
        }

        /// <summary>
        /// 서버를 시작
        /// </summary>
        /// <param name="portNum">서버 Port 번호</param>
        public void StartServer(int portNum)
        {
            mServerIPAddress = mMyIPAddress;
            mServerPortNum = portNum;

            mServerThread = new Thread(new ThreadStart(WaitConnection));    // 접속 대기 함수를 스레드에 연결
            mServerThread.Start();                                          // 접속 대기 함수 시작
        }

        /// <summary>
        /// 서버 중지
        /// </summary>
        public void StopServer()
        {
            // 클라이언트용 소켓을 닫음. 소켓을 닫기 전에 발생할 수 있는 error 처리
            if (mClientSocket != null)
            {
                if (mClientSocket.Connected == true)
                {
                    mClientSocket.Close();                      // 클라이언트용 소켓을 닫음
                }
            }

            // 서버용 소켓을 닫음. 소켓을 닫기 전에 발생할 수 있는 error 처리
            if (mServerSocket != null)
            {
                if (mServerSocket.Connected == true)
                {
                    mServerSocket.Close();                      // 서버용 소켓을 닫음
                }
            }

            // 데이터 수신 스레드를 종료함. 종료하기 전에 발생할 수 있는 error 처리
            if (mReceiverThread == null)                        // 수신 스레드 객체가 null일 경우. error 처리
            {
                mChatWnd.NotifyMessage("에러!\r\n 수신 스레드 객체가 null입니다.");
                return;
            }
            if (mReceiverThread.IsAlive == false)               // 수신 스레드가 동작하지 않을 경우. error 처리
            {
                mChatWnd.NotifyMessage("에러!\r\n 수신 스레드 객체가 동작하고 있지 않습니다.");
                return;
            }
            mReceiverThread.Abort();                            // 데이터 수신 스레드를 종료
        }

        /// <summary>
        /// 서버용 접속 대기 함수
        /// </summary>
        private void WaitConnection()
        {
            // 서버의 IPEndPoint 객체를 생성
            IPEndPoint serverIpep = new IPEndPoint(IPAddress.Any, mServerPortNum);
            IPEndPoint clientIpep = null;
            // 서버에서 접속 대기용으로 사용할 서버용 소켓을 생성
            mServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mServerSocket.Bind(serverIpep);                     // 생성한 서버 소켓에 IP주소와 Port 번호를 지정
            mServerSocket.Listen(10);                           // 최대 접속 가능한 클라이언트의 수를 지정

            mChatWnd.NotifyMessage("서버가 시작되었습니다.\r\n 클라이언트의 접속을 대기합니다.");

            mClientSocket = mServerSocket.Accept();             // 클라이언트가 접속할 경우
            // 접속한 클라이언트의 IPEndPoint 객체를 얻음
            clientIpep = (IPEndPoint)mClientSocket.RemoteEndPoint;
            mChatWnd.NotifyMessage("IP주소 : " + clientIpep.Address.ToString() + "의 클라이언트가 접속하였습니다.");
            // 상대방의 IP 주소를 저장
            mCorrespondentIPAddress = clientIpep.Address.ToString();
            mReceiverThread = new Thread(new ThreadStart(Receive)); // 데이터 수신 함수를 스레드에 지정
            mReceiverThread.Start();                                // 데이터 수신 함수 시작
            mChatWnd.NotifyMessage("클라이언트로부터 메시지 수신을 시작합니다.");
        }

        /// <summary>
        /// 상대방 호스트로부터 데이터 수신
        /// </summary>
        private void Receive()
        {
            String message = "";                            // 수신한 message  
            byte[] data = null;                             // 수신한 raw data

            data = new byte[BUFFER_SIZE];

            while (true)
            {
                if (mClientSocket == null)                  // socket 객체가 null일 경우 error 처리
                {
                    mChatWnd.NotifyMessage("에러!\r\n 소켓 객체가 null입니다.");
                    break;
                }
                if (mClientSocket.Connected == false)       // socket 객체가 연결이 되어 있지 않을 경우 error 처리
                {
                    mChatWnd.NotifyMessage("에러!\r\n 연결되어 있지 않습니다.");
                    break;
                }

                // 데이터 수신
                mClientSocket.Receive(data, SocketFlags.None);

                message = Encoding.Default.GetString(data); // 수신한 데이터를 String 형태로 변환

                mChatWnd.ReceiveMessage(message);           // chat 창에 메시지 전달
            }
        }

        /// <summary>
        /// 문자열 형태의 message를 전송
        /// </summary>
        /// <param name="message">전송할 문자열</param>
        public void Send(String message)
        {
            byte[] data = null;                         // 전송할 raw data

            if (mClientSocket == null)                  // socket 객체가 null일 경우 error 처리
            {
                mChatWnd.NotifyMessage("에러!\r\n 소켓 객체가 null입니다.\r\n 메시지를 전송할 수 없습니다.");
                return;
            }
            if (mClientSocket.Connected == false)       // socket 객체가 연결이 되어 있지 않을 경우 error 처리
            {
                mChatWnd.NotifyMessage("에러!\r\n 연결되어 있지 않습니다.\r\n 메시지를 전송할 수 없습니다.");
                return;
            }

            data = Encoding.Default.GetBytes(message);  // String 형태의 message 값을 byte 배열 형태로 변환

            mClientSocket.Send(data, 0, data.Length, SocketFlags.None);
        }

        /// <summary>
        /// 자신의 IP 주소를 구하여 멤버 변수 mMyIPAddress에 저장
        /// </summary>
        private void SetMyIPAddress()
        {
            String myIPAddress = "";
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    myIPAddress = ip.ToString();
                    break;
                }
            }
            mMyIPAddress = myIPAddress;
        }

        /// <summary>
        /// 자신의 IP 주소를 반환
        /// </summary>
        /// <returns>자신의 IP 주소</returns>
        public String GetMyIPAddress()
        {
            return mMyIPAddress;
        }

        /// <summary>
        /// 상대방의 IP 주소를 반환
        /// </summary>
        /// <returns>상대방의 IP 주소</returns>
        public String GetCorrespondentIPAddress()
        {
            return mCorrespondentIPAddress;
        }

    }
}
