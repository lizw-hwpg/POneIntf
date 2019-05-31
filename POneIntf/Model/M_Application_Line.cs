using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for M_Application_Line
/// </summary>
public class M_Application_Line
{
	public M_Application_Line()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Model
    private string _application_line_number;
    private string _receipt_number;
    private string _receipt_type;
    private string _invoice_number;
    private string _invoice_line_number;
    private string _charge_code;
    private string _amount;
    private string _remarks;
    private string _from_receipt_number;
    private string _from_receipt_type;
    private string _created_by;
    private string _creation_date;
    private string _last_updated_by;
    private string _last_updated_date;
    private string _receipt_status;
    private string _from_receipt_status;
    /// <summary>
    /// 
    /// </summary>
    public string APPLICATION_LINE_NUMBER
    {
        set { _application_line_number = value; }
        get { return _application_line_number; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string RECEIPT_NUMBER
    {
        set { _receipt_number = value; }
        get { return _receipt_number; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string RECEIPT_TYPE
    {
        set { _receipt_type = value; }
        get { return _receipt_type; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string INVOICE_NUMBER
    {
        set { _invoice_number = value; }
        get { return _invoice_number; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string INVOICE_LINE_NUMBER
    {
        set { _invoice_line_number = value; }
        get { return _invoice_line_number; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string CHARGE_CODE
    {
        set { _charge_code = value; }
        get { return _charge_code; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string AMOUNT
    {
        set { _amount = value; }
        get { return _amount; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string REMARKS
    {
        set { _remarks = value; }
        get { return _remarks; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string FROM_RECEIPT_NUMBER
    {
        set { _from_receipt_number = value; }
        get { return _from_receipt_number; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string FROM_RECEIPT_TYPE
    {
        set { _from_receipt_type = value; }
        get { return _from_receipt_type; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string CREATED_BY
    {
        set { _created_by = value; }
        get { return _created_by; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string CREATION_DATE
    {
        set { _creation_date = value; }
        get { return _creation_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string LAST_UPDATED_BY
    {
        set { _last_updated_by = value; }
        get { return _last_updated_by; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string LAST_UPDATED_DATE
    {
        set { _last_updated_date = value; }
        get { return _last_updated_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string RECEIPT_STATUS
    {
        set { _receipt_status = value; }
        get { return _receipt_status; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string FROM_RECEIPT_STATUS
    {
        set { _from_receipt_status = value; }
        get { return _from_receipt_status; }
    }
    #endregion Model
}