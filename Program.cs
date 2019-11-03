using System;
using System.Collections.Generic;
using System.Linq;

namespace RPS.Test
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                // Generate a config object for the RPS.Server connection.
                RPS.Tools.Config config = new RPS.Tools.Config
                {
                    Host = "localhost", // Hostname or IP-Address to the RPS.Server (not the database!)
                    Port = 1896,
                    AuthKey = "1234567890",
                };

                // Initialize the RPS.API with the config.
                RPS.API.Initialize(config);

                // Get the property we want to connect to (readonly). We just select the first available.
                RPS.Data.Property property = RPS.Data.Property.ForAuthKey(config.AuthKey).FirstOrDefault();

                // It is also possible to select a property by its Id if we already know it:
                // var property = RPS.Data.Property.ForId(1);

                // Try to login to the API with user credentials.
                bool login = RPS.API.Login(property, "demo", "");

                // Try to login to the API with a session guid.
                //bool login = RPS.API.Login("a97b76d0cdec4dd491251d6942b4e4a86b90aa769626453ab66d0a2d7d201d5a");

                // If the login was successful, we can for example fetch the appointments from today and do stuff with them.
                if (login)
                {
                    List<RPS.Data.Appointment> appointments = RPS.Data.Appointment.ForDate(DateTime.Today);

                    // Do some stuff here:
                    // -->
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                // Finally logout from the API.
                // This is *highly* recommended because the session will be instantly available for another user.
                if (RPS.API.User != null)
                {
                    RPS.API.Logout();
                }
            }
        }
    }
}
