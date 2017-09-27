using System;
using UnityEngine;
using System.Security.Cryptography;
using System.IO;

namespace NBsn.NCryptography
{

public static class C_MD5
{
    public static byte[] Compute(byte[] byData)
    {
        try
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(byData);
            }
        }
        catch
        {
            return null;
        }
    }
 
}

}
