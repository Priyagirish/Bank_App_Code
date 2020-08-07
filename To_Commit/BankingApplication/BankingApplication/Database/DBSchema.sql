CREATE DATABASE BankApp
GO

USE [BankApp]
Go

CREATE TABLE Customer (

    CustomerID INT IDENTITY(1,1) ,
    AccountNumber INT PRIMARY KEY,
    CustomerName VARCHAR(50),
    [Address] VARCHAR(500),
    AccountBalance DECIMAL
);


CREATE TABLE CustomerTransaction (
	CustomerTransactionID INT IDENTITY(1,1) PRIMARY KEY,
	TransactionDateTime DATETIME,
	Amount DECIMAL,
	srcAccountNumber INT FOREIGN KEY REFERENCES Customer(AccountNumber),
	destAccountNumber INT FOREIGN KEY REFERENCES Customer(AccountNumber)
);




