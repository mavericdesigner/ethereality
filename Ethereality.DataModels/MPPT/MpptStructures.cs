using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ethereality.DataModels.MPPT
{
   public struct MpptMsgStruct
   {
       public int Voltage { get; set; }
       public int Current { get; set; }
       public int Battery { get; set; }
       public int Temperature { get; set; }
   }

    public struct MpptMessages
    {
        public MpptMsgStruct Mppt0 { get; set; }
        public MpptMsgStruct Mppt1 { get; set; }
        public MpptMsgStruct Mppt2 { get; set; }
        public MpptMsgStruct Mppt3 { get; set; }
        public MpptMsgStruct Mppt4 { get; set; }
        public MpptMsgStruct Mppt5 { get; set; }
    }
}
