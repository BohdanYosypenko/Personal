using System;
using System.Collections.Generic;
using System.Text;

namespace Personal
{
    [Serializable]
    public delegate void EmploeeStateHandler(object sender, EmploeeEventArgs e);
    [Serializable]
    public class EmploeeEventArgs
    {
        public string Message { get; set; }
        public EmploeeEventArgs(string mes)
        {
            Message = mes;
        }
    }
}
