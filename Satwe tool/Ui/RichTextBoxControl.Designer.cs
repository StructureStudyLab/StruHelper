namespace Satwe_tool
{
	partial class RichTextBoxControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.richTextBoxEx1 = new Satwe_tool.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // richTextBoxEx1
            // 
            this.richTextBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEx1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxEx1.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.OtherRichTextBox = null;
            this.richTextBoxEx1.Size = new System.Drawing.Size(329, 421);
            this.richTextBoxEx1.TabIndex = 1;
            this.richTextBoxEx1.Text = "";
            this.richTextBoxEx1.WordWrap = false;
            this.richTextBoxEx1.VScroll += new System.EventHandler(this.richTextBoxEx1_VScroll);
            // 
            // RichTextBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBoxEx1);
            this.Name = "RichTextBoxControl";
            this.Size = new System.Drawing.Size(329, 421);
            this.ResumeLayout(false);

		}

		#endregion

		private RichTextBoxEx richTextBoxEx1;
	}
}
