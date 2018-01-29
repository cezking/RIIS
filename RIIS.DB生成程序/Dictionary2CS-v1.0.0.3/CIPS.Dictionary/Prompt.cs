using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;

namespace CIPS.Dictionary
{
    /// <summary>
    /// CIPS�û�������ʾ����
    /// </summary>
    public class Prompt:CIPS.Express.ConvertDat.Prompt
    {
        string weburl = null;
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="dt">DC_RA_PROMPT��</param>
        public Prompt(DataTable dt):base(dt)
        {
            Init();
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="ct">DC_RA_PROMPT��</param>
        public Prompt(ClTable ct):base(ct)
        {
            Init();
        }
        void Init()
        {
            weburl = System.Configuration.ConfigurationManager.AppSettings["WebService"];
            CannotFind += new PromptCannotFind(Prompt_CannotFind);
        }

        void Prompt_CannotFind(CIPS.Express.ConvertDat.Prompt.OnePrompt prompt)
        {
            SavePrompt(prompt);
        }
        /// <summary>
        /// WEB��ַ
        /// </summary>
        public string WebUrl
        {
            get
            {
                return weburl;
            }
            set
            {
                weburl = value;
            }
        }
        void SavePrompt(OnePrompt prompt)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(SaveThread));
            thread.Start(prompt);
        }
        void SaveThread(object obj)
        {
            string url = weburl;
            if (url == null)
                return;
            OnePrompt prompt = obj as OnePrompt;
            try
            {
                //string s = "Insert into DC_RA_PROMPT (C_ACCESS_CODE,C_CHINESE_CODE,C_DISPLAY_CODE,C_NAMESPACE) values('" + prompt.accessname + "','" + prompt.chinesename + "','" + prompt.displayname + "','" + prompt.Namespace + "')";
                //DictTables.ExecuteSqlCommand(url, s, 1);
            }
            catch { }
        }
    }
}
