using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kame.Desktop.Entity
{
    public class Goku
    {
		private System.Media.SoundPlayer player = null;
        private System.Windows.Forms.PictureBox controleBase;
        List<Bitmap> frames = new List<Bitmap>();
        Bitmap frameKamehamehaMeio, frameKamehamehaFim;
        private Timer timer;
        private int indice = 0;
        private int indice2 = 0;
        private int[] animacao = { 0, 1, 0, 1, 0, 1, 2, 3, 4, 5, 6, 5, 4, 5, 6, 5, 4, 
                                   7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 9 };

		private string Mp3FilePath
		{
			get
			{
				string mp3FilePath = AppDomain.CurrentDomain.BaseDirectory;
				if (!mp3FilePath.EndsWith("\\"))
				{
					mp3FilePath += "\\";
				}
				mp3FilePath += "dbz.mp3";
				return mp3FilePath;
			}
		}
        public Goku(System.Windows.Forms.PictureBox controle)
        {
            Bitmap frame = null;
            Rectangle areaFrame;
            Graphics g = null;
            Bitmap gokuBase = Properties.Resources.goku;
            gokuBase.MakeTransparent(Color.FromArgb(255, 0, 255));
            
            //Frame 0
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(0, 0, 53, 93);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 1
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(52, 1, 53, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 2
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(106, 1, 55, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 3
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(162, 1, 58, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 4
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(223, 1, 57, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 5
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(273, 1, 56, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 6
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(327, 1, 51, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 7
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(0, 445, 55, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 8
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(118, 445, 120, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            //Frame 9
            frame = new Bitmap(200, 200);
            g = Graphics.FromImage(frame);
            areaFrame = new Rectangle(118, 445, 120, 95);
            g.DrawImage(gokuBase, 0, 0, areaFrame, GraphicsUnit.Pixel);
            frames.Add(frame);

            areaFrame = new Rectangle(358, 449, 25, 95);
            frameKamehamehaMeio = new Bitmap(200, 200);
            g = Graphics.FromImage(frameKamehamehaMeio);
            g.DrawImage(gokuBase, 113, 3, areaFrame, GraphicsUnit.Pixel);
            //g.DrawImage(gokuBase, 113, 3, areaFrame, GraphicsUnit.Pixel);
            //g.DrawImage(gokuBase, 128, 3, areaFrame, GraphicsUnit.Pixel);
            //g.DrawImage(gokuBase, 143, 3, areaFrame, GraphicsUnit.Pixel);


            areaFrame = new Rectangle(390, 449, 70, 95);
            frameKamehamehaFim = new Bitmap(400, 200);
            g = Graphics.FromImage(frameKamehamehaFim);
            g.DrawImage(gokuBase, 138, 3, areaFrame, GraphicsUnit.Pixel);

            

            
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += new EventHandler(timer_Tick);
            controleBase = controle;
			controle.Visible = false;
            timer.Enabled = false;

			this.player = new System.Media.SoundPlayer(Properties.Resources.dbz);
			//player.SoundLocation = this.Mp3FilePath;
        }

		public void Start()
		{
			if (!this.timer.Enabled)
			{
				indice = 0;
				indice2 = 0;
				controleBase.Visible = true;
				timer.Enabled = true;

				player.Play();
			}
		}

        protected void timer_Tick(object sender, EventArgs e)
        {
            if (indice < animacao.Length && animacao[indice] <frames.Count) 
            {
                ExibirFrame(frames[animacao[indice]]);
            }

            indice++;
            if (indice >= animacao.Length)
            {
                indice = animacao.Length;
                if (indice2 < 18)
                {
                    Bitmap frame = new Bitmap(1000, 200);
                    Graphics g = Graphics.FromImage(frame);
                    Rectangle areaFrame = new Rectangle(0, 0, 120, 95);
                    g.DrawImage(frames[animacao[indice-1]], 0, 0, areaFrame, GraphicsUnit.Pixel);

                    areaFrame = new Rectangle(120 , 3, 15, 95);
                    for (int i = 0; i < indice2*6; i++) 
                    {
                        g.DrawImage(frameKamehamehaMeio, 113 + (15 * i), 3, areaFrame, GraphicsUnit.Pixel);
                    }
                    areaFrame = new Rectangle(120, 3, 70, 95);
                    g.DrawImage(frameKamehamehaFim, 90 + (90 * indice2), 3, areaFrame, GraphicsUnit.Pixel);

                    ExibirFrame(frame);
                    indice2++;
                }
                else
                {
                    controleBase.Visible = false;
                    timer.Enabled = false;

					if (File.Exists(this.Mp3FilePath))
					{
						File.Delete(this.Mp3FilePath);
					}
                }
                
            }
        }

        public void ExibirFrame(Bitmap frame)
        {
            controleBase.Image = (Image)frame;
        }
    }
}
