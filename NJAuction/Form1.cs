using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Imaging;


namespace NJAuction
{
    public partial class Form1 : Form
    {
        
            
        [DllImport("ImageSearchDLL.dll")]
        private static extern IntPtr ImageSearch(int x, int y, int right, int bottom, [MarshalAs(UnmanagedType.LPStr)]string imagePath);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int GetAsyncKeyState(int vKey);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

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

        [DllImport("user32.dll", SetLastError = true)]

        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [DllImport("user32.dll", SetLastError = true)]

        static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        [DllImport("gdi32.dll")]

        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);
        
        const int KEYDOWN = 0x0001;
        const int KEYUP = 0x0002;

        const int Shift = 0x10;
        const int insert = 0x2D;

        const byte Ctrl = 0x11;
        const byte VK_V = 0x56;
        const byte VK_BACK = 0x08;        

        const byte EnterKey = 0x0D;

        const byte zero = 0x30;
        const byte one = 0x31;
        const byte two = 0x32;
        const byte three = 0x33;
        const byte four = 0x34;
        const byte five = 0x35;
        const byte six = 0x36;
        const byte seven = 0x37;
        const byte eight = 0x38;
        const byte nineKey = 0x39;

        const byte F12KEY = 0x7B;

        static int globalX=0;
        static int globalY=0;

        static int buyCount = 0;

        static string[] search;

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

        static int timestep = 35;

        static Label lb1;
        static TextBox tb1;
        static TextBox tb2;
        static PictureBox PB;
        static ListBox LogBox;
        static CheckBox CB1,CB2;


        static List<Item> itemlist;

        static Thread keyAsyncTrhead;
        static Thread Sell_Thread;
        static Thread Auction_Thread;
        static Thread CashAuction_Thread;

        static bool CashAuction_Thread_Flag = false;
        static bool Auction_Thread_Flag = false;
        static bool KeyAsync_Thread_Flag = false;
        static bool Sell_Thread_Flag = false;

        static int hWnd = FindWindow(null, "MapleStory");

        static int right;
        static int bottom;

        static Rectangle rect;
        static int bitsPerPixel;
        static Bitmap bitmap;
        static PixelFormat pixelFormat;
        static Graphics gr;
        static Color pixelcolor;

        static Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);        
        
        static public List<sellItem> sellItemList = new List<sellItem>();


        static HttpWebRequest wReq;
        static HttpWebResponse wRes;

        static string SYSTEMID = "";

        public Form1()
        {            
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1100, 0);

            System.DateTime.Now.ToString("yyyy");
            DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

            lb1 = this.label1;
            tb2 = this.textBox2;
            LogBox = this.listBox1;

            right = Screen.PrimaryScreen.Bounds.Right;
            bottom = Screen.PrimaryScreen.Bounds.Bottom;

            lb1.Text = buyCount.ToString();

            string currTime = getCurrentTime();
            LogBox.Items.Add(currTime + " 프로그램 시작");

        }

        public static string getCurrentTime()
        {
            System.DateTime.Now.ToString("yyyy");
            return DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        public void addComponent(string str)
        {
            //listView2.Items.Add()
        }

        public static void HttpGet(string type)
        {
            string url = "http://220.69.209.170/nj/insert.php?";
            url += "id="+SYSTEMID;
            url += "&time="+getCurrentTime();
            url += "&status=" + type;

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
        }
        
        public static void buy(int Info)
        {
            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
            SetCursorPos(639, 225); //맨위아이템
            for (; ;)
            {                
                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고
                if(get_window_pixel(533, 231).Equals("119")) break;
            }
            
            SetCursorPos(938, 750); // 구매하기버튼
            for (; ; )
            {
                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고
                if (get_window_pixel(431, 523).Equals("170")) break;
            }
            if(threadtype==1)
            {
                string str = tb2.Text.ToString();
                str_to_dec_write(str);

                keybd_event(EnterKey, 0, KEYDOWN, ref Info);
                Thread.Sleep(5);
                keybd_event(EnterKey, 0, KEYUP, ref Info);
            }
            keybd_event(EnterKey, 0, KEYUP, ref Info);

            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
            Thread.Sleep(1);
            keybd_event(EnterKey, 0, KEYUP, ref Info);

            HttpGet("구매시도");           
            
        }

        public static void singleBuy(int Info)
        {            
            //MOUSE CLICK VERSION CODE
            SetCursorPos(globalX, globalY); //검색하기 버튼 위치

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
            Thread.Sleep(1);
            keybd_event(EnterKey, 0, KEYUP, ref Info);

            if (!get_window_pixel(604, 162).Equals("34") && get_window_pixel(284, 217).Equals("204"))
            {
                buy(Info);

                string currTime = getCurrentTime();
                LogBox.Items.Add(currTime + " 구매 시도 ");
                LogBox.SelectedIndex = LogBox.Items.Count - 1;

                buyCount++;

                lb1.Text = buyCount.ToString();
                Thread.Sleep(1);
            }
        }

        [STAThreadAttribute]
        static void ThreadProc()
        {
            int Info = 0;
            {                   
                if (GetAsyncKeyState(112) == -32767)
                {
                    if (Auction_Thread_Flag == true)
                    {
                        Auction_Thread.Abort();
                        AutoClosingMessageBox.Show("Thread STOP!", "알림", 1200);
                        string currTime = getCurrentTime();
                        LogBox.Items.Add(currTime + " 매크로 중지");
                        LogBox.SelectedIndex = LogBox.Items.Count - 1;
                    }
                    if (CashAuction_Thread_Flag == true)
                    {
                        CashAuction_Thread.Abort();
                        AutoClosingMessageBox.Show("Thread STOP!", "알림", 1200);
                        string currTime = getCurrentTime();
                        LogBox.Items.Add(currTime + " 매크로 중지");
                        LogBox.SelectedIndex = LogBox.Items.Count - 1;
                    }
                    if (Sell_Thread_Flag == true)
                    {
                        Sell_Thread.Abort();                        
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
                    
                    Auction_Thread = new Thread(new ThreadStart(ThreadAuction));
                    Auction_Thread.SetApartmentState(ApartmentState.STA);
                    Auction_Thread.Start();
                    Auction_Thread_Flag = true;

                    if (sellItemList.Count > 0)
                    {
                        Sell_Thread = new Thread(new ThreadStart(ThreadSell));
                        Sell_Thread.SetApartmentState(ApartmentState.STA);
                        Sell_Thread.Start();
                        Sell_Thread_Flag = true;
                    }

                    string currTime = getCurrentTime();
                    LogBox.Items.Add(currTime + " 소비/기타 시작");
                    LogBox.SelectedIndex = LogBox.Items.Count - 1;
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

                    CashAuction_Thread = new Thread(new ThreadStart(ThreadCashAuction));
                    CashAuction_Thread.SetApartmentState(ApartmentState.STA);
                    CashAuction_Thread.Start();
                    CashAuction_Thread_Flag = true;

                    if (sellItemList.Count > 0)
                    {
                        Sell_Thread = new Thread(new ThreadStart(ThreadSell));
                        Sell_Thread.SetApartmentState(ApartmentState.STA);
                        Sell_Thread.Start();
                        Sell_Thread_Flag = true;
                    }

                    string currTime = getCurrentTime();
                    LogBox.Items.Add(currTime + " 장비/캐시 시작");
                    LogBox.SelectedIndex = LogBox.Items.Count - 1;
                }         
            }
        }
        [STAThreadAttribute]
        static void ThreadCashAuction()
        {
            int Info = 0;
            for (; ; )
            {                
                singleBuy(Info);
            }
        }

        static void str_to_dec_write(string str)
        {
            int Info = 0;  
            for (int xxx = 0; xxx < str.Length; xxx++)
            {
                switch (int.Parse(str[xxx].ToString()))
                {
                    case 0:
                        keybd_event(zero, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(zero, 0, KEYUP, ref Info);
                        break;
                    case 1:
                        keybd_event(one, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(one, 0, KEYUP, ref Info);
                        break;
                    case 2:
                        keybd_event(two, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(two, 0, KEYUP, ref Info);
                        break;
                    case 3:
                        keybd_event(three, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(three, 0, KEYUP, ref Info);
                        break;
                    case 4:
                        keybd_event(four, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(four, 0, KEYUP, ref Info);
                        break;
                    case 5:
                        keybd_event(five, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(five, 0, KEYUP, ref Info);
                        break;
                    case 6:
                        keybd_event(six, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(six, 0, KEYUP, ref Info);
                        break;
                    case 7:
                        keybd_event(seven, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(seven, 0, KEYUP, ref Info);
                        break;
                    case 8:
                        keybd_event(eight, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(eight, 0, KEYUP, ref Info);
                        break;
                    case 9:
                        keybd_event(nineKey, 0, KEYDOWN, ref Info);
                        Thread.Sleep(10);
                        keybd_event(nineKey, 0, KEYUP, ref Info);
                        break;

                }
            }
        }

        [STAThreadAttribute]
        static void ThreadAuction()
        {
            int current_count = 0;
            int Info = 0;            
            int X;
            int Y;
            for (; ; )
            {
                singleBuy(Info);
                current_count++;
                if (current_count == 5000)
                {                    
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

                                string currTime = getCurrentTime();
                                LogBox.Items.Add(currTime + " 물품 수령/초기화");
                                LogBox.SelectedIndex = LogBox.Items.Count - 1;
                                current_count = 0;
                                Thread.Sleep(50);
                            }
                        }
                    }
                }              
            }   
        }

        static void application_item(int i)
        {
            int Info = 0;

            if (sellItemList.ElementAt(i).position == 1)
            {
                SetCursorPos(105, 180); // 첫칸 위치
            }
            else
            {
                SetCursorPos(155, 180); // 둘째칸 위치
            }


            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(500);

            SetCursorPos(850, 260); // 첫칸 위치

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(500);

            SetCursorPos(950, 284); // 첫칸 위치

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(400);

            string str = sellItemList.ElementAt(i).price.ToString();
            str_to_dec_write(str);

            Thread.Sleep(500);

            SetCursorPos(783, 285); // 첫칸 위치

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(400);

            str_to_dec_write("9999");

            Thread.Sleep(500);

            SetCursorPos(948, 354); // 첫칸 위치

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

            Thread.Sleep(400);

            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
            Thread.Sleep(10);
            keybd_event(EnterKey, 0, KEYUP, ref Info);

            Thread.Sleep(400);

            keybd_event(EnterKey, 0, KEYDOWN, ref Info);
            Thread.Sleep(10);
            keybd_event(EnterKey, 0, KEYUP, ref Info);

            Thread.Sleep(300);

            SetCursorPos(109, 123); // 검색탭

            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
            mouse_event(LBUP, 0, 0, 0, 0); // 떼고
        }

        static void ThreadSell()
        {
            int current_count = 0;
            int Info = 0;
            int X;
            int Y;
            string prev = "";
            
            Thread.Sleep(6000);            //매크로 시작 1분뒤에 판매 등록

            if (Auction_Thread_Flag)
            {
                prev = "a";
                Auction_Thread.Abort();
                Auction_Thread_Flag = false;
            }
            if (CashAuction_Thread_Flag)
            {
                prev = "c";
                CashAuction_Thread.Abort();
                CashAuction_Thread_Flag = false;
            }

            string currTime = getCurrentTime();
            LogBox.Items.Add(currTime + " 매크로 중지");
            LogBox.SelectedIndex = LogBox.Items.Count - 1;
            
            SetCursorPos(938, 750); // 구매하기버튼
            for (; ; )
            {
                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고
                if (get_window_pixel(388, 423).Equals("34")) break;
            }
            for (int i = 0; i < sellItemList.Count; i++)
            {
                //MessageBox.Show(sellItemList.ElementAt(i).getItemInformation());
                search = UseImageSearch("*50 img\\selltab.png");
                if (search == null)
                {
                    if (sellItemList.ElementAt(i).type)
                    {
                        search = UseImageSearch("*50 img\\sell_sobi_btn.png");
                        if (search == null)
                        {
                            application_item(i);
                        }
                        else
                        {
                            X = Convert.ToInt32(search[1]);
                            Y = Convert.ToInt32(search[2]);

                            SetCursorPos(X, Y); // 소비 탭 위치

                            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                            Thread.Sleep(50);

                            application_item(i);
                        }
                    }
                    else
                    {
                        search = UseImageSearch("*50 img\\sell_etc_btn.png");
                        if (search == null)
                        {
                            application_item(i);
                        }
                        else
                        {
                            X = Convert.ToInt32(search[1]);
                            Y = Convert.ToInt32(search[2]);

                            SetCursorPos(X, Y); // 소비 탭 위치

                            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                            Thread.Sleep(50);

                            application_item(i);
                        }
                    }
                }
                else
                {
                    X = Convert.ToInt32(search[1]);
                    Y = Convert.ToInt32(search[2]);

                    SetCursorPos(X, Y); // 판매 탭 위치

                    mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                    mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                    Thread.Sleep(500);

                    if (sellItemList.ElementAt(i).type)
                    {
                        search = UseImageSearch("*50 img\\sell_sobi_btn.png");
                        if (search == null)
                        {
                            application_item(i);
                        }
                        else
                        {
                            X = Convert.ToInt32(search[1]);
                            Y = Convert.ToInt32(search[2]);

                            SetCursorPos(X, Y); // 소비 탭 위치

                            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                            Thread.Sleep(50);

                            application_item(i);
                        }
                    }
                    else
                    {
                        search = UseImageSearch("*50 img\\sell_etc_btn.png");
                        if (search == null)
                        {
                            application_item(i);
                        }
                        else
                        {
                            X = Convert.ToInt32(search[1]);
                            Y = Convert.ToInt32(search[2]);

                            SetCursorPos(X, Y); // 소비 탭 위치

                            mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                            mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                            Thread.Sleep(50);

                            application_item(i);
                        }
                    }
                }
            }

            search = UseImageSearch("*50 img\\complete.png");
            if (search == null)
            {
                
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
                        
                    }
                    else
                    {
                        X = Convert.ToInt32(search[1]);
                        Y = Convert.ToInt32(search[2]);

                        SetCursorPos(X, Y); // 기타탬 탭 위치

                        mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                        mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                        currTime = getCurrentTime();
                        LogBox.Items.Add(currTime + " 물품 수령/초기화");
                        LogBox.SelectedIndex = LogBox.Items.Count - 1;
                        current_count = 0;
                        Thread.Sleep(50);
                    }
                }
            }


            search = UseImageSearch("*50 img\\searchtab.png");
            if (search == null)
            {
                
            }
            else
            {
                X = Convert.ToInt32(search[1]);
                Y = Convert.ToInt32(search[2]);

                SetCursorPos(X, Y); // 판매 탭 위치

                mouse_event(LBDOWN, 0, 0, 0, 0); // 왼쪽 버튼 누르고            
                mouse_event(LBUP, 0, 0, 0, 0); // 떼고

                Thread.Sleep(500);
            }

            if (prev == "a")
            {
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
                Auction_Thread = new Thread(new ThreadStart(ThreadAuction));
                Auction_Thread.SetApartmentState(ApartmentState.STA);
                Auction_Thread.Start();
                Auction_Thread_Flag = true;

                currTime = getCurrentTime();
                LogBox.Items.Add(currTime + " 소비/기타 시작");
                LogBox.SelectedIndex = LogBox.Items.Count - 1;
            }
            else
            {
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
                CashAuction_Thread = new Thread(new ThreadStart(ThreadCashAuction));
                CashAuction_Thread.SetApartmentState(ApartmentState.STA);
                CashAuction_Thread.Start();
                CashAuction_Thread_Flag = true;

                currTime = getCurrentTime();
                LogBox.Items.Add(currTime + " 장비/캐시 시작");
                LogBox.SelectedIndex = LogBox.Items.Count - 1;
            }

            currTime = getCurrentTime();
            LogBox.Items.Add(currTime + " 판매 등록 완료");
            LogBox.SelectedIndex = LogBox.Items.Count - 1;

            currTime = getCurrentTime();
            LogBox.Items.Add(currTime + " 구매 매크로 재시작");
            LogBox.SelectedIndex = LogBox.Items.Count - 1;

            Thread.Sleep(7200000);                  //1시간에 한번 판매 등록 수행

            Sell_Thread = new Thread(new ThreadStart(ThreadSell));
            Sell_Thread.SetApartmentState(ApartmentState.STA);
            Sell_Thread.Start();
        }
        
        public static String[] UseImageSearch(string imgPath)
        {
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

            if(Sell_Thread_Flag)
               Sell_Thread.Abort();
            
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

        public void datasave()
        {
            sellItemList.Clear();
            
            SYSTEMID = this.textBox1.Text;

            MessageBox.Show("아이디가 잘 저장되었습니다");

            sellItem nItem = new sellItem();
            bool type = false;
            if(checkBox1.Checked)
            {
                if (sobi1.Checked) type = true;
                else type = false;

                nItem.setItemInformation(int.Parse(pos1.Text), int.Parse(price1.Text), true, type);
                MessageBox.Show(nItem.getItemInformation());
                sellItemList.Add(nItem);
            }
            nItem = new sellItem();
            if (checkBox2.Checked)
            {
                if (sobi2.Checked) type = true;
                else type = false;

                nItem.setItemInformation(int.Parse(pos2.Text), int.Parse(price2.Text), true, type);
                MessageBox.Show(nItem.getItemInformation());
                sellItemList.Add(nItem);
            }

            MessageBox.Show("판매정보가 잘 저장되었습니다");

            string currTime = getCurrentTime();
            LogBox.Items.Add(currTime + " 정보 저장 완료");
            LogBox.SelectedIndex = LogBox.Items.Count - 1;
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
            Auction_Thread = new Thread(new ThreadStart(ThreadAuction));
            Auction_Thread.SetApartmentState(ApartmentState.STA);
            Auction_Thread.Start();
            Auction_Thread_Flag = true;

            if (sellItemList.Count > 0)
            {
                Sell_Thread = new Thread(new ThreadStart(ThreadSell));
                Sell_Thread.SetApartmentState(ApartmentState.STA);
                Sell_Thread.Start();
                Sell_Thread_Flag = true;
            }

            string currTime = getCurrentTime();
            LogBox.Items.Add(currTime + " 소비/기타 시작");
            LogBox.SelectedIndex = LogBox.Items.Count - 1;

            
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
            CashAuction_Thread = new Thread(new ThreadStart(ThreadCashAuction));
            CashAuction_Thread.SetApartmentState(ApartmentState.STA);
            CashAuction_Thread.Start();
            CashAuction_Thread_Flag = true;

            if(sellItemList.Count>0)
            {
                Sell_Thread = new Thread(new ThreadStart(ThreadSell));
                Sell_Thread.SetApartmentState(ApartmentState.STA);
                Sell_Thread.Start();
                Sell_Thread_Flag = true;
            }
            

            string currTime = getCurrentTime();
            LogBox.Items.Add(currTime + " 장비/캐시 시작");
            LogBox.SelectedIndex = LogBox.Items.Count - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Auction_Thread_Flag == true)
            {
                Auction_Thread.Abort();
                AutoClosingMessageBox.Show("Thread STOP!", "알림", 1200);
                string currTime = getCurrentTime();
                LogBox.Items.Add(currTime + " 매크로 중지");
                LogBox.SelectedIndex = LogBox.Items.Count - 1;
            }
            if (CashAuction_Thread_Flag == true)
            {
                CashAuction_Thread.Abort();
                AutoClosingMessageBox.Show("Thread STOP!", "알림", 1200);
                string currTime = getCurrentTime();
                LogBox.Items.Add(currTime + " 매크로 중지");
                LogBox.SelectedIndex = LogBox.Items.Count - 1;
            }
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

        private static string get_window_pixel(int x, int y)
        {   
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, x, y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            //MessageBox.Show(screenPixel.GetPixel(0, 0).ToString());
            return screenPixel.GetPixel(0,0).G.ToString();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            datasave();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show(get_window_pixel(431, 523).ToString());
            //MessageBox.Show(get_window_pixel(604, 162).ToString());
            //MessageBox.Show(get_window_pixel(284, 217).ToString());


            
            MessageBox.Show(Control.MousePosition.X.ToString() + " " + Control.MousePosition.Y.ToString());            
        }

    }    
    public class sellItem
    {
        public int position = 0;
        public int price = 0;
        public bool isChecked = false;
        public bool type = true;        // true : 소비, false : 기타

        public sellItem()
        {
            this.position = 0;
            this.price = 0;
            this.isChecked = false;
            this.type = true;
        }

        public void setItemInformation(int pos, int pri, bool chk, bool ty)
        {
            this.position = pos;
            this.price = pri;
            this.isChecked = chk;
            this.type = ty;
        }
        public string getItemInformation()
        {
            return this.position + "," + this.price + "," + this.isChecked + "," + this.type;
        }
    }
}
