USE [UtilityServices]
GO
/****** Object:  Index [IX_SourceData_ProcessingStatus]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[BudgetMeter].[SourceData]') AND name = N'IX_SourceData_ProcessingStatus')
DROP INDEX [IX_SourceData_ProcessingStatus] ON [BudgetMeter].[SourceData]
GO
/****** Object:  View [BudgetMeter].[vw_SourceData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_SourceData]'))
DROP VIEW [BudgetMeter].[vw_SourceData]
GO
/****** Object:  View [BudgetMeter].[vw_InitialSourceData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_InitialSourceData]'))
DROP VIEW [BudgetMeter].[vw_InitialSourceData]
GO
/****** Object:  View [BudgetMeter].[vw_ErrorDataDetailsExport]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_ErrorDataDetailsExport]'))
DROP VIEW [BudgetMeter].[vw_ErrorDataDetailsExport]
GO
/****** Object:  View [BudgetMeter].[vw_ErrorDataDetails]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_ErrorDataDetails]'))
DROP VIEW [BudgetMeter].[vw_ErrorDataDetails]
GO
/****** Object:  View [BudgetMeter].[vw_ErrorData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_ErrorData]'))
DROP VIEW [BudgetMeter].[vw_ErrorData]
GO
/****** Object:  View [BudgetMeter].[vw_CODAData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_CODAData]'))
DROP VIEW [BudgetMeter].[vw_CODAData]
GO
/****** Object:  View [BudgetMeter].[vw_AllSourceData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_AllSourceData]'))
DROP VIEW [BudgetMeter].[vw_AllSourceData]
GO
/****** Object:  Table [BudgetMeter].[TalexusHeader]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[TalexusHeader]') AND type in (N'U'))
DROP TABLE [BudgetMeter].[TalexusHeader]
GO
/****** Object:  Table [BudgetMeter].[TalexusBody]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[TalexusBody]') AND type in (N'U'))
DROP TABLE [BudgetMeter].[TalexusBody]
GO
/****** Object:  Table [BudgetMeter].[SSISConfigurations]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[SSISConfigurations]') AND type in (N'U'))
DROP TABLE [BudgetMeter].[SSISConfigurations]
GO
/****** Object:  Table [BudgetMeter].[SourceData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[SourceData]') AND type in (N'U'))
DROP TABLE [BudgetMeter].[SourceData]
GO
/****** Object:  Table [BudgetMeter].[GridOwner]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[GridOwner]') AND type in (N'U'))
DROP TABLE [BudgetMeter].[GridOwner]
GO
/****** Object:  Table [BudgetMeter].[CODAData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[CODAData]') AND type in (N'U'))
DROP TABLE [BudgetMeter].[CODAData]
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_UpdateSourceDataErrors]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_UpdateSourceDataErrors]') AND type in (N'P', N'PC'))
DROP PROCEDURE [BudgetMeter].[USP_UpdateSourceDataErrors]
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_UpdateSourceData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_UpdateSourceData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [BudgetMeter].[USP_UpdateSourceData]
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_CheckIndexes]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_CheckIndexes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [BudgetMeter].[USP_CheckIndexes]
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_BuildSourceData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_BuildSourceData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [BudgetMeter].[USP_BuildSourceData]
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_BuildCodaData]    Script Date: 12/04/2013 7:32:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_BuildCodaData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [BudgetMeter].[USP_BuildCodaData]
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_BuildCodaData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_BuildCodaData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [BudgetMeter].[USP_BuildCodaData]
as

      ------------------------------------------------
      -- GET CODA DATA FOR TALEXUS FILES TO PROCESS --
      ------------------------------------------------
      INSERT INTO BudgetMeter.CODAData
                  (CODARecId,TalexusReference, BankAccountID,PaymentDate,Amount)
      SELECT      RECID             as CODARecId
            ,     MESSAGE                 as TalexusReference
            ,     ACCOUNTID         as BankAccountID
            ,     STATEMENTDATE     as PaymentDate
            ,     SUM(AmountMST)    as Amount
      FROM  dbo.BANKCODAACCOUNTSTATEMENTLINES lin
      WHERE ACCOUNTID = ''FORTIS''
            AND   EXISTS (   SELECT      1
                                    FROM  BudgetMeter.SourceData dt
                                    WHERE dt.CODAReference = lin.MESSAGE collate database_default
                              )
            AND NOT EXISTS (  SELECT      1
                                          FROM  BudgetMeter.CODAData dt
                                          WHERE dt.CODARecId = lin.RECID
                                    )
      GROUP BY RECID, MESSAGE, ACCOUNTID, STATEMENTDATE;
      
      ------------------------------
      -- FINANCIAL RECONCILIATION --
      ------------------------------
      WITH CTE_Source as
      (
            SELECT      src.CODAReference as Reference
                  ,     SUM(src.Amount)         as TalexusAmount
                  ,     SUM(dt.Amount)          as CODAAmount
            FROM  BudgetMeter.SourceData src
            INNER JOIN BudgetMeter.CODAData dt
            ON dt.TalexusReference = src.CodaReference
            WHERE ((src.ProcessingStatus = ''To Process'' OR src.ProcessingStatus = ''Error'') AND src.CODAReconciliation = 0)
            GROUP BY src.CODAReference
      )
      UPDATE dte
            SET CODAReconciliation =      CASE 
                                                            WHEN  src.TalexusAmount <= src.CODAAmount THEN 1
                                                            ELSE  0
                                                      END 
                  ,     CODAExists  = 1
      FROM BudgetMeter.SourceData dte
      INNER JOIN CTE_Source src
      On src.Reference = dte.CODAReference;

      ---------------------------
      -- CHANGE STATUS TO OPEN --
      ---------------------------
      UPDATE src
            SET         src.ProcessingStatus = ''Open''
      FROM  BudgetMeter.SourceData src
      WHERE src.CODAReconciliation  = 1
            AND src.ConsumptionExists     = 1
            AND   src.ContractNo                IS NOT NULL
            AND src.ProcessingStatus    IN (''To Process'',''Error'');

      --------------------------------
      -- CONSUMPTION DOESN''T EXISTS --
      --------------------------------
      UPDATE src
            SET src.ErrorMessage += ''- NO CONSUMPTION FOUND! ''
                  ,     src.ProcessingStatus = ''Error''
      FROM BudgetMeter.SourceData src
      WHERE src.ConsumptionExists = 0
            AND src.ErrorMessage NOT LIKE ''%CONSUMPTION%'';

      -----------------------
      -- CODA MATCH ERRORS --
      -----------------------
      UPDATE      src
            SET         src.ErrorMessage  += ''- NO MATCH WITH CODA AMOUNT! ''
                  ,     src.ProcessingStatus = ''Error''      
      FROM  BudgetMeter.SourceData src
      WHERE src.CODAExists = 1 
            AND   src.CODAReconciliation = 0
            AND src.ErrorMessage NOT LIKE ''%CODA AMOUNT%'';

      ----------------------
      -- CHECK CODA DELAY --
      ----------------------
      UPDATE src
            SET         src.ProcessingStatus = ''Error''
                  ,     src.ErrorMessage += ''- NO CODA FOUND FOR CHARGE DATE MORE THAN 40 DAYS IN PAST! ''
      FROM BudgetMeter.SourceData src
      WHERE src.CODAExists = 0
            AND src.ContractNo IS NOT NULL
            AND src.ChargeDate < DATEADD(d,-40,CONVERT(date,GETDATE()))
            AND src.ErrorMessage NOT LIKE ''%DATE MORE THAN 40 DAYS%'';
      
      -----------------------------------------------
      -- REMOVE ERRORMESSAGES FROM NON ERROR LINES --
      -----------------------------------------------
      UPDATE src
            SET src.ErrorMessage  = ''''
      FROM  BudgetMeter.SourceData src
      WHERE src.ProcessingStatus != ''Error'';



' 
END
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_BuildSourceData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_BuildSourceData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [BudgetMeter].[USP_BuildSourceData]
as
      ------------------------------------------------------------------------
      -- SET HEADERS PROCESSED WHEN NO DETAILS AVAILABLE ==> FILE WAS EMPTY --
      ------------------------------------------------------------------------
      UPDATE Budgetmeter.Talexusheader
            SET Processed = 1
      WHERE Processed IS NULL
            AND   [RowCount] = 0;

      --------------------------
      -- GET FILES TO PROCESS --
      --------------------------
      IF OBJECT_ID(''tempdb..#ToProcess'') IS NOT NULL
            DROP TABLE #ToProcess;
            
      SELECT      HeaderID
      INTO #ToProcess
      FROM  BudgetMeter.TalexusHeader
      WHERE Processed IS NULL;
      
      ----------------------
      -- MAKE SOURCE DATA --
      ----------------------
      INSERT INTO Budgetmeter.SourceData (ID,TalexusHeaderID,EAN,Amount,ChargeDate,GridOwnerID,TalexusReference,CodaReference,
                                                            ProcessingStatus,PrePaymentReference,StatementReference,ConsumptionExists,ContractNo, CodaExists,
                                                            ErrorMessage, AccountNo,Booking,MasterDataChecked,CODAReconciliation)
      SELECT      NEWID()
            ,     head.HeaderID
            ,     det.EAN
            ,     CASE SalesType
                        WHEN 1 THEN det.Amount *-1
                        ELSE det.amount
                  END
            ,     det.Generationdate
            ,     head.ID_GRD
            ,     det.TalexusClientRef
            ,     head.TalexusCodaRef
            ,     ''To Process''
            ,     ''''
            ,     ''''
            ,     0
            ,     (
                        SELECT      TOP 1 del.AGREEMENTID
                        FROM  dbo.IUS_AGR_DELIVERYTERMS del
                        INNER JOIN dbo.IUS_MPO_METERINGPOINTTABLE mpo
                              ON    mpo.METERINGPOINTID = del.DELIVERYPOINT
                        INNER JOIN dbo.IUS_AGR_AGREEMENT agr
                              ON    agr.AGREEMENTID = del.AGREEMENTID
                        WHERE det.GenerationDate BETWEEN del.STARTDATE AND del.ENDDATE
                              AND mpo.EANCODE = det.EAN collate database_default
                              AND agr.STATUSID NOT LIKE ''%CANCEL%''
                  )
            ,     0
            ,     ''''
            ,     ''''
            ,     0
            ,     0
            ,     0
      FROM  #ToProcess pro
      INNER JOIN BudgetMeter.TalexusHeader head
            ON pro.headerID = head.HeaderID
      INNER JOIN BudgetMeter.TalexusBody det
            ON det.HeaderID = head.headerID;    
      
      -------------------------------------     
      -- SET PROCESSED DATA TO PROCESSED --
      -------------------------------------
      UPDATE talexus
            SET talexus.Processed = 1
      FROM Budgetmeter.TalexusHeader talexus
      INNER JOIN #ToProcess ToPro
      ON ToPro.HeaderId = talexus.HeaderID;
      
      --------------------
      -- GET ACCOUNT NO --
      --------------------
      UPDATE src
            SET src.AccountNo = agr.CUSTACCOUNT
      FROM BudgetMeter.SourceData src
      INNER JOIN dbo.IUS_AGR_AGREEMENT agr
      ON agr.AGREEMENTID = src.contractNo collate database_Default
      WHERE src.AccountNo = ''''
            OR    src.AccountNo IS NULL;
                  
      ----------------------------------------
      -- CHECK CONSUMPTION MESSAGE RECEIVED --
      ----------------------------------------
      UPDATE src
            SET consumptionExists =       ISNULL((    
                                                                  SELECT      TOP 1 1
                                                                  FROM  dbo.IUS_QTY_PERIODQTY qty   
                                                                  WHERE qty.METERINGPOINTID = mpo.METERINGPOINTID
                                                                        AND   src.ChargeDate            BETWEEN qty.STARTDATE AND qty.ENDDATE
                                                                        AND qty.STATUS                = 1 -- To be Invoiced
                                                            ),0)                                                  
      FROM BudgetMeter.SourceData src
      INNER JOIN dbo.IUS_MPO_METERINGPOINTTABLE mpo
            ON mpo.EANCODE = src.EAN collate database_default
      WHERE src.ConsumptionExists IS NULL 
            OR    src.ConsumptionExists = 0;

      --------------------------
      -- CLEAR ERROR MESSAGES --
      --------------------------
      UPDATE src
            SET src.ErrorMessage = ''''
      FROM BudgetMeter.SourceData src
      WHERE ProcessingStatus = ''Error''

      --------------------------
      -- CHECK CONTRACT MATCH --
      --------------------------
      UPDATE src
            SET         src.errorMessage        = ''- NO CONTRACT FOUND! ''
                  ,     src.ProcessingStatus    = ''Error''
      FROM BudgetMeter.SourceData src
      WHERE ContractNo IS NULL 
            --AND ProcessingStatus != ''Error''
            AND MasterDataChecked != 1;

      ------------------------------------
      -- CHECK STATUS CAN BE TO INVOICE --
      ------------------------------------
      UPDATE      src
            SET ProcessingStatus = ''To Invoice''
      FROM  BudgetMeter.SourceData src
      WHERE src.ProcessingStatus    = ''Booking''
            AND   src.ConsumptionExists   = 1;

      -------------------------------------
      -- CHECK STATUS CAN BE NO DELIVERY --
      -------------------------------------
      UPDATE      src
            SET         src.ProcessingStatus    = ''No Delivery''
                  ,     src.ErrorMessage        = ''''
      FROM  BudgetMeter.SourceData src
      WHERE src.MasterDataChecked = 1
            AND src.ProcessingStatus = ''Error'';

            

/****** Object:  StoredProcedure [BudgetMeter].[USP_BuildCodaData]    Script Date: 04/11/2013 08:20:20 ******/
SET ANSI_NULLS ON
' 
END
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_CheckIndexes]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_CheckIndexes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [BudgetMeter].[USP_CheckIndexes]
as
BEGIN

      ------------------------------------------------
      -- CREATE INDEX BANKCODAACCOUNTSTATEMENTLINES --
      ------------------------------------------------
      IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N''[dbo].[BANKCODAACCOUNTSTATEMENTLINES]'') 
            AND name = ''IX_BankCodaAccountStatementlines_AccountId'')
      BEGIN 
            CREATE NONCLUSTERED INDEX IX_BankCodaAccountStatementlines_AccountId
            ON [dbo].[BANKCODAACCOUNTSTATEMENTLINES] ([ACCOUNTID])
            INCLUDE ([MESSAGE],[AMOUNTMST],[STATEMENTDATE],[RECID])     ;
      END;
      ------------------------------------
      -- CREATE INDEX IUS_QTY_PERIODQTY --
      ------------------------------------
      IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N''[dbo].[IUS_QTY_PERIODQTY]'') 
            AND name = ''IX_IusQtyPeriodQty_StatusMeteringPointIDStartDateEndDate'')
      BEGIN
            CREATE NONCLUSTERED INDEX IX_IusQtyPeriodQty_StatusMeteringPointIDStartDateEndDate
            ON [dbo].[IUS_QTY_PERIODQTY] ([STATUS],[METERINGPOINTID],[STARTDATE],[ENDDATE])
      END;

      -----------------------------------
      -- CREATE INDEX CUSTINVOICETABLE --
      -----------------------------------
      IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N''[dbo].[CUSTINVOICETABLE]'') 
            AND name = ''IX_Custinvoicetable_InvoiceaccountPostedInvoiceidIusinvbillingcategoryIusinvcreditsaledid'')
      BEGIN
            CREATE NONCLUSTERED INDEX IX_Custinvoicetable_InvoiceaccountPostedInvoiceidIusinvbillingcategoryIusinvcreditsaledid
            ON [dbo].[CUSTINVOICETABLE] ([INVOICEACCOUNT],[POSTED],[INVOICEID],[IUS_INV_BILLINGCATEGORY],[IUS_INV_CREDITEDSALESID])
      END;

      --------------------------------------------
      -- CREATE INDEX IUS_INV_INVOICELINEDETAIL --
      --------------------------------------------
      IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N''[dbo].[IUS_INV_INVOICELINEDETAIL]'') 
            AND name = ''IX_Iusinvinvoicelinedetail_salesidAgreementid'')
      BEGIN
            CREATE NONCLUSTERED INDEX IX_Iusinvinvoicelinedetail_salesidAgreementid
            ON [dbo].[IUS_INV_INVOICELINEDETAIL] ([SALESID],[AGREEMENTID]);
      END;
      IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N''[dbo].[IUS_INV_INVOICELINEDETAIL]'') 
            AND name = ''IX_Iusinvinvoicelinedetail_salesidPendinglinerefrecid'')
      BEGIN      
			CREATE NONCLUSTERED INDEX IX_Iusinvinvoicelinedetail_salesidPendinglinerefrecid
			ON [dbo].[IUS_INV_INVOICELINEDETAIL] ([SALESID],[PENDINGLINEREFRECID])
	  END;
END;
' 
END
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_UpdateSourceData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_UpdateSourceData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [BudgetMeter].[USP_UpdateSourceData]
	@AccountNo			varchar(20)	,
	@Type				int			, -- 1 = Prepayment 2 = Statement 3 = Booking
	@InvoiceNo			varchar(20)	,
	@ContractNo			varchar(20) ,
	@Booking			bit,
	@ErrorMessage		varchar(200) output
as
BEGIN
	DECLARE @SalesId			varchar(20);
	DECLARE @AmountToInvoice	decimal(20,2);
	DECLARE @StatementDate		date;
	--SET @type = 1;
	--SET @InvoiceNo = ''FAC120068094'';
	--SET @AccountNo = ''CU000001'';

	-------------
	-- BOOKING --
	-------------
	IF (@Type = 3)
	BEGIN
		
		IF (@Booking= 0)
		BEGIN
			SET @ErrorMessage = ''BOOKING NOT DONE FOR CONTRACT '''''' + @ContractNo + '''''' !'';
			RETURN 1;
		END;

		--------------------
		-- CHECK PREPAYED --
		--------------------
		IF EXISTS(	SELECT  top 1 1
					FROM	BudgetMeter.SourceData src
					WHERE	src.AccountNo = @AccountNo
						AND	src.ContractNo = @ContractNo
						AND src.ProcessingStatus = ''Booking''
					)
			BEGIN
				UPDATE	src
					SET		src.ProcessingStatus = ''To Invoice''
						,	src.booking = 1 
				FROM	budgetMeter.SourceData src
				WHERE	src.AccountNo = @AccountNo
					AND	src.ContractNo = @ContractNo
					AND	src.processingStatus = ''Booking'';

			END 
			ELSE
			BEGIN
				SET @ErrorMessage = ''BOOKING NOT ALLOWED FOR CONTRACT: '''''' + @ContractNo + '''''' !'';
				RETURN 1;
			END;

		SET @ErrorMessage = '''';
		RETURN 0;
	END;

	----------------------
	-- CHECK PREPAYMENT --
	----------------------
	IF (@Type = 1 )
	BEGIN
		----------------------------------
		-- CHECK PREPAYMENT CAN BE DONE --
		----------------------------------
		IF NOT EXISTS (	SELECT  top 1 1
						FROM	BudgetMeter.SourceData src
						WHERE	src.AccountNo			= @AccountNo
							AND	src.ContractNo			= @ContractNo
							AND	src.ProcessingStatus	= ''Open''
							AND	src.CODAExists			= 1
							AND src.CODAReconciliation	= 1
						)
		BEGIN
			SET  @ErrorMessage = ''PREPAYMENT CANNOT BE DONE FOR CONTRACT: '''''' + @ContractNo + '''''' BECAUSE THERE ARE NO LINES TO MAKE A PREPAYMENT FOR! '';
			RETURN 1;
		END;

		-----------------------------
		-- CHECK PREPAYMENT EXISTS --
		-----------------------------
		SELECT	@salesId = inv.IUS_INV_SALESID
		FROM	dbo.CUSTINVOICETABLE inv
		WHERE	inv.IUS_INV_BILLINGCATEGORY = 0 -- PREPAYMENT
			AND inv.POSTED					= 1
			AND inv.IUS_INV_CREDITEDSALESID = '''' -- NOT CREDITED
			AND	inv.INVOICEID				= @InvoiceNo
			AND	inv.INVOICEACCOUNT			= @AccountNo;

		IF (@SalesId IS NULL OR @SalesId = '''')
		BEGIN
			SET @ErrorMessage = ''PREPAYMENT: ''''''+ @InvoiceNo + '''''' DOES NOT EXIST!'';
			RETURN 1;
		END;
		
	END;

	---------------------
	-- CHECK STATEMENT --
	---------------------
	IF (@Type = 2)
	BEGIN
		---------------------------------
		-- CHECK STATEMENT CAN BE DONE --
		---------------------------------
		IF NOT EXISTS (	SELECT top 1 1
				FROM	BudgetMeter.SourceData src
				WHERE	src.AccountNo			= @AccountNo
					AND	src.ContractNo			= @ContractNo
					AND	src.ProcessingStatus	= ''To Invoice''
					AND	src.CODAExists			= 1
					AND src.CODAReconciliation	= 1
					AND src.ConsumptionExists	= 1
				)
		BEGIN
			SET  @ErrorMessage = ''STATEMENT CANNOT BE DONE FOR CONTRACT: '''''' + @ContractNo + '''''' BECAUSE THERE ARE NO LINES TO MAKE A STATEMENT FOR! '';
			RETURN 1;
		END;

		----------------------------
		-- CHECK STATEMENT EXISTS --
		----------------------------
		SELECT	@salesId		= inv.IUS_INV_SALESID
			,	@StatementDate	= inv.INVOICEDATE
		FROM	dbo.CUSTINVOICETABLE inv
		WHERE	inv.IUS_INV_BILLINGCATEGORY IN(1,3,4) -- STATEMENT - STATEMENT & ACCOUNT - FINAL STATEMENT --
			AND inv.POSTED					= 1
			AND inv.IUS_INV_CREDITEDSALESID = '''' -- NOT CREDITED
			AND	inv.INVOICEID				= @InvoiceNo
			AND	inv.INVOICEACCOUNT			= @AccountNo;

		IF (@SalesId IS NULL OR @SalesId = '''')
		BEGIN
			SET @ErrorMessage = ''STATEMENT: ''''''+ @InvoiceNo + '''''' DOES NOT EXIST!'';
			RETURN 1;
		END;
		--------------------------------------
		-- CHECK INVOICE FOR THIS CONCTRACT --
		--------------------------------------		
		IF (
			SELECT	top 1 1
			FROM	dbo.IUS_INV_INVOICELINEDETAIL det
			WHERE	det.AGREEMENTID = @ContractNo
				AND	det.SALESID		= @SalesId
			) IS NULL
		BEGIN
			SET @ErrorMessage = ''INVOICE: '''''' + @InvoiceNo + '''''' DOES NOT EXISTS FOR THE CONTRACT '''''' + @ContractNo + ''''''!'';
			RETURN 1;
		END;
	END;
	
	------------------
	-- CHECK AMOUNT --
	------------------

	IF (@Type = 1)
	BEGIN
	
		----------------
		-- GET AMOUNT --
		----------------
		SELECT @AmountToInvoice = SUM(src.Amount) / 1.21
		FROM BudgetMeter.SourceData src
		WHERE	src.ContractNo	= @ContractNo
			AND	src.AccountNo	= @AccountNo
			AND src.ProcessingStatus IN (''Open'');	
			
		-- PREPAYMENT --
		IF CONVERT(decimal(20,2),(	SELECT	SUM(det.LINEAMOUNT) 
									FROM	dbo.IUS_INV_INVOICELINEDETAIL det
									WHERE	det.PRICECOMPONENTID NOT LIKE ''E_TAX%''
										AND	det.PRICECOMPONENTID NOT LIKE ''G_TAX%''
										AND det.SALESID = @SalesId
								)) != @AmountToInvoice
		BEGIN
			SET @ErrorMessage = ''INVOICE AMOUNT NOT EQUAL TO TALEXUS AMOUNT!'';
			RETURN 1;
		END;

	END;
	IF (@Type = 2)
	BEGIN
		----------------
		-- GET AMOUNT --
		----------------
		SELECT @AmountToInvoice = SUM(src.Amount) / 1.21
		FROM BudgetMeter.SourceData src
		WHERE	src.ContractNo	= @ContractNo
			AND	src.AccountNo	= @AccountNo
			AND src.ProcessingStatus IN (''To invoice'');
			
		-- STATEMENT --
		IF	CONVERT(decimal(20,2),(		SELECT	SUM(det.LINEAMOUNT)*-1
										FROM	dbo.[IUS_INV_INVOICELINEDETAIL] det
										INNER JOIN dbo.[IUS_INV_PENDINGINVOICELINE] pil
										ON pil.RECID = det.PENDINGLINEREFRECID
										INNER JOIN dbo.[DIMENSIONATTRIBUTEVALUECOMBINATION] as gl
										ON gl.RECID = pil.LEDGERDIMENSION
										WHERE	det.PENDINGLINEREFRECID != 0
											AND	det.SALESID = @SalesId
											AND gl.DISPLAYVALUE like ''701996%''
									))!= @AmountToInvoice
		BEGIN
			SET @ErrorMessage = ''INVOICE DISCOUNT AMOUNT NOT EQUAL TO TALEXUS AMOUNT!'';
			RETURN 1;
		END;
	END;

	----------------------------------
	-- ALL OKE -> UPDATE THE SOURCE --
	----------------------------------
	IF	(@Type = 1)
	BEGIN
		
		-- UPDATE PREPAYMENT --
		UPDATE	src
			SET		src.prepaymentReference = @InvoiceNo
				,	src.ProcessingStatus	=	CASE	WHEN src.ConsumptionExists = 0 THEN ''Prepayed''
														ELSE ''Booking''
												END
				,	src.PrepaymentUser		= SUSER_SNAME()
				,	src.PrepaymentDate		= GETDATE()
		FROM	BudgetMeter.SourceData src
		WHERE	src.ContractNo			= @ContractNo
			AND src.AccountNo			= @AccountNo
			AND	src.ProcessingStatus	= ''Open''
			ANd	src.CodaExists			= 1
			AND	src.CODAReconciliation	= 1;
	END;

	IF	(@Type = 2)
	BEGIN
		-- UPDATE STATEMENT --
		UPDATE	src
			SET		src.StatementReference		= @InvoiceNo
				,	src.statementReferenceDate	= @StatementDate
				,	src.ProcessingStatus		= ''Invoiced''
				,	src.StatementUser			= SUSER_SNAME()
				,	src.StatementDate			= GETDATE()
		FROM	BudgetMeter.SourceData src
		WHERE	src.ContractNo			= @ContractNo
			AND	src.AccountNo			= @AccountNo
			AND	src.ProcessingStatus	= ''To Invoice''
			ANd	src.CodaExists			= 1
			AND src.ConsumptionExists	= 1
			AND	src.CODAReconciliation	= 1;
	END;
	
	SET @ErrorMessage = '''';

	RETURN 0;

END' 
END
GO
/****** Object:  StoredProcedure [BudgetMeter].[USP_UpdateSourceDataErrors]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[USP_UpdateSourceDataErrors]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [BudgetMeter].[USP_UpdateSourceDataErrors]
	@CODAReference		varchar(50)	,
	@EAN				varchar(18)	,	
	@ChargeDate			date,	
	@CheckedComment		varchar(250),
	@ErrorMessage		varchar(200) output
as
BEGIN
	BEGIN TRY
		UPDATE src
		SET		src.MasterdataChecked = 1
			,	src.ProcessingStatus = ''No Delivery''
			,	src.CheckedComment = @CheckedComment
			,	src.ErrorMessage = ''''
		FROM BudgetMeter.SourceData src
		WHERE	src.ean				= @EAN
			AND	src.CODAReference	= @CODAReference
			AND src.ChargeDate		= @ChargeDate;
	END TRY
	BEGIN CATCH
		SET @ErrorMessage = ''ERROR SETTING MASTERDATA CHECKED FOR EAN: '' + @EAN + '' ON REF: '' + @CODAReference + '' '';
		RETURN 1;
	END CATCH
	RETURN 0;

END' 
END
GO
/****** Object:  Table [BudgetMeter].[CODAData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[CODAData]') AND type in (N'U'))
BEGIN
CREATE TABLE [BudgetMeter].[CODAData](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CODARecId] [bigint] NOT NULL,
	[TalexusReference] [varchar](50) NULL,
	[BankAccountID] [varchar](20) NULL,
	[PaymentDate] [date] NULL,
	[Amount] [numeric](32, 16) NULL,
 CONSTRAINT [PK_CODAData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BudgetMeter].[GridOwner]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[GridOwner]') AND type in (N'U'))
BEGIN
CREATE TABLE [BudgetMeter].[GridOwner](
	[GridOwnerID] [int] NOT NULL,
	[GridOwnerName] [varchar](100) NULL,
	[EAN] [varchar](18) NULL,
	[GroupName] [varchar](50) NULL,
 CONSTRAINT [PK_GridOwner] PRIMARY KEY CLUSTERED 
(
	[GridOwnerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BudgetMeter].[SourceData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[SourceData]') AND type in (N'U'))
BEGIN
CREATE TABLE [BudgetMeter].[SourceData](
	[ID] [uniqueidentifier] NOT NULL,
	[TalexusHeaderID] [uniqueidentifier] NOT NULL,
	[EAN] [varchar](18) NOT NULL,
	[Amount] [decimal](10, 4) NOT NULL,
	[ChargeDate] [date] NOT NULL,
	[GridOwnerID] [int] NOT NULL,
	[TalexusReference] [varchar](12) NOT NULL,
	[CODAReference] [varchar](12) NOT NULL,
	[ProcessingStatus] [varchar](20) NOT NULL,
	[PrePaymentReference] [varchar](20) NULL,
	[StatementReference] [varchar](20) NULL,
	[ConsumptionExists] [bit] NULL,
	[ContractNo] [varchar](20) NULL,
	[CODAExists] [bit] NULL,
	[ErrorMessage] [varchar](250) NULL,
	[PrepaymentUser] [varchar](256) NULL,
	[PrepaymentDate] [datetime] NULL,
	[StatementUser] [varchar](256) NULL,
	[StatementDate] [datetime] NULL,
	[AccountNo] [varchar](20) NULL,
	[Booking] [bit] NULL,
	[CODAReconciliation] [bit] NULL,
	[MasterdataChecked] [bit] NULL,
	[StatementReferenceDate] [date] NULL,
	[CheckedComment] [varchar](250) NULL,
 CONSTRAINT [PK_SourceData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BudgetMeter].[SSISConfigurations]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[SSISConfigurations]') AND type in (N'U'))
BEGIN
CREATE TABLE [BudgetMeter].[SSISConfigurations](
	[ConfigurationFilter] [nvarchar](255) NOT NULL,
	[ConfiguredValue] [nvarchar](255) NULL,
	[PackagePath] [nvarchar](255) NOT NULL,
	[ConfiguredValueType] [nvarchar](20) NOT NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [BudgetMeter].[TalexusBody]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[TalexusBody]') AND type in (N'U'))
BEGIN
CREATE TABLE [BudgetMeter].[TalexusBody](
	[HeaderID] [uniqueidentifier] NOT NULL,
	[RowNumber] [varchar](5) NOT NULL,
	[ID_SUPPLIER] [int] NULL,
	[GenerationDate] [varchar](10) NULL,
	[GenerationTime] [varchar](5) NULL,
	[EnergyType] [varchar](1) NULL,
	[EAN] [varchar](18) NULL,
	[TalexusClientRef] [varchar](12) NULL,
	[Terminal] [varchar](4) NULL,
	[Amount] [float] NULL,
	[SalesType] [int] NULL,
 CONSTRAINT [PK_TalexusBody] PRIMARY KEY CLUSTERED 
(
	[HeaderID] ASC,
	[RowNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [BudgetMeter].[TalexusHeader]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[BudgetMeter].[TalexusHeader]') AND type in (N'U'))
BEGIN
CREATE TABLE [BudgetMeter].[TalexusHeader](
	[HeaderID] [uniqueidentifier] NOT NULL,
	[RowNumber] [varchar](5) NULL,
	[ID_GRD] [int] NULL,
	[ID_SUPPLIER] [int] NULL,
	[GenerationDate] [varchar](10) NULL,
	[GenerationTime] [varchar](5) NULL,
	[TalexusCodaRef] [varchar](12) NULL,
	[DeltaPlus] [float] NULL,
	[DeltaMinus] [float] NULL,
	[RowCount] [int] NULL,
	[Filename] [varchar](250) NULL,
	[Processed] [bit] NULL,
 CONSTRAINT [PK_TalexusHeader] PRIMARY KEY CLUSTERED 
(
	[HeaderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [BudgetMeter].[vw_AllSourceData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_AllSourceData]'))
EXEC dbo.sp_executesql @statement = N'


CREATE VIEW [BudgetMeter].[vw_AllSourceData]
AS

	SELECT	src.ContractNo 
		,	src.AccountNo 
		,	src.ChargeDate 
		,	src.CODAExists
		,	'''''''' + src.CODAReference  + '''''''' as CODAReference
		,	ISNULL(cod.PaymentDate,''1900-01-01'') as PaymentDate
		,	src.ConsumptionExists
		,	'''''''' + src.EAN  + '''''''' as EAN
		,	src.ErrorMessage
		,	src.ProcessingStatus
		,	src.PrePaymentReference 
		,	src.StatementReference 
		,	ISNULL(src.StatementReferenceDate,''1900-01-01'') as StatementDate
		,	src.Booking 
		,	CONVERT(decimal(20,5),src.Amount / 1.21) as AmountExcl 
		,	CONVERT(decimal(20,5),src.Amount) as AmountIncl 
		,	'''''''' + src.TalexusReference  + '''''''' as TalexusReference
		,	grd.GridOwnerName
	FROM	BudgetMeter.SourceData src
	INNER JOIN BudgetMeter.GridOwner grd
		ON grd.GridOwnerID = src.GridOwnerID
	LEFT JOIN BudgetMeter.CODAData cod
		ON cod.TalexusReference = src.CODAReference



' 
GO
/****** Object:  View [BudgetMeter].[vw_CODAData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_CODAData]'))
EXEC dbo.sp_executesql @statement = N'

CREATE VIEW [BudgetMeter].[vw_CODAData]
as

	----------------------------------------
	-- GET THE CODA DATA FOR THE BOOKINGS --
	----------------------------------------
	SELECT	src.contractNo + ''_'' + src.AccountNo  as ID
		,	convert(decimal(20,5),SUM(src.Amount)) as AmountIncl
		,	src.CODAReference
		,	cod.PaymentDate
	FROM	BudgetMeter.SourceData src
	INNER JOIN BudgetMeter.CODAData cod
	ON cod.TalexusReference = src.CODAReference
	WHERE	src.ProcessingStatus IN (''Booking'')
	GROUP BY src.ContractNo,src.AccountNo,src.CODAReference,cod.PaymentDate;






' 
GO
/****** Object:  View [BudgetMeter].[vw_ErrorData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_ErrorData]'))
EXEC dbo.sp_executesql @statement = N'


CREATE VIEW [BudgetMeter].[vw_ErrorData]
as

	-----------------------------------
	-- GET THE ERROR DATA TO DISPLAY --
	-----------------------------------

	SELECT	src.CODAReference
		,	grd.GridOwnerName
		,	convert(decimal(20,5),SUM(src.Amount)) as TalexusAmountError
		,	convert(decimal(20,5),(
									SELECT	SUM(tot.Amount)
									FROM	BudgetMeter.SourceData Tot
									WHERE	Tot.CODAReference = src.CODAReference
								)) as TalexusAmount
		,	ISNULL((
						SELECT  SUM(Amount)
						FROM	BudgetMeter.CODAData dt
						WHERE	dt.TalexusReference = src.CODAReference
					),0) as CODAAmount
	FROM	BudgetMeter.SourceData src
	INNER JOIN BudgetMeter.GridOwner grd
	ON grd.GridOwnerID = src.GridOwnerID
	WHERE	src.ProcessingStatus IN (''Error'')
	GROUP BY src.CODAReference,grd.GridOwnerName


' 
GO
/****** Object:  View [BudgetMeter].[vw_ErrorDataDetails]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_ErrorDataDetails]'))
EXEC dbo.sp_executesql @statement = N'




CREATE VIEW [BudgetMeter].[vw_ErrorDataDetails]
as

	-----------------------------------
	-- GET THE ERROR DATA TO DISPLAY --
	-----------------------------------
	SELECT	src.CODAReference + ''_'' + src.EAN + ''_'' + convert(varchar(8),src.ChargeDate,112) as ID
		,	src.CODAReference
		,	grd.GridOwnerName
		,	src.EAN
		,	src.ErrorMessage
		,	src.MasterdataChecked
		,	convert(decimal(20,5),SUM(src.Amount)) as TalexusAmount
		,	ISNULL((
						SELECT  SUM(Amount)
						FROM	BudgetMeter.CODAData dt
						WHERE	dt.TalexusReference = src.CODAReference
					),0) as CODAAmount
		,	src.ChargeDate
	FROM	BudgetMeter.SourceData src
	INNER JOIN BudgetMeter.GridOwner grd
	ON grd.GridOwnerID = src.GridOwnerID
	WHERE	src.ProcessingStatus = ''Error''
	GROUP BY src.CODAReference,grd.GridOwnerName, src.EAN,src.ErrorMessage, src.MasterdataChecked, src.ChargeDate




' 
GO
/****** Object:  View [BudgetMeter].[vw_ErrorDataDetailsExport]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_ErrorDataDetailsExport]'))
EXEC dbo.sp_executesql @statement = N'



CREATE VIEW [BudgetMeter].[vw_ErrorDataDetailsExport]
as

	-----------------------------------
	-- GET THE ERROR DATA TO DISPLAY --
	-----------------------------------
	SELECT	src.CODAReference + ''_'' + src.EAN as ID
		,	'''''''' + src.CODAReference + ''''''''  as CODAReference
		,	grd.GridOwnerName
		,	'''''''' + src.EAN + '''''''' as EAN
		,	src.ErrorMessage
		,	src.MasterdataChecked
		,	convert(decimal(20,5),SUM(src.Amount)) as TalexusAmount
		,	ISNULL((
						SELECT  SUM(Amount)
						FROM	BudgetMeter.CODAData dt
						WHERE	dt.TalexusReference = src.CODAReference
					),0) as CODAAmount
		,	src.CODAReference as CODARef
		,	src.ChargeDate
	FROM	BudgetMeter.SourceData src
	INNER JOIN BudgetMeter.GridOwner grd
	ON grd.GridOwnerID = src.GridOwnerID
	WHERE	src.ProcessingStatus = ''Error''
	GROUP BY src.CODAReference,grd.GridOwnerName, src.EAN,src.ErrorMessage, src.MasterdataChecked,	src.ChargeDate



' 
GO
/****** Object:  View [BudgetMeter].[vw_InitialSourceData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_InitialSourceData]'))
EXEC dbo.sp_executesql @statement = N'



CREATE VIEW [BudgetMeter].[vw_InitialSourceData]
AS
	SELECT	REPLACE(src.contractNo + ''_'' + src.AccountNo,'' '','''') as ID
		,	src.ContractNo
		,	src.AccountNo
		,	src.ProcessingStatus
		,	src.ErrorMessage
		,	'''''''' + src.EAN   + '''''''' as EAN
		,	CONVERT(decimal(20,5),src.Amount / 1.21) as AmountExcl
		,	convert(decimal(20,5),src.Amount) as AmountIncl
		,	src.PrePaymentReference
		,	src.StatementReference
		,	grd.GridOwnerName
	FROM	BudgetMeter.SourceData src
	INNER JOIN BudgetMeter.GridOwner grd
	ON grd.GridOwnerID = src.GridOwnerID
	WHERE	src.ProcessingStatus IN (''Open'',''To invoice'',''Prepayed'',''Booking'')





' 
GO
/****** Object:  View [BudgetMeter].[vw_SourceData]    Script Date: 12/04/2013 7:32:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[BudgetMeter].[vw_SourceData]'))
EXEC dbo.sp_executesql @statement = N'




CREATE VIEW [BudgetMeter].[vw_SourceData]
as

	------------------------------------
	-- GET THE SOURCE DATA TO DISPLAY --
	------------------------------------
	SELECT	src.contractNo + ''_'' + src.AccountNo  as ID
		,	src.ContractNo
		,	src.AccountNo
		,	src.ProcessingStatus
		,	src.ErrorMessage
		,	src.EAN
		,	CONVERT(decimal(20,5),SUM(src.Amount) / 1.21) as AmountExcl
		,	convert(decimal(20,5),SUM(src.Amount)) as AmountIncl
		,	src.PrePaymentReference
		,	src.StatementReference
		,	src.booking
		,	grd.GridOwnerName
	FROM	BudgetMeter.SourceData src
	INNER JOIN BudgetMeter.GridOwner grd
	ON grd.GridOwnerID = src.GridOwnerID
	WHERE	src.ProcessingStatus IN (''Open'',''To invoice'',''Prepayed'',''Booking'')
	GROUP BY src.ContractNo,src.AccountNo,src.ProcessingStatus,src.ErrorMessage,src.EAN, src.PrePaymentReference,src.StatementReference,src.booking,grd.GridOwnerName;





' 
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_SourceData_ProcessingStatus]    Script Date: 12/04/2013 7:32:55 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[BudgetMeter].[SourceData]') AND name = N'IX_SourceData_ProcessingStatus')
CREATE NONCLUSTERED INDEX [IX_SourceData_ProcessingStatus] ON [BudgetMeter].[SourceData]
(
	[ProcessingStatus] ASC,
	[CODAExists] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
