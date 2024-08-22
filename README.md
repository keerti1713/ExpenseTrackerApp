# Expense Tracker App

This is a personal expense tracker application built with ASP.NET Core MVC. 

## Features

Add, edit and delete transactions.
View total income, expenses, and balance.
Display recent transactions from the past month.
Analyze spending patterns with a donut chart.

## Setup Instructions

Change Server Name and connectionString name:
   - Open appsettings.json.
   - Locate the ConnectionStrings section and update the DefaultConnection string with your server details.
   
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;TrustServerCertificate=Yes;"
     }
   }
