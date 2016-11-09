using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class FileOperateHelper
    {
        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FileFullPath">文件的全路径.</param>
        /// <returns>bool</returns>
        public static bool DeleteFile(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true) //用静态类判断文件是否存在
            {
                File.SetAttributes(FileFullPath, FileAttributes.Normal); //设置文件的属性为正常（如果文件为只读的话直接删除会报错）
                File.Delete(FileFullPath); //删除文件
                return true;
            }
            else //文件不存在
            {
                return false;
            }
        }
        #endregion

        #region 获取文件名（包含扩展名）
        /// <summary>
        /// 获取文件名（包含扩展名）
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>string</returns>
        public static string GetFileName(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileInfo F = new FileInfo(FileFullPath); //FileInfo类为提供创建、复制、删除等方法
                return F.Name; //获取文件名（包含扩展名）
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取文件文件扩展名
        /// <summary>
        /// 获取文件文件扩展名
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>string</returns>
        public static string GetFileExtension(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileInfo F = new FileInfo(FileFullPath);
                return F.Extension; //获取文件扩展名（包含".",如：".mp3"）
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 获取文件名（可包含扩展名）
        /// <summary>
        /// 获取文件名（可包含扩展名）
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <param name="IncludeExtension">是否包含扩展名</param>
        /// <returns>string</returns>
        public static string GetFileName(string FileFullPath, bool IncludeExtension)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileInfo F = new FileInfo(FileFullPath);
                if (IncludeExtension)
                {
                    return F.Name;   //返回文件名（包含扩展名）
                }
                else
                {
                    return F.Name.Replace(F.Extension, ""); //把扩展名替换为空字符
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 外部打开文件
        /// <summary>
        /// 根据传来的文件全路径，外部打开文件，默认用系统注册类型关联软件打开
        /// </summary>
        /// <param name="FileFullPath">文件的全路径</param>
        /// <returns>bool</returns>
        public static bool OpenFile(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                System.Diagnostics.Process.Start(FileFullPath); //打开文件，默认用系统注册类型关联软件打开
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 获取文件大小
        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>string</returns>
        public static string GetFileSize(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileInfo F = new FileInfo(FileFullPath);
                long FL = F.Length;
                if (FL > (1024 * 1024 * 1024)) //由大向小来判断文件的大小
                {
                    return Math.Round((FL + 0.00) / (1024 * 1024 * 1024), 2).ToString() + " GB"; //将双精度浮点数舍入到指定的小数（long类型与double类型运算，结果会是一个double类型）
                }
                else if (FL > (1024 * 1024))
                {
                    return Math.Round((FL + 0.00) / (1024 * 1024), 2).ToString() + " MB";
                }
                else if (FL > 1024)
                {
                    return Math.Round((FL + 0.00) / 1024, 2).ToString() + " KB";
                }
                else
                {
                    return FL.ToString();
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 文件转换成二进制
        /// <summary>
        /// 文件转换成二进制，返回二进制数组Byte[]
        /// </summary>
        /// <param name="FileFullPath">文件全路径</param>
        /// <returns>byte[] 包含文件流的二进制数组</returns>
        public static byte[] FileToStreamByte(string FileFullPath)
        {
            if (File.Exists(FileFullPath) == true)
            {
                FileStream FS = new FileStream(FileFullPath, FileMode.Open); //创建一个文件流
                byte[] fileData = new byte[FS.Length];                       //创建一个字节数组，用于保存流
                FS.Read(fileData, 0, fileData.Length);                       //从流中读取字节块，保存到缓存中
                FS.Close();                                                  //关闭流（一定得关闭，否则流一直存在）
                return fileData;                                             //返回字节数组
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region 二进制生成文件
        /// <summary>
        /// 二进制数组Byte[]生成文件
        /// </summary>
        /// <param name="FileFullPath">要生成的文件全路径</param>
        /// <param name="StreamByte">要生成文件的二进制 Byte 数组</param>
        /// <returns>bool 是否生成成功</returns>
        public static bool ByteStreamToFile(string FileFullPath, byte[] StreamByte)
        {
            try
            {
                if (File.Exists(FileFullPath) == true) //判断要创建的文件是否存在，若存在则先删除
                {
                    File.Delete(FileFullPath);
                }
                FileStream FS = new FileStream(FileFullPath, FileMode.OpenOrCreate); //创建文件流(打开或创建的方式)
                FS.Write(StreamByte, 0, StreamByte.Length); //把文件流写到文件中
                FS.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 写文件
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Strings">文件内容</param>
        public static void WriteFile(string FileFullPath, string Strings)
        {
            if (!System.IO.File.Exists(FileFullPath))
            {
                System.IO.FileStream fs = System.IO.File.Create(FileFullPath);
                fs.Close();
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(FileFullPath, false, System.Text.Encoding.GetEncoding("gb2312"));
            sw.Write(Strings);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        #endregion

        #region 读文件
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string FileFullPath)
        {
            string stemp = "";
            if (!System.IO.File.Exists(FileFullPath))
                stemp = "不存在相应的目录";
            else
            {
                StreamReader fr = new StreamReader(FileFullPath, System.Text.Encoding.GetEncoding("gb2312"));
                stemp = fr.ReadToEnd();
                fr.Close();
                fr.Dispose();
            }

            return stemp;
        }
        #endregion

        #region 追加文本
        /// <summary>
        /// 追加文本
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="strings">内容</param>
        public static void FileAdd(string FileFullPath, string strings)
        {
            StreamWriter sw = File.AppendText(FileFullPath);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
        }
        #endregion

        #region Xml文件序列化
        /// <summary>
        /// 将Xml文件序列化(可起到加密和压缩XML文件的目的)
        /// </summary>
        /// <param name="FileFullPath">要序列化的XML文件全路径</param>
        /// <returns>bool 是否序列化成功</returns>
        public static bool SerializeXml(string FileFullPath)   //序列化：
        {
            try
            {
                System.Data.DataSet DS = new System.Data.DataSet(); //创建数据集，用来临时存储XML文件
                DS.ReadXml(FileFullPath); //将XML文件读入到数据集中
                FileStream FS = new FileStream(FileFullPath + ".tmp", FileMode.OpenOrCreate); //创建一个.tmp的临时文件
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter FT = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(); //使用二进制格式化程序进行序列化
                FT.Serialize(FS, DS); //把数据集序列化后存入文件中
                FS.Close(); //一定要关闭文件流，否则文件改名会报错（文件正在使用错误）
                DeleteFile(FileFullPath); //删除原XML文件
                File.Move(FileFullPath + ".tmp", FileFullPath); //改名(把临时文件名改成原来的xml文件名)
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 反序列化XML文件
        /// <summary>
        /// 反序列化XML文件
        /// </summary>
        /// <param name="FileFullPath">要反序列化XML文件的全路径</param>
        /// <returns>bool 是否反序列化XML文件</returns>
        public static bool DeSerializeXml(string FileFullPath)
        {
            FileStream FS = new FileStream(FileFullPath, FileMode.Open); //打开XML文件流
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter FT = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter(); //使用二进制格式化程序进行序列化
            ((System.Data.DataSet)FT.Deserialize(FS)).WriteXml(FileFullPath + ".tmp"); //把文件反序列化后存入.tmp临时文件中
            FS.Close(); //关闭并释放流
            DeleteFile(FileFullPath); //删除原文件
            File.Move(FileFullPath + ".tmp", FileFullPath); //改名(把临时文件改成原来的xml文件)
            return true;
        }
        #endregion

        #region 压缩文件
        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="destinationFile">目标文件</param>
        public static void CompressFile(string sourceFile, string destinationFile)
        {
            // 文件是否存在
            if (File.Exists(sourceFile) == false)
            {
                throw new FileNotFoundException();
            }

            byte[] buffer = null;
            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream compressedStream = null;

            try
            {
                // 读取源文件到byte数组
                sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);

                buffer = new byte[sourceStream.Length];
                int checkCounter = sourceStream.Read(buffer, 0, buffer.Length);

                if (checkCounter != buffer.Length)
                {
                    throw new ApplicationException();
                }

                destinationStream = new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write);

                compressedStream = new GZipStream(destinationStream, CompressionMode.Compress, true);

                compressedStream.Write(buffer, 0, buffer.Length);
            }
            catch (ApplicationException ex)
            {
                throw (ex);
            }
            finally
            {
                if (sourceStream != null)
                    sourceStream.Close();

                if (compressedStream != null)
                    compressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }
        }
        #endregion

        #region 解压文件
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="sourceFile">源文件</param>
        /// <param name="destinationFile">目标文件</param>
        public static void DeCompressFile(string sourceFile, string destinationFile)
        {
            if (File.Exists(sourceFile) == false)
            {
                throw new FileNotFoundException();
            }

            FileStream sourceStream = null;
            FileStream destinationStream = null;
            GZipStream decompressedStream = null;
            byte[] quartetBuffer = null;

            try
            {
                sourceStream = new FileStream(sourceFile, FileMode.Open);

                decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true);

                quartetBuffer = new byte[4];
                int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);

                byte[] buffer = new byte[checkLength + 100];

                int offset = 0;
                int total = 0;

                while (true)
                {
                    int bytesRead = decompressedStream.Read(buffer, offset, 100);

                    if (bytesRead == 0)
                        break;

                    offset += bytesRead;
                    total += bytesRead;
                }

                destinationStream = new FileStream(destinationFile, FileMode.Create);
                destinationStream.Write(buffer, 0, total);

                destinationStream.Flush();
            }
            catch (ApplicationException ex)
            {
                throw (ex);
            }
            finally
            {
                if (sourceStream != null)
                    sourceStream.Close();

                if (decompressedStream != null)
                    decompressedStream.Close();

                if (destinationStream != null)
                    destinationStream.Close();
            }

        }
        #endregion

    }
}
