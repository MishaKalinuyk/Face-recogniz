using Emgu.CV;
using Emgu.CV.Structure;
using FaceRecognizer.Face;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FaceRecognizer
{
    public partial class Form1 : Form
    {
        private Image<Bgr, byte> photo;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Analyze.Text = "Аналізую";
            Analyze.Enabled = false;
            LoadPhoto.Enabled = false;
            try
            {
                //перерисовуем при повторном запуске
                InfoLable.Text = "";
                
                long startTime = DateTime.Now.Ticks;

                FaceRosponse facesInfo;

                if (isAsync.Checked)
                {
                    facesInfo = Recognizer.AnalyzePhotoAsync(photo);
                }
                else
                {
                    facesInfo = Recognizer.AnalyzePhoto(photo);
                }

                long endTimeTime = DateTime.Now.Ticks;


                if (facesInfo == null)
                {
                    MessageBox.Show("Не вдалось ропізнати лиця");
                    return;
                }

                InfoLable.Text = string.Format("Час роботи алгоритму : {0}(ms)", (endTimeTime - startTime) / TimeSpan.TicksPerMillisecond);

                System.Drawing.Image imageToDraw = photo.ToBitmap();

                Graphics graphycs = Graphics.FromImage(imageToDraw);

                for (int i = 0; i < facesInfo.personsData.Count; i++)
                {
                    using (Pen pen = new Pen(Color.White))
                    {
                        Rectangle faceRect = facesInfo.personsFaces[i];
                        graphycs.DrawRectangle(pen, faceRect);

                        using (Font font = new Font("Arial", 10))
                        {
                            RecognizerResponse faceInfo = facesInfo.personsData[i];
                            string toPrint = string.Format("Стать : {0}-{1:0}%\nВік : ({2})-{3:0}%",
                                (faceInfo.gender == "male" ? "Чоловік" : "Жінка"),
                                faceInfo.genderProb,
                                faceInfo.ageDiapason,
                                faceInfo.ageProb
                                );

                            graphycs.FillRectangle(
                                new SolidBrush(Color.FromArgb(200, 255, 255, 255)),
                                new RectangleF( //этот ужас рисует фон для текста что б читать было удобно
                                new PointF(faceRect.X, faceRect.Y - 40), graphycs.MeasureString(toPrint, font)));


                            graphycs.DrawString(toPrint, font, Brushes.Red, new PointF(faceRect.X, faceRect.Y - 40));
                        }

                    }
                }


                ImageBox.Image = imageToDraw;

                graphycs.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Невідома помилка\n" + ex.Message);
            }

            Analyze.Text = "Аналізувати";
            Analyze.Enabled = true;
            LoadPhoto.Enabled = true;
        }

        private void Image_Click(object sender, EventArgs e)
        {
            ImageBox.Image = photo.ToBitmap();
            InfoLable.Text = "";
        }

        private void LoadPhoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg, *.bmp, *.jpeg, *.jpg) | *.jpg;*.bmp;*.jpeg;*.jpg";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    photo = new Image<Bgr, byte>(dialog.FileName);

                    if (photo.Height > 600 || photo.Width > 800) //размер меням если больше 800
                    {
                        photo = photo.Resize((1 / (photo.Height / 600f)), Emgu.CV.CvEnum.Inter.Cubic);
                    }
                    else if (photo.Height < 500 || photo.Width < 500)
                    {
                        photo = photo.Resize((1 / (photo.Height / 500f)), Emgu.CV.CvEnum.Inter.Cubic);
                    }


                    ImageBox.Image = photo.ToBitmap();
                    isAsync.Visible = true;
                    Analyze.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка під час завантаження фото\n" + ex.Message);
            }
            
        }
        
    }
}
