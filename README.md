# Stationery-Store-Inventory-System
An ASP.NET Web App built with National University of Singapore, Institute of Systems Science. 
This app provides inventory management, requisitions, approval, email and charting functions. It syncs with an Android app.

## Contributors
- **All**: Database design
- **Geraldine:** Integration (ASP.NET), Stock Adjustment-related (adding, approve/reject, escalation), Low stock, List of stock cards
- **Kyler:** Integration (Android), Authentication, Role allocation
- **Saphira:** Retrieval, Allocation, Disbursement
- **Priyanga & Shalin:** Reorder levels, Update inventory, Collection point
- **Trang:** Stationery Collection, Stock Card details
- **Pengkai:** Purchase Orders, Suppliers, Chart displays
- **Cheng Yuan:** Requisition-related (create, approve/reject, view history)

## Main Features (by workflow)
- Staff makes requisitions for stationery, where Department Head approve/rejects.
- Approved requisitions are sent to Store Clerk.
- Store Clerk creates a summarized Retrieval list of all stationery from all departments. When there is insufficient stock, Clerk can (1) re-allocate stationery proportion to departments, (2) submit stock adjustment, (3) create purchase orders.
- Supervisor will approve the stock adjustments, leading to successful change in inventory, as reflected in the Stock Cards.
- Store Manager can view charts displaying the purchase and requisition trends.

## Screenshots
![](foodiereviews/reviewapp/static/reviewapp/images/screenshot1.PNG)

![](foodiereviews/reviewapp/static/reviewapp/images/screenshot2.PNG)

![](foodiereviews/reviewapp/static/reviewapp/images/screenshot3.PNG)

## Running the project
- Import the database SSIS10.bacpac into SSMS 
- Open the .sln file to open the ASP.NET project in Visual Studio. Click run (on development server)
