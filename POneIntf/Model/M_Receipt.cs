using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for M_Receipt_
/// </summary>
public class M_Receipt
{
	public M_Receipt()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public const string TableName = "T_Payment";

    #region Model
    private string _post_status;
    private string _receipt_date;
    private string _receipt_number;
    private string _payment_method;
    private string _cheque_number;
    private string _receipt_type;
    private string _customer_number;
    private string _site_number;
    private string _gl_date;
    private string _posting_date;
    private string _bank_in_date;
    private string _currency;
    private double _rate;
    private double _amount;
    private string _description;
    private string _receiving_company_code;
    private string _property;
    private string _application_status;
    private string _receipt_status;
    private double _print;
    private string _print_date;
    private string _bank_account;
    private string _created_by;
    private string _creation_date;
    private string _last_updated_by;
    private string _last_updated_date;
    private string _billing_company_code;
    private string _approval_status;
    private string _active;
    private string _void_date;
    private string _pdc_receipt_date;
    private string _identified_date;
    private string _ind_void_receipt_on_statement;
    private string _receipt_source;
    private string _payer;
    private double _overplus_amount;
    private string _original_receipt_number;
    private string _lease_number;
    /// <summary>
    /// 
    /// </summary>
    public string POST_STATUS
    {
        set { _post_status = value; }
        get { return _post_status; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string RECEIPT_DATE
    {
        set { _receipt_date = value; }
        get { return _receipt_date; }
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
    public string PAYMENT_METHOD
    {
        set { _payment_method = value; }
        get { return _payment_method; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string CHEQUE_NUMBER
    {
        set { _cheque_number = value; }
        get { return _cheque_number; }
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
    public string CUSTOMER_NUMBER
    {
        set { _customer_number = value; }
        get { return _customer_number; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string SITE_NUMBER
    {
        set { _site_number = value; }
        get { return _site_number; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string GL_DATE
    {
        set { _gl_date = value; }
        get { return _gl_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string POSTING_DATE
    {
        set { _posting_date = value; }
        get { return _posting_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string BANK_IN_DATE
    {
        set { _bank_in_date = value; }
        get { return _bank_in_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string CURRENCY
    {
        set { _currency = value; }
        get { return _currency; }
    }
    /// <summary>
    /// 
    /// </summary>
    public double RATE
    {
        set { _rate = value; }
        get { return _rate; }
    }
    /// <summary>
    /// 
    /// </summary>
    public double AMOUNT
    {
        set { _amount = value; }
        get { return _amount; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string DESCRIPTION
    {
        set { _description = value; }
        get { return _description; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string RECEIVING_COMPANY_CODE
    {
        set { _receiving_company_code = value; }
        get { return _receiving_company_code; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string PROPERTY
    {
        set { _property = value; }
        get { return _property; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string APPLICATION_STATUS
    {
        set { _application_status = value; }
        get { return _application_status; }
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
    public double PRINT
    {
        set { _print = value; }
        get { return _print; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string PRINT_DATE
    {
        set { _print_date = value; }
        get { return _print_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string BANK_ACCOUNT
    {
        set { _bank_account = value; }
        get { return _bank_account; }
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
    public string BILLING_COMPANY_CODE
    {
        set { _billing_company_code = value; }
        get { return _billing_company_code; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string APPROVAL_STATUS
    {
        set { _approval_status = value; }
        get { return _approval_status; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ACTIVE
    {
        set { _active = value; }
        get { return _active; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string VOID_DATE
    {
        set { _void_date = value; }
        get { return _void_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string PDC_RECEIPT_DATE
    {
        set { _pdc_receipt_date = value; }
        get { return _pdc_receipt_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string IDENTIFIED_DATE
    {
        set { _identified_date = value; }
        get { return _identified_date; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string IND_VOID_RECEIPT_ON_STATEMENT
    {
        set { _ind_void_receipt_on_statement = value; }
        get { return _ind_void_receipt_on_statement; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string RECEIPT_SOURCE
    {
        set { _receipt_source = value; }
        get { return _receipt_source; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string PAYER
    {
        set { _payer = value; }
        get { return _payer; }
    }
    /// <summary>
    /// 
    /// </summary>
    public double OVERPLUS_AMOUNT
    {
        set { _overplus_amount = value; }
        get { return _overplus_amount; }
    }
    /// <summary>
    /// 
    /// </summary>
    public string ORIGINAL_RECEIPT_NUMBER
    {
        set { _original_receipt_number = value; }
        get { return _original_receipt_number; }
    }
    public string LEASE_NUMBER
    {
        set { this._lease_number = value; }
        get { return this._lease_number; }
    }
    #endregion Model
}