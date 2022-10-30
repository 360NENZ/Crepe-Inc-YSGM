using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Proto;
using System.Text.Json;
using System.Xml.Linq;

namespace YSGM.Handlers
{
    public class PullCommand : BaseCommand
    {
        public string Execute(string[] args)
        {
            // smh mihoyo...
            int UID = int.Parse(args[0]);
            var c = UID.ToString().Last();

            // Pull user data
            var user = SQLManager.Instance.Execute("hk4e_db_user_32live", $"SELECT * FROM t_player_data_{c} WHERE uid = '{UID}'");

            var binDataStr = user.SelectSingleNode("row/field[@name='bin_data']")?.InnerText;
            var binData = FromHex(binDataStr?.Remove(0, 10) ?? ""); // Remove 0xZLIB
            byte[] decompressed = new byte[999999];
            var inf = new InflaterInputStream(new MemoryStream(binData));

            int bytesRead = 0;
            while (bytesRead < binData.Length)
            {
                int readBytes = inf.Read(decompressed, bytesRead, 999999 - bytesRead);
                if (readBytes == 0)
                {
                    break;
                }
                bytesRead += readBytes;
            }

            var trimHex = BitConverter.ToString(decompressed).Replace("-", "").TrimEnd('0'); // God forgive me for this abomination

            var parsedBin = parseProtoBin(trimHex);

            // Create folder and save
            var path = Path.GetFullPath($"./data_{UID}");
            Directory.CreateDirectory(path);
            File.WriteAllText($"{path}/user.xml", FormatXml(user.InnerXml));
            File.WriteAllText($"{path}/bin_data.json", Prettify(parsedBin?.ToString() ?? ""));

            return $"Saved to {path}";
        }

        private PlayerDataBin? parseProtoBin(string dataHex, int trailingZeroes = 0)
        {
            try
            {
                for (int i = 0; i < trailingZeroes; i++)
                {
                    dataHex += "0";
                }
                return PlayerDataBin.Parser.ParseFrom(FromHex(dataHex));
            } catch
            {
                return parseProtoBin(dataHex, trailingZeroes + 1);
            }
        }

        private string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }
        
        private byte[] FromHex(string hex)
        {
            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        private string Prettify(string unPrettyJson)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(unPrettyJson);

            return JsonSerializer.Serialize(jsonElement, options);
        }
    }
}
