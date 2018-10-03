﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;

namespace NJAuction
{
    public partial class Form1 : Form
    {
        [DllImport("ImageSearchDLL.dll")]
        private static extern IntPtr ImageSearch(int x, int y, int right, int bottom, [MarshalAs(UnmanagedType.LPStr)]string imagePath);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKey);

        [DllImport("user32.dll")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll")]
        private static extern void SetForegroundWindow(int hwnd);
        [DllImport("user32.dll")]
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, string lParam);
        [DllImport("user32.dll")]
        public static extern uint PostMessage(int hwnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte vk, byte scan, int flags, ref int extrainfo);
        [DllImport("user32.dll")]
        internal extern static Int32 SetCursorPos(int x, int y);

        [DllImport("user32.dll")] // 입력 제어
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, int dwData, int dwExtraInfo);

        static int timestep = 15;

        const int KEYDOWN = 0x0001;
        const int KEYUP = 0x0002;

        const byte VK_V = 0x56;
        const byte VK_BACK = 0x08;
        const byte Ctrl = 0x11;
        const byte EnterKey = 0x0D;

        static int globalX=0;
        static int globalY=0;

        public static int threadtype = 0;

        private const uint LBDOWN = 0x00000002; // 왼쪽 마우스 버튼 눌림
        private const uint LBUP = 0x00000004; // 왼쪽 마우스 버튼 떼어짐
        private const uint RBDOWN = 0x00000008; // 오른쪽 마우스 버튼 눌림
        private const uint RBUP = 0x000000010; // 오른쪽 마우스 버튼 떼어짐
        private const uint MBDOWN = 0x00000020; // 휠 버튼 눌림
        private const uint MBUP = 0x000000040; // 휠 버튼 떼어짐
        private const uint WHEEL = 0x00000800; //휠 스크롤
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x0101;

        static Label lb1;
        static TextBox tb1;

        static List<Item> itemlist;

        static Thread keyAsyncTrhead;
        static Thread Auction_Thread;
        static Thread CashAuction_Thread;

        static bool CashAuction_Thread_Flag = false;
        static bool Auction_Thread_Flag = false;
        static bool KeyAsync_Thread_Flag = false;

        static int hWnd = FindWindow(null, "MapleStory");

        public Form1()
        {            
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1100, 0);

            lb1 = this.label1;
            tb1 = this.textBox1;
        }

        public void addComponent(string str)
        {
            //listView2.Items.Add()
        }

        public static void buy()
        {
            int Info = 0;
            SetCursorPos(639, 225); //맨위아이템

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(timestep);

            SetCursorPos(639, 225); // 맨위아이템

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(20);

            SetCursorPos(938, 750); // 구매하기버튼

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            if(threadtype==1)
            {
                Thread.Sleep(timestep);

                Clipboard.SetText("9999");

                keybd_event(Ctrl, 0, 0, ref Info);

                keybd_event(VK_V, 0, 0, ref Info);

                Thread.Sleep(10);

                keybd_event(VK_V, 0, KEYUP, ref Info);

                keybd_event(Ctrl, 0, KEYUP, ref Info);
            }

            Thread.Sleep(timestep);
            
            SetCursorPos(476, 528); // 구매하기 버튼

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(timestep);

            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
            Thread.Sleep(10);
            keybd_event(EnterKey, 0, KEYUP, ref Info);

            Thread.Sleep(timestep);

            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
            Thread.Sleep(10);
            keybd_event(EnterKey, 0, KEYUP, ref Info);

            Thread.Sleep(timestep);

            SetCursorPos(476, 528); // 구매하기 버튼

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(75);
        }

        public static void singleBuy()
        {
            string[] search;           
            int Info = 0;

            //MOUSE CLICK VERSION CODE
            SetCursorPos(globalX, globalY); //검색하기 버튼 위치

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            //mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            //mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(timestep);

            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                        
            Thread.Sleep(timestep);

            search = UseImageSearch("*50 img\\itemOK.png");
            if (search == null)
            {

            }
            else
            {
                keybd_event(EnterKey, 0, KEYUP, ref Info);

                buy();
            }
            
            //IMAGE SEARCH VERSION CODE
            /*
            search = UseImageSearch("*50 img\\search.png");
            if (search == null)
            {
                SetCursorPos(639, 225); // 소비템 입력창

                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고
            }
            else
            {
                X = Convert.ToInt32(search[1]);
                Y = Convert.ToInt32(search[2]);

                MessageBox.Show(X + "," + Y + "");

                SetCursorPos(X, Y); // 기타탬 탭 위치

                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                Thread.Sleep(timestep);
                
                keybd_event(EnterKey, 0, KEYDOWN, ref Info);


                Thread.Sleep(1);
                keybd_event(EnterKey, 0, KEYUP, ref Info);

                Thread.Sleep(timestep);

                keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                Thread.Sleep(1);
                keybd_event(EnterKey, 0, KEYUP, ref Info);

                Thread.Sleep(timestep);
                
                search = UseImageSearch("*50 img\\itemOK.png");
                if (search == null)
                {

                }
                else
                {
                    buy();
                }
                
            }
            */
        }

        static void ThreadProc()
        {
            for (; ; )
            {
                                
                //SetWindowPos(hWnd, 0, 1, 1, 800, 600, 0x01);

                if (GetAsyncKeyState(112) == -32767)
                {                    
                    if (Auction_Thread_Flag == true)
                    {
                        Auction_Thread.Abort();
                        lb1.Text = "현재상태 : 휴식중";
                        AutoClosingMessageBox.Show("Thread STOP!", "알림", 1200);                               
                    }
                    if (CashAuction_Thread_Flag == true)
                    {
                        CashAuction_Thread.Abort();
                        lb1.Text = "현재상태 : 휴식중";
                        AutoClosingMessageBox.Show("Thread STOP!", "알림", 1200);                        
                    }
                }
                if (GetAsyncKeyState(113) == -32767)
                {
                    foreach(Process process in Process.GetProcesses())
                    {
                        if (process.ProcessName.ToUpper().StartsWith("NJ"))
                        {
                            process.Kill();
                        }
                    }
                }
                if (GetAsyncKeyState(122) == -32767)
                {
                    SetForegroundWindow(hWnd);
                    SetWindowPos(hWnd, 0, 1, 1, 800, 600, 0x01);

                    string[] search;
                    search = UseImageSearch("*50 img\\search.png");
                    if (search == null)
                    {

                    }
                    else
                    {
                        globalX = Convert.ToInt32(search[1]);
                        globalY = Convert.ToInt32(search[2]);
                    }

                    threadtype = 1;

                    timestep = int.Parse(tb1.Text.ToString());

                    Auction_Thread = new Thread(new ThreadStart(ThreadAuction));
                    Auction_Thread.SetApartmentState(ApartmentState.STA);
                    Auction_Thread.Start();
                    Auction_Thread_Flag = true;
                    lb1.Text = "현재상태 : 매크로 작동중";
                }
                if (GetAsyncKeyState(123) == -32767)
                {
                    SetForegroundWindow(hWnd);
                    SetWindowPos(hWnd, 0, 1, 1, 800, 600, 0x01);

                    string[] search;
                    search = UseImageSearch("*50 img\\search.png");
                    if (search == null)
                    {

                    }
                    else
                    {
                        globalX = Convert.ToInt32(search[1]);
                        globalY = Convert.ToInt32(search[2]);
                    }

                    threadtype = 2;
                    timestep = int.Parse(tb1.Text.ToString());

                    CashAuction_Thread = new Thread(new ThreadStart(ThreadCashAuction));
                    CashAuction_Thread.SetApartmentState(ApartmentState.STA);
                    CashAuction_Thread.Start();
                    CashAuction_Thread_Flag = true;

                    lb1.Text = "현재상태 : 매크로 작동중";
                }         
            }
        }
        [STAThreadAttribute]
        static void ThreadCashAuction()
        {            
            for (; ; )
            {
                string[] search;
                int Info = 0;

                //MOUSE CLICK VERSION CODE
                SetCursorPos(globalX, globalY); //검색하기 버튼 위치

                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                Thread.Sleep(timestep);

                keybd_event(EnterKey, 0, KEYDOWN, ref Info);

                Thread.Sleep(1);
                keybd_event(EnterKey, 0, KEYUP, ref Info);

                Thread.Sleep(timestep);

                keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                Thread.Sleep(1);
                keybd_event(EnterKey, 0, KEYUP, ref Info);

                Thread.Sleep(timestep);

                search = UseImageSearch("*50 img\\itemOK.png");
                if (search == null)
                {

                }
                else
                {                    
                    SetCursorPos(639, 225); //맨위아이템

                    mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                    mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                    Thread.Sleep(25);

                    SetCursorPos(639, 225); // 맨위아이템

                    mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                    mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                    Thread.Sleep(25);

                    SetCursorPos(938, 750); // 구매하기버튼

                    mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                    mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                    Thread.Sleep(25);

                    SetCursorPos(476, 528); // 구매하기 버튼

                    mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                    mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                    Thread.Sleep(25);

                    keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                    Thread.Sleep(timestep);
                    keybd_event(EnterKey, 0, KEYUP, ref Info);

                    Thread.Sleep(25);

                    keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                    Thread.Sleep(timestep);
                    keybd_event(EnterKey, 0, KEYUP, ref Info);

                    Thread.Sleep(25);

                    SetCursorPos(476, 528); // 구매하기 버튼

                    mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                    mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                    Thread.Sleep(45);
                }
            }
        }
        [STAThreadAttribute]
        static void ThreadAuction()
        {
            int currentCount = 0;
            for (; ; )
            {
                string[] search;
                int X;
                int Y;

                singleBuy();
                currentCount++;
                
                if (currentCount == 10000)
                {
                    currentCount = 0;
                    search = UseImageSearch("*50 img\\complete.png");
                    if (search == null)
                    {
                        continue;
                    }
                    else
                    {
                        X = Convert.ToInt32(search[1]);
                        Y = Convert.ToInt32(search[2]);

                        SetCursorPos(X, Y); // 기타탬 탭 위치

                        mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                        mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                        Thread.Sleep(50);

                        mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                        mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                        search = UseImageSearch("*50 img\\allclick.png");
                        if (search == null)
                        {
                            search = UseImageSearch("*50 img\\searchmenu.png");
                            if (search == null)
                            {
                                continue;
                            }
                            else
                            {
                                X = Convert.ToInt32(search[1]);
                                Y = Convert.ToInt32(search[2]);

                                SetCursorPos(X, Y); // 기타탬 탭 위치

                                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                                mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                                Thread.Sleep(50);
                            }
                        }
                        else
                        {
                            X = Convert.ToInt32(search[1]);
                            Y = Convert.ToInt32(search[2]);

                            SetCursorPos(X, Y); // 기타탬 탭 위치

                            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                            Thread.Sleep(50);

                            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                            int Info = 0;
                            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                            Thread.Sleep(10);
                            keybd_event(EnterKey, 0, KEYUP, ref Info);

                            Thread.Sleep(10000);

                            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                            Thread.Sleep(10);
                            keybd_event(EnterKey, 0, KEYUP, ref Info);

                            Thread.Sleep(500);

                            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                            Thread.Sleep(10);
                            keybd_event(EnterKey, 0, KEYUP, ref Info);

                            Thread.Sleep(500);

                            search = UseImageSearch("*50 img\\searchmenu.png");
                            if (search == null)
                            {
                                continue;
                            }
                            else
                            {
                                X = Convert.ToInt32(search[1]);
                                Y = Convert.ToInt32(search[2]);

                                SetCursorPos(X, Y); // 기타탬 탭 위치

                                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                                mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                                Thread.Sleep(50);
                            }
                        }
                    }
                }              
            }   
        }

        public static String[] UseImageSearch(string imgPath)
        {
            int right = Screen.PrimaryScreen.Bounds.Right;
            int bottom = Screen.PrimaryScreen.Bounds.Bottom;
            IntPtr result = ImageSearch(0, 0, right, bottom, imgPath);
            String res = Marshal.PtrToStringAnsi(result);

            //이미지 서치 결과값  0번 =  결과 성공1 실패0 1,2번 = x,y 3,4번 = 이미지의 세로가로길이
            //res = 한자씩 나눠져있음

            if (res[0] == '0') //res를 이용하여 이미지여부 확인
            {
                //MessageBox.Show("Not found");
                return null;
            } 

            String[] data = res.Split('|'); // |로 결과 값을 조각
            int x; int y;
            int.TryParse(data[1], out x); //x좌표
            int.TryParse(data[2], out y); //y좌표

            return data;
        }
      

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(KeyAsync_Thread_Flag)            
                keyAsyncTrhead.Abort();
            
            if (Auction_Thread_Flag)
                Auction_Thread.Abort();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            int hWnd = FindWindow(null, "MapleStory");
            SetForegroundWindow(hWnd);
            SetWindowPos(hWnd, 0, 1, 1, 800, 600, 0x01);

            keyAsyncTrhead = new Thread(new ThreadStart(ThreadProc));
            keyAsyncTrhead.Start();
            KeyAsync_Thread_Flag = true;
        }

        
        private void button1_Click_1(object sender, EventArgs e)
        {            
            SetForegroundWindow(hWnd);
            SetWindowPos(hWnd, 0, 1, 1, 800, 600, 0x01);

            string[] search;
            search = UseImageSearch("*50 img\\search.png");
            if (search == null)
            {
                
            }
            else
            {
                globalX = Convert.ToInt32(search[1]);
                globalY = Convert.ToInt32(search[2]);
            }            
            
            threadtype = 1;
            
            timestep = int.Parse(textBox1.Text.ToString());
        
            Auction_Thread = new Thread(new ThreadStart(ThreadAuction));
            Auction_Thread.SetApartmentState(ApartmentState.STA);
            Auction_Thread.Start();
            Auction_Thread_Flag = true;
            label1.Text = "현재상태 : 매크로 작동중";
        }
        private void button3_Click(object sender, EventArgs e)
        {            
            SetForegroundWindow(hWnd);
            SetWindowPos(hWnd, 0, 1, 1, 800, 600, 0x01);

            string[] search;
            search = UseImageSearch("*50 img\\search.png");
            if (search == null)
            {

            }
            else
            {
                globalX = Convert.ToInt32(search[1]);
                globalY = Convert.ToInt32(search[2]);
            }

            threadtype = 2;
            timestep = int.Parse(textBox1.Text.ToString());
            
            CashAuction_Thread = new Thread(new ThreadStart(ThreadCashAuction));
            CashAuction_Thread.SetApartmentState(ApartmentState.STA);
            CashAuction_Thread.Start();
            CashAuction_Thread_Flag = true;

            label1.Text = "현재상태 : 매크로 작동중";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Auction_Thread_Flag == true)
            {
                Auction_Thread.Abort();
                AutoClosingMessageBox.Show("Thread STOP!", "알림", 1200);

            }
            if (CashAuction_Thread_Flag == true)
            {
                CashAuction_Thread.Abort();
                AutoClosingMessageBox.Show("Thread STOP!", "알림", 1200);
            }
            label1.Text = "현재상태 : 휴식중";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName.ToUpper().StartsWith("NJ"))
                {
                    process.Kill();
                }
            }
        }

        
    }    
}
