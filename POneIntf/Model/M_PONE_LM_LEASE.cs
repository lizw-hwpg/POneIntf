using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for M_PONE_LM_LEASE
/// </summary>
public class M_PONE_LM_LEASE
{
    public const string Table_Name = "PONE_LM_LEASE";

    public string Lease_Number { get; set; }
    public int Lease_Version { get; set; }
    public string CUSTOMER_NUMBER { get; set; }
}

/*
 *
LEASE_NUMBER	N	CHAR(30)	N			
LEASE_VERSION	N	NUMBER(38)	N			
CUSTOMER_NUMBER	N	VARCHAR2(30)	Y			
SITE_NUMBER	N	CHAR(10)	N			
LEASE_TYPE	N	CHAR(1)	Y			
LEASE_STATUS	N	CHAR(1)	N			
PROPERTY_CODE	N	CHAR(15)	N			
PREMISE_NAME1	N	VARCHAR2(150)	N			
TENANT_TRADE_NAME1	N	VARCHAR2(150)	N			
LEASE_COMMENCE_DATE	N	DATE	N			
LEASE_TERM_FROM	N	DATE	N			
LEASE_TERM_TO	N	DATE	Y			
LOO_ISSUE_DATE	N	DATE	Y			
TRADE_TYPE	N	VARCHAR2(150)	N			
TOTAL_RENTAL_AREA	N	NUMBER(11,2)	N			
CREATION_DATE	N	DATE	N			
APPROVAL_DATE	N	DATE	Y			
BILLING_COMPANY_CODE	N	VARCHAR2(60)	Y			

 */