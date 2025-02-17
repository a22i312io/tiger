using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

public class NetworkInterfaceData
{
    public string InterfaceName;
    public IPAddress IpAddress;
    public PhysicalAddress MacAddress;

    public NetworkInterfaceData(string InterfaceName, IPAddress IpAddress, PhysicalAddress MacAddress)
    {
        this.InterfaceName = InterfaceName;
        this.IpAddress = IpAddress;
        this.MacAddress = MacAddress;
    }

    /// <summary>
    /// �l�b�g���[�N�@��̃��X�g��Ԃ�
    /// </summary>
    /// <returns>List<NetworkInterfaceData></returns>
    public static List<NetworkInterfaceData> GetIpAddress()
    {
        List<NetworkInterfaceData> networkInterfaces = new List<NetworkInterfaceData>();

        // NetworkInterface���擾
        NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();

        foreach (var ni in nis)
        {
            IPInterfaceProperties ipip = ni.GetIPProperties();
            PhysicalAddress physicalAddress = ni.GetPhysicalAddress();
            UnicastIPAddressInformationCollection uipaic = ipip.UnicastAddresses;

            // Debug.Log("�ڑ���:" + ni.Name);
            // Debug.Log("����:" + ni.Description);
            // Debug.Log("���:" + ni.NetworkInterfaceType);
            // Debug.Log("���x:" + ni.Speed);
            // Debug.Log("MAC(����)�A�h���X:" + ni.GetPhysicalAddress());

            foreach (var uipai in uipaic)
            {
                if (ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                    uipai.Address.AddressFamily.ToString() == "InterNetwork" &&
                    ni.Name != "lo" &&
                    ni.Name != "lo0" &&
                    uipai.Address.ToString().IndexOf("169.254") == -1)  // Link Local Address �͏���
                {
                    networkInterfaces.Add(new NetworkInterfaceData(ni.Name, uipai.Address, physicalAddress));
                }
            }
        }

        return networkInterfaces;
    }
}