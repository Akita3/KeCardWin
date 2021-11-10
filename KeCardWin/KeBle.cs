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

    // デリゲート：
    public delegate void DelegateKeBleRecieveMsg(byte[] data);


    public class KeBle
    {

        // UUID
        const String KE_UUID_SERVICE_STR = "4ed10001-9710-f67e-885d-dc6be684707b";
        const String KE_UUID_STATUS_STR = "4ed10002-9710-f67e-885d-dc6be684707b";
        const String KE_UUID_IMAGE_NO_STR = "4ed10003-9710-f67e-885d-dc6be684707b";
        const String KE_UUID_COMMAND_STR = "4ed10004-9710-f67e-885d-dc6be684707b";
        const String KE_UUID_MSG_STR = "4ed10005-9710-f67e-885d-dc6be684707b";
        const String KE_UUID_MODE_STR = "4ed10006-9710-f67e-885d-dc6be684707b";
        Guid KE_UUID_SERVICE = new Guid(KE_UUID_SERVICE_STR);
        Guid KE_UUID_STATUS = new Guid(KE_UUID_STATUS_STR);
        Guid KE_UUID_IMAGE_NO = new Guid(KE_UUID_IMAGE_NO_STR);
        Guid KE_UUID_COMMAND = new Guid(KE_UUID_COMMAND_STR);
        Guid KE_UUID_MSG = new Guid(KE_UUID_MSG_STR);
        Guid KE_UUID_MODE = new Guid(KE_UUID_MODE_STR);


        // Command
        public const int KE_CMD_ERASE_FLASH = 0x01;
        public const int KE_CMD_DISPLAY = 0x03;
        public const int KE_CMD_FAST_MODE = 0x04;
        public const int KE_CMD_SLOW_MODE = 0x05;

        public const int KE_CMD_2BYTE = 0xFF;
        public const int KE_CMD_TIME = 0xFE;
        public const int KE_CMD_COND = 0xFD;
        public const int KE_CMD_ACT = 0xFC;

        public const int KE_CMD_2BYTE_LEN = 2;
        public const int KE_CMD_TIME_LEN = 6;
        public const int KE_CMD_COND_LEN = 2 + 4 + 8;
        public const int KE_CMD_ACT_LEN = 2 + 4 + 8;

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
        GattCharacteristic keCharMsg;
        GattCharacteristic keCharMode;

        // 状態変数
        public bool hasConnected = false;
        public bool fastMode = true;

        // デリゲート
        public DelegateKeBleRecieveMsg delegateKeBleRecieveMsg = null;


        // BLEアドレス→文字列変換
        static public string BleAddrToString(ulong addr)
        {
            string text = "";
            for (int i = 5; i >= 0; i--)
            {
                text += ((addr >> (i * 8)) & 0xFF).ToString("X02");
                if (i != 0) text += ":";
            }
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
            if (advWatcher == null) return;
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

            keDevice.ConnectionStatusChanged += OnConnectionStatusChanged;

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

#if true  // Ver2.0以降
            var charMsgResult = await keService.GetCharacteristicsForUuidAsync(KE_UUID_MSG);
            if (charMsgResult.Status != GattCommunicationStatus.Success) return false;
            keCharMsg = charMsgResult.Characteristics[0];

            // CCCD への書き込みが必要。これがないとイベントハンドラが呼ばれない。
            var charMsgNotifystatus
                = await keCharMsg.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);

            if (charMsgNotifystatus == GattCommunicationStatus.Success)
            {
                keCharMsg.ValueChanged += RecieveMsg;
            }

            // Mode
            var charModeResult = await keService.GetCharacteristicsForUuidAsync(KE_UUID_MODE);
            if (charModeResult.Status != GattCommunicationStatus.Success) return false;
            keCharMode = charModeResult.Characteristics[0];

#endif

            return true;
        }

        // (ECARDとBLE切断
        public void DisconnectKeCard()
        {
            // hasConnected = false;

            if (keService != null) keService.Dispose();
            if(keDevice != null ) keDevice.Dispose();

            // keDevice.ConnectionStatusChanged -= OnConnectionStatusChanged;

            keDevice = null;
            keService = null;
            keCharStatus = null;
            keCharImageNo = null;
            keCharCommand = null;
            keCharMsg = null;
            keCharMode = null;

        }

        // キャラクタリスティック変化通知
        public void RecieveMsg(GattCharacteristic sender, GattValueChangedEventArgs eventArgs)
        {
            Console.WriteLine($"characteristicChanged...Length={eventArgs.CharacteristicValue.Length}");
            byte[] data = new byte[eventArgs.CharacteristicValue.Length];
            Windows.Storage.Streams.DataReader.FromBuffer(eventArgs.CharacteristicValue).ReadBytes(data);

            var tmp = BitConverter.ToString(data);
            Console.WriteLine($"characteristicChanged...{tmp}");

            if(delegateKeBleRecieveMsg != null )
            {
                delegateKeBleRecieveMsg(data);
            }

            return;
        }


        // キャラクタリスティック書き込み
        public async Task<bool> WriteCharacteristic(GattCharacteristic c , byte[] data , GattWriteOption option = GattWriteOption.WriteWithoutResponse)
        {
            if (hasConnected == false) return false;
            if (c == null) return false;

            var writer = new DataWriter();
            writer.WriteBytes(data);

            GattCommunicationStatus result = await c.WriteValueAsync(writer.DetachBuffer() , option);

            return (result == GattCommunicationStatus.Success);
        }

        // キャラクタリスティック読み込み
        public async Task<byte[]> ReadCharacteristic(GattCharacteristic c)
        {
            if (hasConnected == false) return null;
            if (c == null) return null;

            GattReadResult result = await c.ReadValueAsync(BluetoothCacheMode.Uncached);
            if (result.Status != GattCommunicationStatus.Success) return null;

            var reader = DataReader.FromBuffer(result.Value);
            byte[] data = new byte[reader.UnconsumedBufferLength];
            reader.ReadBytes(data);

            return data;
        }

        // コマンド送信(1byte)
        public async Task<bool> SendCommand(byte cmd)
        {
            if (hasConnected == false) return false;

            byte[] data = new byte[1] { cmd };
            bool r = await WriteCharacteristic(keCharCommand, data );
            return r;
        }

        // コマンド送信(MultiByte)
        public async Task<bool> SendCommand(byte[] cmd)
        {
            if (hasConnected == false) return false;

            bool r = await WriteCharacteristic(keCharCommand, cmd);
            return r;
        }


        // イメージデータ送信
        public async Task<bool> SendImageData(byte[] data)
        {
            if (hasConnected == false) return false;

            bool r = await WriteCharacteristic(keCharCommand, data);
            return r;
        }

        // イメージ選択
        public async Task<bool> SelectImage(byte no)
        {
            if (hasConnected == false) return false;

            byte[] data = new byte[1] { no };
            bool r = await WriteCharacteristic(keCharImageNo, data , GattWriteOption.WriteWithResponse);

            return r;
        }

        // Timeコマンド送信)
        public async Task<bool> SendTimeCmd()
        {
            if (hasConnected == false) return false;

            byte[] data = GetTimeCmdPacket();
            bool r = await SendCommand(data);
            return r;
        }

        // Runbook条件コマンド送信
        public async Task<bool> SendCondCmd(Runbook.CondPacket cond)
        {
            if (hasConnected == false) return false;

            byte[] data = GetCondCmdPacket(cond);
            bool r = await SendCommand(data);
            return r;
        }

        // Runbookアクションコマンド送信
        public async Task<bool> SendActCmd(Runbook.ActionPacket act)
        {
            if (hasConnected == false) return false;

            byte[] data = GetActCmdPacket(act);
            bool r = await SendCommand(data);
            return r;
        }

        // ステータス読み込み
        public async Task<byte> ReadStatus()
        {
            if (hasConnected == false) return 0x00;

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

        // モード読み込み
        public async Task<bool> ReadMode()
        {
            if (hasConnected == false) return false;

            byte[] data = await ReadCharacteristic(keCharMode);
            if (data == null) return false;
            return data[0] == 0x00 ? false : true;
        }


        // BLE切断まで待つ
        public async Task<bool> WaitDisconnect(int waitSec)
        {
            bool res = false;
            for (int i = 0; i < waitSec; i++)
            {
                if (!hasConnected)
                {
                    res = true;
                    break;
                }

                await Task.Delay(1000);
            }

            return res;
        }


        // BLE接続状態変化
        private void OnConnectionStatusChanged(BluetoothLEDevice sender, object args)
        {

            hasConnected = (sender.ConnectionStatus == BluetoothConnectionStatus.Connected);
            Debug.WriteLine("OnConnectionStatusChanged : " + 
                (hasConnected ? "Connected" : "Disconnected" )
                );

        }


        // Timeコマンドパケット取得
        public static byte[] GetTimeCmdPacket()
        {
            int unixTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            byte[] cmd = new byte[KeBle.KE_CMD_TIME_LEN];

            cmd[0] = KeBle.KE_CMD_TIME;
            cmd[1] = KeBle.KE_CMD_2BYTE;
            cmd[2] = (byte)(unixTime & 0xFF);
            cmd[3] = (byte)((unixTime >> 8) & 0xFF);
            cmd[4] = (byte)((unixTime >> 16) & 0xFF);
            cmd[5] = (byte)((unixTime >> 24) & 0xFF);

            return cmd;
        }

        // Runbook条件パケット取得
        public static byte[] GetCondCmdPacket(Runbook.CondPacket cond)
        {
            byte[] cmd_id = { Cmd.CMD_COND, Cmd.CMD_2BYTE };
            byte[] data = Runbook.CondPacket.ToBytes(cond);
            byte[] packet = cmd_id.Concat(data).ToArray();

            return packet;
        }

        // Runbookアクションパケット取得
        public static byte[] GetActCmdPacket(Runbook.ActionPacket act)
        {
            byte[] cmd_id = { Cmd.CMD_ACT, Cmd.CMD_2BYTE };
            byte[] data = Runbook.ActionPacket.ToBytes(act);
            byte[] packet = cmd_id.Concat(data).ToArray();

            return packet;
        }


    }
}
