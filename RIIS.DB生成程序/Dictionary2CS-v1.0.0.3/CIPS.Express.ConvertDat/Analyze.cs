using System;
using System.Collections.Generic;
using System.Text;

namespace CIPS.Express.ConvertDat
{
    /// <summary>
    /// ͳ�Ʒ���
    /// ͳ�Ʒ�����Ԥ��
    /// </summary>
    public class Analyze
    {
        /// <summary>
        /// ID_��泵������
        /// 1
        /// </summary>
        public const int ID_CurrentCars = 1;
        /// <summary>
        /// ͣ����ʱ����
        /// </summary>
        public const int ID_OldCars = 2;
        /// <summary>
        /// �ص㳵
        /// </summary>
        public const int ID_EmphasesCars = 3;
        /// <summary>
        /// ������
        /// </summary>
        public const int ID_ExchangeCars = 4;
        /// <summary>
        /// ���ᳵ
        /// </summary>
        public const int ID_ClassiCars = 5;
        /// <summary>
        /// ����ռ��
        /// </summary>
        public const int ID_SplittingTracks = 6;
        /// <summary>
        /// ����ռ��
        /// </summary>
        public const int ID_DeparttingTracks = 7;
        /// <summary>
        /// ���ﳡռ����
        /// </summary>
        public const int ID_RateOfYardArr = 8;
        /// <summary>
        /// ���鳡ռ����
        /// </summary>
        public const int ID_RateOfYardSwit = 9;
        /// <summary>
        /// ������ռ����
        /// </summary>
        public const int ID_RateOfYardDep = 10;
        /// <summary>
        /// װ��
        /// </summary>
        public const int ID_UnloaddedCars = 11;
        /// <summary>
        /// ж��
        /// </summary>
        public const int ID_LoaddedCars = 12;
        /// <summary>
        /// �е���ʱ
        /// </summary>
        public const int ID_TimeForSwit = 13;
        /// <summary>
        /// �޵���ʱ
        /// </summary>
        public const int ID_TimeForUnswit = 14;
        /// <summary>
        /// ͣʱ
        /// </summary>
        public const int ID_TimeForStay = 15;
        /// <summary>
        /// ��������
        /// </summary>
        public const int ID_CarsProcess = 16;
        /// <summary>
        /// ��������
        /// </summary>
        public const int ID_TrainsProcess = 17;
        /// <summary>
        /// ��������
        /// </summary>
        public const int ID_TrainsSplit = 18;
        /// <summary>
        /// ��������
        /// </summary>
        public const int ID_TrainsClassi = 19;
        /// <summary>
        /// ������
        /// </summary>
        public const int ID_TrainsIntime = 20;
        /// <summary>
        /// ��������
        /// </summary>
        public const int ID_SplitCapability = 21;
        /// <summary>
        /// ��������
        /// </summary>
        public const int ID_SwitCapability = 22;
        /// <summary>
        /// ���е�������
        /// </summary>
        public const int ID_ArrDepCapability_Up = 23;
        /// <summary>
        /// ���ﳡ����
        /// </summary>
        public const int ID_YardCapability_Arr = 23;
        /// <summary>
        /// ���е�������
        /// </summary>
        public const int ID_ArrDepCapability_Down = 25;
        /// <summary>
        /// ����������
        /// </summary>
        public const int ID_YardCapability_Dep = 25;
        /// <summary>
        /// �ۺ���ʱ
        /// </summary>
        public const int ID_TimeForSynthesis = 24;
        /// <summary>
        /// �����ʺ�ͨ������
        /// </summary>
        public const int ID_SwitchCapability_Up = 26;
        /// <summary>
        /// �����ʺ�����
        /// </summary>
        public const int ID_SwitchCapability_Down = 27;
        /// <summary>
        /// ���ﳡ�ʺ�����
        /// </summary>
        public const int ID_SwitchCapability_Arr = 26;
        /// <summary>
        /// �����ʺ�����
        /// </summary>
        public const int ID_SwitchCapability_Dep = 27;
        /// <summary>
        /// ����ռ����
        /// </summary>
        public const int ID_RateOfYardGoods = 28;
        /// <summary>
        /// ���޳�
        /// </summary>
        public const int ID_RepairCars = 29;
        /// <summary>
        /// ��װ��
        /// </summary>
        public const int ID_ReloadCars = 30;
        /// <summary>
        /// ������
        /// </summary>
        public const int ID_LocomInfo = 31;
        /// <summary>
        /// ����
        /// </summary>
        public const int ID_Wegh_Empty = 32;
        /// <summary>
        /// �г�
        /// </summary>
        public const int ID_TrainSum = 33;
        /// <summary>
        /// ����
        /// ��Сֵ
        /// </summary>
        public const int ParamID_ValueMin = 1;
        /// <summary>
        /// ����
        /// �м�ֵ
        /// </summary>
        public const int ParamID_ValueMid = 2;
        /// <summary>
        /// ����
        /// ���ֵ
        /// </summary>
        public const int ParamID_ValueMax = 3;
        /// <summary>
        /// ����
        /// ��Сֵ��ɫ
        /// </summary>
        public const int ParamID_ColorMin = 4;
        /// <summary>
        /// ����
        /// �м�ֵ��ɫ
        /// </summary>
        public const int ParamID_ColorMid = 5;
        /// <summary>
        /// ����
        /// ���ֵ��ɫ
        /// </summary>
        public const int ParamID_ColorMax = 6;
        /// <summary>
        /// ����
        /// ������ɫ
        /// </summary>
        public const int ParamID_ColorCurve = 7;
        /// <summary>
        /// ����
        /// �Ƿ���ʾ
        /// </summary>
        public const int ParamID_Display = 8;
        /// <summary>
        /// ����
        /// �Զ���
        /// </summary>
        public const int ParamID_User = 100;
    }
}
