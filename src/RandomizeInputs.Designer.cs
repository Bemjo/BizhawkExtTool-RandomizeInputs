namespace BizhawkRandomizeInputs
{
    partial class BizhawkRandomizeInputs
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label_CoreName = new System.Windows.Forms.Label();
            this.button_Randomize = new System.Windows.Forms.Button();
            this.button_ResetControls = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_UniqueRandom = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl_ControllerButtonsLists = new System.Windows.Forms.TabControl();
            this.tabControl_RemappedLists = new System.Windows.Forms.TabControl();
            this.textBox_RandomizeHotkey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ResetHotkey = new System.Windows.Forms.TextBox();
            this.button_ChangeRandomizeHotkey = new System.Windows.Forms.Button();
            this.button_ChangeResetHotkey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loaded Core: ";
            // 
            // label_CoreName
            // 
            this.label_CoreName.AutoSize = true;
            this.label_CoreName.Location = new System.Drawing.Point(92, 9);
            this.label_CoreName.Name = "label_CoreName";
            this.label_CoreName.Size = new System.Drawing.Size(33, 13);
            this.label_CoreName.TabIndex = 1;
            this.label_CoreName.Text = "None";
            // 
            // button_Randomize
            // 
            this.button_Randomize.Enabled = false;
            this.button_Randomize.Location = new System.Drawing.Point(11, 25);
            this.button_Randomize.Name = "button_Randomize";
            this.button_Randomize.Size = new System.Drawing.Size(75, 23);
            this.button_Randomize.TabIndex = 2;
            this.button_Randomize.Text = "Randomize";
            this.button_Randomize.UseVisualStyleBackColor = true;
            this.button_Randomize.Click += new System.EventHandler(this.button_Randomize_Click);
            // 
            // button_ResetControls
            // 
            this.button_ResetControls.Enabled = false;
            this.button_ResetControls.Location = new System.Drawing.Point(92, 25);
            this.button_ResetControls.Name = "button_ResetControls";
            this.button_ResetControls.Size = new System.Drawing.Size(75, 23);
            this.button_ResetControls.TabIndex = 4;
            this.button_ResetControls.Text = "Reset";
            this.button_ResetControls.UseVisualStyleBackColor = true;
            this.button_ResetControls.Click += new System.EventHandler(this.button_ResetControls_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 534);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Randomize These Buttons";
            // 
            // checkBox_UniqueRandom
            // 
            this.checkBox_UniqueRandom.AutoSize = true;
            this.checkBox_UniqueRandom.Location = new System.Drawing.Point(12, 54);
            this.checkBox_UniqueRandom.Name = "checkBox_UniqueRandom";
            this.checkBox_UniqueRandom.Size = new System.Drawing.Size(103, 17);
            this.checkBox_UniqueRandom.TabIndex = 9;
            this.checkBox_UniqueRandom.Text = "Unique Random";
            this.toolTip1.SetToolTip(this.checkBox_UniqueRandom, "Ensures each button will be assigned a different button");
            this.checkBox_UniqueRandom.UseVisualStyleBackColor = true;
            this.checkBox_UniqueRandom.CheckedChanged += new System.EventHandler(this.checkBox_UniqueRandom_CheckedChanged);
            // 
            // tabControl_ControllerButtonsLists
            // 
            this.tabControl_ControllerButtonsLists.Location = new System.Drawing.Point(11, 125);
            this.tabControl_ControllerButtonsLists.Name = "tabControl_ControllerButtonsLists";
            this.tabControl_ControllerButtonsLists.SelectedIndex = 0;
            this.tabControl_ControllerButtonsLists.Size = new System.Drawing.Size(250, 397);
            this.tabControl_ControllerButtonsLists.TabIndex = 10;
            // 
            // tabControl_RemappedLists
            // 
            this.tabControl_RemappedLists.Location = new System.Drawing.Point(267, 125);
            this.tabControl_RemappedLists.Name = "tabControl_RemappedLists";
            this.tabControl_RemappedLists.SelectedIndex = 0;
            this.tabControl_RemappedLists.Size = new System.Drawing.Size(250, 397);
            this.tabControl_RemappedLists.TabIndex = 11;
            // 
            // textBox_RandomizeHotkey
            // 
            this.textBox_RandomizeHotkey.Location = new System.Drawing.Point(265, 44);
            this.textBox_RandomizeHotkey.MaxLength = 256;
            this.textBox_RandomizeHotkey.Name = "textBox_RandomizeHotkey";
            this.textBox_RandomizeHotkey.ReadOnly = true;
            this.textBox_RandomizeHotkey.Size = new System.Drawing.Size(252, 20);
            this.textBox_RandomizeHotkey.TabIndex = 12;
            this.textBox_RandomizeHotkey.WordWrap = false;
            this.textBox_RandomizeHotkey.TextChanged += new System.EventHandler(this.textBox_RandomizeHotkey_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Randomize Hotkey";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(264, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Reset Hotkey";
            // 
            // textBox_ResetHotkey
            // 
            this.textBox_ResetHotkey.Location = new System.Drawing.Point(265, 99);
            this.textBox_ResetHotkey.MaxLength = 256;
            this.textBox_ResetHotkey.Name = "textBox_ResetHotkey";
            this.textBox_ResetHotkey.ReadOnly = true;
            this.textBox_ResetHotkey.Size = new System.Drawing.Size(252, 20);
            this.textBox_ResetHotkey.TabIndex = 16;
            this.textBox_ResetHotkey.WordWrap = false;
            this.textBox_ResetHotkey.TextChanged += new System.EventHandler(this.textBox_ResetHotkey_TextChanged);
            // 
            // button_ChangeRandomizeHotkey
            // 
            this.button_ChangeRandomizeHotkey.Location = new System.Drawing.Point(367, 15);
            this.button_ChangeRandomizeHotkey.Name = "button_ChangeRandomizeHotkey";
            this.button_ChangeRandomizeHotkey.Size = new System.Drawing.Size(75, 23);
            this.button_ChangeRandomizeHotkey.TabIndex = 17;
            this.button_ChangeRandomizeHotkey.Text = "Change";
            this.button_ChangeRandomizeHotkey.UseVisualStyleBackColor = true;
            this.button_ChangeRandomizeHotkey.Click += new System.EventHandler(this.button_ChangeRandomizeHotkey_Click);
            // 
            // button_ChangeResetHotkey
            // 
            this.button_ChangeResetHotkey.Location = new System.Drawing.Point(367, 70);
            this.button_ChangeResetHotkey.Name = "button_ChangeResetHotkey";
            this.button_ChangeResetHotkey.Size = new System.Drawing.Size(75, 23);
            this.button_ChangeResetHotkey.TabIndex = 18;
            this.button_ChangeResetHotkey.Text = "Change";
            this.button_ChangeResetHotkey.UseVisualStyleBackColor = true;
            this.button_ChangeResetHotkey.Click += new System.EventHandler(this.button_ChangeResetHotkey_Click);
            // 
            // BizhawkRandomizeInputs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 534);
            this.Controls.Add(this.button_ChangeResetHotkey);
            this.Controls.Add(this.button_ChangeRandomizeHotkey);
            this.Controls.Add(this.textBox_ResetHotkey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_RandomizeHotkey);
            this.Controls.Add(this.tabControl_RemappedLists);
            this.Controls.Add(this.tabControl_ControllerButtonsLists);
            this.Controls.Add(this.checkBox_UniqueRandom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.button_ResetControls);
            this.Controls.Add(this.button_Randomize);
            this.Controls.Add(this.label_CoreName);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "BizhawkRandomizeInputs";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_CoreName;
        private System.Windows.Forms.Button button_Randomize;
        private System.Windows.Forms.Button button_ResetControls;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_UniqueRandom;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TabControl tabControl_ControllerButtonsLists;
        private System.Windows.Forms.TabControl tabControl_RemappedLists;
        private System.Windows.Forms.TextBox textBox_RandomizeHotkey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ResetHotkey;
        private System.Windows.Forms.Button button_ChangeRandomizeHotkey;
        private System.Windows.Forms.Button button_ChangeResetHotkey;
    }
}