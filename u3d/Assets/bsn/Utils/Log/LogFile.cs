using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace NBsn  
{

	
public class C_LogFile 
{
   // strDirName "log"
   public bool Init(string strDirName)
   {
       m_strDirName = strDirName;
       m_strDirPath = Application.persistentDataPath.PathCombine(m_strDirName);

       Directory.CreateDirectory(m_strDirPath);
       CleanupLogsOlderThan(30);

       CreateFile();
       if (m_streamWriter == null) {
           return false;
       }

       Application.logMessageReceivedThreaded += OnLogCallBack;
       return true;
   }

   public void UnInit()
   {
       FlushFoldedMessage();
       Application.logMessageReceivedThreaded -= OnLogCallBack;
       m_streamWriter.Close();
       m_streamWriter = null;
   }

   private void CreateFile() 
   {
       DateTime now = DateTime.Now;
       string strFileName = string.Format(
           "{0}_{1}.txt"
           , now.FormatDateAsFileNameString()
           , now.FormatTimeAsFileNameString()
       );
       string strFilePath = m_strDirPath.PathCombine(strFileName);

       try
       {
           m_streamWriter = new FileInfo(strFilePath).CreateText();
           m_streamWriter.AutoFlush = true;
       }
       catch (System.Exception ex)
       {
           m_streamWriter = null;           
           Debug.LogException(ex);
       }
   }

   private void OnLogCallBack(string condition, string stackTrace, LogType type)
   {
       try
       {
           ++m_seqId;

           if (type == LogType.Exception)
           {
               condition = string.Format("{0}\r\n  {1}", condition, stackTrace.Replace("\n", "\r\n  "));
           }

           if (condition == m_lastWrittenContent && m_lastWrittenType == type)
           {
               m_foldedCount++;
           }
           else 
           {
               FlushFoldedMessage();

               var strWriteText = string.Format("{0:0.00} {1} {3} {2}\r\n"
                   , Time.realtimeSinceStartup
                   , type
                   , condition
                   , m_seqId
               );
               Write(strWriteText);

               m_lastWrittenType       = type;
               m_lastWrittenContent    = condition;
           }
       }
       catch (System.Exception ex)
       {
           Debug.LogException(ex); 
       }
       //finally
       //{
 
       //}
   }

   private void FlushFoldedMessage()
   {
       if (m_foldedCount == 0) {
           return;
       }

       var strWriteText = string.Format("{0:0.00} ↑*{1}\r\n"
           , Time.realtimeSinceStartup
           , m_foldedCount
       );
       m_foldedCount = 0;
       Write(strWriteText);
   }

   private void Write(string strText)
   {
       m_streamWriter.Write(strText);
   }

   private void CleanupLogsOlderThan(int days)
   {
       DateTime timePointForDeleting = DateTime.Now.Subtract(TimeSpan.FromDays(days));
       string timeStrForDeleting = DateTimeEx.FormatDateAsFileNameString(timePointForDeleting);

       DirectoryInfo logDirInfo = new DirectoryInfo(m_strDirPath);
       DirectoryInfo[] dirsByDate = logDirInfo.GetDirectories();
       List<string> toBeDeleted = new List<string>();
       foreach (var item in dirsByDate)
       {
           //Log.Info("[COMPARING]: {0}, {1}", item.Name, timeStrForDeleting);
           if (string.CompareOrdinal(item.Name, timeStrForDeleting) <= 0)
           {
               toBeDeleted.Add(item.FullName);
               //Log.Info("[TO_BE_DELETED]: {0}", item.FullName);
           }
       }

       foreach (var item in toBeDeleted)
       {
           Directory.Delete(item, true);
           Debug.LogFormat("[ Log Cleanup ]: {0}", item);
       }
   }

   protected string m_strDirName = null;
   protected string m_strDirPath = null;
   protected StreamWriter m_streamWriter = null;
   protected uint m_seqId = 0;

   protected string m_lastWrittenContent = null;
   protected LogType m_lastWrittenType = LogType.Exception;
   protected uint m_foldedCount = 0;
}
}
