using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AssetCategorization
/// </summary>
public class AssetCategorization
{
	public AssetCategorization()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _parentAssetCode;

    public string ParentAssetCode
    {
        get { return _parentAssetCode; }
        set { _parentAssetCode = value; }
    }
    private string _assetName;

    public string AssetName
    {
        get { return _assetName; }
        set { _assetName = value; }
    }
    public string CheckSub { set; get; }
    public string CheckItemCreate { set; get; }
    public string Itm_Det_stk_unit { set; get; }
    public string Itm_Det_Acc_code { set; get; }
    public int ItemTypeId { set; get; }
    public string Itm_Det_Others1_flag { set; get; }
    public string FaAcc { get; set; }
    public string DepAcc { get; set; }
    public string AcmDepAcc { get; set; }
    public string DispAcc { get; set; }
    public string RevAcc { get; set; }
    public string ModelNumber { get; set; }
    public float LifeCycle { get; set; }
    public string CogsAcc { get; set; }
    public string ExpenseAcc { get; set; }
}