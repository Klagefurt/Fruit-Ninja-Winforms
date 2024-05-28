namespace FruitNinjaWinFormsApp
{
    partial class GameForm
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
            ballsLabel = new Label();
            catchedBallsLabel = new Label();
            countdownLabel = new Label();
            SuspendLayout();
            // 
            // ballsLabel
            // 
            ballsLabel.AutoSize = true;
            ballsLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            ballsLabel.ForeColor = Color.Red;
            ballsLabel.Location = new Point(149, 22);
            ballsLabel.Name = "ballsLabel";
            ballsLabel.Size = new Size(24, 28);
            ballsLabel.TabIndex = 0;
            ballsLabel.Text = "0";
            ballsLabel.Visible = false;
            // 
            // catchedBallsLabel
            // 
            catchedBallsLabel.AutoSize = true;
            catchedBallsLabel.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            catchedBallsLabel.ForeColor = Color.Red;
            catchedBallsLabel.Location = new Point(31, 22);
            catchedBallsLabel.Name = "catchedBallsLabel";
            catchedBallsLabel.Size = new Size(28, 32);
            catchedBallsLabel.TabIndex = 1;
            catchedBallsLabel.Text = "0";
            // 
            // countdownLabel
            // 
            countdownLabel.AutoSize = true;
            countdownLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            countdownLabel.ForeColor = Color.Blue;
            countdownLabel.Location = new Point(268, 22);
            countdownLabel.Name = "countdownLabel";
            countdownLabel.Size = new Size(24, 28);
            countdownLabel.TabIndex = 2;
            countdownLabel.Text = "0";
            countdownLabel.Visible = false;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(countdownLabel);
            Controls.Add(catchedBallsLabel);
            Controls.Add(ballsLabel);
            Name = "GameForm";
            Text = "Fruit Ninja";
            Load += GameForm_Load;
            Paint += GameForm_Paint;
            MouseMove += GameForm_MouseMove;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ballsLabel;
        private Label catchedBallsLabel;
        private Label countdownLabel;
    }
}
