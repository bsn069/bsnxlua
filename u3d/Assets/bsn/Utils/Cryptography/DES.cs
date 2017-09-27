using System;
using UnityEngine;
using System.Security.Cryptography;
using System.IO;

namespace NBsn.NCryptography
{

public class C_DES
{
    static byte[] rgbIV =
    {
        0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
    };

    public byte[] Encrypt(byte[] byData, string strKey8Char)
    {
        try
        {
            using (var DCSP = new DESCryptoServiceProvider())  
            {  
                byte[] rgbKey = System.Text.Encoding.UTF8.GetBytes(strKey8Char); 
                using (var ce = DCSP.CreateEncryptor(rgbKey, rgbIV))  
                {  
                    using (var ms = new MemoryStream())  
                    {  
                        using (var cs = new CryptoStream(ms, ce, CryptoStreamMode.Write))  
                        {  
                            cs.Write(byData, 0, byData.Length);
                            cs.FlushFinalBlock(); 
                        }  
                        return ms.ToArray();  
                    }  
                }  
            }  
        }
        catch
        {
            return null;
        }
    }

    public byte[] Decrypt(byte[] byData, string strKey8Char)
    {
        try
        {
            using (var DCSP = new DESCryptoServiceProvider())  
            {  
                byte[] rgbKey = System.Text.Encoding.UTF8.GetBytes(strKey8Char); 
                using (var ce = DCSP.CreateDecryptor(rgbKey, rgbIV))  
                {  
                    using (var ms = new MemoryStream())  
                    {  
                        using (var cs = new CryptoStream(ms, ce, CryptoStreamMode.Write))  
                        {  
                            cs.Write(byData, 0, byData.Length);
                            cs.FlushFinalBlock(); 
                        }  
                        return ms.ToArray();  
                    }  
                }  
            }  
        }
        catch
        {
            return null;
        }
    }
}

}
