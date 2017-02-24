# Specs:

* Employee enters new Stylist info, and can view that info
    - input "Melvin", "206-437-4055"
    - output "Melvin", "206-437-4055"

* The new Stylist info will be saved to a database
    - Database query: Stylist
    - output: Stylist

* Employees can go to specific pages of Stylists and see their database
    - Page reference /specific_stylist
    - output: Stylist Info
    - (this is demonstrated in tests by using the Find method)

* Employees can add new client info and view that info
    - input "Dude", 1
    - output "Dude", 1

* Employees can remove clients from database
    - delete client 1
    - query database for client 1
    - client 1 not found

* The main page has employee drop downs that let you view their client list, with the option of adding additional clients
