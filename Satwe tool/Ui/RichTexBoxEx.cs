using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Satwe_tool
{
	public class RichTextBoxEx:RichTextBox
 	{
// 		[DllImport("user32.dll")]
//         public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        private RichTextBox otherRichTextBox;
        public RichTextBox OtherRichTextBox
        {
            get { return otherRichTextBox; }
            set { otherRichTextBox = value; }
        }
        public bool NeedSync = false;      

        public RichTextBoxEx()
        {
            this.VScroll += new EventHandler(RichTextBoxEx_VScroll);           
        }

        void RichTextBoxEx_VScroll(object sender, EventArgs e)
        {
            if (otherRichTextBox != null && NeedSync && this.Focused)
            {
                int charIndex = this.GetCharIndexFromPosition(this.Location);
                int firstLine = this.GetLineFromCharIndex(charIndex);
                int otherIndex=otherRichTextBox.GetFirstCharIndexFromLine(firstLine);
                if (otherIndex>=0)
                {
                    otherRichTextBox.Select(otherIndex, 0);
                    otherRichTextBox.ScrollToCaret();
                }                
            }
        }
//         public const int WM_HSCROLL = 276;
//         public const int WM_VSCROLL = 277;
//         public const int WM_SETCURSOR = 32;
//         public const int WM_MOUSEWHEEL = 522;
//         public const int WM_MOUSEMOVE = 512;
//         public const int WM_MOUSELEAVE = 675;
//         public const int WM_MOUSELAST = 521;
//         public const int WM_MOUSEHOVER = 673;
//         public const int WM_MOUSEFIRST = 512;
//         public const int WM_MOUSEACTIVATE = 33;

//         protected override void WndProc(ref Message m)
//         {
//             if ((otherRichTextBox != null&&NeedSync&&this.Focused) &&
//                 (m.Msg == WM_HSCROLL ||
//                 m.Msg == WM_VSCROLL ||
//                 m.Msg == WM_SETCURSOR ||
//                 m.Msg == WM_MOUSEWHEEL ||
//                 m.Msg == WM_MOUSEMOVE ||
//                 m.Msg == WM_MOUSELEAVE ||
//                 m.Msg == WM_MOUSELAST ||
//                 m.Msg == WM_MOUSEHOVER ||
//                 m.Msg == WM_MOUSEFIRST ||
//                 m.Msg == WM_MOUSEACTIVATE))
//             {
// 				if (otherRichTextBox!=null) {
// 					SendMessage(otherRichTextBox.Handle, m.Msg, m.WParam, m.LParam);
// 				}                
//             }
//             base.WndProc(ref m);
//         }
    }
}
