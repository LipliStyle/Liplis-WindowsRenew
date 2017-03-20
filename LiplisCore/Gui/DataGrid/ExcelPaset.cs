//=======================================================================
//  ClassName : ExcelPaset
//  概要      : データグリッドにエクセルを貼り付ける
//
//  Liplis2.0
//  Copyright(c) 2010-2016 LipliStyle. All Rights Reserved. 
//=======================================================================

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Liplis.Gui.DataGrid
{
    public class ExcelPaset
    {
        /// <summary>
        /// クリップボードの中身をデータグリッドに貼り付ける
        /// </summary>
        /// <param name="dataGrid"></param>
        public static void pasteClipboard(DataGridView dataGrid)
        {
            try
            {
                //nullチェック
                if(dataGrid == null)
                {
                    return;
                }


                // 張り付け開始位置設定
                int startRowIndex = getSelectedRowIndex(dataGrid);
                int startColIndex = getSelectedColIndex(dataGrid);

                //補正
                if (startRowIndex < 0) { startRowIndex = 0; }
                if (startColIndex < 0) { startColIndex = 0; }


                // クリップボード文字列から行を取得
                List<string> pasteRows = new List<string>(((string)Clipboard.GetData(DataFormats.Text)).Replace("\r", "")
                    .Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

                int maxRowCount = pasteRows.Count;
                for (int rowCount = 0; rowCount < maxRowCount; rowCount++)
                {
                    int rowIndex = startRowIndex + rowCount;

                    // タブ区切りでセル値を取得
                    List<string> pasteCells = new List<string>(pasteRows[rowCount].Split('\t'));

                    // 選択位置から列数繰り返す
                    int maxColCount = Math.Min(pasteCells.Count, dataGrid.Columns.Count - startColIndex);
                    for (int colCount = 0; colCount < maxColCount; colCount++)
                    {
                        DataGridViewCell cell = dataGrid[colCount + startColIndex, rowIndex];

                        // 貼り付け
                        try
                        {
                            cell.Value = pasteCells[colCount];
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// ローインデックスを取得する
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static int getSelectedRowIndex(DataGridView dataGrid)
        {
            try
            {
                return dataGrid.SelectedRows[0].Index;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// コルインデックスを取得する
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <returns></returns>
        public static int getSelectedColIndex(DataGridView dataGrid)
        {
            try
            {
                return dataGrid.SelectedColumns[0].Index;
            }
            catch
            {
                return 0;
            }
        }
    }
}
