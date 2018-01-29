using System;
using System.Collections.Generic;
using System.Text;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// վ����ʾ���δ�
    /// </summary>
    public class TrainWnd
    {
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public class CarInfomation
        {
            /// <summary>
            /// ����
            /// </summary>
            public short dir = 0;
            /// <summary>
            /// ����
            /// </summary>
            public short cnt = 0;
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public List<CarInfomation> infomations = new List<CarInfomation>();
        /// <summary>
        /// ��·
        /// </summary>
        public short lineID = 0;
        /// <summary>
        /// ��·��
        /// </summary>
        public string linename = "";
        /// <summary>
        /// ����
        /// </summary>
        public string trainnum = "";
        /// <summary>
        /// ��Ϣ
        /// </summary>
        public string infomation = "";
        /// <summary>
        /// ����
        /// </summary>
        public float weight;
        /// <summary>
        /// ����
        /// </summary>
        public float length;
        /// <summary>
        /// ����
        /// </summary>
        public short cnt;
        /// <summary>
        /// ����
        /// </summary>
        public string Count
        {
            get
            {
                if (cnt >= 255 || cnt < 0)
                    return "**";
                else
                    return cnt.ToString();
            }
        }
        /// <summary>
        /// ��������,0:δ֪;1:����;2:��ȼ
        /// </summary>
        public byte LocomType = 0;
        /// <summary>
        /// ����
        /// </summary>
        public byte flag = 0;
        /// <summary>
        /// ����/���
        /// </summary>
        public bool Set_Clear
        {
            get
            {
                return (flag & 1) != 0;
            }
            set
            {
                if (value)
                    flag |= 1;
                else
                    flag &= 0xfe;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public bool Lock
        {
            get
            {
                return (flag & 2) != 0;
            }
            set
            {
                if (value)
                    flag |= 2;
                else
                    flag &= 0xfd;
            }
        }
        /// <summary>
        /// ���г�����
        /// </summary>
        public bool EnableTrainDir
        {
            get
            {
                return (flag & 4) != 0;
            }
            set
            {
                if (value)
                    flag |= 4;
                else
                    flag &= 0xfb;
            }
        }
        /// <summary>
        /// �г�����
        /// </summary>
        public bool TrainDir
        {
            get
            {
                return (flag & 8) != 0;
            }
            set
            {
                if (value)
                    flag |= 8;
                else
                    flag &= 0xf7;
            }
        }
        /// <summary>
        /// ���л�
        /// </summary>
        /// <param name="wnds">���δ�</param>
        /// <returns>�ֽ���</returns>
        public static byte[] Serialize(TrainWnd[] wnds)
        {
            return Serialize(wnds, 0);
        }
        /// <summary>
        /// ���л�
        /// </summary>
        /// <param name="wnds">���δ�</param>
        /// <param name="color">��ɫ</param>
        /// <returns>�ֽ���</returns>
        public static byte[] Serialize(TrainWnd[] wnds, byte color)
        {
            ClSet cs = new ClSet();
            ClTable ct1 = new ClTable("Infomations");
            ct1.AddColumn("I_LINE_ID", typeof(short));
            ct1.AddColumn("C_TRA_NUM", typeof(string));
            ct1.AddColumn("F_WEGH", typeof(float));
            ct1.AddColumn("F_LEN", typeof(short));
            ct1.AddColumn("I_CAR_CNT", typeof(short));
            ct1.AddColumn("B_FLAG", typeof(byte));
            ct1.AddColumn("C_NOTE", typeof(string));
            ct1.AddColumn("B_LOCOM_TYPE", typeof(byte));
            ClTable ct2 = new ClTable("CarStream");
            ct2.AddColumn("I_LINE_ID", typeof(short));
            ct2.AddColumn("I_GRO_ID", typeof(short));
            ct2.AddColumn("I_CAR_CNT", typeof(short));
            foreach (TrainWnd w in wnds)
            {
                ClRow cr = ct1.NewRow();
                cr["I_LINE_ID"] = w.lineID;
                cr["F_WEGH"] = w.weight;
                cr["F_LEN"] = (short)(w.length * 10 + .5);
                cr["I_CAR_CNT"] = w.cnt;
                cr["C_TRA_NUM"] = w.trainnum;
                cr["B_FLAG"] = w.flag;
                cr["C_NOTE"] = w.infomation;
                cr["B_LOCOM_TYPE"] = w.LocomType;
                ct1.Rows.Add(cr);
                foreach (CarInfomation inf in w.infomations)
                {
                    if (inf.cnt > 0)
                    {
                        cr = ct2.NewRow();
                        cr["I_LINE_ID"] = w.lineID;
                        cr["I_GRO_ID"] = inf.dir;
                        cr["I_CAR_CNT"] = inf.cnt;
                        ct2.Rows.Add(cr);
                    }
                }
            }
            cs.Tables.Add(ct1);
            cs.Tables.Add(ct2);
            if (color != 0)
            {
                ct2 = new ClTable("SYSCOLOR");
                ct2.AddColumn("COLOR", typeof(byte));
                ClRow _cr = ct2.NewRow();
                _cr[0] = color;
                ct2.Rows.Add(_cr);
                cs.Tables.Add(ct2);
            }
            byte[] b = cs.Serialize(2);
            b[0] = 0x33;
            return b;
        }
        /// <summary>
        /// ���л������ڣ�
        /// </summary>
        /// <param name="wnds">���δ�</param>
        /// <returns></returns>
        public static byte[] SerializeFromStruct(TrainWnd[] wnds)
        {
            int n;
            n = 4;
            foreach (TrainWnd w in wnds)
            {
                n += 8;
                n += System.Text.Encoding.Default.GetByteCount(w.trainnum) + 1;
                n++;
                int m = w.infomations.Count;
                if (m > 255)
                    m = 255;
                n += 3 * m;
            }
            short a;
            byte[] b = new byte[n];
            b[0] = 0x33;
            a = (short)wnds.Length;
            BitConverter.GetBytes(a).CopyTo(b, 2);
            n = 4;
            foreach (TrainWnd w in wnds)
            {
                a = w.lineID;
                BitConverter.GetBytes(a).CopyTo(b, n);
                n += 2;
                b[n] = w.flag;
                n++;
                if (w.cnt >= 0 && w.cnt <= 255)
                    b[n] = (byte)w.cnt;
                else
                    b[n] = 0xff;
                n++;
                a = (short)(w.weight + .5);
                BitConverter.GetBytes(a).CopyTo(b, n);
                n += 2;
                a = (short)(w.length * 10 + .5);
                BitConverter.GetBytes(a).CopyTo(b, n);
                n += 2;
                Tools.String2Byte(w.trainnum, b, ref n);
                a = (short)w.infomations.Count;
                if (a < 0 || a > 255)
                    a = 255;
                b[n] = (byte)a;
                n++;
                for (int i = 0; i < a; i++)
                {
                    BitConverter.GetBytes(w.infomations[i].dir).CopyTo(b, n);
                    n += 2;
                    if (w.infomations[i].cnt >= 0 && w.infomations[i].cnt <= 255)
                        b[n] = (byte)w.infomations[i].cnt;
                    else
                        b[n] = 0xff;
                    n++;
                }
            }
            return b;
        }
        /// <summary>
        /// �����л�
        /// </summary>
        /// <param name="b">�ֽ���</param>
        /// <returns>���δ�</returns>
        public static TrainWnd[] Deserialize(byte[] b)
        {
            byte color;
            return Deserialize(b, out color);
        }
        /// <summary>
        /// �����л�
        /// </summary>
        /// <param name="b">�ֽ���</param>
        /// <param name="color">������ɫ</param>
        /// <returns>���δ�</returns>
        public static TrainWnd[] Deserialize(byte[] b, out byte color)
        {
            color = 0;
            if (b == null || b.Length < 4 || b[0] != 0x33)
                return new TrainWnd[0];
            if (ClSet.isClSet(b, 2))
                return DeserializeFromClSet(b, out color);
            else
                return DeserializeFromStruct(b);
        }
        static TrainWnd[] DeserializeFromClSet(byte[] b, out byte color)
        {
            color = 0;
            ClSet cs = new ClSet(b, 2);
            ClTable ct1 = cs.Tables["Infomations"], ct2 = cs.Tables["CarStream"];
            if (ct1 == null || ct2 == null)
                return new TrainWnd[0];
            TrainWnd[] wnds = new TrainWnd[ct1.Rows.Count];
            ClRow[] crs;
            int col_carcnt = ct1.GetColumnIndex("I_CAR_CNT");
            int col_flag = ct1.GetColumnIndex("B_FLAG");
            int col_len = ct1.GetColumnIndex("F_LEN");
            int col_wegh = ct1.GetColumnIndex("F_WEGH");
            int col_tranum = ct1.GetColumnIndex("C_TRA_NUM");
            int col_line = ct1.GetColumnIndex("I_LINE_ID");
            int col_note = ct1.GetColumnIndex("C_NOTE");
            int col_locomtype = ct1.GetColumnIndex("B_LOCOM_TYPE");
            int col_grocnt = ct2.GetColumnIndex("I_CAR_CNT");
            int col_groid = ct2.GetColumnIndex("I_GRO_ID");
            for (int i = 0; i < wnds.Length; i++)
            {
                wnds[i] = new TrainWnd();
                wnds[i].cnt = Convert.ToInt16(ct1.Rows[i][col_carcnt]);
                wnds[i].flag = Convert.ToByte(ct1.Rows[i][col_flag]);
                wnds[i].length = Convert.ToInt16(ct1.Rows[i][col_len]) / 10f;
                wnds[i].lineID = Convert.ToInt16(ct1.Rows[i][col_line]);
                wnds[i].weight = Convert.ToSingle(ct1.Rows[i][col_wegh]);
                wnds[i].trainnum = Convert.ToString(ct1.Rows[i][col_tranum]);
                wnds[i].infomation = ct1.Rows[i][col_note].ToString();
                if (col_locomtype >= 0)
                    wnds[i].LocomType = Convert.ToByte(ct1.Rows[i][col_locomtype]);
                crs = ct2["I_LINE_ID", "=", wnds[i].lineID];
                foreach (ClRow cr in crs)
                {
                    CarInfomation inf = new CarInfomation();
                    inf.cnt = Convert.ToInt16(cr[col_grocnt]);
                    inf.dir = Convert.ToInt16(cr[col_groid]);
                    wnds[i].infomations.Add(inf);
                }
            }
            try
            {
                ct1 = cs.Tables["SYSCOLOR"];
                if (ct1 != null && ct1.Rows.Count > 0)
                    color = Convert.ToByte(ct1.Rows[0][0]);
            }
            catch { }
            return wnds;
        }
        static TrainWnd[] DeserializeFromStruct(byte[] b)
        {
            int n = BitConverter.ToInt16(b, 2);
            TrainWnd[] wnds = new TrainWnd[n];
            int i;
            n = 4;
            for (i = 0; i < wnds.Length; i++)
            {
                wnds[i] = new TrainWnd();
                wnds[i].lineID = BitConverter.ToInt16(b, n);
                n += 2;
                wnds[i].flag = b[n];
                n++;
                wnds[i].cnt = b[n];
                n++;
                wnds[i].weight = BitConverter.ToInt16(b, n);
                n += 2;
                wnds[i].length = BitConverter.ToInt16(b, n) / 10f;
                n += 2;
                wnds[i].trainnum = Tools.Byte2String(b, ref n);
                int m = b[n];
                n++;
                for (int j = 0; j < m; j++)
                {
                    CarInfomation ci = new CarInfomation();
                    ci.dir = BitConverter.ToInt16(b, n);
                    n += 2;
                    ci.cnt = b[n];
                    n++;
                    wnds[i].infomations.Add(ci);
                }
            }
            return wnds;
        }
    }
}
