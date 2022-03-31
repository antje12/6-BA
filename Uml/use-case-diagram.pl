@startuml
skinparam shadowing false
left to right direction

skinparam nodesep 5
skinparam ranksep 5

together {
    actor "User" as u  #00FF00
    actor "Service Consumer" as sc #0000FF
    actor "Service Provider" as sp #FF0000
}

sc .> u
sp .> u

rectangle "I Know A Guy" {
 usecase "Create profile" as UC1
 usecase "Log in" as UC2

    together {
        usecase "View profile" as UC3
        usecase "Update profile" as UC31
        usecase "Delete profile" as UC32
        usecase "Request GDPR user info" as UC33
    }

    together {
        usecase "View contract" as UC4
        usecase "Write review" as UC41
    }

 usecase "Contact support" as UC5
 usecase "Get NemID validation" as UC6
 usecase "Get CV from LinkedIn" as UC7

    together {
    usecase "Requesting a service" as UC8
    usecase "Look through available jobs" as UC9
    usecase "Making an offer" as UC10
    usecase "View offers for job" as UC11
    usecase "Accepting an offer" as UC12
    usecase "Adjusting an offer" as UC13
    usecase "Declining an offer" as UC14
    usecase "Concluding the contract" as UC15
    }
}

together {
    actor "Auth0" as a #808080
    actor "Mail Client" as mc #808080
    actor "NemID" as n #808080
    actor "Linkedin" as l #808080
}

u -- UC1 #00FF00
u -- UC2 #00FF00
u -- UC3 #00FF00
u -- UC4 #00FF00
u -- UC5 #00FF00
u -- UC6 #00FF00
u -- UC7 #00FF00

UC1 -- a #808080
UC2 -- a #808080

UC3 <.. UC31
UC3 <.. UC32
UC3 <.. UC33

UC4 <.. UC41

UC5 --- mc #808080
UC6 --- n #808080
UC7 --- l #808080

sc -- UC8 #0000FF
sc -- UC9 #0000FF
sc -- UC10 #0000FF
sc -- UC11 #0000FF
sc -- UC12 #0000FF
sc -- UC13 #0000FF
sc -- UC14 #0000FF
sc -- UC15 #0000FF

sp -- UC8 #FF0000
sp -- UC9 #FF0000
sp -- UC10 #FF0000
sp -- UC11 #FF0000
sp -- UC12 #FF0000
sp -- UC13 #FF0000
sp -- UC14 #FF0000
sp -- UC15 #FF0000
@enduml