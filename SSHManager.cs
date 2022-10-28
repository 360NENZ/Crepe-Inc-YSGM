using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YSGM
{
    public class SSHManager
    {
        public SshClient client;
        public static SSHManager Instance = new();

        private SSHManager()
        {
#if DEBUG
            string? host = "hk4e-storage.mihoyo.com";
            string? user = "root";
            string? keyPath = @"C:\Users\Paris\.ssh\id_rsa";
#else
            string? host = ConfigurationManager.AppSettings.Get("SSH_HOST");
            string? user = ConfigurationManager.AppSettings.Get("SSH_USER");
            string? keyPath = ConfigurationManager.AppSettings.Get("SSH_KEY_PATH");
#endif
            // Read the private key file
            PrivateKeyFile keyFile = new(keyPath);
            var keyFiles = new[] { keyFile };
            PrivateKeyAuthenticationMethod authMethod = new(user, keyFiles);
            ConnectionInfo connectionInfo = new (host, user, authMethod);

            client = new SshClient(connectionInfo);
        }

        public void Init()
        {
            client.Connect();
        }
    }
}
