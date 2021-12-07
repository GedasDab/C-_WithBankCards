
using System;
using System.Diagnostics;
using SmartCard;
using SmartCard.Services;
using System.Text;

namespace MyCompany.MyOnCardApp
{

    /// <summary>
    /// A service that has methods that both are and are not under transaction.  The objective here is
    /// to demonstrate how fields are rolled back when an unexpected event such as an uncaught exception
    /// or card removal occur.  It also shows the use of OutOfTransaction attribute.
    /// </summary>
    public class MyService : MarshalByRefObject
    {
        ContentManager cm = (ContentManager)Activator.GetObject(typeof(ContentManager),      "ContentManager");
        // funkcija pasiimti numerius
        public byte[] getSerialNumber()
        {
            return cm.SerialNumber;
        }
        public string[] GetDirs(string path)
        {
            return cm.GetDirectories(path);
        }
        // funkcija kurti naują katalogą
        public int CreateDir(string path)
        {
            cm.CreateDirectory(path);
            return 0;
        }
        // funkcija kopijuoti failą iš kompiuterio į kortelę
        public int PutFile(string path, byte[] file)
        {
            cm.LoadFile(path, file);
            return 0;
        }
        //
        public byte[] getTheFile(string path)
        {
            return cm.GetFile(path);
        }
    }
}

