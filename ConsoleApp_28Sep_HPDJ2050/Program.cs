using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using WIA; //Goto Project rightclick -> Properties -> Add References -> COM -> Microsoft.Windows.ImageAcquisition


namespace ConsoleApp_28Sep_HPDJ2050
{
    internal class Program
    {        
        static void Main(string[] args)
        {
            var deviceManager  =new DeviceManager();

            for (int i = 1; i<=deviceManager.DeviceInfos.Count; i++)
            {
                //1- Iterating and fetching all the connected devices:
                for(int j =1; j<= deviceManager.DeviceInfos[i].Properties.Count; j++)
                {
                    var prop = deviceManager.DeviceInfos[i].Properties[j].get_Value();
                    Console.WriteLine($"Name: {deviceManager.DeviceInfos[i].Properties[j].Name}");
                    Console.WriteLine($"PropertyID: {deviceManager.DeviceInfos[i].Properties[j].PropertyID}");
                    Console.WriteLine($"Type: {deviceManager.DeviceInfos[i].Properties[j].Type}");                
                    Console.WriteLine(prop);
                    Console.WriteLine("\n");
                }
                //var devName = deviceManager.DeviceInfos[i].Properties["Name"].get_Value();
                //Console.WriteLine(devName);

                if (deviceManager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    Console.WriteLine("This is not scanner");
                }
                else
                {
                    Console.WriteLine("This is a scanner");
                    //----------------------------------------------------------------------------

                    //2- Scan document:
                    var scanner = deviceManager.DeviceInfos[i];

                    var device = scanner.Connect();

                    // Start the scan (Tried on HP Deskjet 2050)
                    var item = device.Items[1];
                    var imageFile = (ImageFile)item.Transfer(FormatID.wiaFormatJPEG);
                    Console.WriteLine("scanned");
                    //----------------------------------------------------------------------------

                    //3- Save scanned image on drive:
                    var path = @"e:\scan.jpeg";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    imageFile.SaveFile(path);
                }
            }
        }
    }
}
