//=======================================================================
//  ClassName : IOleClientSite
//  概要      : webキャプチャーに必要なインターフェース
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Runtime.InteropServices;

namespace Liplis.Web.WebCapture
{
    // MEMO: このインタフェースは未使用。IOleObjectインタフェースの定義（再定義？）に必要。
    [Guid("00000118-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComVisible(false), ComImport]
    public interface IOleClientSite
    {
        void SaveObject();
        void GetMoniker(uint dwAssign, uint dwWhichMoniker, object ppmk);
        void GetContainer(object ppContainer);
        void ShowObject();
        void OnShowWindow(bool fShow);
        void RequestNewObjectLayout();
    };
}
