# Product List - Checkpoint2
Simple console application to manage a product list.  

## Menu  
<img width="294" height="301" alt="image" src="https://github.com/user-attachments/assets/a946973f-e595-4b3e-aaef-6d612f9d5c3e" />

## Add Product
Enter Category, Name and Price.  
You add products one by one until you enter "q" as a Category.  
When entering "q" you will be taken back to the menu.  

## Show Products
Shows a list of all the Products currently in the system and the total price for all the Products.  
  
<img width="393" height="347" alt="image" src="https://github.com/user-attachments/assets/6d3696b4-7b57-44bf-af62-1249fc000ef0" />

## Search Product
The system will try and find Products by matching the search string you enter with part of the Product Name or Category.  
The system will list all the Products and highlight all product that match your search criteria.  
  
<img width="402" height="322" alt="image" src="https://github.com/user-attachments/assets/0531cfc1-c952-4fbd-bea7-e0686e54ad74" />


## Edit Product  
Enter a product Id. If a Product is found it will be shown. Then you can update the Category, Name and/or Price. But you need to save products to persist your changes.  
  
<img width="508" height="325" alt="image" src="https://github.com/user-attachments/assets/fa6648f3-0a30-4769-9139-846dded685d8" />  


## Delete Product  
Enter a product Id. If a Product is found it will be removed from the system. But you need to save products to persist the removal.  
  
<img width="469" height="223" alt="image" src="https://github.com/user-attachments/assets/2503e171-8ef2-44fc-9ab1-b305ff8ac142" />  


## Statistics  
<img width="300" height="398" alt="image" src="https://github.com/user-attachments/assets/619229d5-4479-4560-b678-84a9aa7ec93c" />  

## Save Products  
Saves the Products currently in the system to a JSON file.

## Load Products  
If the a JSON file exists it will be loaded into the system, otherwise the system will have an empty list of Products.  

## Exit  
Stops the system from running. Any unsaved changes will be lost, so remember to save the Products before exiting.  
