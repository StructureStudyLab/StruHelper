﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Satwe_tool
{
    public partial class ThanksForm : Form
    {
        public ThanksForm()
        {
            InitializeComponent();
            this.richTextBox1.Text = Properties.Resources.ThanksInfo;
        }
    }
}
