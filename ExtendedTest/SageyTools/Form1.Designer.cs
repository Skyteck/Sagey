namespace SageyTools
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tbItemName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbStacks = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nWeight = new System.Windows.Forms.NumericUpDown();
            this.nSaleValue = new System.Windows.Forms.NumericUpDown();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnNewItem = new System.Windows.Forms.ToolStripButton();
            this.BtnSave = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAddIngredient = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.numIngredientAmt = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.cbIngreident = new System.Windows.Forms.ComboBox();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numCraftTime = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numAmount = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.cbItems = new System.Windows.Forms.ComboBox();
            this.tbRecipeName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewRecipe = new System.Windows.Forms.ToolStripButton();
            this.btnSaveRecipes = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteIngredient = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSaleValue)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIngredientAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCraftTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 31);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 433);
            this.listBox1.TabIndex = 3;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tbItemName
            // 
            this.tbItemName.Location = new System.Drawing.Point(197, 37);
            this.tbItemName.Name = "tbItemName";
            this.tbItemName.Size = new System.Drawing.Size(100, 20);
            this.tbItemName.TabIndex = 4;
            this.tbItemName.TextChanged += new System.EventHandler(this.tbItemName_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Item Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Weight";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Sale Value";
            // 
            // cbStacks
            // 
            this.cbStacks.AutoSize = true;
            this.cbStacks.Location = new System.Drawing.Point(197, 127);
            this.cbStacks.Name = "cbStacks";
            this.cbStacks.Size = new System.Drawing.Size(15, 14);
            this.cbStacks.TabIndex = 10;
            this.cbStacks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbStacks.UseVisualStyleBackColor = true;
            this.cbStacks.CheckedChanged += new System.EventHandler(this.cbStacks_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(133, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Stacks?";
            // 
            // nWeight
            // 
            this.nWeight.Location = new System.Drawing.Point(197, 68);
            this.nWeight.Name = "nWeight";
            this.nWeight.Size = new System.Drawing.Size(120, 20);
            this.nWeight.TabIndex = 12;
            this.nWeight.ValueChanged += new System.EventHandler(this.nWeight_ValueChanged);
            // 
            // nSaleValue
            // 
            this.nSaleValue.Location = new System.Drawing.Point(197, 98);
            this.nSaleValue.Name = "nSaleValue";
            this.nSaleValue.Size = new System.Drawing.Size(120, 20);
            this.nSaleValue.TabIndex = 13;
            this.nSaleValue.ValueChanged += new System.EventHandler(this.nSaleValue_ValueChanged);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(340, 31);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(120, 433);
            this.listBox2.TabIndex = 14;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(516, 499);
            this.tabControl1.TabIndex = 15;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.toolStrip2);
            this.tabPage1.Controls.Add(this.listBox2);
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.nSaleValue);
            this.tabPage1.Controls.Add(this.tbItemName);
            this.tabPage1.Controls.Add(this.nWeight);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cbStacks);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(508, 473);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewItem,
            this.BtnSave});
            this.toolStrip2.Location = new System.Drawing.Point(3, 3);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(502, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnNewItem
            // 
            this.btnNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewItem.Image = ((System.Drawing.Image)(resources.GetObject("btnNewItem.Image")));
            this.btnNewItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewItem.Name = "btnNewItem";
            this.btnNewItem.Size = new System.Drawing.Size(23, 22);
            this.btnNewItem.Text = "btnNewItem";
            this.btnNewItem.Click += new System.EventHandler(this.BtnNewItem_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnSave.Image = ((System.Drawing.Image)(resources.GetObject("BtnSave.Image")));
            this.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(23, 22);
            this.BtnSave.Text = "btnSaveItems";
            this.BtnSave.Click += new System.EventHandler(this.btnSaveItems_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteIngredient);
            this.tabPage2.Controls.Add(this.btnAddIngredient);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.numIngredientAmt);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.cbIngreident);
            this.tabPage2.Controls.Add(this.listBox4);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.numCraftTime);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.numAmount);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cbItems);
            this.tabPage2.Controls.Add(this.tbRecipeName);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.listBox3);
            this.tabPage2.Controls.Add(this.toolStrip1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(508, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnAddIngredient
            // 
            this.btnAddIngredient.Location = new System.Drawing.Point(147, 167);
            this.btnAddIngredient.Name = "btnAddIngredient";
            this.btnAddIngredient.Size = new System.Drawing.Size(90, 23);
            this.btnAddIngredient.TabIndex = 15;
            this.btnAddIngredient.Text = "Add Ingredient";
            this.btnAddIngredient.UseVisualStyleBackColor = true;
            this.btnAddIngredient.Click += new System.EventHandler(this.btnAddIngredient_Click_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(321, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Amount: ";
            // 
            // numIngredientAmt
            // 
            this.numIngredientAmt.Location = new System.Drawing.Point(376, 226);
            this.numIngredientAmt.Name = "numIngredientAmt";
            this.numIngredientAmt.Size = new System.Drawing.Size(120, 20);
            this.numIngredientAmt.TabIndex = 13;
            this.numIngredientAmt.ValueChanged += new System.EventHandler(this.numIngredientAmt_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(302, 198);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 12;
            this.label10.Text = "Output Item: ";
            // 
            // cbIngreident
            // 
            this.cbIngreident.FormattingEnabled = true;
            this.cbIngreident.Location = new System.Drawing.Point(376, 198);
            this.cbIngreident.Name = "cbIngreident";
            this.cbIngreident.Size = new System.Drawing.Size(121, 21);
            this.cbIngreident.TabIndex = 11;
            this.cbIngreident.SelectedIndexChanged += new System.EventHandler(this.cbIngreident_SelectedIndexChanged);
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.Location = new System.Drawing.Point(147, 196);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(120, 186);
            this.listBox4.TabIndex = 10;
            this.listBox4.SelectedIndexChanged += new System.EventHandler(this.listBox4_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(144, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Craft Time: ";
            // 
            // numCraftTime
            // 
            this.numCraftTime.DecimalPlaces = 1;
            this.numCraftTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numCraftTime.Location = new System.Drawing.Point(211, 116);
            this.numCraftTime.Name = "numCraftTime";
            this.numCraftTime.Size = new System.Drawing.Size(120, 20);
            this.numCraftTime.TabIndex = 8;
            this.numCraftTime.ValueChanged += new System.EventHandler(this.numCraftTime_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(156, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Amount: ";
            // 
            // numAmount
            // 
            this.numAmount.Location = new System.Drawing.Point(211, 90);
            this.numAmount.Name = "numAmount";
            this.numAmount.Size = new System.Drawing.Size(120, 20);
            this.numAmount.TabIndex = 6;
            this.numAmount.ValueChanged += new System.EventHandler(this.numAmount_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Output Item: ";
            // 
            // cbItems
            // 
            this.cbItems.FormattingEnabled = true;
            this.cbItems.Location = new System.Drawing.Point(211, 62);
            this.cbItems.Name = "cbItems";
            this.cbItems.Size = new System.Drawing.Size(121, 21);
            this.cbItems.TabIndex = 4;
            this.cbItems.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tbRecipeName
            // 
            this.tbRecipeName.Location = new System.Drawing.Point(211, 36);
            this.tbRecipeName.Name = "tbRecipeName";
            this.tbRecipeName.Size = new System.Drawing.Size(121, 20);
            this.tbRecipeName.TabIndex = 3;
            this.tbRecipeName.TextChanged += new System.EventHandler(this.tbRecipeName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(164, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Name: ";
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(7, 32);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(120, 420);
            this.listBox3.TabIndex = 1;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.listBox3_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewRecipe,
            this.btnSaveRecipes});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(502, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewRecipe
            // 
            this.btnNewRecipe.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewRecipe.Image = ((System.Drawing.Image)(resources.GetObject("btnNewRecipe.Image")));
            this.btnNewRecipe.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewRecipe.Name = "btnNewRecipe";
            this.btnNewRecipe.Size = new System.Drawing.Size(23, 22);
            this.btnNewRecipe.Text = "toolStripButton1";
            this.btnNewRecipe.Click += new System.EventHandler(this.btnNewRecipe_Click);
            // 
            // btnSaveRecipes
            // 
            this.btnSaveRecipes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveRecipes.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveRecipes.Image")));
            this.btnSaveRecipes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveRecipes.Name = "btnSaveRecipes";
            this.btnSaveRecipes.Size = new System.Drawing.Size(23, 22);
            this.btnSaveRecipes.Text = "toolStripButton2";
            this.btnSaveRecipes.Click += new System.EventHandler(this.btnSaveRecipes_Click);
            // 
            // btnDeleteIngredient
            // 
            this.btnDeleteIngredient.Location = new System.Drawing.Point(147, 389);
            this.btnDeleteIngredient.Name = "btnDeleteIngredient";
            this.btnDeleteIngredient.Size = new System.Drawing.Size(98, 23);
            this.btnDeleteIngredient.TabIndex = 16;
            this.btnDeleteIngredient.Text = "Delete Ingredient";
            this.btnDeleteIngredient.UseVisualStyleBackColor = true;
            this.btnDeleteIngredient.Click += new System.EventHandler(this.btnDeleteIngredient_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 497);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nSaleValue)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIngredientAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCraftTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAmount)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox tbItemName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbStacks;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nWeight;
        private System.Windows.Forms.NumericUpDown nSaleValue;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnNewItem;
        private System.Windows.Forms.ToolStripButton BtnSave;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNewRecipe;
        private System.Windows.Forms.ToolStripButton btnSaveRecipes;
        private System.Windows.Forms.ComboBox cbItems;
        private System.Windows.Forms.TextBox tbRecipeName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numCraftTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numIngredientAmt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbIngreident;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.Button btnAddIngredient;
        private System.Windows.Forms.Button btnDeleteIngredient;
    }
}

