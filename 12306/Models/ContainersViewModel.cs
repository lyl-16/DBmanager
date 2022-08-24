namespace Containers
{
    public struct User
    {
        public string UserID    { get; set;}
        public string UserPWD   { get; set;}
        public string UserPhone { get; set;}
        public string UserEmail { get; set;}
        public string UserRName { get; set;}
        public int    UserType  { get; set;}
        public string UserGender{ get; set;}
        public string UserAddr  { get; set;}
        public string UserPID   { get; set;}
        
        public User(string ID, string PWD, string Phone, string Email, string RName, int Type, string Gender, string Addr, string PID)
        {
            this.UserID     = ID;
            this.UserPWD    = PWD;
            this.UserPhone  = Phone;
            this.UserEmail  = Email;
            this.UserRName  = RName;
            this.UserType   = Type;
            this.UserGender = Gender;
            this.UserAddr   = Addr;
            this.UserPID    = PID;
        }
    }


    public struct Station
    {
        public int StationNo{ get; set;}
        public string StationName{ get; set;}

        public Station(int No, string Name)
        {
            this.StationNo   = No;
            this.StationName = Name;
        }
    }

    public struct Train
    {
        public int    TrainNum    { get; set;}
        public string TrainType   { get; set;}
        public int    CargCnt     { get; set;}
        public int    VIPAmount   { get; set;}
        public int    EXAmount    { get; set;}
        public int    FirstAmount { get; set;}
        public int    SecondAmount{ get ;set;}
        public int    TrainState  { get; set;}

        public Carriage VIPInfo   { get; set;}
        public Carriage EXInfo    { get; set;}
        public Carriage FirstInfo { get; set;}
        public Carriage SecondInfo{ get; set;}

        public Train(int TrainNum, string TrainType, int CargCnt, int VIPAmount, int EXAmount, int FirsetAmount, int SecondAmount, int TrainState, Carriage VIPInfo, Carriage EXInfo, Carriage FirstInfo, Carriage SecondInfo)
        {
            this.TrainNum     = TrainNum;
            this.TrainType    = TrainType;
            this.CargCnt      = CargCnt;
            this.VIPAmount    = VIPAmount;
            this.EXAmount     = EXAmount;
            this.FirstAmount  = FirsetAmount;
            this.SecondAmount = SecondAmount;
            this.TrainState   = TrainState;
            this.VIPInfo      = VIPInfo;
            this.EXInfo       = EXInfo;
            this.FirstInfo    = FirstInfo;
            this.SecondInfo   = SecondInfo;
        }

    }

    public struct Carriage
    {
        public string TrainType  { get; set;}
        public int    SeatLevel  { get; set;}
        public int    SeatRowCnt { get; set;}
        public int    SeatColCnt { get; set;}

        public Carriage(string TrainType, int SeatLevel, int SeatRowCnt, int SeatColCnt)
        {
            this.TrainType   = TrainType;
            this.SeatLevel   = SeatLevel;
            this.SeatRowCnt  = SeatRowCnt;
            this.SeatColCnt  = SeatColCnt;
        }
    }
}