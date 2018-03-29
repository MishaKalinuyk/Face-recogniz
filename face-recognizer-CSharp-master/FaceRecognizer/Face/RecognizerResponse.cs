namespace FaceRecognizer.Face
{
    class RecognizerResponse
    {
        public string gender { get; set; }
        public int genderProb { get; set; }
        public int ageClass { get; set; }
        public string ageDiapason { get; set; }
        public int ageProb { get; set; }
    }
}
