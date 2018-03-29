using System;
using System.Collections.Generic;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Dnn;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace FaceRecognizer.Face
{
    class Recognizer
    {

        private static Importer importerGender = Importer.CreateCaffeImporter(Application.StartupPath + @"\Face\Data\deploy_gender.prototxt",
                   Application.StartupPath + @"\Face\Data\gender_net.caffemodel");

        private static Net netGender = new Net();

        private static bool isNetGenderPopulated = false;

        private static Importer importerAge = Importer.CreateCaffeImporter(Application.StartupPath + @"\Face\Data\deploy_age.prototxt",
                   Application.StartupPath + @"\Face\Data\age_net.caffemodel");

        private static Net netAge = new Net();

        private static bool isNetAgePopulated = false;

        private static int[] GetGender(Image<Bgr, byte> face)
        {
            try
            {
                if (!isNetGenderPopulated)
                {
                    importerGender.PopulateNet(netGender);
                    isNetGenderPopulated = true;
                    importerGender = null;
                }

                Blob inputBlob = new Blob(face.Resize(227, 227, Emgu.CV.CvEnum.Inter.Cubic));

                lock (netGender)
                {
                    netGender.SetBlob(".data", inputBlob);

                    netGender.Forward();

                    Blob prob = netGender.GetBlob("prob");

                    int[] outData = new int[2];

                    GetMaxClass(prob, out outData[0], out outData[1]);

                    return outData;
                }
                
            }
            catch
            {
                return new int[0];
            }
        }

        private static int[] GetAge(Image<Bgr, byte> face)
        {
            try
            {
                if (!isNetAgePopulated)
                {
                    importerAge.PopulateNet(netAge);
                    isNetAgePopulated = true;
                    importerAge = null;
                }

                Blob inputBlob = new Blob(face.Resize(227, 227, Emgu.CV.CvEnum.Inter.Cubic));

                lock (netAge)
                {
                    netAge.SetBlob(".data", inputBlob);

                    netAge.Forward();

                    Blob prob = netAge.GetBlob("prob");

                    int[] outData = new int[2];

                    GetMaxClass(prob, out outData[0], out outData[1]);

                    inputBlob.Dispose();
                    prob.Dispose();

                    return outData;
                }

            }
            catch
            {
                return new int[0];
            }
        }

        public static FaceRosponse AnalyzePhoto(Image<Bgr, byte> image)
        {
            FaceRosponse outData = new FaceRosponse();

            string frontFaceFilePath = Application.StartupPath + @"\Face\Data\haarcascade_frontalface_default.xml";

            string sideFaceFilePath = Application.StartupPath + @"\Face\Data\lbpcascade_profileface.xml";

            List<Rectangle> faceRects = new List<Rectangle>();
            List<Rectangle> faceRectFront = new List<Rectangle>();
            List<Rectangle> faceRectSide = new List<Rectangle>();

            faceRectFront = DetectFace(image, frontFaceFilePath);

            faceRectSide = DetectFace(image, sideFaceFilePath);

            faceRects.AddRange(faceRectFront);
            faceRects.AddRange(faceRectSide);

            foreach (Rectangle faceRect in faceRects)
            {
                Image<Bgr, byte> _croppedImage = image.Copy();
                _croppedImage.ROI = new Rectangle(faceRect.X, faceRect.Y, faceRect.Width, faceRect.Height);


                RecognizerResponse faceResponce = new RecognizerResponse();


                int[] genderFaces = GetGender(_croppedImage);
                if (genderFaces.Length < 2)
                    continue;

                faceResponce.gender = genderFaces[0] == 0 ? "male" : "female";
                faceResponce.genderProb = genderFaces[1];

                int[] ageFaces = GetAge(_croppedImage);
                if (ageFaces.Length < 2)
                    continue;

                faceResponce.ageClass = ageFaces[0];
                faceResponce.ageProb = ageFaces[1];
                faceResponce.ageDiapason = GetAgeDiapasonByClass(faceResponce.ageClass);

                outData.personsData.Add(faceResponce);

                _croppedImage = null;
            }

            outData.personsFaces.AddRange(faceRects);

            if (faceRects.Count == 0)
                outData = null;


            GC.Collect();


            return outData;
        }

        public static FaceRosponse AnalyzePhotoAsync(Image<Bgr, byte> image)
        {
            string frontFaceFilePath = Application.StartupPath + @"\Face\Data\haarcascade_frontalface_default.xml";

            string sideFaceFilePath = Application.StartupPath + @"\Face\Data\lbpcascade_profileface.xml";

            List<Rectangle> faceRects = new List<Rectangle>();
            List<Rectangle> faceRectFront = new List<Rectangle>();
            List<Rectangle> faceRectSide = new List<Rectangle>();

            faceRectFront = DetectFace(image, frontFaceFilePath);

            faceRectSide = DetectFace(image, sideFaceFilePath);

            faceRects.AddRange(faceRectFront);
            faceRects.AddRange(faceRectSide);

            FaceRosponse outData = new FaceRosponse();

            List<Thread> tasks = new List<Thread>();

            CancellationTokenSource cancelSourse = new CancellationTokenSource();

            foreach (Rectangle faceRect in faceRects)
            {
                tasks.Add(new Thread(() =>
                {
                    Image<Bgr, byte> _croppedImage = image.Copy();
                    _croppedImage.ROI = new Rectangle(faceRect.X, faceRect.Y, faceRect.Width, faceRect.Height);

                    RecognizerResponse faceResponce = new RecognizerResponse();


                    int[] genderFaces = GetGender(_croppedImage);

                    if (genderFaces.Length < 2)
                        return;

                    faceResponce.gender = genderFaces[0] == 0 ? "male" : "female";
                    faceResponce.genderProb = genderFaces[1];

                    int[] ageFaces = GetAge(_croppedImage);

                    if (ageFaces.Length < 2)
                        return;

                    faceResponce.ageClass = ageFaces[0];
                    faceResponce.ageProb = ageFaces[1];
                    faceResponce.ageDiapason = GetAgeDiapasonByClass(faceResponce.ageClass);

                    lock (outData)
                    {
                        outData.personsData.Add(faceResponce);
                        outData.personsFaces.Add(faceRect);
                    }

                    _croppedImage = null;
                }));
            }

            for (int i = 0; i < tasks.Count; i++)
                tasks[i].Start();

            for (int i = 0; i < tasks.Count; i++)
                tasks[i].Join();

            if (faceRects.Count == 0)
                outData = null;

            GC.Collect();

            return outData;
        }

        public static string GetAgeDiapasonByClass(int classId)
        {
            switch (classId)
            {
                case 0:
                    return "0-4";
                case 1:
                    return "4-6";
                case 2:
                    return "6-13";
                case 3:
                    return "13-20";
                case 4:
                    return "20-32";
                case 5:
                    return "32-43";
                case 6:
                    return "43-53";
                case 7:
                    return "53+";
                default:
                    return null;
            }
        }

        private static List<Rectangle> DetectFace(Image<Bgr, byte> image, string faceFilePath)
        {
            try
            {
                List<Rectangle> detectedFaces = new List<Rectangle>();

                //Read the HaarCascade objects
                using (CascadeClassifier face = new CascadeClassifier(faceFilePath))
                {
                    using (Image<Gray, byte> Gray = image.Convert<Gray, byte>()) //Convert it to Bgrscale
                    {
                        //normalizes brightness and increases contrast of the image
                        Gray._EqualizeHist();

                        //Detect the faces  from the Bgr scale image and store the locations as rectangle
                        //The first dimensional is the channel
                        //The second dimension is the index of the rectangle in the specific channel
                        Rectangle[] facesDetected = face.DetectMultiScale(
                           Gray,
                           1.1,
                           9,
                           new Size(30, 30),
                           Size.Empty);

                        //_croppedImage.ROI = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
                        detectedFaces.AddRange(facesDetected);
                    }
                }
                return detectedFaces;
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
                throw ex;
            }

        }

        private static bool isFileImage(string filePath)
        {
            string fileName = Path.GetFileName(filePath).ToLower();


            if (fileName.Contains(".jpg"))
                return true;
            else if (fileName.Contains(".bmp"))
                return true;
            else if (fileName.Contains(".pbm"))
                return true;
            else if (fileName.Contains(".pgm"))
                return true;
            else if (fileName.Contains(".ppm"))
                return true;
            else if (fileName.Contains(".sr"))
                return true;
            else if (fileName.Contains(".ras"))
                return true;
            else if (fileName.Contains(".jpeg"))
                return true;
            else if (fileName.Contains(".jpe"))
                return true;
            else if (fileName.Contains(".jp2"))
                return true;
            else if (fileName.Contains(".tiff"))
                return true;
            else if (fileName.Contains(".tif"))
                return true;
            else if (fileName.Contains(".png"))
                return true;

            return false;
        }

        private static void GetMaxClass(Blob probBlob, out int classId, out int maxval)
        {
            Mat probMat = probBlob.MatRef().Reshape(1, 1); //reshape the blob to 1x1000 matrix
            Point maxlock = new Point();

            maxval = 0;
            double minval = 0;
            Point minlock = new Point();

            double returnedMaxVal = 0;

            CvInvoke.MinMaxLoc(probMat, ref minval, ref returnedMaxVal, ref minlock, ref maxlock);

            maxval = (int)(returnedMaxVal * 100);

            classId = maxlock.X;
        }

    }
}
