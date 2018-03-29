using System.Collections.Generic;
using System.Drawing;

namespace FaceRecognizer.Face
{
    class FaceRosponse
    {
        public List<RecognizerResponse> personsData { get; set; }
        public List<Rectangle> personsFaces { get; set; }

        public FaceRosponse()
        {
            personsData = new List<RecognizerResponse>();
            personsFaces = new List<Rectangle>();
        }
    }
}
