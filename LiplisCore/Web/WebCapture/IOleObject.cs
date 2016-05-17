//=======================================================================
//  ClassName : IOleClientSite
//  概要      : webキャプチャーに必要なインターフェース
//
//  Liplis5.0
//
//  Copyright(c) 2010-2016 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Liplis.Web.WebCapture
{
    // MEMO: エクステント・サイズを取得/設定するために使用。
    [Guid("00000112-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), ComVisible(false), ComImport]
    public interface IOleObject
    {
        void SetClientSite(IOleClientSite pClientSite);
        void GetClientSite(IOleClientSite ppClientSite);
        void SetHostNames(object szContainerApp, object szContainerObj);
        void Close(uint dwSaveOption);
        void SetMoniker(uint dwWhichMoniker, object pmk);
        void GetMoniker(uint dwAssign, uint dwWhichMoniker, object ppmk);
        void InitFromData(IDataObject pDataObject, bool fCreation, uint dwReserved);
        void GetClipboardData(uint dwReserved, IDataObject ppDataObject);
        void DoVerb(uint iVerb, uint lpmsg, object pActiveSite, uint lindex, uint hwndParent, uint lprcPosRect);
        void EnumVerbs(object ppEnumOleVerb);
        void Update();
        void IsUpToDate();
        void GetUserClassID(uint pClsid);
        void GetUserType(uint dwFormOfType, uint pszUserType);
        void SetExtent(DVASPECT dwDrawAspect, ref Size psizel);
        void GetExtent(DVASPECT dwDrawAspect, out Size psizel);
        void Advise(object pAdvSink, uint pdwConnection);
        void Unadvise(uint dwConnection);
        void EnumAdvise(object ppenumAdvise);
        void GetMiscStatus(uint dwAspect, uint pdwStatus);
        void SetColorScheme(object pLogpal);
    };
}

