using System;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// Command ��ժҪ˵����
    /// </summary>
    public class Command
    {
        /// <summary>
        /// ����
        /// </summary>
        public Command()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        /// <summary>
        /// ��������
        /// �ֳ�1
        /// </summary>
        public const byte DataType_Car1 = 1;
        /// <summary>
        /// ��������
        /// �ֳ�2
        /// </summary>
        public const byte DataType_Car2 = 2;
        /// <summary>
        /// ��������
        /// �ֳ�3
        /// </summary>
        public const byte DataType_Car3 = 3;

        /// <summary>
        /// ��������
        /// �ܱ�
        /// </summary>
        public const byte DataType_MainTable = 5;//���ܱ�
        /// <summary>
        /// ��������
        /// ��������
        /// </summary>
        public const byte DataType_Plan = 10;
        /// <summary>
        /// ��������
        /// ��������
        /// </summary>
        public const byte DataType_PlanIndex = 11;
        /// <summary>
        /// ��������
        /// ����ƻ�
        /// </summary>
        public const byte DataType_Ass = 15;//���ᳵ��
        /// <summary>
        /// ��������
        /// ����Ŀ¼
        /// </summary>
        public const byte DataType_ArrConsList = 20;
        /// <summary>
        /// ��������
        /// ����Ŀ¼
        /// </summary>
        public const byte DataType_DepConsList = 21;
        /// <summary>
        /// ��������
        /// ʵʱ�ֳ�
        /// </summary>
        public const byte DataType_RealCar = 30;
        /// <summary>
        /// ��������
        /// �ƻ��ֳ�
        /// </summary>
        public const byte DataType_PlanCar = 31;
        /// <summary>
        /// ��������
        /// �ƻ��ֳ����ƻ�
        /// </summary>
        public const byte DataType_PlanCarAndPlan = 32;
        /// <summary>
        /// ��������
        /// �Զ���ӡ
        /// </summary>
        public const byte DataType_AutoPrint = 1;
        /// <summary>
        /// ��������
        /// �����ӡ
        /// </summary>
        public const byte DataType_Ask4Print = 2;

        /// <summary>
        /// ��������
        /// �˻�5-1
        /// </summary>
        public const byte DataType_YH5_1 = 41;
        /// <summary>
        /// ��������
        /// �˻�5-2
        /// </summary>
        public const byte DataType_YH5_2 = 42;
        /// <summary>
        /// ��������
        /// ǿ�Ʊ���
        /// </summary>
        public const byte DataType_CipsForceSave = 50;
        /// <summary>
        /// ��������
        /// �޲��ƻ�
        /// </summary>
        public const byte DataType_CipsRepairPlan = 51;
        /// <summary>
        /// ��������
        /// ���ϻָ�
        /// </summary>
        public const byte DataType_CipsRestorePast = 52;
        /// <summary>
        /// ��������-��λ
        /// ����
        /// </summary>
        public const byte DataType_BoothCmd_GoodsIn = 1;
        /// <summary>
        /// ��������-��λ
        /// ����
        /// </summary>
        public const byte DataType_BoothCmd_GoodsOut = 2;
        /// <summary>
        /// ��������-��λ
        /// ���
        /// </summary>
        public const byte DataType_BoothCmd_Finish = 3;
        /// <summary>
        /// ��������-��λ
        /// ��ʼ����
        /// </summary>
        public const byte DataType_BoothCmd_GoodsInStart = 4;
        /// <summary>
        /// ��������-��λ
        /// ��ʼ����
        /// </summary>
        public const byte DataType_BoothCmd_GoodsOutStart = 5;
        /// <summary>
        /// ��������-��λ
        /// ��ʼװ��
        /// </summary>
        public const byte DataType_BoothCmd_LoadStart = 6;
        /// <summary>
        /// ��������-��λ
        /// ��ʼж��
        /// </summary>
        public const byte DataType_BoothCmd_UnloadStart = 7;
        /// <summary>
        /// ���·������
        /// </summary>
        public const byte DataType_TranslateCarGroup = 1;
        /// <summary>
        /// ���·�������
        /// </summary>
        public const byte DataType_TranslateCarChara = 2;


        /// <summary>
        /// ��������
        /// ��
        /// </summary>
        public const byte Cmd_Null = 0;
        /// <summary>
        /// ��������
        /// �����״̬
        /// </summary>
        public const byte Cmd_CmdResponse = 1;      //
        /// <summary>
        /// ��������
        /// �豸ͣ�ù���
        /// </summary>
        public const byte Cmd_DeviceMan = 5;        //

        /// <summary>
        /// ��������
        /// ��ϵͳ�㲥�ֳ�
        /// </summary>
        public const byte Cmd_SendCar = 50;         //
        /// <summary>
        /// ��������
        /// ��ϵͳ�㲥�ƻ�ͷ
        /// ����
        /// </summary>
        public const byte Cmd_SendPlanIndex = 51;   //
        /// <summary>
        /// ��������
        /// ��ϵͳ���ҳ�������
        /// �����ڣ�
        /// </summary>
        public const byte Cmd_FindCar = 52;         //
        /// <summary>
        /// ��������
        /// CIPSϵͳ�㲥ʵʱ�ֳ�
        /// ����
        /// </summary>
        public const byte Cmd_SendRealCar = 53;     //
        /// <summary>
        /// CIPSϵͳ�㲥�ƻ��ֳ�
        /// ����
        /// </summary>
        public const byte Cmd_SendPlanCar = 54;     //
        /// <summary>
        /// ��������
        /// ���ͼ���ĳ�����Ϣ
        /// </summary>
        public const byte Cmd_SendAss = 55;         //
        /// <summary>
        /// ��������
        /// ����/���ͺ�ϵͳ�ܱ�
        /// </summary>
        public const byte Cmd_MainInfo = 56;        //

        /// <summary>
        /// ��������
        /// ��ҵ���������-���Datatypeʹ��
        /// </summary>
        public const byte Cmd_CarsWork_Update = 62;        //
        /// <summary>
        /// ��������
        /// ȡ����ҵ���������
        /// </summary>
        public const byte Cmd_CancelCarsWork_Update = 63;  //

        /// <summary>
        /// ��������
        /// �����ֳ��������޸��ֳ���
        /// </summary>
        public const byte Cmd_AskCars4Load = 64;
        /// <summary>
        /// ��������
        /// �����ֳ��������޸��ֳ���
        /// </summary>
        public const byte Cmd_SendCars4Load = 65;
        /// <summary>
        /// ��������
        /// װ��
        /// </summary>
        public const byte Cmd_CarsLoaded = 66;      //
        /// <summary>
        /// ��������
        /// �ƶ�����
        /// </summary>
        public const byte Cmd_MoveCars = 67;        //
        /// <summary>
        /// ����һ�������ƻ���Ϊȡ�������ã�
        /// </summary>
        public const byte Cmd_Ask1Plan = 68;        //
        /// <summary>
        /// Ԥװ��
        /// </summary>
        public const byte Cmd_PreLoad = 69;
        /// <summary>
        /// ��������
        /// ������������
        /// </summary>
        public const byte Cmd_HoldupCars = 70;      //
        /// <summary>
        /// ��������
        /// ���һ����
        /// </summary>
        public const byte Cmd_AddCar = 71;          //
        /// <summary>
        /// ��������
        /// ɾ��һ����
        /// </summary>
        public const byte Cmd_DelCar = 72;          //
        /// <summary>
        /// ��������
        /// �����ֳ���ϸ
        /// </summary>
        public const byte Cmd_AskCarsDetail = 73;
        /// <summary>
        /// ��������
        /// ����װ���ֵ�����
        /// </summary>
        public const byte Cmd_ReloadDictionary = 74;
        /// <summary>
        /// ��������
        /// ȡʵʱ������ϸ
        /// ����
        /// </summary>
        public const byte Cmd_GetAnalyzeDetail = 80;//
        /// <summary>
        /// ��������
        /// �½��ƻ�����
        /// </summary>
        public const byte Cmd_NewPlan = 100;        //
        /// <summary>
        /// ��������
        /// ���ͼƻ�����
        /// </summary>
        public const byte Cmd_LoadPlan = 101;       //
        /// <summary>
        /// ��������
        /// ��ӡ�ƻ�
        /// </summary>
        public const byte Cmd_PrintPlan = 102;      //
        /// <summary>
        /// ��������
        /// ����ƻ�����
        /// </summary>
        public const byte Cmd_AskPlans = 103;       //
        /// <summary>
        /// ��������
        /// ����ƻ�����
        /// </summary>
        public const byte Cmd_SendPlans = 104;      //
        /// <summary>
        /// ��������
        /// ����ƻ�ִ��ǰ���ֳ�����
        /// </summary>
        public const byte Cmd_AskTracksBeforePlan = 105;    //
        /// <summary>
        /// ��������
        /// ��������ID
        /// </summary>
        public const byte Cmd_AskFlowID = 106;

        /// <summary>
        /// ��������
        /// ��ֹ��ƻ�
        /// </summary>
        public const byte Cmd_SplitPlan = 108;      //
        /// <summary>
        /// ��������
        /// ����༭�ƻ�
        /// </summary>
        public const byte Cmd_EditPlan = 109;       //
        /// <summary>
        /// ��������
        /// ����
        /// </summary>
        public const byte Cmd_PlanEnter = 110;      //
        /// <summary>
        /// ��������
        /// �ֲ�����
        /// </summary>
        public const byte Cmd_PlanPartEnter = 111;  //
        /// <summary>
        /// ��������
        /// ȡ������
        /// </summary>
        public const byte Cmd_PlanExit = 112;       //
        /// <summary>
        /// ��������
        /// �����ֳ�
        /// </summary>
        public const byte Cmd_PushPlan = 113;       //
        /// <summary>
        /// ��������
        /// ȡ������
        /// </summary>
        public const byte Cmd_PopPlan = 114;        //
        /// <summary>
        /// ��������
        /// ����ƻ�
        /// </summary>
        public const byte Cmd_AskPlan = 115;        //
        /// <summary>
        /// ��������
        /// �ƶ��ƻ�
        /// DataType==0:intdata[0]=�������̣�intdata[1]=�ο�����
        /// DataType==1:intdata=Ҫ�������µ�����˳��
        /// </summary>
        public const byte Cmd_MovePlan = 116;       //
        /// <summary>
        /// ��������
        /// ɾ���ƻ�
        /// </summary>
        public const byte Cmd_DeletePlan = 117;     //
        /// <summary>
        /// ��������
        /// �޸Ĺ��ƻ���ͷ
        /// time1=�ƻ���ʼ,time2=�ƻ�����,time3=ʵ�ʿ�ʼ,time4=ʵ�ʽ���;
        /// int1=FLOWID,int2=LOCOMID;string1=����,string2=������
        /// </summary>
        public const byte Cmd_ModifyFlowHead = 118; //
        /// <summary>
        /// ��������
        /// ���͹��ƻ����շ�
        /// </summary>
        public const byte Cmd_Send2Hump = 119;      //

        /// <summary>
        /// ��������
        /// �շ�����ȫ���ƻ�
        /// </summary>
        public const byte Cmd_AskAllPlan4Hump = 120;//
        /// <summary>
        /// ��������
        /// ��ſ�ʼ
        /// </summary>
        public const byte Cmd_SplitStart = 121;     //
        /// <summary>
        /// ��������
        /// ��Ž���
        /// </summary>
        public const byte Cmd_SplitOver = 122;      //
        /// <summary>
        /// ��������
        /// �շ����ӱ�IP
        /// </summary>
        public const byte Cmd_HumpLink = 123;       //
        /// <summary>
        /// ��������
        /// �ύ�ƻ��������ֳ�
        /// </summary>
        public const byte Cmd_SubmitPlan = 200;     //
        /// <summary>
        /// ��������
        /// ����ƻ��ݸ�
        /// </summary>
        public const byte Cmd_SavePlan = 201;       //
        /// <summary>
        /// ��������
        /// �޸ļƻ�
        /// </summary>
        public const byte Cmd_ModifyPlan = 202;     //
        /// <summary>
        /// ��������
        /// �ֳ�תȷ��
        /// </summary>
        public const byte Cmd_TrackToCons = 210;    //
        /// <summary>
        /// ��������
        /// ȷ��ת�ֳ�
        /// </summary>
        public const byte Cmd_ConsToTrack = 211;    //
        /// <summary>
        /// ��������
        /// ��������
        /// </summary>
        public const byte Cmd_TrainDepart = 212;    //
        /// <summary>
        /// ��������
        /// ���ﱨ��
        /// </summary>
        public const byte Cmd_TrainArrive = 213;    //
        /// <summary>
        /// ��������
        /// ����
        /// </summary>
        public const byte Cmd_SendCons = 214;       //
        /// <summary>
        /// ��������
        /// ȷ��ת�ֳ��ɹ���ִ
        /// </summary>
        public const byte Cmd_ConsToTrackSuccess = 215;//
        /// <summary>
        /// ��������
        /// ȷ����׼
        /// </summary>
        public const byte Cmd_ApproveCons = 216;    //
        /// <summary>
        /// ��������
        /// ɾ����
        /// </summary>
        public const byte Cmd_DelConsArr = 220;     //
        /// <summary>
        /// ��������
        /// ɾ����
        /// </summary>
        public const byte Cmd_DelConsDep = 221;     //
        /// <summary>
        /// ��������
        /// �½�����
        /// </summary>
        public const byte Cmd_NewConsArr = 222;     //
        /// <summary>
        /// ��������
        /// �½�����
        /// </summary>
        public const byte Cmd_NewConsDep = 223;     //
        /// <summary>
        /// ��������
        /// �޸��ֳ�
        /// </summary>
        public const byte Cmd_UpdateCars = 224;     //
        /// <summary>
        /// ��������
        /// �޸ĳ���
        /// </summary>
        public const byte Cmd_ModifyCarNum = 225;   //
        /// <summary>
        /// ��������
        /// ����/�����ֳ���ϸ��ë����
        /// </summary>
        public const byte Cmd_CarsDetail = 226;     //
        /// <summary>
        /// ��������
        /// ��λ����
        /// </summary>
        public const byte Cmd_BoothCommand = 227;
        /// <summary>
        /// ��������
        /// ���Ӱ�
        /// </summary>
        public const byte Cmd_HandOver = 230;       //
        /// <summary>
        /// ��������
        /// ת������
        /// </summary>
        public const byte Cmd_ToOnLine = 231;       //
        /// <summary>
        /// ��������
        /// ת��ֵ��
        /// </summary>
        public const byte Cmd_ToBackup = 232;       //
        /// <summary>
        /// ��������
        /// �ӹ�CIPS
        /// </summary>
        public const byte Cmd_ToOnLineFromCIPS = 233;//
        /// <summary>
        /// ��������
        /// ѡ���Ƿ�ʹ��TDCS����
        /// </summary>
        public const byte Cmd_TdcsMode = 234;       //
        /// <summary>
        /// ��������
        /// ��ԭ�����нӹ�
        /// </summary>
        public const byte Cmd_ToOnLineFromPast = 235;//
        /// <summary>
        /// ��������
        /// CIPSϵͳ�޸�
        /// </summary>
        public const byte Cmd_CipsRepair = 236;     //
        /// <summary>
        /// ��������
        /// ��ϵͳ���ݼ�����ҵͼ��дʵ
        /// </summary>
        public const byte Cmd_ToChart = 240;        //
        /// <summary>
        /// ��������
        /// �׶μƻ�����
        /// </summary>
        public const byte Cmd_PlanFromChart = 241;  //

      

        /// <summary>
        /// ��ϵͳ��������
        /// </summary>
        public class ExpressCommand
        {
            /// <summary>
            /// ��������
            /// </summary>
            public byte Cmd = Cmd_Null;
            /// <summary>
            /// ��������
            /// </summary>
            public byte Datatype = 0;
            /// <summary>
            /// ��������
            /// </summary>
            public short Workstation = 0;
            /// <summary>
            /// �ֽ�����
            /// </summary>
            public byte[] byteData = new byte[0];
            /// <summary>
            /// ��������
            /// </summary>
            public uint[] intData = new uint[0];
            /// <summary>
            /// �ַ�������
            /// </summary>
            public string[] strData = new string[0];
            /// <summary>
            /// ʱ������
            /// </summary>
            public DateTime[] timeData = new DateTime[0];
            /// <summary>
            /// ��λ
            /// </summary>
            public short postid;
            /// <summary>
            /// ����
            /// </summary>
            public string[] strs = { "", "", "", "", "" ,"", "",""};
            /// <summary>
            /// ������
            /// </summary>
            public string maker
            {
                get { return strs[0]; }
                set { strs[0] = value; }
            }
            /// <summary>
            /// IP
            /// </summary>
            public string ip
            {
                get { return strs[1]; }
                set { strs[1] = value; }
            }
            /// <summary>
            /// ��·
            /// </summary>
            public string trackname
            {
                get { return strs[2]; }
                set { strs[2] = value; }
            }
            /// <summary>
            /// ����
            /// </summary>
            public string carnum
            {
                get { return strs[3]; }
                set { strs[3] = value; }
            }
            /// <summary>
            /// ����
            /// </summary>
            public string trainnum
            {
                get { return strs[4]; }
                set { strs[4] = value; }
            }
            /// <summary>
            /// ��������
            /// </summary>
            public string flowtype
            {
                get { return strs[5]; }
                set { strs[5] = value; }
            }
            /// <summary>
            /// ��λ
            /// </summary>
            public string postname
            {
                get { return strs[6]; }
                set { strs[6] = value; }
            }
            /// <summary>
            /// վ��
            /// </summary>
            public string stationcode
            {
                get { return strs[7]; }
                set { strs[7] = value; }
            }
            /// <summary>
            /// ע��
            /// </summary>
            public string note = "";
            /// <summary>
            /// ���л�
            /// </summary>
            /// <returns></returns>
            public byte[] Serialize()
            {
                return Command.Serialize(this);
            }
        }
        /// <summary>
        /// ���л�
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Serialize(ExpressCommand data)
        {
            int n = 5 + 4 + data.byteData.Length + 4 + data.intData.Length * 4 + 4 + data.timeData.Length * 6 + 4;
            foreach (string s in data.strData)
                n += System.Text.Encoding.Default.GetByteCount(s) + 1;
            foreach (string s in data.strs)
                n += System.Text.Encoding.Default.GetByteCount(s) + 1;
            byte[] b = new byte[n];
            b[0] = data.Cmd;
            BitConverter.GetBytes(data.Workstation).CopyTo(b, 1);
           // b[1] = data.Workstation;
            b[3] = data.Datatype;
            BitConverter.GetBytes(data.postid).CopyTo(b, 4);
            n = 6;
            BitConverter.GetBytes((int)data.byteData.Length).CopyTo(b, n);
            n += 4;
            data.byteData.CopyTo(b, n);
            n += data.byteData.Length;
            BitConverter.GetBytes((int)data.intData.Length).CopyTo(b, n);
            n += 4;
            foreach (uint u in data.intData)
            {
                BitConverter.GetBytes(u).CopyTo(b, n);
                n += 4;
            }
            BitConverter.GetBytes((int)data.timeData.Length).CopyTo(b, n);
            n += 4;
            foreach (DateTime t in data.timeData)
            {
                Tools.Time2Byte(t).CopyTo(b, n);
                n += 6;
            }
            BitConverter.GetBytes((int)data.strData.Length).CopyTo(b, n);
            n += 4;
            foreach (string s in data.strData)
            {
                Tools.String2Byte(s, b, ref n);
            }
            foreach (string s in data.strs)
            {
                Tools.String2Byte(s, b, ref n);
            }
            return b;
        }
        /// <summary>
        /// �����л�
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static ExpressCommand Deserialize(byte[] b)
        {
            if (b.Length < 12)
                return null;
            int i, n;
            try
            {
                ExpressCommand cmd = new ExpressCommand();

                cmd.Cmd = b[0];
                cmd.Workstation = BitConverter.ToInt16(b, 1);
               // cmd.Workstation = b[1];
                cmd.Datatype = b[3];
                cmd.postid = BitConverter.ToInt16(b, 4);
                i = 6;
                n = BitConverter.ToInt32(b, i);
                i += 4;
                if (i + n + 3 > b.Length)
                    return null;
                cmd.byteData = new byte[n];
                Buffer.BlockCopy(b, i, cmd.byteData, 0, n);
                i += n;
                n = BitConverter.ToInt32(b, i);
                i += 4;
                if (i + n * 4 + 2 > b.Length)
                    return null;
                cmd.intData = new uint[n];
                int j;
                for (j = 0; j < n; j++)
                {
                    cmd.intData[j] = BitConverter.ToUInt32(b, i);
                    i += 4;
                }
                n = BitConverter.ToInt32(b, i);
                i += 4;
                cmd.timeData = new DateTime[n];
                if (i + n * 6 + 1 > b.Length)
                    return null;
                for (j = 0; j < n; j++)
                {
                    cmd.timeData[j] = Tools.Byte2Time(b, i);
                    i += 6;
                }
                n = BitConverter.ToInt32(b, i);
                i += 4;
                cmd.strData = new string[n];
                if (i + n > b.Length)
                    return null;
                try
                {
                    for (j = 0; j < n; j++)
                    {
                        cmd.strData[j] = Tools.Byte2String(b, ref i);
                    }
                }
                catch
                {
                    cmd = null;
                }
                try
                {
                    for (j = 0; j < cmd.strs.Length; j++)
                    {
                        try
                        {
                            cmd.strs[j] = Tools.Byte2String(b, ref i);
                        }
                        catch
                        {
                            cmd.strs[j] = "";
                        }
                    }
                }
                catch
                {
                    cmd = null;
                }

                return cmd;
            }
            catch
            {
                return null;
            }
        }
    }
}
