using Liplis.Properties;
using System.Drawing;
using System.Resources;


namespace Liplis.Com
{
    public class LpsResourceCreator
    {
        /// <summary>
        /// getResourceBitmap
        /// リソースからビットマップを取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        #region getResourceBitmap
        public static Bitmap getResourceBitmap(string resourceName)
        {
            ResourceManager rm = new ResourceManager("Liplis.Properties.Resources", typeof(Resources).Assembly);
            return (Bitmap)rm.GetObject(resourceName);
        }
        #endregion

        /// <summary>
        /// getResourceXml
        /// リソースからXmlを取得する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        #region getResourceXml
        public static string getResourceXml(string resourceName)
        {
            ResourceManager rm = new ResourceManager("Liplis.Properties.Resources", typeof(Resources).Assembly);
            return (string)rm.GetObject(resourceName);
        }
        #endregion       

    }
}
