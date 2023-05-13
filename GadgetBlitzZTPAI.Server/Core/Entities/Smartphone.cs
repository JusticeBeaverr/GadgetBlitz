namespace GadgetBlitzZTPAI.Server.Core.Entities
{
    public class Smartphone: AggregateRoot
    {
        public Guid SmartphoneId { get; set; }       
        public string Name { get; set; }
        public string RAM { get; set; }
        public string Memory { get; set; }
        public string ScreenDiagonal { get; set; }
        public string Resolution { get; set; }
        public List<Camera> Cameras { get; set; }
        public int CamerasCount { get; set; }
        public List<Color> Colors { get; set; }
        public float AVGPrice { get; set; }
        public string PhotoUrl { get; set; }


        public Smartphone(string name, string ram, string memory, string screenDiagonal, string resolution, int camerasCount, float avgPrice, string photoUrl)
        {
            SmartphoneId = Guid.NewGuid();
            Name = name;
            RAM = ram;
            Memory = memory;
            ScreenDiagonal = screenDiagonal;
            Resolution = resolution;
            Cameras = new List<Camera>();
            CamerasCount = camerasCount;
            Colors = new List<Color>();
            AVGPrice = avgPrice;
            PhotoUrl = photoUrl;

        }

        public static Smartphone Create(string name, string ram, string memory, string screenDiagonal, string resolution, int camerasCount, float avgPrice, string photoUrl)
        {
            var smartphone = new Smartphone(name, ram, memory, screenDiagonal, resolution, camerasCount, avgPrice, photoUrl);
            smartphone.IncrementVersion();
            smartphone.SetCreationDate();
            smartphone.SetModyficationDate();
            return smartphone;
        }

        public void AddCameras(List<Camera> cameras)
        {
            Cameras.AddRange(cameras);
        }
        public void AddColors(List<Color> colors)
        {
            Colors.AddRange(colors);
        }

        public  Smartphone Modify(string name, string ram, string memory, string screenDiagonal, string resolution, int camerasCount, float avgPrice, string photoUrl)
        {
            SmartphoneId = new Guid();
            Name = name;
            RAM = ram;
            Memory = memory;
            ScreenDiagonal = screenDiagonal;
            Resolution = resolution;          
            CamerasCount = camerasCount; 
            AVGPrice = avgPrice;
            PhotoUrl = photoUrl;
            IncrementVersion();
            SetModyficationDate();
            return this;
        }

        public void ReplaceCameras(List<Camera> cameras)
        {
            Cameras.Clear();
            Cameras.AddRange(cameras);
            SetModyficationDate();
        }

        public void ReplaceColors(List<Color> colors)
        {
            Colors.Clear();
            Colors.AddRange(colors);
            SetModyficationDate();
        }
    }
}
