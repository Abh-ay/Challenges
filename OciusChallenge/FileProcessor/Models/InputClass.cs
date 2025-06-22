namespace FileProcessor.Models
{
    public class InputClass
    {
        public string PickupPath { get; set; }
        public string DropPath { get; set; }

        private string _seperator = " ";

        // ' ' if user doesn't privide anything as seperator
        public string Separator 
        { get => _seperator; 
          set => _seperator = string.IsNullOrEmpty(value) ? _seperator :value;
        }

        private int _parallelCount = 1;

        // 1 if user puts value <=0
        public int ParallelCount 
        {
            get =>_parallelCount;
            set => _parallelCount = value > 0 ? value : 1;
        } 

        public int BatchSize { get; set; }
        public int BatchCount { get; set; }

    }
}
