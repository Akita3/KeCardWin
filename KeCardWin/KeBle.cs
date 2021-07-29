using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;



namespace KeCardWin
{
    public class KeBle
    {

        // UUID
        const String KE_UUID_SERVICE_STR = "4ed10001-9710-f67e-885d-dc6be684707b";
        const String KE_UUID_STATUS_STR = "4ed10002-9710-f67e-885d-dc6be684707b";
        const String KE_UUID_IMAGE_NO_STR = "4ed10003-9710-f67e-885d-dc6be684707b";
        const String KE_UUID_COMMAND_STR = "4ed10004-9710-f67e-885d-dc6be684707b";
        Guid KE_UUID_SERVICE = new Guid(KE_UUID_SERVICE_STR);
        Guid KE_UUID_STATUS = new Guid(KE_UUID_STATUS_STR);
        Guid KE_UUID_IMAGE_NO = new Guid(KE_UUID_IMAGE_NO_STR);
        Guid KE_UUID_COMMAND = new Guid(KE_UUID_COMMAND_STR);


        // Command
        public const int KE_CMD_ERASE_FLASH = 0x01;
        public const int KE_CMD_DISPLAY = 0x03;

        // Status
        public const int KE_STATUS_FLASH_ERASE_SUCCESS = 0x11;
        public const int KE_STATUS_IMAGE_WRITE_SUCCESS = 0x21;
        public const int KE_STATUS_DISPLAY_IMAGE_SUCCESS = 0x31;

        // 画像Noの最大値 (0 - 2)
        public const int KE_IMAGE_NO_MAX = 2;

        // スキャン用
        BluetoothLEAdvertisementWatcher advWatcher;
        static List<ulong> scanDevices;

        // デバイス、サービス、キャラクタリスティック
        BluetoothLEDevice keDevice;
        GattDeviceService keService;
        GattCharacteristic keCharStatus;
        GattCharacteristic keCharImageNo;
        GattCharacteristic keCharCommand;

        // BLEアドレス→文字列変換
        static public string BleAddrToString(ulong addr)
        {
            string text = "";
            for (int i = 5; i >= 0; i--) text += ((addr >> (i * 8)) & 0xFF).ToString("X02");
            return text;
        }

        // スキャンデバイス
        public async Task<List<ulong>> ScanDevice(int sec) 
        {
            // 結果を初期化
            scanDevices = new List<ulong>();

            advWatcher = new BluetoothLEAdvertisementWatcher();
            advWatcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(1000);
            advWatcher.ScanningMode = BluetoothLEScanningMode.Active;
            advWatcher.Received += ScanWatcherReceived;

            // スキャン開始
            advWatcher.Start();

            // Wait 
            await Task.Delay(sec * 1000);

            // Stop Scan
            advWatcher.Stop();

            return scanDevices;
        }

        // スキャン停止
        public void StopScan()
        {
            // Stop Scan
            advWatcher.Stop();
        }


        // スキャン受信
        private async void ScanWatcherReceived(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            if (args.Advertisement.LocalName != "keCard") return;

            var addr = args.BluetoothAddress;

            if ( scanDevices.Contains(addr) == false )
            {
                scanDevices.Add(addr);
            }
        }


        // (E)CARDとBLE接続
        public async Task<bool> ConnectKeCard(ulong bleAddr)
        {

            keDevice = await BluetoothLEDevice.FromBluetoothAddressAsync(bleAddr);
            if (keDevice == null) return false;

            // Service
            var servicesResult = await keDevice.GetGattServicesForUuidAsync(KE_UUID_SERVICE);
            if (servicesResult.Status != GattCommunicationStatus.Success) return false;
            keService = servicesResult.Services[0];


            // Characteristics
            var charStatusResult = await keService.GetCharacteristicsForUuidAsync(KE_UUID_STATUS);
            if (charStatusResult.Status != GattCommunicationStatus.Success) return false;
            keCharStatus = charStatusResult.Characteristics[0];

            var charImageNoResult = await keService.GetCharacteristicsForUuidAsync(KE_UUID_IMAGE_NO , BluetoothCacheMode.Uncached);
            if (charImageNoResult.Status != GattCommunicationStatus.Success) return false;
            keCharImageNo = charImageNoResult.Characteristics[0];

            var charCommandResult = await keService.GetCharacteristicsForUuidAsync(KE_UUID_COMMAND);
            if (charCommandResult.Status != GattCommunicationStatus.Success) return false;
            keCharCommand = charCommandResult.Characteristics[0];


            return true;
        }

        // (ECARDとBLE切断
        public void DisconnectKeCard()
        {
            keService.Dispose();
            keDevice.Dispose();

            keDevice = null;
            keService = null;
            keCharStatus = null;
            keCharImageNo = null;
            keCharCommand = null;

        }


        // キャラクタリスティック書き込み
        public async Task<bool> WriteCharacteristic(GattCharacteristic c , byte[] data , GattWriteOption option = GattWriteOption.WriteWithoutResponse)
        {
            if (c == null) return false;

            var writer = new DataWriter();
            writer.WriteBytes(data);

            GattCommunicationStatus result = await c.WriteValueAsync(writer.DetachBuffer() , option);

            return (result == GattCommunicationStatus.Success);
        }

        // キャラクタリスティック読み込み
        public async Task<byte[]> ReadCharacteristic(GattCharacteristic c)
        {
            if (c == null) return null;

            GattReadResult result = await c.ReadValueAsync(BluetoothCacheMode.Uncached);
            if (result.Status != GattCommunicationStatus.Success) return null;

            var reader = DataReader.FromBuffer(result.Value);
            byte[] data = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(data);

            return data;
        }

        // コマンド送信
        public async Task<bool> SendCommand(byte cmd)
        {
            byte[] data = new byte[1] { cmd };
            bool r = await WriteCharacteristic(keCharCommand, data );
            return r;
        }

        // イメージデータ送信
        public async Task<bool> SendImageData(byte[] data)
        {
            bool r = await WriteCharacteristic(keCharCommand, data);
            return r;
        }

        // イメージ選択
        public async Task<bool> SelectImage(byte no)
        {
            byte[] data = new byte[1] { no };
            bool r = await WriteCharacteristic(keCharImageNo, data , GattWriteOption.WriteWithResponse);

            return r;
        }

        // ステータス読み込み
        public async Task<byte> ReadStatus()
        {
            byte[] data = await ReadCharacteristic(keCharStatus);
            if (data == null) return 0x00;
            return data[0];
        }

        // ステータス変化待ち
        public async Task<bool> WaitStatus(byte status , int waitSec)
        {
            bool res = false;
            for( int i = 0; i < waitSec; i ++ )
            {
                byte s = await ReadStatus();

                if( s == status )
                {
                    res = true;
                    break;
                }

                await Task.Delay(1000);
            }

            return res;
        }

    }
}
