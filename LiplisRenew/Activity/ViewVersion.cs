
//=======================================================================
//  ClassName : ActivityVersion
//  概要      : バージョンアクティヴィティ
//
//  Liplis2.3
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using Liplis.Utl;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;


namespace Liplis.Activity
{
    public partial class ViewVersion : Form
    {
        ///====================================================================
        ///
        ///                       onCreate
        ///                         
        ///====================================================================

        //コンストラクター
        #region ViewVersion
        public ViewVersion()
            : base()
        {
            InitializeComponent();
            this.lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        #endregion

        ///====================================================================
        ///
        ///                       onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// btnClose_Click
        /// </summary>
        #region btnClose_Click
        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// バージョンチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region btnVersion_Click
        private void btnVersion_Click(object sender, System.EventArgs e)
        {
            runUpdate();
        }
        #endregion




        ///====================================================================
        ///
        ///                              onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// onDelete
        /// </summary>
        #region Dispose
        public void dispose()
        {
            this.Close();
        }
        #endregion

        /// <summary>
        /// onDelete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region ActivityRss_FormClosing
        private void ActivitySetting_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
        #endregion

        /// <summary>
        /// アップデートプログラム起動
        /// </summary>
        #region runUpdate
        private void runUpdate()
        {
            if (LpsPathController.checkFileExist(LpsPathController.getUpdatePrgPath()))
            {
                Process p = Process.Start(LpsPathController.getUpdatePrgPath());
            }

        }
        #endregion

    }
}
