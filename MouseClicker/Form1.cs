using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace MouseClicker
{
    public partial class Form1 : Form
    {
        Timer timerClicker = null!;
        GlobalKeyboardHook gkh = new();
        
        private long _count;
        private bool _isCount;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_isCount)
            {                
                var milliSeconds = Convert.ToInt32(upDownSec.Text);
                var clicks = Convert.ToInt32(upDownClicks.Text);
                var interval = milliSeconds / clicks;
                if (interval >= 1)
                {
                    upDownClicks.Enabled = false;
                    upDownSec.Enabled = false;
                    timerClicker = new Timer();
                    timerClicker.Interval = interval;
                    timerClicker.Tick += timerClicker_Tick;
                    timerClicker.Start();
                    _isCount = true;
                }
                else
                {
                    MessageBox.Show("Кол-во миллисекунд должно быть больше кол-ва кликов");
                }

                button1.Text = "Всего: 0";
            }
            else
            {
                upDownClicks.Enabled = true;
                upDownSec.Enabled = true;
                timerClicker.Stop();
                button1.Text = "Start!";
                _count = 0;
                _isCount = false;
            }
        }

        // ReSharper disable once ArrangeTypeMemberModifiers
        void timerClicker_Tick(object sender, EventArgs e)
        {
            var x = (uint)Cursor.Position.X;
            var y = (uint)Cursor.Position.Y;
            DoMouseClick(x, y);
            button1.Text = $"Всего: {++_count}";
        }

        private static void DoMouseClick(uint X, uint Y)
        {
            mouse_event((uint)(MouseEventFlags.LEFTDOWN | MouseEventFlags.LEFTUP), X, Y, 0, UIntPtr.Zero);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

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
            gkh.KeyUp += gkh_KeyUp;
        }

        // ReSharper disable once ArrangeTypeMemberModifiers
        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Pause)
            {
                button1.PerformClick();
            }
        }
    }
}
