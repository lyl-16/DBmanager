namespace _12306TrainSystemProject.Models
{
    public class TrainViewModel
    {
        public int TrainNum { get; set; }
        public string TrainType { get; set; }
        public int CargCnt { get; set; }
        public int VIPAmount { get; set; }
        public int EXAmount { get; set; }
        public int FirstAmount { get; set; }
        public int SecondAmount { get; set; }
        public int TrainState { get; set; }

        public CarriageViewModel VIPInfo { get; set; }
        public CarriageViewModel EXInfo { get; set; }
        public CarriageViewModel FirstInfo { get; set; }
        public CarriageViewModel SecondInfo { get; set; }
    }
}
