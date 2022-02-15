using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace KeCardWin
{

    // 定義
    public class KeConfDef
    {

        public static UInt32 MSEC_TO_UNITS(double time , UInt32 resolution)
        {
            return (UInt32)(((time) * 1000) / (resolution));
        }

        public const UInt32 UNIT_1_25_MS = 1250;    /**< Number of microseconds in 1.25 milliseconds. */
        public const UInt32 UNIT_10_MS = 10000;


        // The advertising interval (in units of 0.625 ms.).
        // 初期値
        public const UInt16 KECONF_ADV_SLOW_INTERVAL = 2056;            /* 1285 ms */
        // 設定値の最大と最小
        public const UInt16 KECONF_ADV_SLOW_INTERVAL_MIN = 32;        /* 20 ms */
        public const UInt16 KECONF_ADV_SLOW_INTERVAL_MAX = 16384;     /* 10240ms */


        // 低速アドバタイジング継続時間
        // 初期値
        public const UInt32 KECONF_ADV_SLOW_DURATION = 7200;    /* 2h */
        // 設定値の最大と最小
        public const UInt32 KECONF_ADV_SLOW_DURATION_MIN = 0;
        public const UInt32 KECONF_ADV_SLOW_DURATION_MAX = 8640000;


        // Minimum acceptable connection interval, Connection interval uses 1.25 ms units.
        // 初期値
        public static UInt16 KECONF_SLOW_CONN_INTERVAL_MIN = (UInt16)MSEC_TO_UNITS(1500, UNIT_1_25_MS);
        // 設定値の最大と最小
        public static UInt16 KECONF_CONN_INTERVAL_MIN = (UInt16)MSEC_TO_UNITS(7.5, UNIT_1_25_MS);
        public static UInt16 KECONF_CONN_INTERVAL_MAX = (UInt16)MSEC_TO_UNITS(4000, UNIT_1_25_MS);


        // Maximum acceptable connection interval, Connection interval uses 1.25 ms units.
        // 初期値
        public static UInt16 KECONF_SLOW_CONN_INTERVAL_MAX = (UInt16)MSEC_TO_UNITS(2000, UNIT_1_25_MS);


        // Slave latency.
        // 初期値
        public const UInt16 KECONF_SLOW_SLAVE_LATENCY = 0;
        // 設定値の最大と最小
        public const UInt16 KECONF_SLAVE_LATENCY_MIN = 0;
        public const UInt16 KECONF_SLAVE_LATENCY_MAX = 500;


        // Connection supervisory timeout, Supervision Timeout uses 10 ms units.
        // 初期値
        public static UInt16 KECONF_FAST_CONN_TIMEOUT = (UInt16)MSEC_TO_UNITS(4000, UNIT_10_MS);
        public static UInt16 KECONF_SLOW_CONN_TIMEOUT = (UInt16)MSEC_TO_UNITS(6000, UNIT_10_MS);
        // 設定値の最大と最小
        public static UInt16 KECONF_CONN_TIMEOUT_MIN = (UInt16)MSEC_TO_UNITS(100, UNIT_10_MS);
        public static UInt16 KECONF_CONN_TIMEOUT_MAX = (UInt16)MSEC_TO_UNITS(32000, UNIT_10_MS);


        // Auto Disconnect Timeout
        // 初期値
        public const UInt32 KECONF_AUTO_DISCON_TIME_FAST = 180;
        public const UInt32 KECONF_AUTO_DISCON_TIME_SLOW = 259200;     /* 3day : 60 * 60 * 24 */
        // 設定値の最大と最小
        public const UInt32 KECONF_AUTO_DISCON_TIME_MIN = (180);
        public const UInt32 KECONF_AUTO_DISCON_TIME_MAX = (0xFFFFFFFF);/* 1day : 60 * 60 * 24 */
        public const UInt32 KECONF_DISCON_TIME_INF = 0;



        // Rssi interval
        // 初期値
        public const UInt16 KECONF_RSSI_IVL = 1000;
        // 設定値の最大と最小
        public const UInt16 KECONF_RSSI_IVL_MIN = 1000;
        public const UInt16 KECONF_RSSI_IVL_MAX = 0xFFFF;


        // Read data interval
        // 初期値
        public const UInt16 KECONF_READ_DATA_IVL = 200;
        // 設定値の最大と最小
        public const UInt16 KECONF_READ_DATA_IVL_MIN = 200;
        public const UInt16 KECONF_READ_DATA_IVL_MAX = 0xFFFF;



        // Images enable
        // 初期値
        public const UInt32 KECONF_IMAGES_ENABLE = 0x7;
        // 設定値の最大と最小
        public const UInt32 KECONF_IMAGES_ENABLE_MIN = 0x0;
        public const UInt32 KECONF_IMAGES_ENABLE_MAX = 0x7;


        // Ble Tx Power
        // 初期値
        public const sbyte KECONF_BLE_TX_POWER = 0;
        // 設定値の最大と最小
        public const sbyte KECONF_BLE_TX_POWER_MIN = -20;
        public const sbyte KECONF_BLE_TX_POWER_MAX = 4;


        // Debug print ram
        // 初期値
        public const byte KECONF_DEBUG_PRINT_RAM = 0;


        // Check value
        public const byte KECONF_CHECK_VALUE = 0xAA;


        public static void SetDefaultKeConfig(ref KeConfig _keConfig)
        {
            // adv_slow
            _keConfig.advSlowEnable = 0x01;
            _keConfig.advSlowInterval = KECONF_ADV_SLOW_INTERVAL;
            _keConfig.advSlowDuration = KECONF_ADV_SLOW_DURATION;
            // conn_slow
            _keConfig.connSlowMinConnInterval = KECONF_SLOW_CONN_INTERVAL_MIN;
            _keConfig.connSlowMaxConnInterval = KECONF_SLOW_CONN_INTERVAL_MAX;
            _keConfig.connSlowSlaveLatency = KECONF_SLOW_SLAVE_LATENCY;
            _keConfig.connSlowConnSupTimeout = KECONF_SLOW_CONN_TIMEOUT;
            // auto_discon_time_fast
            _keConfig.autoDisconTimeFast = KECONF_AUTO_DISCON_TIME_FAST;
            // auto_discon_time_slow
            _keConfig.autoDisconTimeSlow = KECONF_AUTO_DISCON_TIME_SLOW;
            // rssi_ivl
            _keConfig.rssiIvl = KECONF_RSSI_IVL;
            // read_data_ivl
            _keConfig.readDataIvl = KECONF_READ_DATA_IVL;
            // images_enable
            _keConfig.imagesEnable = KECONF_IMAGES_ENABLE;
            // ble_tx_power
            _keConfig.bleTxPower = KECONF_BLE_TX_POWER;
            // check_value
            _keConfig.checkValue = KECONF_CHECK_VALUE;
            // debug_print_ram
            _keConfig.debugPrintRam = KECONF_DEBUG_PRINT_RAM;

        }

    }


    [StructLayout(LayoutKind.Sequential)]
    public struct KeConfig               /* keconf_t */
    {
        public byte checkValue;                /* check_value */
        public byte dummyChk1;                  /* dummy_chk1 */
        public byte dummyChk2;                  /* dummy_chk2 */
        public byte dummyChk3;                  /* dummy_chk3 */

        /* keconf_adv_slow_t */
        public byte advSlowEnable;              /* enable */
        public byte advSlowDummy;               /* dummy */
        public UInt16 advSlowInterval;          /* interval */
        public UInt32 advSlowDuration;          /* duration */

        /* ble_gap_conn_params_t */
        public UInt16 connSlowMinConnInterval;  /* min_conn_interval */
        public UInt16 connSlowMaxConnInterval;  /* max_conn_interval */
        public UInt16 connSlowSlaveLatency;     /* slave_latency */
        public UInt16 connSlowConnSupTimeout;   /* conn_sup_timeout */

        public UInt32 autoDisconTimeFast;      /* auto_discon_time_fast */
        public UInt32 autoDisconTimeSlow;      /* auto_discon_time_slow */
        public UInt16 rssiIvl;                 /* rssi_ivl */
        public UInt16 readDataIvl;             /* read_data_ivl */
        public UInt32 imagesEnable;            /* images_enable */
        public sbyte bleTxPower;               /* ble_tx_power */
        public byte debugPrintRam;             /* debug_print_ram */

        public byte dummy1;                     /* dummy1 */
        public byte dummy2;                     /* dummy2 */

    }

    public static class KeConfigDef
    {
        public const int DATA_ADDR = 0x00;


    }

}
