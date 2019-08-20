using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FlightControl.Models;
using Newtonsoft.Json;

namespace FlightControl.Utility
{
    class FileOps
    {
        private static NLog.Logger logger = NLog.LogManager.GetLogger("File Ops");

        public static async Task<List<ControlSet>> GetControlSets()
        {
            return await Task.Run(() =>
            {

                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"FlightControl\profile.json");

                if (File.Exists(path))
                {
                    try
                    {
                        var val = File.ReadAllText(path);
                        return JsonConvert.DeserializeObject<List<ControlSet>>(val);
                    }
                    catch (Exception e)
                    {
                        logger.Error($"Exception loading profile: {e.Message}");
                    }
                }

                return new List<ControlSet>();
            }).ConfigureAwait(false);
        }

        public static async Task<bool> SaveControlSets(List<ControlSet> controlSets)
        {
            return await Task.Run(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    @"FlightControl");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    var val = JsonConvert.SerializeObject(controlSets);

                    File.WriteAllText(Path.Combine(path, "profile.json"), val);
                }
                catch (Exception e)
                {
                    logger.Error($"Exception saving profile: {e.Message}");
                    return false;
                }

                return true;
            }).ConfigureAwait(false);
        }
    }
}
