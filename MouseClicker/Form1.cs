using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseForms
{
    public partial class Form1 : Form
    {
        Timer timerClicker;
        long count = 0;
        bool isCount = false;
        globalKeyboardHook gkh = new globalKeyboardHook();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isCount)
            {                
                int milliSeconds = Convert.ToInt32(upDownSec.Text);
                int clicks = Convert.ToInt32(upDownClicks.Text);
                int interval = milliSeconds / clicks;
                if (interval >= 1)
                {
                    upDownClicks.Enabled = false;
                    upDownSec.Enabled = false;
                    timerClicker = new Timer();
                    timerClicker.Interval = interval;
                    timerClicker.Tick += new EventHandler(timerClicker_Tick);
                    timerClicker.Start();
                    isCount = true;
                }
                else
                {
                    MessageBox.Show("Кол-во миллисекунд должно быть больше кол-ва кликов");
                }
            }
            else
            {
                upDownClicks.Enabled = true;
                upDownSec.Enabled = true;
                timerClicker.Stop();
                button1.Text = "Start!";
                count = 0;
                isCount = false;
            }
        }

        void timerClicker_Tick(object sender, EventArgs e)
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            DoMouseClick(X, Y);
            button1.Text = $"Всего: {++count}";
        }

        public void DoMouseClick(uint X, uint Y)
        {
            mouse_event((uint)(MouseEventFlags.LEFTDOWN | MouseEventFlags.LEFTUP), X, Y, 0, UIntPtr.Zero);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

        [Flags]
        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gkh.HookedKeys.Add(Keys.Pause);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
        }

        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Pause)
            {
                button1.PerformClick();
            }
        }
    }
}
