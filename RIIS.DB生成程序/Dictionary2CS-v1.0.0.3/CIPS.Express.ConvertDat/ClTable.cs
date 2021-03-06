using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CIPS
{

    /// <summary>
    /// CL行!
    /// </summary>
    public class ClRow
    {
        object[] values;
        /// <summary>
        /// 相关对象
        /// </summary>
        public object Tag = null;
        /// <summary>
        /// 构造ClRow!
        /// 无!
        /// DataRow dr = 行原形,table = 所属表
        /// </summary>
        public ClRow(DataRow dr, ClTable table)
        {
            if (table == null)
            {
                values = new object[0];
                return;
            }
            Table = table;
            values = new object[Table.Columns.Length];
            for (int i = 0; i < values.Length; i++)
            {
                try
                {
                    values[i] = dr[Table.Columns[i]];
                }
                catch
                {
                    values[i] = dr[Table.Columns[i]].ToString().Trim().ToUpper();
                }
            }
        }
        /// <summary>
        /// 构造ClRow!
        /// </summary>
        /// <param name="dr">行</param>
        /// <param name="table">表</param>
        public ClRow(DataRowView dr, ClTable table)
        {
            if (table == null)
            {
                values = new object[0];
                return;
            }
            Table = table;
            values = new object[Table.Columns.Length];
            for (int i = 0; i < values.Length - 1; i++)
            {
                try
                {
                    values[i] = dr[Table.Columns[i]];
                }
                catch
                {
                    values[i] = dr[Table.Columns[i]].ToString().Trim().ToUpper();
                }
            }
            DataRowState st = dr.Row.RowState;
            byte n;
            if (st == DataRowState.Added)
                n = 1;
            else if (st == DataRowState.Modified)
                n = 2;
            else if (st == DataRowState.Deleted)
                n = 3;
            else
                n = 0;
            values[values.Length - 1] = n;
        }

        /// <summary>
        /// 构造ClRow!
        /// 无!
        /// table = 所属表
        /// </summary>
        public ClRow(ClTable table)
        {
            if (table == null)
            {
                values = new object[0];
                return;
            }
            Table = table;
            values = new object[Table.Columns.Length];
        }
        /// <summary>
        /// 返回行中对应的字段!
        /// 无!
        /// i = 字段索引
        /// </summary>
        public object this[int i]
        {
            get
            {
                if (i >= 0 && i < values.Length)
                    return values[i];
                else
                    return null;
            }
            set
            {
                if (i >= 0 && i < values.Length)
                    values[i] = value;
            }
        }
        /// <summary>
        /// 返回行中对应的字段!
        /// 无!
        /// field = 字段名
        /// </summary>
        public object this[string field]
        {
            get
            {
                field = field.Trim().ToUpper();
                for (int i = 0; i < values.Length; i++)
                {
                    if (Table.Columns[i] == field)
                        return values[i];
                }
                return null;
            }
            set
            {
                field = field.Trim().ToUpper();
                for (int i = 0; i < values.Length; i++)
                {
                    if (Table.Columns[i] == field)
                    {
                        values[i] = value;
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 所属表
        /// </summary>
        public ClTable Table = null;
        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="index"></param>
        public void RemoveColumn(int index)
        {
            if (index < 0 || index >= values.Length)
                return;
            object[] vs = new object[values.Length - 1];
            int j = 0;
            for (int i = 0; i < vs.Length; i++)
            {
                if (index < i)
                    j = 0;
                else
                    j = 1;
                vs[i] = values[i + j];
            }
            values = vs;
        }
    }
    /// <summary>
    /// CL表!
    /// </summary>
    public class ClTable
    {
        const string RowStatusColumn = "CLTABLE_COL_ROWSTATUS";
        /// <summary>
        /// 表中行的集合!
        /// 无!
        /// 无
        /// </summary>
        public List<ClRow> Rows = new List<ClRow>();
        /// <summary>
        /// 相关对象
        /// </summary>
        public object Tag = null;

        /// <summary>
        /// 表名!
        /// 无!
        /// 无
        /// </summary>
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                if (value == null)
                    value = "";
                tableName = value.Trim().ToUpper();
            }
        }
        string tableName;
        /// <summary>
        /// 返回原形表
        /// 无!
        /// 无
        /// </summary>
        public DataTable GetDataTable()
        {
            int statuscol = -1;
            DataTable dt = new DataTable(tableName);
            for (int i = 0; i < columns.Length; i++)
            {
                DataColumn dc = new DataColumn(columns[i], Type.GetType(Datatypes[columnsdatatype[i]]));
                if (columns[i] != RowStatusColumn)
                    dt.Columns.Add(dc);
                else
                    statuscol = i;
            }
            foreach (ClRow cr in Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dt.Columns.Count; i++)
                    dr[i] = cr[i];
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            if (statuscol >= 0)
            {
                for (int i = 0; i < Rows.Count; i++)
                {
                    byte st = 0;
                    try
                    {
                        st = Convert.ToByte(Rows[i][statuscol]);
                    }
                    catch { }
                    if (st == 1)
                        dt.Rows[i].SetAdded();
                    else if (st == 2)
                        dt.Rows[i].SetModified();
                    else if (st == 3)
                        dt.Rows[i].Delete();
                }
            }
            return dt;
        }

        /// <summary>
        /// 表列集合
        /// 无!
        /// 无
        /// </summary>
        public string[] Columns
        {
            get
            {
                return columns;
            }
        }
        /// <summary>
        /// 字节流是否为ClTable
        /// </summary>
        /// <param name="buf">字节流</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static bool isClTable(byte[] buf, int offset)
        {
            return CIPS.Express.ConvertDat.Tools.Byte2String(buf, ref offset) == ClTableVersion;
        }
        string[] columns;
        byte[] columnsdatatype;
        short primaryKey = -1;
        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                if (primaryKey >= 0 && primaryKey < columns.Length)
                    return columns[primaryKey];
                else
                    return null;
            }
            set
            {
                value = value.Trim().ToUpper();
                for (int i = 0; i < columns.Length; i++)
                {
                    if (columns[i] == value)
                    {
                        primaryKey = (short)i;
                        return;
                    }
                }
                primaryKey = -1;
            }
        }
        /// <summary>
        /// CL表!
        /// drs = 行数组
        /// </summary>
        public ClTable(DataRow[]drs)
        {
            if (drs.Length == 0)
            {
                columns = new string[0];
                columnsdatatype = new byte[0];
            }
            else
            {
                DataTable dt = drs[0].Table;
                Init(dt, drs);
            }
        }
        /// <summary>
        /// CL表!
        /// dt = 原形表
        /// </summary>
        public ClTable(DataTable dt)
        {
            Init(dt, true, false);
        }
        /// <summary>
        /// CL表!
        /// dt = 原形表
        /// </summary>
        public ClTable(DataTable dt,bool fillrows)
        {
            Init(dt, fillrows, false);
        }
        /// <summary>
        /// 表
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="fillrows">是否添加记录</param>
        /// <param name="rowstatus">是否包括记录状态</param>
        public ClTable(DataTable dt, bool fillrows, bool rowstatus)
        {
            Init(dt, fillrows, rowstatus);
        }
        void InitTable(DataTable dt)
        {
            string tp;
            List<string> cname = new List<string>();
            List<byte> ctype = new List<byte>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                tp = dt.Columns[i].DataType.FullName;
                for (byte t = 0; t < Datatypes.Length; t++)
                {
                    if (tp == Datatypes[t])
                    {
                        cname.Add(dt.Columns[i].ColumnName.Trim().ToUpper());
                        ctype.Add(t);
                        break;
                    }
                }
            }
            columns = new string[cname.Count];
            columnsdatatype = new byte[columns.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = cname[i];
                columnsdatatype[i] = ctype[i];
            }
            TableName = dt.TableName;
            if (dt.PrimaryKey != null && dt.PrimaryKey.Length > 0)
                PrimaryKey = dt.PrimaryKey[0].ColumnName;
        }
        void Init(DataTable dt, DataRow[] drs)
        {
            InitTable(dt);
            if (drs != null)
            {
                foreach (DataRow dr in drs)
                    Rows.Add(new ClRow(dr, this));
            }
        }
        void Init(DataTable dt, bool fillrows, bool rowstatus)
        {
            if (!fillrows)
            {
                InitTable(dt);
                return;
            }
            if (!rowstatus)
            {
                InitTable(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState == DataRowState.Deleted)
                        continue;
                    Rows.Add(new ClRow(dr, this));
                }
                //DataRow[] drs = dt.Select("", "", DataViewRowState.CurrentRows);
                //Init(dt, drs);
            }
            else
            {
                InitTable(dt);
                AddColumn(RowStatusColumn, typeof(byte));
                DataView dv = new DataView(dt, "", "", DataViewRowState.CurrentRows | DataViewRowState.Deleted);
                foreach (DataRowView dr in dv)
                    Rows.Add(new ClRow(dr, this));
            }
        }
        /// <summary>
        /// CL表!
        /// buf = 字节流
        /// </summary>
        public ClTable(byte[] buf)
        {
            int n = 0;
            Deserialize(buf, ref n);
        }
        /// <summary>
        /// CL表!
        /// buf = 字节流, offset = 偏移值
        /// </summary>
        public ClTable(byte[] buf, ref int offset)
        {
            Deserialize(buf, ref offset);
        }
        /// <summary>
        /// CL表!
        /// 
        /// </summary>
        public ClTable(string tablename)
        {
            columns = new string[0];
            columnsdatatype = new byte[columns.Length];
            TableName = tablename;
        }
        /// <summary>
        /// Clone表结构
        /// </summary>
        /// <returns></returns>
        public ClTable Clone()
        {
            ClTable ct = new ClTable(TableName);
            ct.columns = new string[columns.Length];
            ct.columnsdatatype = new byte[columnsdatatype.Length];
            for (int i = 0; i < columns.Length; i++)
            {
                ct.columns[i] = columns[i];
                ct.columnsdatatype[i] = columnsdatatype[i];
            }
            return ct;
        }
        /// <summary>
        /// 获取列索引
        /// </summary>
        /// <param name="columnname">列名</param>
        /// <returns></returns>
        public int GetColumnIndex(string columnname)
        {
            string s = columnname.Trim().ToUpper();
            for (int i = 0; i < columns.Length; i++)
            {
                if (columns[i] == s)
                    return i;
            }
            return -1;
        }
        /// <summary>
        /// 添加列
        /// columnname:列名,type:数据类型
        /// 行数为0时才能添加列
        /// </summary>
        public bool AddColumn(string columnname, Type type)
        {
            if (Rows.Count > 0)
                return false;
            byte tp = 0xff;
            for (byte t = 0; t < Datatypes.Length; t++)
            {
                if (type.FullName == Datatypes[t])
                {
                    tp = t;
                    break;
                }
            }
            if (tp == 0xff)
                return false;
            string[] cns = new string[columns.Length + 1];
            byte[] tps = new byte[cns.Length];
            int i;
            for (i = 0; i < columns.Length; i++)
            {
                cns[i] = columns[i];
                tps[i] = columnsdatatype[i];
            }
            cns[i] = columnname.ToUpper().Trim();
            tps[i] = tp;
            columns = cns;
            columnsdatatype = tps;
            return true;
        }
        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="column">列名</param>
        /// <returns></returns>
        public bool RemoveColumn(string column)
        {
            for(int i=0;i<columns.Length;i++)
            {
                if (columns[i].ToUpper() == column)
                    return RemoveColumn(i);
            }
            return false;
        }
        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns></returns>
        public bool RemoveColumn(int index)
        {
            if (index < 0 || index >= columns.Length)
                return false;
            string[] name = new string[columns.Length - 1];
            byte[] tp = new byte[columnsdatatype.Length - 1];
            int j;
            for (int i = 0; i < name.Length; i++)
            {
                if (index < i)
                    j = 0;
                else
                    j = 1;
                name[i] = columns[i + j];
                tp[i] = columnsdatatype[i + j];
            }
            columnsdatatype = tp;
            columns = name;
            foreach (ClRow cr in Rows)
                cr.RemoveColumn(index);
            return true;
        }
        /// <summary>
        /// 返回属于该表的新行!
        /// </summary>
        public ClRow NewRow()
        {
            ClRow cr = new ClRow(this);
            //Type tp;
            //for (int i = 0; i < columnsdatatype.Length; i++)
            //{
            //    tp = Type.GetType(Datatypes[columnsdatatype[i]]);
            //    if (tp == typeof(string))
            //        cr[i] = "";
            //    else if (tp == typeof(DateTime))
            //        cr[i] = new DateTime(1999, 1, 1);
            //    else if (tp == typeof(byte[]))
            //        cr[i] = new byte[0];
            //    else
            //        cr[i] = 0;
            //}
            return cr;
        }
        /// <summary>
        /// 返回符合条件的行!
        /// 无!
        /// field = 字段名, filterExpression = ">,>= ... ",value = 值
        /// </summary>
        public ClRow[] this[string field, string filterExpression, object value]
        {
            get
            {
                return this.Select(field, filterExpression, value);
            }
        }
        /// <summary>
        /// 返回符合条件的行!
        /// 无!
        /// field = 字段名, filterExpression = ">,>= ... ",value = 值
        /// </summary>
        public ClRow[] Select(string field, string filterExpression, object value)
        {
            return this.Select(field, filterExpression, value, Orderby);
        }
        /// <summary>
        /// 返回符合条件的行!
        /// 无!
        /// field = 字段名, filterExpression = ">,>= ... ",value = 值, sort = 排序
        /// </summary>
        public ClRow[] Select(string field, string filterExpression, object value, string sort)
        {
            ClRow[] rows = new ClRow[Rows.Count];
            for (int i = 0; i < rows.Length; i++)
                rows[i] = Rows[i];
            return ClTable.Select(rows, field, filterExpression, value, sort);
        }
        /// <summary>
        /// 返回符合条件的行!
        /// 同 DataTable
        /// </summary>
        public ClRow[] Select(string filterExpression, string sort)
        {
            DataTable dt;
            dt = GetDataTable();
            DataRow[] drs = dt.Select(filterExpression, sort);
            ClRow[] crs = new ClRow[drs.Length];
            int j;
            for (int i = 0; i < drs.Length; i++)
            {
                for (j = 0; j < dt.Rows.Count; j++)
                {
                    if (drs[i] == dt.Rows[j])
                    {
                        if (j < Rows.Count)
                            crs[i] = Rows[j];
                        break;
                    }
                }
                if (crs[i] == null)
                    crs[i] = new ClRow(drs[i], this);
            }
            dt.Dispose();
            return crs;
        }
        /// <summary>
        /// 返回符合条件的行!
        /// 同 DataTable
        /// </summary>
        public ClRow[] Select(string filterExpression)
        {
            return Select(filterExpression, "");
        }

        /// <summary>
        /// 在行数组中返回符合条件的行!
        /// 无!
        /// field = 字段名, filterExpression = ">,>=...",value = 值
        /// </summary>
        public static ClRow[] Select(ClRow[] rows, string field, string filterExpression, object value)
        {
            return Select(rows, field, filterExpression, value, null);
        }
        /// <summary>
        /// 在行数组中返回符合条件的行!
        /// 无!
        /// field = 字段名, filterExpression = ">,>=,IN,...",value = 值, orderby = 按照此字段排序
        /// </summary>
        public static ClRow[] Select(ClRow[] rows, string field, string filterExpression, object value, string orderby)
        {
            field = field.Trim().ToUpper();
            if (rows == null || rows.Length == 0)
                return rows;
            int ifield = rows[0].Table.GetColumnIndex(field);
            string sym = filterExpression.Trim().ToUpper();
            ClRow[] ms;
            if (ifield < 0|| filterExpression == null || filterExpression == "")
                ms = rows;
            else
            {
                Type tp = rows[0][ifield].GetType();
                TypeCode tpcode = Type.GetTypeCode(tp);
                List<ClRow> l = new List<ClRow>();
                foreach (ClRow m in rows)
                {
                    bool sure;
                    if (sym == "INCLUDE")
                    {
                        sure = false;
                        try
                        {
                            if (tpcode == TypeCode.String)
                            {
                                sure = ((string)m[ifield]).ToUpper().Contains(((string)value).Trim().ToUpper());
                            }
                            else
                            {
                                if (tp.IsPrimitive)
                                {
                                    Int64 v1 = Convert.ToInt64(m[ifield]);
                                    Int64 v2 = Convert.ToInt64(value);
                                    sure = (v1 & v2) != 0;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                    else if (sym == "INCLUDEALL")
                    {
                        sure = false;
                        try
                        {
                            if (tpcode == TypeCode.String)
                            {
                                sure = ((string)m[ifield]).ToUpper().Contains(((string)value).Trim().ToUpper());
                            }
                            else
                            {
                                if (tp.IsPrimitive)
                                {
                                    Int64 v1 = Convert.ToInt64(m[ifield]);
                                    Int64 v2 = Convert.ToInt64(value);
                                    sure = (v1 & v2) == v2;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        int r = Compare(m[ifield], value);
                        sure = sym == "<" && r == 2;
                        if (!sure)
                            sure = sym == "<=" && (r == 2 || r == 0);
                        if (!sure)
                            sure = sym == "=" && r == 0;
                        if (!sure)
                            sure = sym == ">=" && (r == 1 || r == 0);
                        if (!sure)
                            sure = sym == ">" && r == 1;
                        if (!sure)
                            sure = sym == "<>" && r > 0;
                    }
                    if (sure)
                        l.Add(m);
                }
                ms = new ClRow[l.Count];
                for (int i = 0; i < ms.Length; i++)
                    ms[i] = l[i];
            }
            if (orderby != null)
                Sort(ms, orderby);
            return ms;
        }
        string[] Datatypes ={ typeof(Int16).FullName, typeof(Int32).FullName, typeof(Int64).FullName, typeof(SByte).FullName, 
            typeof(Byte).FullName, typeof(Char).FullName, typeof(UInt16).FullName, typeof(UInt32).FullName, typeof(UInt64).FullName,
            typeof(Decimal).FullName, typeof(Double).FullName, typeof(Single).FullName, typeof(Boolean).FullName, typeof(DateTime).FullName,
            typeof(String).FullName, typeof(byte[]).FullName };
        static int Compare(object v1, object v2)   //0:v1=v2, 1:v1>v2, 2:v1<v2, -1:err
        {
            try
            {
                TypeCode tc = Type.GetTypeCode(v1.GetType());
                switch (tc)
                {
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        {
                            Int64 vv1 = Convert.ToInt64(v1);
                            Int64 vv2 = Convert.ToInt64(v2);
                            if (vv1 == vv2)
                                return 0;
                            else if (vv1 > vv2)
                                return 1;
                            else
                                return 2;
                        }
                    case TypeCode.Byte:
                    case TypeCode.Char:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        {
                            UInt64 vv1 = Convert.ToUInt64(v1);
                            UInt64 vv2 = Convert.ToUInt64(v2);
                            if (vv1 == vv2)
                                return 0;
                            else if (vv1 > vv2)
                                return 1;
                            else
                                return 2;
                        }
                    case TypeCode.Double:
                    case TypeCode.Single:
                        {
                            Double vv1 = Convert.ToDouble(v1);
                            Double vv2 = Convert.ToDouble(v2);
                            if (vv1 == vv2)
                                return 0;
                            else if (vv1 > vv2)
                                return 1;
                            else
                                return 2;
                        }
                    case TypeCode.Decimal:
                        {
                            Decimal vv1 = Convert.ToDecimal(v1);
                            Decimal vv2 = Convert.ToDecimal(v2);
                            if (vv1 == vv2)
                                return 0;
                            else if (vv1 > vv2)
                                return 1;
                            else
                                return 2;
                        }
                    case TypeCode.DateTime:
                        {
                            DateTime vv1 = Convert.ToDateTime(v1);
                            DateTime vv2 = Convert.ToDateTime(v2);
                            if (vv1 == vv2)
                                return 0;
                            else if (vv1 > vv2)
                                return 1;
                            else
                                return 2;
                        }
                    case TypeCode.String:
                        {
                            string vv1 = Convert.ToString(v1).Trim().ToUpper();
                            string vv2 = Convert.ToString(v2).Trim().ToUpper();
                            int n = vv1.CompareTo(vv2);
                            if (n == 0)
                                return 0;
                            else if (n > 0)
                                return 1;
                            else
                                return 2;
                        }
                    case TypeCode.Boolean:
                        {
                            bool vv1 = Convert.ToBoolean(v1);
                            bool vv2 = Convert.ToBoolean(v2);
                            if (vv1 == vv2)
                                return 0;
                            else
                                return -1;
                        }
                    default:
                        return -1;
                }
            }
            catch
            {
                return -1;
            }
        }
        string orderBy = "";
        /// <summary>
        /// orderby = 按照此字段排序
        /// 无!
        /// orderby = 按照此字段排序
        /// </summary>
        public string Orderby
        {
            get
            {
                return orderBy;
            }
            set
            {
                if (value == null)
                    orderBy = "";
                else
                    orderBy = value.Trim().ToUpper();
            }
        }
        /// <summary>
        /// 排序
        /// 无!
        /// rows = 行数组, orderby = 按照此字段排序
        /// </summary>
        public static void Sort(ClRow[] rows, string orderby)
        {
            if (rows == null || rows.Length <= 1)
                return;
            if (rows[0][orderby] == null)
                return;
            bool mov = true;
            int i, sum;

            sum = rows.Length;
            ClRow c;

            while (mov)
            {
                mov = false;
                for (i = 1; i < sum; i++)
                {
                    if (Compare(rows[i][orderby], rows[i - 1][orderby]) == 2)
                    {
                        c = rows[i];
                        rows[i] = rows[i - 1];
                        rows[i - 1] = c;
                        mov = true;
                    }
                }
                sum--;
            }
        }
        /// <summary>
        /// 表重新排序
        /// 无!
        /// orderby = 按照此字段排序
        /// </summary>
        public void SortBy(string column)
        {
            ClRow[] crs = new ClRow[Rows.Count];
            for (int i = 0; i < crs.Length; i++)
                crs[i] = Rows[i];
            Sort(crs, column);
            Rows.Clear();
            foreach (ClRow cr in crs)
                Rows.Add(cr);
        }
        int GetTypeWidth(string tpname)
        {
            TypeCode tc = Type.GetTypeCode(Type.GetType(tpname));
            switch (tc)
            {
                case TypeCode.Boolean:
                case TypeCode.Byte:
                case TypeCode.SByte:
                    return 1;
                case TypeCode.Int16:
                case TypeCode.Char:
                case TypeCode.UInt16:
                    return 2;
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Single:
                    return 4;
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Double:
                    return 8;
                case TypeCode.DateTime:
                    return 6;
                case TypeCode.String:
                case TypeCode.Decimal:
                    return 0;
                default:
                    {
                        if (tpname == typeof(byte[]).FullName)
                            return 0;
                        else
                            return -1;
                    }
            }
        }
        const string ClTableVersion = "CT1.0";
        /// <summary>
        /// 序列化本表
        /// 无!
        /// 
        /// </summary>
        public byte[] Serialize()
        {
            byte[] buf;
            int n, m;
            int l = 20;
            l += CIPS.Express.ConvertDat.Tools.GetByteCountFromString(tableName);
            for (int i = 0; i < columns.Length; i++)
            {
                l++;
                l += CIPS.Express.ConvertDat.Tools.GetByteCountFromString(columns[i]);
                n = GetTypeWidth(Datatypes[columnsdatatype[i]]);
                if (n == 0)
                {
                    if (Datatypes[columnsdatatype[i]] == typeof(string).FullName)
                    {
                        foreach (ClRow r in Rows)
                            l += CIPS.Express.ConvertDat.Tools.GetByteCountFromString(Convert.ToString(r[columns[i]]).Trim());
                    }
                    else if (Datatypes[columnsdatatype[i]] == typeof(byte[]).FullName)
                    {
                        foreach (ClRow r in Rows)
                        {
                            byte[] _bs = r[columns[i]] as byte[];
                            l += 4;
                            if (_bs != null)
                                l += _bs.Length;
                        }
                    }
                    else if (Datatypes[columnsdatatype[i]] == typeof(Decimal).FullName)
                    {
                        foreach (ClRow r in Rows)
                        {
                            l+=CIPS.Express.ConvertDat.Tools.SerializeDecimal(r[columns[i]]).Length;
                        }
                    }
                }
                else if (n > 0)
                    l += n * Rows.Count;
            }
            buf = new byte[l];
            System.Text.Encoding.Default.GetBytes(ClTableVersion).CopyTo(buf, 0);
            BitConverter.GetBytes(primaryKey).CopyTo(buf, 12);//PrimaryKey
            BitConverter.GetBytes((short)columns.Length).CopyTo(buf, 14);
            BitConverter.GetBytes((int)Rows.Count).CopyTo(buf, 16);
            l = 20;
            CIPS.Express.ConvertDat.Tools.String2Byte(tableName, buf, ref l);
            for (int i = 0; i < columns.Length; i++)
            {
                buf[l] = columnsdatatype[i];
                l++;
                CIPS.Express.ConvertDat.Tools.String2Byte(columns[i], buf, ref l);
                n = GetTypeWidth(Datatypes[columnsdatatype[i]]);
                TypeCode tc = Type.GetTypeCode(Type.GetType(Datatypes[columnsdatatype[i]]));
                if (n < 0)
                    continue;
                else if (n == 0)
                {
                    if (Datatypes[columnsdatatype[i]] == typeof(string).FullName)
                    {
                        foreach (ClRow r in Rows)
                            CIPS.Express.ConvertDat.Tools.String2Byte(Convert.ToString(r[columns[i]]).Trim(), buf, ref l);
                    }
                    else if (Datatypes[columnsdatatype[i]] == typeof(byte[]).FullName)
                    {
                        foreach (ClRow r in Rows)
                        {
                            byte[] _bs = r[columns[i]] as byte[];
                            int nn = 0;
                            if (_bs != null)
                            {
                                nn = _bs.Length;
                                BitConverter.GetBytes(nn).CopyTo(buf, l);
                                _bs.CopyTo(buf, l + 4);
                            }
                            else
                                BitConverter.GetBytes((int)-1).CopyTo(buf, l);
                            l += nn + 4;
                        }
                    }
                    else if (Datatypes[columnsdatatype[i]] == typeof(Decimal).FullName)
                    {
                        foreach (ClRow r in Rows)
                        {
                            byte[] bd = CIPS.Express.ConvertDat.Tools.SerializeDecimal(r[columns[i]]);
                            bd.CopyTo(buf, l);
                            l += bd.Length;
                        }
                    }
                }
                else
                {
                    m = 0;
                    foreach (ClRow r in Rows)
                    {
                        try
                        {
                            if (n == 1)
                            {
                                if (tc == TypeCode.Byte)
                                    buf[l + m] = Convert.ToByte(r[columns[i]]);
                                else if (tc == TypeCode.Boolean)
                                    buf[l + m] = Convert.ToByte(r[columns[i]]);
                                else if (tc == TypeCode.SByte)
                                    buf[l + m] = (byte)Convert.ToSByte(r[columns[i]]);
                            }
                            else if (n == 2)
                            {
                                if (tc == TypeCode.Int16)
                                    BitConverter.GetBytes(Convert.ToInt16(r[columns[i]])).CopyTo(buf, l + m * n);
                                else if (tc == TypeCode.UInt16)
                                    BitConverter.GetBytes(Convert.ToUInt16(r[columns[i]])).CopyTo(buf, l + m * n);
                            }
                            else if (n == 4)
                            {
                                if (tc == TypeCode.Single)
                                    BitConverter.GetBytes(Convert.ToSingle(r[columns[i]])).CopyTo(buf, l + m * n);
                                else if (tc == TypeCode.Int32)
                                    BitConverter.GetBytes(Convert.ToInt32(r[columns[i]])).CopyTo(buf, l + m * n);
                                else if (tc == TypeCode.UInt32)
                                    BitConverter.GetBytes(Convert.ToUInt32(r[columns[i]])).CopyTo(buf, l + m * n);
                            }
                            else if (n == 8)
                            {
                               if (tc == TypeCode.Double)
                               {
                                   BitConverter.GetBytes(Convert.ToDouble(r[columns[i]])).CopyTo(buf, l + m * n);
                               }
                               else
                                   BitConverter.GetBytes(Convert.ToInt64(r[columns[i]])).CopyTo(buf, l + m * n);
                            }
                            else if (n == 6 && tc == TypeCode.DateTime)
                            {
                                CIPS.Express.ConvertDat.Tools.Time2Byte(Convert.ToDateTime(r[columns[i]])).CopyTo(buf, l + m * n);
                            }
                        }
                        catch { }
                        m++;
                    }
                    l += n * Rows.Count;
                }
            }
            return buf;
        }
        void Deserialize(byte[] buf, ref int off)
        {
            Rows.Clear();
            int l = off;
            if (buf == null || buf.Length - off < 20 ||
                CIPS.Express.ConvertDat.Tools.Byte2String(buf, ref l) != ClTableVersion)
            {
                columns = new string[0];
                columnsdatatype = new byte[0];
                return;
            }
            int n;
            primaryKey = BitConverter.ToInt16(buf, off + 12);
            n = BitConverter.ToInt16(buf, off + 14);
            columns = new string[n];
            columnsdatatype = new byte[n];
            n = BitConverter.ToInt32(buf, off + 16);
            int i, j;
            for (i = 0; i < n; i++)
                Rows.Add(new ClRow(this));
            List<ClRow> rows = Rows;
            l = off + 20;
            tableName = CIPS.Express.ConvertDat.Tools.Byte2String(buf, ref l);
            for (i = 0; i < columns.Length; i++)
            {
                columnsdatatype[i] = buf[l];
                l++;
                columns[i] = CIPS.Express.ConvertDat.Tools.Byte2String(buf, ref l);
                n = GetTypeWidth(Datatypes[columnsdatatype[i]]);
                TypeCode tc = Type.GetTypeCode(Type.GetType(Datatypes[columnsdatatype[i]]));
                for (j = 0; j < rows.Count; j++)
                {
                    try
                    {
                        if (tc == TypeCode.Boolean)
                            rows[j][columns[i]] = Convert.ToBoolean(buf[l + j]);
                        else if (tc == TypeCode.Byte)
                            rows[j][columns[i]] = buf[l + j];
                        else if (tc == TypeCode.SByte)
                            rows[j][columns[i]] = (sbyte)buf[l + j];
                        else if (tc == TypeCode.Int16)
                            rows[j][columns[i]] = BitConverter.ToInt16(buf, l + j * n);
                        else if (tc == TypeCode.UInt16)
                            rows[j][columns[i]] = BitConverter.ToUInt16(buf, l + j * n);
                        else if (tc == TypeCode.Char)
                            rows[j][columns[i]] = BitConverter.ToChar(buf, l + j * n);
                        else if (tc == TypeCode.Int32)
                            rows[j][columns[i]] = BitConverter.ToInt32(buf, l + j * n);
                        else if (tc == TypeCode.UInt32)
                            rows[j][columns[i]] = BitConverter.ToUInt32(buf, l + j * n);
                        else if (tc == TypeCode.Int64)
                            rows[j][columns[i]] = BitConverter.ToInt64(buf, l + j * n);
                        else if (tc == TypeCode.UInt64)
                            rows[j][columns[i]] = BitConverter.ToUInt64(buf, l + j * n);
                        else if (tc == TypeCode.Single)
                            rows[j][columns[i]] = BitConverter.ToSingle(buf, l + j * n);
                        else if (tc == TypeCode.Double)
                            rows[j][columns[i]] = BitConverter.ToDouble(buf, l + j * n);
                        else if ( tc == TypeCode.Decimal)
                        {
                            int b_length = Convert.ToInt32(buf[l])+1;
                            byte[] bb = new byte[b_length];
                            Buffer.BlockCopy(buf, l, bb, 0, b_length);
                            rows[j][columns[i]] = CIPS.Express.ConvertDat.Tools.DeSerializeDecimal(bb);
                            l += b_length;  
                        }
                        else if (tc == TypeCode.DateTime)
                            rows[j][columns[i]] = CIPS.Express.ConvertDat.Tools.Byte2Time(buf, l + j * n);
                        else if (tc == TypeCode.String)
                            rows[j][columns[i]] = CIPS.Express.ConvertDat.Tools.Byte2String(buf, ref l);
                        else if (tc == TypeCode.Object)
                        {
                            if (Datatypes[columnsdatatype[i]] == typeof(byte[]).FullName)
                            {
                                int nn = BitConverter.ToInt32(buf, l);
                                l += 4;
                                if (nn >= 0)
                                {
                                    byte[] bb = new byte[nn];
                                    Buffer.BlockCopy(buf, l, bb, 0, nn);
                                    rows[j][columns[i]] = bb;
                                    l += nn;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                if (n >= 0)
                    l += n * rows.Count;
            }
            off = l;
        }
        /// <summary>
        /// 复制表
        /// 无!
        /// cltable = 原表
        /// </summary>
        public void CopyFrom(ClTable cltable)
        {
            byte[] b = cltable.Serialize();
            int n = 0;
            Deserialize(b, ref n);
        }
        /// <summary>
        /// 写到文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool WriteToFile(string filename)
        {
            System.IO.FileStream fs = null;
            System.IO.BinaryWriter w = null;
            try
            {
                fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                w = new System.IO.BinaryWriter(fs);
                w.Write(this.Serialize());
                w.Close();
                fs.Close();
                return true;
            }
            catch { }
            finally
            {
                if (w != null)
                    w.Close();
                if (fs != null)
                    fs.Close();
            }
            return false;
        }
    }
    /// <summary>
    /// CL数据集
    /// </summary>
    public class ClSet
    {
        /// <summary>
        /// 表的集合
        /// </summary>
        public class TableCollection : List<ClTable>
        {
            /// <summary>
            /// 表
            /// </summary>
            /// <param name="TableName">表名</param>
            /// <returns>表</returns>
            public ClTable this[string TableName]
            {
                get
                {
                    TableName = TableName.Trim().ToUpper();
                    for (int i = 0; i < Count; i++)
                    {
                        if (this[i].TableName == TableName)
                            return this[i];
                    }
                    return null;
                }
            }
        }
        TableCollection _Tables = new TableCollection();
        /// <summary>
        /// 表的集合
        /// </summary>
        public TableCollection Tables
        {
            get
            {
                return _Tables;
            }
        }
        /// <summary>
        /// 相关对象
        /// </summary>
        public object Tag = null;
        /// <summary>
        /// CL数据集
        /// </summary>
        public ClSet()
        {
        }
        /// <summary>
        /// CL数据集
        /// </summary>
        /// <param name="ds">数据集</param>
        public ClSet(DataSet ds)
        {
            foreach (DataTable dt in ds.Tables)
            {
                Tables.Add(new ClTable(dt));
            }
        }
        /// <summary>
        /// CL数据集
        /// </summary>
        /// <param name="buf">字节流</param>
        public ClSet(byte[] buf)
        {
            Deserialize(buf, 0);
        }
        /// <summary>
        /// CL数据集
        /// </summary>
        /// <param name="buf">字节流</param>
        /// <param name="offset">偏移量</param>
        public ClSet(byte[] buf, int offset)
        {
            Deserialize(buf, offset);
        }
        /// <summary>
        /// 是否为CL数据集
        /// </summary>
        /// <param name="buf">字节流</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static bool isClSet(byte[] buf, int offset)
        {
            return CIPS.Express.ConvertDat.Tools.Byte2String(buf, ref offset) == ClSetVersion;
        }
        /// <summary>
        /// 获取CL数据集ID
        /// </summary>
        /// <param name="buf">字节流</param>
        /// <returns></returns>
        public static byte GetID(byte[] buf)
        {
            if (buf == null || buf.Length < 20)
                return 0xff;
            return buf[17];
        }
        /// <summary>
        /// CL数据集版本
        /// </summary>
        public const string ClSetVersion = "CS1.0";
        /// <summary>
        /// 数据集标识
        /// </summary>
        public byte ID = 0;
        /// <summary>
        /// 序列化本数据集
        /// 无!
        /// 
        /// </summary>
        public byte[] Serialize()
        {
            return Serialize(0);
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public byte[] Serialize(int offset)
        {
            byte[][] bufs;
            bufs = new byte[Tables.Count][];
            int n = 20 + offset;
            for (int i = 0; i < Tables.Count; i++)
            {
                bufs[i] = Tables[i].Serialize();
                n += bufs[i].Length;
            }
            byte[] buf;
            buf = new byte[n];
            n = offset;
            CIPS.Express.ConvertDat.Tools.String2Byte(ClSetVersion, buf, ref n);
            buf[offset + 17] = ID;
            BitConverter.GetBytes((short)Tables.Count).CopyTo(buf, offset + 18);
            n = 20 + offset;
            foreach (byte[] b in bufs)
            {
                Buffer.BlockCopy(b, 0, buf, n, b.Length);
                n += b.Length;
            }
            return buf;
        }
        void Deserialize(byte[] buf, int off)
        {
            Tables.Clear();
            int n = off;
            if (buf == null || buf.Length - off < 20 ||
                CIPS.Express.ConvertDat.Tools.Byte2String(buf, ref n) != ClSetVersion)
                return;
            ID = buf[off + 17];
            n = BitConverter.ToInt16(buf, off + 18);
            int l = 20 + off;
            for (int i = 0; i < n; i++)
            {
                ClTable ct = new ClTable(buf, ref l);
                Tables.Add(ct);
            }
        }
        /// <summary>
        /// 完全复制
        /// </summary>
        /// <param name="cs">源CL数据集</param>
        public void CopyFrom(ClSet cs)
        {
            if (cs == null)
                return;
            byte[] b = cs.Serialize();
            Deserialize(b, 0);
        }
        /// <summary>
        /// 攻取DataSet数据集
        /// </summary>
        /// <returns></returns>
        public DataSet GetDataSet()
        {
            DataSet ds = new DataSet();
            foreach (ClTable ct in Tables)
            {
                DataTable dt = ct.GetDataTable();
                if (ds.Tables[dt.TableName] != null)
                    ds.Tables.Remove(ds.Tables[dt.TableName]);
                ds.Tables.Add(ct.GetDataTable());
            }
            return ds;
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>成功返回TRUE</returns>
        public bool WriteToFile(string filename)
        {
            System.IO.FileStream fs = null;
            System.IO.BinaryWriter w = null;
            try
            {
                fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                w = new System.IO.BinaryWriter(fs);
                byte[] b = new byte[20];
                int n = 0;
                CIPS.Express.ConvertDat.Tools.String2Byte(ClSetVersion, b, ref n);
                BitConverter.GetBytes((short)Tables.Count).CopyTo(b, 18);
                w.Write(b);
                foreach (ClTable ct in Tables)
                    w.Write(ct.Serialize());
                w.Close();
                fs.Close();
                return true;
            }
            catch { }
            finally 
            {
                if (w != null)
                    w.Close();
                if (fs != null)
                    fs.Close();
            }
            return false;
        }
    }
}
