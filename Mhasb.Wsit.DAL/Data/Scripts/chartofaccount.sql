

INSERT [acc].[chartofaccount] (acode,aname,alevel,showsnsashboard,showinexpenseclaims,iscostcenter) 
							VALUES (N'1', N'Assets',1, 0, 0, 0),
							 (N'2', N'Liabilities',1, 0, 0, 0),
							 (N'3', N'Equity',1, 0, 0, 0),
							 (N'4', N'Expenses',1, 0, 0, 0),
							 (N'5', N'Revenue',1, 0, 0, 0),
							--Assets
							 (N'101', N'Non Current Assets',2, 0, 0, 0),
							 (N'102', N'Current Assets',2, 0, 0, 0),
							
							 (N'10101', N'Motor Vehicle',3, 0, 0, 0),
							 (N'10102', N'Electrical Equipments',3, 0, 0, 0),
							 (N'10103', N'Medical Equipments',3, 0, 0, 0),
							 (N'10104', N'Other Fixed Assets',3, 0, 0, 0),
							
							 (N'10201', N'Cash',3, 0, 0, 0),
							
							-- Liability
							 (N'201', N'Long Term Liabilities',2, 0, 0, 0),
							 (N'202', N'Current Liabilities',2, 0, 0, 0),
							
							-- Equity							    
							 (N'301', N'Shareholders Equity',2, 0, 0, 0),
							--Expenses
							 (N'401', N'Direct Expense',2, 0, 0, 0),
							 (N'402', N'Indirect Expense',2, 0, 0, 0),
							
							 (N'40101', N'Utility Bill',3, 0, 0, 0),
							 (N'40102', N'General Expenses',3, 0, 0, 0),
							
							 (N'40201', N'Marketing Expenses',3, 0, 0, 0),
							 (N'40202', N'Financial Expenses',3, 0, 0, 0),
							
							-- Revenue
							 (N'501', N'Direct Income',2, 0, 0, 0),
							 (N'502', N'Indirect Income',2, 0, 0, 0),
							
							 (N'50101', N'Software Service',3, 0, 0, 0),
							
							 (N'50201', N'Non Operating Income',3, 0, 0, 0)
INSERT INTO [acc].[vouchertype]([name],[code])
								 VALUES('New journal','01'),
								 ('Debit Voucher','02'),
								 ('Credit Voucher','03'),
								 ('Repeating Journal','04');


Insert into com.industries(industry_name)
						VALUES('Software Company'),
						('Developer Firm');

Insert into org.designations(designation_name)
						VALUES('Owner'),
						('Software Develope');

Insert into com.legal_entities(legal_entity_name)
						VALUES('Individual Ownership'),
						('Limited Liability'),
						('Private Shareholding'),
						('Public Shareholding');

GO
							