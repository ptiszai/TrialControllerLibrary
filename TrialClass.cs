using System;
using Microsoft.Win32;
using System.Linq;
using System.Diagnostics;

//https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.registry?view=net-7.0
//https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.registrykey.deletesubkey?view=net-7.0

//HKEY_CURRENT_USER/SOFTWARE/<name>
/*
 * ds = int // start date
 * de = int // end   date
 * tn = int // trial number of days
*/
namespace TrialControllerLibrary
{
    public class TrialClass
    {
        #region private variables
        private const string userRoot = "HKEY_CURRENT_USER\\SOFTWARE";
        private string subkey = "";
        private string keyName = "";
        private const string ds = "ds"; // start date
        private const string de = "de"; // end date
        private const string tn = "tn"; // trial number of days
        private RegistryKey regName;
        #endregion

        #region public functions
        public TrialClass(String name_a) {            
            subkey = name_a;
            keyName = userRoot + "\\" + subkey;
        }

        public bool Create(int trialDays, bool currentDate = true, int year = 0, int months = 0, int days = 0, int hours = 0)
        {
            try {
                if (trialDays <= 0) {
                    return true;
                }
                if (Registry.GetValue(keyName, ds, null) == null)
                { // not exist
                    if (subkey.Count() > 0)
                    {
                        regName = Registry.CurrentUser.CreateSubKey($"SOFTWARE\\{subkey}");
                        regName.CreateSubKey(ds);
                        regName.CreateSubKey(de);
                        regName.CreateSubKey(tn);
                    }
                    SetDsDeTn(trialDays, currentDate, year, months, days, hours);
                }
            }
            catch (Exception ex) {
                Trace.WriteLine($"Create: {ex.Message}");
                return false;
            }
            return true;
        }

        public void SetDsDeTn(int trialDays, bool currentDate = true, int year = 0, int months = 0, int days = 0, int hours = 0)
        {
            if (Registry.GetValue(keyName, ds, null) == null)
            {
                int startdt = 0;
                int enddt = 0;
                if (currentDate)
                {
                    startdt = GetHoursFromUnix1970();
                }
                else
                {
                    startdt = GetHoursFromUnix1970(year, months, days, hours);
                }
                Registry.SetValue(keyName, ds, startdt);
                enddt = startdt + trialDays;
                Registry.SetValue(keyName, de, enddt);
                Registry.SetValue(keyName, tn, trialDays);               
            }
        }

        public void Delete()
        {
            try
            {
                if (subkey.Count() > 0)
                {
                    Registry.CurrentUser.DeleteSubKeyTree($"SOFTWARE\\{subkey}");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"Delete: {ex.Message}");
            }
        }

        public int IsTrial()
        {
            if (Registry.GetValue(keyName, ds, null) == null)
            {
                return -1;
            }
            int currentdt = GetHoursFromUnix1970();
            int _ds_value = (int)Registry.GetValue(keyName, ds, null);        
            int _de_value = (int)Registry.GetValue(keyName, de, null);            
            return (_de_value - currentdt);
        }
        #endregion

        #region privet functions
        private int GetHoursFromUnix1970(int year, int months, int days, int hours)
        {
            DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime _date = new DateTime(year, months, days, hours, 0, 0, DateTimeKind.Utc);
            int unixTimestamp = (int)_date.Subtract(_unixEpoch).TotalDays;
            Console.WriteLine($"Current date Hours from 1970: {unixTimestamp}");
            return unixTimestamp;
        }
        private int GetHoursFromUnix1970()
        {
            DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime _date = DateTime.UtcNow;
            int unixTimestamp = (int)_date.Subtract(_unixEpoch).TotalDays;
            Console.WriteLine($"Current date Hours from 1970: {unixTimestamp}");
            return unixTimestamp;
        }
        #endregion
    }
}
