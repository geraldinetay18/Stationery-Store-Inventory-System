# Stationery-Store-Inventory-System
An ASP.NET Web App built with National University of Singapore, Institute of Systems Science. 
This app provides inventory management, requisitions, approval, email and charting functions. It syncs with an Android app.

## Screenshots
![](AD\ Project\ SA\ 47\ -\ Team\ 10\ (ASP Web)/ADProject_Team10/Images/SSIS1.png)

![](AD Project SA 47 - Team 10 (ASP Web)/ADProject_Team10/Images/SSIS2.png)

![](AD Project SA 47 - Team 10 (ASP Web)/ADProject_Team10/Images/SSIS3.png)

![](AD Project SA 47 - Team 10 (ASP Web)/ADProject_Team10/Images/SSIS4.png)

![](AD Project SA 47 - Team 10 (ASP Web)/ADProject_Team10/Images/SSIS5.png)

## Contributors
- **All**: Database design
- **Geraldine:** Integration (ASP.NET), Stock Adjustment-related (adding, approve/reject, escalation), Low stock, List of stock cards
- **Kyler:** Integration (Android), Authentication, change Department Representative/ Acting Department Head
- **Saphira:** Retrieval, Allocation, Disbursement
- **Priyanga & Shalin:** Reorder levels, Update inventory, Collection point, change Collection Point
- **Trang:** Stationery Collection, Stock Card details
- **Pengkai:** Purchase Orders, Suppliers, Chart displays
- **Cheng Yuan:** Requisition-related (create, approve/reject, view history)

## Main Features (by workflow)
- Staff makes requisitions for stationery, where Department Head approve/rejects.
- Approved requisitions are sent to Store Clerk.
- Store Clerk creates a summarized Retrieval list of all stationery from all departments. When there is insufficient stock, Clerk can (1) re-allocate stationery proportion to departments, (2) submit stock adjustment, (3) create purchase orders.
- Supervisor will approve the stock adjustments, leading to successful change in inventory, as reflected in the Stock Cards.
- Store Manager can view charts displaying the purchase and requisition trends.

## Running the project
- Import the database SSIS10.bacpac into SSMS 
- Open the .sln file to open the ASP.NET project in Visual Studio. Click run (on development server)
