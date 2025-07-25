using System;
using System.IO;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace HWIDChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "HWID Checker";
            SetConsoleColor(ConsoleColor.Cyan);
            
            
            ShowLogo();
            
            
            string hwid = GetHWID();
            
            
            DisplaySystemInfo();
            
            
            SetConsoleColor(ConsoleColor.Green);
            Console.WriteLine("\n====== ВАШ HWID ======");
            SetConsoleColor(ConsoleColor.White);
            Console.WriteLine(hwid);
            SetConsoleColor(ConsoleColor.Green);
            Console.WriteLine("=====================\n");
            
            
            SetConsoleColor(ConsoleColor.Yellow);
            Console.Write("Сохранить HWID в файл? (y/n): ");
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                SaveHWIDToFile(hwid);
            }
            
            Console.WriteLine("\n");
            SetConsoleColor(ConsoleColor.Cyan);
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        
        
        private static void ShowLogo()
        {
            Console.WriteLine(@"
╔═══════════════════════════════════════════╗
║                                           ║
║           Hwid Checker v1.0               ║
║                                           ║
╚═══════════════════════════════════════════╝
");
            Console.WriteLine("Этот инструмент собирает и отображает уникальный HWID вашей системы");
            Console.WriteLine("Используйте этот HWID для запроса активации программного обеспечения");
            Console.WriteLine();
        }
        
        
        private static string GetHWID()
        {
            
            string cpuId = GetCPUId();
            string motherboardId = GetMotherboardId();
            string diskId = GetDiskId();
            
           
            string combined = cpuId + motherboardId + diskId;
            
            
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                
                return builder.ToString();
            }
        }
        
        
        private static string GetCPUId()
        {
            string cpuId = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                
                foreach (ManagementObject mo in moc)
                {
                    cpuId = mo["ProcessorId"].ToString();
                    break;
                }
            }
            catch
            {
                cpuId = "CPU_ERR";
            }
            
            return cpuId;
        }
        
        
        private static string GetMotherboardId()
        {
            string mbId = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_BaseBoard");
                ManagementObjectCollection moc = mc.GetInstances();
                
                foreach (ManagementObject mo in moc)
                {
                    mbId = mo["SerialNumber"].ToString();
                    break;
                }
            }
            catch
            {
                mbId = "MB_ERR";
            }
            
            return mbId;
        }
        
       
        private static string GetDiskId()
        {
            string diskId = "";
            try
            {
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                
                foreach (ManagementObject mo in moc)
                {
                    diskId = mo["SerialNumber"].ToString();
                    break;
                }
            }
            catch
            {
                diskId = "DISK_ERR";
            }
            
            return diskId;
        }
        
       
        private static void DisplaySystemInfo()
        {
            SetConsoleColor(ConsoleColor.Yellow);
            Console.WriteLine("===== ИНФОРМАЦИЯ О СИСТЕМЕ =====");
            
           
            SetConsoleColor(ConsoleColor.White);
            Console.Write("ЦП: ");
            try
            {
                ManagementObjectSearcher cpuSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                foreach (ManagementObject obj in cpuSearcher.Get())
                {
                    SetConsoleColor(ConsoleColor.Green);
                    Console.WriteLine(obj["Name"]);
                    SetConsoleColor(ConsoleColor.Gray);
                    Console.WriteLine("  ID процессора: " + obj["ProcessorId"]);
                }
            }
            catch
            {
                SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Не удалось получить данные о процессоре");
            }
            
           
            SetConsoleColor(ConsoleColor.White);
            Console.Write("Материнская плата: ");
            try
            {
                ManagementObjectSearcher mbSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                foreach (ManagementObject obj in mbSearcher.Get())
                {
                    SetConsoleColor(ConsoleColor.Green);
                    Console.WriteLine(obj["Manufacturer"] + " " + obj["Product"]);
                    SetConsoleColor(ConsoleColor.Gray);
                    Console.WriteLine("  Серийный номер: " + obj["SerialNumber"]);
                }
            }
            catch
            {
                SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Не удалось получить данные о материнской плате");
            }
            
           
            SetConsoleColor(ConsoleColor.White);
            Console.WriteLine("Жесткие диски:");
            try
            {
                ManagementObjectSearcher diskSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                foreach (ManagementObject obj in diskSearcher.Get())
                {
                    SetConsoleColor(ConsoleColor.Green);
                    Console.WriteLine("  " + obj["Model"]);
                    SetConsoleColor(ConsoleColor.Gray);
                    Console.WriteLine("    Серийный номер: " + obj["SerialNumber"]);
                    double sizeGB = Convert.ToDouble(obj["Size"]) / (1024 * 1024 * 1024);
                    Console.WriteLine("    Размер: " + Math.Round(sizeGB, 2) + " GB");
                }
            }
            catch
            {
                SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("  Не удалось получить данные о дисках");
            }
            
            
            SetConsoleColor(ConsoleColor.White);
            Console.Write("Операционная система: ");
            try
            {
                ManagementObjectSearcher osSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                foreach (ManagementObject obj in osSearcher.Get())
                {
                    SetConsoleColor(ConsoleColor.Green);
                    Console.WriteLine(obj["Caption"] + " " + obj["Version"]);
                }
            }
            catch
            {
                SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Не удалось получить данные об операционной системе");
            }
        }
        
       
        private static void SaveHWIDToFile(string hwid)
        {
            try
            {
                string filename = "hwid.txt";
                File.WriteAllText(filename, hwid);
                SetConsoleColor(ConsoleColor.Green);
                Console.WriteLine($"HWID успешно сохранен в файл {filename}");
            }
            catch (Exception ex)
            {
                SetConsoleColor(ConsoleColor.Red);
                Console.WriteLine("Ошибка при сохранении файла: " + ex.Message);
            }
        }
        
        
        private static void SetConsoleColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
    }
}