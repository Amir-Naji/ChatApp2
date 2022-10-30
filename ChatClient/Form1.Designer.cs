using System.Net.Sockets;
using System.Text;

namespace ChatClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.txtSendMessage = new System.Windows.Forms.TextBox();
            this.txtAllMessages = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(77, 64);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSendMessage.TabIndex = 0;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // txtSendMessage
            // 
            this.txtSendMessage.AcceptsReturn = true;
            this.txtSendMessage.Location = new System.Drawing.Point(77, 35);
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.Size = new System.Drawing.Size(238, 23);
            this.txtSendMessage.TabIndex = 1;
            this.txtSendMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSendMessage_KeyDown);
            // 
            // txtAllMessages
            // 
            this.txtAllMessages.Location = new System.Drawing.Point(77, 106);
            this.txtAllMessages.Multiline = true;
            this.txtAllMessages.Name = "txtAllMessages";
            this.txtAllMessages.Size = new System.Drawing.Size(364, 194);
            this.txtAllMessages.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtAllMessages);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.btnSendMessage);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnSendMessage;
        private TextBox txtSendMessage;
        private TextBox txtAllMessages;
    }
}