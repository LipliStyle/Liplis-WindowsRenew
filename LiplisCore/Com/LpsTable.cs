//=======================================================================
//  ClassName : LpsTable
//  概要      : テーブルクラス
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Collections;
using System.Collections.Generic;

namespace Liplis.Com
{
    public class LpsTable<VALUE>
    {
        public Dictionary<string, VALUE> table;     //key / url 順序はAPI応答の順序IDと一致する。 順序は下のキーリストで引く
        public List<string> indexTable;                  //key / index

        /// <summary>
        /// コンストラクター
        /// </summary>
        public LpsTable()
        {
            this.table = new Dictionary<string, VALUE>();
            this.indexTable = new List<string>();
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public VALUE this[string key]
        {
            get
            {
                return table[key];
            }
            set
            {
                table[key] = value;
            }
        }

        /// <summary>
        /// インデクサ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public VALUE this[int index]
        {
            get
            {
                return table[indexTable[index]];
            }
            set
            {
                table[indexTable[index]] = value;
            }
        }

        //============================================================
        //
        //要素の追加削除
        //
        //============================================================

        /// <summary>
        /// 要素を追加する
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, VALUE value)
        {
            if(!table.ContainsKey(key))
            {
                this.table.Add(key, value);
                this.indexTable.Add(key);
            }
        }

        /// <summary>
        /// 要素をキー指定で削除する
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if(table.ContainsKey(key))
            {
                table.Remove(key);
            }

            if(indexTable.Contains(key))
            {
                indexTable.Remove(key);
            }
        }

        /// <summary>
        /// 要素をインデックス指定で削除する
        /// </summary>
        /// <param name="removeIdx"></param>
        public void RemoveAt(int removeIdx)
        {
            if(indexTable.Count -1 <= removeIdx)
            {
                string targetKey = indexTable[removeIdx];

                table.Remove(targetKey);
                this.indexTable.RemoveAt(removeIdx);
            }
        }

        /// <summary>
        /// テーブルをクリアする
        /// </summary>
        public void Clear()
        {
            this.table.Clear();
            this.indexTable.Clear();
        }


        //============================================================
        //
        //要素の取得
        //
        //============================================================

        /// <summary>
        /// キー指定で要素を取得する
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public VALUE GetByKey(string key)
        {
            if (table.ContainsKey(key))
            {
                return table[key];
            }
            else
            {
                return default(VALUE);
            }
        }

        /// <summary>
        /// インデックス指定で要素を取得する
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public VALUE GetByIndex(int index)
        {
            if (indexTable.Count - 1 <= index)
            {
                return table[indexTable[index]];
            }
            else
            {
                return default(VALUE);
            }
        }
    }
}
