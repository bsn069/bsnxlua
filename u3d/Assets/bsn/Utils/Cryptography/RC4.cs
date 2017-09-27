using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NBsn.NCryptography
{


public class C_RC4
{
    public bool SetKey(byte[] key)
    {
        if (key == null)
            return false;
        if (key.Length < s_keySize)
            return false;
        Ksa(m_keyTable, key);
        return true;
    }

    public int Encrypt(byte[] bySrc, ref byte[] byDest, int srcLength = 0)
    {
        if (bySrc == null) {
            return 0;
        }

        if (byDest == null) 
        {
            byDest = new byte[bySrc.Length];
        }

        if (srcLength == 0)
        {
            srcLength = bySrc.Length;
        }

        return EncryptImpl(byDest, bySrc, srcLength, m_keyTable);
    }

    public int Decrypt(byte[] bySrc, ref byte[] byDest, int srcLength = 0)
    {
        return Encrypt(bySrc, ref byDest, srcLength);
    }
 
    private byte[] m_keyTable = new byte[256];
    private static int s_keySize = 16;
    

#region private function
    private void Swap(byte[] buffer, int idx1, int idx2)
    {
        byte temp;
        temp = buffer[idx1];
        buffer[idx1] = buffer[idx2];
        buffer[idx2] = temp;
    }

    private void Ksa(byte[] buffer, byte[] key)
    {
        int i = 0, j = 0;
        for (i = 0; i < 256; i++)
            buffer[i] = (byte)i;
        for (i = 0, j = 0; i < 256; i++)
        {
            j = (j + buffer[i] + key[i % s_keySize]) % 256;
            Swap(buffer, i, j);
        }
    }

    private int EncryptImpl(byte[] desBuf, byte[] srcBuf, int length, byte[] key)
    {
        if (desBuf.Length < length)
        {
            return -1;
        }
        if (srcBuf.Length < length)
        {
            return -2;
        }

        int x = 0, y = 0, t = 0, i = 0;
        for (i = 0; i < length; i++)
        {
            x = (x + 1) % 256;
            y = (y + key[x]) % 256;
            Swap(key, x, y);
            t = (key[x] + key[y]) % 256;
            desBuf[i] = (byte)(srcBuf[i] ^ key[t]);
        }

        return length;
    }
#endregion private function
}

}